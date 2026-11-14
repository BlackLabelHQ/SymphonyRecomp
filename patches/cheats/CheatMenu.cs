using RecompOne.UI;

namespace Recompiled;

public static class CheatMenu
{
    public static void Register()
    {
        PanelManager.Register(new MovementCheatPanel());
        PanelManager.Register(new StatsCheatPanel());
        PanelManager.Register(new InventoryCheatPanel());

        MenuRegistry.Menu("menu.misc")
            .Submenu("menu.misc.cheats").After("panel.qol")
                .Panel<MovementCheatPanel>("panel.cheats.movement")
                .Panel<StatsCheatPanel>("panel.cheats.stats")
                .Panel<InventoryCheatPanel>("panel.cheats.inventory")
                .End();
    }
}
