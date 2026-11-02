using RecompOne.Runtime.Context;
using RecompOne.Runtime.Dispatch;
using RecompOne.Runtime.Events;
using RecompOne.Runtime.Hle;
using RecompOne.Runtime.Memory;

namespace Recompiled;

public static partial class WidescreenPatch
{
    const int FlashW = 256;
    const int FlashH = 240;
    const int TeleportW = 0xC0;
    const int SpyglassFirstStep = 3;
    const int SpyglassLastStep = 6;

    static bool _spyglass;

    public static void RegisterEffectPrims()
    {
        Event.AddListener<RenderPrimEvent>(WidenScreenFlash);
        Event.AddListener<RenderPrimEvent>(WidenSpyglass);
        Event.AddListener<TilemapRenderedEvent>(TrackSpyglass);
        Event.AddListener<TilemapRenderedEvent>(ExtendStageBackgrounds);
        Event.AddListener<TilemapRenderedEvent>(ExtendStageDestruction);
        Event.AddListener<TilemapRenderedEvent>(FixCornerText);
    }

    static void WidenScreenFlash(RenderPrimEvent e)
    {
        if (OriginalAspect || !DisplayModeHooks.IsStage) return;
        if (e.Textured || e.Gouraud || e.Count != 2) return;
        if (e.Y[1] - e.Y[0] != FlashH) return;

        int margin = StageMargin();
        if (margin <= 0) return;

        int width = e.X[1] - e.X[0];
        int left = e.X[0] - e.DrawLeft;
        if (width == FlashW)
        {
            e.X[0] -= margin;
            e.X[1] += margin;
        }
        else if (width == TeleportW && left == 0)
            e.X[0] -= margin;
        else if (width == TeleportW && left == TeleportW)
            e.X[1] = Math.Max(e.X[1], e.DrawLeft + FlashW + margin);
    }

    static void TrackSpyglass(TilemapRenderedEvent e)
    {
        _spyglass = false;
        if (OriginalAspect || !DisplayModeHooks.IsStage) return;
        var m = e.Memory;
        if (!string.Equals(StageOverlay(m)?.Name, "no1", StringComparison.OrdinalIgnoreCase)) return;
        uint fn = StageSym(m, "func_us_801BA290").Addr;
        if (fn == 0) return;
        for (int i = StageEntityStart; i < TotalEntityCount; i++)
        {
            uint entity = PedAt(i);
            if (m.ReadU32(entity + EntPfnUpdate) != fn) continue;
            _spyglass = m.ReadU16(entity + EntStep) is >= SpyglassFirstStep and <= SpyglassLastStep;
            return;
        }
    }

