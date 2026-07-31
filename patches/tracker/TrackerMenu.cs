using ImGuiNET;
using RecompOne.Runtime.Host.Window;

namespace Recompiled;

public static class TrackerMenu
{
    public static void Register()
    {
        PanelManager.Register(new TrackerOverlayPanel());
        PanelManager.Register(new MapOverlayPanel());
        MenuRegistry.Register("Overlays", DrawItems, "Misc");
    }

    static void DrawItems()
    {
        Toggle<TrackerOverlayPanel>("Tracker Overlay");
        Toggle<MapOverlayPanel>("Map Overlay");
    }

    static void Toggle<T>(string label) where T : class, IPanel
    {
        var panel = PanelManager.Get<T>();
        if (panel == null) return;
        if (ImGui.MenuItem(label, null, panel.IsOpen))
            panel.IsOpen = !panel.IsOpen;
    }
}
