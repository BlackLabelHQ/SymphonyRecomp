using ImGuiNET;
using RecompOne.Runtime.Host.Window;

namespace Recompiled;

public static class RandoMenu
{
    public static void Register()
    {
        PanelManager.Register(new RandoPanel());
        MenuRegistry.Register("Randomizer", DrawItems);
    }

    static void DrawItems()
    {
        Toggle<RandoPanel>("Settings");
    }

    static void Toggle<T>(string label) where T : class, IPanel
    {
        var panel = PanelManager.Get<T>();
        if (panel == null) return;
        if (ImGui.MenuItem(label, null, panel.IsOpen))
            panel.IsOpen = !panel.IsOpen;
    }
}
