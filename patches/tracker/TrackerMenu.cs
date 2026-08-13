using RecompOne.Runtime.Host.Window;

namespace Recompiled;

public static class TrackerMenu
{
    public static void Register()
    {
        PanelManager.Register(new TrackerOverlayPanel());
        PanelManager.Register(new MapOverlayPanel());

        MenuRegistry.Menu("menu.misc", MenuRegistry.OrderGame)
            .Submenu("menu.misc.overlays").Order(30)
                .Panel<TrackerOverlayPanel>("panel.tracker")
                .Panel<MapOverlayPanel>("panel.map")
                .End();
    }
}
