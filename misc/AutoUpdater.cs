using System;
using System.Diagnostics;
using System.Formats.Tar;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ImGuiNET;
using RecompOne.Runtime.Config;
using RecompOne.Runtime.Host;
using RecompOne.Runtime.Host.Window;

namespace Recompiled;

public static class AutoUpdater //should be generic enough to use on other recomps
{
    const string Repo = "BlackLabelHQ/SymphonyRecomp";
    const string ReleasesUrl = "https://github.com/" + Repo + "/releases/latest";
    const string ApiUrl = "https://api.github.com/repos/" + Repo + "/releases/latest";
    const string ApplyArg = "--apply-update";
    const string UserAgent = "SymphonyRecomp-AutoUpdater";
    const string EnabledKey = "AutoUpdateEnabled";
    const string SkipTagKey = "AutoUpdateSkipTag";

    enum Phase { Idle, Checking, Available, Downloading, Applying, Failed }

    static volatile Phase _phase = Phase.Idle;
    static volatile string _latestTag = "";
    static volatile string _releaseNotes = "";
    static volatile string _assetUrl = "";
    static volatile string _error = "";
    static volatile bool _dismissed;
    static volatile bool _popupOpen;
    static CancellationTokenSource? _cancel;
    static long _downloaded;
    static long _total;

    public static string? CurrentTag
    {
        get
        {
            var v = Assembly.GetEntryAssembly()?
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            if (string.IsNullOrEmpty(v)) return null;
            int plus = v.IndexOf('+');
            if (plus >= 0) v = v[..plus];
            return v.StartsWith("v", StringComparison.OrdinalIgnoreCase) ? v : null;
        }
    }

