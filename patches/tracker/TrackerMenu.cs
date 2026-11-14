using RecompOne.UI;

namespace Recompiled;

public static class TrackerMenu
{
    public static void Register()
    {
        PanelManager.Register(new TrackerOverlayPanel());
        PanelManager.Register(new MapOverlayPanel());

        MenuRegistry.Menu("menu.misc")
            .Submenu("menu.misc.overlays").After("menu.misc.cheats")
                .Panel<TrackerOverlayPanel>("panel.tracker")
                .Panel<MapOverlayPanel>("panel.map")
                .End();
    }
}
