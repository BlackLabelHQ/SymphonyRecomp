using RecompOne.Runtime.Context;
using RecompOne.Runtime.Events;
using RecompOne.Runtime.Memory;

namespace Recompiled;

public static partial class WidescreenPatch
{
    const uint GameStepAddr = 0x80073060;
    const uint StageIdAddr = 0x800974A0;
    const uint GameOverPrimAddr = 0x8013640C;

    const uint GameOverDone = 11;
    const uint GameOverDoneAlt = 111;

    public static void PostHandleGameOver(CpuContext c, IMemory m)
    {
        if (OriginalAspect) return;
        uint step = m.ReadU32(GameStepAddr);
        if (step is GameOverDone or GameOverDoneAlt)
        {
            SuspendZoom(m);
            return;
        }
        if (step < 2 || step > 8) return;
        if (m.ReadU32(StageIdAddr) == 0x1F) return;

        uint red = PrimBufAddr + m.ReadU32(GameOverPrimAddr) * PrimStride;
        if (m.ReadU16(red + 0x1A) != 8) return;
        uint left = m.ReadU32(red);
        if (left == 0) return;
        uint right = m.ReadU32(left);
        if (right == 0) return;

        int margin = StageMargin();
        int wide = 0x100 + margin * 2;
        bool zoomed = Zoomed;
        uint prim = m.ReadU32(right);
        for (int i = 0; i < 0x100 && prim != 0; i++, prim = m.ReadU32(prim))
        {
            if (margin != 0) SetPrimX(m, prim, -margin + i * wide / 0x100, -margin + (i + 1) * wide / 0x100);
            if (zoomed) FitMeltStrip(m, prim);
        }
    }

    static void FitMeltStrip(IMemory m, uint prim)
    {
        if (m.ReadU16(prim + 0x0A) != 0 || m.ReadU16(prim + 0x22) != 0xF0 || m.ReadU8(prim + 0x25) != 0xF0) return;
        m.WriteU16(prim + 0x0A, StageViewTop);
        m.WriteU16(prim + 0x16, StageViewTop);
        m.WriteU16(prim + 0x22, StageViewTop + StageViewHeight);
        m.WriteU16(prim + 0x2E, StageViewTop + StageViewHeight);
        m.WriteU8(prim + 0x25, StageViewHeight);
        m.WriteU8(prim + 0x31, StageViewHeight);
    }

    static void SetPrimX(IMemory m, uint prim, int x0, int x1)
    {
        m.WriteU16(prim + 0x08, (ushort)(short)x0);
        m.WriteU16(prim + 0x20, (ushort)(short)x0);
        m.WriteU16(prim + 0x14, (ushort)(short)x1);
        m.WriteU16(prim + 0x2C, (ushort)(short)x1);
    }

    const uint PlayerHudAddr = 0x8013796C;
    const uint BossBarStateAddr = 0x8003C744;
    const uint PlayableCharAddr = 0x8003C9A0;

    static bool RichterHudActive(IMemory m) =>
        m.ReadU32(StageIdAddr) == 0x1F || m.ReadU32(PlayableCharAddr) != 0;

    static uint HudPrim(IMemory m, uint indexAddr) =>
        PrimBufAddr + m.ReadU32(indexAddr) * PrimStride;

    static void ShiftPrimX(IMemory m, uint prim, int dx)
    {
        m.WriteU16(prim + 0x08, (ushort)((short)m.ReadU16(prim + 0x08) + dx));
        m.WriteU16(prim + 0x14, (ushort)((short)m.ReadU16(prim + 0x14) + dx));
        m.WriteU16(prim + 0x20, (ushort)((short)m.ReadU16(prim + 0x20) + dx));
        m.WriteU16(prim + 0x2C, (ushort)((short)m.ReadU16(prim + 0x2C) + dx));
    }
    static int _hudMargin;
    static int _richterHudMargin;

    static int HudMargin() => OriginalAspect ? 0 : StageMargin();

    static void ShiftPrims(IMemory m, uint prim, int count, int dx)
    {
        for (int i = 0; i < count && prim != 0; i++, prim = m.ReadU32(prim))
            ShiftPrimX(m, prim, dx);
    }