    static void WidenSpyglass(RenderPrimEvent e)
    {
        if (!_spyglass || e.Textured || OriginalAspect || !DisplayModeHooks.IsStage) return;
        int margin = StageMargin();
        if (margin <= 0) return;

        int min = int.MaxValue, max = int.MinValue;
        for (int i = 0; i < e.Count; i++)
        {
            min = Math.Min(min, e.X[i]);
            max = Math.Max(max, e.X[i]);
        }

        int left = min - e.DrawLeft, right = max - e.DrawLeft;
        for (int i = 0; i < e.Count; i++)
        {
            if (left <= 0 && e.X[i] == min) e.X[i] -= margin;
            if (right >= FlashW - 1 && e.X[i] == max) e.X[i] += margin;
        }
    }

//todo: this can probably be shortned
    public static void CloudsWide(CpuContext c, IMemory m)
    {
        int xMin = -StageMargin();
        int xMax = 257 + StageMargin();
        c.SP = c.SP - 0x40u;
        m.WriteU32((c.SP + 0x20u), c.S2);
        c.S2 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x3Cu), c.RA);
        m.WriteU32((c.SP + 0x38u), c.FP);
        m.WriteU32((c.SP + 0x34u), c.S7);
        m.WriteU32((c.SP + 0x30u), c.S6);
        m.WriteU32((c.SP + 0x2Cu), c.S5);
        m.WriteU32((c.SP + 0x28u), c.S4);
        m.WriteU32((c.SP + 0x24u), c.S3);
        m.WriteU32((c.SP + 0x1Cu), c.S1);
        m.WriteU32((c.SP + 0x18u), c.S0);
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        if (c.V0 != 0u) {
            c.S3 = 0x1F800000u;
            goto L801BAC6C;
        }
        c.S3 = 0x1F800000u;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x5D4u;
        c.RA = 0x801BAB58u;
        SoTN_st0.InitializeEntity_st0(c, m);
        c.A0 = 0u | 0x0004u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3820u));
        c.A1 = 0u | 0x0070u;
        c.RA = 0x801BAB70u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.A0 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.A0 == c.V0) {
            c.V0 = c.A0 << 1;
            goto L801BB270;
        }
        c.V0 = c.A0 << 1;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.T1 = c.V0 + c.V1;
        c.V0 = m.ReadU32((c.S2 + 0x34u));
        c.V1 = 0x00800000u;
        m.WriteU32((c.S2 + 0x64u), c.A0);
        m.WriteU32((c.S2 + 0x7Cu), c.T1);
        c.V0 = c.V0 | c.V1;
        m.WriteU32((c.S2 + 0x34u), c.V0);
        c.V0 = 0u | 0x000Fu;
        m.WriteU16((c.T1 + 0x1Au), (ushort)c.V0);
        c.V0 = 0u | 0x0046u;
        m.WriteU16((c.T1 + 0xEu), (ushort)c.V0);
        c.V0 = 0u | 0x0065u;
        m.WriteU8((c.T1 + 0x30u), (byte)c.V0);
        m.WriteU8((c.T1 + 0x18u), (byte)c.V0);
        c.V0 = 0u | 0x0080u;
        m.WriteU8((c.T1 + 0x19u), (byte)c.V0);
        m.WriteU8((c.T1 + 0xDu), (byte)c.V0);
        c.V0 = 0u | 0x00FBu;
        c.V1 = 0u | 0x004Bu;
        m.WriteU8((c.T1 + 0x31u), (byte)c.V0);
        m.WriteU8((c.T1 + 0x25u), (byte)c.V0);
        c.V0 = 0u | 0x00B0u;
        m.WriteU16((c.T1 + 0x2Cu), (ushort)c.V0);
        m.WriteU16((c.T1 + 0x14u), (ushort)c.V0);
        c.V0 = 0u | 0x007Bu;
        m.WriteU16((c.T1 + 0x2Eu), (ushort)c.V0);
        m.WriteU16((c.T1 + 0x22u), (ushort)c.V0);
        c.V0 = 0u | 0x0004u;
        m.WriteU8((c.T1 + 0x24u), (byte)0u);
        m.WriteU8((c.T1 + 0xCu), (byte)0u);
        m.WriteU16((c.T1 + 0x20u), (ushort)c.V1);
        m.WriteU16((c.T1 + 0x8u), (ushort)c.V1);
        m.WriteU16((c.T1 + 0x16u), (ushort)0u);
        m.WriteU16((c.T1 + 0xAu), (ushort)0u);
        m.WriteU16((c.S2 + 0x9Eu), (ushort)c.V1);
        m.WriteU16((c.S2 + 0xA2u), (ushort)0u);
        m.WriteU16((c.T1 + 0x26u), (ushort)c.V0);
        m.WriteU16((c.T1 + 0x32u), (ushort)0u);
        c.T1 = m.ReadU32(c.T1);
        if (c.T1 == 0u) {
            c.A0 = 0u | 0x000Fu;
            goto L801BAC68;
        }
        c.A0 = 0u | 0x000Fu;
        c.V1 = 0u | 0x0226u;
        c.V0 = 0u | 0x0008u;
        L801BAC4C: ;
        m.WriteU16((c.T1 + 0x1Au), (ushort)c.A0);
        m.WriteU16((c.T1 + 0xEu), (ushort)c.V1);
        m.WriteU16((c.T1 + 0x32u), (ushort)c.V0);
        c.T1 = m.ReadU32(c.T1);
        if (c.T1 != 0u) {
            goto L801BAC4C;
        }
        L801BAC68: ;
        c.S3 = 0x1F800000u;
        L801BAC6C: ;
        c.S0 = 0x1F800000u;
        c.S0 = c.S0 | 0x0020u;
        c.S5 = 0x1F800000u;
        c.S5 = c.S5 | 0x0060u;
        c.A2 = 0x1F800000u;
        c.A2 = c.A2 | 0x0080u;
        c.T8 = 0x80180000u;
        c.T8 = c.T8 + 0x2084u;
        c.T9 = 0u + 0u;
        c.V1 = 0x00030000u;
        c.V0 = m.ReadU32((c.S2 + 0x84u));
        c.A0 = 0x00080000u;
        c.V0 = c.V0 + c.V1;
        m.WriteU32((c.S2 + 0x84u), c.V0);
        c.V0 = m.ReadU32((c.S2 + 0x8Cu));
        c.V1 = m.ReadU32((c.S2 + 0x88u));
        c.V0 = c.V0 + c.A0;
        m.WriteU32((c.S2 + 0x8Cu), c.V0);
        c.V0 = m.ReadU32((c.S2 + 0x90u));
        c.V1 = c.V1 + c.A0;
        m.WriteU32((c.S2 + 0x88u), c.V1);
        c.V1 = 0x00040000u;
        c.V0 = c.V0 + c.V1;
        m.WriteU32((c.S2 + 0x90u), c.V0);
        c.A1 = 0x80180000u;
        c.A1 = c.A1 + 0x20CCu;
        c.V0 = m.ReadWordLeft(c.V0, (c.A1 + 0x3u));
        c.V0 = m.ReadWordRight(c.V0, c.A1);
        c.V1 = m.ReadWordLeft(c.V1, (c.A1 + 0x7u));
        c.V1 = m.ReadWordRight(c.V1, (c.A1 + 0x4u));
        m.WriteWordLeft((c.S5 + 0x3u), c.V0);
        m.WriteWordRight(c.S5, c.V0);
        m.WriteWordLeft((c.S5 + 0x7u), c.V1);
        m.WriteWordRight((c.S5 + 0x4u), c.V1);
        c.A1 = 0x80180000u;
        c.A1 = c.A1 + 0x20D4u;
        c.A0 = 0x1F800000u;
        c.A0 = c.A0 | 0x0068u;
        c.V0 = m.ReadWordLeft(c.V0, (c.A1 + 0x3u));
        c.V0 = m.ReadWordRight(c.V0, c.A1);
        c.V1 = m.ReadWordLeft(c.V1, (c.A1 + 0x7u));
        c.V1 = m.ReadWordRight(c.V1, (c.A1 + 0x4u));
        m.WriteWordLeft((c.A0 + 0x3u), c.V0);
        m.WriteWordRight(c.A0, c.V0);
        m.WriteWordLeft((c.A0 + 0x7u), c.V1);
        m.WriteWordRight((c.A0 + 0x4u), c.V1);
        c.A1 = 0x80180000u;
        c.A1 = c.A1 + 0x20DCu;
        c.A0 = 0x1F800000u;
        c.A0 = c.A0 | 0x0070u;
        c.V0 = m.ReadWordLeft(c.V0, (c.A1 + 0x3u));
        c.V0 = m.ReadWordRight(c.V0, c.A1);
        c.V1 = m.ReadWordLeft(c.V1, (c.A1 + 0x7u));
        c.V1 = m.ReadWordRight(c.V1, (c.A1 + 0x4u));
        m.WriteWordLeft((c.A0 + 0x3u), c.V0);
        m.WriteWordRight(c.A0, c.V0);
        m.WriteWordLeft((c.A0 + 0x7u), c.V1);
        m.WriteWordRight((c.A0 + 0x4u), c.V1);
        c.A1 = 0x80180000u;
        c.A1 = c.A1 + 0x20E4u;
        c.A0 = 0x1F800000u;
        c.A0 = c.A0 | 0x0078u;
        c.V0 = m.ReadWordLeft(c.V0, (c.A1 + 0x3u));
        c.V0 = m.ReadWordRight(c.V0, c.A1);
        c.V1 = m.ReadWordLeft(c.V1, (c.A1 + 0x7u));
        c.V1 = m.ReadWordRight(c.V1, (c.A1 + 0x4u));
        m.WriteWordLeft((c.A0 + 0x3u), c.V0);
        m.WriteWordRight(c.A0, c.V0);
        m.WriteWordLeft((c.A0 + 0x7u), c.V1);
        m.WriteWordRight((c.A0 + 0x4u), c.V1);
        c.A0 = 0u | 0x0010u;
        c.V1 = 0u | 0x0008u;
        c.V0 = 0u | 0x0020u;
        c.At = 0x80040000u;
        m.WriteU8((c.At - 0x34DBu), (byte)c.A0);
        c.At = 0x80040000u;
        m.WriteU8((c.At - 0x34DAu), (byte)c.V1);
        c.At = 0x80040000u;
        m.WriteU8((c.At - 0x34D9u), (byte)c.V0);
        c.At = 0x80050000u;
        m.WriteU8((c.At + 0x4319u), (byte)c.A0);
        c.At = 0x80050000u;
        m.WriteU8((c.At + 0x431Au), (byte)c.V1);
        c.At = 0x80050000u;
        m.WriteU8((c.At + 0x431Bu), (byte)c.V0);
        L801BADC0: ;
        c.T9 = c.T9 + 0x1u;
        c.V0 = (uint)(short)m.ReadU16(c.T8);
        c.T8 = c.T8 + 0x2u;
        m.WriteU32(c.A2, c.V0);
        c.V0 = (int)c.T9 < 36 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.A2 = c.A2 + 0x4u;
            goto L801BADC0;
        }
        c.A2 = c.A2 + 0x4u;
        c.S1 = 0x1F800000u;
        c.S1 = c.S1 | 0x0200u;
        c.V0 = 0u | 0x0004u;
        c.At = 0x1F800000u;
        m.WriteU8((c.At + 0x203u), (byte)c.V0);
        c.At = 0x1F800000u;
        m.WriteU8((c.At + 0x207u), (byte)c.V0);
        c.A0 = 0u | 0x0100u;
        c.RA = 0x801BAE00u;
        SoTN_main.SetGeomScreen(c, m);
        c.A0 = 0u | 0x0080u;
        c.A1 = 0u | 0x00A0u;
        c.RA = 0x801BAE0Cu;
        SoTN_main.SetGeomOffset(c, m);
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x217Cu;
        c.A1 = c.S3 + 0u;
        c.RA = 0x801BAE1Cu;
        SoTN_main.RotMatrix(c, m);
        c.A0 = c.S3 + 0u;
        c.RA = 0x801BAE24u;
        SoTN_main.SetRotMatrix(c, m);
        c.S4 = 0x80180000u;
        c.S4 = c.S4 + 0x216Cu;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3668u));
        c.T1 = m.ReadU32((c.S2 + 0x7Cu));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u) {
            c.T8 = c.S2 + 0x86u;
            goto L801BAE4C;
        }
        c.T8 = c.S2 + 0x86u;
        c.V0 = 0u | 0x0046u;
        goto L801BAE50;
        L801BAE4C: ;
        c.V0 = 0u | 0x0047u;
        L801BAE50: ;
        m.WriteU16((c.T1 + 0xEu), (ushort)c.V0);
        c.V0 = 0x2AAA0000u;
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x7908u));
        c.V0 = c.V0 | 0xAAABu;
        c.V1 = c.V1 << 16;
        { var _r = (long)(int)c.V1 * (int)c.V0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.A0 = 0x80090000u;
        c.A0 = m.ReadU32((c.A0 + 0x790Cu));
        c.A1 = c.HI;
        c.A0 = c.A0 << 16;
        { var _r = (long)(int)c.A0 * (int)c.V0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.T9 = 0u + 0u;
        c.V1 = (uint)((int)c.V1 >> 31);
        c.A1 = c.A1 - c.V1;
        c.V0 = m.ReadU32((c.S2 + 0x9Cu));
        c.A0 = (uint)((int)c.A0 >> 31);
        c.V0 = c.V0 - c.A1;
        m.WriteU32((c.S2 + 0x9Cu), c.V0);
        c.V0 = m.ReadU32((c.S2 + 0xA0u));
        c.V1 = c.HI;
        c.V1 = c.V1 - c.A0;
        c.A0 = m.ReadU16((c.S2 + 0x9Eu));
        c.V0 = c.V0 - c.V1;
        m.WriteU32((c.S2 + 0xA0u), c.V0);
        m.WriteU16((c.T1 + 0x20u), (ushort)c.A0);
        m.WriteU16((c.T1 + 0x8u), (ushort)c.A0);
        c.V0 = m.ReadU16((c.S2 + 0x9Eu));
        c.S6 = 0u | 0x0008u;
        c.V0 = c.V0 + 0x65u;
        m.WriteU16((c.T1 + 0x2Cu), (ushort)c.V0);
        m.WriteU16((c.T1 + 0x14u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S2 + 0xA2u));
        c.FP = 0x80040000u;
        c.FP = c.FP - 0x3668u;
        m.WriteU16((c.T1 + 0x16u), (ushort)c.V0);
        m.WriteU16((c.T1 + 0xAu), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S2 + 0xA2u));
        c.S3 = c.S4 + 0x4u;
        c.V0 = c.V0 + 0x7Bu;
        m.WriteU16((c.T1 + 0x2Eu), (ushort)c.V0);
        m.WriteU16((c.T1 + 0x22u), (ushort)c.V0);
        c.T1 = m.ReadU32(c.T1);
        L801BAF00: ;
        c.V1 = (uint)(short)m.ReadU16((c.S2 + 0x2u));
        c.V0 = (uint)(short)m.ReadU16(c.T8);
        c.T6 = c.V1 + c.V0;
        if ((int)c.T6 >= 0) {
            c.V0 = c.T6 + 0u;
            goto L801BAF1C;
        }
        c.V0 = c.T6 + 0u;
        c.V0 = c.T6 + 0x7FFu;
        L801BAF1C: ;
        c.V0 = (uint)((int)c.V0 >> 11);
        c.V0 = c.V0 << 11;
        c.T6 = c.T6 - c.V0;
        c.A2 = (uint)(short)m.ReadU16(c.S3);
        c.V0 = 0x80070000u;
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x3092u));
        c.S7 = m.ReadU16((c.S3 + 0x2u));
        if ((int)c.V0 >= 0) {
            c.V1 = c.A2 + 0x40u;
            goto L801BAF44;
        }
        c.V1 = c.A2 + 0x40u;
        c.V0 = c.V0 + 0x3u;
        L801BAF44: ;
        c.V0 = (uint)((int)c.V0 >> 2);
        c.A2 = c.V1 - c.V0;
        RecompOne.Runtime.Gte.WriteControl(6, c.A2);
        c.T7 = 0u + 0u;
        L801BAF54: ;
        c.T2 = m.ReadU16((c.T8 + 0x4u));
        c.V0 = c.T7 << 8;
        c.A2 = c.V0 + 0x1C0u;
        if ((int)c.T2 >= 0) {
            c.V1 = c.T2 + 0u;
            goto L801BAF6C;
        }
        c.V1 = c.T2 + 0u;
        c.V1 = c.T2 + 0xFFu;
        L801BAF6C: ;
        c.V1 = (uint)((int)c.V1 >> 8);
        c.V0 = c.V1 << 8;
        c.V0 = c.T2 - c.V0;
        c.A2 = c.A2 - c.V0;
        RecompOne.Runtime.Gte.WriteControl(7, c.A2);
        c.V1 = c.T7 + c.V1;
        if ((int)c.V1 >= 0) {
            c.V0 = c.V1 + 0u;
            goto L801BAF90;
        }
        c.V0 = c.V1 + 0u;
        c.V0 = c.V1 + 0x7u;
        L801BAF90: ;
        c.T2 = (uint)((int)c.V0 >> 3);
        c.V0 = c.T2 << 3;
        c.T2 = c.V1 - c.V0;
        c.A2 = c.A2 - 0x1C0u;
        c.V1 = c.A2 << 1;
        c.V1 = c.V1 + c.A2;
        c.V1 = c.V1 << 2;
        c.V0 = c.V1 + c.A2;
        c.V0 = c.V0 << 5;
        c.A3 = 0x000F0000u;
        c.T0 = c.A3 - c.V0;
        c.A0 = 0xFFFE0000u;
        c.A0 = c.A0 | 0x6000u;
        c.A3 = (uint)((int)c.T0 >> 12);
        c.A0 = c.T0 + c.A0;
        c.V0 = c.A2 << 3;
        c.V0 = c.V0 - c.A2;
        c.V0 = c.V0 << 6;
        m.WriteU32((c.SP + 0x10u), c.A3);
        c.A3 = 0x000F0000u;
        c.T0 = c.A3 - c.V0;
        c.V0 = 0xFFFE0000u;
        c.V0 = c.V0 | 0x4000u;
        c.A1 = (uint)((int)c.T0 >> 12);
        c.V0 = c.T0 + c.V0;
        c.V1 = c.V1 - c.A2;
        c.V1 = c.V1 << 5;
        c.T0 = c.A3 - c.V1;
        c.V1 = 0xFFFE0000u;
        c.V1 = c.V1 | 0xA000u;
        c.T5 = c.T6 + 0u;
        c.T3 = 0u + 0u;
        c.T4 = 0xFFFFFFFFu;
        c.A0 = (uint)((int)c.A0 >> 12);
        c.A2 = m.ReadU32(c.S4);
        c.A3 = m.ReadU8((c.SP + 0x10u));
        c.V0 = (uint)((int)c.V0 >> 12);
        m.WriteU8((c.S1 + 0x5u), (byte)c.V0);
        c.V0 = (uint)((int)c.T0 >> 12);
        c.V1 = c.T0 + c.V1;
        c.V1 = (uint)((int)c.V1 >> 12);
        m.WriteU8((c.S1 + 0x2u), (byte)c.V0);
        c.V0 = c.T2 << 3;
        m.WriteU8((c.S1 + 0x4u), (byte)c.A0);
        m.WriteU8((c.S1 + 0x1u), (byte)c.A1);
        m.WriteU8((c.S1 + 0x6u), (byte)c.V1);
        c.A0 = c.V0 + c.A2;
        m.WriteU8(c.S1, (byte)c.A3);
        L801BB050: ;
        c.T3 = c.T3 + c.T4;
        L801BB054: ;
        c.T3 = c.T3 & 0x0007u;
        c.V0 = c.A0 + c.T3;
        c.A2 = m.ReadU8(c.V0);
        c.V0 = c.T4 << 8;
        if (c.A2 == 0u) {
            c.T5 = c.T5 + c.V0;
            goto L801BB050;
        }
        c.T5 = c.T5 + c.V0;
        RecompOne.Runtime.Gte.WriteControl(5, c.T5);
        c.V0 = c.S5 + 0x18u;
        RecompOne.Runtime.Gte.LoadWord(0, m.ReadU32(c.V0));
        RecompOne.Runtime.Gte.LoadWord(1, m.ReadU32((c.V0 + 0x4u)));
        RecompOne.Runtime.Gte.Execute(0x4A180001u);
        c.V1 = 0x1F800000u;
        c.V1 = c.V1 | 0x0080u;
        c.V0 = 0u | 0x0015u;
        m.WriteU16((c.T1 + 0x32u), (ushort)c.V0);
        c.V0 = c.A2 << 4;
        c.V1 = c.V0 + c.V1;
        c.V0 = m.ReadU16(c.V1);
        m.WriteU16((c.T1 + 0xCu), (ushort)c.V0);
        c.V0 = c.S0 + 0x2Cu;
        m.WriteU32(c.V0, RecompOne.Runtime.Gte.StoreWord(14));
        RecompOne.Runtime.Gte.LoadWord(0, m.ReadU32(c.S5));
        RecompOne.Runtime.Gte.LoadWord(1, m.ReadU32((c.S5 + 0x4u)));
        RecompOne.Runtime.Gte.LoadWord(2, m.ReadU32((c.S5 + 0x8u)));
        RecompOne.Runtime.Gte.LoadWord(3, m.ReadU32((c.S5 + 0xCu)));
        RecompOne.Runtime.Gte.LoadWord(4, m.ReadU32((c.S5 + 0x10u)));
        RecompOne.Runtime.Gte.LoadWord(5, m.ReadU32((c.S5 + 0x14u)));
        RecompOne.Runtime.Gte.Execute(0x4A280030u);
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0x2Eu));
        if ((int)c.V0 >= 0) {
            goto L801BB0F0;
        }
        m.WriteU16((c.T1 + 0x32u), (ushort)c.S6);
        goto L801BB208;
        L801BB0F0: ;
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0x2Cu));
        if ((int)c.V0 >= xMin) {
            goto L801BB118;
        }
        if ((int)c.T4 >= 0) {
            m.WriteU16((c.T1 + 0x32u), (ushort)c.S6);
            goto L801BB050;
        }
        m.WriteU16((c.T1 + 0x32u), (ushort)c.S6);
        c.T4 = c.T4 + 0x2u;
        c.T5 = c.T6 - 0x100u;
        c.T3 = 0u | 0x0007u;
        goto L801BB050;
        L801BB118: ;
        c.V0 = (int)c.A2 < 5 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801BB138;
        }
        c.V0 = m.ReadU16(c.FP);
        c.V0 = c.V0 & 0x0001u;
        c.V0 = c.V0 + 0x4Au;
        goto L801BB148;
        L801BB138: ;
        c.V0 = m.ReadU16(c.FP);
        c.V0 = c.V0 & 0x0001u;
        c.V0 = c.V0 + 0x48u;
        L801BB148: ;
        m.WriteU16((c.T1 + 0xEu), (ushort)c.V0);
        c.V0 = m.ReadU16((c.V1 + 0x4u));
        m.WriteU16((c.T1 + 0x18u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.V1 + 0x8u));
        m.WriteU16((c.T1 + 0x24u), (ushort)c.V0);
        m.WriteU32((c.S0 + 0x8u), RecompOne.Runtime.Gte.StoreWord(12));
        m.WriteU32((c.S0 + 0x14u), RecompOne.Runtime.Gte.StoreWord(13));
        m.WriteU32((c.S0 + 0x20u), RecompOne.Runtime.Gte.StoreWord(14));
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0x20u));
        c.V0 = (int)c.V0 < xMax ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L801BB194;
        }
        if ((int)c.T4 > 0) {
            m.WriteU16((c.T1 + 0x32u), (ushort)c.S6);
            goto L801BB208;
        }
        m.WriteU16((c.T1 + 0x32u), (ushort)c.S6);
        c.T3 = c.T3 + c.T4;
        goto L801BB054;
        L801BB194: ;
        c.V0 = m.ReadU16((c.V1 + 0xCu));
        m.WriteU16((c.T1 + 0x30u), (ushort)c.V0);
        c.V0 = m.ReadU32((c.S0 + 0x8u));
        m.WriteU32((c.T1 + 0x8u), c.V0);
        c.V0 = m.ReadU32((c.S0 + 0x14u));
        m.WriteU32((c.T1 + 0x14u), c.V0);
        c.V0 = m.ReadU32((c.S0 + 0x20u));
        m.WriteU32((c.T1 + 0x20u), c.V0);
        c.V0 = m.ReadU32((c.S0 + 0x2Cu));
        m.WriteU32((c.T1 + 0x2Cu), c.V0);
        c.V0 = m.ReadU32(c.S1);
        m.WriteU32((c.T1 + 0x10u), c.V0);
        m.WriteU32((c.T1 + 0x4u), c.V0);
        c.V0 = m.ReadU32((c.S1 + 0x4u));
        m.WriteU16((c.T1 + 0x26u), (ushort)c.S7);
        m.WriteU32((c.T1 + 0x28u), c.V0);
        m.WriteU32((c.T1 + 0x1Cu), c.V0);
        c.T1 = m.ReadU32(c.T1);
        if (c.T1 == 0u) {
            c.T3 = c.T3 + c.T4;
            goto L801BB278;
        }
        c.T3 = c.T3 + c.T4;
        goto L801BB054;
        L801BB208: ;
        c.T7 = c.T7 + 0x1u;
        c.V0 = (int)c.T7 < 8 ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L801BAF54;
        }
        c.T9 = c.T9 + 0x1u;
        c.S3 = c.S3 + 0x8u;
        c.S4 = c.S4 + 0x8u;
        c.V0 = (int)c.T9 < 2 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.T8 = c.T8 + 0x8u;
            goto L801BAF00;
        }
        c.T8 = c.T8 + 0x8u;
        if (c.T1 == 0u) {
            c.V0 = 0u | 0x0008u;
            goto L801BB24C;
        }
        c.V0 = 0u | 0x0008u;
        L801BB238: ;
        m.WriteU16((c.T1 + 0x32u), (ushort)c.V0);
        c.T1 = m.ReadU32(c.T1);
        if (c.T1 != 0u) {
            goto L801BB238;
        }
        L801BB24C: ;
        c.V0 = 0x80070000u;
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x308Eu));
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)m.ReadU16((c.V1 + 0x33DAu));
        c.T5 = c.V0 + c.V1;
        c.V0 = (int)c.T5 < 256 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801BB278;
        }
        L801BB270: ;
        c.A0 = c.S2 + 0u;
        c.RA = 0x801BB278u;
        SoTN_st0.DestroyEntity_st0(c, m);
        L801BB278: ;
        c.RA = m.ReadU32((c.SP + 0x3Cu));
        c.FP = m.ReadU32((c.SP + 0x38u));
        c.S7 = m.ReadU32((c.SP + 0x34u));
        c.S6 = m.ReadU32((c.SP + 0x30u));
        c.S5 = m.ReadU32((c.SP + 0x2Cu));
        c.S4 = m.ReadU32((c.SP + 0x28u));
        c.S3 = m.ReadU32((c.SP + 0x24u));
        c.S2 = m.ReadU32((c.SP + 0x20u));
        c.S1 = m.ReadU32((c.SP + 0x1Cu));
        c.S0 = m.ReadU32((c.SP + 0x18u));
        c.SP = c.SP + 0x40u;
        return;
    }

    // vortex eff
    public static void VortexWide(CpuContext c, IMemory m)
    {
        int xMin = -StageMargin();
        int xMax = 257 + StageMargin();
        c.SP = c.SP - 0x100u;
        m.WriteU32((c.SP + 0xE4u), c.S3);
        c.S3 = c.A0 + 0u;
        m.WriteU32((c.SP + 0xFCu), c.RA);
        m.WriteU32((c.SP + 0xF8u), c.FP);
        m.WriteU32((c.SP + 0xF4u), c.S7);
        m.WriteU32((c.SP + 0xF0u), c.S6);
        m.WriteU32((c.SP + 0xECu), c.S5);
        m.WriteU32((c.SP + 0xE8u), c.S4);
        m.WriteU32((c.SP + 0xE0u), c.S2);
        m.WriteU32((c.SP + 0xDCu), c.S1);
        m.WriteU32((c.SP + 0xD8u), c.S0);
        c.V1 = m.ReadU16((c.S3 + 0x2Cu));
        c.A0 = 0u | 0x0001u;
        if (c.V1 == c.A0) {
            c.V0 = (int)c.V1 < 2 ? 1u : 0u;
            goto L801BE2E0;
        }
        c.V0 = (int)c.V1 < 2 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801BE20C;
        }
        if (c.V1 == 0u) {
            goto L801BE228;
        }
        goto L801BEAD0;
        L801BE20C: ;
        c.V0 = 0u | 0x0002u;
        if (c.V1 == c.V0) {
            c.V0 = 0u | 0x0003u;
            goto L801BE3DC;
        }
        c.V0 = 0u | 0x0003u;
        if (c.V1 == c.V0) {
            goto L801BE578;
        }
        goto L801BEAD0;
        L801BE228: ;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x5D4u;
        c.RA = 0x801BE238u;
        SoTN_st0.InitializeEntity_st0(c, m);
        c.A0 = 0u | 0x0004u;
        c.V0 = 0u | 0x0080u;
        m.WriteU16((c.S3 + 0x2u), (ushort)c.V0);
        c.V0 = 0u | 0x01A0u;
        m.WriteU16((c.S3 + 0x6u), (ushort)c.V0);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3820u));
        c.A1 = 0u | 0x0110u;
        c.RA = 0x801BE260u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.A0 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.A0 == c.V0) {
            c.V0 = c.A0 << 1;
            goto L801BE544;
        }
        c.V0 = c.A0 << 1;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.S0 = c.V0 + c.V1;
        c.V0 = m.ReadU32((c.S3 + 0x34u));
        c.V1 = 0x00800000u;
        m.WriteU32((c.S3 + 0x64u), c.A0);
        m.WriteU32((c.S3 + 0x7Cu), c.S0);
        c.V0 = c.V0 | c.V1;
        if (c.S0 == 0u) {
            m.WriteU32((c.S3 + 0x34u), c.V0);
            goto L801BE2C4;
        }
        m.WriteU32((c.S3 + 0x34u), c.V0);
        c.V0 = 0u | 0x0008u;
        L801BE2B0: ;
        m.WriteU16((c.S0 + 0x32u), (ushort)c.V0);
        c.S0 = m.ReadU32(c.S0);
        if (c.S0 != 0u) {
            goto L801BE2B0;
        }
        L801BE2C4: ;
        c.V0 = 0u | 0x05F0u;
        m.WriteU16((c.S3 + 0x9Cu), (ushort)c.V0);
        c.V0 = 0u | 0x00B0u;
        m.WriteU16((c.S3 + 0x9Eu), (ushort)c.V0);
        c.V0 = 0u | 0x00D0u;
        m.WriteU16((c.S3 + 0xA0u), (ushort)0u);
        m.WriteU16((c.S3 + 0xAEu), (ushort)c.V0);
        L801BE2E0: ;
        c.S7 = 0u | 0x0060u;
        c.S6 = 0u | 0x0010u;
        c.S5 = 0u + 0u;
        c.S4 = 0u + 0u;
        m.WriteU16((c.SP + 0xC8u), (ushort)0u);
        m.WriteU32((c.SP + 0xD0u), 0u);
        L801BE2F8: ;
        c.A0 = m.ReadU32((c.SP + 0xD0u));
        c.FP = 0u + 0u;
        c.RA = 0x801BE304u;
        SoTN_main.SquareRoot0(c, m);
        c.V0 = c.V0 << 2;
        c.T3 = m.ReadU16((c.SP + 0xC8u));
        c.S1 = c.S5 << 3;
        m.WriteU32((c.SP + 0xC0u), c.V0);
        c.V0 = c.T3 << 16;
        c.S2 = (uint)((int)c.V0 >> 16);
        L801BE31C: ;
        c.A0 = c.S2 + 0u;
        c.RA = 0x801BE324u;
        SoTN_main.rcos(c, m);
        { var _r = (long)(int)c.S7 * (int)c.V0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.A0 = c.S2 + 0u;
        c.S0 = c.LO;
        c.S0 = (uint)((int)c.S0 >> 12);
        c.RA = 0x801BE338u;
        SoTN_main.rsin(c, m);
        { var _r = (long)(int)c.S7 * (int)c.V0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.S5 = c.S5 + 0x1u;
        c.T3 = m.ReadU32((c.SP + 0xC0u));
        c.S2 = c.S2 + 0x100u;
        m.WriteU32((c.SP + 0xB8u), c.T3);
        c.V0 = m.ReadU16((c.SP + 0xB8u));
        c.FP = c.FP + 0x1u;
        c.At = 0x801C0000u;
        c.At = c.At + c.S1;
        m.WriteU16((c.At + 0x1BC8u), (ushort)c.S0);
        c.At = 0x801C0000u;
        c.At = c.At + c.S1;
        m.WriteU16((c.At + 0x1BCCu), (ushort)c.V0);
        c.V0 = c.LO;
        c.V0 = (uint)((int)c.V0 >> 12);
        c.At = 0x801C0000u;
        c.At = c.At + c.S1;
        m.WriteU16((c.At + 0x1BCAu), (ushort)c.V0);
        c.V0 = (int)c.FP < 16 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.S1 = c.S1 + 0x8u;
            goto L801BE31C;
        }
        c.S1 = c.S1 + 0x8u;
        c.S7 = c.S7 + 0x1Cu;
        c.At = 0x801C0000u;
        c.At = c.At + c.S4;
        m.WriteU8((c.At + 0x23C8u), (byte)c.S6);
        c.S6 = c.S6 + 0x28u;
        c.V0 = c.S6 & 0x00FFu;
        c.T3 = m.ReadU16((c.SP + 0xC8u));
        c.V0 = c.V0 < 0x000000A1u ? 1u : 0u;
        c.T3 = c.T3 + 0x80u;
        if (c.V0 != 0u) {
            m.WriteU16((c.SP + 0xC8u), (ushort)c.T3);
            goto L801BE3BC;
        }
        m.WriteU16((c.SP + 0xC8u), (ushort)c.T3);
        c.S6 = 0u | 0x00A0u;
        L801BE3BC: ;
        c.S4 = c.S4 + 0x1u;
        c.T3 = m.ReadU32((c.SP + 0xD0u));
        c.V0 = (int)c.S4 < 16 ? 1u : 0u;
        c.T3 = c.T3 + 0x120u;
        if (c.V0 != 0u) {
            m.WriteU32((c.SP + 0xD0u), c.T3);
            goto L801BE2F8;
        }
        m.WriteU32((c.SP + 0xD0u), c.T3);
        goto L801BE564;
        L801BE3DC: ;
        c.S0 = m.ReadU32((c.S3 + 0x7Cu));
        if (c.S0 == 0u) {
            c.A1 = 0u | 0x0016u;
            goto L801BE438;
        }
        c.A1 = 0u | 0x0016u;
        c.A0 = 0u | 0x019Eu;
        c.V0 = 0u | 0x0040u;
        c.V1 = 0u | 0x0080u;
        L801BE3F8: ;
        m.WriteU16((c.S0 + 0x1Au), (ushort)c.A1);
        m.WriteU16((c.S0 + 0xEu), (ushort)c.A0);
        m.WriteU8((c.S0 + 0x24u), (byte)c.V0);
        m.WriteU8((c.S0 + 0xCu), (byte)c.V0);
        m.WriteU8((c.S0 + 0x30u), (byte)c.V1);
        m.WriteU8((c.S0 + 0x18u), (byte)c.V1);
        m.WriteU8((c.S0 + 0x19u), (byte)0u);
        m.WriteU8((c.S0 + 0xDu), (byte)0u);
        m.WriteU8((c.S0 + 0x31u), (byte)c.V0);
        m.WriteU8((c.S0 + 0x25u), (byte)c.V0);
        m.WriteU16((c.S0 + 0x26u), (ushort)c.V1);
        m.WriteU16((c.S0 + 0x32u), (ushort)0u);
        c.S0 = m.ReadU32(c.S0);
        if (c.S0 != 0u) {
            goto L801BE3F8;
        }
        L801BE438: ;
        c.A3 = c.SP + 0x48u;
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 - 0x3C84u));
        c.S0 = m.ReadU32((c.S3 + 0x7Cu));
        c.A2 = c.V0 + 0x4u;
        c.T0 = c.V0 + 0x54u;
        L801BE450: ;
        c.V0 = m.ReadU32(c.A2);
        c.V1 = m.ReadU32((c.A2 + 0x4u));
        c.A0 = m.ReadU32((c.A2 + 0x8u));
        c.A1 = m.ReadU32((c.A2 + 0xCu));
        m.WriteU32(c.A3, c.V0);
        m.WriteU32((c.A3 + 0x4u), c.V1);
        m.WriteU32((c.A3 + 0x8u), c.A0);
        m.WriteU32((c.A3 + 0xCu), c.A1);
        c.A2 = c.A2 + 0x10u;
        if (c.A2 != c.T0) {
            c.A3 = c.A3 + 0x10u;
            goto L801BE450;
        }
        c.A3 = c.A3 + 0x10u;
        c.V0 = m.ReadU32(c.A2);
        c.V1 = m.ReadU32((c.A2 + 0x4u));
        c.A0 = m.ReadU32((c.A2 + 0x8u));
        m.WriteU32(c.A3, c.V0);
        m.WriteU32((c.A3 + 0x4u), c.V1);
        m.WriteU32((c.A3 + 0x8u), c.A0);
        c.S2 = 0x80040000u;
        c.S2 = c.S2 - 0x3804u;
        c.V0 = m.ReadU32(c.S2);
        c.A0 = c.S0 + 0u;
        c.RA = 0x801BE4ACu;
        Dispatcher.Call(c, m, c.V0);
        c.A2 = c.V0 + 0u;
        if (c.A2 == 0u) {
            c.S1 = 0u | 0x0007u;
            goto L801BE544;
        }
        c.S1 = 0u | 0x0007u;
        c.V0 = 0u | 0x0040u;
        m.WriteU8((c.S0 + 0x7u), (byte)c.S1);
        m.WriteU8((c.SP + 0x60u), (byte)0u);
        m.WriteU8((c.SP + 0x61u), (byte)0u);
        m.WriteU8((c.SP + 0x62u), (byte)0u);
        m.WriteU8((c.SP + 0x63u), (byte)0u);
        m.WriteU8((c.SP + 0x5Eu), (byte)0u);
        m.WriteU16((c.SP + 0x48u), (ushort)0u);
        m.WriteU16((c.SP + 0x50u), (ushort)0u);
        m.WriteU16((c.SP + 0xA8u), (ushort)0u);
        m.WriteU16((c.SP + 0xAAu), (ushort)0u);
        m.WriteU16((c.SP + 0xACu), (ushort)c.V0);
        m.WriteU16((c.SP + 0xAEu), (ushort)c.V0);
        c.V0 = m.ReadWordLeft(c.V0, (c.SP + 0xABu));
        c.V0 = m.ReadWordRight(c.V0, (c.SP + 0xA8u));
        c.V1 = m.ReadWordLeft(c.V1, (c.SP + 0xAFu));
        c.V1 = m.ReadWordRight(c.V1, (c.SP + 0xACu));
        m.WriteWordLeft((c.SP + 0x57u), c.V0);
        m.WriteWordRight((c.SP + 0x54u), c.V0);
        m.WriteWordLeft((c.SP + 0x5Bu), c.V1);
        m.WriteWordRight((c.SP + 0x58u), c.V1);
        c.A0 = c.A2 + 0u;
        c.A1 = c.SP + 0x48u;
        c.RA = 0x801BE518u;
        SoTN_main.SetDrawEnv(c, m);
        c.V0 = 0u | 0x0002u;
        m.WriteU16((c.S0 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x1000u;
        m.WriteU16((c.S0 + 0x32u), (ushort)c.V0);
        c.S0 = m.ReadU32(c.S0);
        c.V0 = m.ReadU32(c.S2);
        c.A0 = c.S0 + 0u;
        c.RA = 0x801BE53Cu;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0059u;
            goto L801BE554;
        }
        c.V0 = 0u | 0x0059u;
        L801BE544: ;
        c.A0 = c.S3 + 0u;
        c.RA = 0x801BE54Cu;
        SoTN_st0.DestroyEntity_st0(c, m);
        goto L801BEAD0;
        L801BE554: ;
        m.WriteU16((c.S0 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0800u;
        m.WriteU8((c.S0 + 0x7u), (byte)c.S1);
        m.WriteU16((c.S0 + 0x32u), (ushort)c.V0);
        L801BE564: ;
        c.V0 = m.ReadU16((c.S3 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S3 + 0x2Cu), (ushort)c.V0);
        goto L801BEAD0;
        L801BE578: ;
        c.V0 = m.ReadU16((c.S3 + 0x2Eu));
        if (c.V0 == 0u) {
            c.V1 = 0xFFFF0000u;
            goto L801BE598;
        }
        c.V1 = 0xFFFF0000u;
        if (c.V0 == c.A0) {
            goto L801BE5D0;
        }
        goto L801BE660;
        L801BE598: ;
        c.V0 = m.ReadU32((c.S3 + 0x4u));
        c.V1 = c.V1 | 0x4000u;
        c.V0 = c.V0 + c.V1;
        m.WriteU32((c.S3 + 0x4u), c.V0);
        c.V0 = (uint)(short)m.ReadU16((c.S3 + 0x6u));
        c.V0 = (int)c.V0 < 160 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V1 = 0u | 0x00A0u;
            goto L801BE660;
        }
        c.V1 = 0u | 0x00A0u;
        c.V0 = m.ReadU16((c.S3 + 0x2Eu));
        m.WriteU16((c.S3 + 0x6u), (ushort)c.V1);
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S3 + 0x2Eu), (ushort)c.V0);
        goto L801BE660;
        L801BE5D0: ;
        c.V0 = m.ReadU16((c.S3 + 0xA4u));
        c.V1 = m.ReadU16((c.S3 + 0xAAu));
        c.V0 = c.V0 + 0x10u;
        m.WriteU16((c.S3 + 0xA4u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S3 + 0xA6u));
        c.A0 = (uint)(short)m.ReadU16((c.S3 + 0xA4u));
        c.V0 = c.V0 + 0x8u;
        m.WriteU16((c.S3 + 0xA6u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S3 + 0xA8u));
        c.V1 = c.V1 + 0x2u;
        m.WriteU16((c.S3 + 0xAAu), (ushort)c.V1);
        c.V0 = c.V0 + 0x20u;
        m.WriteU16((c.S3 + 0xA8u), (ushort)c.V0);
        c.RA = 0x801BE608u;
        SoTN_main.rsin(c, m);
        c.V0 = c.V0 << 6;
        c.V0 = (uint)((int)c.V0 >> 12);
        c.A0 = (uint)(short)m.ReadU16((c.S3 + 0xA6u));
        c.V0 = c.V0 + 0x5F0u;
        m.WriteU16((c.S3 + 0x9Cu), (ushort)c.V0);
        c.RA = 0x801BE620u;
        SoTN_main.rsin(c, m);
        c.V0 = c.V0 << 7;
        c.V0 = (uint)((int)c.V0 >> 12);
        c.A0 = (uint)(short)m.ReadU16((c.S3 + 0xA8u));
        c.V0 = c.V0 + 0xB0u;
        m.WriteU16((c.S3 + 0x9Eu), (ushort)c.V0);
        c.RA = 0x801BE638u;
        SoTN_main.rsin(c, m);
        c.A0 = (uint)(short)m.ReadU16((c.S3 + 0xAAu));
        c.V0 = c.V0 >> 8;
        m.WriteU16((c.S3 + 0xA0u), (ushort)c.V0);
        c.RA = 0x801BE648u;
        SoTN_main.rsin(c, m);
        c.V1 = c.V0 << 1;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 3;
        c.V1 = (uint)((int)c.V1 >> 12);
        c.V1 = c.V1 + 0xD0u;
        m.WriteU16((c.S3 + 0xAEu), (ushort)c.V1);
        L801BE660: ;
        c.S0 = m.ReadU32((c.S3 + 0x7Cu));
        c.S0 = m.ReadU32(c.S0);
        c.S0 = m.ReadU32(c.S0);
        c.A1 = m.ReadU8((c.S0 + 0xDu));
        c.A0 = m.ReadU8((c.S0 + 0x25u));
        c.A1 = c.A1 + 0x2u;
        c.A2 = c.A0 + 0x2u;
        c.A0 = c.A2 + 0u;
        c.V0 = c.A0 & 0x00FFu;
        c.V1 = c.A1 & 0x00FFu;
        c.V0 = c.V0 < c.V1 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801BE6A8;
        }
        c.A1 = c.A0 + 0u;
        c.A0 = c.A2 + 0x40u;
        L801BE6A8: ;
        if (c.S0 == 0u) {
            goto L801BE6D0;
        }
        L801BE6B0: ;
        m.WriteU8((c.S0 + 0x19u), (byte)c.A1);
        m.WriteU8((c.S0 + 0xDu), (byte)c.A1);
        m.WriteU8((c.S0 + 0x31u), (byte)c.A0);
        m.WriteU8((c.S0 + 0x25u), (byte)c.A0);
        c.S0 = m.ReadU32(c.S0);
        if (c.S0 != 0u) {
            goto L801BE6B0;
        }
        L801BE6D0: ;
        c.V0 = m.ReadU16((c.S3 + 0x80u));
        c.A0 = 0u | 0x00C0u;
        c.V0 = c.V0 - 0x4u;
        m.WriteU16((c.S3 + 0x80u), (ushort)c.V0);
        c.RA = 0x801BE6E4u;
        SoTN_main.SetGeomScreen(c, m);
        c.A0 = (uint)(short)m.ReadU16((c.S3 + 0x2u));
        c.A1 = (uint)(short)m.ReadU16((c.S3 + 0x6u));
        c.FP = 0u + 0u;
        c.RA = 0x801BE6F4u;
        SoTN_main.SetGeomOffset(c, m);
        c.A0 = 0u + 0u;
        c.A1 = 0u + 0u;
        c.A2 = 0u + 0u;
        c.RA = 0x801BE704u;
        SoTN_main.SetFarColor(c, m);
        c.A0 = 0u | 0x0128u;
        c.A1 = 0u | 0x00C0u;
        c.RA = 0x801BE710u;
        SoTN_main.SetFogNear(c, m);
        m.WriteU16((c.SP + 0x10u), (ushort)0u);
        m.WriteU16((c.SP + 0x12u), (ushort)0u);
        c.V1 = m.ReadU16((c.S3 + 0x80u));
        m.WriteU16((c.SP + 0x14u), (ushort)c.V1);
        c.V0 = m.ReadU16((c.S3 + 0x9Cu));
        c.A0 = c.SP + 0x10u;
        m.WriteU16((c.SP + 0x10u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S3 + 0x9Eu));
        c.S0 = c.SP + 0x28u;
        m.WriteU16((c.SP + 0x12u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S3 + 0xA0u));
        c.A1 = c.S0 + 0u;
        c.V1 = c.V1 + c.V0;
        m.WriteU16((c.SP + 0x14u), (ushort)c.V1);
        c.RA = 0x801BE750u;
        SoTN_main.RotMatrix(c, m);
        c.A0 = c.S0 + 0u;
        c.RA = 0x801BE758u;
        SoTN_main.SetRotMatrix(c, m);
        c.A0 = c.S0 + 0u;
        c.V0 = 0u | 0x00C0u;
        m.WriteU32((c.SP + 0x18u), 0u);
        m.WriteU32((c.SP + 0x1Cu), 0u);
        m.WriteU32((c.SP + 0x20u), c.V0);
        c.V0 = (uint)(short)m.ReadU16((c.S3 + 0xAEu));
        c.A1 = c.SP + 0x18u;
        c.V0 = c.V0 + 0xC0u;
        m.WriteU32((c.SP + 0x20u), c.V0);
        c.RA = 0x801BE780u;
        SoTN_main.TransMatrix(c, m);
        c.A0 = c.S0 + 0u;
        c.RA = 0x801BE788u;
        SoTN_main.SetTransMatrix(c, m);
        c.A2 = 0x1F800000u;
        c.A3 = 0u | 0x0004u;
        c.A0 = 0x1F800000u;
        c.A0 = c.A0 | 0x0003u;
        c.A1 = 0x801C0000u;
        c.A1 = c.A1 + 0x23C8u;
        L801BE7A0: ;
        c.V1 = c.FP & 0x0001u;
        c.V1 = c.V1 << 1;
        c.V0 = m.ReadU8(c.A1);
        c.S5 = c.V1 + 0x1u;
        if (c.S5 != 0u) { if ((int)c.V0 == int.MinValue && (int)c.S5 == -1) { c.LO = 0x80000000u; c.HI = 0u; } else { c.LO = (uint)((int)c.V0 / (int)c.S5); c.HI = (uint)((int)c.V0 % (int)c.S5); } }
        if (c.S5 != 0u) {
            goto L801BE7C0;
        }
        Bios.Break(c, m);
        L801BE7C0: ;
        c.At = 0xFFFFFFFFu;
        if (c.S5 != c.At) {
            c.At = 0x80000000u;
            goto L801BE7D8;
        }
        c.At = 0x80000000u;
        if (c.V0 != c.At) {
            goto L801BE7D8;
        }
        Bios.Break(c, m);
        L801BE7D8: ;
        c.V0 = c.LO;
        m.WriteU8((c.A0 - 0x1u), (byte)c.V0);
        c.V0 = m.ReadU8(c.A1);
        c.V0 = c.V0 >> 1;
        if (c.S5 != 0u) { if ((int)c.V0 == int.MinValue && (int)c.S5 == -1) { c.LO = 0x80000000u; c.HI = 0u; } else { c.LO = (uint)((int)c.V0 / (int)c.S5); c.HI = (uint)((int)c.V0 % (int)c.S5); } }
        if (c.S5 != 0u) {
            goto L801BE800;
        }
        Bios.Break(c, m);
        L801BE800: ;
        c.At = 0xFFFFFFFFu;
        if (c.S5 != c.At) {
            c.At = 0x80000000u;
            goto L801BE818;
        }
        c.At = 0x80000000u;
        if (c.V0 != c.At) {
            goto L801BE818;
        }
        Bios.Break(c, m);
        L801BE818: ;
        c.V0 = c.LO;
        c.FP = c.FP + 0x1u;
        c.A1 = c.A1 + 0x1u;
        m.WriteU8((c.A0 - 0x2u), (byte)c.V0);
        m.WriteU8(c.A2, (byte)c.V0);
        m.WriteU8(c.A0, (byte)c.A3);
        c.A0 = c.A0 + 0x4u;
        c.V0 = (int)c.FP < 16 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.A2 = c.A2 + 0x4u;
            goto L801BE7A0;
        }
        c.A2 = c.A2 + 0x4u;
        c.FP = 0u + 0u;
        c.T0 = 0x801C0000u;
        c.T0 = c.T0 + 0x1BC8u;
        c.T2 = c.T0 + 0x80u;
        c.S0 = m.ReadU32((c.S3 + 0x7Cu));
        c.T1 = 0u | 0x0004u;
        c.S0 = m.ReadU32(c.S0);
        c.A3 = 0x1F800000u;
        c.S0 = m.ReadU32(c.S0);
        c.V0 = 0u | 0x0080u;
        m.WriteU8((c.SP + 0xB0u), (byte)c.V0);
        m.WriteU8((c.SP + 0xB1u), (byte)c.V0);
        m.WriteU8((c.SP + 0xB2u), (byte)c.V0);
        c.V0 = 0u | 0x0004u;
        m.WriteU8((c.SP + 0xB3u), (byte)c.V0);
        L801BE87C: ;
        c.S4 = 0u + 0u;
        c.S5 = c.FP << 4;
        c.A2 = c.A3 + 0u;
        c.A1 = c.A3 + 0x4u;
        c.V0 = c.S5 + c.S4;
        L801BE890: ;
        c.V1 = c.S4 + 0x1u;
        c.V1 = c.V1 & 0x000Fu;
        c.V1 = c.S5 + c.V1;
        c.A0 = c.V0 << 3;
        c.V0 = c.A0 + c.T0;
        RecompOne.Runtime.Gte.LoadWord(0, m.ReadU32(c.V0));
        RecompOne.Runtime.Gte.LoadWord(1, m.ReadU32((c.V0 + 0x4u)));
        RecompOne.Runtime.Gte.Execute(0x4A180001u);
        c.V0 = c.S0 + 0x8u;
        m.WriteU32(c.V0, RecompOne.Runtime.Gte.StoreWord(14));
        RecompOne.Runtime.Gte.LoadWord(6, m.ReadU32(c.A2));
        RecompOne.Runtime.Gte.Execute(0x4A780010u);
        c.V0 = c.S0 + 0x4u;
        m.WriteU32(c.V0, RecompOne.Runtime.Gte.StoreWord(22));
        c.V1 = c.V1 << 3;
        c.V0 = c.V1 + c.T0;
        m.WriteU8((c.S0 + 0x7u), (byte)c.T1);
        RecompOne.Runtime.Gte.LoadWord(0, m.ReadU32(c.V0));
        RecompOne.Runtime.Gte.LoadWord(1, m.ReadU32((c.V0 + 0x4u)));
        RecompOne.Runtime.Gte.Execute(0x4A180001u);
        c.V0 = c.S0 + 0x14u;
        m.WriteU32(c.V0, RecompOne.Runtime.Gte.StoreWord(14));
        RecompOne.Runtime.Gte.LoadWord(6, m.ReadU32(c.A2));
        RecompOne.Runtime.Gte.Execute(0x4A780010u);
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0xAu));
        if ((int)c.V0 >= 0) {
            c.V0 = c.S0 + 0x10u;
            goto L801BE930;
        }
        c.V0 = c.S0 + 0x10u;
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0x16u));
        if ((int)c.V0 < 0) {
            c.V0 = c.S0 + 0x10u;
            goto L801BEA94;
        }
        c.V0 = c.S0 + 0x10u;
        L801BE930: ;
        m.WriteU32(c.V0, RecompOne.Runtime.Gte.StoreWord(22));
        c.V0 = c.A0 + c.T2;
        RecompOne.Runtime.Gte.LoadWord(0, m.ReadU32(c.V0));
        RecompOne.Runtime.Gte.LoadWord(1, m.ReadU32((c.V0 + 0x4u)));
        RecompOne.Runtime.Gte.Execute(0x4A180001u);
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0xAu));
        c.V0 = (int)c.V0 < 257 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V0 = c.S0 + 0x20u;
            goto L801BE974;
        }
        c.V0 = c.S0 + 0x20u;
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0x16u));
        c.V0 = (int)c.V0 < 257 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = c.S0 + 0x20u;
            goto L801BEA94;
        }
        c.V0 = c.S0 + 0x20u;
        L801BE974: ;
        m.WriteU32(c.V0, RecompOne.Runtime.Gte.StoreWord(14));
        RecompOne.Runtime.Gte.LoadWord(6, m.ReadU32(c.A1));
        RecompOne.Runtime.Gte.Execute(0x4A780010u);
        c.V0 = c.S0 + 0x1Cu;
        m.WriteU32(c.V0, RecompOne.Runtime.Gte.StoreWord(22));
        c.V0 = c.V1 + c.T2;
        RecompOne.Runtime.Gte.LoadWord(0, m.ReadU32(c.V0));
        RecompOne.Runtime.Gte.LoadWord(1, m.ReadU32((c.V0 + 0x4u)));
        RecompOne.Runtime.Gte.Execute(0x4A180001u);
        c.V0 = c.S0 + 0x2Cu;
        m.WriteU32(c.V0, RecompOne.Runtime.Gte.StoreWord(14));
        RecompOne.Runtime.Gte.LoadWord(6, m.ReadU32(c.A1));
        RecompOne.Runtime.Gte.Execute(0x4A780010u);
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0x8u));
        c.V0 = (int)c.V0 < xMax ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V0 = c.S0 + 0x28u;
            goto L801BEA10;
        }
        c.V0 = c.S0 + 0x28u;
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0x14u));
        c.V0 = (int)c.V0 < xMax ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V0 = c.S0 + 0x28u;
            goto L801BEA10;
        }
        c.V0 = c.S0 + 0x28u;
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0x20u));
        c.V0 = (int)c.V0 < xMax ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V0 = c.S0 + 0x28u;
            goto L801BEA10;
        }
        c.V0 = c.S0 + 0x28u;
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0x2Cu));
        c.V0 = (int)c.V0 < xMax ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = c.S0 + 0x28u;
            goto L801BEA94;
        }
        c.V0 = c.S0 + 0x28u;
        L801BEA10: ;
        m.WriteU32(c.V0, RecompOne.Runtime.Gte.StoreWord(22));
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0x8u));
        if ((int)c.V0 >= xMin) {
            c.V0 = c.SP + 0xB8u;
            goto L801BEA54;
        }
        c.V0 = c.SP + 0xB8u;
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0x14u));
        if ((int)c.V0 >= xMin) {
            c.V0 = c.SP + 0xB8u;
            goto L801BEA54;
        }
        c.V0 = c.SP + 0xB8u;
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0x20u));
        if ((int)c.V0 >= xMin) {
            c.V0 = c.SP + 0xB8u;
            goto L801BEA54;
        }
        c.V0 = c.SP + 0xB8u;
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0x2Cu));
        if ((int)c.V0 < xMin) {
            c.V0 = c.SP + 0xB8u;
            goto L801BEA94;
        }
        c.V0 = c.SP + 0xB8u;
        L801BEA54: ;
        c.T4 = RecompOne.Runtime.Gte.Read(19);
        c.T4 = (uint)((int)c.T4 >> 2);
        m.WriteU32(c.V0, c.T4);
        c.V0 = m.ReadU32((c.SP + 0xB8u));
        c.V0 = (int)c.V0 < 85 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = 0u | 0x0058u;
            goto L801BEA84;
        }
        c.V0 = 0u | 0x0058u;
        c.V1 = m.ReadU16((c.SP + 0xB8u));
        c.V0 = c.V0 - c.V1;
        goto L801BEA88;
        L801BEA84: ;
        c.V0 = 0u | 0x0003u;
        L801BEA88: ;
        m.WriteU16((c.S0 + 0x26u), (ushort)c.V0);
        m.WriteU16((c.S0 + 0x32u), (ushort)c.T1);
        c.S0 = m.ReadU32(c.S0);
        L801BEA94: ;
        c.S4 = c.S4 + 0x1u;
        c.V0 = (int)c.S4 < 16 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V0 = c.S5 + c.S4;
            goto L801BE890;
        }
        c.V0 = c.S5 + c.S4;
        c.FP = c.FP + 0x1u;
        c.V0 = (int)c.FP < 15 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.A3 = c.A3 + 0x4u;
            goto L801BE87C;
        }
        c.A3 = c.A3 + 0x4u;
        if (c.S0 == 0u) {
            c.V0 = 0u | 0x0008u;
            goto L801BEAD0;
        }
        c.V0 = 0u | 0x0008u;
        L801BEABC: ;
        m.WriteU16((c.S0 + 0x32u), (ushort)c.V0);
        c.S0 = m.ReadU32(c.S0);
        if (c.S0 != 0u) {
            goto L801BEABC;
        }
        L801BEAD0: ;
        c.RA = m.ReadU32((c.SP + 0xFCu));
        c.FP = m.ReadU32((c.SP + 0xF8u));
        c.S7 = m.ReadU32((c.SP + 0xF4u));
        c.S6 = m.ReadU32((c.SP + 0xF0u));
        c.S5 = m.ReadU32((c.SP + 0xECu));
        c.S4 = m.ReadU32((c.SP + 0xE8u));
        c.S3 = m.ReadU32((c.SP + 0xE4u));
        c.S2 = m.ReadU32((c.SP + 0xE0u));
        c.S1 = m.ReadU32((c.SP + 0xDCu));
        c.S0 = m.ReadU32((c.SP + 0xD8u));
        c.SP = c.SP + 0x100u;
        return;
    }

    // title card fade fx
    public static void TitleFadeoutWide(CpuContext c, IMemory m)
    {
        int margin = StageMargin();
        c.SP = c.SP - 0x20u;
        m.WriteU32((c.SP + 0x18u), c.S0);
        c.S0 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x1Cu), c.RA);
        c.V1 = m.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V1 < 0x00000007u ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = c.V1 << 2;
            goto L801AB5D0;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x801A0000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At + 0x7AE0u));
        switch (c.V0)
        {
            case 0x801AB104u: goto L801AB104;
            case 0x801AB20Cu: goto L801AB20C;
            case 0x801AB268u: goto L801AB268;
            case 0x801AB2ECu: goto L801AB2EC;
            case 0x801AB344u: goto L801AB344;
            case 0x801AB3D4u: goto L801AB3D4;
            case 0x801AB4E4u: goto L801AB4E4;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801AB104: ;
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0x908u));
        if (c.V0 != 0u) {
            goto L801AB5B8;
        }
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x5D4u;
        c.RA = 0x801AB128u;
        SoTN_st0.InitializeEntity_st0(c, m);
        c.A0 = 0u | 0x0003u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0005u;
        c.RA = 0x801AB140u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.A0 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.A0 == c.V0) {
            c.V0 = c.A0 << 1;
            goto L801AB5B8;
        }
        c.V0 = c.A0 << 1;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A2 = c.V0 + c.V1;
        c.V0 = m.ReadU32((c.S0 + 0x34u));
        c.V1 = 0x00800000u;
        m.WriteU32((c.S0 + 0x64u), c.A0);
        m.WriteU32((c.S0 + 0x7Cu), c.A2);
        c.V0 = c.V0 | c.V1;
        if (c.A2 == 0u) {
            m.WriteU32((c.S0 + 0x34u), c.V0);
            goto L801AB1A4;
        }
        m.WriteU32((c.S0 + 0x34u), c.V0);
        c.V0 = 0u | 0x0008u;
        L801AB190: ;
        m.WriteU16((c.A2 + 0x32u), (ushort)c.V0);
        c.A2 = m.ReadU32(c.A2);
        if (c.A2 != 0u) {
            goto L801AB190;
        }
        L801AB1A4: ;
        c.A2 = m.ReadU32((c.S0 + 0x7Cu));
        m.WriteU8((c.A2 + 0x6u), (byte)0u);
        m.WriteU8((c.A2 + 0x5u), (byte)0u);
        m.WriteU8((c.A2 + 0x4u), (byte)0u);
        c.V1 = m.ReadU32((c.A2 + 0x4u));
        c.A0 = m.ReadU32((c.A2 + 0x4u));
        c.A1 = m.ReadU32((c.A2 + 0x4u));
        c.V0 = (uint)(0x100 + margin);
        m.WriteU16((c.A2 + 0x2Cu), (ushort)c.V0);
        m.WriteU16((c.A2 + 0x14u), (ushort)c.V0);
        c.V0 = 0u | 0x00F0u;
        m.WriteU16((c.A2 + 0x2Eu), (ushort)c.V0);
        m.WriteU16((c.A2 + 0x22u), (ushort)c.V0);
        c.V0 = 0u | 0x01FDu;
        m.WriteU16((c.A2 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0020u;
        m.WriteU16((c.A2 + 0x20u), (ushort)(short)(-margin));
        m.WriteU16((c.A2 + 0x8u), (ushort)(short)(-margin));
        m.WriteU16((c.A2 + 0x16u), (ushort)0u);
        m.WriteU16((c.A2 + 0xAu), (ushort)0u);
        m.WriteU16((c.A2 + 0x32u), (ushort)0u);
        m.WriteU32((c.A2 + 0x10u), c.V1);
        m.WriteU32((c.A2 + 0x1Cu), c.A0);
        m.WriteU32((c.A2 + 0x28u), c.A1);
        m.WriteU16((c.S0 + 0x88u), (ushort)c.V0);
        L801AB20C: ;
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0x908u));
        if (c.V0 == 0u) {
            c.V0 = 0u | 0x00F7u;
            goto L801AB5D0;
        }
        c.V0 = 0u | 0x00F7u;
        c.A2 = m.ReadU32((c.S0 + 0x7Cu));
        m.WriteU8((c.A2 + 0x6u), (byte)c.V0);
        m.WriteU8((c.A2 + 0x5u), (byte)c.V0);
        m.WriteU8((c.A2 + 0x4u), (byte)c.V0);
        c.V1 = m.ReadU32((c.A2 + 0x4u));
        c.A0 = m.ReadU32((c.A2 + 0x4u));
        c.A1 = m.ReadU32((c.A2 + 0x4u));
        c.V0 = 0u | 0x0051u;
        m.WriteU16((c.A2 + 0x32u), (ushort)c.V0);
        m.WriteU32((c.A2 + 0x10u), c.V1);
        m.WriteU32((c.A2 + 0x1Cu), c.A0);
        m.WriteU32((c.A2 + 0x28u), c.A1);
        c.V0 = m.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        goto L801AB5D0;
        L801AB268: ;
        c.A2 = m.ReadU32((c.S0 + 0x7Cu));
        c.A2 = m.ReadU32(c.A2);
        m.WriteU8((c.A2 + 0x6u), (byte)0u);
        m.WriteU8((c.A2 + 0x5u), (byte)0u);
        m.WriteU8((c.A2 + 0x4u), (byte)0u);
        c.V1 = m.ReadU32((c.A2 + 0x4u));
        c.A0 = m.ReadU32((c.A2 + 0x4u));
        c.A1 = m.ReadU32((c.A2 + 0x4u));
        c.V0 = 0u | 0x00C0u;
        m.WriteU16((c.A2 + 0x26u), (ushort)c.V0);
        c.V0 = (uint)(0x100 + margin);
        m.WriteU16((c.A2 + 0x2Cu), (ushort)c.V0);
        m.WriteU16((c.A2 + 0x14u), (ushort)c.V0);
        c.V0 = 0u | 0x00F0u;
        m.WriteU16((c.A2 + 0x2Eu), (ushort)c.V0);
        m.WriteU16((c.A2 + 0x22u), (ushort)c.V0);
        c.V0 = 0u | 0x00D0u;
        m.WriteU16((c.A2 + 0x32u), (ushort)0u);
        m.WriteU16((c.A2 + 0x20u), (ushort)(short)(-margin));
        m.WriteU16((c.A2 + 0x8u), (ushort)(short)(-margin));
        m.WriteU16((c.A2 + 0x16u), (ushort)0u);
        m.WriteU16((c.A2 + 0xAu), (ushort)0u);
        m.WriteU32((c.A2 + 0x10u), c.V1);
        m.WriteU32((c.A2 + 0x1Cu), c.A0);
        m.WriteU32((c.A2 + 0x28u), c.A1);
        c.At = 0x80070000u;
        m.WriteU16((c.At + 0x33FCu), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        L801AB2EC: ;
        c.V0 = 0u | 0x00D0u;
        c.At = 0x80070000u;
        m.WriteU16((c.At + 0x33FCu), (ushort)c.V0);
        c.A2 = m.ReadU32((c.S0 + 0x7Cu));
        c.V0 = m.ReadU8((c.A2 + 0x4u));
        c.V0 = c.V0 + 0xF8u;
        m.WriteU8((c.A2 + 0x4u), (byte)c.V0);
        c.V1 = m.ReadU8((c.A2 + 0x4u));
        c.V0 = c.V1 < 0x000000F9u ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V0 = c.V1 + 0u;
            goto L801AB3AC;
        }
        c.V0 = c.V1 + 0u;
        c.V0 = 0u | 0x0008u;
        m.WriteU8((c.A2 + 0x4u), (byte)0u);
        m.WriteU16((c.A2 + 0x32u), (ushort)c.V0);
        c.A2 = m.ReadU32(c.A2);
        c.V0 = 0u | 0x0005u;
        m.WriteU16((c.A2 + 0x32u), (ushort)0u);
        m.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        goto L801AB5D0;
        L801AB344: ;
        c.V1 = 0x80070000u;
        c.V1 = c.V1 + 0x33FCu;
        c.V0 = 0u | 0x00D0u;
        m.WriteU16(c.V1, (ushort)c.V0);
        c.A2 = m.ReadU32((c.S0 + 0x7Cu));
        c.A2 = m.ReadU32(c.A2);
        c.V0 = m.ReadU8((c.A2 + 0x4u));
        c.V0 = c.V0 + 0xF8u;
        m.WriteU8((c.A2 + 0x4u), (byte)c.V0);
        c.V0 = m.ReadU8((c.A2 + 0x4u));
        c.V0 = c.V0 < 0x000000F9u ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0008u;
            goto L801AB3A8;
        }
        c.V0 = 0u | 0x0008u;
        m.WriteU8((c.A2 + 0x4u), (byte)0u);
        m.WriteU16((c.A2 + 0x32u), (ushort)c.V0);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU16((c.V0 + 0x7408u));
        m.WriteU16(c.V1, (ushort)c.V0);
        c.V0 = 0u | 0x0001u;
        m.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        L801AB3A8: ;
        c.V0 = m.ReadU8((c.A2 + 0x4u));
        L801AB3AC: ;
        m.WriteU8((c.A2 + 0x6u), (byte)c.V0);
        m.WriteU8((c.A2 + 0x5u), (byte)c.V0);
        c.V0 = m.ReadU32((c.A2 + 0x4u));
        c.V1 = m.ReadU32((c.A2 + 0x4u));
        c.A0 = m.ReadU32((c.A2 + 0x4u));
        m.WriteU32((c.A2 + 0x10u), c.V0);
        m.WriteU32((c.A2 + 0x1Cu), c.V1);
        m.WriteU32((c.A2 + 0x28u), c.A0);
        goto L801AB5D0;
        L801AB3D4: ;
        c.A2 = m.ReadU32((c.S0 + 0x7Cu));
        if (c.A2 == 0u) {
            goto L801AB444;
        }
        c.A3 = 0u | 0x00F0u;
        c.T1 = 0u | 0x00B0u;
        c.T0 = 0u | 0x0055u;
        c.A1 = 0u | 0x00FFu;
        L801AB3F4: ;
        m.WriteU8((c.A2 + 0x6u), (byte)c.A1);
        m.WriteU8((c.A2 + 0x5u), (byte)c.A1);
        m.WriteU8((c.A2 + 0x4u), (byte)c.A1);
        c.V0 = m.ReadU32((c.A2 + 0x4u));
        c.V1 = m.ReadU32((c.A2 + 0x4u));
        c.A0 = m.ReadU32((c.A2 + 0x4u));
        m.WriteU16((c.A2 + 0x16u), (ushort)0u);
        m.WriteU16((c.A2 + 0xAu), (ushort)0u);
        m.WriteU16((c.A2 + 0x2Eu), (ushort)c.A3);
        m.WriteU16((c.A2 + 0x22u), (ushort)c.A3);
        m.WriteU16((c.A2 + 0x26u), (ushort)c.T1);
        m.WriteU16((c.A2 + 0x32u), (ushort)c.T0);
        m.WriteU32((c.A2 + 0x10u), c.V0);
        m.WriteU32((c.A2 + 0x1Cu), c.V1);
        m.WriteU32((c.A2 + 0x28u), c.A0);
        c.A2 = m.ReadU32(c.A2);
        if (c.A2 != 0u) {
            goto L801AB3F4;
        }
        c.A2 = m.ReadU32((c.S0 + 0x7Cu));
        L801AB444: ;
        m.WriteU16((c.A2 + 0x20u), (ushort)(short)(-margin));
        m.WriteU16((c.A2 + 0x8u), (ushort)(short)(-margin));
        c.A2 = m.ReadU32(c.A2);
        c.A0 = 0u | 0x00FFu;
        m.WriteU8((c.A2 + 0x6u), (byte)c.A0);
        m.WriteU8((c.A2 + 0x5u), (byte)c.A0);
        m.WriteU8((c.A2 + 0x4u), (byte)c.A0);
        c.V0 = m.ReadU32((c.A2 + 0x4u));
        m.WriteU8((c.A2 + 0x12u), (byte)0u);
        m.WriteU8((c.A2 + 0x11u), (byte)0u);
        m.WriteU8((c.A2 + 0x10u), (byte)0u);
        c.V1 = m.ReadU32((c.A2 + 0x10u));
        m.WriteU32((c.A2 + 0x1Cu), c.V0);
        m.WriteU32((c.A2 + 0x28u), c.V1);
        c.A2 = m.ReadU32(c.A2);
        c.V0 = (uint)(0x100 + margin);
        m.WriteU16((c.A2 + 0x2Cu), (ushort)c.V0);
        m.WriteU16((c.A2 + 0x14u), (ushort)c.V0);
        c.A2 = m.ReadU32(c.A2);
        m.WriteU8((c.A2 + 0x6u), (byte)0u);
        m.WriteU8((c.A2 + 0x5u), (byte)0u);
        m.WriteU8((c.A2 + 0x4u), (byte)0u);
        c.V0 = m.ReadU32((c.A2 + 0x4u));
        m.WriteU8((c.A2 + 0x12u), (byte)c.A0);
        m.WriteU8((c.A2 + 0x11u), (byte)c.A0);
        m.WriteU8((c.A2 + 0x10u), (byte)c.A0);
        c.V1 = m.ReadU32((c.A2 + 0x10u));
        m.WriteU32((c.A2 + 0x1Cu), c.V0);
        m.WriteU32((c.A2 + 0x28u), c.V1);
        c.A0 = 0x80070000u;
        c.A0 = (uint)(short)m.ReadU16((c.A0 + 0x33DAu));
        c.V1 = m.ReadU16((c.S0 + 0x2Cu));
        c.V0 = 0u | 0x0020u;
        m.WriteU16((c.S0 + 0x88u), (ushort)c.V0);
        c.V1 = c.V1 + 0x1u;
        m.WriteU32((c.S0 + 0x84u), c.A0);
        m.WriteU32((c.S0 + 0x80u), c.A0);
        m.WriteU16((c.S0 + 0x2Cu), (ushort)c.V1);
        L801AB4E4: ;
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0x88u));
        if (c.V0 == 0u) {
            c.V1 = c.V0 + 0u;
            goto L801AB518;
        }
        c.V1 = c.V0 + 0u;
        c.V0 = c.V1 - 0x1u;
        m.WriteU16((c.S0 + 0x88u), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        if (c.V0 != 0u) {
            goto L801AB518;
        }
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU16((c.V0 + 0x7408u));
        c.At = 0x80070000u;
        m.WriteU16((c.At + 0x33FCu), (ushort)c.V0);
        L801AB518: ;
        c.A0 = m.ReadU32((c.S0 + 0x80u));
        c.A1 = m.ReadU32((c.S0 + 0x84u));
        c.A2 = m.ReadU32((c.S0 + 0x7Cu));
        c.A0 = c.A0 - 0x4u;
        c.A1 = c.A1 + 0x4u;
        c.V0 = c.A0 + 0u;
        m.WriteU16((c.A2 + 0x2Cu), (ushort)c.V0);
        m.WriteU16((c.A2 + 0x14u), (ushort)c.V0);
        c.A2 = m.ReadU32(c.A2);
        c.V1 = c.A0 + 0x40u;
        m.WriteU16((c.A2 + 0x20u), (ushort)c.V0);
        m.WriteU16((c.A2 + 0x8u), (ushort)c.V0);
        m.WriteU16((c.A2 + 0x2Cu), (ushort)c.V1);
        m.WriteU16((c.A2 + 0x14u), (ushort)c.V1);
        c.A2 = m.ReadU32(c.A2);
        c.V0 = c.A1 + 0u;
        m.WriteU16((c.A2 + 0x20u), (ushort)c.V0);
        m.WriteU16((c.A2 + 0x8u), (ushort)c.V0);
        c.A2 = m.ReadU32(c.A2);
        c.V1 = c.A1 - 0x40u;
        m.WriteU16((c.A2 + 0x2Cu), (ushort)c.V0);
        m.WriteU16((c.A2 + 0x14u), (ushort)c.V0);
        c.V0 = (int)c.A0 < -64 - margin ? 1u : 0u;
        m.WriteU16((c.A2 + 0x20u), (ushort)c.V1);
        if (c.V0 == 0u) {
            m.WriteU16((c.A2 + 0x8u), (ushort)c.V1);
            goto L801AB5C8;
        }
        m.WriteU16((c.A2 + 0x8u), (ushort)c.V1);
        c.V0 = (int)c.A1 < 321 + margin ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L801AB5C8;
        }
        c.A2 = m.ReadU32((c.S0 + 0x7Cu));
        if (c.A2 == 0u) {
            c.V0 = 0u | 0x0008u;
            goto L801AB5B0;
        }
        c.V0 = 0u | 0x0008u;
        L801AB59C: ;
        m.WriteU16((c.A2 + 0x32u), (ushort)c.V0);
        c.A2 = m.ReadU32(c.A2);
        if (c.A2 != 0u) {
            goto L801AB59C;
        }
        L801AB5B0: ;
        c.A0 = c.S0 + 0u;
        c.RA = 0x801AB5B8u;
        SoTN_st0.PreventEntityFromRespawning_st0(c, m);
        L801AB5B8: ;
        c.A0 = c.S0 + 0u;
        c.RA = 0x801AB5C0u;
        SoTN_st0.DestroyEntity_st0(c, m);
        goto L801AB5D0;
        L801AB5C8: ;
        m.WriteU32((c.S0 + 0x80u), c.A0);
        m.WriteU32((c.S0 + 0x84u), c.A1);
        L801AB5D0: ;
        c.RA = m.ReadU32((c.SP + 0x1Cu));
        c.S0 = m.ReadU32((c.SP + 0x18u));
        c.SP = c.SP + 0x20u;
        return;
    }
    //working bad, needs fix
    public static void TransparentWaterWide_no3(CpuContext c, IMemory m)
    {
        c.SP = c.SP - 0x18u;
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.S0 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x14u), c.RA);
        c.V1 = m.ReadU16((c.S0 + 0x2Cu));
        c.V0 = 0u | 0x0001u;
        if (c.V1 == c.V0) {
            c.V0 = (int)c.V1 < 2 ? 1u : 0u;
            goto L801B92A8;
        }
        c.V0 = (int)c.V1 < 2 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801B91D0;
        }
        if (c.V1 == 0u) {
            goto L801B91E4;
        }
        goto L801B93B8;
        L801B91D0: ;
        c.V0 = 0u | 0x0002u;
        if (c.V1 == c.V0) {
            c.V0 = 0u | 0x00FFu;
            goto L801B9330;
        }
        c.V0 = 0u | 0x00FFu;
        goto L801B93B8;
        L801B91E4: ;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0xADCu;
        c.RA = 0x801B91F4u;
        SoTN_no3.InitializeEntity_no3(c, m);
        c.A0 = 0u | 0x0004u;
        c.V0 = 0u | 0x0004u;
        m.WriteU16((c.S0 + 0x80u), (ushort)c.V0);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0010u;
        c.RA = 0x801B9214u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.A0 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.A0 != c.V0) {
            c.V0 = c.A0 << 1;
            goto L801B9238;
        }
        c.V0 = c.A0 << 1;
        c.A0 = c.S0 + 0u;
        c.RA = 0x801B9230u;
        SoTN_no3.DestroyEntity_no3(c, m);
        goto L801B94DC;
        L801B9238: ;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A2 = c.V0 + c.V1;
        c.V0 = m.ReadU32((c.S0 + 0x34u));
        c.V1 = 0x00800000u;
        m.WriteU32((c.S0 + 0x64u), c.A0);
        m.WriteU32((c.S0 + 0x7Cu), c.A2);
        c.V0 = c.V0 | c.V1;
        if (c.A2 == 0u) {
            m.WriteU32((c.S0 + 0x34u), c.V0);
            goto L801B93B8;
        }
        m.WriteU32((c.S0 + 0x34u), c.V0);
        c.A1 = 0u | 0x000Fu;
        c.A0 = 0u | 0x0018u;
        c.V1 = 0u | 0x00B0u;
        c.V0 = 0u | 0x0008u;
        m.WriteU16((c.A2 + 0x1Au), (ushort)c.A1);
        L801B9284: ;
        m.WriteU16((c.A2 + 0xEu), (ushort)c.A0);
        m.WriteU16((c.A2 + 0x26u), (ushort)c.V1);
        m.WriteU16((c.A2 + 0x32u), (ushort)c.V0);
        c.A2 = m.ReadU32(c.A2);
        if (c.A2 == 0u) {
            goto L801B93B8;
        }
        m.WriteU16((c.A2 + 0x1Au), (ushort)c.A1);
        goto L801B9284;
        L801B92A8: ;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x11F8u;
        c.V1 = m.ReadU8(c.A0);
        c.V0 = 0u | 0x00FFu;
        if (c.V1 == c.V0) {
            goto L801B9304;
        }
        c.A1 = 0x80040000u;
        c.A1 = c.A1 - 0x3EFCu;
        c.A3 = c.A1 + 0x400u;
        c.A2 = 0u | 0x00FFu;
        L801B92D0: ;
        c.V1 = m.ReadU8(c.A0);
        c.V0 = m.ReadU8((c.A0 + 0x2u));
        c.A0 = c.A0 + 0x4u;
        c.V1 = c.V1 << 1;
        c.V0 = c.V0 << 1;
        c.V0 = c.V0 + c.A3;
        c.V0 = m.ReadU16(c.V0);
        c.V1 = c.V1 + c.A1;
        m.WriteU16(c.V1, (ushort)c.V0);
        c.V0 = m.ReadU8(c.A0);
        if (c.V0 != c.A2) {
            goto L801B92D0;
        }
        L801B9304: ;
        c.V0 = m.ReadU16((c.S0 + 0x80u));
        c.V0 = c.V0 - 0x1u;
        m.WriteU16((c.S0 + 0x80u), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        if (c.V0 != 0u) {
            c.V1 = 0u | 0x0004u;
            goto L801B93B8;
        }
        c.V1 = 0u | 0x0004u;
        c.V0 = m.ReadU16((c.S0 + 0x2Cu));
        m.WriteU16((c.S0 + 0x80u), (ushort)c.V1);
        c.V0 = c.V0 + 0x1u;
        goto L801B93B4;
        L801B9330: ;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x11F8u;
        c.V1 = m.ReadU8(c.A0);
        if (c.V1 == c.V0) {
            goto L801B938C;
        }
        c.A1 = 0x80040000u;
        c.A1 = c.A1 - 0x3EFCu;
        c.A3 = c.A1 + 0x400u;
        c.A2 = 0u | 0x00FFu;
        L801B9358: ;
        c.V1 = m.ReadU8(c.A0);
        c.V0 = m.ReadU8((c.A0 + 0x3u));
        c.A0 = c.A0 + 0x4u;
        c.V1 = c.V1 << 1;
        c.V0 = c.V0 << 1;
        c.V0 = c.V0 + c.A3;
        c.V0 = m.ReadU16(c.V0);
        c.V1 = c.V1 + c.A1;
        m.WriteU16(c.V1, (ushort)c.V0);
        c.V0 = m.ReadU8(c.A0);
        if (c.V0 != c.A2) {
            goto L801B9358;
        }
        L801B938C: ;
        c.V0 = m.ReadU16((c.S0 + 0x80u));
        c.V0 = c.V0 - 0x1u;
        m.WriteU16((c.S0 + 0x80u), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        if (c.V0 != 0u) {
            c.V1 = 0u | 0x0004u;
            goto L801B93B8;
        }
        c.V1 = 0u | 0x0004u;
        c.V0 = m.ReadU16((c.S0 + 0x2Cu));
        m.WriteU16((c.S0 + 0x80u), (ushort)c.V1);
        c.V0 = c.V0 - 0x1u;
        L801B93B4: ;
        m.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        L801B93B8: ;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x1224u;
        c.A1 = c.S0 + 0u;
        c.RA = 0x801B93C8u;
        Dispatcher.Call(c, m, 0x801C4D94u);
        c.V0 = 0x6BCA0000u;
        c.A0 = 0x80070000u;
        c.A0 = (uint)(short)m.ReadU16((c.A0 + 0x308Eu));
        c.V0 = c.V0 | 0x1AF3u;
        c.A0 = 0u - c.A0;
        { var _r = (long)(int)c.A0 * (int)c.V0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V1 = (uint)((int)c.A0 >> 31);
        c.V0 = c.HI;
        c.V0 = (uint)((int)c.V0 >> 4);
        c.A3 = c.V0 - c.V1;
        c.V0 = c.A3 << 2;
        c.V0 = c.V0 + c.A3;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 - c.A3;
        c.V0 = c.V0 << 1;
        c.A3 = c.A0 - c.V0;
        c.V0 = m.ReadU16((c.S0 + 0x30u));
        if (c.V0 == 0u) {
            c.A3 = c.A3 + 0x130u;
            while ((int)c.A3 < 256 + StageMargin()) c.A3 = c.A3 + 0x26u;
            goto L801B941C;
        }
        c.A3 = c.A3 + 0x130u;
        c.A3 = 0u | 0x0060u;
        L801B941C: ;
        c.V1 = 0x80180000u;
        c.V1 = c.V1 + 0x1218u;
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0x56u));
        c.A2 = m.ReadU32((c.S0 + 0x7Cu));
        c.A0 = (uint)(short)m.ReadU16((c.S0 + 0x6u));
        c.V0 = c.V0 << 2;
        c.V1 = c.V0 + c.V1;
        c.V0 = m.ReadU8(c.V1);
        c.V1 = m.ReadU8((c.V1 + 0x1u));
        if ((int)c.A3 <= -StageMargin()) {
            goto L801B94C0;
        }
        c.T3 = c.V0 + 0u;
        c.T2 = c.V0 + 0x26u;
        c.A1 = c.V1 + 0u;
        c.T1 = c.V1 + 0x3Eu;
        c.T0 = c.A0 + 0u;
        c.V1 = c.A0 + 0x3Eu;
        c.V0 = c.A3 + 0u;
        L801B9464: ;
        c.A3 = c.A3 - 0x26u;
        m.WriteU16((c.A2 + 0x2Cu), (ushort)c.V0);
        m.WriteU16((c.A2 + 0x14u), (ushort)c.V0);
        c.V0 = c.A3 + 0u;
        m.WriteU16((c.A2 + 0x20u), (ushort)c.V0);
        m.WriteU16((c.A2 + 0x8u), (ushort)c.V0);
        c.V0 = 0u | 0x0033u;
        m.WriteU8((c.A2 + 0x24u), (byte)c.T3);
        m.WriteU8((c.A2 + 0xCu), (byte)c.T3);
        m.WriteU8((c.A2 + 0x30u), (byte)c.T2);
        m.WriteU8((c.A2 + 0x18u), (byte)c.T2);
        m.WriteU8((c.A2 + 0x19u), (byte)c.A1);
        m.WriteU8((c.A2 + 0xDu), (byte)c.A1);
        m.WriteU8((c.A2 + 0x31u), (byte)c.T1);
        m.WriteU8((c.A2 + 0x25u), (byte)c.T1);
        m.WriteU16((c.A2 + 0x16u), (ushort)c.T0);
        m.WriteU16((c.A2 + 0xAu), (ushort)c.T0);
        m.WriteU16((c.A2 + 0x2Eu), (ushort)c.V1);
        m.WriteU16((c.A2 + 0x22u), (ushort)c.V1);
        m.WriteU16((c.A2 + 0x32u), (ushort)c.V0);
        c.A2 = m.ReadU32(c.A2);
        if ((int)c.A3 > -StageMargin() && c.A2 != 0u) {
            c.V0 = c.A3 + 0u;
            goto L801B9464;
        }
        c.V0 = c.A3 + 0u;
        L801B94C0: ;
        if (c.A2 == 0u) {
            c.V0 = 0u | 0x0008u;
            goto L801B94DC;
        }
        c.V0 = 0u | 0x0008u;
        L801B94C8: ;
        m.WriteU16((c.A2 + 0x32u), (ushort)c.V0);
        c.A2 = m.ReadU32(c.A2);
        if (c.A2 != 0u) {
            goto L801B94C8;
        }
        L801B94DC: ;
        c.RA = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x18u;
        return;
    }
    public static void TransparentWaterWide_np3(CpuContext c, IMemory m)
    {
        c.SP = c.SP - 0x18u;
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.S0 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x14u), c.RA);
        c.V1 = m.ReadU16((c.S0 + 0x2Cu));
        c.V0 = 0u | 0x0001u;
        if (c.V1 == c.V0) {
            c.V0 = (int)c.V1 < 2 ? 1u : 0u;
            goto L801B3ADC;
        }
        c.V0 = (int)c.V1 < 2 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801B3A04;
        }
        if (c.V1 == 0u) {
            goto L801B3A18;
        }
        goto L801B3BEC;
        L801B3A04: ;
        c.V0 = 0u | 0x0002u;
        if (c.V1 == c.V0) {
            c.V0 = 0u | 0x00FFu;
            goto L801B3B64;
        }
        c.V0 = 0u | 0x00FFu;
        goto L801B3BEC;
        L801B3A18: ;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0xA6Cu;
        c.RA = 0x801B3A28u;
        SoTN_np3.InitializeEntity_np3(c, m);
        c.A0 = 0u | 0x0004u;
        c.V0 = 0u | 0x0004u;
        m.WriteU16((c.S0 + 0x80u), (ushort)c.V0);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0010u;
        c.RA = 0x801B3A48u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.A0 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.A0 != c.V0) {
            c.V0 = c.A0 << 1;
            goto L801B3A6C;
        }
        c.V0 = c.A0 << 1;
        c.A0 = c.S0 + 0u;
        c.RA = 0x801B3A64u;
        SoTN_np3.DestroyEntity_np3(c, m);
        goto L801B3D10;
        L801B3A6C: ;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A2 = c.V0 + c.V1;
        c.V0 = m.ReadU32((c.S0 + 0x34u));
        c.V1 = 0x00800000u;
        m.WriteU32((c.S0 + 0x64u), c.A0);
        m.WriteU32((c.S0 + 0x7Cu), c.A2);
        c.V0 = c.V0 | c.V1;
        if (c.A2 == 0u) {
            m.WriteU32((c.S0 + 0x34u), c.V0);
            goto L801B3BEC;
        }
        m.WriteU32((c.S0 + 0x34u), c.V0);
        c.A1 = 0u | 0x000Fu;
        c.A0 = 0u | 0x0018u;
        c.V1 = 0u | 0x00B0u;
        c.V0 = 0u | 0x0008u;
        m.WriteU16((c.A2 + 0x1Au), (ushort)c.A1);
        L801B3AB8: ;
        m.WriteU16((c.A2 + 0xEu), (ushort)c.A0);
        m.WriteU16((c.A2 + 0x26u), (ushort)c.V1);
        m.WriteU16((c.A2 + 0x32u), (ushort)c.V0);
        c.A2 = m.ReadU32(c.A2);
        if (c.A2 == 0u) {
            goto L801B3BEC;
        }
        m.WriteU16((c.A2 + 0x1Au), (ushort)c.A1);
        goto L801B3AB8;
        L801B3ADC: ;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x10C0u;
        c.V1 = m.ReadU8(c.A0);
        c.V0 = 0u | 0x00FFu;
        if (c.V1 == c.V0) {
            goto L801B3B38;
        }
        c.A1 = 0x80040000u;
        c.A1 = c.A1 - 0x3EFCu;
        c.A3 = c.A1 + 0x400u;
        c.A2 = 0u | 0x00FFu;
        L801B3B04: ;
        c.V1 = m.ReadU8(c.A0);
        c.V0 = m.ReadU8((c.A0 + 0x2u));
        c.A0 = c.A0 + 0x4u;
        c.V1 = c.V1 << 1;
        c.V0 = c.V0 << 1;
        c.V0 = c.V0 + c.A3;
        c.V0 = m.ReadU16(c.V0);
        c.V1 = c.V1 + c.A1;
        m.WriteU16(c.V1, (ushort)c.V0);
        c.V0 = m.ReadU8(c.A0);
        if (c.V0 != c.A2) {
            goto L801B3B04;
        }
        L801B3B38: ;
        c.V0 = m.ReadU16((c.S0 + 0x80u));
        c.V0 = c.V0 - 0x1u;
        m.WriteU16((c.S0 + 0x80u), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        if (c.V0 != 0u) {
            c.V1 = 0u | 0x0004u;
            goto L801B3BEC;
        }
        c.V1 = 0u | 0x0004u;
        c.V0 = m.ReadU16((c.S0 + 0x2Cu));
        m.WriteU16((c.S0 + 0x80u), (ushort)c.V1);
        c.V0 = c.V0 + 0x1u;
        goto L801B3BE8;
        L801B3B64: ;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x10C0u;
        c.V1 = m.ReadU8(c.A0);
        if (c.V1 == c.V0) {
            goto L801B3BC0;
        }
        c.A1 = 0x80040000u;
        c.A1 = c.A1 - 0x3EFCu;
        c.A3 = c.A1 + 0x400u;
        c.A2 = 0u | 0x00FFu;
        L801B3B8C: ;
        c.V1 = m.ReadU8(c.A0);
        c.V0 = m.ReadU8((c.A0 + 0x3u));
        c.A0 = c.A0 + 0x4u;
        c.V1 = c.V1 << 1;
        c.V0 = c.V0 << 1;
        c.V0 = c.V0 + c.A3;
        c.V0 = m.ReadU16(c.V0);
        c.V1 = c.V1 + c.A1;
        m.WriteU16(c.V1, (ushort)c.V0);
        c.V0 = m.ReadU8(c.A0);
        if (c.V0 != c.A2) {
            goto L801B3B8C;
        }
        L801B3BC0: ;
        c.V0 = m.ReadU16((c.S0 + 0x80u));
        c.V0 = c.V0 - 0x1u;
        m.WriteU16((c.S0 + 0x80u), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        if (c.V0 != 0u) {
            c.V1 = 0u | 0x0004u;
            goto L801B3BEC;
        }
        c.V1 = 0u | 0x0004u;
        c.V0 = m.ReadU16((c.S0 + 0x2Cu));
        m.WriteU16((c.S0 + 0x80u), (ushort)c.V1);
        c.V0 = c.V0 - 0x1u;
        L801B3BE8: ;
        m.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        L801B3BEC: ;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x10ECu;
        c.A1 = c.S0 + 0u;
        c.RA = 0x801B3BFCu;
        SoTN_np3.AnimateEntity_np3(c, m);
        c.V0 = 0x6BCA0000u;
        c.A0 = 0x80070000u;
        c.A0 = (uint)(short)m.ReadU16((c.A0 + 0x308Eu));
        c.V0 = c.V0 | 0x1AF3u;
        c.A0 = 0u - c.A0;
        { var _r = (long)(int)c.A0 * (int)c.V0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V1 = (uint)((int)c.A0 >> 31);
        c.V0 = c.HI;
        c.V0 = (uint)((int)c.V0 >> 4);
        c.A3 = c.V0 - c.V1;
        c.V0 = c.A3 << 2;
        c.V0 = c.V0 + c.A3;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 - c.A3;
        c.V0 = c.V0 << 1;
        c.A3 = c.A0 - c.V0;
        c.V0 = m.ReadU16((c.S0 + 0x30u));
        if (c.V0 == 0u) {
            c.A3 = c.A3 + 0x130u;
            while ((int)c.A3 < 256 + StageMargin()) c.A3 = c.A3 + 0x26u;
            goto L801B3C50;
        }
        c.A3 = c.A3 + 0x130u;
        c.A3 = 0u | 0x0060u;
        L801B3C50: ;
        c.V1 = 0x80180000u;
        c.V1 = c.V1 + 0x10E0u;
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0x56u));
        c.A2 = m.ReadU32((c.S0 + 0x7Cu));
        c.A0 = (uint)(short)m.ReadU16((c.S0 + 0x6u));
        c.V0 = c.V0 << 2;
        c.V1 = c.V0 + c.V1;
        c.V0 = m.ReadU8(c.V1);
        c.V1 = m.ReadU8((c.V1 + 0x1u));
        if ((int)c.A3 <= -StageMargin()) {
            goto L801B3CF4;
        }
        c.T3 = c.V0 + 0u;
        c.T2 = c.V0 + 0x26u;
        c.A1 = c.V1 + 0u;
        c.T1 = c.V1 + 0x3Eu;
        c.T0 = c.A0 + 0u;
        c.V1 = c.A0 + 0x3Eu;
        c.V0 = c.A3 + 0u;
        L801B3C98: ;
        c.A3 = c.A3 - 0x26u;
        m.WriteU16((c.A2 + 0x2Cu), (ushort)c.V0);
        m.WriteU16((c.A2 + 0x14u), (ushort)c.V0);
        c.V0 = c.A3 + 0u;
        m.WriteU16((c.A2 + 0x20u), (ushort)c.V0);
        m.WriteU16((c.A2 + 0x8u), (ushort)c.V0);
        c.V0 = 0u | 0x0033u;
        m.WriteU8((c.A2 + 0x24u), (byte)c.T3);
        m.WriteU8((c.A2 + 0xCu), (byte)c.T3);
        m.WriteU8((c.A2 + 0x30u), (byte)c.T2);
        m.WriteU8((c.A2 + 0x18u), (byte)c.T2);
        m.WriteU8((c.A2 + 0x19u), (byte)c.A1);
        m.WriteU8((c.A2 + 0xDu), (byte)c.A1);
        m.WriteU8((c.A2 + 0x31u), (byte)c.T1);
        m.WriteU8((c.A2 + 0x25u), (byte)c.T1);
        m.WriteU16((c.A2 + 0x16u), (ushort)c.T0);
        m.WriteU16((c.A2 + 0xAu), (ushort)c.T0);
        m.WriteU16((c.A2 + 0x2Eu), (ushort)c.V1);
        m.WriteU16((c.A2 + 0x22u), (ushort)c.V1);
        m.WriteU16((c.A2 + 0x32u), (ushort)c.V0);
        c.A2 = m.ReadU32(c.A2);
        if ((int)c.A3 > -StageMargin() && c.A2 != 0u) {
            c.V0 = c.A3 + 0u;
            goto L801B3C98;
        }
        c.V0 = c.A3 + 0u;
        L801B3CF4: ;
        if (c.A2 == 0u) {
            c.V0 = 0u | 0x0008u;
            goto L801B3D10;
        }
        c.V0 = 0u | 0x0008u;
        L801B3CFC: ;
        m.WriteU16((c.A2 + 0x32u), (ushort)c.V0);
        c.A2 = m.ReadU32(c.A2);
        if (c.A2 != 0u) {
            goto L801B3CFC;
        }
        L801B3D10: ;
        c.RA = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x18u;
        return;
    }

    public static void WaterSurface801C12B0Wide_no4(CpuContext c, IMemory m)
    {
        int adj = System.Math.Max(0, StageMargin() - 0x10);
        c.SP = c.SP - 0x20u;
        m.WriteU32((c.SP + 0x14u), c.S1);
        c.S1 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x1Cu), c.RA);
        m.WriteU32((c.SP + 0x18u), c.S2);
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.V0 = m.ReadU16((c.S1 + 0x2Cu));
        if (c.V0 != 0u) {
            goto L801C139C;
        }
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0xBBCu;
        c.RA = 0x801C12E8u;
        SoTN_no4.InitializeEntity_no4(c, m);
        c.A0 = 0u | 0x0004u;
        c.V0 = 0u | 0x0004u;
        m.WriteU16((c.S1 + 0x54u), (ushort)0u);
        m.WriteU16((c.S1 + 0x80u), (ushort)c.V0);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0010u;
        c.RA = 0x801C130Cu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.A0 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.A0 != c.V0) {
            c.V0 = c.A0 << 1;
            goto L801C1330;
        }
        c.V0 = c.A0 << 1;
        c.A0 = c.S1 + 0u;
        c.RA = 0x801C1328u;
        SoTN_no4.DestroyEntity_no4(c, m);
        goto L801C15DC;
        L801C1330: ;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.S0 = c.V0 + c.V1;
        c.V0 = m.ReadU32((c.S1 + 0x34u));
        c.V1 = 0x00800000u;
        m.WriteU32((c.S1 + 0x64u), c.A0);
        m.WriteU32((c.S1 + 0x7Cu), c.S0);
        m.WriteU16((c.S1 + 0x82u), (ushort)0u);
        c.V0 = c.V0 | c.V1;
        if (c.S0 == 0u) {
            m.WriteU32((c.S1 + 0x34u), c.V0);
            goto L801C139C;
        }
        m.WriteU32((c.S1 + 0x34u), c.V0);
        c.A1 = 0u | 0x000Fu;
        c.A0 = 0u | 0x005Eu;
        c.V1 = 0u | 0x009Eu;
        c.V0 = 0u | 0x0008u;
        L801C137C: ;
        m.WriteU16((c.S0 + 0x1Au), (ushort)c.A1);
        m.WriteU16((c.S0 + 0xEu), (ushort)c.A0);
        m.WriteU16((c.S0 + 0x26u), (ushort)c.V1);
        m.WriteU16((c.S0 + 0x32u), (ushort)c.V0);
        c.S0 = m.ReadU32(c.S0);
        if (c.S0 != 0u) {
            goto L801C137C;
        }
        L801C139C: ;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x1144u;
        c.S0 = m.ReadU32((c.S1 + 0x7Cu));
        c.A1 = c.S1 + 0u;
        c.RA = 0x801C13B0u;
        SoTN_no4.AnimateEntity_no4(c, m);
        c.V0 = m.ReadU16((c.S1 + 0x30u));
        c.V1 = m.ReadU8((c.S1 + 0x30u));
        c.A0 = 0x80070000u;
        c.A0 = (uint)(short)m.ReadU16((c.A0 + 0x308Eu));
        c.T7 = 0x80070000u;
        c.T7 = (uint)(short)m.ReadU16((c.T7 + 0x3092u));
        c.T4 = c.V0 >> 8;
        c.V0 = c.V1 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 1;
        c.V1 = 0x80180000u;
        c.V1 = c.V1 + 0x1150u;
        c.A2 = c.V0 + c.V1;
        c.T3 = c.A0 - 0x10u;
        c.V0 = (uint)(short)m.ReadU16((c.S1 + 0x56u));
        c.V1 = 0x80180000u;
        c.V1 = c.V1 + 0x112Cu;
        c.V0 = c.V0 << 2;
        c.T2 = c.V0 + c.V1;
        c.V0 = m.ReadU8((c.T2 + 0x1u));
        if ((int)c.T4 <= 0) {
            c.T8 = (uint)((int)c.A0 + 0x110 + adj);
            goto L801C15C0;
        }
        c.T8 = (uint)((int)c.A0 + 0x110 + adj);
        c.T9 = 0x82080000u;
        c.T9 = c.T9 | 0x2083u;
        c.S2 = 0u | 0x007Eu;
        c.T6 = c.V0 + 0u;
        c.T5 = c.V0 + 0x1u;
        L801C141C: ;
        c.A1 = (uint)(short)m.ReadU16(c.A2);
        c.A2 = c.A2 + 0x2u;
        c.V0 = (uint)(short)m.ReadU16(c.A2);
        c.A3 = c.A1 + c.V0;
        c.V0 = (int)c.T3 < (int)c.A3 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.A2 = c.A2 + 0x2u;
            goto L801C1448;
        }
        c.A2 = c.A2 + 0x2u;
        c.V0 = (int)c.T8 < (int)c.A1 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = c.T7 - 0x4u;
            goto L801C1450;
        }
        c.V0 = c.T7 - 0x4u;
        L801C1448: ;
        c.A2 = c.A2 + 0x6u;
        goto L801C15B4;
        L801C1450: ;
        c.T0 = (uint)(short)m.ReadU16(c.A2);
        c.V0 = (int)c.T0 < (int)c.V0 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.A2 = c.A2 + 0x2u;
            goto L801C1474;
        }
        c.A2 = c.A2 + 0x2u;
        c.V0 = c.T7 + 0xE0u;
        c.V0 = (int)c.V0 < (int)c.T0 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = (int)c.A1 < ((int)c.T3 - adj) ? 1u : 0u;
            goto L801C147C;
        }
        c.V0 = (int)c.A1 < ((int)c.T3 - adj) ? 1u : 0u;
        L801C1474: ;
        c.A2 = c.A2 + 0x4u;
        goto L801C15B4;
        L801C147C: ;
        c.A2 = c.A2 + 0x2u;
        c.A0 = (uint)(short)m.ReadU16(c.A2);
        c.A2 = c.A2 + 0x2u;
        if (c.V0 == 0u) {
            c.T0 = c.T0 - c.T7;
            goto L801C1494;
        }
        c.T0 = c.T0 - c.T7;
        c.A1 = (uint)((int)c.T3 - adj);
        L801C1494: ;
        c.V0 = (int)c.T8 < (int)c.A3 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801C14A4;
        }
        c.A3 = c.T8 + 0u;
        L801C14A4: ;
        if (c.A0 == 0u) {
            c.V0 = c.A1 >> 1;
            goto L801C14EC;
        }
        c.V0 = c.A1 >> 1;
        c.V1 = m.ReadU16((c.S1 + 0x82u));
        c.V1 = c.V1 + c.A0;
        c.A0 = c.V1 << 16;
        c.A0 = (uint)((int)c.A0 >> 16);
        c.A0 = c.A1 - c.A0;
        c.V0 = c.A0 >> 1;
        { var _r = (ulong)c.V0 * c.T9; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        m.WriteU16((c.S1 + 0x82u), (ushort)c.V1);
        c.V0 = c.HI;
        c.V1 = c.V0 >> 5;
        c.V0 = c.V1 << 6;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 << 1;
        c.V1 = c.A0 - c.V0;
        goto L801C1508;
        L801C14EC: ;
        { var _r = (ulong)c.V0 * c.T9; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V0 = c.HI;
        c.V1 = c.V0 >> 5;
        c.V0 = c.V1 << 6;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 << 1;
        c.V1 = c.A1 - c.V0;
        L801C1508: ;
        c.A3 = c.A3 - c.A1;
        c.V0 = c.A1 - 0x10u;
        c.A1 = c.V0 - c.T3;
        c.T1 = c.T0 + 0u;
        c.V0 = m.ReadU8(c.T2);
        c.T0 = c.T0 + 0x1u;
        c.V1 = c.V1 + c.V0;
        c.V0 = c.V1 + 0u;
        L801C1528: ;
        m.WriteU8((c.S0 + 0x24u), (byte)c.V0);
        m.WriteU8((c.S0 + 0xCu), (byte)c.V0);
        c.V0 = m.ReadU8(c.T2);
        c.V0 = c.V1 - c.V0;
        c.A0 = c.S2 - c.V0;
        c.V0 = (int)c.A3 < (int)c.A0 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = c.V1 + c.A0;
            goto L801C1554;
        }
        c.V0 = c.V1 + c.A0;
        c.A0 = c.A3 + 0u;
        c.V0 = c.V1 + c.A0;
        L801C1554: ;
        c.V1 = c.A1 + 0u;
        c.A1 = c.A1 + c.A0;
        m.WriteU8((c.S0 + 0x30u), (byte)c.V0);
        m.WriteU8((c.S0 + 0x18u), (byte)c.V0);
        c.V0 = c.A1 + 0u;
        c.A3 = c.A3 - c.A0;
        m.WriteU8((c.S0 + 0x19u), (byte)c.T6);
        m.WriteU8((c.S0 + 0xDu), (byte)c.T6);
        m.WriteU8((c.S0 + 0x31u), (byte)c.T5);
        m.WriteU8((c.S0 + 0x25u), (byte)c.T5);
        m.WriteU16((c.S0 + 0x20u), (ushort)c.V1);
        m.WriteU16((c.S0 + 0x8u), (ushort)c.V1);
        m.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        m.WriteU16((c.S0 + 0x14u), (ushort)c.V0);
        c.V1 = m.ReadU8(c.T2);
        c.V0 = 0u | 0x0013u;
        m.WriteU16((c.S0 + 0x16u), (ushort)c.T1);
        m.WriteU16((c.S0 + 0xAu), (ushort)c.T1);
        m.WriteU16((c.S0 + 0x2Eu), (ushort)c.T0);
        m.WriteU16((c.S0 + 0x22u), (ushort)c.T0);
        m.WriteU16((c.S0 + 0x32u), (ushort)c.V0);
        c.S0 = m.ReadU32(c.S0);
        if (c.A3 != 0u) {
            c.V0 = c.V1 + 0u;
            goto L801C1528;
        }
        c.V0 = c.V1 + 0u;
        L801C15B4: ;
        c.T4 = c.T4 - 0x1u;
        if ((int)c.T4 > 0) {
            goto L801C141C;
        }
        L801C15C0: ;
        if (c.S0 == 0u) {
            c.V0 = 0u | 0x0008u;
            goto L801C15DC;
        }
        c.V0 = 0u | 0x0008u;
        L801C15C8: ;
        m.WriteU16((c.S0 + 0x32u), (ushort)c.V0);
        c.S0 = m.ReadU32(c.S0);
        if (c.S0 != 0u) {
            goto L801C15C8;
        }
        L801C15DC: ;
        c.RA = m.ReadU32((c.SP + 0x1Cu));
        c.S2 = m.ReadU32((c.SP + 0x18u));
        c.S1 = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x20u;
        return;
    }

    public static void LavaGlowWide_cat(CpuContext c, IMemory m)
    {
        int margin = StageMargin();
        c.SP = c.SP - 0x18u;
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.S0 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x14u), c.RA);
        c.V1 = m.ReadU16((c.S0 + 0x2Cu));
        c.V0 = 0u | 0x0001u;
        if (c.V1 == c.V0) {
            c.V0 = (int)c.V1 < 2 ? 1u : 0u;
            goto L801CA188;
        }
        c.V0 = (int)c.V1 < 2 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801CA290;
        }
        if (c.V1 != 0u) {
            goto L801CA290;
        }
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x114Cu;
        c.RA = 0x801CA17Cu;
        SoTN_cat.InitializeEntity_cat(c, m);
        m.WriteU16((c.S0 + 0x54u), (ushort)0u);
        m.WriteU16((c.S0 + 0x56u), (ushort)0u);
        goto L801CA290;
        L801CA188: ;
        c.A0 = 0u | 0x0003u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3820u));
        c.A1 = 0u | 0x0001u;
        c.RA = 0x801CA1A0u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.A1 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.A1 == c.V0) {
            c.V0 = c.A1 << 1;
            goto L801CA270;
        }
        c.V0 = c.A1 << 1;
        c.V0 = c.V0 + c.A1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.A1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A0 = c.V0 + c.V1;
        c.V0 = m.ReadU32((c.S0 + 0x34u));
        c.V1 = 0x00800000u;
        m.WriteU32((c.S0 + 0x64u), c.A1);
        m.WriteU32((c.S0 + 0x7Cu), c.A0);
        c.V0 = c.V0 | c.V1;
        if (c.A0 == 0u) {
            m.WriteU32((c.S0 + 0x34u), c.V0);
            goto L801CA280;
        }
        m.WriteU32((c.S0 + 0x34u), c.V0);
        c.A2 = 0u | 0x0090u;
        c.A1 = 0u | 0x0100u;
        c.T3 = 0u | 0x00E0u;
        c.T2 = 0u | 0x0030u;
        c.T1 = 0u | 0x0050u;
        c.T0 = 0u | 0x00D0u;
        c.A3 = 0u | 0x0435u;
        m.WriteU8((c.A0 + 0x4u), (byte)0u);
        L801CA20C: ;
        m.WriteU8((c.A0 + 0x6u), (byte)0u);
        m.WriteU8((c.A0 + 0x5u), (byte)0u);
        c.V0 = m.ReadU32((c.A0 + 0x4u));
        m.WriteU8((c.A0 + 0x1Cu), (byte)c.T3);
        m.WriteU8((c.A0 + 0x1Eu), (byte)c.T2);
        m.WriteU8((c.A0 + 0x1Du), (byte)c.T1);
        c.V1 = m.ReadU32((c.A0 + 0x1Cu));
        m.WriteU16((c.A0 + 0x8u), (ushort)(short)(-margin));
        m.WriteU16((c.A0 + 0xAu), (ushort)c.A2);
        m.WriteU16((c.A0 + 0x14u), (ushort)(0x100 + margin));
        m.WriteU16((c.A0 + 0x16u), (ushort)c.A2);
        m.WriteU16((c.A0 + 0x20u), (ushort)(short)(-margin));
        m.WriteU16((c.A0 + 0x22u), (ushort)c.A1);
        m.WriteU16((c.A0 + 0x2Cu), (ushort)(0x100 + margin));
        m.WriteU16((c.A0 + 0x2Eu), (ushort)c.A1);
        m.WriteU16((c.A0 + 0x26u), (ushort)c.T0);
        m.WriteU16((c.A0 + 0x32u), (ushort)c.A3);
        m.WriteU32((c.A0 + 0x10u), c.V0);
        m.WriteU32((c.A0 + 0x28u), c.V1);
        c.A0 = m.ReadU32(c.A0);
        if (c.A0 == 0u) {
            goto L801CA280;
        }
        m.WriteU8((c.A0 + 0x4u), (byte)0u);
        goto L801CA20C;
        L801CA270: ;
        c.A0 = c.S0 + 0u;
        c.RA = 0x801CA278u;
        SoTN_cat.DestroyEntity_cat(c, m);
        goto L801CA290;
        L801CA280: ;
        c.V0 = m.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        L801CA290: ;
        c.RA = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x18u;
        return;
    }
    const uint BackgroundPacketBase = 0x80300000;
    const int BackgroundPacketCapacity = 2048;
    const uint BackgroundPacketStride = 64;

    readonly record struct BackgroundVertex(int X, int Y, ushort Uv, uint Color);
    readonly record struct BackgroundQuad(BackgroundVertex A, BackgroundVertex B,
        BackgroundVertex C, BackgroundVertex D, ushort Page, ushort Palette, ushort Order,
        ushort Mode, bool Textured)
    {
        public int Left => Math.Min(Math.Min(A.X, B.X), Math.Min(C.X, D.X));
        public int Right => Math.Max(Math.Max(A.X, B.X), Math.Max(C.X, D.X));
        public BackgroundQuad Shift(int top, int bottom) => this with
        {
            A = A with { X = A.X + top }, B = B with { X = B.X + top },
            C = C with { X = C.X + bottom }, D = D with { X = D.X + bottom }
        };
    }

    static bool BackgroundPrimitive(IMemory m, uint p) => p >= PrimBufAddr &&
        p < PrimBufAddr + 0x500 * PrimStride && (p - PrimBufAddr) % PrimStride == 0;

    static BackgroundQuad ReadBackgroundQuad(IMemory m, uint p)
    {
        BackgroundVertex Vertex(uint offset) => new((short)m.ReadU16(p + offset + 4),
            (short)m.ReadU16(p + offset + 6), m.ReadU16(p + offset + 8),
            m.ReadU32(p + offset) & 0xFFFFFF);
        return new(Vertex(4), Vertex(16), Vertex(28), Vertex(40), m.ReadU16(p + 26),
            m.ReadU16(p + 14), m.ReadU16(p + 38), m.ReadU16(p + 50),
            (m.ReadU8(p + 7) & 15) == 4);
    }

    static void EmitBackground(IMemory m, BackgroundQuad q, ref int count)
    {
        if (count >= BackgroundPacketCapacity || q.Order >= OtSize || (q.Mode & 8) != 0 ||
            q.Right <= ViewLeft || q.Left >= ViewRight) return;
        uint buffer = m.ReadU32(CurrentBufferPtr);
        if (buffer != Buf(0) && buffer != Buf(1)) return;
        uint p = BackgroundPacketBase + (buffer == Buf(1)
            ? BackgroundPacketCapacity * BackgroundPacketStride : 0) + (uint)count++ * BackgroundPacketStride;
        uint page = (uint)((q.Textured ? q.Page : 0) + (q.Mode & 0x60));
        m.WriteU32(p + 4, 0xE1000000 | page | ((q.Mode & 0x400) != 0 ? 0x200u : 0));
        m.WriteU32(p + 8, 0xE2000000);
        uint cmd = q.Textured ? 0x3C000000u : 0x38000000u;
        if ((q.Mode & 1) != 0) cmd |= 0x02000000;
        if (q.Textured && (q.Mode & 4) == 0) cmd |= 0x01000000;
        int bx = (q.Mode & 0x2080) == 0 ? (int)m.ReadU32(BackbufferX) : 0;
        int by = (q.Mode & 0x2080) == 0 ? (int)m.ReadU32(BackbufferY) : 0;
        uint cursor = p + 12;
        void Write(BackgroundVertex v, int index)
        {
            m.WriteU32(cursor, v.Color | (index == 0 ? cmd : 0));
            m.WriteU32(cursor + 4, (ushort)(v.X + bx) | ((uint)(ushort)(v.Y + by) << 16));
            cursor += 8;
            if (!q.Textured) return;
            uint extra = index == 0 ? m.ReadU16(ClutIdsAddr + (uint)q.Palette * 2) : index == 1 ? page : 0;
            m.WriteU32(cursor, v.Uv | (extra << 16));
            cursor += 4;
        }
        Write(q.A, 0); Write(q.B, 1); Write(q.C, 2); Write(q.D, 3);
        m.WriteU32(cursor, 0xE1000000);
        cursor += 4;
        AddPrim(m, buffer + OtOfs, q.Order, p, (cursor - p - 4) / 4);
    }

    static bool SameBackgroundRow(BackgroundQuad a, BackgroundQuad b) =>
        a.A.Y == b.A.Y && a.B.Y == b.B.Y && a.C.Y == b.C.Y && a.D.Y == b.D.Y &&
        a.Order == b.Order && a.Palette == b.Palette && a.Textured == b.Textured;

    static void ExtendBackgroundRows(IMemory m, ReadOnlySpan<BackgroundQuad> quads,
        int spacing, ref int count)
    {
        for (int i = 0; i < quads.Length; i++)
        {
            var q = quads[i];
            if ((q.Mode & 8) != 0 || !q.Textured || q.B.X <= q.A.X || q.D.X <= q.C.X) continue;
            bool left = true, right = true;
            for (int j = 0; j < quads.Length; j++)
            {
                if (i == j || (quads[j].Mode & 8) != 0 || !SameBackgroundRow(q, quads[j])) continue;
                if (quads[j].Left < q.Left) left = false;
                if (quads[j].Right > q.Right) right = false;
            }
            int top = spacing > 0 ? spacing : q.B.X - q.A.X;
            int bottom = spacing > 0 ? spacing : q.D.X - q.C.X;
            if (left)
                for (int n = 1; n <= BackgroundPacketCapacity; n++)
                {
                    var next = q.Shift(-n * top, -n * bottom);
                    if (next.Right <= ViewLeft) break;
                    EmitBackground(m, next, ref count);
                }
            if (right)
                for (int n = 1; n <= BackgroundPacketCapacity; n++)
                {
                    var next = q.Shift(n * top, n * bottom);
                    if (next.Left >= ViewRight) break;
                    EmitBackground(m, next, ref count);
                }
        }
    }

    static BackgroundQuad BackgroundRect(BackgroundQuad q, int x, int y, int w, int h) => q with
    {
        A = q.A with { X = x, Y = y }, B = q.B with { X = x + w, Y = y },
        C = q.C with { X = x, Y = y + h }, D = q.D with { X = x + w, Y = y + h }
    };

    static void ExtendCavernColumns(IMemory m, uint table, ReadOnlySpan<BackgroundQuad> quads,
        int sx, int sy, ref int count, bool inverted = false)
    {
        if ((!inverted && sy >= 176) || quads.IsEmpty) return;
        var template = quads[0] with { Mode = 0 };
        for (int i = 0; i < 4; i++)
        {
            int x = (short)m.ReadU16(table + (uint)i * 6) - ((sx / 2) & 255);
            int y = (short)m.ReadU16(table + (uint)i * 6 + 2) - sy / 2;
            if (inverted) y = ((int)m.ReadU32(0x800730CC) - 256) / 2 + 122 -
                (short)m.ReadU16(table + (uint)i * 6 + 2) - sy / 2;
            int flip = m.ReadU16(table + (uint)i * 6 + 4);
            if (y + 126 < 0 || y >= 224) continue;
            int first = (int)Math.Floor((ViewLeft - x - 20) / 256.0);
            int last = (int)Math.Ceiling((ViewRight - x) / 256.0);
            for (int k = first; k <= last; k++)
            {
                int px = x + k * 256;
                if (px >= -20 && px < 256) continue;
                var q = BackgroundRect(template, px + ((flip & 1) != 0 ? 20 : 0),
                    y + ((flip & 2) != 0 ? 0 : 126), (flip & 1) != 0 ? -20 : 20,
                    (flip & 2) != 0 ? 126 : -126);
                EmitBackground(m, q, ref count);
            }
        }
    }

    static void ExtendCavernWaterfalls(IMemory m, ReadOnlySpan<BackgroundQuad> quads, ref int count)
    {
        for (int i = 0; i < quads.Length; i++)
        {
            var q = quads[i];
            if ((q.Mode & 8) != 0) continue;
            bool duplicate = false;
            for (int j = 0; j < i; j++)
                if ((quads[j].Mode & 8) == 0 && SameBackgroundRow(q, quads[j]) &&
                    (q.A.X - quads[j].A.X) % 256 == 0) duplicate = true;
            if (duplicate) continue;
            int first = (int)Math.Floor((ViewLeft - q.Right) / 256.0);
            int last = (int)Math.Ceiling((ViewRight - q.Left) / 256.0);
            for (int k = first; k <= last; k++)
            {
                var next = q.Shift(k * 256, k * 256);
                if (next.A.X >= -2 && next.A.X < 256) continue;
                EmitBackground(m, next, ref count);
            }
        }
    }

    static void ExtendEntranceBushes(IMemory m, ReadOnlySpan<BackgroundQuad> quads, ref int count)
    {
        for (int i = 0; i < quads.Length; i++)
        {
            var q = quads[i];
            int period = q.Order switch { 0x5E => 128, 0x5B => 63, 0x5D => 160, 0x5C => 208, _ => 0 };
            if (period == 0 || (q.Mode & 8) != 0) continue;
            bool duplicate = false;
            for (int j = 0; j < i; j++)
            {
                int dx = quads[j].A.X - q.A.X;
                if (dx % period == 0 && q.Shift(dx, dx) == quads[j]) duplicate = true;
            }
            if (duplicate) continue;
            int first = (int)Math.Floor((ViewLeft - q.Right) / (double)period);
            int last = (int)Math.Ceiling((ViewRight - q.Left) / (double)period);
            for (int k = first; k <= last; k++)
            {
                var next = q.Shift(k * period, k * period);
                bool existing = false;
                for (int j = 0; j < quads.Length; j++)
                    if (next == quads[j]) { existing = true; break; }
                if (!existing) EmitBackground(m, next, ref count);
            }
        }
    }

    static int BackgroundProjectionScale(int h, int depth)
    {
        if (h >= depth * 2) return 0x1FFFF;
        int shift = System.Numerics.BitOperations.LeadingZeroCount((uint)depth) - 16;
        ulong n = (ulong)h << shift, d = (ulong)depth << shift;
        int index = Math.Clamp((int)((d - 0x7FC0) >> 7), 0, 256);
        ulong u = (ulong)Math.Clamp((0x40000 / (index + 0x100) + 1) / 2 - 0x101, 0, 255) + 0x101;
        d = (0x2000080UL - d * u) >> 8;
        d = (0x80UL + d * u) >> 8;
        return (int)Math.Min((n * d + 0x8000) >> 16, 0x1FFFF);
    }

    static void ExtendPerspectiveClouds(IMemory m, uint entity, string stage,
        ReadOnlySpan<BackgroundQuad> quads, int scrollY, ref int count)
    {
        (uint uv, uint data) = stage switch
        {
            "dai" => (0x8018242Cu, 0x80182544u),
            "rdai" => (0x80181BD8u, 0x80181CF0u),
            "bo5" => (0x801818ECu, 0x80181A04u),
            "rbo3" => (0x801812DCu, 0x801813F4u),
            "top" => (0x80181BFCu, 0x80181CE4u),
            "rtop" => (0x801815B4u, 0x8018169Cu),
            _ => (0u, 0u)
        };
        if (data == 0 || quads.IsEmpty) return;
        bool keep = stage is "top" or "rtop";
        int layers = keep || (stage == "rdai" && m.ReadU16(entity + 0x30) != 0) ? 2 : 3;
        int dz = keep ? 224 : 256, h = keep ? 320 : 256, start = keep ? 576 : 448;
        int centerY = stage is "rdai" or "rbo3" ? 96 : 160;
        int timer = (int)(m.ReadU32(0x8003C998) & 1);
        for (int layer = 0; layer < layers; layer++)
        {
            uint descriptor = data + (uint)layer * 8;
            uint pattern = m.ReadU32(descriptor);
            if (pattern < 0x80180000 || pattern > 0x801FFF00) continue;
            int y = (short)m.ReadU16(descriptor + 4) + (keep ? (0x340 - scrollY) >> 3 : 0);
            int phase = m.ReadU16(entity + 0x8A + (uint)layer * 8);
            int x = ((short)m.ReadU16(entity + 2) + (short)m.ReadU16(entity + 0x86 + (uint)layer * 8)) % 2048;
            for (int row = 0; row < 8; row++)
            {
                int z = start + row * dz - phase % dz;
                int near = BackgroundProjectionScale(h, z), far = BackgroundProjectionScale(h, z + dz);
                int y0 = Math.Clamp(centerY + (int)(((long)y * near) >> 16), -1024, 1023);
                int y1 = Math.Clamp(centerY + (int)(((long)y * far) >> 16), -1024, 1023);
                if (y1 < 0) continue;
                uint colors(int distance) => (uint)((byte)((((keep ? 15 : 8) << 16) - distance * (keep ? 475 : 160)) >> 12) |
                    ((byte)((((keep ? 15 : 8) << 16) - distance * (keep ? 512 : 176)) >> 12) << 8) |
                    ((byte)((((keep ? 15 : 8) << 16) - distance * (keep ? 402 : 128)) >> 12) << 16));
                uint color0 = colors(z - start), color1 = colors(z - start + dz);
                int first = (int)Math.Floor((((ViewLeft - 128) * 65536.0 / far) - x - 128) / 256);
                int last = (int)Math.Ceiling((((ViewRight - 128) * 65536.0 / far) - x + 128) / 256);
                for (int column = first; column <= last; column++)
                {
                    int kind = m.ReadU8(pattern + (uint)(((row + phase / dz) & 7) * 8 + (column & 7)));
                    if (kind == 0 || kind > (keep ? 8 : 6)) continue;
                    int tx = x + column * 256;
                    int x0 = Math.Clamp(128 + (int)(((long)(tx - 128) * near) >> 16), -1024, 1023);
                    int x1 = Math.Clamp(128 + (int)(((long)(tx + 128) * near) >> 16), -1024, 1023);
                    int x2 = Math.Clamp(128 + (int)(((long)(tx - 128) * far) >> 16), -1024, 1023);
                    int x3 = Math.Clamp(128 + (int)(((long)(tx + 128) * far) >> 16), -1024, 1023);
                    if (x3 >= 0 && x2 <= 256) continue;
                    uint texture = uv + (uint)kind * 8;
                    ushort palette = keep ? (ushort)((kind < 5 ? 0x4A : 0x48) + timer) : quads[0].Palette;
                    var q = new BackgroundQuad(new(x0, y0, m.ReadU16(texture), color0),
                        new(x1, y0, m.ReadU16(texture + 2), color0),
                        new(x2, y1, m.ReadU16(texture + 4), color1),
                        new(x3, y1, m.ReadU16(texture + 6), color1), 15, palette,
                        m.ReadU16(descriptor + 6), (ushort)(keep ? 0x15 : 4), true);
                    EmitBackground(m, q, ref count);
                }
            }
        }
    }

    static int PositiveMod(int value, int divisor)
    {
        int result = value % divisor;
        return result < 0 ? result + divisor : result;
    }

    static void ExtendCavernBackdrop(IMemory m, uint entity, int scrollX, int scrollY, ref int count)
    {
        int timer = (int)m.ReadU32(0x8003C998);
        int pulse = (timer & 0x80) == 0 ? 0 : (timer & 0x40) == 0 ? timer & 0x3F : 0x3F - (timer & 0x3F);
        uint color = (uint)(0x80 + pulse) | 0x8000u | ((uint)(0x80 + pulse) << 16);
        ushort parameters = m.ReadU16(entity + 0x30);
        uint entry = 0x8018133Cu + (uint)(parameters & 0xFF) * 8;
        for (int i = 0; i < parameters >> 8; i++, entry += 8)
        {
            int left = (short)m.ReadU16(entry) - scrollX;
            int right = (short)m.ReadU16(entry + 2) - scrollX;
            int top = Math.Max((short)m.ReadU16(entry + 4) - scrollY, 0);
            int bottom = Math.Min((short)m.ReadU16(entry + 6) - scrollY, 224);
            left = Math.Max(left, ViewLeft);
            right = Math.Min(right, ViewRight);
            if (left >= right || top >= bottom) continue;
            for (int y = top; y < bottom;)
            {
                int v = PositiveMod(scrollY / 4 + y - 88, 70) + 0x81;
                int height = Math.Min(bottom - y, 70 - (v - 0x81));
                for (int x = left; x < right;)
                {
                    int u = PositiveMod((scrollX - 16) / 4 + x, 126) + 1;
                    int width = Math.Min(right - x, 126 - (u - 1));
                    if (x < 0) width = Math.Min(width, -x);
                    else if (x < 256) width = Math.Min(width, 256 - x);
                    var q = new BackgroundQuad(new(x, y, (ushort)(u | v << 8), color),
                        new(x + width, y, (ushort)(u + width | v << 8), color),
                        new(x, y + height, (ushort)(u | (v + height) << 8), color),
                        new(x + width, y + height, (ushort)(u + width | (v + height) << 8), color),
                        15, 0x85, 0x18, 4, true);
                    if (x + width <= 0 || x >= 256) EmitBackground(m, q, ref count);
                    x += width;
                }
                y += height;
            }
        }
    }

    const uint OuterWallSkyBounds = 0x80180ED4;
    const uint OuterWallWeather = 0x80180EEC;
    const uint OuterWallWaveOffsets = 0x801D635C;
    const int OuterWallWaveRows = 13;
    const int OuterWallWaveFirstPrim = 5;
    const int InvertedOuterWallPrims = 64;

    static bool OuterWallSkyRange(IMemory m, int param, int scrollX, int scrollY, out int xMin, out int xMax)
    {
        int x0 = (short)m.ReadU16(OuterWallSkyBounds + (uint)param * 4) - scrollX;
        int x1 = (short)m.ReadU16(OuterWallSkyBounds + (uint)param * 4 + 2) - scrollX;
        if (param == 4)
        {
            if (scrollY < 0x260) x0 += 0x50;
            if (scrollY < 0x80) x0 += 0x10;
            if (scrollY > 0x190) x1 = 0x7000;
        }
        if (param == 2 && scrollY > 0x1B0)
        {
            x0 -= 0x210;
            x1 = 0x7000;
        }

        if (x0 < 0) x0 = 0;
        xMax = 0xFF;
        if (x1 + 0x20 > 0 && x1 < 0x100)
        {
            if (x1 < 0) x1 = 0;
            if (x0 >= 0x100 && x1 < 0xE0) xMax = x1 + 0x20;
        }
        else
        {
            x1 = 0x7000;
        }

        xMin = Math.Min(x0, x1);
        return xMin < 0x100;
    }

    static void ExtendOuterWallSky(IMemory m, uint entity, int scrollX, int scrollY, ref int count)
    {
        int index = (short)m.ReadU16(entity + EntPrimIndex);
        if (index < 0 || index >= 0x400) return;
        int param = m.ReadU16(entity + EntParams);
        uint weather = m.ReadU32(OuterWallWeather);

        Span<uint> prims = stackalloc uint[OuterWallWaveFirstPrim + OuterWallWaveRows];
        int n = 0;
        for (uint p = PrimBufAddr + (uint)index * PrimStride; p != 0 && n < prims.Length; p = m.ReadU32(p))
            prims[n++] = p;
        if (n < OuterWallWaveFirstPrim) return;
        bool waves = weather != 2 && n == prims.Length;

        if (OuterWallSkyRange(m, param, scrollX - 256, scrollY, out int left, out int leftEnd))
        {
            int a = Math.Max(left - 256, ViewLeft);
            int b = leftEnd == 0xFF ? 0 : leftEnd - 256;
            if (a < b) OuterWallSkyBand(m, prims[..n], weather, waves, a, b, false, ref count);
        }

        if (OuterWallSkyRange(m, param, scrollX + 256, scrollY, out int right, out int rightEnd))
        {
            int a = Math.Max(right + 256, 0xFF);
            int b = Math.Min(rightEnd == 0xFF ? ViewRight : rightEnd + 256, ViewRight);
            if (a < b) OuterWallSkyBand(m, prims[..n], weather, waves, a, b, true, ref count);
        }
    }

    static void ExtendInvertedOuterWallSky(IMemory m, uint entity, ref int count)
    {
        int index = (short)m.ReadU16(entity + EntPrimIndex);
        if (index < 0 || index >= 0x400) return;
        Span<BackgroundQuad> quads = stackalloc BackgroundQuad[InvertedOuterWallPrims];
        int n = 0;
        for (uint p = PrimBufAddr + (uint)index * PrimStride; BackgroundPrimitive(m, p) && n < quads.Length; p = m.ReadU32(p))
            quads[n++] = ReadBackgroundQuad(m, p);
        if (n < OuterWallWaveFirstPrim) return;

        for (int i = 0; i < 2; i++)
        {
            var q = quads[i];
            if ((q.Mode & 8) != 0) continue;
            EmitBackground(m, BackgroundRect(q, ViewLeft, q.A.Y, Margin, q.C.Y - q.A.Y), ref count);
            EmitBackground(m, BackgroundRect(q, 256, q.A.Y, Margin, q.C.Y - q.A.Y), ref count);
        }
        ExtendBackgroundRows(m, quads[2..4], 0, ref count);
        ExtendBackgroundRows(m, quads[OuterWallWaveFirstPrim..n], 0, ref count);
    }

    static void OuterWallSkyBand(IMemory m, ReadOnlySpan<uint> prims, uint weather, bool waves, int a, int b,
        bool right, ref int count)
    {
        var sky = ReadBackgroundQuad(m, prims[0]);
        EmitBackground(m, BackgroundRect(sky, a, sky.A.Y, b - a, sky.C.Y - sky.A.Y) with { Mode = 0 }, ref count);

        if (weather != 1)
        {
            var glow = ReadBackgroundQuad(m, prims[1]);
            EmitBackground(m, BackgroundRect(glow, a, glow.A.Y, b - a, glow.C.Y - glow.A.Y) with { Mode = DrawColors },
                ref count);
        }

        var sea = ReadBackgroundQuad(m, prims[2]);
        int seaTop = sea.A.Uv >> 8, seaBottom = sea.C.Uv >> 8;
        for (int x = a; x < b;)
        {
            int boundary = ((x >> 7) + 1) << 7;
            int end = Math.Min(b, boundary);
            int u0 = x & 0x7F, u1 = end == boundary ? 0x7F : end & 0x7F;
            EmitBackground(m, sea with
            {
                A = new(x, sea.A.Y, (ushort)(u0 | seaTop << 8), sea.A.Color),
                B = new(end, sea.A.Y, (ushort)(u1 | seaTop << 8), sea.A.Color),
                C = new(x, sea.C.Y, (ushort)(u0 | seaBottom << 8), sea.C.Color),
                D = new(end, sea.C.Y, (ushort)(u1 | seaBottom << 8), sea.C.Color),
                Mode = 0
            }, ref count);
            x = end;
        }

        if (!waves) return;
        for (int row = 0; row < OuterWallWaveRows; row++)
        {
            var wave = ReadBackgroundQuad(m, prims[OuterWallWaveFirstPrim + row]);
            int top = (short)m.ReadU16(OuterWallWaveOffsets + (uint)row * 2);
            int bottom = (short)m.ReadU16(OuterWallWaveOffsets + (uint)(row + 1) * 2);
            for (int column = 0;; column++)
            {
                int offset = right ? 0xEE + column * 0x7F : -0x10 - column * 0x7F;
                if (right ? offset + Math.Min(top, bottom) >= b : offset + Math.Max(top, bottom) + 0x80 <= a) break;
                OuterWallWave(m, wave, top + offset, bottom + offset, a, b, ref count);
            }
        }
    }

    static void OuterWallWave(IMemory m, BackgroundQuad wave, int top, int bottom, int a, int b, ref int count)
    {
        static (int X0, int U0, int X1, int U1) Clip(int x0, int a, int b)
        {
            int x1 = x0 + 0x80, u0 = 0x80, u1 = 0xFF;
            if (a >= x0)
            {
                x0 = a;
                u0 = Math.Max(0x80, 0xFF - (x1 - x0));
            }
            if (x1 > b)
            {
                u1 = Math.Max(0x80, 0xFF - (x1 - b));
                x1 = b;
            }
            return (x0, u0, x1, u1);
        }

        var t = Clip(top, a, b);
        var d = Clip(bottom, a, b);
        if (t.X0 >= t.X1 && d.X0 >= d.X1) return;
        int vTop = wave.A.Uv >> 8, vBottom = wave.C.Uv >> 8;
        EmitBackground(m, wave with
        {
            A = new(t.X0, wave.A.Y, (ushort)(t.U0 | vTop << 8), wave.A.Color),
            B = new(t.X1, wave.A.Y, (ushort)(t.U1 | vTop << 8), wave.A.Color),
            C = new(d.X0, wave.C.Y, (ushort)(d.U0 | vBottom << 8), wave.C.Color),
            D = new(d.X1, wave.C.Y, (ushort)(d.U1 | vBottom << 8), wave.C.Color),
            Mode = 0
        }, ref count);
    }

    static void ExtendStageBackgrounds(TilemapRenderedEvent e)
    {
        if (OriginalAspect || !DisplayModeHooks.IsStage || Margin <= 0) return;
        var m = e.Memory;
        if (m.ReadU32(0x800973EC) != 0) return;
        string? stage = StageOverlay(m)?.Name.ToLowerInvariant();
        if (stage == null) return;
        int count = 0;
        Span<BackgroundQuad> quads = stackalloc BackgroundQuad[128];
        for (int i = StageEntityStart; i < TotalEntityCount; i++)
        {
            uint entity = PedAt(i), fn = m.ReadU32(entity + EntPfnUpdate);
            if (fn == 0 || (m.ReadU32(entity + EntFlags) & 0x800000) == 0) continue;
            bool Match(string name) => StageSym(m, name).Addr == fn;
            bool columns = (stage == "no4" && Match("func_us_801C1C94")) ||
                (stage == "rno4" && Match("func_801C5748"));
            bool waterfalls = (stage == "no4" && Match("func_us_801C1EE4")) ||
                (stage == "rno4" && Match("func_801C59AC"));
            bool cavernBackdrop = stage == "no4" && Match("func_us_801C1844");
            bool bushes = ((stage is "no3" or "np3") && Match("EntityBackgroundBushes")) ||
                (stage == "rno3" && Match("func_801B0F50"));
            bool warp = (stage == "wrp" && Match("EntityWarpRoom")) ||
                (stage == "rwrp" && Match("EntityRWarpRoom"));
            bool clouds = ((stage is "dai" or "bo5" or "rbo3" or "top" or "rtop") && Match("EntityClouds")) ||
                (stage == "rdai" && Match("func_801C6FE4"));
            bool rows = ((stage is "nz0" or "rnz0") && Match("EntityPurpleBrickScrollingBackground")) ||
                ((stage is "nz1" or "rnz1") && Match("EntityBackgroundGears")) ||
                ((stage is "cat" or "rcat") && (Match("EntityCavernBackgroundLossoth") || Match("EntityCavernBackgroundDiscusLord"))) ||
                (stage == "lib" && (Match("EntityBackgroundBookcase") || Match("EntityBackgroundFurniture"))) ||
                (stage == "rlib" && (Match("RLIB_EntityBackgroundBookcase") || Match("RLIB_EntityBackgroundFurniture"))) ||
                ((stage is "no3" or "np3") && (Match("EntityBackgroundTrees") ||
                    Match("EntityDistantBackgroundTrees"))) ||
                (stage == "rno3" && Match("RNO3_EntityBackgroundTrees")) ||
                (stage == "dre" && Match("EntityBackgroundClouds"));
            bool corridor = (stage == "no0" && Match("func_us_801CC750")) ||
                (stage == "rno0" && Match("func_801C08F8"));
            bool outerWallSky = stage == "no1" && Match("func_us_801B6490");
            bool invertedOuterWallSky = stage == "rno1" && Match("func_801A68AC");
            if (!columns && !rows && !corridor && !waterfalls && !cavernBackdrop && !warp && !clouds && !bushes &&
                !outerWallSky && !invertedOuterWallSky) continue;
            if (cavernBackdrop)
            {
                ExtendCavernBackdrop(m, entity, e.ScrollX, e.ScrollY, ref count);
                continue;
            }
            if (outerWallSky)
            {
                ExtendOuterWallSky(m, entity, e.ScrollX, e.ScrollY, ref count);
                continue;
            }
            if (invertedOuterWallSky)
            {
                ExtendInvertedOuterWallSky(m, entity, ref count);
                continue;
            }
            int index = (short)m.ReadU16(entity + 0x64);
            if (index < 0 || index >= 0x400) continue;
            uint p = PrimBufAddr + (uint)index * PrimStride;
            int n = 0;
            for (int visited = 0; BackgroundPrimitive(m, p) && visited < quads.Length; visited++)
            {
                byte type = (byte)(m.ReadU8(p + 7) & 15);
                if (type is 3 or 4) quads[n++] = ReadBackgroundQuad(m, p);
                p = m.ReadU32(p);
            }
            if (columns) ExtendCavernColumns(m, stage == "no4" ? 0x801814E4u : 0x80181428u,
                quads[..n], e.ScrollX, e.ScrollY, ref count, stage == "rno4");
            else if (waterfalls) ExtendCavernWaterfalls(m, quads[..n], ref count);
            else if (clouds) ExtendPerspectiveClouds(m, entity, stage, quads[..n], e.ScrollY, ref count);
            else if (bushes) ExtendEntranceBushes(m, quads[..n], ref count);
            else if (!warp) ExtendBackgroundRows(m, quads[..n], corridor ? 30 : 0, ref count);
            if (stage == "dre" || warp)
                for (int j = 0; j < n; j++)
                {
                    var q = quads[j];
                    if (q.Textured || q.Left != 0 || q.Right != 256) continue;
                    EmitBackground(m, BackgroundRect(q, ViewLeft, q.A.Y, Margin, q.C.Y - q.A.Y), ref count);
                    EmitBackground(m, BackgroundRect(q, 256, q.A.Y, Margin, q.C.Y - q.A.Y), ref count);
                }
        }
    }
}
