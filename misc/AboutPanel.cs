using System.Numerics;
using ImGuiNET;
using RecompOne.Runtime.Host;
using RecompOne.Runtime.Host.Window;

namespace Recompiled;

public sealed class AboutPanel : IFloatingPanel
{
    public const string Title = "SymphonyRecomp";

    static readonly string[] Credits =
    [
        "Flaffy",
        "DerpPrincess",
        "Wojinxy",
        "Mottzila",
        "Eldri7ch",
    ];

    public string Name => "About";
    public bool IsOpen { get; set; }

    public void Draw()
    {
        ImGui.SetNextWindowSize(new Vector2(340 * HostWindow.DpiScale, 0), ImGuiCond.Always);
        ImGui.SetNextWindowPos(ImGui.GetMainViewport().GetCenter(), ImGuiCond.Always, new Vector2(0.5f, 0.5f));

        bool open = IsOpen;
        if (!ImGui.Begin(Name, ref open, ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoMove))
        {
            IsOpen = open;
            ImGui.End();
            return;
        }

        string version = $"(version: {AutoUpdater.CurrentTag ?? "dev"})";
        float titleWidth = ImGui.CalcTextSize(Title).X + ImGui.GetStyle().ItemSpacing.X + ImGui.CalcTextSize(version).X;
        Center(titleWidth);
        ImGui.TextUnformatted(Title);
        ImGui.SameLine();
        ImGui.TextDisabled(version);

        ImGui.Spacing();
        ImGui.Separator();
        ImGui.Spacing();

        ImGui.TextWrapped("SymphonyRecomp is not affiliated with konami or sony, this is a fan project made from fans for the fans ,<3");
        ImGui.Spacing();
        ImGui.TextUnformatted("made by:");
        ImGui.Spacing();
        ImGui.TextWrapped(string.Join(", ", Credits));

        IsOpen = open;
        ImGui.End();
    }

    static void Center(float width)
    {
        float off = (ImGui.GetContentRegionAvail().X - width) * 0.5f;
        if (off > 0) ImGui.SetCursorPosX(ImGui.GetCursorPosX() + off);
    }
}

public static class HelpMenu
{
    public static void Register()
    {
        PanelManager.Register(new AboutPanel());
        MenuRegistry.Register("Help", DrawItems, null, 600);
    }

    static void DrawItems()
    {
        if (ImGui.MenuItem("About"))
            if (PanelManager.Get<AboutPanel>() is { } about) about.IsOpen = true;
    }
}