    public static void PostDrawHud(CpuContext c, IMemory m)
    {
        _hudMargin = RichterHudActive(m) ? 0 : HudMargin();
        if (_hudMargin == 0) return;
        for (uint prim = HudPrim(m, PlayerHudAddr + 4); prim != 0; prim = m.ReadU32(prim))
            ShiftPrimX(m, prim, -_hudMargin);
    }

    static void FixHudFrame(IMemory m)
    {
        if (RichterHudActive(m)) return;
        int delta = HudMargin() - _hudMargin;
        if (delta == 0) return;
        ShiftPrims(m, HudPrim(m, PlayerHudAddr + 4), 6, -delta);
        _hudMargin += delta;
    }

    public static void PostDrawHudSubweapon(CpuContext c, IMemory m)
    {
        FixHudFrame(m);
        int margin = HudMargin();
        if (margin == 0 || RichterHudActive(m)) return;
        uint prim = HudPrim(m, PlayerHudAddr + 4);
        for (int i = 0; i < 6 && prim != 0; i++)
            prim = m.ReadU32(prim);
        ShiftPrims(m, prim, 8, -margin);
    }

    static int RichterHudShift(int index) => index is 1 or 3 or 4 ? 1 : -1;

    public static void PostDrawRichterHud(CpuContext c, IMemory m)
    {
        _richterHudMargin = HudMargin();
        if (_richterHudMargin == 0) return;
        uint prim = HudPrim(m, PlayerHudAddr + 4);
        for (int i = 0; prim != 0; i++, prim = m.ReadU32(prim))
            ShiftPrimX(m, prim, RichterHudShift(i) * _richterHudMargin);
        for (uint col = HudPrim(m, PlayerHudAddr + 8); col != 0; col = m.ReadU32(col))
            ShiftPrimX(m, col, _richterHudMargin);
    }

    static bool RichterCrashRedrawn(IMemory m)
    {
        uint crash = m.ReadU32(PlayerHudAddr + 0x24);
        return crash - 10 <= 3 || crash - 60 <= 3;
    }

    static void FixRichterHud(IMemory m)
    {
        int delta = HudMargin() - _richterHudMargin;
        if (delta == 0) return;
        bool crashRedrawn = RichterCrashRedrawn(m);
        uint prim = HudPrim(m, PlayerHudAddr + 4);
        for (int i = 0; prim != 0; i++, prim = m.ReadU32(prim))
            if (i is 0 or 2 or 5 or 6 || (i == 8 && !crashRedrawn))
                ShiftPrimX(m, prim, RichterHudShift(i) * delta);
        for (uint col = HudPrim(m, PlayerHudAddr + 8); col != 0; col = m.ReadU32(col))
            ShiftPrimX(m, col, delta);
        _richterHudMargin += delta;
    }

    public static void PostDrawRichterHudSubweapon(CpuContext c, IMemory m)
    {
        FixRichterHud(m);
        int margin = HudMargin();
        if (margin == 0) return;
        if (m.ReadU32(BossBarStateAddr) == 5) return;
        uint prim = HudPrim(m, PlayerHudAddr + 4);
        for (int i = 0; i < 9 && prim != 0; i++, prim = m.ReadU32(prim))
        {
            bool redrawn = i switch
            {
                1 or 3 or 4 => true,
                7 => m.ReadU16(prim + 0x32) != 8,
                8 => RichterCrashRedrawn(m),
                _ => false,
            };
            if (redrawn) ShiftPrimX(m, prim, RichterHudShift(i) * margin);
        }
    }

    const uint CornerTextTimerAddr = 0x80097410;
    const uint CornerTextPrimsAddr = 0x80097414;
    const byte CornerTextLeftBlue = 0xAF;
    const int CornerTextLeftX = 7;
    const int CornerTextRightX = 0xF4;

    static void FixCornerText(TilemapRenderedEvent e)
    {
        var m = e.Memory;
        if (m.ReadU32(CornerTextTimerAddr) == 0) return;

        uint box = PrimBufAddr + m.ReadU32(CornerTextPrimsAddr) * PrimStride;
        int margin = HudMargin();
        int delta = m.ReadU8(box + 0x06) == CornerTextLeftBlue
            ? CornerTextLeftX - margin - (short)m.ReadU16(box + 0x08)
            : CornerTextRightX + margin - (short)m.ReadU16(box + 0x14);
        if (delta == 0) return;

        for (uint prim = box; prim != 0; prim = m.ReadU32(prim))
            ShiftPrimX(m, prim, delta);
    }
}