    static string InstallDir => AppContext.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar);
    static string ExePath => Environment.ProcessPath ?? "";
    static string WorkDir => Path.Combine(Path.GetTempPath(), "SymphonyRecomp-update");
    static string StagingDir => Path.Combine(WorkDir, "staging");

    public static bool HandleRelaunch(string[] args)
    {
        if (args.Length < 5 || args[0] != ApplyArg) return false;

        string staging = args[1];
        string target = args[2];
        string exe = args[4];
        _ = int.TryParse(args[3], out int pid);

        Log($"waiting for pid {pid} to exit before writing to {target}");
        WaitForExit(pid);
        try
        {
            CopyTree(staging, target);
            Log("files replaced, relaunching");
        }
        catch (Exception e)
        {
            Log($"failed to apply update: {e.Message}");
        }

        MakeExecutable(exe);
        try
        {
            Process.Start(new ProcessStartInfo(exe) { WorkingDirectory = target, UseShellExecute = false });
        }
        catch (Exception e)
        {
            Log($"failed to relaunch: {e.Message}");
        }

        return true;
    }

    public static void Register()
    {
        TryDelete(WorkDir);
        CleanStaleFiles();
        PanelManager.Register(new UpdatePanel());
        MenuRegistry.Register("Updates", DrawMenuItems, null, 500);

        if (CurrentTag == null)
        {
            Log("development build, update check skipped");
            return;
        }

        if (!ConfigManager.View.GetBool(EnabledKey, true))
        {
            Log($"running {CurrentTag}, update check disabled in settings");
            return;
        }

        _phase = Phase.Checking;
        Log($"running {CurrentTag}, checking {Repo} for updates...");
        Task.Run(CheckAsync);
    }

    static async Task CheckAsync()
    {
        try
        {
            using var http = NewClient(TimeSpan.FromSeconds(15));
            using var doc = JsonDocument.Parse(await http.GetStringAsync(ApiUrl));
            var root = doc.RootElement;

            string tag = root.GetProperty("tag_name").GetString() ?? "";
            if (string.IsNullOrEmpty(tag))
            {
                Log("the latest release have no tag");
                _phase = Phase.Idle;
                return;
            }

            if (tag == CurrentTag)
            {
                Log($"up to date, {tag} is the latest release");
                _phase = Phase.Idle;
                return;
            }

            if (tag == ConfigManager.View.GetString(SkipTagKey))
            {
                Log($"{tag} is available but is skipped, staying on {CurrentTag}");
                _phase = Phase.Idle;
                return;
            }

            string suffix = AssetSuffix();
            foreach (var asset in root.GetProperty("assets").EnumerateArray())
            {
                string name = asset.GetProperty("name").GetString() ?? "";
                if (!name.EndsWith(suffix, StringComparison.OrdinalIgnoreCase)) continue;
                _assetUrl = asset.GetProperty("browser_download_url").GetString() ?? "";
                break;
            }

            if (string.IsNullOrEmpty(_assetUrl))
            {
                Log($"{tag} is available but it ships no *{suffix} build for this platform, is this an error?");
                _phase = Phase.Idle;
                return;
            }

            _latestTag = tag;
            _releaseNotes = (root.TryGetProperty("body", out var body) ? body.GetString() : null)?.Trim() ?? "";
            _phase = Phase.Available;
            Log($"update available: {tag} (running {CurrentTag})");
        }
        catch (Exception e)
        {
            Log($"update check failed: {e.Message}");
            _phase = Phase.Idle;
        }
    }

    static void Log(string message) => Console.WriteLine($"[AutoUpdater] {message}");

    static async Task UpdateAsync(CancellationToken token)
    {
        try
        {
            _phase = Phase.Downloading;
            Log($"downloading {_latestTag} from {_assetUrl}");
            TryDelete(WorkDir);
            Directory.CreateDirectory(WorkDir);

            string suffix = AssetSuffix();
            string archive = Path.Combine(WorkDir, "release" + suffix[(suffix.IndexOf('.'))..]);

            using (var http = NewClient(TimeSpan.FromMinutes(30)))
            using (var response = await http.GetAsync(_assetUrl, HttpCompletionOption.ResponseHeadersRead, token))
            {
                response.EnsureSuccessStatusCode();
                _total = response.Content.Headers.ContentLength ?? 0;
                _downloaded = 0;

                await using var src = await response.Content.ReadAsStreamAsync(token);
                await using var dst = File.Create(archive);
                var buffer = new byte[81920];
                int read;
                while ((read = await src.ReadAsync(buffer, token)) > 0)
                {
                    await dst.WriteAsync(buffer.AsMemory(0, read), token);
                    Interlocked.Add(ref _downloaded, read);
                }
            }

            Log($"downloaded {Interlocked.Read(ref _downloaded) / 1048576f:0.0} MB, extracting");
            Directory.CreateDirectory(StagingDir);
            Extract(archive, StagingDir);

            string exeName = Path.GetFileName(ExePath);
            string staged = Path.Combine(StagingDir, exeName);
            if (!File.Exists(staged)) throw new FileNotFoundException($"{exeName} not found in the release archive");

            MakeExecutable(staged);

            _phase = Phase.Applying;
            Log($"restarting to apply {_latestTag} into {InstallDir}");

            Process.Start(new ProcessStartInfo(staged)
            {
                WorkingDirectory = StagingDir,
                UseShellExecute = false,
                ArgumentList =
                {
                    ApplyArg,
                    StagingDir,
                    InstallDir,
                    Environment.ProcessId.ToString(),
                    ExePath,
                },
            });

            Environment.Exit(0);
        }
        catch (OperationCanceledException)
        {
            Log("download cancelled");
            TryDelete(WorkDir);
            _dismissed = true;
            _phase = Phase.Available;
            _popupOpen = false;
        }
        catch (Exception e)
        {
            Log($"update failed: {e.Message}");
            _error = e.Message;
            _phase = Phase.Failed;
        }
    }

    static void DrawMenuItems()
    {
        ImGui.TextDisabled(StatusLine());
        ImGui.Separator();

        bool enabled = ConfigManager.View.GetBool(EnabledKey, true);
        if (ImGui.MenuItem("Check on Startup", null, enabled))
        {
            ConfigManager.View.SetBool(EnabledKey, !enabled);
            ConfigManager.SaveView(PanelManager.Panels);
        }

        if (ImGui.MenuItem("Check For Updates Now", null, false, _phase is Phase.Idle or Phase.Available))
        {
            if (_phase == Phase.Available) _dismissed = false;
            else
            {
                ConfigManager.View.SetString(SkipTagKey, "");
                _dismissed = false;
                _phase = Phase.Checking;
                Log("manual update check requested");
                Task.Run(CheckAsync);
            }
        }

        if (ImGui.MenuItem("Open releases page")) OpenUrl(ReleasesUrl);
    }

    static string StatusLine() => _phase switch
    {
        Phase.Checking => "Checking for updates...",
        Phase.Available => $"{_latestTag} available",
        Phase.Downloading => $"Downloading {_latestTag}...",
        Phase.Applying => "Restarting to update...",
        Phase.Failed => "Last update attempt failed",
        _ => CurrentTag == null ? "Development Build" : $"Running {CurrentTag}",
    };

    internal static bool ShouldShowUi =>
        !_dismissed && _phase is Phase.Available or Phase.Downloading or Phase.Applying or Phase.Failed;

    internal static void CloseUi()
    {
        if (ShouldShowUi) _dismissed = true;
    }

    internal static void Draw()
    {
        var phase = _phase;
        if (phase is not (Phase.Available or Phase.Downloading or Phase.Applying or Phase.Failed))
        {
            _popupOpen = false;
            return;
        }

        _popupOpen = true;

        var vp = ImGui.GetMainViewport();
        ImGui.SetNextWindowPos(vp.GetCenter(), ImGuiCond.Appearing, new Vector2(0.5f, 0.5f));
        ImGui.SetNextWindowSize(new Vector2(520, 430) * HostWindow.DpiScale, ImGuiCond.Appearing);
        ImGui.SetNextWindowSizeConstraints(new Vector2(420, 280) * HostWindow.DpiScale, new Vector2(1000, 1000) * HostWindow.DpiScale);

        bool open = true;
        bool visible = ImGui.Begin("Update", ref open,
            ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoSavedSettings);

        if (visible)
        {
            DrawHeader(phase);
            ImGui.Spacing();

            float footer = ImGui.GetFrameHeightWithSpacing() + ImGui.GetStyle().ItemSpacing.Y * 2f;
            if (phase is Phase.Downloading or Phase.Applying) footer += ImGui.GetFrameHeightWithSpacing();
            if (phase == Phase.Failed) footer += ImGui.GetTextLineHeightWithSpacing() * 2f;

            ImGui.BeginChild("##notes", new Vector2(0, -footer), ImGuiChildFlags.Border);
            DrawNotes(phase);
            ImGui.EndChild();

            ImGui.Spacing();
            DrawActions(phase);
        }



        ImGui.End();
        if (!open) DismissForSession();
    }

    static void DrawHeader(Phase phase)
    {
        string headline = phase switch
        {
            Phase.Downloading => $"Downloading {_latestTag}",
            Phase.Applying => $"Installing {_latestTag}",
            Phase.Failed => "Update failed",
            _ => $"{_latestTag} is available",
        };

        ImGui.TextUnformatted(headline);
        ImGui.SameLine();
        float tail = ImGui.CalcTextSize($"current {CurrentTag}").X;
        float off = ImGui.GetContentRegionAvail().X - tail;
        if (off > 0) ImGui.SetCursorPosX(ImGui.GetCursorPosX() + off);
        ImGui.TextDisabled($"current {CurrentTag}");
        ImGui.Separator();
    }

    static void DrawNotes(Phase phase)
    {
        if (phase == Phase.Failed)
        {
            ImGui.TextWrapped(_error);
            return;
        }

        if (!CanWriteInstallDir())
        {
            ImGui.TextWrapped("This folder is not writable, so the update cannot be installed automatically. Download it manually or move the game somewhere you can write to.");
            ImGui.Spacing();
        }

        if (string.IsNullOrWhiteSpace(_releaseNotes))
        {
            ImGui.TextDisabled("This release ships no notes.");
            return;
        }

        foreach (var line in _releaseNotes.Replace("\r", "").Split('\n'))
        {
            if (line.Length == 0) { ImGui.Spacing(); continue; }
            if (line.StartsWith('#'))
            {
                ImGui.Spacing();
                ImGui.SeparatorText(line.TrimStart('#').Trim());
                continue;
            }
            ImGui.Bullet();
            ImGui.TextWrapped(line.TrimStart('-', '*', ' '));
        }
    }

    static void DrawActions(Phase phase)
    {
        switch (phase)
        {
            case Phase.Available when !CanWriteInstallDir():
                if (Row(0, 2, "Open releases page")) OpenUrl(ReleasesUrl);
                if (Row(1, 2, "Not now")) DismissForSession();
                break;

            case Phase.Available:
                if (Row(0, 3, "Install"))
                {
                    _cancel = new CancellationTokenSource();
                    Task.Run(() => UpdateAsync(_cancel.Token));
                }
                if (Row(1, 3, "Not now")) DismissForSession();
                if (Row(2, 3, $"Skip {_latestTag}"))
                {
                    ConfigManager.View.SetString(SkipTagKey, _latestTag);
                    ConfigManager.SaveView(PanelManager.Panels);
                    Log($"{_latestTag} skipped, it will not be offered again");
                    DismissForSession();
                }
                break;

            case Phase.Downloading:
            {
                long total = Interlocked.Read(ref _total);
                long done = Interlocked.Read(ref _downloaded);
                ImGui.ProgressBar(total > 0 ? done / (float)total : 0f, new Vector2(-1, 0),
                    total > 0
                        ? $"{done / 1048576f:0.0} / {total / 1048576f:0.0} MB"
                        : $"{done / 1048576f:0.0} MB");
                ImGui.Spacing();
                if (Row(0, 1, "Cancel")) _cancel?.Cancel();
                break;
            }

            case Phase.Applying:
                ImGui.ProgressBar(1f, new Vector2(-1, 0), "restarting");
                ImGui.Spacing();
                ImGui.BeginDisabled();
                Row(0, 1, "The game will reopen in a moment");
                ImGui.EndDisabled();
                break;

            case Phase.Failed:
                if (Row(0, 3, "Retry"))
                {
                    _cancel = new CancellationTokenSource();
                    Task.Run(() => UpdateAsync(_cancel.Token));
                }
                if (Row(1, 3, "Releases page")) OpenUrl(ReleasesUrl);
                if (Row(2, 3, "Close"))
                {
                    _phase = Phase.Available;
                    DismissForSession();
                }
                break;
        }
        
    }

    static bool Row(int index, int count, string label)
    {
        float spacing = ImGui.GetStyle().ItemSpacing.X;
        float width = (ImGui.GetContentRegionAvail().X - spacing * (count - 1)) / count;
        if (index > 0) ImGui.SameLine();
        return ImGui.Button(label, new Vector2(width, 0));
    }

    static void DismissForSession()
    {
        _dismissed = true;
        ClosePopup();
    }

    static void ClosePopup()
    {
        _popupOpen = false;
    }

    static HttpClient NewClient(TimeSpan timeout)
    {
        var http = new HttpClient { Timeout = timeout };
        http.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
        return http;
    }

    static string AssetSuffix()
    {
        string arch = RuntimeInformation.ProcessArchitecture == Architecture.Arm64 ? "arm64" : "x64";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return $"windows-{arch}.zip";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return $"macos-{arch}.zip";
        return $"linux-{arch}.tar.gz";
    }

    static void Extract(string archive, string dir)
    {
        if (archive.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
        {
            ZipFile.ExtractToDirectory(archive, dir, true);
            return;
        }

        using var fs = File.OpenRead(archive);
        using var gz = new GZipStream(fs, CompressionMode.Decompress);
        TarFile.ExtractToDirectory(gz, dir, true);
    }

    static void CopyTree(string source, string target)
    {
        foreach (string dir in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
            Directory.CreateDirectory(Path.Combine(target, Path.GetRelativePath(source, dir)));

        foreach (string file in Directory.GetFiles(source, "*", SearchOption.AllDirectories))
        {
            string relative = Path.GetRelativePath(source, file);
            if (IsUserFile(relative)) continue;
            CopyWithRetry(file, Path.Combine(target, relative));
        }
    }

    static bool IsUserFile(string relative)
    {
        string name = Path.GetFileName(relative);
        if (name.Equals("settings.json", StringComparison.OrdinalIgnoreCase)) return true;
        if (name.Equals("interface.ini", StringComparison.OrdinalIgnoreCase)) return true;
        if (name.EndsWith(".sav", StringComparison.OrdinalIgnoreCase)) return true;

        string root = relative.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)[0];
        return root.Equals("disc", StringComparison.OrdinalIgnoreCase)
            || root.Equals("mods", StringComparison.OrdinalIgnoreCase);
    }

    static void CopyWithRetry(string source, string target)
    {
        for (int attempt = 0; ; attempt++)
        {
            try
            {
                File.Copy(source, target, true);
                MakeExecutable(target);
                return;
            }
            catch (IOException) when (attempt < 40)
            {
                Thread.Sleep(250);
            }
            catch (IOException)
            {
                MoveAside(target);
                File.Copy(source, target, true);
                MakeExecutable(target);
                return;
            }
        }
    }

    const string StaleSuffix = ".pending-delete";

    static void MoveAside(string target)
    {
        for (int n = 0; ; n++)
        {
            string aside = $"{target}{StaleSuffix}{(n == 0 ? "" : n.ToString())}";
            if (File.Exists(aside)) continue;
            File.Move(target, aside);
            return;
        }
    }

    static void CleanStaleFiles()
    {
        try
        {
            foreach (string f in Directory.GetFiles(InstallDir, "*" + StaleSuffix + "*", SearchOption.AllDirectories))
                try { File.Delete(f); } catch { }
        }
        catch
        {
        }
    }

    static void WaitForExit(int pid)
    {
        try
        {
            using var process = Process.GetProcessById(pid);
            process.WaitForExit(30000);
        }
        catch
        {
        }

        Thread.Sleep(500);
    }

    static void MakeExecutable(string path)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
        try
        {
            File.SetUnixFileMode(path, File.GetUnixFileMode(path) |
                UnixFileMode.UserExecute | UnixFileMode.GroupExecute | UnixFileMode.OtherExecute);
        }
        catch
        {
        }
    }

    static bool CanWriteInstallDir()
    {
        try
        {
            string probe = Path.Combine(InstallDir, ".update-probe");
            File.WriteAllText(probe, "");
            File.Delete(probe);
            return true;
        }
        catch
        {
            return false;
        }
    }

    static void OpenUrl(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        catch
        {
        }
    }

    static void TryDelete(string dir)
    {
        try
        {
            if (Directory.Exists(dir)) Directory.Delete(dir, true);
        }
        catch
        {
        }
    }
}

public sealed class UpdatePanel : IFloatingPanel
{
    public string Name => "Update";

    public bool IsOpen
    {
        get => AutoUpdater.ShouldShowUi;
        set { if (!value) AutoUpdater.CloseUi(); }
    }

    public void Draw() => AutoUpdater.Draw();
}
