using RecompOne.Runtime.Host.Window;

namespace Recompiled;

public static class GameMenu
{
    const uint SoftResetTimer = 0x80136408;
    const uint SoftResetTrigger = 0x80;

    public static void Register()
    {
        MenuRegistry.Menu("menu.system", MenuRegistry.OrderSystem)
            .Item("menu.system.soft_reset", SoftReset).Order(MainMenuBar.SystemSoftReset).Enabled(Cheats.InPlay);
    }

    static void SoftReset() => RecompOne.Runtime.Runtime.Mem?.WriteU32(SoftResetTimer, SoftResetTrigger);
}
