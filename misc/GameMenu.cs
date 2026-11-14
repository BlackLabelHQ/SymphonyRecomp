using RecompOne.Runtime.Events;
using RecompOne.UI;
using Sotn;

namespace Recompiled;

public static class GameMenu
{
    const uint ClearBackbufferAddr = 0x800E44EC;
    const uint Func801073C0Addr = 0x801073C0;
    const uint Func801361F8Addr = 0x801361F8;
    const uint MuteSoundAddr = 0x80132760;
    const uint SetGameStateAddr = 0x800E4124;

    const uint GameStateAddr = 0x8003C734;
    const uint GamePlay = 2;
    const uint GameTitle = 1;

    const int SfxSoftResetA = 0x12;
    const int SfxSoftResetB = 0x0B;

    static bool _softResetPending;

    public static void Register()
    {
        MenuRegistry.Menu("menu.system").Item("menu.system.soft_reset", SoftReset).Before("menu.system.hard_reset").Enabled(Cheats.InPlay);

        Event.AddListener<VSyncEvent>(OnVSync);
    }

    static void SoftReset() => _softResetPending = true;

    static void OnVSync(VSyncEvent e)
    {
        if (!_softResetPending) return;

        var m = e.Memory;
        var c = e.Context;
        if (m == null || c == null) return;
        if (m.ReadU32(GameStateAddr) != GamePlay) return;

        _softResetPending = false;

        GameApi.Call(c, m, ClearBackbufferAddr);
        GameApi.Call(c, m, Func801073C0Addr);
        GameApi.PlaySfx(SfxSoftResetA);
        GameApi.PlaySfx(SfxSoftResetB);
        GameApi.Call(c, m, Func801361F8Addr);
        GameApi.Call(c, m, MuteSoundAddr);
        GameApi.Call(c, m, SetGameStateAddr, GameTitle);
    }
}
