using System.Numerics;
using System.Text;
using ImGuiNET;
using RecompOne.Runtime.Cdrom;
using RecompOne.Runtime.Host;
using RecompOne.Runtime.Host.Window;
using RecompOne.Runtime.Memory;

namespace Recompiled;

public static class DiscCheck
{
    const string UsExecutable = "SLUS_000.67";

    static readonly (string File, string Message)[] KnownRegions =
    [
        ("SLPM_860.23", "You provided a Japanese copy, a US one is needed."),
        ("SLES_005.24", "You provided a European copy, a US one is needed."),
    ];

    const uint PresetAddr = 0x801A78E5;
    const int PresetMaxLength = 100; //abritary, not sure the correct size

    //is this right?
    public static readonly HashSet<string> SupportedPresets = new(StringComparer.OrdinalIgnoreCase)
    {
        "adventure", "agonize-twtw", "any-percent", "bounty-hunter", "brawler", "casual",
        "crash-course", "empty-hand", "expedition", "grand-tour", "guarded-og", "hitman",
        "lookingglass", "lycanthrope", "magic-mirror", "mobility", "nimble", "nimble-lite",
        "og", "rat-race", "recycler", "safe", "safe-stwo", "scavenger", "sequence-breaker",
        "sight-seer", "target-confirmed", "third-castle", "vanilla", "warlock", "recycler"
    };

    static string _preset = "";
    static bool _showPresetModal;

    public static string Preset => _preset;

    public static void Register()
    {
        RecompOne.Runtime.Runtime.DiscValidator = Validate;
        PanelManager.Register(new PresetWarningPanel());
    }

    static string? Validate(string path)
    {
        CueFs? fs = null;
        try
        {
            fs = CueFs.Open(path);
        }
        catch
        {
            fs?.Dispose();
            return "This file could not be read as a disc image.";
        }

        try
        {
            if (fs.FindFile(UsExecutable) != null) return null;

            foreach (var (file, message) in KnownRegions)
                if (fs.FindFile(file) != null) return message;

            return "This is not a Symphony of the Night disc.";
        }
        finally
        {
            fs.Dispose();
        }
    }


    public static string ReadCString(IMemory m, uint addr, int max)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < max; i++)
        {
            byte b = m.ReadU8(addr + (uint)i);
            if (b == 0) break;
            if (b < 0x20 || b > 0x7E) return "";
            sb.Append((char)b);
        }
        return sb.ToString();
    }

    public static string NormalizePreset(string raw) =>raw.Trim().Replace(' ', '-').ToLowerInvariant();//every use - as space? if not need to change this

    public static string CheckPreset(IMemory m) //also update title to be SymphonyRecomp - Version - Playing randomizer <preset> if preset is not none
    {
        //tfk do i do
        return _preset;
    }

    public static bool IsPresetSupported(string preset) => preset.Length == 0 || SupportedPresets.Contains(preset);

    public static void ShowUncompatiblePresetModal() => _showPresetModal = true;

    sealed class PresetWarningPanel : IFloatingPanel
    {
        public string Name => "Randomizer";
        public bool IsOpen { get => _showPresetModal; set { if (!value) _showPresetModal = false; } }

        public void Draw()
        {
            ImGui.SetNextWindowSize(new Vector2(420 * HostWindow.DpiScale, 0), ImGuiCond.Always);
            ImGui.SetNextWindowPos(ImGui.GetMainViewport().GetCenter(), ImGuiCond.Appearing, new Vector2(0.5f, 0.5f));

            bool open = true;
            if (!ImGui.Begin(Name, ref open,
                    ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse))
            {
                ImGui.End();
                if (!open) _showPresetModal = false;
                return;
            }

            ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1f, 0.85f, 0.3f, 1f));
            ImGui.TextWrapped("You're using an incompatible randomizer preset, it may not work as intended!");
            ImGui.PopStyleColor();

            if (_preset.Length > 0)
            {
                ImGui.Spacing();
                ImGui.TextDisabled($"preset: {_preset}");
            }

            ImGui.Spacing();
            ImGui.Separator();
            ImGui.Spacing();

            if (ImGui.Button("Continue anyway", new Vector2(-1, 0))) _showPresetModal = false;

            ImGui.End();
            if (!open) _showPresetModal = false;
        }
    }
}
