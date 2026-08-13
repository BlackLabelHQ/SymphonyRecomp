using System.Numerics;
using ImGuiNET;
using RecompOne.Runtime.Host.Window;

namespace Recompiled;

public sealed class AboutPopup : Popup
{
    public const string Project = "SymphonyRecomp";

    static readonly string[] Credits =
    [
        "Flaffy",
        "DerpPrincess",
        "Wowjinxy",
        "Mottzilla",
        "Eldri7ch",
    ];

    protected override string TitleKey => "about.title";
    protected override Vector2 Size => new(420f, 0f);

    protected override void DrawContent()
    {
        string version = $"(version: {AutoUpdater.CurrentTag ?? "dev"})";
        float width = ImGui.CalcTextSize(Project).X + ImGui.GetStyle().ItemSpacing.X + ImGui.CalcTextSize(version).X;
        Center(width);
        ImGui.TextUnformatted(Project);
        ImGui.SameLine();
        ImGui.TextDisabled(version);

        ImGui.Spacing();
        ImGui.Separator();
        ImGui.Spacing();

        ImGui.TextWrapped(Localization.T("about.disclaimer"));
        ImGui.Spacing();
        ImGui.TextWrapped(Localization.T("about.recomp"));
        ImGui.Spacing();
        ImGui.TextWrapped(Localization.T("about.decomp_server"));
        ImGui.Spacing();
        ImGui.TextUnformatted(Localization.T("about.made_by"));
        ImGui.Spacing();
        ImGui.TextWrapped(string.Join(", ", Credits));
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
        PopupManager.Register(new AboutPopup());

        MenuRegistry.Menu("menu.help", MenuRegistry.OrderHelp)
            .Popup<AboutPopup>("menu.help.about");
    }
}
