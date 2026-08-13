using RecompOne.Runtime.Host.Window;

namespace Recompiled;

public static class CheatMenu
{
    public static void Register()
    {
        PanelManager.Register(new MovementCheatPanel());
        PanelManager.Register(new StatsCheatPanel());
        PanelManager.Register(new InventoryCheatPanel());

        MenuRegistry.Menu("menu.misc", MenuRegistry.OrderGame)
            .Submenu("menu.misc.cheats").Order(20)
                .Panel<MovementCheatPanel>("panel.cheats.movement")
                .Panel<StatsCheatPanel>("panel.cheats.stats")
                .Panel<InventoryCheatPanel>("panel.cheats.inventory")
                .End();
    }
}
