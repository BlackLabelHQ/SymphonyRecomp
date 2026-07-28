using ImGuiNET;
using RecompOne.Runtime.Host.Window;

namespace Recompiled;

public static class QualityOfLifeMenu
{
    public static void Register()
    {
        PanelManager.Register(new QualityOfLifePanel());
        MenuRegistry.Register("Quality Of Life", DrawItems, "Misc");
    }

    static void DrawItems()
    {
        Toggle<QualityOfLifePanel>("Quality Of Life");
    }

    static void Toggle<T>(string label) where T : class, IPanel
    {
        var panel = PanelManager.Get<T>();
        if (panel == null) return;
        if (ImGui.MenuItem(label, null, panel.IsOpen))
            panel.IsOpen = !panel.IsOpen;
    }
}
