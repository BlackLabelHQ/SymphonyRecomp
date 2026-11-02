using RecompOne.Runtime.Context;
using RecompOne.Runtime.Dispatch;
using RecompOne.Runtime.Memory;

namespace Recompiled;

//some richter fixes, not sure if all of them are properly handled
public static partial class WidescreenPatch
{
    public static bool RicStepSlideWide(CpuContext c, IMemory m) => WideExt(RicStepSlideEdge, c, m);
    public static bool RicCheckInputWide(CpuContext c, IMemory m) => WideExt(RicCheckInputEdge, c, m);
    public static bool RicEntitySubwpnCrossWide(CpuContext c, IMemory m) => WideExt(RicEntitySubwpnCrossEdge, c, m);
    public static bool RicEntitySubwpnReboundStoneWide(CpuContext c, IMemory m) => WideExt(RicEntitySubwpnReboundStoneEdge, c, m);
    public static bool RicEntitySubwpnAguneaWide(CpuContext c, IMemory m) => WideExt(RicEntitySubwpnAguneaEdge, c, m);

    static void RicStepSlideEdge(CpuContext c, IMemory m)
    {
        var mem = (PSMemory)m;
        c.SP = c.SP - 0x18u;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x33ECu));
        c.V1 = 0u;
        if (c.V0 != 0u) {
            mem.WriteU32((c.SP + 0x10u), c.RA);
            goto L8015B8C8;
        }
        mem.WriteU32((c.SP + 0x10u), c.RA);
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2F20u));
        c.V0 = c.V0 & 0x0004u;
        c.V1 = 0u < c.V0 ? 1u : 0u;
        goto L8015B8E4;
        L8015B8C8: ;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2F20u));
        c.V0 = c.V0 & 0x0008u;
        if (c.V0 == 0u) {
            goto L8015B8E4;
        }
        c.V1 = 0u | 0x0001u;
        L8015B8E4: ;
        c.V0 = 0x80070000u;
        c.V0 = (uint)(short)mem.ReadU16((c.V0 + 0x33DAu));
        c.V0 = (int)c.V0 < OutsetRight(252) ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L8015B914;
        }
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x33ECu));
        if (c.V0 != 0u) {
            goto L8015B914;
        }
        c.V1 = 0u | 0x0001u;
        L8015B914: ;
        c.V0 = 0x80070000u;
        c.V0 = (uint)(short)mem.ReadU16((c.V0 + 0x33DAu));
        c.V0 = (int)c.V0 < OutsetLeft(5) ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L8015B944;
        }
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x33ECu));
        if (c.V0 == 0u) {
            goto L8015B958;
        }
        c.V1 = 0u | 0x0001u;
        L8015B944: ;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x33ECu));
        if (c.V0 != 0u) {
            goto L8015B984;
        }
        L8015B958: ;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2C18u));
        c.V0 = c.V0 & 0x8000u;
        if (c.V0 != 0u) {
            goto L8015B99C;
        }
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x33ECu));
        if (c.V0 == 0u) {
            goto L8015B9A0;
        }
        L8015B984: ;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2C3Cu));
        c.V0 = c.V0 & 0x8000u;
        if (c.V0 == 0u) {
            goto L8015B9A0;
        }
        L8015B99C: ;
        c.V1 = 0u | 0x0001u;
        L8015B9A0: ;
        if (c.V1 == 0u) {
            goto L8015BA20;
        }
        c.V1 = 0x80070000u;
        c.V1 = c.V1 + 0x3428u;
        c.V0 = mem.ReadU16(c.V1);
        c.V0 = c.V0 < 0x00000006u ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = 0u | 0x0006u;
            goto L8015BA20;
        }
        c.V0 = 0u | 0x0006u;
        mem.WriteU16(c.V1, (ushort)c.V0);
        c.V1 = 0x80070000u;
        c.V1 = mem.ReadU32((c.V1 + 0x33E0u));
        c.V0 = 0x00010000u;
        c.V0 = (int)c.V0 < (int)c.V1 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V1 = 0xFFFF0000u;
            goto L8015B9EC;
        }
        c.V1 = 0xFFFF0000u;
        c.V0 = 0x00020000u;
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x33E0u), c.V0);
        L8015B9EC: ;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x33E0u));
        c.V0 = (int)c.V0 < (int)c.V1 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.A1 = 0u;
            goto L8015BA10;
        }
        c.A1 = 0u;
        c.V0 = 0xFFFE0000u;
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x33E0u), c.V0);
        L8015BA10: ;
        c.A0 = 0x80070000u;
        c.A0 = mem.ReadU32((c.A0 - 0x3C48u));
        c.A2 = 0u;
        c.RA = 0x8015BA20u;
        SoTN_ric.RicCreateEntFactoryFromEntity(c, m);
        L8015BA20: ;
        c.V1 = 0x80070000u;
        c.V1 = mem.ReadU16((c.V1 + 0x3428u));
        c.V0 = c.V1 < 0x00000005u ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = c.V1 < 0x00000007u ? 1u : 0u;
            goto L8015BA9C;
        }
        c.V0 = c.V1 < 0x00000007u ? 1u : 0u;
        c.A0 = 0u | 0x0044u;
        c.RA = 0x8015BA40u;
        SoTN_ric.RicCheckInput(c, m);
        if (c.V0 != 0u) {
            goto L8015BB70;
        }
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x2F92u));
        if (c.V0 != 0u) {
            goto L8015BAC0;
        }
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2EECu));
        c.V0 = c.V0 & 0x0040u;
        if (c.V0 == 0u) {
            goto L8015BAC0;
        }
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x33DEu));
        c.V0 = c.V0 - 0x4u;
        c.At = 0x80070000u;
        mem.WriteU16((c.At + 0x33DEu), (ushort)c.V0);
        c.RA = 0x8015BA94u;
        SoTN_ric.RicSetSlideKick(c, m);
        goto L8015BB70;
        L8015BA9C: ;
        if (c.V0 == 0u) {
            c.A0 = 0x00040000u;
            goto L8015BAAC;
        }
        c.A0 = 0x00040000u;
        c.A0 = c.A0 | 0x0044u;
        goto L8015BAB0;
        L8015BAAC: ;
        c.A0 = c.A0 | 0x004Cu;
        L8015BAB0: ;
        c.RA = 0x8015BAB8u;
        SoTN_ric.RicCheckInput(c, m);
        if (c.V0 != 0u) {
            goto L8015BB70;
        }
        L8015BAC0: ;
        c.A0 = 0u | 0x2000u;
        c.RA = 0x8015BAC8u;
        SoTN_ric.RicDecelerateX(c, m);
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x3406u));
        if (c.V0 != 0u) {
            goto L8015BB70;
        }
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x373Cu));
        c.V0 = c.V0 & 0x0003u;
        if (c.V0 != 0u) {
            c.V1 = 0x00010000u;
            goto L8015BB28;
        }
        c.V1 = 0x00010000u;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x3428u));
        c.V0 = c.V0 - 0x3u;
        c.V0 = c.V0 < 0x00000003u ? 1u : 0u;
        if (c.V0 == 0u) {
            c.A1 = 0x00020000u;
            goto L8015BB28;
        }
        c.A1 = 0x00020000u;
        c.A0 = 0x80070000u;
        c.A0 = mem.ReadU32((c.A0 - 0x3C48u));
        c.A1 = c.A1 | 0x0018u;
        c.A2 = 0u;
        c.RA = 0x8015BB24u;
        SoTN_ric.RicCreateEntFactoryFromEntity(c, m);
        c.V1 = 0x00010000u;
        L8015BB28: ;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x3428u));
        c.V1 = c.V1 | 0x0006u;
        if (c.V0 != c.V1) {
            c.A1 = 0u;
            goto L8015BB4C;
        }
        c.A1 = 0u;
        c.A0 = 0x80070000u;
        c.A0 = mem.ReadU32((c.A0 - 0x3C48u));
        c.A2 = 0u;
        c.RA = 0x8015BB4Cu;
        SoTN_ric.RicCreateEntFactoryFromEntity(c, m);
        L8015BB4C: ;
        c.V0 = 0x80070000u;
        c.V0 = (uint)(short)mem.ReadU16((c.V0 + 0x342Au));
        if ((int)c.V0 >= 0) {
            goto L8015BB70;
        }
        c.A1 = 0x80070000u;
        c.A1 = mem.ReadU32((c.A1 + 0x33E0u));
        c.A0 = 0u;
        c.RA = 0x8015BB70u;
        SoTN_ric.RicSetCrouch(c, m);
        L8015BB70: ;
        c.RA = mem.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x18u;
        return;
    }


    static void RicCheckInputEdge(CpuContext c, IMemory m)
    {
        var mem = (PSMemory)m;
        c.SP = c.SP - 0x18u;
        mem.WriteU32((c.SP + 0x10u), c.S0);
        c.S0 = c.A0;
        c.V0 = c.S0 & 0x0008u;
        if (c.V0 == 0u) {
            mem.WriteU32((c.SP + 0x14u), c.RA);
            goto L8015DBE4;
        }
        mem.WriteU32((c.SP + 0x14u), c.RA);
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x2F66u));
        if (c.V0 != 0u) {
            c.V0 = c.S0 & 0x8000u;
            goto L8015DBE8;
        }
        c.V0 = c.S0 & 0x8000u;
        c.RA = 0x8015DBE4u;
        SoTN_ric.RicCheckFacing(c, m);
        L8015DBE4: ;
        c.V0 = c.S0 & 0x8000u;
        L8015DBE8: ;
        c.V0 = 0u < c.V0 ? 1u : 0u;
        c.V0 = 0u - c.V0;
        c.V1 = c.V0 & 0x3800u;
        c.V0 = 0x00010000u;
        c.V0 = c.S0 & c.V0;
        if (c.V0 == 0u) {
            c.V0 = c.S0 & 0x0200u;
            goto L8015DC58;
        }
        c.V0 = c.S0 & 0x0200u;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x33E4u));
        c.V0 = c.V0 + 0x1FFFu;
        c.V0 = c.V0 < 0x00007FFFu ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V1 = 0u | 0x3800u;
            goto L8015DC54;
        }
        c.V1 = 0u | 0x3800u;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x2F64u));
        c.V0 = c.V0 & 0x0020u;
        if (c.V0 != 0u) {
            c.V0 = c.S0 & 0x0200u;
            goto L8015DC58;
        }
        c.V0 = c.S0 & 0x0200u;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2EE8u));
        c.V0 = c.V0 & 0x0040u;
        if (c.V0 == 0u) {
            c.V0 = c.S0 & 0x0200u;
            goto L8015DC58;
        }
        c.V0 = c.S0 & 0x0200u;
        c.V1 = 0u | 0x0B33u;
        L8015DC54: ;
        c.V0 = c.S0 & 0x0200u;
        L8015DC58: ;
        if (c.V0 == 0u) {
            goto L8015DC80;
        }
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x33E4u));
        c.V0 = c.V0 + 0x1FFFu;
        c.V0 = c.V0 < 0x00007FFFu ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V1 = 0u | 0x3800u;
            goto L8015DC80;
        }
        c.V1 = 0u | 0x3800u;
        c.V1 = 0u | 0x1C00u;
        L8015DC80: ;
        c.V0 = 0x80090000u;
        c.V0 = mem.ReadU32((c.V0 + 0x7448u));
        c.V0 = (int)c.V0 < 41 ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L8015DCA8;
        }
        if ((int)c.V1 >= 0) {
            c.V0 = c.V1;
            goto L8015DCA4;
        }
        c.V0 = c.V1;
        c.V0 = c.V1 + 0x3u;
        L8015DCA4: ;
        c.V1 = (uint)((int)c.V0 >> 2);
        L8015DCA8: ;
        c.A0 = 0x80070000u;
        c.A0 = c.A0 + 0x33E4u;
        c.V0 = mem.ReadU32(c.A0);
        c.V0 = c.V1 + c.V0;
        c.V1 = 0x00070000u;
        mem.WriteU32(c.A0, c.V0);
        c.V0 = (int)c.V1 < (int)c.V0 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = c.S0 & 0x0080u;
            goto L8015DCD4;
        }
        c.V0 = c.S0 & 0x0080u;
        mem.WriteU32(c.A0, c.V1);
        L8015DCD4: ;
        if (c.V0 == 0u) {
            goto L8015DD0C;
        }
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2F20u));
        c.V0 = c.V0 & 0x0002u;
        if (c.V0 == 0u) {
            c.V1 = 0xFFFF0000u;
            goto L8015DD0C;
        }
        c.V1 = 0xFFFF0000u;
        c.V0 = mem.ReadU32(c.A0);
        c.V0 = (int)c.V0 < (int)c.V1 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L8015DD0C;
        }
        mem.WriteU32(c.A0, c.V1);
        L8015DD0C: ;
        c.A0 = 0x80070000u;
        c.A0 = mem.ReadU32((c.A0 + 0x33E4u));
        if ((int)c.A0 < 0) {
            c.V0 = c.S0 & 0x0001u;
            goto L8015E0E8;
        }
        c.V0 = c.S0 & 0x0001u;
        if (c.V0 == 0u) {
            c.V0 = 0x00020000u;
            goto L8015E048;
        }
        c.V0 = 0x00020000u;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2F20u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u) {
            c.V0 = 0u | 0x0001u;
            goto L8015E044;
        }
        c.V0 = 0u | 0x0001u;
        c.V1 = 0x80070000u;
        c.V1 = mem.ReadU16((c.V1 + 0x2F66u));
        if (c.V1 == c.V0) {
            c.V0 = (int)c.V1 < 2 ? 1u : 0u;
            goto L8015DE58;
        }
        c.V0 = (int)c.V1 < 2 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0002u;
            goto L8015DD6C;
        }
        c.V0 = 0u | 0x0002u;
        if (c.V1 == c.V0) {
            c.V0 = 0u | 0x0003u;
            goto L8015DED8;
        }
        c.V0 = 0u | 0x0003u;
        if (c.V1 == c.V0) {
            c.V0 = 0x00060000u;
            goto L8015DFAC;
        }
        c.V0 = 0x00060000u;
        L8015DD6C: ;
        c.V1 = 0x80070000u;
        c.V1 = mem.ReadU16((c.V1 + 0x2F64u));
        c.V0 = c.V1 & 0x0008u;
        if (c.V0 == 0u) {
            c.V0 = 0x00060000u;
            goto L8015DDB8;
        }
        c.V0 = 0x00060000u;
        c.A1 = 0x80070000u;
        c.A1 = mem.ReadU32((c.A1 + 0x33E0u));
        c.A0 = 0u | 0x0003u;
        c.RA = 0x8015DD94u;
        SoTN_ric.RicSetCrouch(c, m);
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x064Bu;
        c.RA = 0x8015DDA8u;
        Dispatcher.Call(c, m, c.V0);
        c.A0 = 0x80070000u;
        c.A0 = mem.ReadU32((c.A0 - 0x3C48u));
        c.A1 = 0u;
        goto L8015E0D8;
        L8015DDB8: ;
        c.V0 = c.V0 | 0xE000u;
        c.V0 = (int)c.V0 < (int)c.A0 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.A0 = 0u | 0x0001u;
            goto L8015DDF4;
        }
        c.A0 = 0u | 0x0001u;
        c.A1 = 0u;
        c.RA = 0x8015DDD0u;
        SoTN_ric.RicSetCrouch(c, m);
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x064Bu;
        c.RA = 0x8015DDE4u;
        Dispatcher.Call(c, m, c.V0);
        c.A0 = 0x80070000u;
        c.A0 = mem.ReadU32((c.A0 - 0x3C48u));
        c.A1 = 0u;
        goto L8015E0D8;
        L8015DDF4: ;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2EE8u));
        c.V0 = c.V0 & 0xA000u;
        if (c.V0 == 0u) {
            c.V0 = c.V1 & 0x0010u;
            goto L8015DE48;
        }
        c.V0 = c.V1 & 0x0010u;
        if (c.V0 == 0u) {
            goto L8015DE38;
        }
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x2F9Au));
        if (c.V0 != 0u) {
            goto L8015DE38;
        }
        c.RA = 0x8015DE30u;
        SoTN_ric.RicSetRun(c, m);
        c.V0 = 0u | 0x0001u;
        goto L8015E36C;
        L8015DE38: ;
        c.A0 = 0u;
        c.RA = 0x8015DE40u;
        SoTN_ric.RicSetWalk(c, m);
        c.V0 = 0u | 0x0001u;
        goto L8015E36C;
        L8015DE48: ;
        c.A0 = 0u;
        c.RA = 0x8015DE50u;
        SoTN_ric.RicSetStand(c, m);
        c.V0 = 0u | 0x0001u;
        goto L8015E36C;
        L8015DE58: ;
        c.V0 = 0x00060000u;
        c.V0 = c.V0 | 0xE000u;
        c.V0 = (int)c.V0 < (int)c.A0 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = 0u | 0x0002u;
            goto L8015DE8C;
        }
        c.V0 = 0u | 0x0002u;
        c.V1 = 0x80040000u;
        c.V1 = mem.ReadU32((c.V1 - 0x3824u));
        c.At = 0x80070000u;
        mem.WriteU16((c.At + 0x3404u), (ushort)c.V0);
        c.V0 = 0x80150000u;
        c.V0 = c.V0 + 0x55A8u;
        c.A0 = 0u | 0x0647u;
        goto L8015DF08;
        L8015DE8C: ;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x2F64u));
        c.V1 = 0x80150000u;
        c.V1 = c.V1 + 0x5588u;
        c.At = 0x80070000u;
        mem.WriteU16((c.At + 0x3404u), (ushort)0u);
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x3424u), c.V1);
        c.V0 = c.V0 & 0x0008u;
        if (c.V0 == 0u) {
            c.A1 = 0u;
            goto L8015DF60;
        }
        c.A1 = 0u;
        c.A0 = 0x80070000u;
        c.A0 = mem.ReadU32((c.A0 - 0x3C48u));
        c.A2 = 0u;
        c.RA = 0x8015DEC8u;
        SoTN_ric.RicCreateEntFactoryFromEntity(c, m);
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x0647u;
        goto L8015DF8C;
        L8015DED8: ;
        c.V0 = 0x00060000u;
        c.V0 = c.V0 | 0xE000u;
        c.V0 = (int)c.V0 < (int)c.A0 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.A0 = 0u | 0x064Bu;
            goto L8015DF34;
        }
        c.A0 = 0u | 0x064Bu;
        c.V1 = 0x80040000u;
        c.V1 = mem.ReadU32((c.V1 - 0x3824u));
        c.V0 = 0u | 0x0002u;
        c.At = 0x80070000u;
        mem.WriteU16((c.At + 0x3404u), (ushort)c.V0);
        c.V0 = 0x80150000u;
        c.V0 = c.V0 + 0x5738u;
        L8015DF08: ;
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x3424u), c.V0);
        c.RA = 0x8015DF18u;
        Dispatcher.Call(c, m, c.V1);
        c.A1 = 0u;
        c.A0 = 0x80070000u;
        c.A0 = mem.ReadU32((c.A0 - 0x3C48u));
        c.A2 = 0u;
        c.RA = 0x8015DF2Cu;
        SoTN_ric.RicCreateEntFactoryFromEntity(c, m);
        goto L8015DF94;
        L8015DF34: ;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x2F64u));
        c.V1 = 0x80150000u;
        c.V1 = c.V1 + 0x5730u;
        c.At = 0x80070000u;
        mem.WriteU16((c.At + 0x3404u), (ushort)0u);
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x3424u), c.V1);
        c.V0 = c.V0 & 0x0008u;
        if (c.V0 != 0u) {
            c.A1 = 0u;
            goto L8015DF70;
        }
        c.A1 = 0u;
        L8015DF60: ;
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x33E0u), 0u);
        goto L8015DF94;
        L8015DF70: ;
        c.A0 = 0x80070000u;
        c.A0 = mem.ReadU32((c.A0 - 0x3C48u));
        c.A2 = 0u;
        c.RA = 0x8015DF80u;
        SoTN_ric.RicCreateEntFactoryFromEntity(c, m);
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x064Bu;
        L8015DF8C: ;
        c.RA = 0x8015DF94u;
        Dispatcher.Call(c, m, c.V0);
        L8015DF94: ;
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x33E4u), 0u);
        c.At = 0x80070000u;
        mem.WriteU16((c.At + 0x2F64u), (ushort)0u);
        c.V0 = 0u | 0x0001u;
        goto L8015E36C;
        L8015DFAC: ;
        c.V0 = c.V0 | 0xE000u;
        c.V0 = (int)c.V0 < (int)c.A0 ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L8015DFD4;
        }
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x2F64u));
        c.V0 = c.V0 & 0x0008u;
        if (c.V0 == 0u) {
            goto L8015DFFC;
        }
        L8015DFD4: ;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x064Bu;
        c.RA = 0x8015DFE8u;
        Dispatcher.Call(c, m, c.V0);
        c.A1 = 0u;
        c.A0 = 0x80070000u;
        c.A0 = mem.ReadU32((c.A0 - 0x3C48u));
        c.A2 = 0u;
        c.RA = 0x8015DFFCu;
        SoTN_ric.RicCreateEntFactoryFromEntity(c, m);
        L8015DFFC: ;
        c.A0 = 0x80070000u;
        c.A0 = c.A0 + 0x2F64u;
        c.V0 = mem.ReadU16(c.A0);
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x33E4u), 0u);
        c.V0 = c.V0 & 0x0008u;
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0001u;
            goto L8015E024;
        }
        c.V0 = 0u | 0x0001u;
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x33E0u), 0u);
        L8015E024: ;
        c.V1 = 0x80150000u;
        c.V1 = c.V1 + 0x55E8u;
        c.At = 0x80070000u;
        mem.WriteU16((c.At + 0x3404u), (ushort)0u);
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x3424u), c.V1);
        mem.WriteU16(c.A0, (ushort)0u);
        goto L8015E36C;
        L8015E044: ;
        c.V0 = 0x00020000u;
        L8015E048: ;
        c.V0 = c.S0 & c.V0;
        if (c.V0 == 0u) {
            c.V0 = c.S0 & 0x0004u;
            goto L8015E0EC;
        }
        c.V0 = c.S0 & 0x0004u;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2F20u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u) {
            c.V0 = c.S0 & 0x0004u;
            goto L8015E0EC;
        }
        c.V0 = c.S0 & 0x0004u;
        c.A1 = 0x80070000u;
        c.A1 = mem.ReadU32((c.A1 + 0x33E0u));
        c.A0 = 0u | 0x0001u;
        c.RA = 0x8015E07Cu;
        SoTN_ric.RicSetCrouch(c, m);
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x064Bu;
        c.RA = 0x8015E090u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x2F7Cu));
        if (c.V0 == 0u) {
            goto L8015E0CC;
        }
        c.V0 = 0x80090000u;
        c.V0 = mem.ReadU32((c.V0 + 0x7BA0u));
        c.V0 = (int)c.V0 < 2 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L8015E0CC;
        }
        c.RA = 0x8015E0C4u;
        SoTN_ric.RicSetDeadPrologue(c, m);
        c.V0 = 0u | 0x0001u;
        goto L8015E36C;
        L8015E0CC: ;
        c.A0 = 0x80070000u;
        c.A0 = mem.ReadU32((c.A0 - 0x3C48u));
        c.A1 = 0u;
        L8015E0D8: ;
        c.A2 = 0u;
        c.RA = 0x8015E0E0u;
        SoTN_ric.RicCreateEntFactoryFromEntity(c, m);
        c.V0 = 0u | 0x0001u;
        goto L8015E36C;
        L8015E0E8: ;
        c.V0 = c.S0 & 0x0004u;
        L8015E0EC: ;
        if (c.V0 == 0u) {
            goto L8015E1B4;
        }
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2F20u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 != 0u) {
            goto L8015E1B4;
        }
        c.V1 = 0x80070000u;
        c.V1 = mem.ReadU16((c.V1 + 0x2F66u));
        if (c.V1 == 0u) {
            c.V0 = 0u | 0x0001u;
            goto L8015E1A4;
        }
        c.V0 = 0u | 0x0001u;
        if (c.V1 != c.V0) {
            goto L8015E130;
        }
        c.V1 = 0u | 0x0040u;
        goto L8015E144;
        L8015E130: ;
        c.V0 = 0u | 0x0002u;
        if (c.V1 != c.V0) {
            c.V0 = 0u | 0x0003u;
            goto L8015E170;
        }
        c.V0 = 0u | 0x0003u;
        c.V0 = 0u | 0x0001u;
        c.V1 = 0u | 0x0041u;
        L8015E144: ;
        c.At = 0x80070000u;
        mem.WriteU16((c.At + 0x3406u), (ushort)c.V1);
        c.V1 = 0u | 0x0004u;
        c.At = 0x80070000u;
        mem.WriteU16((c.At + 0x3404u), (ushort)c.V1);
        c.V1 = 0x80150000u;
        c.V1 = c.V1 + 0x55C8u;
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x3424u), c.V1);
        goto L8015E36C;
        L8015E170: ;
        if (c.V1 != c.V0) {
            c.V1 = 0u | 0x0042u;
            goto L8015E1B4;
        }
        c.V1 = 0u | 0x0042u;
        c.At = 0x80070000u;
        mem.WriteU16((c.At + 0x3406u), (ushort)c.V1);
        c.V1 = 0u | 0x0004u;
        c.At = 0x80070000u;
        mem.WriteU16((c.At + 0x3404u), (ushort)c.V1);
        c.V1 = 0x80150000u;
        c.V1 = c.V1 + 0x5638u;
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x3424u), c.V1);
        c.V0 = 0u | 0x0001u;
        goto L8015E36C;
        L8015E1A4: ;
        c.RA = 0x8015E1ACu;
        SoTN_ric.RicSetFall(c, m);
        c.V0 = 0u | 0x0001u;
        goto L8015E36C;
        L8015E1B4: ;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x2F66u));
        if (c.V0 != 0u) {
            c.V0 = 0u;
            goto L8015E36C;
        }
        c.V0 = 0u;
        c.V0 = c.S0 & 0x0040u;
        if (c.V0 == 0u) {
            c.V0 = 0x00040000u;
            goto L8015E200;
        }
        c.V0 = 0x00040000u;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2EECu));
        c.V0 = c.V0 & 0x0010u;
        if (c.V0 == 0u) {
            c.V0 = 0x00040000u;
            goto L8015E200;
        }
        c.V0 = 0x00040000u;
        c.RA = 0x8015E1F4u;
        SoTN_ric.RicDoCrash(c, m);
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0001u;
            goto L8015E36C;
        }
        c.V0 = 0u | 0x0001u;
        c.V0 = 0x00040000u;
        L8015E200: ;
        c.V0 = c.S0 & c.V0;
        if (c.V0 == 0u) {
            c.V0 = c.S0 & 0x0010u;
            goto L8015E2D4;
        }
        c.V0 = c.S0 & 0x0010u;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x33ECu));
        if (c.V0 != 0u) {
            goto L8015E230;
        }
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2C18u));
        c.V0 = c.V0 & 0x8000u;
        goto L8015E240;
        L8015E230: ;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2C3Cu));
        c.V0 = c.V0 & 0x8000u;
        L8015E240: ;
        if (c.V0 != 0u) {
            c.V0 = c.S0 & 0x0010u;
            goto L8015E2D4;
        }
        c.V0 = c.S0 & 0x0010u;
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)mem.ReadU16((c.V1 + 0x33DAu));
        c.V0 = (int)c.V1 < OutsetRight(252) ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V0 = (int)c.V1 < OutsetLeft(5) ? 1u : 0u;
            goto L8015E274;
        }
        c.V0 = (int)c.V1 < OutsetLeft(5) ? 1u : 0u;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x33ECu));
        if (c.V0 == 0u) {
            c.V0 = (int)c.V1 < OutsetLeft(5) ? 1u : 0u;
            goto L8015E2D0;
        }
        c.V0 = (int)c.V1 < OutsetLeft(5) ? 1u : 0u;
        L8015E274: ;
        if (c.V0 == 0u) {
            goto L8015E290;
        }
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x33ECu));
        if (c.V0 != 0u) {
            c.V0 = c.S0 & 0x0010u;
            goto L8015E2D4;
        }
        c.V0 = c.S0 & 0x0010u;
        L8015E290: ;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2EE8u));
        c.V0 = c.V0 & 0x4000u;
        if (c.V0 == 0u) {
            c.V0 = c.S0 & 0x0010u;
            goto L8015E2D4;
        }
        c.V0 = c.S0 & 0x0010u;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2EECu));
        c.V0 = c.V0 & 0x0040u;
        if (c.V0 == 0u) {
            c.V0 = c.S0 & 0x0010u;
            goto L8015E2D4;
        }
        c.V0 = c.S0 & 0x0010u;
        c.RA = 0x8015E2C8u;
        SoTN_ric.RicSetSlide(c, m);
        c.V0 = 0u | 0x0001u;
        goto L8015E36C;
        L8015E2D0: ;
        c.V0 = c.S0 & 0x0010u;
        L8015E2D4: ;
        if (c.V0 == 0u) {
            c.V0 = c.S0 & 0x1000u;
            goto L8015E304;
        }
        c.V0 = c.S0 & 0x1000u;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2EECu));
        c.V0 = c.V0 & 0x0040u;
        if (c.V0 == 0u) {
            c.V0 = c.S0 & 0x1000u;
            goto L8015E304;
        }
        c.V0 = c.S0 & 0x1000u;
        c.RA = 0x8015E2FCu;
        SoTN_ric.RicSetJump(c, m);
        c.V0 = 0u | 0x0001u;
        goto L8015E36C;
        L8015E304: ;
        if (c.V0 == 0u) {
            c.V0 = c.S0 & 0x2000u;
            goto L8015E338;
        }
        c.V0 = c.S0 & 0x2000u;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2EECu));
        c.V0 = c.V0 & 0x0080u;
        if (c.V0 == 0u) {
            c.V0 = c.S0 & 0x2000u;
            goto L8015E338;
        }
        c.V0 = c.S0 & 0x2000u;
        c.RA = 0x8015E32Cu;
        SoTN_ric.RicDoAttack(c, m);
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0001u;
            goto L8015E36C;
        }
        c.V0 = 0u | 0x0001u;
        c.V0 = c.S0 & 0x2000u;
        L8015E338: ;
        if (c.V0 == 0u) {
            c.V0 = 0u;
            goto L8015E36C;
        }
        c.V0 = 0u;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2EE8u));
        c.V0 = c.V0 & 0x4000u;
        if (c.V0 == 0u) {
            c.A0 = 0u | 0x0002u;
            goto L8015E368;
        }
        c.A0 = 0u | 0x0002u;
        c.A1 = 0u;
        c.RA = 0x8015E360u;
        SoTN_ric.RicSetCrouch(c, m);
        c.V0 = 0u | 0x0001u;
        goto L8015E36C;
        L8015E368: ;
        c.V0 = 0u;
        L8015E36C: ;
        c.RA = mem.ReadU32((c.SP + 0x14u));
        c.S0 = mem.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x18u;
        return;
    }

    static void RicEntitySubwpnCrossEdge(CpuContext c, IMemory m)
    {
        var mem = (PSMemory)m;
        c.SP = c.SP - 0x18u;
        mem.WriteU32((c.SP + 0x10u), c.S0);
        c.S0 = c.A0;
        mem.WriteU32((c.SP + 0x14u), c.RA);
        c.V1 = mem.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V1 < 0x00000008u ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = c.V1 << 2;
            goto L80169A98;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x80150000u;
        c.At = c.At + c.V0;
        c.V0 = mem.ReadU32((c.At + 0x6B04u));
        switch (c.V0)
        {
            case 0x80169740u: goto L80169740;
            case 0x80169818u: goto L80169818;
            case 0x8016983Cu: goto L8016983C;
            case 0x80169898u: goto L80169898;
            case 0x80169900u: goto L80169900;
            case 0x80169950u: goto L80169950;
            case 0x80169A98u: goto L80169A98;
            case 0x80169A54u: goto L80169A54;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L80169740: ;
        c.A0 = c.S0;
        c.A1 = 0u | 0x0005u;
        c.V0 = 0x0C100000u;
        c.A3 = 0x80170000u;
        c.A3 = mem.ReadU32((c.A3 + 0x5888u));
        c.V1 = 0x80170000u;
        c.V1 = c.V1 + 0x5088u;
        mem.WriteU32((c.S0 + 0x34u), c.V0);
        c.V0 = c.A3 << 9;
        c.V0 = c.V0 + c.V1;
        c.A3 = c.A3 + 0x1u;
        c.A3 = c.A3 & 0x0003u;
        mem.WriteU32((c.S0 + 0x84u), c.V0);
        c.At = 0x80170000u;
        mem.WriteU32((c.At + 0x5888u), c.A3);
        c.A2 = 0u;
        c.RA = 0x80169784u;
        SoTN_ric.RicCreateEntFactoryFromEntity(c, m);
        c.V0 = 0xFFFF8011u;
        mem.WriteU16((c.S0 + 0x54u), (ushort)c.V0);
        c.V0 = 0u | 0x0066u;
        mem.WriteU16((c.S0 + 0x5Au), (ushort)c.V0);
        c.V0 = 0x80150000u;
        c.V0 = c.V0 + 0x5E44u;
        mem.WriteU32((c.S0 + 0x4Cu), c.V0);
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x33ECu));
        c.A0 = 0x00030000u;
        mem.WriteU16((c.S0 + 0x14u), (ushort)c.V0);
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x33FCu));
        c.A0 = c.A0 | 0x9000u;
        mem.WriteU16((c.S0 + 0x24u), (ushort)c.V0);
        c.RA = 0x801697C4u;
        SoTN_ric.RicSetSpeedX(c, m);
        c.A0 = c.S0;
        c.V0 = 0u | 0x0004u;
        mem.WriteU8((c.S0 + 0x19u), (byte)c.V0);
        c.V0 = 0u | 0x0C00u;
        mem.WriteU16((c.S0 + 0x1Eu), (ushort)c.V0);
        c.V0 = 0u | 0x0004u;
        mem.WriteU16((c.S0 + 0xB0u), (ushort)c.V0);
        c.RA = 0x801697E4u;
        SoTN_ric.RicSetSubweaponParams(c, m);
        c.V0 = mem.ReadU16((c.S0 + 0x6u));
        c.V1 = 0u | 0x0008u;
        mem.WriteU8((c.S0 + 0x46u), (byte)c.V1);
        mem.WriteU8((c.S0 + 0x47u), (byte)c.V1);
        c.V0 = c.V0 - 0x8u;
        mem.WriteU16((c.S0 + 0x6u), (ushort)c.V0);
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x069Fu;
        c.RA = 0x80169810u;
        Dispatcher.Call(c, m, c.V0);
        goto L801698EC;
        L80169818: ;
        c.V1 = 0x80070000u;
        c.V1 = mem.ReadU16((c.V1 + 0x3428u));
        c.V0 = 0u | 0x0001u;
        if (c.V1 != c.V0) {
            c.A1 = 0u | 0x1000u;
            goto L80169840;
        }
        c.A1 = 0u | 0x1000u;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        L8016983C: ;
        c.A1 = 0u | 0x1000u;
        L80169840: ;
        c.V0 = mem.ReadU16((c.S0 + 0x1Eu));
        c.V1 = mem.ReadU32((c.S0 + 0x8u));
        c.V0 = c.V0 - 0x80u;
        mem.WriteU16((c.S0 + 0x1Eu), (ushort)c.V0);
        c.V0 = mem.ReadU32(c.S0);
        c.A0 = mem.ReadU16((c.S0 + 0x14u));
        c.V0 = c.V0 + c.V1;
        if (c.A0 == 0u) {
            mem.WriteU32(c.S0, c.V0);
            goto L80169868;
        }
        mem.WriteU32(c.S0, c.V0);
        c.A1 = 0xFFFFF000u;
        L80169868: ;
        c.V1 = mem.ReadU32((c.S0 + 0x8u));
        c.V0 = 0u | 0xBFFFu;
        c.V1 = c.V1 - c.A1;
        mem.WriteU32((c.S0 + 0x8u), c.V1);
        if ((int)c.V1 >= 0) {
            goto L80169884;
        }
        c.V1 = 0u - c.V1;
        L80169884: ;
        c.V0 = (int)c.V0 < (int)c.V1 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801698EC;
        }
        goto L80169A98;
        L80169898: ;
        c.A1 = 0u | 0x1000u;
        c.V0 = mem.ReadU16((c.S0 + 0x1Eu));
        c.V1 = mem.ReadU32((c.S0 + 0x8u));
        c.V0 = c.V0 - 0x100u;
        mem.WriteU16((c.S0 + 0x1Eu), (ushort)c.V0);
        c.V0 = mem.ReadU32(c.S0);
        c.A0 = mem.ReadU16((c.S0 + 0x14u));
        c.V0 = c.V0 + c.V1;
        if (c.A0 == 0u) {
            mem.WriteU32(c.S0, c.V0);
            goto L801698C4;
        }
        mem.WriteU32(c.S0, c.V0);
        c.A1 = 0xFFFFF000u;
        L801698C4: ;
        c.V1 = mem.ReadU32((c.S0 + 0x8u));
        c.V0 = 0u | 0xC000u;
        c.V1 = c.V1 - c.A1;
        mem.WriteU32((c.S0 + 0x8u), c.V1);
        if ((int)c.V1 >= 0) {
            goto L801698E0;
        }
        c.V1 = 0u - c.V1;
        L801698E0: ;
        c.V0 = (int)c.V0 < (int)c.V1 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L80169A98;
        }
        L801698EC: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        goto L80169A98;
        L80169900: ;
        c.V0 = mem.ReadU16((c.S0 + 0x14u));
        if (c.V0 == 0u) {
            c.A1 = 0u | 0x1000u;
            goto L80169914;
        }
        c.A1 = 0u | 0x1000u;
        c.A1 = 0xFFFFF000u;
        L80169914: ;
        c.V1 = 0x00020000u;
        c.V0 = mem.ReadU32((c.S0 + 0x8u));
        c.V1 = c.V1 | 0x8000u;
        c.V0 = c.V0 - c.A1;
        mem.WriteU32((c.S0 + 0x8u), c.V0);
        if ((int)c.V0 >= 0) {
            goto L80169934;
        }
        c.V0 = 0u - c.V0;
        L80169934: ;
        c.V1 = (int)c.V1 < (int)c.V0 ? 1u : 0u;
        if (c.V1 == 0u) {
            goto L80169950;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        L80169950: ;
        c.V1 = 0x80070000u;
        c.V1 = mem.ReadU16((c.V1 + 0x33DAu));
        c.A0 = 0x80070000u;
        c.A0 = mem.ReadU16((c.A0 + 0x33E8u));
        c.A1 = 0x80070000u;
        c.A1 = mem.ReadU16((c.A1 + 0x33DEu));
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x33EAu));
        c.V1 = c.V1 + c.A0;
        c.A1 = c.A1 + c.V0;
        c.V1 = c.V1 << 16;
        c.V1 = (uint)((int)c.V1 >> 16);
        c.V0 = (uint)(short)mem.ReadU16((c.S0 + 0x2u));
        c.A0 = mem.ReadU8((c.S0 + 0x46u));
        c.V0 = c.V0 - c.V1;
        c.V1 = 0x80070000u;
        c.V1 = mem.ReadU8((c.V1 + 0x341Eu));
        if ((int)c.V0 >= 0) {
            goto L801699A0;
        }
        c.V0 = 0u - c.V0;
        L801699A0: ;
        c.V1 = c.V1 + c.A0;
        c.V0 = (int)c.V0 < (int)c.V1 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V1 = c.A1 << 16;
            goto L801699E4;
        }
        c.V1 = c.A1 << 16;
        c.V1 = (uint)((int)c.V1 >> 16);
        c.V0 = (uint)(short)mem.ReadU16((c.S0 + 0x6u));
        c.A0 = mem.ReadU8((c.S0 + 0x47u));
        c.V0 = c.V0 - c.V1;
        c.V1 = 0x80070000u;
        c.V1 = mem.ReadU8((c.V1 + 0x341Fu));
        if ((int)c.V0 >= 0) {
            goto L801699D4;
        }
        c.V0 = 0u - c.V0;
        L801699D4: ;
        c.V1 = c.V1 + c.A0;
        c.V0 = (int)c.V0 < (int)c.V1 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0007u;
            goto L80169A24;
        }
        c.V0 = 0u | 0x0007u;
        L801699E4: ;
        c.V0 = mem.ReadU16((c.S0 + 0x14u));
        if (c.V0 != 0u) {
            goto L80169A10;
        }
        c.V0 = (uint)(short)mem.ReadU16((c.S0 + 0x2u));
        c.V0 = (int)c.V0 < OutsetLeft(-32) ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0007u;
            goto L80169A24;
        }
        c.V0 = 0u | 0x0007u;
        goto L80169A34;
        L80169A10: ;
        c.V0 = (uint)(short)mem.ReadU16((c.S0 + 0x2u));
        c.V0 = (int)c.V0 < OutsetRight(289) ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0007u;
            goto L80169A34;
        }
        c.V0 = 0u | 0x0007u;
        L80169A24: ;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        c.V0 = 0u | 0x0020u;
        mem.WriteU16((c.S0 + 0x7Cu), (ushort)c.V0);
        goto L80169BFC;
        L80169A34: ;
        c.V0 = mem.ReadU16((c.S0 + 0x1Eu));
        c.V1 = mem.ReadU32(c.S0);
        c.A0 = mem.ReadU32((c.S0 + 0x8u));
        c.V0 = c.V0 - 0x80u;
        c.V1 = c.V1 + c.A0;
        mem.WriteU16((c.S0 + 0x1Eu), (ushort)c.V0);
        mem.WriteU32(c.S0, c.V1);
        goto L80169A98;
        L80169A54: ;
        c.V0 = mem.ReadU16((c.S0 + 0x7Cu));
        c.V0 = c.V0 - 0x1u;
        mem.WriteU16((c.S0 + 0x7Cu), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        if (c.V0 != 0u) {
            goto L80169A80;
        }
        c.A0 = c.S0;
        c.RA = 0x80169A78u;
        SoTN_ric.DestroyEntity_ric(c, m);
        goto L80169BFC;
        L80169A80: ;
        c.V0 = mem.ReadU32(c.S0);
        c.V1 = mem.ReadU32((c.S0 + 0x8u));
        mem.WriteU16((c.S0 + 0x3Cu), (ushort)0u);
        mem.WriteU16((c.S0 + 0x54u), (ushort)0u);
        c.V0 = c.V0 + c.V1;
        mem.WriteU32(c.S0, c.V0);
        L80169A98: ;
        c.V1 = mem.ReadU16((c.S0 + 0x7Eu));
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu));
        c.V1 = c.V1 + 0x1u;
        c.V0 = c.V0 - 0x2u;
        c.V0 = c.V0 < 0x00000004u ? 1u : 0u;
        if (c.V0 == 0u) {
            mem.WriteU16((c.S0 + 0x7Eu), (ushort)c.V1);
            goto L80169B7C;
        }
        mem.WriteU16((c.S0 + 0x7Eu), (ushort)c.V1);
        c.V1 = c.V1 & 0x000Fu;
        c.V0 = 0u | 0x0001u;
        if (c.V1 != c.V0) {
            c.A0 = c.S0;
            goto L80169AD0;
        }
        c.A0 = c.S0;
        c.A1 = 0u | 0x0003u;
        c.A2 = 0u;
        c.RA = 0x80169AD0u;
        SoTN_ric.RicCreateEntFactoryFromEntity(c, m);
        L80169AD0: ;
        c.V0 = mem.ReadU16((c.S0 + 0x7Eu));
        c.V1 = 0u | 0x0004u;
        c.V0 = c.V0 & 0x000Fu;
        if (c.V0 != c.V1) {
            c.A0 = c.S0;
            goto L80169AF4;
        }
        c.A0 = c.S0;
        c.A1 = 0x00060000u;
        c.A1 = c.A1 | 0x0004u;
        c.A2 = 0u;
        c.RA = 0x80169AF4u;
        SoTN_ric.RicCreateEntFactoryFromEntity(c, m);
        L80169AF4: ;
        c.V0 = mem.ReadU16((c.S0 + 0x7Eu));
        c.V1 = 0u | 0x0006u;
        c.V0 = c.V0 & 0x000Fu;
        if (c.V0 != c.V1) {
            c.A0 = c.S0;
            goto L80169B14;
        }
        c.A0 = c.S0;
        c.A1 = 0u | 0x0003u;
        c.A2 = 0u;
        c.RA = 0x80169B14u;
        SoTN_ric.RicCreateEntFactoryFromEntity(c, m);
        L80169B14: ;
        c.V0 = mem.ReadU16((c.S0 + 0x7Eu));
        c.V1 = 0u | 0x0008u;
        c.V0 = c.V0 & 0x000Fu;
        if (c.V0 != c.V1) {
            c.A0 = c.S0;
            goto L80169B38;
        }
        c.A0 = c.S0;
        c.A1 = 0x00060000u;
        c.A1 = c.A1 | 0x0004u;
        c.A2 = 0u;
        c.RA = 0x80169B38u;
        SoTN_ric.RicCreateEntFactoryFromEntity(c, m);
        L80169B38: ;
        c.V0 = mem.ReadU16((c.S0 + 0x7Eu));
        c.V1 = 0u | 0x000Cu;
        c.V0 = c.V0 & 0x000Fu;
        if (c.V0 != c.V1) {
            c.A0 = c.S0;
            goto L80169B5C;
        }
        c.A0 = c.S0;
        c.A1 = 0x00060000u;
        c.A1 = c.A1 | 0x0004u;
        c.A2 = 0u;
        c.RA = 0x80169B5Cu;
        SoTN_ric.RicCreateEntFactoryFromEntity(c, m);
        L80169B5C: ;
        c.V0 = mem.ReadU16((c.S0 + 0x7Eu));
        c.V1 = 0u | 0x000Bu;
        c.V0 = c.V0 & 0x000Fu;
        if (c.V0 != c.V1) {
            c.A0 = c.S0;
            goto L80169B7C;
        }
        c.A0 = c.S0;
        c.A1 = 0u | 0x0003u;
        c.A2 = 0u;
        c.RA = 0x80169B7Cu;
        SoTN_ric.RicCreateEntFactoryFromEntity(c, m);
        L80169B7C: ;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x373Cu));
        c.V0 = c.V0 >> 1;
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x81B0u;
            goto L80169B9C;
        }
        c.V0 = 0u | 0x81B0u;
        c.V0 = 0u | 0x81B1u;
        L80169B9C: ;
        mem.WriteU16((c.S0 + 0x16u), (ushort)c.V0);
        c.V1 = mem.ReadU16((c.S0 + 0x80u));
        c.A0 = mem.ReadU32((c.S0 + 0x84u));
        c.V0 = mem.ReadU16((c.S0 + 0x2u));
        c.A1 = 0x80070000u;
        c.A1 = mem.ReadU16((c.A1 + 0x308Eu));
        c.V1 = c.V1 << 2;
        c.A0 = c.A0 + c.V1;
        c.V0 = c.V0 + c.A1;
        mem.WriteU16(c.A0, (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x6u));
        c.V1 = 0x80070000u;
        c.V1 = mem.ReadU16((c.V1 + 0x3092u));
        c.V0 = c.V0 + c.V1;
        mem.WriteU16((c.A0 + 0x2u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x80u));
        c.V0 = c.V0 + 0x1u;
        c.V0 = c.V0 & 0x003Fu;
        mem.WriteU16((c.S0 + 0x80u), (ushort)c.V0);
        c.V0 = 0u | 0x0002u;
        c.At = 0x80070000u;
        mem.WriteU16((c.At + 0x2F06u), (ushort)c.V0);
        L80169BFC: ;
        c.RA = mem.ReadU32((c.SP + 0x14u));
        c.S0 = mem.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x18u;
        return;
    }

    static void RicEntitySubwpnReboundStoneEdge(CpuContext c, IMemory m)
    {
        var mem = (PSMemory)m;
        c.SP = c.SP - 0x78u;
        mem.WriteU32((c.SP + 0x54u), c.S1);
        c.S1 = c.A0;
        mem.WriteU32((c.SP + 0x74u), c.RA);
        mem.WriteU32((c.SP + 0x70u), c.FP);
        mem.WriteU32((c.SP + 0x6Cu), c.S7);
        mem.WriteU32((c.SP + 0x68u), c.S6);
        mem.WriteU32((c.SP + 0x64u), c.S5);
        mem.WriteU32((c.SP + 0x60u), c.S4);
        mem.WriteU32((c.SP + 0x5Cu), c.S3);
        mem.WriteU32((c.SP + 0x58u), c.S2);
        mem.WriteU32((c.SP + 0x50u), c.S0);
        c.S7 = mem.ReadU16((c.S1 + 0x6u));
        c.V1 = mem.ReadU16((c.S1 + 0x2Cu));
        c.T1 = mem.ReadU16((c.S1 + 0x2u));
        c.V0 = 0u | 0x0001u;
        mem.WriteU16((c.S1 + 0x82u), (ushort)0u);
        if (c.V1 == c.V0) {
            mem.WriteU16((c.SP + 0x38u), (ushort)c.T1);
            goto L8016BBA8;
        }
        mem.WriteU16((c.SP + 0x38u), (ushort)c.T1);
        c.V0 = (int)c.V1 < 2 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L8016B9E4;
        }
        if (c.V1 == 0u) {
            c.A0 = 0u | 0x0002u;
            goto L8016B9F8;
        }
        c.A0 = 0u | 0x0002u;
        c.S2 = 0u;
        goto L8016C06C;
        L8016B9E4: ;
        c.V0 = 0u | 0x0002u;
        if (c.V1 == c.V0) {
            c.S2 = 0u;
            goto L8016BFEC;
        }
        c.S2 = 0u;
        goto L8016C06C;
        L8016B9F8: ;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0010u;
        c.RA = 0x8016BA0Cu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V1 = 0xFFFFFFFFu;
        if (c.V0 == c.V1) {
            mem.WriteU32((c.S1 + 0x64u), c.V0);
            goto L8016C00C;
        }
        mem.WriteU32((c.S1 + 0x64u), c.V0);
        c.S2 = 0u;
        c.A0 = mem.ReadU16((c.S1 + 0x6u));
        c.V1 = mem.ReadU32((c.S1 + 0x64u));
        c.A0 = c.A0 - 0x10u;
        c.S7 = c.A0;
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.V1 = c.V0 + c.V1;
        if (c.V1 == 0u) {
            mem.WriteU16((c.S1 + 0x6u), (ushort)c.A0);
            goto L8016BACC;
        }
        mem.WriteU16((c.S1 + 0x6u), (ushort)c.A0);
        c.A0 = 0u | 0x00FFu;
        c.T0 = 0x80070000u;
        c.T0 = c.T0 + 0x33FCu;
        c.A3 = 0u | 0x0033u;
        c.A2 = 0u | 0x003Bu;
        c.A1 = 0u | 0x0014u;
        L8016BA74: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU8((c.V1 + 0x12u), (byte)c.A0);
        mem.WriteU8((c.V1 + 0x11u), (byte)c.A0);
        mem.WriteU8((c.V1 + 0x10u), (byte)c.A0);
        mem.WriteU8((c.V1 + 0x6u), (byte)c.A0);
        mem.WriteU8((c.V1 + 0x5u), (byte)c.A0);
        mem.WriteU8((c.V1 + 0x4u), (byte)c.A0);
        c.V0 = mem.ReadU16(c.T0);
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A3);
        c.V0 = c.V0 + 0x2u;
        if (c.S2 == 0u) {
            mem.WriteU16((c.V1 + 0x26u), (ushort)c.V0);
            goto L8016BAA4;
        }
        mem.WriteU16((c.V1 + 0x26u), (ushort)c.V0);
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A2);
        L8016BAA4: ;
        c.T1 = mem.ReadU16((c.SP + 0x38u));
        mem.WriteU16((c.V1 + 0x16u), (ushort)c.S7);
        mem.WriteU16((c.V1 + 0xAu), (ushort)c.S7);
        mem.WriteU16((c.V1 + 0x30u), (ushort)c.A1);
        mem.WriteU16((c.V1 + 0x14u), (ushort)c.T1);
        mem.WriteU16((c.V1 + 0x8u), (ushort)c.T1);
        c.V1 = mem.ReadU32(c.V1);
        if (c.V1 != 0u) {
            c.S2 = c.S2 + 0x1u;
            goto L8016BA74;
        }
        c.S2 = c.S2 + 0x1u;
        L8016BACC: ;
        c.V0 = 0x0C800000u;
        mem.WriteU32((c.S1 + 0x34u), c.V0);
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x33FCu));
        c.V0 = c.V0 + 0x2u;
        mem.WriteU16((c.S1 + 0x24u), (ushort)c.V0);
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x33ECu));
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0980u;
            goto L8016BB00;
        }
        c.V0 = 0u | 0x0980u;
        c.V0 = 0u | 0x0E80u;
        L8016BB00: ;
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)c.V0);
        c.RA = 0x8016BB08u;
        SoTN_main.rand(c, m);
        c.A0 = c.S1;
        c.V1 = 0u | 0x0040u;
        c.A1 = 0u | 0x0007u;
        mem.WriteU16((c.S1 + 0x7Eu), (ushort)c.V1);
        c.V1 = mem.ReadU16((c.S1 + 0x7Cu));
        c.V0 = c.V0 & 0x007Fu;
        mem.WriteU16((c.S1 + 0xB0u), (ushort)c.A1);
        c.V1 = c.V1 - 0x40u;
        c.V1 = c.V1 + c.V0;
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)c.V1);
        c.RA = 0x8016BB34u;
        SoTN_ric.RicSetSubweaponParams(c, m);
        c.A2 = c.SP + 0x10u;
        c.A0 = (uint)(short)mem.ReadU16((c.S1 + 0x2u));
        c.A1 = (uint)(short)mem.ReadU16((c.S1 + 0x6u));
        c.V0 = 0u | 0x0004u;
        mem.WriteU8((c.S1 + 0x46u), (byte)c.V0);
        mem.WriteU8((c.S1 + 0x47u), (byte)c.V0);
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3844u));
        c.A3 = 0u;
        c.RA = 0x8016BB60u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = mem.ReadU32((c.SP + 0x10u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u) {
            c.S2 = 0u;
            goto L8016BB7C;
        }
        c.S2 = 0u;
        c.V0 = 0u | 0x0004u;
        mem.WriteU16((c.S1 + 0x84u), (ushort)c.V0);
        L8016BB7C: ;
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x060Cu;
        c.RA = 0x8016BBA0u;
        Dispatcher.Call(c, m, c.V0);
        c.S0 = 0u | 0x0002u;
        goto L8016C070;
        L8016BBA8: ;
        c.A0 = (uint)(short)mem.ReadU16((c.S1 + 0x7Cu));
        c.RA = 0x8016BBB4u;
        SoTN_main.rcos(c, m);
        c.A0 = (uint)(short)mem.ReadU16((c.S1 + 0x7Cu));
        c.S4 = c.V0 << 4;
        c.RA = 0x8016BBC0u;
        SoTN_main.rsin(c, m);
        c.V0 = 0u - c.V0;
        c.S3 = c.V0 << 4;
        c.S6 = mem.ReadU32(c.S1);
        c.V0 = (uint)(short)mem.ReadU16((c.S1 + 0x84u));
        c.S5 = mem.ReadU32((c.S1 + 0x4u));
        if (c.V0 != 0u) {
            c.V1 = c.V0;
            goto L8016BEF0;
        }
        c.V1 = c.V0;
        c.S2 = 0u;
        c.FP = 0u | 0x0800u;
        L8016BBE4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A0 = (uint)((int)c.S6 >> 16);
        c.A1 = c.S5 + c.S3;
        c.A1 = (uint)((int)c.A1 >> 16);
        c.A2 = c.SP + 0x10u;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3844u));
        c.A3 = 0u;
        c.RA = 0x8016BC08u;
        Dispatcher.Call(c, m, c.V0);
        c.V1 = mem.ReadU32((c.SP + 0x10u));
        c.V0 = c.V1 & 0x0001u;
        if (c.V0 == 0u) {
            c.A0 = c.S6 + c.S4;
            goto L8016BD4C;
        }
        c.A0 = c.S6 + c.S4;
        if ((int)c.S3 <= 0) {
            c.S0 = c.V1 & 0xF800u;
            goto L8016BCB4;
        }
        c.S0 = c.V1 & 0xF800u;
        if (c.S0 == 0u) {
            c.V0 = c.V1 & 0x0800u;
            goto L8016BC34;
        }
        c.V0 = c.V1 & 0x0800u;
        if (c.V0 == 0u) {
            c.T1 = 0u | 0x8000u;
            goto L8016BC40;
        }
        c.T1 = 0u | 0x8000u;
        L8016BC34: ;
        c.A0 = 0u | 0x0800u;
        c.RA = 0x8016BC3Cu;
        SoTN_ric.ReboundStoneBounce1_ric(c, m);
        c.T1 = 0u | 0x8000u;
        L8016BC40: ;
        if (c.S0 != c.T1) {
            c.V0 = 0u | 0x9000u;
            goto L8016BC54;
        }
        c.V0 = 0u | 0x9000u;
        c.A0 = 0u | 0x0200u;
        c.RA = 0x8016BC50u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.V0 = 0u | 0x9000u;
        L8016BC54: ;
        if (c.S0 != c.V0) {
            c.V0 = 0u | 0xA000u;
            goto L8016BC68;
        }
        c.V0 = 0u | 0xA000u;
        c.A0 = 0u | 0x012Eu;
        c.RA = 0x8016BC64u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.V0 = 0u | 0xA000u;
        L8016BC68: ;
        if (c.S0 != c.V0) {
            c.T1 = 0u | 0xC000u;
            goto L8016BC7C;
        }
        c.T1 = 0u | 0xC000u;
        c.A0 = 0u | 0x00A0u;
        c.RA = 0x8016BC78u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.T1 = 0u | 0xC000u;
        L8016BC7C: ;
        if (c.S0 != c.T1) {
            c.V0 = 0u | 0xD000u;
            goto L8016BC90;
        }
        c.V0 = 0u | 0xD000u;
        c.A0 = 0u | 0x0600u;
        c.RA = 0x8016BC8Cu;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.V0 = 0u | 0xD000u;
        L8016BC90: ;
        if (c.S0 != c.V0) {
            c.V0 = 0u | 0xE000u;
            goto L8016BCA4;
        }
        c.V0 = 0u | 0xE000u;
        c.A0 = 0u | 0x06D2u;
        c.RA = 0x8016BCA0u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.V0 = 0u | 0xE000u;
        L8016BCA4: ;
        if (c.S0 != c.V0) {
            goto L8016BCB4;
        }
        c.A0 = 0u | 0x0760u;
        c.RA = 0x8016BCB4u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        L8016BCB4: ;
        if ((int)c.S3 >= 0) {
            goto L8016BD48;
        }
        if (c.S0 == 0u) {
            c.V0 = c.S0 & 0x8000u;
            goto L8016BCCC;
        }
        c.V0 = c.S0 & 0x8000u;
        if (c.V0 == 0u) {
            goto L8016BCD4;
        }
        L8016BCCC: ;
        c.A0 = 0u | 0x0800u;
        c.RA = 0x8016BCD4u;
        SoTN_ric.ReboundStoneBounce1_ric(c, m);
        L8016BCD4: ;
        if (c.S0 != c.FP) {
            c.V0 = 0u | 0x1800u;
            goto L8016BCE8;
        }
        c.V0 = 0u | 0x1800u;
        c.A0 = 0u | 0x0E00u;
        c.RA = 0x8016BCE4u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.V0 = 0u | 0x1800u;
        L8016BCE8: ;
        if (c.S0 != c.V0) {
            c.V0 = 0u | 0x2800u;
            goto L8016BCFC;
        }
        c.V0 = 0u | 0x2800u;
        c.A0 = 0u | 0x0ED2u;
        c.RA = 0x8016BCF8u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.V0 = 0u | 0x2800u;
        L8016BCFC: ;
        if (c.S0 != c.V0) {
            c.V0 = 0u | 0x4800u;
            goto L8016BD10;
        }
        c.V0 = 0u | 0x4800u;
        c.A0 = 0u | 0x0F60u;
        c.RA = 0x8016BD0Cu;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.V0 = 0u | 0x4800u;
        L8016BD10: ;
        if (c.S0 != c.V0) {
            c.V0 = 0u | 0x5800u;
            goto L8016BD24;
        }
        c.V0 = 0u | 0x5800u;
        c.A0 = 0u | 0x0A00u;
        c.RA = 0x8016BD20u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.V0 = 0u | 0x5800u;
        L8016BD24: ;
        if (c.S0 != c.V0) {
            c.V0 = 0u | 0x6800u;
            goto L8016BD38;
        }
        c.V0 = 0u | 0x6800u;
        c.A0 = 0u | 0x092Eu;
        c.RA = 0x8016BD34u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.V0 = 0u | 0x6800u;
        L8016BD38: ;
        if (c.S0 != c.V0) {
            c.A0 = c.S6 + c.S4;
            goto L8016BD4C;
        }
        c.A0 = c.S6 + c.S4;
        c.A0 = 0u | 0x08A0u;
        c.RA = 0x8016BD48u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        L8016BD48: ;
        c.A0 = c.S6 + c.S4;
        L8016BD4C: ;
        c.A0 = (uint)((int)c.A0 >> 16);
        c.A1 = (uint)((int)c.S5 >> 16);
        c.A2 = c.SP + 0x10u;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3844u));
        c.A3 = 0u;
        c.RA = 0x8016BD6Cu;
        Dispatcher.Call(c, m, c.V0);
        c.A0 = mem.ReadU32((c.SP + 0x10u));
        c.V0 = c.A0 & 0x0001u;
        if (c.V0 == 0u) {
            goto L8016BEC8;
        }
        if ((int)c.S4 <= 0) {
            c.S0 = c.A0 & 0xF800u;
            goto L8016BE24;
        }
        c.S0 = c.A0 & 0xF800u;
        if (c.S0 == 0u) {
            c.V1 = c.A0 & 0x4800u;
            goto L8016BDA8;
        }
        c.V1 = c.A0 & 0x4800u;
        c.V0 = 0u | 0x4800u;
        if (c.V1 == c.V0) {
            c.V0 = c.A0 & 0xC000u;
            goto L8016BDA8;
        }
        c.V0 = c.A0 & 0xC000u;
        c.T1 = 0u | 0xC000u;
        if (c.V0 != c.T1) {
            goto L8016BDB0;
        }
        L8016BDA8: ;
        c.A0 = 0u | 0x0400u;
        c.RA = 0x8016BDB0u;
        SoTN_ric.ReboundStoneBounce1_ric(c, m);
        L8016BDB0: ;
        if (c.S0 != c.FP) {
            c.V0 = 0u | 0x1800u;
            goto L8016BDC4;
        }
        c.V0 = 0u | 0x1800u;
        c.A0 = 0u | 0x0E00u;
        c.RA = 0x8016BDC0u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.V0 = 0u | 0x1800u;
        L8016BDC4: ;
        if (c.S0 != c.V0) {
            c.V0 = 0u | 0x2800u;
            goto L8016BDD8;
        }
        c.V0 = 0u | 0x2800u;
        c.A0 = 0u | 0x0ED2u;
        c.RA = 0x8016BDD4u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.V0 = 0u | 0x2800u;
        L8016BDD8: ;
        if (c.S0 != c.V0) {
            c.T1 = 0u | 0x8000u;
            goto L8016BDEC;
        }
        c.T1 = 0u | 0x8000u;
        c.A0 = 0u | 0x0F60u;
        c.RA = 0x8016BDE8u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.T1 = 0u | 0x8000u;
        L8016BDEC: ;
        if (c.S0 != c.T1) {
            c.V0 = 0u | 0x9000u;
            goto L8016BE00;
        }
        c.V0 = 0u | 0x9000u;
        c.A0 = 0u | 0x0200u;
        c.RA = 0x8016BDFCu;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.V0 = 0u | 0x9000u;
        L8016BE00: ;
        if (c.S0 != c.V0) {
            c.V0 = 0u | 0xA000u;
            goto L8016BE14;
        }
        c.V0 = 0u | 0xA000u;
        c.A0 = 0u | 0x012Eu;
        c.RA = 0x8016BE10u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.V0 = 0u | 0xA000u;
        L8016BE14: ;
        if (c.S0 != c.V0) {
            goto L8016BE24;
        }
        c.A0 = 0u | 0x00A0u;
        c.RA = 0x8016BE24u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        L8016BE24: ;
        if ((int)c.S4 >= 0) {
            goto L8016BEC8;
        }
        if (c.S0 == 0u) {
            c.V0 = c.S0 & 0x4800u;
            goto L8016BE48;
        }
        c.V0 = c.S0 & 0x4800u;
        if (c.V0 == c.FP) {
            c.V0 = c.S0 & 0xC000u;
            goto L8016BE48;
        }
        c.V0 = c.S0 & 0xC000u;
        c.T1 = 0u | 0x8000u;
        if (c.V0 != c.T1) {
            c.V0 = 0u | 0x4800u;
            goto L8016BE54;
        }
        c.V0 = 0u | 0x4800u;
        L8016BE48: ;
        c.A0 = 0u | 0x0400u;
        c.RA = 0x8016BE50u;
        SoTN_ric.ReboundStoneBounce1_ric(c, m);
        c.V0 = 0u | 0x4800u;
        L8016BE54: ;
        if (c.S0 != c.V0) {
            c.V0 = 0u | 0x5800u;
            goto L8016BE68;
        }
        c.V0 = 0u | 0x5800u;
        c.A0 = 0u | 0x0A00u;
        c.RA = 0x8016BE64u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.V0 = 0u | 0x5800u;
        L8016BE68: ;
        if (c.S0 != c.V0) {
            c.V0 = 0u | 0x6800u;
            goto L8016BE7C;
        }
        c.V0 = 0u | 0x6800u;
        c.A0 = 0u | 0x092Eu;
        c.RA = 0x8016BE78u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.V0 = 0u | 0x6800u;
        L8016BE7C: ;
        if (c.S0 != c.V0) {
            c.T1 = 0u | 0xC000u;
            goto L8016BE90;
        }
        c.T1 = 0u | 0xC000u;
        c.A0 = 0u | 0x08A0u;
        c.RA = 0x8016BE8Cu;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.T1 = 0u | 0xC000u;
        L8016BE90: ;
        if (c.S0 != c.T1) {
            c.V0 = 0u | 0xD000u;
            goto L8016BEA4;
        }
        c.V0 = 0u | 0xD000u;
        c.A0 = 0u | 0x0600u;
        c.RA = 0x8016BEA0u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.V0 = 0u | 0xD000u;
        L8016BEA4: ;
        if (c.S0 != c.V0) {
            c.V0 = 0u | 0xE000u;
            goto L8016BEB8;
        }
        c.V0 = 0u | 0xE000u;
        c.A0 = 0u | 0x06D2u;
        c.RA = 0x8016BEB4u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        c.V0 = 0u | 0xE000u;
        L8016BEB8: ;
        if (c.S0 != c.V0) {
            goto L8016BEC8;
        }
        c.A0 = 0u | 0x0760u;
        c.RA = 0x8016BEC8u;
        SoTN_ric.ReboundStoneBounce2_ric(c, m);
        L8016BEC8: ;
        c.V0 = (uint)(short)mem.ReadU16((c.S1 + 0x82u));
        if (c.V0 != 0u) {
            c.S6 = c.S6 + c.S4;
            goto L8016BF08;
        }
        c.S6 = c.S6 + c.S4;
        c.S2 = c.S2 + 0x1u;
        c.V0 = (int)c.S2 < 6 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.S5 = c.S5 + c.S3;
            goto L8016BBE4;
        }
        c.S5 = c.S5 + c.S3;
        goto L8016BEF8;
        L8016BEF0: ;
        c.V0 = c.V1 - 0x1u;
        mem.WriteU16((c.S1 + 0x84u), (ushort)c.V0);
        L8016BEF8: ;
        c.V0 = (uint)(short)mem.ReadU16((c.S1 + 0x82u));
        if (c.V0 == 0u) {
            goto L8016BF3C;
        }
        L8016BF08: ;
        c.A0 = c.S1;
        c.A1 = 0x00020000u;
        c.A1 = c.A1 | 0x002Au;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x380Cu));
        c.A2 = 0u;
        c.RA = 0x8016BF28u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x06A4u;
        c.RA = 0x8016BF3Cu;
        Dispatcher.Call(c, m, c.V0);
        L8016BF3C: ;
        c.V0 = mem.ReadU16((c.S1 + 0x2u));
        c.V0 = c.V0 + 0x40u + (uint)Margin;
        c.V0 = c.V0 & 0xFFFFu;
        c.V0 = c.V0 < 0x00000181u + (uint)(Margin * 2) ? 1u : 0u;
        if (c.V0 == 0u) {
            c.S2 = 0u;
            goto L8016BF84;
        }
        c.S2 = 0u;
        c.V0 = mem.ReadU16((c.S1 + 0x6u));
        c.V0 = c.V0 + 0x40u;
        c.V0 = c.V0 & 0xFFFFu;
        c.V0 = c.V0 < 0x00000181u ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = 0u | 0x000Fu;
            goto L8016BF84;
        }
        c.V0 = 0u | 0x000Fu;
        c.V1 = (uint)(short)mem.ReadU16((c.S1 + 0x80u));
        if (c.V1 != c.V0) {
            goto L8016BF90;
        }
        L8016BF84: ;
        c.V0 = 0u | 0x0002u;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
        goto L8016C06C;
        L8016BF90: ;
        c.A0 = (uint)(short)mem.ReadU16((c.S1 + 0x7Cu));
        c.S2 = 0u;
        c.RA = 0x8016BF9Cu;
        SoTN_main.rcos(c, m);
        c.V0 = c.V0 << 4;
        c.T1 = 0u | 0x0400u;
        { var _r = (long)(int)c.V0 * (int)c.T1; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.A0 = (uint)(short)mem.ReadU16((c.S1 + 0x7Cu));
        c.V0 = mem.ReadU32(c.S1);
        c.V1 = c.LO;
        c.S4 = (uint)((int)c.V1 >> 8);
        c.V0 = c.S4 + c.V0;
        mem.WriteU32(c.S1, c.V0);
        c.RA = 0x8016BFC4u;
        SoTN_main.rsin(c, m);
        c.V0 = c.V0 << 4;
        c.T1 = 0u | 0x0400u;
        { var _r = (long)(int)c.V0 * (int)c.T1; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V1 = mem.ReadU32((c.S1 + 0x4u));
        c.V0 = c.LO;
        c.V0 = 0u - c.V0;
        c.S3 = (uint)((int)c.V0 >> 8);
        c.V1 = c.S3 + c.V1;
        mem.WriteU32((c.S1 + 0x4u), c.V1);
        goto L8016C06C;
        L8016BFEC: ;
        c.V0 = mem.ReadU16((c.S1 + 0x7Eu));
        c.V0 = c.V0 - 0x1u;
        mem.WriteU16((c.S1 + 0x7Eu), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        c.V1 = (uint)((int)c.V0 >> 16);
        if (c.V1 != 0u) {
            c.V0 = 0u | 0x0020u;
            goto L8016C01C;
        }
        c.V0 = 0u | 0x0020u;
        L8016C00C: ;
        c.A0 = c.S1;
        c.RA = 0x8016C014u;
        SoTN_ric.DestroyEntity_ric(c, m);
        goto L8016C188;
        L8016C01C: ;
        if (c.V1 != c.V0) {
            goto L8016C028;
        }
        mem.WriteU16((c.S1 + 0x3Cu), (ushort)0u);
        L8016C028: ;
        c.V0 = mem.ReadU32((c.S1 + 0x64u));
        c.V1 = c.V0 << 1;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.V0 = 0x80080000u;
        c.V0 = c.V0 + 0x6FECu;
        c.V1 = c.V1 + c.V0;
        if (c.V1 == 0u) {
            c.S2 = 0u;
            goto L8016C06C;
        }
        c.S2 = 0u;
        L8016C058: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU16((c.V1 + 0x30u), (ushort)0u);
        c.V1 = mem.ReadU32(c.V1);
        if (c.V1 != 0u) {
            c.S2 = 0u;
            goto L8016C058;
        }
        c.S2 = 0u;
        L8016C06C: ;
        c.S0 = 0u | 0x0002u;
        L8016C070: ;
        c.V1 = mem.ReadU32((c.S1 + 0x64u));
        c.A0 = mem.ReadU16((c.S1 + 0x2Cu));
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        if (c.A0 != c.S0) {
            c.V1 = c.V0 + c.V1;
            goto L8016C0A0;
        }
        c.V1 = c.V0 + c.V1;
        c.S0 = 0u | 0x0004u;
        L8016C0A0: ;
        if (c.V1 == 0u) {
            goto L8016C188;
        }
        L8016C0A8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = (uint)(short)mem.ReadU16((c.S1 + 0x82u));
        if (c.V0 == 0u) {
            goto L8016C0E0;
        }
        c.V0 = (uint)(short)mem.ReadU16((c.S1 + 0x80u));
        if (c.S2 != c.V0) {
            goto L8016C108;
        }
        c.V0 = mem.ReadU16((c.V1 + 0x32u));
        c.T1 = mem.ReadU16((c.SP + 0x38u));
        mem.WriteU16((c.V1 + 0xAu), (ushort)c.S7);
        c.V0 = c.V0 & 0xFFF7u;
        mem.WriteU16((c.V1 + 0x8u), (ushort)c.T1);
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.V0);
        L8016C0E0: ;
        c.V0 = (uint)(short)mem.ReadU16((c.S1 + 0x80u));
        if (c.S2 != c.V0) {
            goto L8016C108;
        }
        c.V0 = mem.ReadU16((c.S1 + 0x2u));
        mem.WriteU16((c.V1 + 0x14u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S1 + 0x6u));
        mem.WriteU16((c.V1 + 0x16u), (ushort)c.V0);
        L8016C108: ;
        c.V0 = mem.ReadU16((c.V1 + 0x32u));
        c.V0 = c.V0 & 0x0008u;
        if (c.V0 != 0u) {
            goto L8016C178;
        }
        c.V0 = (uint)(short)mem.ReadU16((c.V1 + 0x30u));
        if (c.V0 == 0u) {
            c.A0 = c.V0;
            goto L8016C138;
        }
        c.A0 = c.V0;
        c.V0 = c.A0 - 0x1u;
        mem.WriteU16((c.V1 + 0x30u), (ushort)c.V0);
        goto L8016C178;
        L8016C138: ;
        c.V0 = mem.ReadU8((c.V1 + 0x12u));
        c.V0 = (int)c.S0 < (int)c.V0 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L8016C15C;
        }
        c.V0 = mem.ReadU8((c.V1 + 0x12u));
        c.V0 = c.V0 - c.S0;
        mem.WriteU8((c.V1 + 0x12u), (byte)c.V0);
        L8016C15C: ;
        c.V0 = mem.ReadU8((c.V1 + 0x12u));
        mem.WriteU8((c.V1 + 0x11u), (byte)c.V0);
        mem.WriteU8((c.V1 + 0x10u), (byte)c.V0);
        mem.WriteU8((c.V1 + 0x6u), (byte)c.V0);
        mem.WriteU8((c.V1 + 0x5u), (byte)c.V0);
        mem.WriteU8((c.V1 + 0x4u), (byte)c.V0);
        L8016C178: ;
        c.V1 = mem.ReadU32(c.V1);
        if (c.V1 != 0u) {
            c.S2 = c.S2 + 0x1u;
            goto L8016C0A8;
        }
        c.S2 = c.S2 + 0x1u;
        L8016C188: ;
        c.RA = mem.ReadU32((c.SP + 0x74u));
        c.FP = mem.ReadU32((c.SP + 0x70u));
        c.S7 = mem.ReadU32((c.SP + 0x6Cu));
        c.S6 = mem.ReadU32((c.SP + 0x68u));
        c.S5 = mem.ReadU32((c.SP + 0x64u));
        c.S4 = mem.ReadU32((c.SP + 0x60u));
        c.S3 = mem.ReadU32((c.SP + 0x5Cu));
        c.S2 = mem.ReadU32((c.SP + 0x58u));
        c.S1 = mem.ReadU32((c.SP + 0x54u));
        c.S0 = mem.ReadU32((c.SP + 0x50u));
        c.SP = c.SP + 0x78u;
        return;
    }

    static void RicEntitySubwpnAguneaEdge(CpuContext c, IMemory m)
    {
        var mem = (PSMemory)m;
        c.SP = c.SP - 0x20u;
        mem.WriteU32((c.SP + 0x10u), c.S0);
        c.S0 = c.A0;
        c.V1 = 0x00010000u;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2F2Cu));
        c.V1 = c.V1 | 0x0007u;
        mem.WriteU32((c.SP + 0x1Cu), c.RA);
        mem.WriteU32((c.SP + 0x18u), c.S2);
        c.V0 = c.V0 & c.V1;
        if (c.V0 == 0u) {
            mem.WriteU32((c.SP + 0x14u), c.S1);
            goto L8016C774;
        }
        mem.WriteU32((c.SP + 0x14u), c.S1);
        c.RA = 0x8016C76Cu;
        SoTN_ric.DestroyEntity_ric(c, m);
        goto L8016CC58;
        L8016C774: ;
        c.V1 = mem.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V1 < 0x00000005u ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = c.V1 << 2;
            goto L8016CB70;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x80150000u;
        c.At = c.At + c.V0;
        c.V0 = mem.ReadU32((c.At + 0x6B24u));
        switch (c.V0)
        {
            case 0x8016C7A0u: goto L8016C7A0;
            case 0x8016C8C0u: goto L8016C8C0;
            case 0x8016C968u: goto L8016C968;
            case 0x8016C9B0u: goto L8016C9B0;
            case 0x8016C930u: goto L8016C930;
            case 0x8016DC64u: goto L8016DC64;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L8016C7A0: ;
        c.A0 = 0u | 0x0004u;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0001u;
        c.RA = 0x8016C7B8u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V1 = 0xFFFFFFFFu;
        if (c.V0 == c.V1) {
            mem.WriteU32((c.S0 + 0x64u), c.V0);
            goto L8016C9A0;
        }
        mem.WriteU32((c.S0 + 0x64u), c.V0);
        c.A0 = c.S0;
        c.V0 = 0x0C800000u;
        mem.WriteU32((c.S0 + 0x34u), c.V0);
        c.V1 = 0x80070000u;
        c.V1 = mem.ReadU16((c.V1 + 0x33ECu));
        c.V0 = 0u | 0x0009u;
        mem.WriteU16((c.S0 + 0xB0u), (ushort)c.V0);
        mem.WriteU16((c.S0 + 0x14u), (ushort)c.V1);
        c.RA = 0x8016C7F0u;
        SoTN_ric.RicSetSubweaponParams(c, m);
        c.V0 = 0u | 0x0004u;
        mem.WriteU8((c.S0 + 0x47u), (byte)c.V0);
        mem.WriteU8((c.S0 + 0x46u), (byte)c.V0);
        c.V0 = 0u | 0x0004u;
        mem.WriteU16((c.S0 + 0x10u), (ushort)c.V0);
        mem.WriteU16((c.S0 + 0x12u), (ushort)0u);
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x33DEu));
        c.V1 = 0x80070000u;
        c.V1 = mem.ReadU16((c.V1 + 0x33EAu));
        c.A0 = 0x00060000u;
        c.V0 = c.V0 + c.V1;
        c.V1 = mem.ReadU32((c.S0 + 0x64u));
        c.V0 = c.V0 - 0x8u;
        mem.WriteU16((c.S0 + 0x82u), (ushort)c.V0);
        mem.WriteU16((c.S0 + 0x6u), (ushort)c.V0);
        c.A1 = 0x80070000u;
        c.A1 = mem.ReadU16((c.A1 + 0x33DAu));
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A2 = c.V0 + c.V1;
        c.V0 = 0u | 0x0002u;
        mem.WriteU16((c.S0 + 0x80u), (ushort)c.A1);
        mem.WriteU16((c.S0 + 0x2u), (ushort)c.A1);
        mem.WriteU8((c.A2 + 0x7u), (byte)c.V0);
        c.V1 = 0x80070000u;
        c.V1 = mem.ReadU16((c.V1 + 0x33FCu));
        c.V0 = 0u | 0x0331u;
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.V0);
        c.V0 = 0u | 0x0060u;
        mem.WriteU8((c.A2 + 0x10u), (byte)c.V0);
        c.V0 = 0u | 0x0080u;
        mem.WriteU8((c.A2 + 0x11u), (byte)0u);
        mem.WriteU8((c.A2 + 0x12u), (byte)c.V0);
        c.V1 = c.V1 + 0x2u;
        mem.WriteU16((c.A2 + 0x26u), (ushort)c.V1);
        c.RA = 0x8016C898u;
        SoTN_ric.RicSetSpeedX(c, m);
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x060Cu;
        c.RA = 0x8016C8ACu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        goto L8016CB70;
        L8016C8C0: ;
        c.V0 = mem.ReadU32(c.S0);
        c.V1 = mem.ReadU32((c.S0 + 0x8u));
        c.V0 = c.V0 + c.V1;
        mem.WriteU32(c.S0, c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x2u));
        c.V0 = c.V0 + 0x40u + (uint)Margin;
        c.V0 = c.V0 & 0xFFFFu;
        c.V0 = c.V0 < 0x00000181u + (uint)(Margin * 2) ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = 0u | 0x0002u;
            goto L8016C90C;
        }
        c.V0 = 0u | 0x0002u;
        c.V0 = mem.ReadU16((c.S0 + 0x6u));
        c.V0 = c.V0 + 0x20u;
        c.V0 = c.V0 & 0xFFFFu;
        c.V0 = c.V0 < 0x00000141u ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0002u;
            goto L8016C910;
        }
        c.V0 = 0u | 0x0002u;
        L8016C90C: ;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        L8016C910: ;
        c.V0 = mem.ReadU8((c.S0 + 0x48u));
        if (c.V0 == 0u) {
            c.V0 = 0u | 0x0003u;
            goto L8016CB70;
        }
        c.V0 = 0u | 0x0003u;
        c.V1 = mem.ReadU32((c.S0 + 0xB8u));
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        mem.WriteU32((c.S0 + 0x8Cu), c.V1);
        goto L8016CB70;
        L8016C930: ;
        c.V0 = mem.ReadU32((c.S0 + 0x8Cu));
        c.V1 = mem.ReadU32((c.S0 + 0x8Cu));
        c.V0 = mem.ReadU16((c.V0 + 0x2u));
        mem.WriteU16((c.S0 + 0x2u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x7Cu));
        c.V1 = mem.ReadU16((c.V1 + 0x6u));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S0 + 0x7Cu), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V0 < 16 ? 1u : 0u;
        if (c.V0 != 0u) {
            mem.WriteU16((c.S0 + 0x6u), (ushort)c.V1);
            goto L8016CB70;
        }
        mem.WriteU16((c.S0 + 0x6u), (ushort)c.V1);
        L8016C968: ;
        c.V0 = mem.ReadU32((c.S0 + 0x64u));
        c.V1 = c.V0 << 1;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.At = 0x80080000u;
        c.At = c.At + c.V1;
        c.V0 = mem.ReadU8((c.At + 0x6FFCu));
        c.V0 = c.V0 < 0x00000005u ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L8016CB70;
        }
        L8016C9A0: ;
        c.A0 = c.S0;
        c.RA = 0x8016C9A8u;
        SoTN_ric.DestroyEntity_ric(c, m);
        goto L8016CC58;
        L8016C9B0: ;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x2EE8u));
        c.V1 = 0u | 0x1080u;
        c.V0 = c.V0 & 0x1080u;
        if (c.V0 == c.V1) {
            c.V0 = 0u | 0x0004u;
            goto L8016C9CC;
        }
        c.V0 = 0u | 0x0004u;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        L8016C9CC: ;
        c.V1 = mem.ReadU32((c.S0 + 0x8Cu));
        c.V0 = mem.ReadU16((c.V1 + 0x26u));
        if (c.V0 == 0u) {
            c.V0 = 0u | 0x0002u;
            goto L8016CA24;
        }
        c.V0 = 0u | 0x0002u;
        c.V0 = (uint)(short)mem.ReadU16((c.S0 + 0x7Cu));
        if (c.V0 == 0u) {
            c.V0 = 0x2AAA0000u;
            goto L8016CA2C;
        }
        c.V0 = 0x2AAA0000u;
        c.A0 = (uint)(short)mem.ReadU16((c.V1 + 0x3Eu));
        c.V0 = (int)c.A0 < 28673 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L8016CA20;
        }
        if (c.A0 == 0u) {
            c.V0 = 0u | 0x0002u;
            goto L8016CA24;
        }
        c.V0 = 0u | 0x0002u;
        c.V0 = mem.ReadU16((c.V1 + 0x3Cu));
        if (c.V0 != 0u) {
            c.V0 = 0x2AAA0000u;
            goto L8016CA2C;
        }
        c.V0 = 0x2AAA0000u;
        L8016CA20: ;
        c.V0 = 0u | 0x0002u;
        L8016CA24: ;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        goto L8016CC58;
        L8016CA2C: ;
        c.A0 = mem.ReadU16((c.S0 + 0x7Cu));
        c.V0 = c.V0 | 0xAAABu;
        c.A0 = c.A0 << 16;
        c.A1 = (uint)((int)c.A0 >> 16);
        { var _r = (long)(int)c.A1 * (int)c.V0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V0 = mem.ReadU32((c.S0 + 0x8Cu));
        c.V1 = mem.ReadU16((c.V0 + 0x2u));
        c.V0 = mem.ReadU32((c.S0 + 0x8Cu));
        c.A0 = (uint)((int)c.A0 >> 31);
        mem.WriteU16((c.S0 + 0x2u), (ushort)c.V1);
        c.V0 = mem.ReadU16((c.V0 + 0x6u));
        c.S1 = c.V1;
        c.S2 = c.V0;
        mem.WriteU16((c.S0 + 0x6u), (ushort)c.V0);
        c.V1 = c.HI;
        c.V1 = (uint)((int)c.V1 >> 1);
        c.V1 = c.V1 - c.A0;
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.A1 = c.A1 - c.V0;
        c.A1 = c.A1 << 16;
        if (c.A1 != 0u) {
            goto L8016CB5C;
        }
        c.RA = 0x8016CA98u;
        SoTN_main.rand(c, m);
        c.V1 = mem.ReadU16((c.S0 + 0x2u));
        c.V0 = c.V0 & 0x000Fu;
        c.V1 = c.V1 - 0x8u;
        c.V1 = c.V1 + c.V0;
        mem.WriteU16((c.S0 + 0x2u), (ushort)c.V1);
        c.RA = 0x8016CAB0u;
        SoTN_main.rand(c, m);
        c.V0 = c.V0 & 0x000Fu;
        c.V1 = mem.ReadU16((c.S0 + 0x6u));
        c.A3 = 0x80090000u;
        c.A3 = c.A3 + 0x7BA8u;
        c.V1 = c.V1 - 0x8u;
        c.V1 = c.V1 + c.V0;
        mem.WriteU16((c.S0 + 0x6u), (ushort)c.V1);
        c.V1 = mem.ReadU32(c.A3);
        c.V0 = (int)c.V1 < 5 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.A0 = c.S0;
            goto L8016CB10;
        }
        c.A0 = c.S0;
        c.A1 = 0u | 0x0034u;
        c.A2 = 0u;
        c.V0 = c.V1 - 0x5u;
        mem.WriteU32(c.A3, c.V0);
        c.RA = 0x8016CAF4u;
        SoTN_ric.RicCreateEntFactoryFromEntity(c, m);
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x0665u;
        c.RA = 0x8016CB08u;
        Dispatcher.Call(c, m, c.V0);
        goto L8016CB5C;
        L8016CB10: ;
        c.V0 = mem.ReadU32((c.S0 + 0x84u));
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0004u;
            goto L8016CB58;
        }
        c.V0 = 0u | 0x0004u;
        c.A0 = c.S0;
        c.A1 = 0u | 0x0034u;
        c.A2 = 0u;
        c.RA = 0x8016CB30u;
        SoTN_ric.RicCreateEntFactoryFromEntity(c, m);
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x0665u;
        c.RA = 0x8016CB44u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = mem.ReadU32((c.S0 + 0x84u));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU32((c.S0 + 0x84u), c.V0);
        goto L8016CB5C;
        L8016CB58: ;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        L8016CB5C: ;
        c.V0 = mem.ReadU16((c.S0 + 0x7Cu));
        mem.WriteU16((c.S0 + 0x2u), (ushort)c.S1);
        mem.WriteU16((c.S0 + 0x6u), (ushort)c.S2);
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S0 + 0x7Cu), (ushort)c.V0);
        L8016CB70: ;
        c.V1 = mem.ReadU32((c.S0 + 0x64u));
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A2 = c.V0 + c.V1;
        c.V0 = mem.ReadU8((c.A2 + 0x10u));
        c.V0 = c.V0 < 0x00000004u ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L8016CBBC;
        }
        c.V0 = mem.ReadU8((c.A2 + 0x10u));
        c.V0 = c.V0 + 0xFCu;
        mem.WriteU8((c.A2 + 0x10u), (byte)c.V0);
        L8016CBBC: ;
        c.V0 = mem.ReadU8((c.A2 + 0x11u));
        c.V0 = c.V0 < 0x00000004u ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L8016CBE0;
        }
        c.V0 = mem.ReadU8((c.A2 + 0x11u));
        c.V0 = c.V0 + 0xFCu;
        mem.WriteU8((c.A2 + 0x11u), (byte)c.V0);
        L8016CBE0: ;
        c.V0 = mem.ReadU8((c.A2 + 0x12u));
        c.V0 = c.V0 < 0x00000004u ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L8016CC04;
        }
        c.V0 = mem.ReadU8((c.A2 + 0x12u));
        c.V0 = c.V0 + 0xFCu;
        mem.WriteU8((c.A2 + 0x12u), (byte)c.V0);
        L8016CC04: ;
        c.V0 = mem.ReadU8((c.A2 + 0x12u));
        c.V0 = c.V0 < 0x00000005u ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L8016CC28;
        }
        c.V0 = mem.ReadU16((c.A2 + 0x32u));
        c.V0 = c.V0 | 0x0008u;
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.V0);
        L8016CC28: ;
        c.V0 = mem.ReadU16((c.S0 + 0x80u));
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x82u));
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x2u));
        mem.WriteU16((c.A2 + 0x14u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x6u));
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.V0);
        L8016CC58: ;
        c.RA = mem.ReadU32((c.SP + 0x1Cu));
        c.S2 = mem.ReadU32((c.SP + 0x18u));
        c.S1 = mem.ReadU32((c.SP + 0x14u));
        c.S0 = mem.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x20u;
        return;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x33DAu));
        c.SP = c.SP - 0x30u;
        mem.WriteU32((c.SP + 0x20u), c.S4);
        c.S4 = c.A0;
        mem.WriteU32((c.SP + 0x2Cu), c.RA);
        mem.WriteU32((c.SP + 0x28u), c.S6);
        mem.WriteU32((c.SP + 0x24u), c.S5);
        mem.WriteU32((c.SP + 0x1Cu), c.S3);
        mem.WriteU32((c.SP + 0x18u), c.S2);
        mem.WriteU32((c.SP + 0x14u), c.S1);
        mem.WriteU32((c.SP + 0x10u), c.S0);
        mem.WriteU16((c.S4 + 0x2u), (ushort)c.V0);
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x33DEu));
        c.V1 = 0x80070000u;
        c.V1 = mem.ReadU16((c.V1 + 0x33EAu));
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 - 0x8u;
        mem.WriteU16((c.S4 + 0x6u), (ushort)c.V0);
        c.V0 = mem.ReadU32((c.S4 + 0x8Cu));
        c.S6 = mem.ReadU32((c.S4 + 0x8Cu));
        c.V0 = mem.ReadU16((c.V0 + 0x26u));
        c.V1 = 0u | 0x002Cu;
        if (c.V0 == c.V1) {
            goto L8016CD20;
        }
        c.V1 = mem.ReadU16((c.S4 + 0x2Cu));
        c.V0 = (int)c.V1 < 3 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L8016CD0C;
        }
        if (c.V1 != 0u) {
            goto L8016CD18;
        }
        if (c.V1 == 0u) {
            c.V0 = 0u | 0x0001u;
            goto L8016D2F4;
        }
        c.V0 = 0u | 0x0001u;
        goto L8016CD28;
        L8016CD0C: ;
        c.V0 = 0u | 0x0004u;
        if (c.V1 != c.V0) {
            goto L8016CD20;
        }
        L8016CD18: ;
        c.V0 = 0u | 0x0003u;
        mem.WriteU16((c.S4 + 0x2Cu), (ushort)c.V0);
        L8016CD20: ;
        c.V1 = mem.ReadU16((c.S4 + 0x2Cu));
        c.V0 = 0u | 0x0001u;
        L8016CD28: ;
        if (c.V1 == c.V0) {
            c.V0 = (int)c.V1 < 2 ? 1u : 0u;
            goto L8016CF74;
        }
        c.V0 = (int)c.V1 < 2 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L8016CD48;
        }
        if (c.V1 == 0u) {
            c.A0 = 0u | 0x0004u;
            goto L8016CD64;
        }
        c.A0 = 0u | 0x0004u;
        goto L8016D2FC;
        L8016CD48: ;
        c.V0 = 0u | 0x0002u;
        if (c.V1 == c.V0) {
            c.V0 = 0u | 0x0003u;
            goto L8016D1D4;
        }
        c.V0 = 0u | 0x0003u;
        if (c.V1 == c.V0) {
            goto L8016D288;
        }
        goto L8016D2FC;
        L8016CD64: ;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0028u;
        c.RA = 0x8016CD78u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V1 = 0xFFFFFFFFu;
        if (c.V0 == c.V1) {
            mem.WriteU32((c.S4 + 0x64u), c.V0);
            goto L8016D2F4;
        }
        mem.WriteU32((c.S4 + 0x64u), c.V0);
        c.V0 = 0x04800000u;
        mem.WriteU32((c.S4 + 0x34u), c.V0);
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x33ECu));
        c.S5 = 0u;
        mem.WriteU16((c.S4 + 0x14u), (ushort)c.V0);
        c.RA = 0x8016CDA8u;
        SoTN_main.rand(c, m);
        c.V0 = c.V0 & 0x03FFu;
        c.V0 = c.V0 - 0x200u;
        c.V0 = c.V0 & 0x0FFFu;
        c.V1 = mem.ReadU32((c.S4 + 0x64u));
        c.A0 = 0x80080000u;
        c.A0 = c.A0 + 0x6FECu;
        mem.WriteU16((c.S4 + 0x84u), (ushort)c.V0);
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = mem.ReadU16((c.S4 + 0x30u));
        c.S0 = c.V0 + c.A0;
        mem.WriteU32((c.S4 + 0x7Cu), c.S0);
        mem.WriteU32((c.S4 + 0x80u), c.S0);
        c.V1 = c.V1 >> 8;
        if (c.S0 == 0u) {
            mem.WriteU16((c.S4 + 0x90u), (ushort)c.V1);
            goto L8016CEBC;
        }
        mem.WriteU16((c.S4 + 0x90u), (ushort)c.V1);
        c.T2 = 0u | 0x001Au;
        c.T1 = 0u | 0x0194u;
        c.T0 = 0u | 0x00D0u;
        c.A3 = 0u | 0x00C0u;
        c.A2 = 0u | 0x00F0u;
        c.V0 = c.S5 << 4;
        L8016CE0C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V1 = c.V0 - 0x70u;
        c.V0 = c.V0 - 0x60u;
        mem.WriteU16((c.S0 + 0x1Au), (ushort)c.T2);
        mem.WriteU16((c.S0 + 0xEu), (ushort)c.T1);
        mem.WriteU8((c.S0 + 0x18u), (byte)c.V1);
        mem.WriteU8((c.S0 + 0xCu), (byte)c.V1);
        mem.WriteU8((c.S0 + 0x30u), (byte)c.V0);
        mem.WriteU8((c.S0 + 0x24u), (byte)c.V0);
        mem.WriteU8((c.S0 + 0x25u), (byte)c.T0);
        mem.WriteU8((c.S0 + 0xDu), (byte)c.T0);
        mem.WriteU8((c.S0 + 0x31u), (byte)c.A3);
        mem.WriteU8((c.S0 + 0x19u), (byte)c.A3);
        c.V0 = mem.ReadU16((c.S4 + 0x2u));
        mem.WriteU16((c.S0 + 0x8u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S4 + 0x6u));
        mem.WriteU16((c.S0 + 0xAu), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S4 + 0x2u));
        mem.WriteU16((c.S0 + 0x20u), (ushort)c.V0);
        c.A1 = mem.ReadU16((c.S4 + 0x2u));
        mem.WriteU8((c.S0 + 0x6u), (byte)c.A2);
        mem.WriteU8((c.S0 + 0x5u), (byte)c.A2);
        mem.WriteU8((c.S0 + 0x4u), (byte)c.A2);
        c.V0 = mem.ReadU32((c.S0 + 0x4u));
        c.V1 = mem.ReadU32((c.S0 + 0x4u));
        c.A0 = mem.ReadU32((c.S0 + 0x4u));
        c.S5 = c.S5 + 0x1u;
        mem.WriteU16((c.S0 + 0x22u), (ushort)c.A1);
        mem.WriteU32((c.S0 + 0x10u), c.V0);
        mem.WriteU32((c.S0 + 0x1Cu), c.V1);
        mem.WriteU32((c.S0 + 0x28u), c.A0);
        c.V1 = mem.ReadU16((c.S4 + 0x24u));
        c.V0 = 0u | 0x0008u;
        mem.WriteU16((c.S0 + 0x32u), (ushort)c.V0);
        c.V0 = (int)c.S5 < 6 ? 1u : 0u;
        mem.WriteU16((c.S0 + 0x26u), (ushort)c.V1);
        c.S0 = mem.ReadU32(c.S0);
        if (c.V0 != 0u) {
            goto L8016CEB4;
        }
        c.S5 = 0u;
        L8016CEB4: ;
        if (c.S0 != 0u) {
            c.V0 = c.S5 << 4;
            goto L8016CE0C;
        }
        c.V0 = c.S5 << 4;
        L8016CEBC: ;
        c.S0 = mem.ReadU32((c.S4 + 0x7Cu));
        c.V0 = mem.ReadU16((c.S4 + 0x2u));
        mem.WriteU16((c.S0 + 0x8u), (ushort)c.V0);
        c.V1 = mem.ReadU16((c.S4 + 0x6u));
        c.V0 = mem.ReadU16((c.S0 + 0x16u));
        c.A0 = mem.ReadU16((c.S0 + 0x8u));
        c.V0 = c.V0 - 0x10u;
        mem.WriteU16((c.S0 + 0xAu), (ushort)c.V1);
        mem.WriteU16((c.S0 + 0x14u), (ushort)c.A0);
        mem.WriteU16((c.S0 + 0x16u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S4 + 0x2u));
        mem.WriteU16((c.S0 + 0x20u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S4 + 0x6u));
        c.V1 = mem.ReadU16((c.S0 + 0x20u));
        mem.WriteU16((c.S0 + 0x22u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x22u));
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V1);
        c.V0 = c.V0 - 0x10u;
        mem.WriteU16((c.S0 + 0x2Eu), (ushort)c.V0);
        if (c.S0 == 0u) {
            mem.WriteU32((c.S4 + 0x80u), c.S0);
            goto L8016CF68;
        }
        mem.WriteU32((c.S4 + 0x80u), c.S0);
        c.A3 = 0u | 0x0194u;
        c.A1 = 0u | 0x0080u;
        c.A2 = 0u | 0x0008u;
        L8016CF24: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU8((c.S0 + 0x6u), (byte)c.A1);
        mem.WriteU8((c.S0 + 0x5u), (byte)c.A1);
        mem.WriteU8((c.S0 + 0x4u), (byte)c.A1);
        c.V0 = mem.ReadU32((c.S0 + 0x4u));
        c.V1 = mem.ReadU32((c.S0 + 0x4u));
        c.A0 = mem.ReadU32((c.S0 + 0x4u));
        mem.WriteU16((c.S0 + 0xEu), (ushort)c.A3);
        mem.WriteU32((c.S0 + 0x10u), c.V0);
        mem.WriteU32((c.S0 + 0x1Cu), c.V1);
        mem.WriteU32((c.S0 + 0x28u), c.A0);
        c.V0 = mem.ReadU16((c.S4 + 0x24u));
        mem.WriteU16((c.S0 + 0x32u), (ushort)c.A2);
        mem.WriteU16((c.S0 + 0x26u), (ushort)c.V0);
        c.S0 = mem.ReadU32(c.S0);
        if (c.S0 != 0u) {
            goto L8016CF24;
        }
        L8016CF68: ;
        c.V0 = mem.ReadU16((c.S4 + 0x2Cu));
        mem.WriteU16((c.S4 + 0x88u), (ushort)0u);
        goto L8016D278;
        L8016CF74: ;
        c.S5 = 0u;
        L8016CF78: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.S0 = mem.ReadU32((c.S4 + 0x80u));
        c.S2 = mem.ReadU16((c.S4 + 0x84u));
        c.V0 = mem.ReadU16((c.S6 + 0x2u));
        c.A1 = mem.ReadU16((c.S6 + 0x6u));
        c.A0 = mem.ReadU16((c.S0 + 0x20u));
        c.V1 = mem.ReadU16((c.S0 + 0x22u));
        c.V0 = c.V0 - c.A0;
        c.A0 = c.A1 - c.V1;
        c.V0 = c.V0 << 16;
        c.A1 = (uint)((int)c.V0 >> 16);
        if ((int)c.A1 >= 0) {
            c.V1 = c.A1;
            goto L8016CFAC;
        }
        c.V1 = c.A1;
        c.V1 = 0u - c.V1;
        L8016CFAC: ;
        c.V0 = (int)c.V1 < 8 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.S3 = 0u;
            goto L8016CFD8;
        }
        c.S3 = 0u;
        c.V0 = c.A0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        if ((int)c.V0 >= 0) {
            goto L8016CFCC;
        }
        c.V0 = 0u - c.V0;
        L8016CFCC: ;
        c.V0 = (int)c.V0 < 8 ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L8016D274;
        }
        L8016CFD8: ;
        c.V0 = (int)c.V1 < 64 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = c.A0 << 16;
            goto L8016CFFC;
        }
        c.V0 = c.A0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        if ((int)c.V0 >= 0) {
            goto L8016CFF4;
        }
        c.V0 = 0u - c.V0;
        L8016CFF4: ;
        c.V0 = (int)c.V0 < 64 ? 1u : 0u;
        c.S3 = c.V0;
        L8016CFFC: ;
        c.V0 = (uint)(short)mem.ReadU16((c.S4 + 0x88u));
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0004u;
            goto L8016D09C;
        }
        c.V0 = 0u | 0x0004u;
        c.S1 = c.S3 & 0x00FFu;
        if (c.S1 == 0u) {
            mem.WriteU16((c.S4 + 0x88u), (ushort)c.V0);
            goto L8016D020;
        }
        mem.WriteU16((c.S4 + 0x88u), (ushort)c.V0);
        c.V0 = 0u | 0x0002u;
        mem.WriteU16((c.S4 + 0x88u), (ushort)c.V0);
        L8016D020: ;
        c.A0 = c.A0 << 16;
        c.A0 = (uint)((int)c.A0 >> 16);
        c.A0 = 0u - c.A0;
        c.RA = 0x8016D030u;
        SoTN_main.ratan2(c, m);
        c.A0 = c.V0 - c.S2;
        c.V0 = c.A0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V0 < 2049 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V1 = c.A0;
            goto L8016D050;
        }
        c.V1 = c.A0;
        c.V0 = 0u | 0x1000u;
        c.V1 = c.V0 - c.A0;
        L8016D050: ;
        c.V0 = c.V1 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V0 < -2048 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L8016D068;
        }
        c.V1 = c.V1 + 0x1000u;
        L8016D068: ;
        if (c.S1 != 0u) {
            c.V0 = c.V1 << 16;
            goto L8016D088;
        }
        c.V0 = c.V1 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        if ((int)c.V0 >= 0) {
            c.V1 = c.V0 >> 2;
            goto L8016D098;
        }
        c.V1 = c.V0 >> 2;
        c.V0 = c.V0 + 0x3u;
        c.V1 = c.V0 >> 2;
        goto L8016D098;
        L8016D088: ;
        c.V1 = (uint)((int)c.V0 >> 16);
        c.V0 = c.V0 >> 31;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 >> 1;
        L8016D098: ;
        mem.WriteU16((c.S4 + 0x86u), (ushort)c.V1);
        L8016D09C: ;
        c.V0 = mem.ReadU16((c.S4 + 0x86u));
        c.S1 = c.S2 + c.V0;
        c.V0 = c.S3 & 0x00FFu;
        if (c.V0 != 0u) {
            c.S2 = c.S1;
            goto L8016D0CC;
        }
        c.S2 = c.S1;
        c.RA = 0x8016D0BCu;
        SoTN_main.rand(c, m);
        c.V1 = c.S1 + 0x180u;
        c.V0 = c.V0 & 0x0003u;
        c.V0 = c.V0 << 8;
        c.S2 = c.V1 - c.V0;
        L8016D0CC: ;
        c.S1 = c.S2 & 0x0FFFu;
        c.S3 = mem.ReadU32(c.S0);
        if (c.S3 == 0u) {
            c.S2 = c.S1;
            goto L8016D274;
        }
        c.S2 = c.S1;
        c.V0 = mem.ReadU32((c.S0 + 0x20u));
        mem.WriteU32((c.S3 + 0x8u), c.V0);
        c.V0 = mem.ReadU32((c.S0 + 0x2Cu));
        c.A0 = c.S1;
        mem.WriteU32((c.S3 + 0x14u), c.V0);
        mem.WriteU16((c.S4 + 0x84u), (ushort)c.S2);
        mem.WriteU32((c.S4 + 0x80u), c.S3);
        c.RA = 0x8016D104u;
        SoTN_main.rcos(c, m);
        c.A0 = c.S1;
        c.S0 = c.V0 << 1;
        c.S0 = c.S0 + c.V0;
        c.S0 = c.S0 << 2;
        c.S0 = (uint)((int)c.S0 >> 12);
        c.RA = 0x8016D11Cu;
        SoTN_main.rsin(c, m);
        c.S1 = c.S1 - 0x400u;
        c.S1 = c.S1 << 16;
        c.S1 = (uint)((int)c.S1 >> 16);
        c.V1 = mem.ReadU16((c.S3 + 0x8u));
        c.A0 = c.S1;
        c.S0 = c.S0 + c.V1;
        c.V1 = c.V0 << 1;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.V0 = mem.ReadU16((c.S3 + 0xAu));
        c.V1 = (uint)((int)c.V1 >> 12);
        mem.WriteU16((c.S3 + 0x20u), (ushort)c.S0);
        c.V0 = c.V0 - c.V1;
        mem.WriteU16((c.S3 + 0x22u), (ushort)c.V0);
        c.V1 = mem.ReadU16((c.S4 + 0x30u));
        c.V0 = 0u | 0x0010u;
        c.V1 = c.V1 << 2;
        c.S2 = c.V0 - c.V1;
        c.RA = 0x8016D168u;
        SoTN_main.rcos(c, m);
        c.S2 = c.S2 & 0x00FFu;
        { var _r = (long)(int)c.S2 * (int)c.V0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.A0 = c.S1;
        c.S0 = c.LO;
        c.S0 = (uint)((int)c.S0 >> 12);
        c.RA = 0x8016D180u;
        SoTN_main.rsin(c, m);
        { var _r = (long)(int)c.S2 * (int)c.V0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V1 = mem.ReadU16((c.S3 + 0x20u));
        c.S0 = c.S0 + c.V1;
        c.V1 = mem.ReadU16((c.S3 + 0x22u));
        c.V0 = 0u | 0x0006u;
        mem.WriteU16((c.S3 + 0x32u), (ushort)c.V0);
        mem.WriteU16((c.S3 + 0x2Cu), (ushort)c.S0);
        c.V0 = c.LO;
        c.V0 = (uint)((int)c.V0 >> 12);
        c.V1 = c.V1 - c.V0;
        mem.WriteU16((c.S3 + 0x2Eu), (ushort)c.V1);
        c.V0 = mem.ReadU16((c.S4 + 0x88u));
        c.S5 = c.S5 + 0x1u;
        c.V0 = c.V0 - 0x1u;
        mem.WriteU16((c.S4 + 0x88u), (ushort)c.V0);
        c.V0 = (int)c.S5 < 2 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L8016D2FC;
        }
        goto L8016CF78;
        L8016D1D4: ;
        c.V0 = mem.ReadU16((c.S4 + 0x2Eu));
        if (c.V0 != 0u) {
            goto L8016D21C;
        }
        c.S0 = mem.ReadU32((c.S4 + 0x7Cu));
        if (c.S0 == 0u) {
            c.V0 = 0u | 0x015Fu;
            goto L8016D208;
        }
        c.V0 = 0u | 0x015Fu;
        L8016D1F4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU16((c.S0 + 0xEu), (ushort)c.V0);
        c.S0 = mem.ReadU32(c.S0);
        if (c.S0 != 0u) {
            goto L8016D1F4;
        }
        L8016D208: ;
        c.V0 = mem.ReadU16((c.S4 + 0x2Eu));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S4 + 0x2Eu), (ushort)c.V0);
        goto L8016D2FC;
        L8016D21C: ;
        c.S0 = mem.ReadU32((c.S4 + 0x7Cu));
        if (c.S0 == 0u) {
            c.A2 = 0u | 0x0194u;
            goto L8016D268;
        }
        c.A2 = 0u | 0x0194u;
        c.A1 = 0u | 0x0060u;
        L8016D230: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU8((c.S0 + 0x6u), (byte)c.A1);
        mem.WriteU8((c.S0 + 0x5u), (byte)c.A1);
        mem.WriteU8((c.S0 + 0x4u), (byte)c.A1);
        c.V0 = mem.ReadU32((c.S0 + 0x4u));
        c.V1 = mem.ReadU32((c.S0 + 0x4u));
        c.A0 = mem.ReadU32((c.S0 + 0x4u));
        mem.WriteU16((c.S0 + 0xEu), (ushort)c.A2);
        mem.WriteU32((c.S0 + 0x10u), c.V0);
        mem.WriteU32((c.S0 + 0x1Cu), c.V1);
        mem.WriteU32((c.S0 + 0x28u), c.A0);
        c.S0 = mem.ReadU32(c.S0);
        if (c.S0 != 0u) {
            goto L8016D230;
        }
        L8016D268: ;
        c.V0 = mem.ReadU16((c.S4 + 0x2Cu));
        mem.WriteU16((c.S4 + 0x2Eu), (ushort)0u);
        goto L8016D278;
        L8016D274: ;
        c.V0 = mem.ReadU16((c.S4 + 0x2Cu));
        L8016D278: ;
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S4 + 0x2Cu), (ushort)c.V0);
        goto L8016D2FC;
        L8016D288: ;
        c.S0 = mem.ReadU32((c.S4 + 0x7Cu));
        if (c.S0 == 0u) {
            c.S2 = 0u | 0x0001u;
            goto L8016D2C4;
        }
        c.S2 = 0u | 0x0001u;
        c.A0 = c.S0;
        L8016D29C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A1 = 0u | 0x0004u;
        c.RA = 0x8016D2A4u;
        SoTN_ric.PrimDecreaseBrightness_ric(c, m);
        c.V0 = c.V0 & 0x00FFu;
        if (c.V0 == 0u) {
            c.S2 = c.S2 & 0x0001u;
            goto L8016D2B4;
        }
        c.S2 = c.S2 & 0x0001u;
        c.S2 = 0u;
        L8016D2B4: ;
        c.S0 = mem.ReadU32(c.S0);
        if (c.S0 != 0u) {
            c.A0 = c.S0;
            goto L8016D29C;
        }
        c.A0 = c.S0;
        L8016D2C4: ;
        c.V0 = c.S2 & 0x00FFu;
        if (c.V0 == 0u) {
            goto L8016D2FC;
        }
        c.S0 = mem.ReadU32((c.S4 + 0x7Cu));
        if (c.S0 == 0u) {
            c.V0 = 0u | 0x0008u;
            goto L8016D2F4;
        }
        c.V0 = 0u | 0x0008u;
        L8016D2E0: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU16((c.S0 + 0x32u), (ushort)c.V0);
        c.S0 = mem.ReadU32(c.S0);
        if (c.S0 != 0u) {
            goto L8016D2E0;
        }
        L8016D2F4: ;
        c.A0 = c.S4;
        c.RA = 0x8016D2FCu;
        SoTN_ric.DestroyEntity_ric(c, m);
        L8016D2FC: ;
        c.RA = mem.ReadU32((c.SP + 0x2Cu));
        c.S6 = mem.ReadU32((c.SP + 0x28u));
        c.S5 = mem.ReadU32((c.SP + 0x24u));
        c.S4 = mem.ReadU32((c.SP + 0x20u));
        c.S3 = mem.ReadU32((c.SP + 0x1Cu));
        c.S2 = mem.ReadU32((c.SP + 0x18u));
        c.S1 = mem.ReadU32((c.SP + 0x14u));
        c.S0 = mem.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x30u;
        return;
        c.SP = c.SP - 0x20u;
        mem.WriteU32((c.SP + 0x14u), c.S1);
        c.S1 = c.A0;
        mem.WriteU32((c.SP + 0x18u), c.RA);
        mem.WriteU32((c.SP + 0x10u), c.S0);
        c.V1 = mem.ReadU16((c.S1 + 0x2Cu));
        if (c.V1 == 0u) {
            c.V0 = 0u | 0x0001u;
            goto L8016D35C;
        }
        c.V0 = 0u | 0x0001u;
        if (c.V1 == c.V0) {
            goto L8016D46C;
        }
        goto L8016D4C0;
        L8016D35C: ;
        c.A0 = 0u | 0x0004u;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0001u;
        c.RA = 0x8016D374u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V1 = 0xFFFFFFFFu;
        if (c.V0 == c.V1) {
            mem.WriteU32((c.S1 + 0x64u), c.V0);
            goto L8016D490;
        }
        mem.WriteU32((c.S1 + 0x64u), c.V0);
        c.V1 = mem.ReadU32((c.S1 + 0x8Cu));
        c.V0 = 0x08800000u;
        mem.WriteU32((c.S1 + 0x34u), c.V0);
        c.V0 = mem.ReadU32((c.V1 + 0x84u));
        c.V1 = mem.ReadU32((c.S1 + 0x8Cu));
        mem.WriteU32(c.S1, c.V0);
        c.V0 = mem.ReadU32((c.V1 + 0x88u));
        c.V1 = mem.ReadU32((c.S1 + 0x8Cu));
        c.A0 = c.S1;
        mem.WriteU32((c.S1 + 0x4u), c.V0);
        c.V1 = mem.ReadU16((c.V1 + 0x8Cu));
        c.V0 = 0u | 0x0018u;
        mem.WriteU16((c.S1 + 0xB0u), (ushort)c.V0);
        mem.WriteU16((c.S1 + 0x14u), (ushort)c.V1);
        c.RA = 0x8016D3C4u;
        SoTN_ric.RicSetSubweaponParams(c, m);
        c.V0 = 0u | 0x0079u;
        mem.WriteU16((c.S1 + 0x5Au), (ushort)c.V0);
        c.V0 = 0u | 0x000Eu;
        mem.WriteU16((c.S1 + 0x54u), (ushort)c.V0);
        c.V0 = 0u | 0x819Eu;
        mem.WriteU16((c.S1 + 0x16u), (ushort)c.V0);
        c.V0 = 0x80150000u;
        c.V0 = c.V0 + 0x5EA8u;
        mem.WriteU32((c.S1 + 0x4Cu), c.V0);
        c.V0 = 0u | 0x0030u;
        mem.WriteU8((c.S1 + 0x18u), (byte)c.V0);
        c.V0 = 0u | 0x0060u;
        mem.WriteU8((c.S1 + 0x6Cu), (byte)c.V0);
        c.V0 = mem.ReadU32((c.S1 + 0x34u));
        c.V1 = 0u | 0x0008u;
        mem.WriteU8((c.S1 + 0x19u), (byte)c.V1);
        mem.WriteU8((c.S1 + 0x46u), (byte)c.V1);
        mem.WriteU8((c.S1 + 0x47u), (byte)c.V1);
        c.V1 = 0x00100000u;
        c.V0 = c.V0 | c.V1;
        mem.WriteU32((c.S1 + 0x34u), c.V0);
        c.RA = 0x8016D41Cu;
        SoTN_main.rand(c, m);
        if ((int)c.V0 >= 0) {
            c.S0 = c.V0;
            goto L8016D428;
        }
        c.S0 = c.V0;
        c.S0 = c.V0 + 0x1FFu;
        L8016D428: ;
        c.S0 = (uint)((int)c.S0 >> 9);
        c.S0 = c.S0 << 9;
        c.S0 = c.V0 - c.S0;
        c.S0 = c.S0 + 0x300u;
        c.A0 = c.S0;
        c.RA = 0x8016D440u;
        SoTN_main.rcos(c, m);
        c.A0 = c.S0;
        c.V0 = c.V0 << 5;
        mem.WriteU32((c.S1 + 0x8u), c.V0);
        c.RA = 0x8016D450u;
        SoTN_main.rsin(c, m);
        c.V0 = c.V0 << 5;
        c.V1 = mem.ReadU16((c.S1 + 0x2Cu));
        c.V0 = 0u - c.V0;
        mem.WriteU32((c.S1 + 0xCu), c.V0);
        c.V1 = c.V1 + 0x1u;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V1);
        goto L8016D4C0;
        L8016D46C: ;
        c.V0 = mem.ReadU16((c.S1 + 0x7Cu));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V0 < 39 ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L8016D4A0;
        }
        L8016D490: ;
        c.A0 = c.S1;
        c.RA = 0x8016D498u;
        SoTN_ric.DestroyEntity_ric(c, m);
        goto L8016D4C0;
        L8016D4A0: ;
        c.V0 = mem.ReadU32(c.S1);
        c.A0 = mem.ReadU32((c.S1 + 0x8u));
        c.V1 = mem.ReadU32((c.S1 + 0x4u));
        c.A1 = mem.ReadU32((c.S1 + 0xCu));
        c.V0 = c.V0 + c.A0;
        c.V1 = c.V1 + c.A1;
        mem.WriteU32(c.S1, c.V0);
        mem.WriteU32((c.S1 + 0x4u), c.V1);
        L8016D4C0: ;
        c.RA = mem.ReadU32((c.SP + 0x18u));
        c.S1 = mem.ReadU32((c.SP + 0x14u));
        c.S0 = mem.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x20u;
        return;
        c.SP = c.SP - 0x30u;
        mem.WriteU32((c.SP + 0x24u), c.S3);
        c.S3 = c.A0;
        mem.WriteU32((c.SP + 0x28u), c.RA);
        mem.WriteU32((c.SP + 0x20u), c.S2);
        mem.WriteU32((c.SP + 0x1Cu), c.S1);
        mem.WriteU32((c.SP + 0x18u), c.S0);
        c.V1 = mem.ReadU16((c.S3 + 0x2Cu));
        c.V0 = 0u | 0x0001u;
        if (c.V1 == c.V0) {
            c.V0 = (int)c.V1 < 2 ? 1u : 0u;
            goto L8016D60C;
        }
        c.V0 = (int)c.V1 < 2 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L8016D51C;
        }
        if (c.V1 == 0u) {
            c.A0 = 0u | 0x0001u;
            goto L8016D538;
        }
        c.A0 = 0u | 0x0001u;
        goto L8016D900;
        L8016D51C: ;
        c.V0 = 0u | 0x0002u;
        if (c.V1 == c.V0) {
            c.V0 = 0u | 0x0003u;
            goto L8016D788;
        }
        c.V0 = 0u | 0x0003u;
        if (c.V1 == c.V0) {
            c.V0 = 0u | 0x0001u;
            goto L8016D8F0;
        }
        c.V0 = 0u | 0x0001u;
        goto L8016D900;
        L8016D538: ;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0009u;
        c.RA = 0x8016D54Cu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V1 = 0xFFFFFFFFu;
        if (c.V0 != c.V1) {
            mem.WriteU32((c.S3 + 0x64u), c.V0);
            goto L8016D57C;
        }
        mem.WriteU32((c.S3 + 0x64u), c.V0);
        c.A0 = c.S3;
        c.RA = 0x8016D568u;
        SoTN_ric.DestroyEntity_ric(c, m);
        c.V0 = 0u | 0x0001u;
        c.At = 0x80070000u;
        mem.WriteU16((c.At + 0x2F6Eu), (ushort)c.V0);
        goto L8016D900;
        L8016D57C: ;
        c.S0 = 0u;
        c.A0 = 0u | 0x00FFu;
        c.A1 = 0u | 0x0001u;
        c.A3 = 0x80070000u;
        c.A3 = c.A3 + 0x33FCu;
        c.A2 = 0u | 0x000Au;
        c.V1 = mem.ReadU32((c.S3 + 0x64u));
        c.V0 = 0x04820000u;
        mem.WriteU32((c.S3 + 0x34u), c.V0);
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.S2 = c.V0 + c.V1;
        L8016D5C0: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU8((c.S2 + 0x6u), (byte)c.A0);
        mem.WriteU8((c.S2 + 0x5u), (byte)c.A0);
        mem.WriteU8((c.S2 + 0x4u), (byte)c.A0);
        mem.WriteU8((c.S2 + 0xDu), (byte)c.A1);
        mem.WriteU8((c.S2 + 0xCu), (byte)c.A1);
        c.V0 = mem.ReadU16(c.A3);
        c.S0 = c.S0 + 0x1u;
        mem.WriteU16((c.S2 + 0x32u), (ushort)c.A2);
        c.V0 = c.V0 + 0x8u;
        mem.WriteU16((c.S2 + 0x26u), (ushort)c.V0);
        c.V0 = (int)c.S0 < 9 ? 1u : 0u;
        c.S2 = mem.ReadU32(c.S2);
        if (c.V0 != 0u) {
            goto L8016D5C0;
        }
        c.V0 = mem.ReadU16((c.S3 + 0x2Cu));
        c.At = 0x80170000u;
        mem.WriteU32((c.At + 0x5890u), 0u);
        c.V0 = c.V0 + 0x1u;
        goto L8016D8E8;
        L8016D60C: ;
        c.V0 = mem.ReadU16((c.S3 + 0x7Eu));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S3 + 0x7Eu), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S3 + 0x7Eu));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 != 0u) {
            goto L8016D788;
        }
        c.V0 = (uint)(short)mem.ReadU16((c.S3 + 0x7Cu));
        c.V1 = c.V0;
        c.V0 = (int)c.V0 < 8 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V1 = c.V1 + 0x1u;
            goto L8016D788;
        }
        c.V1 = c.V1 + 0x1u;
        c.V0 = mem.ReadU16((c.S3 + 0x80u));
        mem.WriteU16((c.S3 + 0x7Cu), (ushort)c.V1);
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S3 + 0x80u), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V0 < 48 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.S0 = 0u;
            goto L8016D67C;
        }
        c.S0 = 0u;
        c.V0 = mem.ReadU16((c.S3 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S3 + 0x2Cu), (ushort)c.V0);
        L8016D67C: ;
        c.V0 = mem.ReadU32((c.S3 + 0x64u));
        c.V1 = c.V0 << 1;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.V0 = 0x80080000u;
        c.V0 = c.V0 + 0x6FECu;
        c.S2 = c.V1 + c.V0;
        L8016D6A4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU16((c.S2 + 0x32u));
        c.V0 = c.V0 & 0x0008u;
        if (c.V0 != 0u) {
            c.V1 = 0xFFE80000u;
            goto L8016D6CC;
        }
        c.V1 = 0xFFE80000u;
        c.S2 = mem.ReadU32(c.S2);
        c.S0 = c.S0 + 0x1u;
        c.V0 = (int)c.S0 < 9 ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L8016D6A4;
        }
        L8016D6CC: ;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x33D8u));
        mem.WriteU32((c.S2 + 0x10u), c.V0);
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 + 0x33DCu));
        c.V0 = c.V0 + c.V1;
        mem.WriteU32((c.S2 + 0x14u), c.V0);
        c.RA = 0x8016D6F4u;
        SoTN_main.rand(c, m);
        if ((int)c.V0 >= 0) {
            c.S1 = c.V0;
            goto L8016D700;
        }
        c.S1 = c.V0;
        c.S1 = c.V0 + 0x1FFu;
        L8016D700: ;
        c.S1 = (uint)((int)c.S1 >> 9);
        c.S1 = c.S1 << 9;
        c.S1 = c.V0 - c.S1;
        c.S1 = c.S1 + 0x300u;
        c.RA = 0x8016D714u;
        SoTN_main.rand(c, m);
        c.V1 = 0x2AAA0000u;
        c.V1 = c.V1 | 0xAAABu;
        c.S0 = c.V0;
        { var _r = (long)(int)c.S0 * (int)c.V1; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.A0 = c.S1;
        c.V0 = (uint)((int)c.S0 >> 31);
        c.V1 = c.HI;
        c.V1 = (uint)((int)c.V1 >> 2);
        c.V1 = c.V1 - c.V0;
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 3;
        c.S0 = c.S0 - c.V0;
        c.S0 = c.S0 + 0x20u;
        c.RA = 0x8016D750u;
        SoTN_main.rcos(c, m);
        { var _r = (long)(int)c.V0 * (int)c.S0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.A0 = c.S1;
        c.V0 = c.LO;
        mem.WriteU32((c.S2 + 0x18u), c.V0);
        c.RA = 0x8016D764u;
        SoTN_main.rsin(c, m);
        { var _r = (long)(int)c.V0 * (int)c.S0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V0 = mem.ReadU16((c.S2 + 0x32u));
        c.V1 = 0u | 0x0010u;
        mem.WriteU16((c.S2 + 0x24u), (ushort)c.V1);
        c.V0 = c.V0 & 0xFFF7u;
        mem.WriteU16((c.S2 + 0x32u), (ushort)c.V0);
        c.V0 = c.LO;
        c.V0 = 0u - c.V0;
        mem.WriteU32((c.S2 + 0x1Cu), c.V0);
        L8016D788: ;
        c.V0 = 0x80170000u;
        c.V0 = mem.ReadU32((c.V0 + 0x5890u));
        c.V0 = c.V0 + 0x1u;
        c.At = 0x80170000u;
        mem.WriteU32((c.At + 0x5890u), c.V0);
        c.V0 = c.V0 & 0x0007u;
        if (c.V0 != 0u) {
            c.S0 = 0u;
            goto L8016D7C0;
        }
        c.S0 = 0u;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x0672u;
        c.RA = 0x8016D7C0u;
        Dispatcher.Call(c, m, c.V0);
        L8016D7C0: ;
        c.V0 = mem.ReadU32((c.S3 + 0x64u));
        c.V1 = c.V0 << 1;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.V0 = 0x80080000u;
        c.V0 = c.V0 + 0x6FECu;
        c.S2 = c.V1 + c.V0;
        L8016D7E8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU16((c.S2 + 0x32u));
        c.V0 = c.V0 & 0x0008u;
        if (c.V0 != 0u) {
            goto L8016D8A8;
        }
        c.V0 = mem.ReadU16((c.S2 + 0x24u));
        c.V0 = c.V0 - 0x1u;
        mem.WriteU16((c.S2 + 0x24u), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        if (c.V0 != 0u) {
            c.A0 = c.S3;
            goto L8016D86C;
        }
        c.A0 = c.S3;
        c.V0 = mem.ReadU16((c.S2 + 0x32u));
        c.V0 = c.V0 | 0x0008u;
        mem.WriteU16((c.S2 + 0x32u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S3 + 0x7Cu));
        c.V0 = c.V0 - 0x1u;
        mem.WriteU16((c.S3 + 0x7Cu), (ushort)c.V0);
        c.V0 = mem.ReadU32((c.S2 + 0x10u));
        mem.WriteU32((c.S3 + 0x84u), c.V0);
        c.V0 = mem.ReadU32((c.S2 + 0x14u));
        c.A1 = 0u | 0x0037u;
        mem.WriteU32((c.S3 + 0x88u), c.V0);
        c.V0 = mem.ReadU32((c.S2 + 0x18u));
        c.A2 = 0u;
        c.V0 = (int)c.V0 < 1 ? 1u : 0u;
        mem.WriteU16((c.S3 + 0x8Cu), (ushort)c.V0);
        c.RA = 0x8016D864u;
        SoTN_ric.RicCreateEntFactoryFromEntity(c, m);
        goto L8016D8A8;
        L8016D86C: ;
        c.V0 = mem.ReadU32((c.S2 + 0x10u));
        c.A0 = mem.ReadU32((c.S2 + 0x18u));
        c.V1 = mem.ReadU32((c.S2 + 0x14u));
        c.A1 = mem.ReadU32((c.S2 + 0x1Cu));
        c.V0 = c.V0 + c.A0;
        mem.WriteU32((c.S2 + 0x10u), c.V0);
        c.V0 = mem.ReadU32((c.S2 + 0x1Cu));
        c.V1 = c.V1 + c.A1;
        mem.WriteU32((c.S2 + 0x14u), c.V1);
        c.V1 = mem.ReadU16((c.S2 + 0x12u));
        c.A0 = mem.ReadU16((c.S2 + 0x16u));
        c.V0 = c.V0 + 0x4000u;
        mem.WriteU32((c.S2 + 0x1Cu), c.V0);
        mem.WriteU16((c.S2 + 0x8u), (ushort)c.V1);
        mem.WriteU16((c.S2 + 0xAu), (ushort)c.A0);
        L8016D8A8: ;
        c.S2 = mem.ReadU32(c.S2);
        c.S0 = c.S0 + 0x1u;
        c.V0 = (int)c.S0 < 9 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0002u;
            goto L8016D7E8;
        }
        c.V0 = 0u | 0x0002u;
        c.V1 = mem.ReadU16((c.S3 + 0x2Cu));
        if (c.V1 != c.V0) {
            goto L8016D900;
        }
        c.V0 = (uint)(short)mem.ReadU16((c.S3 + 0x7Cu));
        if (c.V0 != 0u) {
            goto L8016D900;
        }
        c.V0 = mem.ReadU16((c.S3 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        L8016D8E8: ;
        mem.WriteU16((c.S3 + 0x2Cu), (ushort)c.V0);
        goto L8016D900;
        L8016D8F0: ;
        c.At = 0x80070000u;
        mem.WriteU16((c.At + 0x2F6Eu), (ushort)c.V0);
        c.A0 = c.S3;
        c.RA = 0x8016D900u;
        SoTN_ric.DestroyEntity_ric(c, m);
        L8016D900: ;
        c.RA = mem.ReadU32((c.SP + 0x28u));
        c.S3 = mem.ReadU32((c.SP + 0x24u));
        c.S2 = mem.ReadU32((c.SP + 0x20u));
        c.S1 = mem.ReadU32((c.SP + 0x1Cu));
        c.S0 = mem.ReadU32((c.SP + 0x18u));
        c.SP = c.SP + 0x30u;
        return;
        c.SP = c.SP - 0x18u;
        mem.WriteU32((c.SP + 0x10u), c.S0);
        c.S0 = c.A0;
        mem.WriteU32((c.SP + 0x14u), c.RA);
        c.V1 = mem.ReadU16((c.S0 + 0x2Cu));
        if (c.V1 == 0u) {
            c.V0 = 0u | 0x0001u;
            goto L8016D950;
        }
        c.V0 = 0u | 0x0001u;
        if (c.V1 == c.V0) {
            goto L8016D984;
        }
        goto L8016D9B0;
        L8016D950: ;
        c.A0 = c.S0;
        c.V0 = 0x04000000u;
        mem.WriteU32((c.S0 + 0x34u), c.V0);
        c.V0 = 0u | 0x0019u;
        mem.WriteU16((c.S0 + 0xB0u), (ushort)c.V0);
        c.RA = 0x8016D968u;
        SoTN_ric.RicSetSubweaponParams(c, m);
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu));
        c.V1 = 0u | 0x0004u;
        mem.WriteU8((c.S0 + 0x46u), (byte)c.V1);
        mem.WriteU8((c.S0 + 0x47u), (byte)c.V1);
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        goto L8016D9B0;
        L8016D984: ;
        c.V0 = mem.ReadU16((c.S0 + 0x7Cu));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S0 + 0x7Cu), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V0 < 4 ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L8016D9B0;
        }
        c.A0 = c.S0;
        c.RA = 0x8016D9B0u;
        SoTN_ric.DestroyEntity_ric(c, m);
        L8016D9B0: ;
        c.RA = mem.ReadU32((c.SP + 0x14u));
        c.S0 = mem.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x18u;
        return;
        c.SP = c.SP - 0x20u;
        mem.WriteU32((c.SP + 0x18u), c.S2);
        c.S2 = c.A0;
        mem.WriteU32((c.SP + 0x1Cu), c.RA);
        mem.WriteU32((c.SP + 0x14u), c.S1);
        mem.WriteU32((c.SP + 0x10u), c.S0);
        c.V1 = mem.ReadU16((c.S2 + 0x2Cu));
        c.V0 = 0u | 0x0001u;
        if (c.V1 == c.V0) {
            c.V0 = (int)c.V1 < 2 ? 1u : 0u;
            goto L8016DC34;
        }
        c.V0 = (int)c.V1 < 2 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L8016DA04;
        }
        if (c.V1 == 0u) {
            c.A0 = 0u | 0x0002u;
            goto L8016DA20;
        }
        c.A0 = 0u | 0x0002u;
        SoTN_ric.func_8016DD60(c, m);
        return;
        L8016DA04: ;
        c.V0 = 0u | 0x0002u;
        if (c.V1 == c.V0) {
            c.V0 = 0u | 0x0003u;
            SoTN_ric.func_8016DD08(c, m);
            return;
        }
        c.V0 = 0u | 0x0003u;
        if (c.V1 == c.V0) {
            SoTN_ric.func_8016DD2C(c, m);
            return;
        }
        SoTN_ric.func_8016DD60(c, m);
        return;
        L8016DA20: ;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0014u;
        c.RA = 0x8016DA34u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V1 = 0xFFFFFFFFu;
        if (c.V0 == c.V1) {
            mem.WriteU32((c.S2 + 0x64u), c.V0);
            SoTN_ric.func_8016DD50(c, m);
            return;
        }
        mem.WriteU32((c.S2 + 0x64u), c.V0);
        c.S1 = 0u;
        c.A1 = 0x80070000u;
        c.A1 = c.A1 + 0x33D8u;
        c.T2 = 0xFFD80000u;
        c.T1 = 0u | 0x00C2u;
        c.T0 = 0u | 0x0008u;
        c.A3 = 0u | 0x0080u;
        c.A2 = 0u | 0x0070u;
        c.A0 = 0x80150000u;
        c.A0 = c.A0 + 0x5EE0u;
        c.V1 = mem.ReadU32((c.S2 + 0x64u));
        c.V0 = 0x04800000u;
        mem.WriteU32((c.S2 + 0x34u), c.V0);
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.S0 = c.V0 + c.V1;
        L8016DA9C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU32(c.A1);
        mem.WriteU32((c.S0 + 0x28u), c.V0);
        c.V0 = mem.ReadU32((c.A1 + 0x4u));
        mem.WriteU16((c.S0 + 0x26u), (ushort)c.T1);
        mem.WriteU16((c.S0 + 0x32u), (ushort)c.T0);
        c.V0 = c.V0 + c.T2;
        mem.WriteU32((c.S0 + 0x2Cu), c.V0);
        c.V0 = mem.ReadU16((c.A1 + 0x2u));
        mem.WriteU16((c.S0 + 0x14u), (ushort)c.V0);
        mem.WriteU16((c.S0 + 0x8u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.A1 + 0x6u));
        c.S1 = c.S1 + 0x1u;
        mem.WriteU8((c.S0 + 0x6u), (byte)c.A3);
        mem.WriteU8((c.S0 + 0x5u), (byte)c.A3);
        mem.WriteU8((c.S0 + 0x4u), (byte)c.A3);
        mem.WriteU8((c.S0 + 0x12u), (byte)c.A2);
        mem.WriteU8((c.S0 + 0x11u), (byte)c.A2);
        mem.WriteU8((c.S0 + 0x10u), (byte)c.A2);
        c.V0 = c.V0 - 0x1Cu;
        mem.WriteU16((c.S0 + 0x16u), (ushort)c.V0);
        mem.WriteU16((c.S0 + 0xAu), (ushort)c.V0);
        c.V1 = mem.ReadU32(c.A0);
        c.V0 = 0u | 0x0001u;
        mem.WriteU16((c.S0 + 0x24u), (ushort)c.V0);
        c.V0 = (int)c.S1 < 4 ? 1u : 0u;
        mem.WriteU32((c.S0 + 0x20u), c.V1);
        c.S0 = mem.ReadU32(c.S0);
        if (c.V0 != 0u) {
            c.A0 = c.A0 + 0x4u;
            goto L8016DA9C;
        }
        c.A0 = c.A0 + 0x4u;
        c.V0 = (int)c.S1 < 20 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.A1 = 0u | 0x0080u;
            goto L8016DC08;
        }
        c.A1 = 0u | 0x0080u;
        c.A3 = 0u | 0x0002u;
        c.A2 = 0x80070000u;
        c.A2 = c.A2 + 0x33DAu;
        c.V0 = c.S1 & 0x0003u;
        L8016DB34: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.V0 != 0u) {
            goto L8016DBA8;
        }
        c.A1 = c.A1 - 0x10u;
        if ((int)c.S1 >= 0) {
            c.V0 = c.S1;
            goto L8016DB4C;
        }
        c.V0 = c.S1;
        c.V0 = c.S1 + 0x3u;
        L8016DB4C: ;
        c.V1 = (uint)((int)c.V0 >> 2);
        if (c.V1 == c.A3) {
            c.V0 = (int)c.V1 < 3 ? 1u : 0u;
            goto L8016DB94;
        }
        c.V0 = (int)c.V1 < 3 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = 0u | 0x0001u;
            goto L8016DB70;
        }
        c.V0 = 0u | 0x0001u;
        if (c.V1 == c.V0) {
            c.A0 = c.A1 - 0x10u;
            goto L8016DB8C;
        }
        c.A0 = c.A1 - 0x10u;
        goto L8016DBAC;
        L8016DB70: ;
        c.V0 = 0u | 0x0003u;
        if (c.V1 == c.V0) {
            c.V0 = 0u | 0x0004u;
            goto L8016DB9C;
        }
        c.V0 = 0u | 0x0004u;
        if (c.V1 == c.V0) {
            c.A0 = c.A1 - 0x10u;
            goto L8016DBA4;
        }
        c.A0 = c.A1 - 0x10u;
        goto L8016DBAC;
        L8016DB8C: ;
        mem.WriteU32((c.S2 + 0x7Cu), c.S0);
        goto L8016DBA8;
        L8016DB94: ;
        mem.WriteU32((c.S2 + 0x80u), c.S0);
        goto L8016DBA8;
        L8016DB9C: ;
        mem.WriteU32((c.S2 + 0x84u), c.S0);
        goto L8016DBA8;
        L8016DBA4: ;
        mem.WriteU32((c.S2 + 0x88u), c.S0);
        L8016DBA8: ;
        c.A0 = c.A1 - 0x10u;
        L8016DBAC: ;
        c.V0 = 0u | 0x00C2u;
        mem.WriteU16((c.S0 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0008u;
        mem.WriteU16((c.S0 + 0x32u), (ushort)c.V0);
        c.V0 = mem.ReadU16(c.A2);
        c.S1 = c.S1 + 0x1u;
        mem.WriteU16((c.S0 + 0x14u), (ushort)c.V0);
        mem.WriteU16((c.S0 + 0x8u), (ushort)c.V0);
        c.V1 = mem.ReadU16((c.A2 + 0x4u));
        c.V0 = c.A1;
        mem.WriteU8((c.S0 + 0x6u), (byte)c.V0);
        mem.WriteU8((c.S0 + 0x5u), (byte)c.V0);
        mem.WriteU8((c.S0 + 0x4u), (byte)c.V0);
        c.V0 = (int)c.S1 < 20 ? 1u : 0u;
        mem.WriteU8((c.S0 + 0x12u), (byte)c.A0);
        mem.WriteU8((c.S0 + 0x11u), (byte)c.A0);
        mem.WriteU8((c.S0 + 0x10u), (byte)c.A0);
        c.V1 = c.V1 - 0x1Cu;
        mem.WriteU16((c.S0 + 0x16u), (ushort)c.V1);
        mem.WriteU16((c.S0 + 0xAu), (ushort)c.V1);
        c.S0 = mem.ReadU32(c.S0);
        if (c.V0 != 0u) {
            c.V0 = c.S1 & 0x0003u;
            goto L8016DB34;
        }
        c.V0 = c.S1 & 0x0003u;
        L8016DC08: ;
        c.V0 = 0u | 0x0004u;
        mem.WriteU16((c.S2 + 0x90u), (ushort)c.V0);
        mem.WriteU16((c.S2 + 0x8Eu), (ushort)0u);
        mem.WriteU16((c.S2 + 0x8Cu), (ushort)0u);
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x0623u;
        c.RA = 0x8016DC2Cu;
        Dispatcher.Call(c, m, c.V0);
        SoTN_ric.func_8016DD18(c, m);
        return;
        L8016DC34: ;
        c.V1 = (uint)(short)mem.ReadU16((c.S2 + 0x8Cu));
        c.V0 = 0u | 0x0001u;
        mem.WriteU16((c.S2 + 0x8Eu), (ushort)c.V0);
        c.V0 = c.V1 < 0x00000005u ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = c.V1 << 2;
            SoTN_ric.func_8016DCB8(c, m);
            return;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x80150000u;
        c.At = c.At + c.V0;
        c.V0 = mem.ReadU32((c.At + 0x6B38u));
        switch (c.V0)
        {
            case 0x8016DC64u: goto L8016DC64;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L8016DC64: ;
        c.V0 = mem.ReadU32((c.S2 + 0x64u));
        c.V1 = c.V0 << 1;
        c.V1 = c.V1 + c.V0;
        Dispatcher.Call(c, m, 0x8016DC74u);
    }
}
