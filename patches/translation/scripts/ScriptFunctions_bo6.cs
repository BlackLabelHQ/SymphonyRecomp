using RecompOne.Runtime.Context;
using RecompOne.Runtime.Dispatch;
using RecompOne.Runtime.Memory;

namespace Recompiled.Translation;

public static class ScriptFunctions_bo6
{
    public static bool CutsceneUnk1_bo6(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo6", m, true)) return true;
        CutsceneUnk1_bo6_Body(c, m);
        return false;
    }
    private static void CutsceneUnk1_bo6_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo6", 0x801D1114u);
        c.V0 = 0u | 0x0102u;
        c.At = 0x801D0000u;
        mem.WriteU16((c.At + 0x1120u), (ushort)c.V0);
        c.At = 0x801D0000u;
        mem.WriteU16((c.At + 0x111Eu), (ushort)c.V0);
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU16((c.V0 + 0x111Cu));
        c.V1 = 0u | 0x0008u;
        c.At = 0x801D0000u;
        mem.WriteU16((c.At + 0x1122u), (ushort)0u);
        c.At = 0x801D0000u;
        mem.WriteU16((c.At + 0x1126u), (ushort)0u);
        c.At = 0x801D0000u;
        mem.WriteU8((c.At + 0x112Au), (byte)0u);
        c.At = 0x801D0000u;
        mem.WriteU8((c.At + 0x112Bu), (byte)c.V1);
        c.V0 = c.V0 + 0x14u;
        c.At = 0x801D0000u;
        mem.WriteU16((c.At + 0x111Au), (ushort)c.V0);
        return;
        }
    public static bool SetCutsceneScript_bo6(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo6", m, true)) return true;
        SetCutsceneScript_bo6_Body(c, m);
        return false;
    }
    private static void SetCutsceneScript_bo6_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo6", 0x801D1114u);
        c.A0 = DialogueScripts.ResolveStart("bo6", c.A0, m);
        c.SP = c.SP - 0x20u;
        mem.WriteU32((c.SP + 0x14u), c.S1);
        c.S1 = c.A0;
        c.A0 = 0u | 0x0006u;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0007u;
        mem.WriteU32((c.SP + 0x18u), c.RA);
        mem.WriteU32((c.SP + 0x10u), c.S0);
        c.RA = 0x801A749Cu;
        Dispatcher.Call(c, m, c.V0);
        c.S0 = 0x801D0000u;
        c.S0 = c.S0 + 0x114Cu;
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V1 = 0xFFFFFFFFu;
        if (c.V0 != c.V1) {
            mem.WriteU32(c.S0, c.V0);
            goto L801A74C4;
        }
        mem.WriteU32(c.S0, c.V0);
        mem.WriteU32(c.S0, 0u);
        c.V0 = 0u;
        goto L801A7620;
        L801A74C4: ;
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x1114u), c.S1);
        c.At = 0x801D0000u;
        mem.WriteU16((c.At + 0x1150u), (ushort)0u);
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x1148u), c.V1);
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x1144u), c.V1);
        c.RA = 0x801A74ECu;
        SoTN_bo6.CutsceneUnk1_bo6(c, m);
        c.V1 = mem.ReadU32(c.S0);
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.V0 = c.V0 + c.V1;
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x112Cu), c.V0);
        c.V1 = mem.ReadU32(c.V0);
        c.A2 = 0u | 0x0008u;
        mem.WriteU16((c.V0 + 0x32u), (ushort)c.A2);
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x1130u), c.V1);
        c.V0 = mem.ReadU32(c.V1);
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A2);
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x1134u), c.V0);
        c.V1 = mem.ReadU32(c.V0);
        mem.WriteU16((c.V0 + 0x32u), (ushort)c.A2);
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x1138u), c.V1);
        c.A0 = mem.ReadU32(c.V1);
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A2);
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x113Cu), c.A0);
        c.A1 = mem.ReadU32(c.A0);
        c.V0 = 0u | 0x0001u;
        mem.WriteU16((c.A0 + 0x32u), (ushort)c.A2);
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x1140u), c.A1);
        c.V1 = mem.ReadU32(c.A1);
        c.A0 = 0u | 0x0004u;
        mem.WriteU8((c.A1 + 0x7u), (byte)c.A0);
        c.A0 = 0u | 0x0003u;
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.A2);
        c.A1 = 0u | 0x00FFu;
        mem.WriteU8((c.V1 + 0x7u), (byte)c.A0);
        c.A0 = 0u | 0x0004u;
        mem.WriteU8((c.V1 + 0x28u), (byte)c.A1);
        mem.WriteU8((c.V1 + 0x1Cu), (byte)c.A1);
        mem.WriteU8((c.V1 + 0x10u), (byte)c.A1);
        mem.WriteU8((c.V1 + 0x4u), (byte)c.A1);
        mem.WriteU8((c.V1 + 0x29u), (byte)0u);
        mem.WriteU8((c.V1 + 0x1Du), (byte)0u);
        mem.WriteU8((c.V1 + 0x11u), (byte)0u);
        mem.WriteU8((c.V1 + 0x5u), (byte)0u);
        mem.WriteU8((c.V1 + 0x2Au), (byte)0u);
        mem.WriteU8((c.V1 + 0x1Eu), (byte)0u);
        mem.WriteU8((c.V1 + 0x12u), (byte)0u);
        mem.WriteU8((c.V1 + 0x6u), (byte)0u);
        mem.WriteU16((c.V1 + 0x20u), (ushort)c.A0);
        mem.WriteU16((c.V1 + 0x8u), (ushort)c.A0);
        c.A0 = 0u | 0x00F8u;
        mem.WriteU16((c.V1 + 0x2Cu), (ushort)c.A0);
        mem.WriteU16((c.V1 + 0x14u), (ushort)c.A0);
        c.A0 = 0u | 0x01FDu;
        mem.WriteU16((c.V1 + 0x26u), (ushort)c.A0);
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A2);
        c.V1 = mem.ReadU32(c.V1);
        c.A0 = 0u | 0x0001u;
        mem.WriteU8((c.V1 + 0x7u), (byte)c.A0);
        c.A0 = 0u | 0x0003u;
        mem.WriteU16((c.V1 + 0x8u), (ushort)c.A0);
        c.A0 = 0u | 0x002Fu;
        mem.WriteU16((c.V1 + 0xAu), (ushort)c.A0);
        c.A0 = 0u | 0x004Au;
        mem.WriteU8((c.V1 + 0xDu), (byte)c.A0);
        c.A0 = 0u | 0x01FCu;
        mem.WriteU8((c.V1 + 0x6u), (byte)c.A1);
        mem.WriteU8((c.V1 + 0x5u), (byte)c.A1);
        mem.WriteU8((c.V1 + 0x4u), (byte)c.A1);
        mem.WriteU16((c.V1 + 0x26u), (ushort)c.A0);
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A2);
        L801A7620: ;
        c.RA = mem.ReadU32((c.SP + 0x18u));
        c.S1 = mem.ReadU32((c.SP + 0x14u));
        c.S0 = mem.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x20u;
        return;
        }
    public static bool CutsceneUnk3_bo6(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo6", m, false)) return true;
        CutsceneUnk3_bo6_Body(c, m);
        return false;
    }
    private static void CutsceneUnk3_bo6_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo6", 0x801D1114u);
        c.SP = c.SP - 0x20u;
        c.V0 = 0u | 0x0100u;
        c.A0 = c.A0 << 16;
        c.A0 = (uint)((int)c.A0 >> 16);
        mem.WriteU16((c.SP + 0x10u), (ushort)c.V0);
        c.V0 = c.A0 << 1;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + 0x180u;
        mem.WriteU16((c.SP + 0x12u), (ushort)c.V0);
        c.V0 = 0u | 0x0040u;
        mem.WriteU16((c.SP + 0x14u), (ushort)c.V0);
        c.V0 = 0u | 0x000Cu;
        c.A0 = c.SP + 0x10u;
        c.A1 = 0u;
        c.A2 = 0u;
        c.A3 = 0u;
        mem.WriteU32((c.SP + 0x18u), c.RA);
        mem.WriteU16((c.SP + 0x16u), (ushort)c.V0);
        c.RA = 0x801A7688u;
        DialogueGlyphs.Clear(c, mem);
        c.RA = mem.ReadU32((c.SP + 0x18u));
        c.SP = c.SP + 0x20u;
        return;
        }
    public static bool CutsceneUnk4_bo6(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo6", m, true)) return true;
        CutsceneUnk4_bo6_Body(c, m);
        return false;
    }
    private static void CutsceneUnk4_bo6_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo6", 0x801D1114u);
        DialogueGlyphs.PrepareLine(c, mem);
        c.SP = c.SP - 0x18u;
        mem.WriteU32((c.SP + 0x10u), c.S0);
        c.S0 = 0x801D0000u;
        c.S0 = c.S0 + 0x1122u;
        mem.WriteU32((c.SP + 0x14u), c.RA);
        c.A0 = (uint)(short)mem.ReadU16(c.S0);
        c.RA = 0x801A76B8u;
        SoTN_bo6.CutsceneUnk3_bo6(c, m);
        c.V0 = (uint)(short)mem.ReadU16(c.S0);
        c.V0 = c.V0 << 2;
        c.V0 = c.S0 + c.V0;
        c.V1 = mem.ReadU32((c.V0 + 0xAu));
        c.V0 = 0u | 0x0014u;
        mem.WriteU16((c.V1 + 0x1Au), (ushort)c.V0);
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU16((c.V0 + 0x1128u));
        mem.WriteU16((c.V1 + 0xEu), (ushort)c.V0);
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU16((c.V0 + 0x111Au));
        mem.WriteU8((c.V1 + 0xCu), (byte)0u);
        mem.WriteU16((c.V1 + 0xAu), (ushort)c.V0);
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU16((c.V0 + 0x1118u));
        c.V0 = c.V0 + 0x4u;
        mem.WriteU16((c.V1 + 0x8u), (ushort)c.V0);
        c.A0 = (uint)(short)mem.ReadU16(c.S0);
        c.V0 = 0u | 0x00C0u;
        mem.WriteU8((c.V1 + 0x18u), (byte)c.V0);
        c.V0 = 0u | 0x000Cu;
        mem.WriteU8((c.V1 + 0x19u), (byte)c.V0);
        c.V0 = 0u | 0x01FFu;
        mem.WriteU16((c.V1 + 0x26u), (ushort)c.V0);
        mem.WriteU16((c.V1 + 0x32u), (ushort)0u);
        c.V0 = c.A0 << 1;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 - 0x80u;
        mem.WriteU8((c.V1 + 0xDu), (byte)c.V0);
        c.RA = mem.ReadU32((c.SP + 0x14u));
        c.S0 = mem.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x18u;
        DialogueGlyphs.BeginLine(mem);
        return;
        }
    public static bool DrawCutsceneActorName_bo6(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo6", m, true)) return true;
        DrawCutsceneActorName_bo6_Body(c, m);
        return false;
    }
    private static void DrawCutsceneActorName_bo6_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo6", 0x801D1114u);
        c.SP = c.SP - 0x20u;
        mem.WriteU32((c.SP + 0x14u), c.S1);
        c.S1 = c.A1;
        mem.WriteU32((c.SP + 0x10u), c.S0);
        c.S0 = c.A0;
        c.V0 = c.S0 & 0xFFFFu;
        c.V0 = c.V0 << 2;
        c.A1 = 0u;
        mem.WriteU32((c.SP + 0x18u), c.RA);
        c.At = 0x80180000u;
        c.At = c.At + c.V0;
        c.A2 = mem.ReadU32((c.At + 0x6BCu));
        c.V0 = 0u | 0x00FFu;
        L801A7784: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V1 = mem.ReadU8(c.A2);
        if (c.V1 != c.V0) {
            c.A2 = c.A2 + 0x1u;
            goto L801A77A4;
        }
        c.A2 = c.A2 + 0x1u;
        c.V1 = mem.ReadU8(c.A2);
        if (c.V1 == 0u) {
            c.A2 = c.A2 + 0x1u;
            goto L801A77B4;
        }
        c.A2 = c.A2 + 0x1u;
        L801A77A4: ;
        if (c.V1 == 0u) {
            goto L801A7784;
        }
        c.A1 = c.A1 + 0x1u;
        goto L801A7784;
        L801A77B4: ;
        c.A0 = 0u | 0x0006u;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3848u));
        c.A1 = c.A1 & 0xFFFFu;
        c.RA = 0x801A77CCu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.A1 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.A1 != c.V0) {
            c.V0 = c.A1 << 1;
            goto L801A77F0;
        }
        c.V0 = c.A1 << 1;
        c.A0 = c.S1;
        c.RA = 0x801A77E8u;
        SoTN_bo6.DestroyEntity_bo6(c, m);
        goto L801A78BC;
        L801A77F0: ;
        c.V0 = c.V0 + c.A1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.A1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A0 = c.V0 + c.V1;
        c.V1 = 0x801D0000u;
        c.V1 = c.V1 + 0x1148u;
        c.V0 = c.S0 & 0xFFFFu;
        c.V0 = c.V0 << 2;
        mem.WriteU32(c.V1, c.A1);
        c.At = 0x80180000u;
        c.At = c.At + c.V0;
        c.A2 = mem.ReadU32((c.At + 0x6BCu));
        if (c.A0 == 0u) {
            c.A3 = 0u | 0x0038u;
            goto L801A78BC;
        }
        c.A3 = 0u | 0x0038u;
        c.T4 = 0u | 0x0006u;
        c.T3 = 0u | 0x001Eu;
        c.T2 = 0u | 0x0196u;
        c.T1 = 0u | 0x01FFu;
        c.A1 = 0u | 0x0008u;
        c.T0 = c.V1 - 0x2Cu;
        L801A784C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V1 = mem.ReadU8(c.A2);
        if (c.V1 != 0u) {
            c.A2 = c.A2 + 0x1u;
            goto L801A7864;
        }
        c.A2 = c.A2 + 0x1u;
        c.A3 = c.A3 + 0x4u;
        goto L801A78B0;
        L801A7864: ;
        c.V0 = c.V1 & 0x000Fu;
        c.V0 = c.V0 << 3;
        mem.WriteU8((c.A0 + 0xCu), (byte)c.V0);
        c.V0 = c.V1 & 0x00F0u;
        c.V0 = c.V0 >> 1;
        mem.WriteU8((c.A0 + 0x7u), (byte)c.T4);
        mem.WriteU16((c.A0 + 0x1Au), (ushort)c.T3);
        mem.WriteU16((c.A0 + 0xEu), (ushort)c.T2);
        mem.WriteU8((c.A0 + 0xDu), (byte)c.V0);
        mem.WriteU8((c.A0 + 0x19u), (byte)c.A1);
        mem.WriteU8((c.A0 + 0x18u), (byte)c.A1);
        mem.WriteU16((c.A0 + 0x26u), (ushort)c.T1);
        mem.WriteU16((c.A0 + 0x32u), (ushort)c.A1);
        mem.WriteU16((c.A0 + 0x8u), (ushort)c.A3);
        c.V0 = mem.ReadU16(c.T0);
        c.A3 = c.A3 + 0x8u;
        c.V0 = c.V0 + 0x6u;
        mem.WriteU16((c.A0 + 0xAu), (ushort)c.V0);
        c.A0 = mem.ReadU32(c.A0);
        L801A78B0: ;
        if (c.A0 != 0u) {
            goto L801A784C;
        }
        L801A78BC: ;
        c.RA = mem.ReadU32((c.SP + 0x18u));
        c.S1 = mem.ReadU32((c.SP + 0x14u));
        c.S0 = mem.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x20u;
        return;
        }
    public static bool SetCutsceneEnd_bo6(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo6", m, true)) return true;
        SetCutsceneEnd_bo6_Body(c, m);
        return false;
    }
    private static void SetCutsceneEnd_bo6_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo6", 0x801D1114u);
        c.V0 = 0x00100000u;
        c.A0 = c.A0 + c.V0;
        c.V0 = 0u | 0x0001u;
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x1154u), c.A0);
        c.At = 0x801D0000u;
        mem.WriteU16((c.At + 0x1152u), (ushort)0u);
        c.At = 0x801D0000u;
        mem.WriteU16((c.At + 0x1150u), (ushort)c.V0);
        return;
        }
    public static bool CutsceneRun_bo6(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo6", m, true)) return true;
        CutsceneRun_bo6_Body(c, m);
        return false;
    }
    private static void CutsceneRun_bo6_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo6", 0x801D1114u);
        c.SP = c.SP - 0x20u;
        c.A0 = 0x801D0000u;
        c.A0 = c.A0 + 0x1152u;
        mem.WriteU32((c.SP + 0x1Cu), c.RA);
        mem.WriteU32((c.SP + 0x18u), c.S2);
        mem.WriteU32((c.SP + 0x14u), c.S1);
        mem.WriteU32((c.SP + 0x10u), c.S0);
        c.V0 = mem.ReadU16(c.A0);
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16(c.A0, (ushort)c.V0);
        c.V1 = mem.ReadU16(c.A0);
        c.V0 = 0u | 0xFFFEu;
        c.V0 = c.V0 < c.V1 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.S1 = c.A0 + 0x2u;
            goto L801A7960;
        }
        c.S1 = c.A0 + 0x2u;
        c.At = 0x801D0000u;
        mem.WriteU16((c.At + 0x1150u), (ushort)0u);
        goto L801A7BC0;
        L801A7950: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU32(c.S1, c.A1);
        goto L801A7BC0;
        L801A7958: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU32(c.S1, c.A2);
        goto L801A7BC0;
        L801A7960: ;
        c.S2 = 0u | 0x0001u;
        L801A7964: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A1 = mem.ReadU32(c.S1);
        c.V1 = mem.ReadU16((c.S1 - 0x2u));
        c.V0 = c.A1 + 0x1u;
        mem.WriteU32(c.S1, c.V0);
        c.A0 = mem.ReadU8(c.A1);
        c.A2 = c.A1 + 0x2u;
        mem.WriteU32(c.S1, c.A2);
        c.V0 = mem.ReadU8((c.A1 + 0x1u));
        c.A0 = c.A0 << 8;
        c.V0 = c.V0 | c.A0;
        c.V1 = c.V1 < c.V0 ? 1u : 0u;
        if (c.V1 != 0u) {
            c.V0 = c.A1 + 0x3u;
            goto L801A7950;
        }
        c.V0 = c.A1 + 0x3u;
        mem.WriteU32(c.S1, c.V0);
        c.V1 = mem.ReadU8((c.A1 + 0x2u));
        if (c.V1 == c.S2) {
            c.V0 = (int)c.V1 < 2 ? 1u : 0u;
            goto L801A7B0C;
        }
        c.V0 = (int)c.V1 < 2 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801A79C4;
        }
        if (c.V1 == 0u) {
            c.V0 = c.A1 + 0x4u;
            goto L801A79E0;
        }
        c.V0 = c.A1 + 0x4u;
        goto L801A7964;
        L801A79C4: ;
        c.V0 = 0u | 0x0002u;
        if (c.V1 == c.V0) {
            c.V0 = 0u | 0x0003u;
            goto L801A7B48;
        }
        c.V0 = 0u | 0x0003u;
        if (c.V1 == c.V0) {
            c.V0 = c.A1 + 0x4u;
            goto L801A7B98;
        }
        c.V0 = c.A1 + 0x4u;
        goto L801A7964;
        L801A79E0: ;
        mem.WriteU32(c.S1, c.V0);
        c.V1 = mem.ReadU8((c.A1 + 0x3u));
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 4;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80070000u;
        c.V1 = c.V1 + 0x62D8u;
        c.S0 = c.V0 + c.V1;
        c.A0 = c.S0;
        c.RA = 0x801A7A14u;
        SoTN_bo6.DestroyEntity_bo6(c, m);
        c.V1 = mem.ReadU32(c.S1);
        c.V0 = c.V1 + 0x1u;
        mem.WriteU32(c.S1, c.V0);
        c.V0 = mem.ReadU8(c.V1);
        mem.WriteU16((c.S0 + 0x26u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x26u));
        c.V0 = c.V0 << 2;
        c.At = 0x80180000u;
        c.At = c.At + c.V0;
        c.V0 = mem.ReadU32((c.At + 0x324u));
        mem.WriteU32((c.S0 + 0x28u), c.V0);
        c.V1 = mem.ReadU32(c.S1);
        c.V0 = c.V1 + 0x1u;
        mem.WriteU32(c.S1, c.V0);
        c.V0 = mem.ReadU8(c.V1);
        c.V0 = c.V0 << 4;
        mem.WriteU16((c.S0 + 0x2u), (ushort)c.V0);
        c.V1 = mem.ReadU32(c.S1);
        c.V0 = c.V1 + 0x1u;
        mem.WriteU32(c.S1, c.V0);
        c.V0 = mem.ReadU8(c.V1);
        c.V1 = mem.ReadU16((c.S0 + 0x2u));
        c.V0 = c.V0 | c.V1;
        mem.WriteU16((c.S0 + 0x2u), (ushort)c.V0);
        c.V1 = mem.ReadU32(c.S1);
        c.V0 = c.V1 + 0x1u;
        mem.WriteU32(c.S1, c.V0);
        c.V0 = mem.ReadU8(c.V1);
        c.V0 = c.V0 << 4;
        mem.WriteU16((c.S0 + 0x6u), (ushort)c.V0);
        c.V1 = mem.ReadU32(c.S1);
        c.V0 = c.V1 + 0x1u;
        mem.WriteU32(c.S1, c.V0);
        c.V0 = mem.ReadU8(c.V1);
        c.V1 = mem.ReadU16((c.S0 + 0x6u));
        c.V0 = c.V0 | c.V1;
        mem.WriteU16((c.S0 + 0x6u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x2u));
        c.V1 = 0x80070000u;
        c.V1 = mem.ReadU16((c.V1 + 0x308Eu));
        c.V0 = c.V0 - c.V1;
        mem.WriteU16((c.S0 + 0x2u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x6u));
        c.V1 = 0x80070000u;
        c.V1 = mem.ReadU16((c.V1 + 0x3092u));
        c.V0 = c.V0 - c.V1;
        mem.WriteU16((c.S0 + 0x6u), (ushort)c.V0);
        goto L801A7964;
        L801A7B0C: ;
        c.V0 = c.A1 + 0x4u;
        mem.WriteU32(c.S1, c.V0);
        c.V0 = mem.ReadU8((c.A1 + 0x3u));
        c.A0 = c.V0 << 1;
        c.A0 = c.A0 + c.V0;
        c.A0 = c.A0 << 4;
        c.A0 = c.A0 - c.V0;
        c.A0 = c.A0 << 2;
        c.V0 = 0x80070000u;
        c.V0 = c.V0 + 0x62D8u;
        c.A0 = c.A0 + c.V0;
        c.RA = 0x801A7B40u;
        SoTN_bo6.DestroyEntity_bo6(c, m);
        goto L801A7964;
        L801A7B48: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1154u));
        c.V1 = 0x801D0000u;
        c.V1 = mem.ReadU32((c.V1 + 0x11C4u));
        c.V0 = mem.ReadU8(c.V0);
        c.V0 = c.V1 >> (int)(c.V0 & 31u);
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u) {
            c.V0 = c.A1 + 0x4u;
            goto L801A7958;
        }
        c.V0 = c.A1 + 0x4u;
        mem.WriteU32(c.S1, c.V0);
        c.V0 = mem.ReadU8((c.A1 + 0x3u));
        c.V0 = c.S2 << (int)(c.V0 & 31u);
        c.V0 = ~(0u | c.V0);
        c.V0 = c.V0 & c.V1;
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x11C4u), c.V0);
        goto L801A7964;
        L801A7B98: ;
        mem.WriteU32(c.S1, c.V0);
        c.V0 = mem.ReadU8((c.A1 + 0x3u));
        c.V1 = 0x801D0000u;
        c.V1 = mem.ReadU32((c.V1 + 0x11C4u));
        c.V0 = c.S2 << (int)(c.V0 & 31u);
        c.V0 = c.V0 | c.V1;
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x11C4u), c.V0);
        goto L801A7964;
        L801A7BC0: ;
        c.RA = mem.ReadU32((c.SP + 0x1Cu));
        c.S2 = mem.ReadU32((c.SP + 0x18u));
        c.S1 = mem.ReadU32((c.SP + 0x14u));
        c.S0 = mem.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x20u;
        return;
        }
    public static bool CutsceneSkip_bo6(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo6", m, true)) return true;
        CutsceneSkip_bo6_Body(c, m);
        return false;
    }
    private static void CutsceneSkip_bo6_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo6", 0x801D1114u);
        c.SP = c.SP - 0x20u;
        mem.WriteU32((c.SP + 0x10u), c.S0);
        c.S0 = c.A0;
        c.V1 = 0x80090000u;
        c.V1 = mem.ReadU16((c.V1 + 0x7494u));
        c.V0 = 0u | 0x0800u;
        mem.WriteU32((c.SP + 0x1Cu), c.RA);
        mem.WriteU32((c.SP + 0x18u), c.S2);
        if (c.V1 != c.V0) {
            mem.WriteU32((c.SP + 0x14u), c.S1);
            goto L801A7CA0;
        }
        mem.WriteU32((c.SP + 0x14u), c.S1);
        c.S1 = 0x80040000u;
        c.S1 = c.S1 - 0x384Cu;
        c.A0 = mem.ReadU32((c.S0 + 0x64u));
        c.V1 = mem.ReadU32(c.S1);
        c.V0 = 0u | 0x0001u;
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x1110u), c.V0);
        c.S2 = 0xFFFFFFFFu;
        c.RA = 0x801A7C28u;
        Dispatcher.Call(c, m, c.V1);
        c.V0 = mem.ReadU32((c.S0 + 0x34u));
        c.V1 = 0x00800000u;
        c.V0 = c.V0 ^ c.V1;
        mem.WriteU32((c.S0 + 0x34u), c.V0);
        c.A0 = 0x801D0000u;
        c.A0 = mem.ReadU32((c.A0 + 0x1148u));
        if (c.A0 == c.S2) {
            goto L801A7C5C;
        }
        c.V0 = mem.ReadU32(c.S1);
        c.RA = 0x801A7C5Cu;
        Dispatcher.Call(c, m, c.V0);
        L801A7C5C: ;
        c.A0 = 0x801D0000u;
        c.A0 = mem.ReadU32((c.A0 + 0x1144u));
        if (c.A0 == c.S2) {
            goto L801A7C80;
        }
        c.V0 = mem.ReadU32(c.S1);
        c.RA = 0x801A7C80u;
        Dispatcher.Call(c, m, c.V0);
        L801A7C80: ;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x000Au;
        c.RA = 0x801A7C94u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0u | 0x0001u;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        mem.WriteU16((c.S0 + 0x2Eu), (ushort)0u);
        L801A7CA0: ;
        c.RA = mem.ReadU32((c.SP + 0x1Cu));
        c.S2 = mem.ReadU32((c.SP + 0x18u));
        c.S1 = mem.ReadU32((c.SP + 0x14u));
        c.S0 = mem.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x20u;
        return;
        }
    public static bool ScaleCutsceneAvatar_bo6(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo6", m, true)) return true;
        ScaleCutsceneAvatar_bo6_Body(c, m);
        return false;
    }
    private static void ScaleCutsceneAvatar_bo6_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo6", 0x801D1114u);
        c.V0 = 0x801D0000u;
        c.V0 = (uint)(short)mem.ReadU16((c.V0 + 0x1122u));
        c.A3 = c.V0 + (uint)(mem.LineCount - DialogueGlyphs.VisibleLines(mem));
        c.V0 = (int)c.A3 < 5 ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L801A7CEC;
        }
        c.A3 = c.A3 - 0x5u;
        L801A7CDC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = (int)c.A3 < 5 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.A3 = c.A3 - 0x5u;
            goto L801A7CDC;
        }
        c.A3 = c.A3 - 0x5u;
        c.A3 = c.A3 + 0x5u;
        L801A7CEC: ;
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3C48u));
        c.V0 = mem.ReadU16((c.V0 + 0x2Eu));
        if (c.V0 != 0u) {
            c.V1 = 0u;
            goto L801A7D68;
        }
        c.V1 = 0u;
        c.V0 = c.A3 << 2;
        c.At = 0x801D0000u;
        c.At = c.At + c.V0;
        c.A1 = mem.ReadU32((c.At + 0x112Cu));
        c.V0 = mem.ReadU8((c.A1 + 0x19u));
        c.V0 = c.V0 - c.A0;
        mem.WriteU8((c.A1 + 0x19u), (byte)c.V0);
        c.V0 = mem.ReadU8((c.A1 + 0xDu));
        c.V1 = mem.ReadU8((c.A1 + 0x19u));
        c.V0 = c.A0 + c.V0;
        if (c.V1 != 0u) {
            mem.WriteU8((c.A1 + 0xDu), (byte)c.V0);
            goto L801A7D64;
        }
        mem.WriteU8((c.A1 + 0xDu), (byte)c.V0);
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3C48u));
        c.V1 = mem.ReadU16((c.V0 + 0x2Eu));
        c.V1 = c.V1 + 0x1u;
        mem.WriteU16((c.V0 + 0x2Eu), (ushort)c.V1);
        c.V0 = 0u | 0x0008u;
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0);
        L801A7D64: ;
        c.V1 = 0u;
        L801A7D68: ;
        c.A0 = c.A0 & 0x00FFu;
        c.A2 = 0x801D0000u;
        c.A2 = c.A2 + 0x112Cu;
        L801A7D74: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.V1 == c.A3) {
            goto L801A7D94;
        }
        c.A1 = mem.ReadU32(c.A2);
        c.V0 = mem.ReadU16((c.A1 + 0xAu));
        c.V0 = c.V0 - c.A0;
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0);
        L801A7D94: ;
        c.V1 = c.V1 + 0x1u;
        c.V0 = (int)c.V1 < 5 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.A2 = c.A2 + 0x4u;
            goto L801A7D74;
        }
        c.A2 = c.A2 + 0x4u;
        c.V1 = 0x801D0000u;
        c.V1 = c.V1 + 0x1124u;
        c.V0 = mem.ReadU16(c.V1);
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16(c.V1, (ushort)c.V0);
        return;
        }
    public static bool func_us_801A7DC0(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo6", m, true)) return true;
        func_us_801A7DC0_Body(c, m);
        return false;
    }
    private static void func_us_801A7DC0_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo6", 0x801D1114u);
        c.SP = c.SP - 0x48u;
        mem.WriteU32((c.SP + 0x38u), c.S2);
        c.S2 = c.A0;
        mem.WriteU32((c.SP + 0x44u), c.RA);
        mem.WriteU32((c.SP + 0x40u), c.S4);
        mem.WriteU32((c.SP + 0x3Cu), c.S3);
        mem.WriteU32((c.SP + 0x34u), c.S1);
        mem.WriteU32((c.SP + 0x30u), c.S0);
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu));
        if (c.V0 == 0u) {
            goto L801A7ED4;
        }
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1698u));
        if (c.V0 == 0u) {
            goto L801A7EA8;
        }
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1110u));
        if (c.V0 != 0u) {
            goto L801A7EA8;
        }
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x4220u));
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0002u;
            goto L801A7EA0;
        }
        c.V0 = 0u | 0x0002u;
        c.V1 = mem.ReadU16((c.S2 + 0x30u));
        if (c.V1 == c.V0) {
            c.V0 = (int)c.V1 < 3 ? 1u : 0u;
            goto L801A7E78;
        }
        c.V0 = (int)c.V1 < 3 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801A7E54;
        }
        if (c.V1 == 0u) {
            goto L801A7E68;
        }
        goto L801A7EA8;
        L801A7E54: ;
        c.V0 = 0u | 0x0003u;
        if (c.V1 == c.V0) {
            c.V1 = 0x00020000u;
            goto L801A7E88;
        }
        c.V1 = 0x00020000u;
        goto L801A7EA8;
        L801A7E68: ;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x34FCu));
        c.V0 = c.V0 & 0x8000u;
        goto L801A7E98;
        L801A7E78: ;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x34FCu));
        c.V1 = 0x00010000u;
        goto L801A7E90;
        L801A7E88: ;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x34FCu));
        L801A7E90: ;
        c.V0 = c.V0 & c.V1;
        L801A7E98: ;
        if (c.V0 == 0u) {
            c.A0 = c.S2;
            goto L801A7EA8;
        }
        c.A0 = c.S2;
        L801A7EA0: ;
        c.RA = 0x801A7EA8u;
        SoTN_bo6.CutsceneSkip_bo6(c, m);
        L801A7EA8: ;
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu));
        if (c.V0 == 0u) {
            goto L801A7ED4;
        }
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU16((c.V0 + 0x1150u));
        if (c.V0 == 0u) {
            goto L801A7ED4;
        }
        c.RA = 0x801A7ED4u;
        SoTN_bo6.CutsceneRun_bo6(c, m);
        L801A7ED4: ;
        c.V1 = mem.ReadU16((c.S2 + 0x2Cu));
        c.V0 = c.V1 < 0x00000008u ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = c.V1 << 2;
            goto L801A8F9C;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x801A0000u;
        c.At = c.At + c.V0;
        c.V0 = mem.ReadU32((c.At + 0x6320u));
        switch (c.V0)
        {
            case 0x801A7F00u: goto L801A7F00;
            case 0x801A80ACu: goto L801A80AC;
            case 0x801A89A0u: goto L801A89A0;
            case 0x801A89D4u: goto L801A89D4;
            case 0x801A8A98u: goto L801A8A98;
            case 0x801A8B30u: goto L801A8B30;
            case 0x801A8E20u: goto L801A8E20;
            case 0x801A8EC8u: goto L801A8EC8;
            case 0x801A8124u: goto L801A8124;
            case 0x801A8130u: goto L801A8130;
            case 0x801A81DCu: goto L801A81DC;
            case 0x801A8208u: goto L801A8208;
            case 0x801A8238u: goto L801A8238;
            case 0x801A8278u: goto L801A8278;
            case 0x801A83B0u: goto L801A83B0;
            case 0x801A8428u: goto L801A8428;
            case 0x801A84BCu: goto L801A84BC;
            case 0x801A84E4u: goto L801A84E4;
            case 0x801A850Cu: goto L801A850C;
            case 0x801A8544u: goto L801A8544;
            case 0x801A858Cu: goto L801A858C;
            case 0x801A80C0u: goto L801A80C0;
            case 0x801A85E8u: goto L801A85E8;
            case 0x801A8654u: goto L801A8654;
            case 0x801A86B4u: goto L801A86B4;
            case 0x801A8700u: goto L801A8700;
            case 0x801A8734u: goto L801A8734;
            case 0x801A8744u: goto L801A8744;
            case 0x801A8804u: goto L801A8804;
            case 0x801A8840u: goto L801A8840;
            case 0x801A8860u: goto L801A8860;
            case 0x801A8F9Cu: goto L801A8F9C;
            case 0x801A889Cu: goto L801A889C;
            case 0x801A9248u: goto L801A9248;
            case 0x801A936Cu: goto L801A936C;
            case 0x801A93E0u: goto L801A93E0;
            case 0x801A9470u: goto L801A9470;
            case 0x801A94FCu: goto L801A94FC;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801A7F00: ;
        c.V1 = mem.ReadU16((c.S2 + 0x30u));
        c.V0 = 0u | 0x0001u;
        if (c.V1 == c.V0) {
            c.V0 = (int)c.V1 < 2 ? 1u : 0u;
            goto L801A7FE0;
        }
        c.V0 = (int)c.V1 < 2 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801A7F28;
        }
        if (c.V1 == 0u) {
            goto L801A7F3C;
        }
        goto L801A8044;
        L801A7F28: ;
        c.V0 = (int)c.V1 < 4 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801A8044;
        }
        goto L801A7FF8;
        L801A7F3C: ;
        c.V0 = 0x80090000u;
        c.V0 = mem.ReadU32((c.V0 + 0x7914u));
        if (c.V0 != 0u) {
            goto L801A8B98;
        }
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU8((c.V0 - 0x417Fu));
        if (c.V0 != 0u) {
            goto L801A8B98;
        }
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3660u));
        if (c.V0 != 0u) {
            goto L801A8B98;
        }
        c.A0 = 0x801A0000u;
        c.A0 = c.A0 - 0x5F30u;
        c.RA = 0x801A7F88u;
        SoTN_bo6.SetCutsceneScript_bo6(c, m);
        c.V1 = 0x80040000u;
        c.V1 = mem.ReadU8((c.V1 - 0x413Cu));
        c.At = 0x801A0000u;
        mem.WriteU8((c.At - 0x5F38u), (byte)0u);
        if (c.V1 == 0u) {
            c.S0 = c.V0 & 0x00FFu;
            goto L801A7FAC;
        }
        c.S0 = c.V0 & 0x00FFu;
        c.V0 = 0u | 0x0001u;
        c.At = 0x801A0000u;
        mem.WriteU8((c.At - 0x5F38u), (byte)c.V0);
        L801A7FAC: ;
        c.A0 = 0u | 0x0022u;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x379Cu));
        c.At = 0x801A0000u;
        mem.WriteU8((c.At - 0x5F34u), (byte)0u);
        c.A1 = 0u | 0x0001u;
        c.RA = 0x801A7FC8u;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 == 0u) {
            c.V0 = 0u | 0x0001u;
            goto L801A8044;
        }
        c.V0 = 0u | 0x0001u;
        c.At = 0x801A0000u;
        mem.WriteU8((c.At - 0x5F34u), (byte)c.V0);
        goto L801A8044;
        L801A7FE0: ;
        c.A0 = 0x801A0000u;
        c.A0 = c.A0 - 0x5D1Fu;
        c.RA = 0x801A7FF0u;
        SoTN_bo6.SetCutsceneScript_bo6(c, m);
        c.S0 = c.V0 & 0x00FFu;
        goto L801A8044;
        L801A7FF8: ;
        c.V0 = 0x80090000u;
        c.V0 = mem.ReadU32((c.V0 + 0x7914u));
        if (c.V0 != 0u) {
            goto L801A8B98;
        }
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU8((c.V0 - 0x417Fu));
        if (c.V0 != 0u) {
            goto L801A8B98;
        }
        c.A0 = 0x801A0000u;
        c.A0 = c.A0 - 0x5D05u;
        c.RA = 0x801A8030u;
        SoTN_bo6.SetCutsceneScript_bo6(c, m);
        c.V1 = mem.ReadU8((c.S2 + 0x30u));
        c.S0 = c.V0 & 0x00FFu;
        c.V1 = c.V1 + 0xFEu;
        c.At = 0x801A0000u;
        mem.WriteU8((c.At - 0x5F34u), (byte)c.V1);
        L801A8044: ;
        if (c.S0 == 0u) {
            c.A0 = 0x00800000u;
            goto L801A8F9C;
        }
        c.A0 = 0x00800000u;
        c.A0 = c.A0 | 0x2000u;
        c.V1 = mem.ReadU32((c.S2 + 0x34u));
        c.V0 = 0u | 0x0001u;
        c.At = 0x80040000u;
        mem.WriteU32((c.At - 0x38FCu), c.V0);
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu));
        c.V1 = c.V1 | c.A0;
        mem.WriteU32((c.S2 + 0x34u), c.V1);
        c.V1 = 0x801D0000u;
        c.V1 = mem.ReadU32((c.V1 + 0x114Cu));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        c.V0 = mem.ReadU32((c.S2 + 0x34u));
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x11C4u), 0u);
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x1698u), 0u);
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x1110u), 0u);
        mem.WriteU32((c.S2 + 0x64u), c.V1);
        c.V1 = 0x00010000u;
        c.V0 = c.V0 | c.V1;
        mem.WriteU32((c.S2 + 0x34u), c.V0);
        goto L801A8F9C;
        L801A80AC: ;
        c.S4 = 0x801D0000u;
        c.S4 = c.S4 + 0x112Au;
        c.S1 = c.S4 - 0x16u;
        c.S0 = c.S4 - 0xCu;
        c.S3 = c.S4 - 0x12u;
        L801A80C0: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.S4);
        if (c.V0 == 0u) {
            goto L801A80E4;
        }
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1110u));
        if (c.V0 == 0u) {
            goto L801A8F3C;
        }
        L801A80E4: ;
        c.V0 = mem.ReadU32(c.S1);
        c.V1 = c.V0 + 0x1u;
        mem.WriteU32(c.S1, c.V1);
        c.A2 = mem.ReadU8(c.V0);
        c.V1 = c.A2 & 0xFFFFu;
        c.V0 = c.V1 < 0x00000019u ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = c.V1 << 2;
            goto L801A88DC;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x801A0000u;
        c.At = c.At + c.V0;
        c.V0 = mem.ReadU32((c.At + 0x6340u));
        switch (c.V0)
        {
            case 0x801A8124u: goto L801A8124;
            case 0x801A8130u: goto L801A8130;
            case 0x801A81DCu: goto L801A81DC;
            case 0x801A8208u: goto L801A8208;
            case 0x801A8238u: goto L801A8238;
            case 0x801A8278u: goto L801A8278;
            case 0x801A83B0u: goto L801A83B0;
            case 0x801A8428u: goto L801A8428;
            case 0x801A84BCu: goto L801A84BC;
            case 0x801A84E4u: goto L801A84E4;
            case 0x801A850Cu: goto L801A850C;
            case 0x801A8544u: goto L801A8544;
            case 0x801A858Cu: goto L801A858C;
            case 0x801A80C0u: goto L801A80C0;
            case 0x801A85E8u: goto L801A85E8;
            case 0x801A8654u: goto L801A8654;
            case 0x801A86B4u: goto L801A86B4;
            case 0x801A8700u: goto L801A8700;
            case 0x801A8734u: goto L801A8734;
            case 0x801A8744u: goto L801A8744;
            case 0x801A8804u: goto L801A8804;
            case 0x801A8840u: goto L801A8840;
            case 0x801A8860u: goto L801A8860;
            case 0x801A8F9Cu: goto L801A8F9C;
            case 0x801A889Cu: goto L801A889C;
            case 0x801A9248u: goto L801A9248;
            case 0x801A936Cu: goto L801A936C;
            case 0x801A93E0u: goto L801A93E0;
            case 0x801A9470u: goto L801A9470;
            case 0x801A94FCu: goto L801A94FC;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801A8124: ;
        c.V0 = 0u | 0x0007u;
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        goto L801A8F9C;
        L801A8130: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1110u));
        if (c.V0 != 0u) {
            goto L801A80C0;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x8u));
        c.V1 = mem.ReadU16((c.S0 + 0x2u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 != 0u) {
            mem.WriteU16(c.S0, (ushort)c.V1);
            goto L801A8168;
        }
        mem.WriteU16(c.S0, (ushort)c.V1);
        c.V0 = mem.ReadU16((c.S0 - 0x4u));
        c.V0 = c.V0 + 0xCu;
        mem.WriteU16((c.S0 - 0x4u), (ushort)c.V0);
        L801A8168: ;
        c.V0 = mem.ReadU16((c.S0 + 0x4u));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S0 + 0x4u), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V0 < 5 ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L801A8190;
        }
        mem.WriteU16((c.S0 + 0x4u), (ushort)0u);
        L801A8190: ;
        c.RA = 0x801A8198u;
        SoTN_bo6.CutsceneUnk4_bo6(c, m);
        c.V1 = mem.ReadU16((c.S0 + 0x8u));
        c.V0 = c.V1 & 0x0001u;
        if (c.V0 != 0u) {
            goto L801A81C4;
        }
        c.V0 = (uint)(short)mem.ReadU16((c.S0 + 0x4u));
        c.V0 = (int)c.V0 < 4 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V0 = c.V1 | 0x0001u;
            goto L801A80C0;
        }
        c.V0 = c.V1 | 0x0001u;
        mem.WriteU16((c.S0 + 0x8u), (ushort)c.V0);
        L801A81C4: ;
        mem.WriteU16((c.S0 + 0x6u), (ushort)0u);
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu));
        mem.WriteU16((c.S2 + 0x2Eu), (ushort)0u);
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        goto L801A8F9C;
        L801A81DC: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1114u));
        c.V1 = c.V0 + 0x1u;
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x1114u), c.V1);
        c.V0 = mem.ReadU8(c.V0);
        c.At = 0x801D0000u;
        mem.WriteU8((c.At + 0x112Bu), (byte)c.V0);
        goto L801A80C0;
        L801A8208: ;
        c.V1 = mem.ReadU32((c.S4 - 0x16u));
        c.V0 = c.V1 + 0x1u;
        mem.WriteU32((c.S4 - 0x16u), c.V0);
        c.V0 = mem.ReadU8(c.V1);
        c.V1 = 0x801D0000u;
        c.V1 = mem.ReadU32((c.V1 + 0x1110u));
        if (c.V1 != 0u) {
            mem.WriteU8(c.S4, (byte)c.V0);
            goto L801A80C0;
        }
        mem.WriteU8(c.S4, (byte)c.V0);
        goto L801A8F9C;
        L801A8238: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1110u));
        if (c.V0 != 0u) {
            c.V1 = 0u | 0x0008u;
            goto L801A80C0;
        }
        c.V1 = 0u | 0x0008u;
        c.S1 = 0x801D0000u;
        c.S1 = mem.ReadU32((c.S1 + 0x112Cu));
        c.S0 = 0u;
        c.S0 = c.S0 + 0x1u;
        L801A825C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = (int)c.S0 < 5 ? 1u : 0u;
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V1);
        c.S1 = mem.ReadU32(c.S1);
        if (c.V0 != 0u) {
            c.S0 = c.S0 + 0x1u;
            goto L801A825C;
        }
        c.S0 = c.S0 + 0x1u;
        goto L801A8F9C;
        L801A8278: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1110u));
        if (c.V0 != 0u) {
            goto L801A84F8;
        }
        c.V1 = mem.ReadU32(c.S1);
        c.V0 = c.V1 + 0x1u;
        mem.WriteU32(c.S1, c.V0);
        c.S0 = mem.ReadU8(c.V1);
        c.V0 = c.V1 + 0x2u;
        mem.WriteU32(c.S1, c.V0);
        c.T2 = mem.ReadU8((c.V1 + 0x1u));
        c.S1 = mem.ReadU32((c.S1 + 0x2Cu));
        c.V0 = c.T2 & 0x0001u;
        c.V1 = c.S0 << 1;
        c.At = 0x80180000u;
        c.At = c.At + c.V0;
        c.A0 = mem.ReadU8((c.At + 0x610u));
        c.At = 0x80180000u;
        c.At = c.At + c.V0;
        c.A1 = mem.ReadU8((c.At + 0x614u));
        c.At = 0x80180000u;
        c.At = c.At + c.V1;
        c.V1 = mem.ReadU16((c.At + 0x618u));
        c.V0 = 0u | 0x0094u;
        mem.WriteU16((c.S1 + 0x1Au), (ushort)c.V0);
        c.V0 = c.T2 & 0x0080u;
        if (c.V0 == 0u) {
            mem.WriteU16((c.S1 + 0xEu), (ushort)c.V1);
            goto L801A82FC;
        }
        mem.WriteU16((c.S1 + 0xEu), (ushort)c.V1);
        c.V1 = c.A0;
        c.V0 = c.V1 + 0x2Fu;
        goto L801A8304;
        L801A82FC: ;
        c.V0 = c.A0;
        c.V1 = c.V0 + 0x2Fu;
        L801A8304: ;
        mem.WriteU8((c.S1 + 0x24u), (byte)c.V0);
        mem.WriteU8((c.S1 + 0xCu), (byte)c.V0);
        mem.WriteU8((c.S1 + 0x30u), (byte)c.V1);
        mem.WriteU8((c.S1 + 0x18u), (byte)c.V1);
        c.V0 = c.A1;
        c.V1 = c.V0 + 0x48u;
        mem.WriteU8((c.S1 + 0x19u), (byte)c.V0);
        mem.WriteU8((c.S1 + 0xDu), (byte)c.V0);
        mem.WriteU8((c.S1 + 0x31u), (byte)c.V1);
        mem.WriteU8((c.S1 + 0x25u), (byte)c.V1);
        c.V0 = mem.ReadU16(c.S3);
        c.V0 = c.V0 - 0x1Eu;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
        mem.WriteU16((c.S1 + 0x20u), (ushort)c.V0);
        mem.WriteU16((c.S1 + 0x14u), (ushort)c.V0);
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S3 + 0x4u));
        c.V0 = c.V0 + 0x24u;
        mem.WriteU16((c.S1 + 0x2Eu), (ushort)c.V0);
        mem.WriteU16((c.S1 + 0x22u), (ushort)c.V0);
        mem.WriteU16((c.S1 + 0x16u), (ushort)c.V0);
        mem.WriteU16((c.S1 + 0xAu), (ushort)c.V0);
        c.V0 = c.S0 << 1;
        c.At = 0x80180000u;
        c.At = c.At + c.V0;
        c.V0 = mem.ReadU16((c.At + 0x624u));
        mem.WriteU16((c.S3 + 0x10u), (ushort)c.V0);
        c.RA = 0x801A837Cu;
        SoTN_bo6.CutsceneUnk1_bo6(c, m);
        c.RA = 0x801A8384u;
        SoTN_bo6.CutsceneUnk4_bo6(c, m);
        c.A0 = c.S0 & 0xFFFFu;
        c.A1 = c.S2;
        c.V0 = 0u | 0x01FEu;
        mem.WriteU16((c.S1 + 0x26u), (ushort)c.V0);
        mem.WriteU16((c.S1 + 0x32u), (ushort)0u);
        c.RA = 0x801A839Cu;
        SoTN_bo6.DrawCutsceneActorName_bo6(c, m);
        c.V0 = 0u | 0x0006u;
        mem.WriteU16((c.S3 + 0xCu), (ushort)c.V0);
        c.V0 = 0u | 0x0003u;
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        goto L801A8F9C;
        L801A83B0: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1110u));
        if (c.V0 != 0u) {
            c.V1 = 0u | 0x0008u;
            goto L801A80C0;
        }
        c.V1 = 0u | 0x0008u;
        c.S1 = 0x801D0000u;
        c.S1 = mem.ReadU32((c.S1 + 0x112Cu));
        c.S0 = 0u;
        c.S0 = c.S0 + 0x1u;
        L801A83D4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = (int)c.S0 < 5 ? 1u : 0u;
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V1);
        c.S1 = mem.ReadU32(c.S1);
        if (c.V0 != 0u) {
            c.S0 = c.S0 + 0x1u;
            goto L801A83D4;
        }
        c.S0 = c.S0 + 0x1u;
        c.S0 = 0x801D0000u;
        c.S0 = c.S0 + 0x1148u;
        c.A0 = mem.ReadU32(c.S0);
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x384Cu));
        c.RA = 0x801A8408u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0xFFFFFFFFu;
        mem.WriteU32(c.S0, c.V0);
        c.V0 = 0u | 0x0006u;
        c.At = 0x801D0000u;
        mem.WriteU16((c.At + 0x1124u), (ushort)c.V0);
        c.V0 = 0u | 0x0004u;
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        goto L801A8F9C;
        L801A8428: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1110u));
        if (c.V0 != 0u) {
            goto L801A84F8;
        }
        c.V1 = mem.ReadU32((c.S3 - 0x4u));
        c.S1 = mem.ReadU32((c.S3 + 0x28u));
        c.V0 = c.V1 + 0x1u;
        mem.WriteU32((c.S3 - 0x4u), c.V0);
        c.A0 = mem.ReadU8(c.V1);
        c.V0 = c.V1 + 0x2u;
        mem.WriteU32((c.S3 - 0x4u), c.V0);
        mem.WriteU16(c.S3, (ushort)c.A0);
        c.V0 = mem.ReadU8((c.V1 + 0x1u));
        mem.WriteU16((c.S3 + 0x4u), (ushort)c.V0);
        c.S1 = mem.ReadU32(c.S1);
        mem.WriteU16((c.S1 + 0x16u), (ushort)c.V0);
        mem.WriteU16((c.S1 + 0xAu), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S3 + 0x4u));
        c.V0 = c.V0 + 0x48u;
        mem.WriteU16((c.S1 + 0x2Eu), (ushort)c.V0);
        mem.WriteU16((c.S1 + 0x22u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S3 + 0x4u));
        c.S1 = mem.ReadU32(c.S1);
        c.V0 = c.V0 - 0x1u;
        mem.WriteU16((c.S1 + 0xAu), (ushort)c.V0);
        c.V0 = 0u | 0x00F6u;
        mem.WriteU8((c.S1 + 0xCu), (byte)c.V0);
        c.V0 = 0u | 0x0018u;
        mem.WriteU16((c.S3 + 0xCu), (ushort)c.V0);
        c.V0 = 0u | 0x0005u;
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        mem.WriteU16((c.S2 + 0x2Eu), (ushort)0u);
        goto L801A8F9C;
        L801A84BC: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1110u));
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0018u;
            goto L801A80C0;
        }
        c.V0 = 0u | 0x0018u;
        c.At = 0x801D0000u;
        mem.WriteU16((c.At + 0x1124u), (ushort)c.V0);
        c.V0 = 0u | 0x0006u;
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        goto L801A8F9C;
        L801A84E4: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1110u));
        if (c.V0 == 0u) {
            goto L801A8804;
        }
        L801A84F8: ;
        c.V0 = mem.ReadU32(c.S1);
        c.V0 = c.V0 + 0x2u;
        mem.WriteU32(c.S1, c.V0);
        goto L801A80C0;
        L801A850C: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1110u));
        if (c.V0 != 0u) {
            goto L801A80C0;
        }
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3808u));
        c.RA = 0x801A8534u;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 == 0u) {
            goto L801A8578;
        }
        goto L801A80C0;
        L801A8544: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1110u));
        if (c.V0 != 0u) {
            goto L801A80C0;
        }
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3808u));
        c.RA = 0x801A856Cu;
        Dispatcher.Call(c, m, c.V0);
        c.V1 = 0u | 0x0001u;
        if (c.V0 != c.V1) {
            goto L801A80C0;
        }
        L801A8578: ;
        c.V0 = mem.ReadU32(c.S1);
        c.V0 = c.V0 - 0x1u;
        mem.WriteU32(c.S1, c.V0);
        goto L801A8F9C;
        L801A858C: ;
        c.V1 = mem.ReadU32(c.S1);
        c.V0 = c.V1 + 0x1u;
        mem.WriteU32(c.S1, c.V0);
        c.T1 = mem.ReadU8(c.V1);
        c.V0 = c.V1 + 0x2u;
        mem.WriteU32(c.S1, c.V0);
        c.A1 = mem.ReadU8((c.V1 + 0x1u));
        c.V0 = c.V1 + 0x3u;
        mem.WriteU32(c.S1, c.V0);
        c.A0 = mem.ReadU8((c.V1 + 0x2u));
        c.V0 = c.V1 + 0x4u;
        mem.WriteU32(c.S1, c.V0);
        c.T1 = c.T1 << 4;
        c.T1 = c.T1 | c.A1;
        c.T1 = c.T1 << 4;
        c.T1 = c.T1 | c.A0;
        c.A0 = mem.ReadU8((c.V1 + 0x3u));
        c.T1 = c.T1 << 4;
        c.A0 = c.T1 | c.A0;
        c.RA = 0x801A85E0u;
        SoTN_bo6.SetCutsceneEnd_bo6(c, m);
        goto L801A80C0;
        L801A85E8: ;
        c.V1 = mem.ReadU32(c.S1);
        c.V0 = c.V1 + 0x1u;
        mem.WriteU32(c.S1, c.V0);
        c.T1 = mem.ReadU8(c.V1);
        c.V0 = c.V1 + 0x2u;
        mem.WriteU32(c.S1, c.V0);
        c.A0 = mem.ReadU8((c.V1 + 0x1u));
        c.V0 = c.V1 + 0x3u;
        mem.WriteU32(c.S1, c.V0);
        c.V0 = mem.ReadU8((c.V1 + 0x2u));
        c.A1 = c.V1 + 0x4u;
        mem.WriteU32(c.S1, c.A1);
        c.T1 = c.T1 << 4;
        c.T1 = c.T1 | c.A0;
        c.T1 = c.T1 << 4;
        c.T1 = c.T1 | c.V0;
        c.V0 = mem.ReadU8((c.V1 + 0x3u));
        c.T1 = c.T1 << 4;
        c.T1 = c.T1 | c.V0;
        c.At = 0x00100000u;
        c.At = c.T1 + c.At;
        c.V1 = mem.ReadU8(c.At);
        c.V1 = c.V1 << 2;
        c.V1 = c.V1 + c.A1;
        goto L801A8658;
        L801A8654: ;
        c.V1 = mem.ReadU32(c.S1);
        L801A8658: ;
        c.V0 = c.V1 + 0x1u;
        mem.WriteU32(c.S1, c.V0);
        c.T1 = mem.ReadU8(c.V1);
        c.V0 = c.V1 + 0x2u;
        mem.WriteU32(c.S1, c.V0);
        c.A0 = mem.ReadU8((c.V1 + 0x1u));
        c.V0 = c.V1 + 0x3u;
        mem.WriteU32(c.S1, c.V0);
        c.V0 = mem.ReadU8((c.V1 + 0x2u));
        c.V1 = 0x801D0000u;
        c.V1 = mem.ReadU32((c.V1 + 0x1114u));
        c.T1 = c.T1 << 4;
        c.T1 = c.T1 | c.A0;
        c.T1 = c.T1 << 4;
        c.T1 = c.T1 | c.V0;
        c.V0 = mem.ReadU8(c.V1);
        c.T1 = c.T1 << 4;
        c.T1 = c.T1 | c.V0;
        c.V0 = 0x00100000u;
        c.V0 = c.T1 + c.V0;
        mem.WriteU32(c.S1, c.V0);
        goto L801A80C0;
        L801A86B4: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1114u));
        c.A0 = 0x801D0000u;
        c.A0 = mem.ReadU32((c.A0 + 0x11C4u));
        c.V1 = mem.ReadU8(c.V0);
        c.V0 = c.A0 >> (int)(c.V1 & 31u);
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u) {
            c.V0 = 0u | 0x0001u;
            goto L801A8F50;
        }
        c.V0 = 0u | 0x0001u;
        c.V0 = c.V0 << (int)(c.V1 & 31u);
        c.V0 = ~(0u | c.V0);
        c.V1 = mem.ReadU32(c.S1);
        c.V0 = c.V0 & c.A0;
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x11C4u), c.V0);
        c.V1 = c.V1 + 0x1u;
        mem.WriteU32(c.S1, c.V1);
        goto L801A80C0;
        L801A8700: ;
        c.V1 = mem.ReadU32(c.S1);
        c.A0 = 0x801D0000u;
        c.A0 = mem.ReadU32((c.A0 + 0x11C4u));
        c.V0 = c.V1 + 0x1u;
        mem.WriteU32(c.S1, c.V0);
        c.V1 = mem.ReadU8(c.V1);
        c.V0 = 0u | 0x0001u;
        c.V0 = c.V0 << (int)(c.V1 & 31u);
        c.V0 = c.V0 | c.A0;
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x11C4u), c.V0);
        goto L801A80C0;
        L801A8734: ;
        c.At = 0x801D0000u;
        mem.WriteU16((c.At + 0x1150u), (ushort)0u);
        goto L801A80C0;
        L801A8744: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1110u));
        if (c.V0 == 0u) {
            c.A1 = 0u | 0x0001u;
            goto L801A876C;
        }
        c.A1 = 0u | 0x0001u;
        c.V0 = mem.ReadU32(c.S1);
        c.V0 = c.V0 + 0x5u;
        mem.WriteU32(c.S1, c.V0);
        goto L801A80C0;
        L801A876C: ;
        c.V1 = mem.ReadU32(c.S1);
        c.A2 = 0u;
        c.V0 = c.V1 + 0x1u;
        mem.WriteU32(c.S1, c.V0);
        c.T1 = mem.ReadU8(c.V1);
        c.V0 = c.V1 + 0x2u;
        mem.WriteU32(c.S1, c.V0);
        c.T0 = mem.ReadU8((c.V1 + 0x1u));
        c.V0 = c.V1 + 0x3u;
        mem.WriteU32(c.S1, c.V0);
        c.A3 = mem.ReadU8((c.V1 + 0x2u));
        c.V0 = c.V1 + 0x4u;
        mem.WriteU32(c.S1, c.V0);
        c.A0 = mem.ReadU8((c.V1 + 0x3u));
        c.V0 = c.V1 + 0x5u;
        mem.WriteU32(c.S1, c.V0);
        c.T2 = mem.ReadU8((c.V1 + 0x4u));
        c.T1 = c.T1 << 4;
        c.T1 = c.T1 | c.T0;
        c.T1 = c.T1 << 4;
        c.T1 = c.T1 | c.A3;
        c.T1 = c.T1 << 4;
        c.T1 = c.T1 | c.A0;
        c.A0 = 0x00100000u;
        c.A0 = c.T1 + c.A0;
        c.V0 = c.T2 << 1;
        c.At = 0x80180000u;
        c.At = c.At + c.V0;
        c.A3 = mem.ReadU16((c.At + 0x620u));
        c.V0 = 0u | 0x0100u;
        mem.WriteU32((c.SP + 0x10u), c.V0);
        c.V0 = 0u | 0x0030u;
        mem.WriteU32((c.SP + 0x14u), c.V0);
        c.V0 = 0u | 0x0048u;
        mem.WriteU32((c.SP + 0x18u), c.V0);
        c.RA = 0x801A87FCu;
        SoTN_main.LoadTPage(c, m);
        goto L801A80C0;
        L801A8804: ;
        c.V1 = mem.ReadU32(c.S1);
        c.V0 = c.V1 + 0x1u;
        mem.WriteU32(c.S1, c.V0);
        c.A2 = mem.ReadU8(c.V1);
        c.V0 = c.V1 + 0x2u;
        mem.WriteU32(c.S1, c.V0);
        c.A0 = mem.ReadU8((c.V1 + 0x1u));
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3824u));
        c.A1 = c.A2 << 4;
        c.A0 = c.A0 | c.A1;
        c.RA = 0x801A8838u;
        Dispatcher.Call(c, m, c.V0);
        goto L801A80C0;
        L801A8840: ;
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x11C4u), 0u);
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x1110u), 0u);
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x1698u), 0u);
        goto L801A80C0;
        L801A8860: ;
        c.V1 = mem.ReadU32(c.S1);
        c.V0 = c.V1 + 0x1u;
        mem.WriteU32(c.S1, c.V0);
        c.V1 = mem.ReadU8(c.V1);
        c.V0 = 0u | 0x0001u;
        c.V0 = c.V0 << (int)(c.V1 & 31u);
        c.V1 = 0x801D0000u;
        c.V1 = mem.ReadU32((c.V1 + 0x11C4u));
        c.V0 = ~(0u | c.V0);
        c.V0 = c.V0 & c.V1;
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x11C4u), c.V0);
        goto L801A80C0;
        L801A889C: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1114u));
        c.V1 = mem.ReadU8(c.V0);
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x11C4u));
        c.V0 = c.V0 >> (int)(c.V1 & 31u);
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u) {
            goto L801A8F50;
        }
        c.V0 = mem.ReadU32(c.S1);
        c.V0 = c.V0 + 0x1u;
        mem.WriteU32(c.S1, c.V0);
        goto L801A80C0;
        L801A88DC: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1110u));
        if (c.V0 != 0u) {
            goto L801A80C0;
        }
        c.V0 = mem.ReadU8((c.S1 + 0x17u));
        mem.WriteU8((c.S1 + 0x16u), (byte)c.V0);
        c.V0 = 0u | 0x0020u;
        if (c.V1 != c.V0) {
            c.A0 = c.SP + 0x20u;
            goto L801A8924;
        }
        c.A0 = c.SP + 0x20u;
        c.V1 = 0x801D0000u;
        c.V1 = c.V1 + 0x111Eu;
        c.V0 = mem.ReadU16(c.V1);
        c.V0 = c.V0 + 0x2u;
        mem.WriteU16(c.V1, (ushort)c.V0);
        goto L801A8F9C;
        L801A8924: ;
        c.V0 = c.A2 & 0x000Fu;
        c.V0 = c.V0 << 1;
        c.V0 = c.V0 + 0x380u;
        mem.WriteU16((c.SP + 0x20u), (ushort)c.V0);
        c.V0 = c.A2 & 0x00F0u;
        c.V0 = c.V0 >> 1;
        c.A1 = 0x801D0000u;
        c.A1 = (uint)(short)mem.ReadU16((c.A1 + 0x111Eu));
        c.V1 = 0x801D0000u;
        c.V1 = (uint)(short)mem.ReadU16((c.V1 + 0x1122u));
        c.V0 = c.V0 + 0xF0u;
        mem.WriteU16((c.SP + 0x22u), (ushort)c.V0);
        c.V0 = 0u | 0x0002u;
        mem.WriteU16((c.SP + 0x24u), (ushort)c.V0);
        c.V0 = 0u | 0x0008u;
        mem.WriteU16((c.SP + 0x26u), (ushort)c.V0);
        c.A2 = c.V1 << 1;
        c.A2 = c.A2 + c.V1;
        c.A2 = c.A2 << 2;
        c.A2 = c.A2 + 0x180u;
        c.A2 = c.A2 << 16;
        c.A2 = (uint)((int)c.A2 >> 16);
        c.RA = 0x801A8980u;
        if (!DialogueGlyphs.Draw(c, mem)) SoTN_main.MoveImage(c, m);
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU16((c.V0 + 0x111Eu));
        c.V0 = c.V0 + 0x2u;
        c.At = 0x801D0000u;
        mem.WriteU16((c.At + 0x111Eu), (ushort)c.V0);
        goto L801A8F9C;
        L801A89A0: ;
        c.A0 = 0u | 0x0002u;
        c.RA = 0x801A89A8u;
        SoTN_bo6.ScaleCutsceneAvatar_bo6(c, m);
        c.V0 = 0x801D0000u;
        c.V0 = (uint)(short)mem.ReadU16((c.V0 + 0x1124u));
        c.V0 = (int)c.V0 < 6 ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L801A8F9C;
        }
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu));
        c.V0 = c.V0 - 0x1u;
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        goto L801A8F9C;
        L801A89D4: ;
        c.S1 = 0x801D0000u;
        c.S1 = mem.ReadU32((c.S1 + 0x1140u));
        c.V0 = mem.ReadU16((c.S1 + 0x20u));
        c.A0 = mem.ReadU16((c.S1 + 0x2Cu));
        c.V1 = mem.ReadU16((c.S1 + 0x16u));
        c.A1 = mem.ReadU16((c.S1 + 0x2Eu));
        c.V0 = c.V0 - 0x4u;
        c.A0 = c.A0 + 0x4u;
        c.V1 = c.V1 - 0x6u;
        c.A1 = c.A1 + 0x6u;
        mem.WriteU16((c.S1 + 0x20u), (ushort)c.V0);
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.V0);
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.A0);
        mem.WriteU16((c.S1 + 0x14u), (ushort)c.A0);
        mem.WriteU16((c.S1 + 0x16u), (ushort)c.V1);
        mem.WriteU16((c.S1 + 0xAu), (ushort)c.V1);
        mem.WriteU16((c.S1 + 0x2Eu), (ushort)c.A1);
        mem.WriteU16((c.S1 + 0x22u), (ushort)c.A1);
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU16((c.V0 + 0x1124u));
        c.V0 = c.V0 - 0x1u;
        c.At = 0x801D0000u;
        mem.WriteU16((c.At + 0x1124u), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0001u;
            goto L801A8F9C;
        }
        c.V0 = 0u | 0x0001u;
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        c.V1 = 0x801D0000u;
        c.V1 = mem.ReadU32((c.V1 + 0x1148u));
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.S1 = c.V0 + c.V1;
        if (c.S1 == 0u) {
            goto L801A8F9C;
        }
        mem.WriteU16((c.S1 + 0x32u), (ushort)0u);
        L801A8A80: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.S1 = mem.ReadU32(c.S1);
        if (c.S1 == 0u) {
            goto L801A8F9C;
        }
        mem.WriteU16((c.S1 + 0x32u), (ushort)0u);
        goto L801A8A80;
        L801A8A98: ;
        c.S1 = 0x801D0000u;
        c.S1 = mem.ReadU32((c.S1 + 0x1140u));
        c.V0 = mem.ReadU16((c.S1 + 0x20u));
        c.A0 = mem.ReadU16((c.S1 + 0x2Cu));
        c.V1 = mem.ReadU16((c.S1 + 0x16u));
        c.A1 = mem.ReadU16((c.S1 + 0x2Eu));
        c.V0 = c.V0 + 0x4u;
        c.A0 = c.A0 - 0x4u;
        c.V1 = c.V1 + 0x6u;
        c.A1 = c.A1 - 0x6u;
        mem.WriteU16((c.S1 + 0x20u), (ushort)c.V0);
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.V0);
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.A0);
        mem.WriteU16((c.S1 + 0x14u), (ushort)c.A0);
        mem.WriteU16((c.S1 + 0x16u), (ushort)c.V1);
        mem.WriteU16((c.S1 + 0xAu), (ushort)c.V1);
        mem.WriteU16((c.S1 + 0x2Eu), (ushort)c.A1);
        mem.WriteU16((c.S1 + 0x22u), (ushort)c.A1);
        c.V1 = 0x801D0000u;
        c.V1 = (uint)(short)mem.ReadU16((c.V1 + 0x1118u));
        c.V0 = (uint)(short)mem.ReadU16((c.S1 + 0x14u));
        c.A0 = c.V1;
        c.V1 = c.V1 - 0x2u;
        c.V0 = (int)c.V0 < (int)c.V1 ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L801A8B10;
        }
        c.V0 = c.A0 - 0x3u;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
        mem.WriteU16((c.S1 + 0x14u), (ushort)c.V0);
        L801A8B10: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU16((c.V0 + 0x1124u));
        c.V0 = c.V0 - 0x1u;
        c.At = 0x801D0000u;
        mem.WriteU16((c.At + 0x1124u), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        goto L801A8E10;
        L801A8B30: ;
        c.V1 = mem.ReadU16((c.S2 + 0x2Eu));
        c.A2 = 0u | 0x0001u;
        if (c.V1 == c.A2) {
            c.V0 = (int)c.V1 < 2 ? 1u : 0u;
            goto L801A8C5C;
        }
        c.V0 = (int)c.V1 < 2 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801A8B58;
        }
        if (c.V1 == 0u) {
            c.A0 = 0u | 0x0002u;
            goto L801A8B6C;
        }
        c.A0 = 0u | 0x0002u;
        goto L801A8F9C;
        L801A8B58: ;
        c.V0 = 0u | 0x0002u;
        if (c.V1 == c.V0) {
            goto L801A8D58;
        }
        goto L801A8F9C;
        L801A8B6C: ;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3848u));
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x1698u), c.A2);
        c.A1 = 0u | 0x0048u;
        c.RA = 0x801A8B84u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.V1 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.V1 != c.V0) {
            c.V0 = c.V1 << 1;
            goto L801A8BA8;
        }
        c.V0 = c.V1 << 1;
        L801A8B98: ;
        c.A0 = c.S2;
        c.RA = 0x801A8BA0u;
        SoTN_bo6.DestroyEntity_bo6(c, m);
        goto L801A8F9C;
        L801A8BA8: ;
        c.A2 = 0x801D0000u;
        c.A2 = c.A2 + 0x1144u;
        mem.WriteU32(c.A2, c.V1);
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.S1 = c.V0 + c.V1;
        if (c.S1 == 0u) {
            c.A0 = 0u;
            goto L801A8D44;
        }
        c.A0 = 0u;
        c.A1 = 0u | 0x007Fu;
        c.V1 = 0u | 0x00F7u;
        c.T1 = c.A2 - 0x28u;
        c.T0 = 0u | 0x01FEu;
        c.A3 = 0x80180000u;
        c.A3 = c.A3 + 0x62Cu;
        c.A2 = 0u | 0x0F70u;
        L801A8BF4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU8((c.S1 + 0x10u), (byte)c.A1);
        mem.WriteU8((c.S1 + 0x4u), (byte)c.A1);
        mem.WriteU8((c.S1 + 0x11u), (byte)0u);
        mem.WriteU8((c.S1 + 0x5u), (byte)0u);
        mem.WriteU8((c.S1 + 0x12u), (byte)0u);
        mem.WriteU8((c.S1 + 0x6u), (byte)0u);
        mem.WriteU16((c.S1 + 0x14u), (ushort)c.V1);
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.V1);
        c.V0 = mem.ReadU16(c.T1);
        mem.WriteU16((c.S1 + 0x26u), (ushort)c.T0);
        mem.WriteU16((c.S1 + 0x32u), (ushort)0u);
        c.V0 = c.A0 + c.V0;
        mem.WriteU16((c.S1 + 0x16u), (ushort)c.V0);
        mem.WriteU16((c.S1 + 0xAu), (ushort)c.V0);
        c.V0 = c.A0 << 16;
        c.V0 = (uint)((int)c.V0 >> 15);
        c.V0 = c.V0 + c.A3;
        c.V0 = mem.ReadU16(c.V0);
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.A2);
        mem.WriteU16((c.S1 + 0x20u), (ushort)c.V0);
        c.S1 = mem.ReadU32(c.S1);
        if (c.S1 != 0u) {
            c.A0 = c.A0 + 0x1u;
            goto L801A8BF4;
        }
        c.A0 = c.A0 + 0x1u;
        goto L801A8D44;
        L801A8C5C: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x1144u));
        c.V1 = c.V0 << 1;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.V0 = 0x80080000u;
        c.V0 = c.V0 + 0x6FECu;
        c.S1 = c.V1 + c.V0;
        if (c.S1 == 0u) {
            c.A0 = 0u;
            goto L801A8CF0;
        }
        c.A0 = 0u;
        c.A1 = 0u | 0x0004u;
        L801A8C94: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu));
        c.V1 = mem.ReadU16((c.S1 + 0x20u));
        c.V0 = c.V0 - c.V1;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S1 + 0x20u));
        c.V1 = (uint)(short)mem.ReadU16((c.S1 + 0x2Cu));
        c.V0 = c.V0 + 0x2u;
        if ((int)c.V1 >= 0) {
            mem.WriteU16((c.S1 + 0x20u), (ushort)c.V0);
            goto L801A8CC0;
        }
        mem.WriteU16((c.S1 + 0x20u), (ushort)c.V0);
        c.V1 = c.V1 + 0xFu;
        L801A8CC0: ;
        c.V0 = (uint)((int)c.V1 >> 4);
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.V0);
        c.V0 = (int)c.V0 < 5 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801A8CDC;
        }
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.A1);
        goto L801A8CE0;
        L801A8CDC: ;
        c.A0 = 0u | 0x0001u;
        L801A8CE0: ;
        c.S1 = mem.ReadU32(c.S1);
        if (c.S1 != 0u) {
            goto L801A8C94;
        }
        L801A8CF0: ;
        c.V0 = c.A0 << 16;
        if (c.V0 != 0u) {
            goto L801A8F9C;
        }
        c.S0 = 0x801D0000u;
        c.S0 = c.S0 + 0x1144u;
        c.A0 = mem.ReadU32(c.S0);
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x384Cu));
        c.RA = 0x801A8D1Cu;
        Dispatcher.Call(c, m, c.V0);
        c.S1 = 0x801D0000u;
        c.S1 = mem.ReadU32((c.S1 + 0x1140u));
        c.V0 = 0xFFFFFFFFu;
        mem.WriteU32(c.S0, c.V0);
        c.S1 = mem.ReadU32(c.S1);
        c.V0 = 0u | 0x0011u;
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0);
        c.S1 = mem.ReadU32(c.S1);
        c.V0 = 0u | 0x0051u;
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0);
        L801A8D44: ;
        c.V0 = mem.ReadU16((c.S2 + 0x2Eu));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S2 + 0x2Eu), (ushort)c.V0);
        goto L801A8F9C;
        L801A8D58: ;
        c.S1 = 0x801D0000u;
        c.S1 = mem.ReadU32((c.S1 + 0x1140u));
        c.S1 = mem.ReadU32(c.S1);
        c.V0 = mem.ReadU8((c.S1 + 0x4u));
        c.V0 = c.V0 < 0x00000010u ? 1u : 0u;
        if (c.V0 == 0u) {
            c.A0 = 0u;
            goto L801A8D94;
        }
        c.A0 = 0u;
        mem.WriteU8((c.S1 + 0x28u), (byte)0u);
        mem.WriteU8((c.S1 + 0x1Cu), (byte)0u);
        mem.WriteU8((c.S1 + 0x10u), (byte)0u);
        mem.WriteU8((c.S1 + 0x4u), (byte)0u);
        goto L801A8DB0;
        L801A8D94: ;
        c.V0 = mem.ReadU8((c.S1 + 0x28u));
        c.A0 = 0u | 0x0001u;
        c.V0 = c.V0 - 0x10u;
        mem.WriteU8((c.S1 + 0x28u), (byte)c.V0);
        mem.WriteU8((c.S1 + 0x1Cu), (byte)c.V0);
        mem.WriteU8((c.S1 + 0x10u), (byte)c.V0);
        mem.WriteU8((c.S1 + 0x4u), (byte)c.V0);
        L801A8DB0: ;
        c.V0 = mem.ReadU8((c.S1 + 0x6u));
        c.V0 = c.V0 < 0x0000007Fu ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = 0u | 0x007Fu;
            goto L801A8DD0;
        }
        c.V0 = 0u | 0x007Fu;
        c.V0 = mem.ReadU8((c.S1 + 0x12u));
        c.A0 = 0u | 0x0001u;
        c.V0 = c.V0 + 0x8u;
        L801A8DD0: ;
        mem.WriteU8((c.S1 + 0x12u), (byte)c.V0);
        mem.WriteU8((c.S1 + 0x6u), (byte)c.V0);
        c.S1 = mem.ReadU32(c.S1);
        c.V0 = mem.ReadU8((c.S1 + 0x4u));
        c.V0 = c.V0 < 0x00000010u ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0010u;
            goto L801A8E00;
        }
        c.V0 = 0u | 0x0010u;
        c.V0 = mem.ReadU8((c.S1 + 0x6u));
        c.A0 = 0u | 0x0001u;
        c.V0 = c.V0 - 0xFu;
        L801A8E00: ;
        mem.WriteU8((c.S1 + 0x6u), (byte)c.V0);
        mem.WriteU8((c.S1 + 0x5u), (byte)c.V0);
        mem.WriteU8((c.S1 + 0x4u), (byte)c.V0);
        c.V0 = c.A0 << 16;
        L801A8E10: ;
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0001u;
            goto L801A8F9C;
        }
        c.V0 = 0u | 0x0001u;
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        goto L801A8F9C;
        L801A8E20: ;
        c.S1 = 0x801D0000u;
        c.S1 = mem.ReadU32((c.S1 + 0x1140u));
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU16((c.V0 + 0x1124u));
        c.S1 = mem.ReadU32(c.S1);
        c.V0 = c.V0 - 0x1u;
        c.At = 0x801D0000u;
        mem.WriteU16((c.At + 0x1124u), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        c.V1 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V1 < 12 ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L801A8E8C;
        }
        c.S1 = mem.ReadU32(c.S1);
        c.V1 = mem.ReadU8((c.S1 + 0xCu));
        c.V0 = c.V1 + 0xECu;
        mem.WriteU8((c.S1 + 0xCu), (byte)c.V0);
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU16((c.V0 + 0x1124u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u) {
            c.V0 = c.V1 - 0x15u;
            goto L801A8F9C;
        }
        c.V0 = c.V1 - 0x15u;
        mem.WriteU8((c.S1 + 0xCu), (byte)c.V0);
        goto L801A8F9C;
        L801A8E8C: ;
        if (c.V1 != 0u) {
            c.V0 = 0u | 0x0001u;
            goto L801A8EA4;
        }
        c.V0 = 0u | 0x0001u;
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        c.V0 = 0u | 0x0008u;
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0);
        goto L801A8EB8;
        L801A8EA4: ;
        c.V0 = mem.ReadU16((c.S1 + 0x2Eu));
        c.V0 = c.V0 - 0x6u;
        mem.WriteU16((c.S1 + 0x2Eu), (ushort)c.V0);
        mem.WriteU16((c.S1 + 0x22u), (ushort)c.V0);
        L801A8EB8: ;
        c.S1 = mem.ReadU32(c.S1);
        c.V0 = 0u | 0x0008u;
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0);
        goto L801A8F9C;
        L801A8EC8: ;
        c.V1 = mem.ReadU16((c.S2 + 0x30u));
        c.V0 = 0u | 0x0002u;
        if (c.V1 == c.V0) {
            c.V0 = (int)c.V1 < 3 ? 1u : 0u;
            goto L801A8F20;
        }
        c.V0 = (int)c.V1 < 3 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801A8EF0;
        }
        if (c.V1 == 0u) {
            goto L801A8F04;
        }
        goto L801A8F8C;
        L801A8EF0: ;
        c.V0 = 0u | 0x0003u;
        if (c.V1 == c.V0) {
            c.V0 = 0u | 0x0001u;
            goto L801A8F6C;
        }
        c.V0 = 0u | 0x0001u;
        goto L801A8F8C;
        L801A8F04: ;
        c.V1 = 0x80040000u;
        c.V1 = c.V1 - 0x34FCu;
        c.V0 = mem.ReadU32(c.V1);
        c.V0 = c.V0 | 0x8000u;
        mem.WriteU32(c.V1, c.V0);
        goto L801A8F8C;
        L801A8F20: ;
        c.V0 = 0x80040000u;
        c.V0 = c.V0 - 0x34FCu;
        c.V1 = mem.ReadU32(c.V0);
        c.A0 = 0x00010000u;
        c.V1 = c.V1 | c.A0;
        mem.WriteU32(c.V0, c.V1);
        goto L801A8F8C;
        L801A8F3C: ;
        c.V0 = mem.ReadU8(c.S4);
        c.V0 = c.V0 - 0x1u;
        mem.WriteU8(c.S4, (byte)c.V0);
        goto L801A8F9C;
        L801A8F50: ;
        c.V1 = 0x801D0000u;
        c.V1 = c.V1 + 0x1114u;
        c.V0 = mem.ReadU32(c.V1);
        c.V0 = c.V0 - 0x1u;
        mem.WriteU32(c.V1, c.V0);
        goto L801A8F9C;
        L801A8F6C: ;
        c.A0 = 0x80040000u;
        c.A0 = c.A0 - 0x34FCu;
        c.At = 0x80040000u;
        mem.WriteU8((c.At - 0x417Fu), (byte)c.V0);
        c.V0 = mem.ReadU32(c.A0);
        c.V1 = 0x00020000u;
        c.V0 = c.V0 | c.V1;
        mem.WriteU32(c.A0, c.V0);
        L801A8F8C: ;
        c.A0 = c.S2;
        c.RA = 0x801A8F94u;
        SoTN_bo6.DestroyEntity_bo6(c, m);
        c.At = 0x80040000u;
        mem.WriteU32((c.At - 0x38FCu), 0u);
        L801A8F9C: ;
        c.RA = mem.ReadU32((c.SP + 0x44u));
        c.S4 = mem.ReadU32((c.SP + 0x40u));
        c.S3 = mem.ReadU32((c.SP + 0x3Cu));
        c.S2 = mem.ReadU32((c.SP + 0x38u));
        c.S1 = mem.ReadU32((c.SP + 0x34u));
        c.S0 = mem.ReadU32((c.SP + 0x30u));
        c.SP = c.SP + 0x48u;
        return;
        c.A0 = 0x80070000u;
        c.A0 = c.A0 + 0x2F2Cu;
        c.V1 = mem.ReadU32(c.A0);
        c.V0 = c.V1 & 0x0007u;
        if (c.V0 == 0u) {
            c.V0 = 0u;
            goto L801A9020;
        }
        c.V0 = 0u;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3668u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u) {
            mem.WriteU32((c.A0 - 0x38u), 0u);
            goto L801A901C;
        }
        mem.WriteU32((c.A0 - 0x38u), 0u);
        c.V0 = c.V1 & 0x0001u;
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0008u;
            goto L801A9018;
        }
        c.V0 = 0u | 0x0008u;
        c.V0 = c.V1 & 0x0002u;
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x0004u;
            goto L801A9018;
        }
        c.V0 = 0u | 0x0004u;
        c.V0 = c.V1 & 0x0004u;
        if (c.V0 == 0u) {
            c.V0 = 0u | 0x0002u;
            goto L801A901C;
        }
        c.V0 = 0u | 0x0002u;
        L801A9018: ;
        mem.WriteU32((c.A0 - 0x38u), c.V0);
        L801A901C: ;
        c.V0 = 0u | 0x0001u;
        L801A9020: ;
        return;
        c.A1 = 0x80070000u;
        c.A1 = c.A1 + 0x30CCu;
        c.V0 = mem.ReadU16(c.A1);
        c.V0 = c.A0 - c.V0;
        c.V0 = c.V0 << 16;
        c.V1 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V1 < 2 ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L801A905C;
        }
        c.V0 = mem.ReadU32(c.A1);
        c.V0 = c.V0 + 0x1u;
        goto L801A907C;
        L801A905C: ;
        c.V0 = (int)c.V1 < -1 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801A9074;
        }
        c.V0 = mem.ReadU32(c.A1);
        c.V0 = c.V0 - 0x1u;
        goto L801A907C;
        L801A9074: ;
        c.V0 = c.A0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        L801A907C: ;
        mem.WriteU32(c.A1, c.V0);
        return;
        c.V1 = 0x80070000u;
        c.V1 = mem.ReadU16((c.V1 + 0x33DAu));
        c.V0 = 0x80070000u;
        c.V0 = mem.ReadU16((c.V0 + 0x308Eu));
        c.SP = c.SP - 0x18u;
        mem.WriteU32((c.SP + 0x10u), c.S0);
        c.S0 = 0u | 0x0001u;
        mem.WriteU32((c.SP + 0x14u), c.RA);
        c.A1 = mem.ReadU16((c.A0 + 0x2Cu));
        if (c.A1 == c.S0) {
            c.V0 = c.V1 + c.V0;
            goto L801A9130;
        }
        c.V0 = c.V1 + c.V0;
        c.V0 = (int)c.A1 < 2 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801A90D0;
        }
        if (c.A1 == 0u) {
            goto L801A90EC;
        }
        goto L801A91F4;
        L801A90D0: ;
        c.V0 = 0u | 0x0002u;
        if (c.A1 == c.V0) {
            c.V0 = 0u | 0x0003u;
            goto L801A9184;
        }
        c.V0 = 0u | 0x0003u;
        if (c.A1 == c.V0) {
            goto L801A91C4;
        }
        goto L801A91F4;
        L801A90EC: ;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x3C4u;
        c.RA = 0x801A90FCu;
        SoTN_bo6.InitializeEntity_bo6(c, m);
        c.V0 = 0u | 0x8000u;
        c.At = 0x80040000u;
        mem.WriteU32((c.At - 0x3748u), 0u);
        c.At = 0x80090000u;
        mem.WriteU32((c.At + 0x7400u), c.S0);
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x2EF4u), c.V0);
        c.At = 0x80070000u;
        mem.WriteU8((c.At + 0x3510u), (byte)0u);
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x2EFCu), c.S0);
        goto L801A91F4;
        L801A9130: ;
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V0 < 193 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V0 = 0u | 0x8000u;
            goto L801A9154;
        }
        c.V0 = 0u | 0x8000u;
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x2EF4u), c.V0);
        c.V0 = 0u | 0x0001u;
        goto L801A9174;
        L801A9154: ;
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x2EF4u), 0u);
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu));
        c.At = 0x801D0000u;
        mem.WriteU32((c.At + 0x11C4u), c.S0);
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.A0 + 0x2Cu), (ushort)c.V0);
        c.V0 = 0u | 0x0001u;
        L801A9174: ;
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x2EFCu), c.V0);
        goto L801A91F4;
        L801A9184: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x11C4u));
        c.V0 = c.V0 & 0x0002u;
        if (c.V0 == 0u) {
            goto L801A91AC;
        }
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.A0 + 0x2Cu), (ushort)c.V0);
        L801A91AC: ;
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x2EF4u), 0u);
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x2EFCu), c.S0);
        goto L801A91F4;
        L801A91C4: ;
        c.V1 = 0x80090000u;
        c.V1 = c.V1 + 0x7400u;
        c.V0 = mem.ReadU32(c.V1);
        c.At = 0x80040000u;
        mem.WriteU32((c.At - 0x3748u), c.S0);
        if (c.V0 == 0u) {
            c.V0 = 0u | 0x0001u;
            goto L801A91E4;
        }
        c.V0 = 0u | 0x0001u;
        mem.WriteU32(c.V1, 0u);
        L801A91E4: ;
        c.At = 0x80070000u;
        mem.WriteU8((c.At + 0x3510u), (byte)c.V0);
        c.RA = 0x801A91F4u;
        SoTN_bo6.DestroyEntity_bo6(c, m);
        L801A91F4: ;
        c.RA = mem.ReadU32((c.SP + 0x14u));
        c.S0 = mem.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x18u;
        return;
        c.SP = c.SP - 0x20u;
        mem.WriteU32((c.SP + 0x14u), c.S1);
        c.S1 = c.A0;
        mem.WriteU32((c.SP + 0x18u), c.RA);
        mem.WriteU32((c.SP + 0x10u), c.S0);
        c.V1 = mem.ReadU16((c.S1 + 0x2Cu));
        c.V0 = c.V1 < 0x00000005u ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = c.V1 << 2;
            goto L801A95DC;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x801A0000u;
        c.At = c.At + c.V0;
        c.V0 = mem.ReadU32((c.At + 0x63A4u));
        switch (c.V0)
        {
            case 0x801A9248u: goto L801A9248;
            case 0x801A936Cu: goto L801A936C;
            case 0x801A93E0u: goto L801A93E0;
            case 0x801A9470u: goto L801A9470;
            case 0x801A94FCu: goto L801A94FC;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801A9248: ;
        c.A0 = 0u | 0x0003u;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0001u;
        c.RA = 0x801A9260u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.S0 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.S0 == c.V0) {
            goto L801A9358;
        }
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x3C4u;
        c.RA = 0x801A9284u;
        SoTN_bo6.InitializeEntity_bo6(c, m);
        c.V0 = c.S0 << 1;
        c.V0 = c.V0 + c.S0;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.S0;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A1 = c.V0 + c.V1;
        c.V0 = 0u | 0x0001u;
        c.At = 0x80090000u;
        mem.WriteU32((c.At + 0x7400u), c.V0);
        c.V0 = mem.ReadU32((c.S1 + 0x34u));
        c.V1 = 0x00800000u;
        c.At = 0x80040000u;
        mem.WriteU32((c.At - 0x3748u), 0u);
        mem.WriteU32((c.S1 + 0x64u), c.S0);
        mem.WriteU16((c.S1 + 0x54u), (ushort)0u);
        c.V0 = c.V0 | c.V1;
        mem.WriteU32((c.S1 + 0x34u), c.V0);
        c.V0 = 0u | 0x0100u;
        mem.WriteU16((c.A1 + 0x2Cu), (ushort)c.V0);
        mem.WriteU16((c.A1 + 0x14u), (ushort)c.V0);
        c.V0 = 0u | 0x0004u;
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V0);
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0);
        c.V0 = 0u | 0x00E8u;
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V0);
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V0);
        c.V0 = 0u | 0x0080u;
        mem.WriteU8((c.A1 + 0x2Au), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x1Eu), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x12u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x6u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x29u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x11u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x5u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x28u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x1Cu), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x10u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x4u), (byte)c.V0);
        c.V0 = 0u | 0x01F8u;
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0035u;
        mem.WriteU16((c.A1 + 0x20u), (ushort)0u);
        mem.WriteU16((c.A1 + 0x8u), (ushort)0u);
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0);
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)0u);
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x062Fu;
        c.RA = 0x801A9358u;
        Dispatcher.Call(c, m, c.V0);
        L801A9358: ;
        c.V0 = mem.ReadU32((c.S1 + 0x34u));
        c.V1 = 0x00010000u;
        c.V0 = c.V0 | c.V1;
        mem.WriteU32((c.S1 + 0x34u), c.V0);
        goto L801A95DC;
        L801A936C: ;
        c.A0 = 0x80080000u;
        c.A0 = c.A0 + 0x6FECu;
        c.V0 = mem.ReadU16((c.S1 + 0x7Cu));
        c.V1 = mem.ReadU32((c.S1 + 0x64u));
        c.A2 = c.V0 + 0x1u;
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)c.A2);
        c.V1 = mem.ReadU16((c.S1 + 0x7Cu));
        c.V1 = c.V1 < 0x00000009u ? 1u : 0u;
        if (c.V1 != 0u) {
            c.A1 = c.V0 + c.A0;
            goto L801A93C0;
        }
        c.A1 = c.V0 + c.A0;
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
        goto L801A93CC;
        L801A93C0: ;
        c.V0 = c.A2 & 0x0001u;
        if (c.V0 == 0u) {
            c.V0 = 0u | 0x0035u;
            goto L801A93D8;
        }
        c.V0 = 0u | 0x0035u;
        L801A93CC: ;
        c.V0 = 0u | 0x0008u;
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0);
        goto L801A95DC;
        L801A93D8: ;
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0);
        goto L801A95DC;
        L801A93E0: ;
        c.V0 = 0x801D0000u;
        c.V0 = mem.ReadU32((c.V0 + 0x11C4u));
        c.V0 = c.V0 & 0x0100u;
        if (c.V0 == 0u) {
            goto L801A95DC;
        }
        c.At = 0x80090000u;
        mem.WriteU32((c.At + 0x7420u), 0u);
        c.A0 = mem.ReadU16((c.S1 + 0x2Cu));
        c.V1 = mem.ReadU32((c.S1 + 0x64u));
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)0u);
        c.A0 = c.A0 + 0x1u;
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A1 = c.V0 + c.V1;
        c.V0 = 0u | 0x0035u;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.A0);
        mem.WriteU8((c.A1 + 0x2Au), (byte)0u);
        mem.WriteU8((c.A1 + 0x1Eu), (byte)0u);
        mem.WriteU8((c.A1 + 0x12u), (byte)0u);
        mem.WriteU8((c.A1 + 0x6u), (byte)0u);
        mem.WriteU8((c.A1 + 0x29u), (byte)0u);
        mem.WriteU8((c.A1 + 0x1Du), (byte)0u);
        mem.WriteU8((c.A1 + 0x11u), (byte)0u);
        mem.WriteU8((c.A1 + 0x5u), (byte)0u);
        mem.WriteU8((c.A1 + 0x28u), (byte)0u);
        mem.WriteU8((c.A1 + 0x1Cu), (byte)0u);
        mem.WriteU8((c.A1 + 0x10u), (byte)0u);
        mem.WriteU8((c.A1 + 0x4u), (byte)0u);
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0);
        goto L801A95DC;
        L801A9470: ;
        c.V1 = mem.ReadU32((c.S1 + 0x64u));
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A1 = c.V0 + c.V1;
        c.V0 = mem.ReadU8((c.A1 + 0x2Au));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU8((c.A1 + 0x4u), (byte)c.V0);
        c.V1 = mem.ReadU8((c.A1 + 0x4u));
        mem.WriteU8((c.A1 + 0x2Au), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x1Eu), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x12u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x6u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x29u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x11u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x5u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x28u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x1Cu), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x10u), (byte)c.V0);
        c.V0 = 0u | 0x00FFu;
        if (c.V1 != c.V0) {
            c.V0 = 0u | 0x0004u;
            goto L801A95DC;
        }
        c.V0 = 0u | 0x0004u;
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
        goto L801A95DC;
        L801A94FC: ;
        c.V1 = mem.ReadU32((c.S1 + 0x64u));
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A1 = c.V0 + c.V1;
        c.V0 = mem.ReadU8((c.A1 + 0x2Au));
        c.V0 = c.V0 - 0x4u;
        mem.WriteU8((c.A1 + 0x4u), (byte)c.V0);
        c.V1 = mem.ReadU8((c.A1 + 0x4u));
        mem.WriteU8((c.A1 + 0x2Au), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x1Eu), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x12u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x6u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x29u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x11u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x5u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x28u), (byte)c.V0);
        mem.WriteU8((c.A1 + 0x1Cu), (byte)c.V0);
        c.V1 = c.V1 < 0x00000005u ? 1u : 0u;
        if (c.V1 == 0u) {
            mem.WriteU8((c.A1 + 0x10u), (byte)c.V0);
            goto L801A95DC;
        }
        mem.WriteU8((c.A1 + 0x10u), (byte)c.V0);
        c.V1 = 0x80090000u;
        c.V1 = c.V1 + 0x7400u;
        c.V0 = mem.ReadU32(c.V1);
        c.A0 = 0u | 0x0001u;
        c.At = 0x80040000u;
        mem.WriteU32((c.At - 0x3748u), c.A0);
        if (c.V0 == 0u) {
            goto L801A9590;
        }
        mem.WriteU32(c.V1, 0u);
        L801A9590: ;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU8((c.V0 - 0x413Cu));
        if (c.V0 == 0u) {
            c.V0 = 0u | 0x0002u;
            goto L801A95B4;
        }
        c.V0 = 0u | 0x0002u;
        c.At = 0x80090000u;
        mem.WriteU32((c.At + 0x78B4u), c.V0);
        goto L801A95BC;
        L801A95B4: ;
        c.At = 0x80090000u;
        mem.WriteU32((c.At + 0x78B4u), c.A0);
        L801A95BC: ;
        c.V1 = mem.ReadU16((c.S1 + 0x2Cu));
        c.V0 = 0u | 0x0009u;
        c.At = 0x80040000u;
        mem.WriteU32((c.At - 0x38CCu), c.V0);
        c.At = 0x80070000u;
        mem.WriteU32((c.At + 0x3060u), 0u);
        c.V1 = c.V1 + 0x1u;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V1);
        L801A95DC: ;
        c.RA = mem.ReadU32((c.SP + 0x18u));
        c.S1 = mem.ReadU32((c.SP + 0x14u));
        c.S0 = mem.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x20u;
        return;
        }
    public static bool EntityRelicOrb_bo6(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo6", m, false)) return true;
        EntityRelicOrb_bo6_Body(c, m);
        return false;
    }
    private static void EntityRelicOrb_bo6_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo6", 0x801D1114u);
        c.SP = c.SP - 0x50u;
        mem.WriteU32((c.SP + 0x34u), c.S1);
        c.S1 = c.A0;
        mem.WriteU32((c.SP + 0x48u), c.RA);
        mem.WriteU32((c.SP + 0x44u), c.S5);
        mem.WriteU32((c.SP + 0x40u), c.S4);
        mem.WriteU32((c.SP + 0x3Cu), c.S3);
        mem.WriteU32((c.SP + 0x38u), c.S2);
        mem.WriteU32((c.SP + 0x30u), c.S0);
        c.V0 = mem.ReadU16((c.S1 + 0x30u));
        c.V1 = mem.ReadU16((c.S1 + 0x2Cu));
        if (c.V1 == 0u) {
            c.S4 = c.V0 & 0x7FFFu;
            goto L801AFC94;
        }
        c.S4 = c.V0 & 0x7FFFu;
        c.V0 = c.V1 < 0x00000005u ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = c.V1 < 0x0000000Au ? 1u : 0u;
            goto L801AFC9C;
        }
        c.V0 = c.V1 < 0x0000000Au ? 1u : 0u;
        c.V0 = mem.ReadU8((c.S1 + 0x48u));
        if (c.V0 == 0u) {
            c.V0 = 0u | 0x0005u;
            goto L801AFC94;
        }
        c.V0 = 0u | 0x0005u;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
        c.V1 = mem.ReadU16((c.S1 + 0x2Cu));
        L801AFC94: ;
        c.V0 = c.V1 < 0x0000000Au ? 1u : 0u;
        L801AFC9C: ;
        if (c.V0 == 0u) {
            c.V0 = c.V1 << 2;
            goto L801B04FC;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x801A0000u;
        c.At = c.At + c.V0;
        c.V0 = mem.ReadU32((c.At + 0x6480u));
        switch (c.V0)
        {
            case 0x801AFCBCu: goto L801AFCBC;
            case 0x801AFE8Cu: goto L801AFE8C;
            case 0x801B04FCu: goto L801B04FC;
            case 0x801AFEECu: goto L801AFEEC;
            case 0x801B00ACu: goto L801B00AC;
            case 0x801B0174u: goto L801B0174;
            case 0x801B0224u: goto L801B0224;
            case 0x801B039Cu: goto L801B039C;
            case 0x801B086Cu: goto L801B086C;
            case 0x801B0950u: goto L801B0950;
            case 0x801B0B2Cu: goto L801B0B2C;
            case 0x801B0C04u: goto L801B0C04;
            case 0x801B0D08u: goto L801B0D08;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801AFCBC: ;
        c.V0 = c.S4 & 0xFFFFu;
        c.At = 0x80090000u;
        c.At = c.At + c.V0;
        c.V0 = mem.ReadU8((c.At + 0x7964u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 != 0u) {
            goto L801B03FC;
        }
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x3A0u;
        c.S0 = 0u;
        c.RA = 0x801AFCECu;
        SoTN_bo6.InitializeEntity_bo6(c, m);
        c.S5 = 0x801D0000u;
        c.S5 = c.S5 + 0x16A8u;
        c.V0 = c.S0 << 16;
        L801AFCF8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = (uint)((int)c.V0 >> 15);
        c.V0 = c.V0 + c.S5;
        c.V0 = mem.ReadU16(c.V0);
        if (c.V0 == 0u) {
            c.V0 = c.S0 + 0x1u;
            goto L801AFD28;
        }
        c.V0 = c.S0 + 0x1u;
        c.S0 = c.V0;
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V0 < 31 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.V0 = c.S0 << 16;
            goto L801AFCF8;
        }
        c.V0 = c.S0 << 16;
        L801AFD28: ;
        c.V0 = c.S0 << 16;
        c.S3 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.S3 < 31 ? 1u : 0u;
        if (c.V0 == 0u) {
            c.A0 = 0u | 0x0004u;
            goto L801AFD64;
        }
        c.A0 = 0u | 0x0004u;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0007u;
        c.RA = 0x801AFD50u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.S2 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.S2 != c.V0) {
            c.V1 = 0x00800000u;
            goto L801AFD6C;
        }
        c.V1 = 0x00800000u;
        L801AFD64: ;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)0u);
        goto L801B06E0;
        L801AFD6C: ;
        c.V0 = mem.ReadU32((c.S1 + 0x34u));
        mem.WriteU32((c.S1 + 0x64u), c.S2);
        mem.WriteU16((c.S1 + 0x8Cu), (ushort)c.S0);
        c.V0 = c.V0 | c.V1;
        mem.WriteU32((c.S1 + 0x34u), c.V0);
        c.V0 = c.S3 << 1;
        c.V0 = c.V0 + c.S5;
        c.V1 = 0u | 0x0010u;
        mem.WriteU16(c.V0, (ushort)c.V1);
        c.V1 = 0x80040000u;
        c.V1 = mem.ReadU32((c.V1 - 0x37B0u));
        c.V0 = c.S4 << 4;
        c.V0 = c.V0 + c.V1;
        c.A0 = mem.ReadU16((c.V0 + 0x8u));
        c.A1 = mem.ReadU16((c.V0 + 0xAu));
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x37D4u));
        c.A2 = c.S3;
        c.RA = 0x801AFDBCu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.S2 << 1;
        c.V0 = c.V0 + c.S2;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.S2;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A2 = c.V0 + c.V1;
        if (c.A2 == 0u) {
            c.A3 = 0u;
            goto L801AFE68;
        }
        c.A3 = 0u;
        c.T3 = 0u | 0x0008u;
        c.T2 = 0u | 0x001Au;
        c.T1 = c.S0 + 0x1D0u;
        c.V1 = c.S0;
        c.V0 = c.V1 & 0x0007u;
        c.V0 = c.V0 << 4;
        c.T0 = c.V0 | 0x0001u;
        c.A1 = c.V0 | 0x000Fu;
        c.V1 = c.V1 & 0x0018u;
        c.V1 = c.V1 << 1;
        c.A0 = c.V1 | 0x0081u;
        c.V1 = c.V1 | 0x008Fu;
        L801AFE14: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 == 0u) {
            c.V0 = 0u | 0x0006u;
            goto L801AFE24;
        }
        c.V0 = 0u | 0x0006u;
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.T3);
        goto L801AFE50;
        L801AFE24: ;
        mem.WriteU16((c.A2 + 0x1Au), (ushort)c.T2);
        mem.WriteU16((c.A2 + 0xEu), (ushort)c.T1);
        mem.WriteU8((c.A2 + 0x24u), (byte)c.T0);
        mem.WriteU8((c.A2 + 0xCu), (byte)c.T0);
        mem.WriteU8((c.A2 + 0x30u), (byte)c.A1);
        mem.WriteU8((c.A2 + 0x18u), (byte)c.A1);
        mem.WriteU8((c.A2 + 0x19u), (byte)c.A0);
        mem.WriteU8((c.A2 + 0xDu), (byte)c.A0);
        mem.WriteU8((c.A2 + 0x31u), (byte)c.V1);
        mem.WriteU8((c.A2 + 0x25u), (byte)c.V1);
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.V0);
        L801AFE50: ;
        c.V0 = 0u | 0x007Eu;
        mem.WriteU16((c.A2 + 0x26u), (ushort)c.V0);
        c.A2 = mem.ReadU32(c.A2);
        if (c.A2 != 0u) {
            c.A3 = c.A3 + 0x1u;
            goto L801AFE14;
        }
        c.A3 = c.A3 + 0x1u;
        L801AFE68: ;
        c.V0 = 0xFFFF8000u;
        mem.WriteU16((c.S1 + 0x4u), (ushort)c.V0);
        c.V0 = 0u | 0x4000u;
        mem.WriteU32((c.S1 + 0xCu), c.V0);
        c.V0 = 0u | 0x0040u;
        mem.WriteU16((c.S1 + 0x90u), (ushort)c.V0);
        c.V0 = 0xFFFFFE00u;
        mem.WriteU32((c.S1 + 0x94u), c.V0);
        goto L801B04FC;
        L801AFE8C: ;
        c.V0 = mem.ReadU32((c.S1 + 0xCu));
        c.V1 = mem.ReadU16((c.S1 + 0x90u));
        c.A0 = mem.ReadU32((c.S1 + 0x94u));
        c.V1 = c.V1 - 0x1u;
        c.V0 = c.V0 + c.A0;
        mem.WriteU16((c.S1 + 0x90u), (ushort)c.V1);
        c.V1 = c.V1 & 0xFFFFu;
        if (c.V1 != 0u) {
            mem.WriteU32((c.S1 + 0xCu), c.V0);
            goto L801AFEC4;
        }
        mem.WriteU32((c.S1 + 0xCu), c.V0);
        c.V0 = mem.ReadU32((c.S1 + 0x94u));
        c.V1 = 0u | 0x0040u;
        mem.WriteU16((c.S1 + 0x90u), (ushort)c.V1);
        c.V0 = 0u - c.V0;
        mem.WriteU32((c.S1 + 0x94u), c.V0);
        L801AFEC4: ;
        c.RA = 0x801AFECCu;
        Dispatcher.Call(c, m, 0x801AD320u);
        c.V0 = (uint)(short)mem.ReadU16((c.S1 + 0x8Cu));
        c.V1 = 0u | 0x0010u;
        c.V0 = c.V0 << 1;
        c.At = 0x801D0000u;
        c.At = c.At + c.V0;
        mem.WriteU16((c.At + 0x16A8u), (ushort)c.V1);
        goto L801B04FC;
        L801AFEEC: ;
        c.S0 = c.S4 & 0xFFFFu;
        c.A0 = c.S0;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x37B8u));
        c.A1 = 0u | 0x2000u;
        c.RA = 0x801AFF08u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.S0 < 0x00000016u ? 1u : 0u;
        if (c.V0 != 0u) {
            c.A0 = c.SP + 0x20u;
            goto L801AFF40;
        }
        c.A0 = c.SP + 0x20u;
        c.V0 = c.S0 < 0x00000014u ? 1u : 0u;
        if (c.V0 == 0u) {
            c.A1 = 0u;
            goto L801AFF44;
        }
        c.A1 = 0u;
        c.At = 0x80090000u;
        c.At = c.At + c.S0;
        c.V0 = mem.ReadU8((c.At + 0x7964u));
        c.V0 = c.V0 ^ 0x0002u;
        c.At = 0x80090000u;
        c.At = c.At + c.S0;
        mem.WriteU8((c.At + 0x7964u), (byte)c.V0);
        L801AFF40: ;
        c.A1 = 0u;
        L801AFF44: ;
        c.A2 = 0u;
        c.A3 = 0u;
        c.V0 = mem.ReadU32((c.S1 + 0x34u));
        c.V1 = 0x00010000u;
        c.V0 = c.V0 | c.V1;
        mem.WriteU32((c.S1 + 0x34u), c.V0);
        c.V0 = 0u | 0x0100u;
        mem.WriteU16((c.SP + 0x22u), (ushort)c.V0);
        c.V0 = 0u | 0x0040u;
        mem.WriteU16((c.SP + 0x24u), (ushort)c.V0);
        c.V0 = 0u | 0x0010u;
        mem.WriteU16((c.SP + 0x20u), (ushort)0u);
        mem.WriteU16((c.SP + 0x26u), (ushort)c.V0);
        c.RA = 0x801AFF7Cu;
        DialogueGlyphs.Clear(c, mem);
        c.A3 = 0u;
        c.T0 = 0u | 0x0010u;
        c.A0 = 0u | 0x0080u;
        c.V1 = mem.ReadU32((c.S1 + 0x64u));
        c.A1 = 0u | 0x00A7u;
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A2 = c.V0 + c.V1;
        L801AFFB0: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 != 0u) {
            c.V0 = 0u | 0x0003u;
            goto L801AFFFC;
        }
        c.V0 = 0u | 0x0003u;
        c.V0 = 0u | 0x0006u;
        mem.WriteU8((c.A2 + 0x7u), (byte)c.V0);
        c.V0 = 0u | 0x00A0u;
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V0);
        c.V0 = 0u | 0x00F0u;
        mem.WriteU8((c.A2 + 0x18u), (byte)c.V0);
        c.V0 = 0u | 0x01A1u;
        mem.WriteU16((c.A2 + 0xEu), (ushort)c.V0);
        c.V0 = 0u | 0x01FEu;
        mem.WriteU16((c.A2 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0008u;
        mem.WriteU16((c.A2 + 0x1Au), (ushort)c.T0);
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.T0);
        mem.WriteU8((c.A2 + 0xCu), (byte)0u);
        mem.WriteU8((c.A2 + 0xDu), (byte)0u);
        mem.WriteU8((c.A2 + 0x19u), (byte)c.T0);
        goto L801B0088;
        L801AFFFC: ;
        mem.WriteU8((c.A2 + 0x7u), (byte)c.V0);
        c.V0 = 0u | 0x0001u;
        mem.WriteU16((c.A2 + 0x2Cu), (ushort)c.A0);
        mem.WriteU16((c.A2 + 0x20u), (ushort)c.A0);
        mem.WriteU16((c.A2 + 0x14u), (ushort)c.A0);
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.A0);
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.A1);
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.A1);
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.A1);
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.A1);
        mem.WriteU8((c.A2 + 0x28u), (byte)0u);
        mem.WriteU8((c.A2 + 0x1Cu), (byte)0u);
        mem.WriteU8((c.A2 + 0x10u), (byte)0u);
        mem.WriteU8((c.A2 + 0x4u), (byte)0u);
        mem.WriteU8((c.A2 + 0x29u), (byte)0u);
        mem.WriteU8((c.A2 + 0x1Du), (byte)0u);
        mem.WriteU8((c.A2 + 0x11u), (byte)0u);
        mem.WriteU8((c.A2 + 0x5u), (byte)0u);
        mem.WriteU8((c.A2 + 0x2Au), (byte)0u);
        mem.WriteU8((c.A2 + 0x1Eu), (byte)0u);
        mem.WriteU8((c.A2 + 0x12u), (byte)0u);
        if (c.A3 != c.V0) {
            mem.WriteU8((c.A2 + 0x6u), (byte)0u);
            goto L801B006C;
        }
        mem.WriteU8((c.A2 + 0x6u), (byte)0u);
        mem.WriteU8((c.A2 + 0x2Au), (byte)c.A0);
        mem.WriteU8((c.A2 + 0x1Eu), (byte)c.A0);
        mem.WriteU8((c.A2 + 0x12u), (byte)c.A0);
        mem.WriteU8((c.A2 + 0x6u), (byte)c.A0);
        goto L801B007C;
        L801B006C: ;
        mem.WriteU8((c.A2 + 0x29u), (byte)c.A0);
        mem.WriteU8((c.A2 + 0x1Du), (byte)c.A0);
        mem.WriteU8((c.A2 + 0x11u), (byte)c.A0);
        mem.WriteU8((c.A2 + 0x5u), (byte)c.A0);
        L801B007C: ;
        c.V0 = 0u | 0x01FDu;
        mem.WriteU16((c.A2 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0011u;
        L801B0088: ;
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.V0);
        c.A2 = mem.ReadU32(c.A2);
        c.A3 = c.A3 + 0x1u;
        c.V0 = (int)c.A3 < 3 ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L801AFFB0;
        }
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        goto L801B0394;
        L801B00AC: ;
        c.S0 = 0u;
        c.S3 = 0x80080000u;
        c.S3 = c.S3 - 0x101Cu;
        c.V0 = c.S3;
        c.A0 = 0x80180000u;
        c.A0 = mem.ReadU32((c.A0 + 0xFDCu));
        c.A3 = 0u | 0x0BFFu;
        mem.WriteU16((c.SP + 0x28u), (ushort)0u);
        L801B00CC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU8(c.V0, (byte)0u);
        c.A3 = c.A3 - 0x1u;
        if ((int)c.A3 >= 0) {
            c.V0 = c.V0 + 0x1u;
            goto L801B00CC;
        }
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.SP + 0x28u), (ushort)0u);
        c.S2 = c.S4 << 4;
        L801B00E4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.A0);
        if (c.V0 != 0u) {
            c.A1 = c.SP + 0x28u;
            goto L801B0118;
        }
        c.A1 = c.SP + 0x28u;
        if (c.S0 != 0u) {
            c.S0 = 0u | 0x0001u;
            goto L801B012C;
        }
        c.S0 = 0u | 0x0001u;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x37B0u));
        c.V0 = c.S2 + c.V0;
        c.A0 = mem.ReadU32(c.V0);
        goto L801B00E4;
        L801B0118: ;
        c.A2 = c.S3;
        c.A3 = 0u | 0x00C0u;
        c.RA = 0x801B0124u;
        SoTN_bo6.BlitChar_bo6(c, m);
        c.A0 = c.V0;
        goto L801B00E4;
        L801B012C: ;
        c.A0 = c.S3;
        c.A1 = 0u;
        c.A2 = 0u;
        c.A3 = 0u;
        c.V0 = 0u | 0x0100u;
        mem.WriteU32((c.SP + 0x10u), c.V0);
        c.V0 = 0u | 0x0180u;
        mem.WriteU32((c.SP + 0x14u), c.V0);
        c.V0 = 0u | 0x0010u;
        mem.WriteU32((c.SP + 0x18u), c.V0);
        c.RA = 0x801B0158u;
        SoTN_main.LoadTPage(c, m);
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu));
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)0u);
        c.V1 = mem.ReadU16((c.SP + 0x28u));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S1 + 0x7Eu), (ushort)c.V1);
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
        goto L801B04FC;
        L801B0174: ;
        c.V1 = mem.ReadU32((c.S1 + 0x64u));
        c.A3 = 0u;
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.At = 0x80080000u;
        c.At = c.At + c.V0;
        c.A2 = mem.ReadU32((c.At + 0x6FECu));
        L801B019C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 != 0u) {
            goto L801B01D0;
        }
        c.A0 = mem.ReadU16((c.A2 + 0x16u));
        c.A1 = mem.ReadU16((c.A2 + 0x2Eu));
        c.V0 = mem.ReadU16((c.A2 + 0x20u));
        c.V1 = mem.ReadU16((c.A2 + 0x2Cu));
        c.A0 = c.A0 - 0x4u;
        c.A1 = c.A1 + 0x2u;
        c.V0 = c.V0 - 0x3u;
        c.V1 = c.V1 + 0x3u;
        mem.WriteU16((c.A2 + 0x20u), (ushort)c.V0);
        mem.WriteU16((c.A2 + 0x2Cu), (ushort)c.V1);
        goto L801B01F8;
        L801B01D0: ;
        c.A0 = mem.ReadU16((c.A2 + 0x16u));
        c.A1 = mem.ReadU16((c.A2 + 0x2Eu));
        c.V0 = mem.ReadU16((c.A2 + 0x8u));
        c.V1 = mem.ReadU16((c.A2 + 0x14u));
        c.A0 = c.A0 - 0x2u;
        c.A1 = c.A1 + 0x4u;
        c.V0 = c.V0 - 0x3u;
        c.V1 = c.V1 + 0x3u;
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.V0);
        mem.WriteU16((c.A2 + 0x14u), (ushort)c.V1);
        L801B01F8: ;
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.A0);
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.A0);
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.A1);
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.A1);
        c.A2 = mem.ReadU32(c.A2);
        c.A3 = c.A3 + 0x1u;
        c.V0 = (int)c.A3 < 2 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801B036C;
        }
        goto L801B019C;
        L801B0224: ;
        c.A3 = 0u;
        c.A2 = 0x92490000u;
        c.A1 = mem.ReadU16((c.S1 + 0x7Cu));
        c.A2 = c.A2 | 0x2493u;
        c.V0 = c.A1 + 0x1u;
        c.V1 = c.V0 << 1;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.A0 = c.A1 << 4;
        c.A0 = c.A0 - c.A1;
        c.A0 = c.A0 << 3;
        { var _r = (long)(int)c.A0 * (int)c.A2; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V0 = 0u | 0x0080u;
        c.T1 = c.V0 - c.V1;
        c.T0 = c.V1 + 0x80u;
        c.V1 = mem.ReadU32((c.S1 + 0x64u));
        c.A1 = c.A1 << 1;
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.At = 0x80080000u;
        c.At = c.At + c.V0;
        c.A2 = mem.ReadU32((c.At + 0x6FECu));
        c.V0 = c.HI;
        c.V0 = c.V0 + c.A0;
        c.V0 = (uint)((int)c.V0 >> 2);
        c.A0 = (uint)((int)c.A0 >> 31);
        c.A0 = c.V0 - c.A0;
        L801B029C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 != 0u) {
            c.V0 = c.A0 + 0x68u;
            goto L801B02FC;
        }
        c.V0 = c.A0 + 0x68u;
        mem.WriteU16((c.A2 + 0x20u), (ushort)c.V0);
        c.V0 = 0u | 0x0098u;
        c.V0 = c.V0 - c.A0;
        mem.WriteU16((c.A2 + 0x14u), (ushort)c.T1);
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.T0);
        mem.WriteU16((c.A2 + 0x2Cu), (ushort)c.V0);
        c.At = 0x80180000u;
        c.At = c.At + c.A1;
        c.V0 = mem.ReadU16((c.At + 0xFF0u));
        c.V0 = c.V0 + 0xA7u;
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.V0);
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V0);
        c.V0 = mem.ReadU8((c.A2 + 0x2Au));
        c.At = 0x80180000u;
        c.At = c.At + c.A1;
        c.V1 = mem.ReadU16((c.At + 0xFE0u));
        c.V0 = c.V0 - 0x10u;
        c.V1 = c.V1 + 0xA7u;
        mem.WriteU8((c.A2 + 0x2Au), (byte)c.V0);
        mem.WriteU8((c.A2 + 0x1Eu), (byte)c.V0);
        goto L801B0350;
        L801B02FC: ;
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.V0);
        c.V0 = 0u | 0x0098u;
        c.V0 = c.V0 - c.A0;
        mem.WriteU16((c.A2 + 0x14u), (ushort)c.V0);
        mem.WriteU16((c.A2 + 0x2Cu), (ushort)c.T1);
        mem.WriteU16((c.A2 + 0x20u), (ushort)c.T0);
        c.At = 0x80180000u;
        c.At = c.At + c.A1;
        c.V0 = mem.ReadU16((c.At + 0x1000u));
        c.V0 = c.V0 + 0xA7u;
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.V0);
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V0);
        c.V0 = mem.ReadU8((c.A2 + 0x11u));
        c.At = 0x80180000u;
        c.At = c.At + c.A1;
        c.V1 = mem.ReadU16((c.At + 0x1010u));
        c.V0 = c.V0 - 0x10u;
        c.V1 = c.V1 + 0xA7u;
        mem.WriteU8((c.A2 + 0x11u), (byte)c.V0);
        mem.WriteU8((c.A2 + 0x5u), (byte)c.V0);
        L801B0350: ;
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.V1);
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.V1);
        c.A2 = mem.ReadU32(c.A2);
        c.A3 = c.A3 + 0x1u;
        c.V0 = (int)c.A3 < 3 ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L801B029C;
        }
        L801B036C: ;
        c.V0 = mem.ReadU16((c.S1 + 0x7Cu));
        c.V1 = 0u | 0x0008u;
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)c.V0);
        c.V0 = c.V0 & 0xFFFFu;
        if (c.V0 != c.V1) {
            goto L801B04FC;
        }
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu));
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)0u);
        c.V0 = c.V0 + 0x1u;
        L801B0394: ;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
        goto L801B04FC;
        L801B039C: ;
        c.V1 = mem.ReadU32((c.S1 + 0x64u));
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A2 = c.V0 + c.V1;
        c.V1 = mem.ReadU16((c.S1 + 0x7Eu));
        c.V0 = 0u | 0x0080u;
        mem.WriteU16((c.A2 + 0x32u), (ushort)0u);
        c.V0 = c.V0 - c.V1;
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S1 + 0x7Cu));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S1 + 0x7Cu));
        c.V0 = c.V0 < 0x00000061u ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L801B04FC;
        }
        L801B03FC: ;
        c.A0 = c.S1;
        c.RA = 0x801B0404u;
        SoTN_bo6.DestroyEntity_bo6(c, m);
        goto L801B06E0;
        L801B040C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = 0u | 0x001Au;
        mem.WriteU16((c.A2 + 0x1Au), (ushort)c.V0);
        c.V0 = 0u | 0x01B1u;
        mem.WriteU16((c.A2 + 0xEu), (ushort)c.V0);
        c.V0 = 0u | 0x0010u;
        mem.WriteU8((c.A2 + 0x30u), (byte)c.V0);
        mem.WriteU8((c.A2 + 0x18u), (byte)c.V0);
        c.V0 = 0u | 0x0050u;
        mem.WriteU8((c.A2 + 0x19u), (byte)c.V0);
        mem.WriteU8((c.A2 + 0xDu), (byte)c.V0);
        c.V0 = 0u | 0x0060u;
        mem.WriteU8((c.A2 + 0x24u), (byte)0u);
        mem.WriteU8((c.A2 + 0xCu), (byte)0u);
        mem.WriteU8((c.A2 + 0x31u), (byte)c.V0);
        mem.WriteU8((c.A2 + 0x25u), (byte)c.V0);
        c.V1 = mem.ReadU16((c.S1 + 0x82u));
        c.S0 = mem.ReadU16((c.S1 + 0x2u));
        c.V1 = c.V1 & 0x0007u;
        c.V1 = c.V1 << 1;
        c.At = 0x80180000u;
        c.At = c.At + c.V1;
        c.V0 = mem.ReadU16((c.At + 0x1020u));
        c.V0 = c.S0 + c.V0;
        c.A0 = c.V0 - 0x6u;
        c.V0 = c.V0 + 0x6u;
        mem.WriteU16((c.A2 + 0x20u), (ushort)c.A0);
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.A0);
        mem.WriteU16((c.A2 + 0x2Cu), (ushort)c.V0);
        mem.WriteU16((c.A2 + 0x14u), (ushort)c.V0);
        c.S0 = mem.ReadU16((c.S1 + 0x6u));
        c.At = 0x80180000u;
        c.At = c.At + c.V1;
        c.V1 = mem.ReadU16((c.At + 0x1030u));
        c.V0 = 0u | 0x0080u;
        mem.WriteU8((c.A2 + 0x28u), (byte)c.V0);
        mem.WriteU8((c.A2 + 0x1Cu), (byte)c.V0);
        mem.WriteU8((c.A2 + 0x10u), (byte)c.V0);
        mem.WriteU8((c.A2 + 0x4u), (byte)c.V0);
        mem.WriteU8((c.A2 + 0x29u), (byte)c.V0);
        mem.WriteU8((c.A2 + 0x1Du), (byte)c.V0);
        mem.WriteU8((c.A2 + 0x11u), (byte)c.V0);
        mem.WriteU8((c.A2 + 0x5u), (byte)c.V0);
        mem.WriteU8((c.A2 + 0x2Au), (byte)c.V0);
        mem.WriteU8((c.A2 + 0x1Eu), (byte)c.V0);
        c.V1 = c.S0 + c.V1;
        c.A0 = c.V1 - 0x6u;
        c.V1 = c.V1 + 0x6u;
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.A0);
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.A0);
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.V1);
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.V1);
        mem.WriteU8((c.A2 + 0x12u), (byte)c.V0);
        mem.WriteU8((c.A2 + 0x6u), (byte)c.V0);
        c.V0 = 0u | 0x007Fu;
        mem.WriteU16((c.A2 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0037u;
        mem.WriteU8((c.A2 + 0x13u), (byte)0u);
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.V0);
        goto L801B058C;
        L801B04FC: ;
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu));
        c.V0 = c.V0 < 0x00000002u ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801B05B4;
        }
        c.A1 = 0x80040000u;
        c.A1 = mem.ReadU16((c.A1 - 0x3668u));
        c.A0 = c.S1;
        c.RA = 0x801B0520u;
        SoTN_bo6.BlinkItem_bo6(c, m);
        c.V0 = mem.ReadU32((c.S1 + 0x64u));
        c.A3 = 0u | 0x0002u;
        c.V1 = c.V0 << 1;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.V0 = 0x80080000u;
        c.V0 = c.V0 + 0x6FECu;
        c.A2 = c.V1 + c.V0;
        L801B0548: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A2 = mem.ReadU32(c.A2);
        c.A3 = c.A3 - 0x1u;
        if ((int)c.A3 >= 0) {
            goto L801B0548;
        }
        c.V0 = mem.ReadU16((c.S1 + 0x80u));
        if (c.V0 != 0u) {
            c.A3 = 0u;
            goto L801B05A4;
        }
        c.A3 = 0u;
        c.V1 = 0u | 0x0008u;
        L801B056C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU16((c.A2 + 0x32u));
        if (c.V0 == c.V1) {
            c.A3 = c.A3 + 0x1u;
            goto L801B040C;
        }
        c.A3 = c.A3 + 0x1u;
        c.A2 = mem.ReadU32(c.A2);
        c.V0 = (int)c.A3 < 4 ? 1u : 0u;
        if (c.V0 != 0u) {
            goto L801B056C;
        }
        L801B058C: ;
        c.V0 = mem.ReadU16((c.S1 + 0x82u));
        c.V1 = 0u | 0x0004u;
        mem.WriteU16((c.S1 + 0x80u), (ushort)c.V1);
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S1 + 0x82u), (ushort)c.V0);
        goto L801B05B4;
        L801B05A4: ;
        c.V0 = mem.ReadU16((c.S1 + 0x80u));
        c.V0 = c.V0 - 0x1u;
        mem.WriteU16((c.S1 + 0x80u), (ushort)c.V0);
        L801B05B4: ;
        c.V0 = mem.ReadU32((c.S1 + 0x64u));
        c.A3 = 0u | 0x0002u;
        c.V1 = c.V0 << 1;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.V0 = 0x80080000u;
        c.V0 = c.V0 + 0x6FECu;
        c.A2 = c.V1 + c.V0;
        L801B05DC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A2 = mem.ReadU32(c.A2);
        c.A3 = c.A3 - 0x1u;
        if ((int)c.A3 >= 0) {
            goto L801B05DC;
        }
        if (c.A2 == 0u) {
            c.A3 = 0u | 0x0008u;
            goto L801B06E0;
        }
        c.A3 = 0u | 0x0008u;
        L801B05F4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU16((c.A2 + 0x32u));
        if (c.V0 == c.A3) {
            goto L801B06D0;
        }
        c.V0 = mem.ReadU8((c.A2 + 0x13u));
        c.V0 = c.V0 & 0x0003u;
        if (c.V0 == 0u) {
            goto L801B063C;
        }
        c.V1 = mem.ReadU16((c.A2 + 0x16u));
        c.A0 = mem.ReadU16((c.A2 + 0x2Eu));
        c.V0 = c.V1 - 0x1u;
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.V0);
        c.V0 = c.A0 - 0x1u;
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V1);
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.V0);
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.A0);
        goto L801B066C;
        L801B063C: ;
        c.V0 = mem.ReadU16((c.A2 + 0x2Eu));
        c.V1 = mem.ReadU16((c.A2 + 0x20u));
        c.A0 = mem.ReadU16((c.A2 + 0x2Cu));
        c.V0 = c.V0 - 0x2u;
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.V0);
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.V0);
        c.V0 = c.V1 + 0x1u;
        mem.WriteU16((c.A2 + 0x20u), (ushort)c.V0);
        c.V0 = c.A0 - 0x1u;
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.V1);
        mem.WriteU16((c.A2 + 0x2Cu), (ushort)c.V0);
        mem.WriteU16((c.A2 + 0x14u), (ushort)c.A0);
        L801B066C: ;
        c.V1 = mem.ReadU8((c.A2 + 0x28u));
        c.A0 = mem.ReadU8((c.A2 + 0x29u));
        c.V0 = mem.ReadU8((c.A2 + 0x13u));
        c.V1 = c.V1 - 0x6u;
        c.V0 = c.V0 + 0x1u;
        mem.WriteU8((c.A2 + 0x13u), (byte)c.V0);
        c.V0 = mem.ReadU8((c.A2 + 0x2Au));
        c.A1 = mem.ReadU8((c.A2 + 0x13u));
        c.A0 = c.A0 - 0x6u;
        mem.WriteU8((c.A2 + 0x28u), (byte)c.V1);
        mem.WriteU8((c.A2 + 0x1Cu), (byte)c.V1);
        mem.WriteU8((c.A2 + 0x10u), (byte)c.V1);
        mem.WriteU8((c.A2 + 0x4u), (byte)c.V1);
        mem.WriteU8((c.A2 + 0x29u), (byte)c.A0);
        mem.WriteU8((c.A2 + 0x1Du), (byte)c.A0);
        mem.WriteU8((c.A2 + 0x11u), (byte)c.A0);
        mem.WriteU8((c.A2 + 0x5u), (byte)c.A0);
        c.V0 = c.V0 - 0x6u;
        c.A1 = c.A1 < 0x00000011u ? 1u : 0u;
        mem.WriteU8((c.A2 + 0x2Au), (byte)c.V0);
        mem.WriteU8((c.A2 + 0x1Eu), (byte)c.V0);
        mem.WriteU8((c.A2 + 0x12u), (byte)c.V0);
        if (c.A1 != 0u) {
            mem.WriteU8((c.A2 + 0x6u), (byte)c.V0);
            goto L801B06D0;
        }
        mem.WriteU8((c.A2 + 0x6u), (byte)c.V0);
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.A3);
        L801B06D0: ;
        c.A2 = mem.ReadU32(c.A2);
        if (c.A2 != 0u) {
            goto L801B05F4;
        }
        L801B06E0: ;
        c.RA = mem.ReadU32((c.SP + 0x48u));
        c.S5 = mem.ReadU32((c.SP + 0x44u));
        c.S4 = mem.ReadU32((c.SP + 0x40u));
        c.S3 = mem.ReadU32((c.SP + 0x3Cu));
        c.S2 = mem.ReadU32((c.SP + 0x38u));
        c.S1 = mem.ReadU32((c.SP + 0x34u));
        c.S0 = mem.ReadU32((c.SP + 0x30u));
        c.SP = c.SP + 0x50u;
        return;
        c.SP = c.SP - 0x18u;
        c.A2 = c.A0;
        mem.WriteU32((c.SP + 0x10u), c.RA);
        c.V0 = mem.ReadU16((c.A2 + 0x2Cu));
        if (c.V0 != 0u) {
            c.V0 = c.V0 < 0x00000005u ? 1u : 0u;
            goto L801B07B8;
        }
        c.V0 = c.V0 < 0x00000005u ? 1u : 0u;
        c.V0 = mem.ReadU16((c.A2 + 0x30u));
        c.V1 = c.V0;
        c.A0 = c.V1 & 0xFFFFu;
        c.V0 = c.A0 >> 3;
        mem.WriteU16((c.A2 + 0xB4u), (ushort)c.V1);
        c.At = 0x80040000u;
        c.At = c.At + c.V0;
        c.V0 = mem.ReadU8((c.At - 0x4114u));
        c.V1 = c.V1 & 0x0007u;
        c.V0 = (uint)((int)c.V0 >> (int)(c.V1 & 31u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u) {
            c.V0 = c.A0 << 1;
            goto L801B076C;
        }
        c.V0 = c.A0 << 1;
        c.A0 = c.A2;
        c.RA = 0x801B0764u;
        SoTN_bo6.DestroyEntity_bo6(c, m);
        goto L801B0814;
        L801B076C: ;
        c.At = 0x80180000u;
        c.At = c.At + c.V0;
        c.V1 = mem.ReadU16((c.At + 0x610u));
        c.V0 = c.V1 < 0x00000080u ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801B0798;
        }
        c.V0 = 0x801B0000u;
        c.V0 = c.V0 - 0x1674u;
        mem.WriteU32((c.A2 + 0xB8u), c.V0);
        goto L801B07A8;
        L801B0798: ;
        c.V0 = 0x801B0000u;
        c.V0 = c.V0 - 0xC60u;
        mem.WriteU32((c.A2 + 0xB8u), c.V0);
        c.V1 = c.V1 - 0x80u;
        L801B07A8: ;
        c.V0 = 0u | 0x8000u;
        c.V0 = c.V1 + c.V0;
        mem.WriteU16((c.A2 + 0x30u), (ushort)c.V0);
        goto L801B0804;
        L801B07B8: ;
        c.V1 = mem.ReadU16((c.A2 + 0xB4u));
        if (c.V0 == 0u) {
            goto L801B0804;
        }
        c.V0 = mem.ReadU8((c.A2 + 0x48u));
        if (c.V0 == 0u) {
            c.A1 = c.V1 >> 3;
            goto L801B0804;
        }
        c.A1 = c.V1 >> 3;
        c.A0 = c.V1 & 0x0007u;
        c.V0 = 0u | 0x0001u;
        c.At = 0x80040000u;
        c.At = c.At + c.A1;
        c.V1 = mem.ReadU8((c.At - 0x4114u));
        c.V0 = c.V0 << (int)(c.A0 & 31u);
        c.V1 = c.V1 | c.V0;
        c.V0 = 0u | 0x0005u;
        c.At = 0x80040000u;
        c.At = c.At + c.A1;
        mem.WriteU8((c.At - 0x4114u), (byte)c.V1);
        mem.WriteU16((c.A2 + 0x2Cu), (ushort)c.V0);
        L801B0804: ;
        c.V0 = mem.ReadU32((c.A2 + 0xB8u));
        c.A0 = c.A2;
        c.RA = 0x801B0814u;
        Dispatcher.Call(c, m, c.V0);
        L801B0814: ;
        c.RA = mem.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x18u;
        return;
        c.SP = c.SP - 0x50u;
        mem.WriteU32((c.SP + 0x38u), c.S0);
        c.S0 = c.A0;
        mem.WriteU32((c.SP + 0x48u), c.RA);
        mem.WriteU32((c.SP + 0x44u), c.S3);
        mem.WriteU32((c.SP + 0x40u), c.S2);
        mem.WriteU32((c.SP + 0x3Cu), c.S1);
        c.V1 = mem.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V1 < 0x00000005u ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = c.V1 << 2;
            goto L801B0D60;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x801A0000u;
        c.At = c.At + c.V0;
        c.V0 = mem.ReadU32((c.At + 0x64A8u));
        switch (c.V0)
        {
            case 0x801B086Cu: goto L801B086C;
            case 0x801B0950u: goto L801B0950;
            case 0x801B0B2Cu: goto L801B0B2C;
            case 0x801B0C04u: goto L801B0C04;
            case 0x801B0D08u: goto L801B0D08;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801B086C: ;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x3A0u;
        c.RA = 0x801B087Cu;
        SoTN_bo6.InitializeEntity_bo6(c, m);
        c.V1 = 0x00010000u;
        c.V0 = mem.ReadU32((c.S0 + 0x34u));
        c.A0 = mem.ReadU16((c.S0 + 0x30u));
        c.V0 = c.V0 | c.V1;
        c.V1 = 0x08000000u;
        c.V0 = c.V0 ^ c.V1;
        if (c.A0 != 0u) {
            mem.WriteU32((c.S0 + 0x34u), c.V0);
            goto L801B08A4;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0);
        c.V0 = 0u | 0x0060u;
        mem.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        L801B08A4: ;
        c.A0 = 0u | 0x0004u;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0003u;
        c.RA = 0x801B08BCu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.A0 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.A0 != c.V0) {
            c.V0 = c.A0 << 1;
            goto L801B08D8;
        }
        c.V0 = c.A0 << 1;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)0u);
        goto L801B0D60;
        L801B08D8: ;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A1 = c.V0 + c.V1;
        c.V0 = mem.ReadU32((c.S0 + 0x34u));
        c.V1 = 0x00800000u;
        mem.WriteU32((c.S0 + 0x64u), c.A0);
        c.V0 = c.V0 | c.V1;
        if (c.A1 == 0u) {
            mem.WriteU32((c.S0 + 0x34u), c.V0);
            goto L801B0924;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0);
        c.V0 = 0u | 0x0008u;
        L801B0910: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0);
        c.A1 = mem.ReadU32(c.A1);
        if (c.A1 != 0u) {
            goto L801B0910;
        }
        L801B0924: ;
        c.A0 = mem.ReadU32((c.S0 + 0x7Cu));
        c.V0 = mem.ReadU8(c.A0);
        mem.WriteU16((c.S0 + 0x80u), (ushort)c.V0);
        c.V0 = mem.ReadU32((c.S0 + 0x7Cu));
        c.V1 = mem.ReadU8((c.A0 + 0x1u));
        c.V0 = c.V0 + 0x2u;
        mem.WriteU32((c.S0 + 0x7Cu), c.V0);
        mem.WriteU16((c.S0 + 0x82u), (ushort)c.V1);
        goto L801B0D60;
        L801B0950: ;
        c.A0 = c.SP + 0x20u;
        c.A1 = 0u;
        c.A2 = 0u;
        c.V0 = 0u | 0x0180u;
        mem.WriteU16((c.SP + 0x22u), (ushort)c.V0);
        c.V0 = 0u | 0x0040u;
        mem.WriteU16((c.SP + 0x20u), (ushort)0u);
        mem.WriteU16((c.SP + 0x24u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x82u));
        c.A3 = 0u;
        mem.WriteU16((c.SP + 0x26u), (ushort)c.V0);
        c.RA = 0x801B0980u;
        DialogueGlyphs.Clear(c, mem);
        c.V1 = mem.ReadU32((c.S0 + 0x64u));
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A1 = c.V0 + c.V1;
        if (c.A1 == 0u) {
            c.A2 = 0u;
            goto L801B0B20;
        }
        c.A2 = 0u;
        c.A0 = 0u | 0x0080u;
        L801B09B4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            c.V0 = 0u | 0x0003u;
            goto L801B0A2C;
        }
        c.V0 = 0u | 0x0003u;
        c.V0 = 0u | 0x0006u;
        mem.WriteU8((c.A1 + 0x7u), (byte)c.V0);
        c.V0 = 0u | 0x0010u;
        mem.WriteU16((c.A1 + 0x1Au), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x80u));
        c.V1 = mem.ReadU16((c.S0 + 0x2u));
        c.V0 = c.V0 >> 1;
        c.V1 = c.V1 - c.V0;
        mem.WriteU16((c.A1 + 0x8u), (ushort)c.V1);
        c.V0 = mem.ReadU16((c.S0 + 0x82u));
        c.V1 = mem.ReadU16((c.S0 + 0x6u));
        mem.WriteU8((c.A1 + 0xCu), (byte)0u);
        mem.WriteU8((c.A1 + 0xDu), (byte)c.A0);
        c.V0 = c.V0 >> 1;
        c.V1 = c.V1 - c.V0;
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V1);
        c.V0 = mem.ReadU8((c.S0 + 0x80u));
        mem.WriteU8((c.A1 + 0x18u), (byte)c.V0);
        c.V1 = mem.ReadU8((c.S0 + 0x82u));
        c.V0 = 0u | 0x01A1u;
        mem.WriteU16((c.A1 + 0xEu), (ushort)c.V0);
        c.V0 = 0u | 0x01FDu;
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0008u;
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0);
        mem.WriteU8((c.A1 + 0x19u), (byte)c.V1);
        goto L801B0B10;
        L801B0A2C: ;
        mem.WriteU8((c.A1 + 0x7u), (byte)c.V0);
        c.V1 = mem.ReadU16((c.S0 + 0x80u));
        c.V0 = mem.ReadU16((c.S0 + 0x2u));
        c.V1 = c.V1 >> 1;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 - 0x4u;
        mem.WriteU16((c.A1 + 0x20u), (ushort)c.V0);
        mem.WriteU16((c.A1 + 0x8u), (ushort)c.V0);
        c.V1 = mem.ReadU16((c.S0 + 0x80u));
        c.V0 = mem.ReadU16((c.S0 + 0x2u));
        mem.WriteU8((c.A1 + 0x28u), (byte)0u);
        mem.WriteU8((c.A1 + 0x1Cu), (byte)0u);
        mem.WriteU8((c.A1 + 0x10u), (byte)0u);
        mem.WriteU8((c.A1 + 0x4u), (byte)0u);
        mem.WriteU8((c.A1 + 0x29u), (byte)0u);
        mem.WriteU8((c.A1 + 0x1Du), (byte)0u);
        mem.WriteU8((c.A1 + 0x11u), (byte)0u);
        mem.WriteU8((c.A1 + 0x5u), (byte)0u);
        mem.WriteU8((c.A1 + 0x2Au), (byte)0u);
        mem.WriteU8((c.A1 + 0x1Eu), (byte)0u);
        mem.WriteU8((c.A1 + 0x12u), (byte)0u);
        mem.WriteU8((c.A1 + 0x6u), (byte)0u);
        c.V1 = c.V1 >> 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 + 0x4u;
        mem.WriteU16((c.A1 + 0x2Cu), (ushort)c.V0);
        mem.WriteU16((c.A1 + 0x14u), (ushort)c.V0);
        c.V0 = 0u | 0x0001u;
        if (c.A2 != c.V0) {
            goto L801B0ACC;
        }
        c.V1 = mem.ReadU16((c.S0 + 0x82u));
        c.V0 = mem.ReadU16((c.S0 + 0x6u));
        mem.WriteU8((c.A1 + 0x2Au), (byte)c.A0);
        mem.WriteU8((c.A1 + 0x1Eu), (byte)c.A0);
        mem.WriteU8((c.A1 + 0x12u), (byte)c.A0);
        mem.WriteU8((c.A1 + 0x6u), (byte)c.A0);
        c.V1 = c.V1 >> 1;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 - 0x4u;
        goto L801B0AF0;
        L801B0ACC: ;
        c.V1 = mem.ReadU16((c.S0 + 0x82u));
        c.V0 = mem.ReadU16((c.S0 + 0x6u));
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0);
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0);
        mem.WriteU8((c.A1 + 0x11u), (byte)c.A0);
        mem.WriteU8((c.A1 + 0x5u), (byte)c.A0);
        c.V1 = c.V1 >> 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 + 0x4u;
        L801B0AF0: ;
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V0);
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V0);
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V0);
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0);
        c.V0 = 0u | 0x01FCu;
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0011u;
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0);
        L801B0B10: ;
        c.A1 = mem.ReadU32(c.A1);
        if (c.A1 != 0u) {
            c.A2 = c.A2 + 0x1u;
            goto L801B09B4;
        }
        c.A2 = c.A2 + 0x1u;
        L801B0B20: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        goto L801B0D00;
        L801B0B2C: ;
        c.V0 = mem.ReadU16((c.S0 + 0x80u));
        c.V1 = mem.ReadU16((c.S0 + 0x82u));
        c.V0 = c.V0 >> 1;
        { var _r = (long)(int)c.V0 * (int)c.V1; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.S2 = 0x80080000u;
        c.S2 = c.S2 - 0x101Cu;
        c.S1 = c.S2;
        c.A0 = mem.ReadU32((c.S0 + 0x7Cu));
        c.A2 = 0u;
        c.V0 = c.LO;
        if ((int)c.V0 <= 0) {
            mem.WriteU16((c.SP + 0x28u), (ushort)0u);
            goto L801B0B88;
        }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u);
        L801B0B5C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU8(c.S1, (byte)0u);
        c.V0 = mem.ReadU16((c.S0 + 0x80u));
        c.V1 = mem.ReadU16((c.S0 + 0x82u));
        c.V0 = c.V0 >> 1;
        { var _r = (long)(int)c.V0 * (int)c.V1; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.A2 = c.A2 + 0x1u;
        c.V0 = c.LO;
        c.V0 = (int)c.A2 < (int)c.V0 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.S1 = c.S1 + 0x1u;
            goto L801B0B5C;
        }
        c.S1 = c.S1 + 0x1u;
        c.S1 = c.S2;
        L801B0B88: ;
        c.S3 = 0u | 0x0001u;
        L801B0B8C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.A0);
        if (c.V0 == 0u) {
            c.A1 = 0u;
            goto L801B0BD4;
        }
        c.A1 = 0u;
        if (c.V0 != c.S3) {
            c.A1 = c.SP + 0x28u;
            goto L801B0BBC;
        }
        c.A1 = c.SP + 0x28u;
        c.V0 = mem.ReadU16((c.S0 + 0x80u));
        c.A0 = c.A0 + 0x1u;
        mem.WriteU16((c.SP + 0x28u), (ushort)0u);
        c.V0 = c.V0 << 3;
        c.S1 = c.S2 + c.V0;
        goto L801B0B8C;
        L801B0BBC: ;
        c.A3 = mem.ReadU16((c.S0 + 0x80u));
        c.A2 = c.S1;
        c.A3 = c.A3 >> 1;
        c.RA = 0x801B0BCCu;
        SoTN_bo6.BlitChar_bo6(c, m);
        c.A0 = c.V0;
        goto L801B0B8C;
        L801B0BD4: ;
        c.A0 = c.S2;
        c.V0 = 0u | 0x0180u;
        mem.WriteU32((c.SP + 0x10u), c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x80u));
        c.A2 = 0u;
        mem.WriteU32((c.SP + 0x14u), c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x82u));
        c.A3 = 0u;
        mem.WriteU32((c.SP + 0x18u), c.V0);
        c.RA = 0x801B0BFCu;
        SoTN_main.LoadTPage(c, m);
        goto L801B0CF4;
        L801B0C04: ;
        c.V0 = mem.ReadU16((c.S0 + 0x86u));
        c.V1 = mem.ReadU32((c.S0 + 0x64u));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S0 + 0x86u), (ushort)c.V0);
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.At = 0x80080000u;
        c.At = c.At + c.V0;
        c.A1 = mem.ReadU32((c.At + 0x6FECu));
        if (c.A1 == 0u) {
            c.A2 = 0u;
            goto L801B0CE4;
        }
        c.A2 = 0u;
        L801B0C40: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            goto L801B0C90;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x82u));
        c.V1 = mem.ReadU16((c.S0 + 0x86u));
        c.V0 = c.V0 + 0x8u;
        { var _r = (long)(int)c.V0 * (int)c.V1; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V0 = c.LO;
        if ((int)c.V0 >= 0) {
            goto L801B0C68;
        }
        c.V0 = c.V0 + 0x7u;
        L801B0C68: ;
        c.V0 = (uint)((int)c.V0 >> 3);
        c.V1 = mem.ReadU16((c.A1 + 0xAu));
        c.A0 = mem.ReadU8((c.A1 + 0x12u));
        c.V1 = c.V1 + c.V0;
        c.A0 = c.A0 - 0x10u;
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V1);
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V1);
        mem.WriteU8((c.A1 + 0x12u), (byte)c.A0);
        mem.WriteU8((c.A1 + 0x6u), (byte)c.A0);
        goto L801B0CD4;
        L801B0C90: ;
        c.V0 = mem.ReadU16((c.S0 + 0x82u));
        c.V1 = mem.ReadU16((c.S0 + 0x86u));
        c.V0 = c.V0 + 0x8u;
        { var _r = (long)(int)c.V0 * (int)c.V1; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V0 = c.LO;
        if ((int)c.V0 >= 0) {
            goto L801B0CB0;
        }
        c.V0 = c.V0 + 0x7u;
        L801B0CB0: ;
        c.V0 = (uint)((int)c.V0 >> 3);
        c.V1 = mem.ReadU16((c.A1 + 0x22u));
        c.A0 = mem.ReadU8((c.A1 + 0x29u));
        c.V1 = c.V1 - c.V0;
        c.A0 = c.A0 - 0x10u;
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V1);
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V1);
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0);
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0);
        L801B0CD4: ;
        c.A1 = mem.ReadU32(c.A1);
        if (c.A1 != 0u) {
            c.A2 = c.A2 + 0x1u;
            goto L801B0C40;
        }
        c.A2 = c.A2 + 0x1u;
        L801B0CE4: ;
        c.V1 = mem.ReadU16((c.S0 + 0x86u));
        c.V0 = 0u | 0x0008u;
        if (c.V1 != c.V0) {
            goto L801B0D60;
        }
        L801B0CF4: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu));
        mem.WriteU16((c.S0 + 0x86u), (ushort)0u);
        c.V0 = c.V0 + 0x1u;
        L801B0D00: ;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        goto L801B0D60;
        L801B0D08: ;
        c.V1 = mem.ReadU32((c.S0 + 0x64u));
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.At = 0x80080000u;
        c.At = c.At + c.V0;
        mem.WriteU16((c.At + 0x701Eu), (ushort)0u);
        c.V0 = mem.ReadU16((c.S0 + 0x86u));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S0 + 0x86u), (ushort)c.V0);
        c.V1 = mem.ReadU16((c.S0 + 0x86u));
        c.V0 = mem.ReadU16((c.S0 + 0x30u));
        c.V0 = c.V0 < c.V1 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801B0D60;
        }
        c.A0 = c.S0;
        c.RA = 0x801B0D60u;
        SoTN_bo6.DestroyEntity_bo6(c, m);
        L801B0D60: ;
        c.RA = mem.ReadU32((c.SP + 0x48u));
        c.S3 = mem.ReadU32((c.SP + 0x44u));
        c.S2 = mem.ReadU32((c.SP + 0x40u));
        c.S1 = mem.ReadU32((c.SP + 0x3Cu));
        c.S0 = mem.ReadU32((c.SP + 0x38u));
        c.SP = c.SP + 0x50u;
        return;
        }
    public static bool EntityMessageBox_bo6(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo6", m, false)) return true;
        EntityMessageBox_bo6_Body(c, m);
        return false;
    }
    private static void EntityMessageBox_bo6_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo6", 0x801D1114u);
        c.SP = c.SP - 0x50u;
        mem.WriteU32((c.SP + 0x38u), c.S0);
        c.S0 = c.A0;
        mem.WriteU32((c.SP + 0x48u), c.RA);
        mem.WriteU32((c.SP + 0x44u), c.S3);
        mem.WriteU32((c.SP + 0x40u), c.S2);
        mem.WriteU32((c.SP + 0x3Cu), c.S1);
        c.V1 = mem.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V1 < 0x00000005u ? 1u : 0u;
        if (c.V0 == 0u) {
            c.V0 = c.V1 << 2;
            goto L801B0D60;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x801A0000u;
        c.At = c.At + c.V0;
        c.V0 = mem.ReadU32((c.At + 0x64A8u));
        switch (c.V0)
        {
            case 0x801B086Cu: goto L801B086C;
            case 0x801B0950u: goto L801B0950;
            case 0x801B0B2Cu: goto L801B0B2C;
            case 0x801B0C04u: goto L801B0C04;
            case 0x801B0D08u: goto L801B0D08;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801B086C: ;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x3A0u;
        c.RA = 0x801B087Cu;
        SoTN_bo6.InitializeEntity_bo6(c, m);
        c.V1 = 0x00010000u;
        c.V0 = mem.ReadU32((c.S0 + 0x34u));
        c.A0 = mem.ReadU16((c.S0 + 0x30u));
        c.V0 = c.V0 | c.V1;
        c.V1 = 0x08000000u;
        c.V0 = c.V0 ^ c.V1;
        if (c.A0 != 0u) {
            mem.WriteU32((c.S0 + 0x34u), c.V0);
            goto L801B08A4;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0);
        c.V0 = 0u | 0x0060u;
        mem.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        L801B08A4: ;
        c.A0 = 0u | 0x0004u;
        c.V0 = 0x80040000u;
        c.V0 = mem.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0003u;
        c.RA = 0x801B08BCu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.A0 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.A0 != c.V0) {
            c.V0 = c.A0 << 1;
            goto L801B08D8;
        }
        c.V0 = c.A0 << 1;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)0u);
        goto L801B0D60;
        L801B08D8: ;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A1 = c.V0 + c.V1;
        c.V0 = mem.ReadU32((c.S0 + 0x34u));
        c.V1 = 0x00800000u;
        mem.WriteU32((c.S0 + 0x64u), c.A0);
        c.V0 = c.V0 | c.V1;
        if (c.A1 == 0u) {
            mem.WriteU32((c.S0 + 0x34u), c.V0);
            goto L801B0924;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0);
        c.V0 = 0u | 0x0008u;
        L801B0910: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0);
        c.A1 = mem.ReadU32(c.A1);
        if (c.A1 != 0u) {
            goto L801B0910;
        }
        L801B0924: ;
        c.A0 = mem.ReadU32((c.S0 + 0x7Cu));
        c.V0 = mem.ReadU8(c.A0);
        mem.WriteU16((c.S0 + 0x80u), (ushort)c.V0);
        c.V0 = mem.ReadU32((c.S0 + 0x7Cu));
        c.V1 = mem.ReadU8((c.A0 + 0x1u));
        c.V0 = c.V0 + 0x2u;
        mem.WriteU32((c.S0 + 0x7Cu), c.V0);
        mem.WriteU16((c.S0 + 0x82u), (ushort)c.V1);
        goto L801B0D60;
        L801B0950: ;
        c.A0 = c.SP + 0x20u;
        c.A1 = 0u;
        c.A2 = 0u;
        c.V0 = 0u | 0x0180u;
        mem.WriteU16((c.SP + 0x22u), (ushort)c.V0);
        c.V0 = 0u | 0x0040u;
        mem.WriteU16((c.SP + 0x20u), (ushort)0u);
        mem.WriteU16((c.SP + 0x24u), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x82u));
        c.A3 = 0u;
        mem.WriteU16((c.SP + 0x26u), (ushort)c.V0);
        c.RA = 0x801B0980u;
        DialogueGlyphs.Clear(c, mem);
        c.V1 = mem.ReadU32((c.S0 + 0x64u));
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A1 = c.V0 + c.V1;
        if (c.A1 == 0u) {
            c.A2 = 0u;
            goto L801B0B20;
        }
        c.A2 = 0u;
        c.A0 = 0u | 0x0080u;
        L801B09B4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            c.V0 = 0u | 0x0003u;
            goto L801B0A2C;
        }
        c.V0 = 0u | 0x0003u;
        c.V0 = 0u | 0x0006u;
        mem.WriteU8((c.A1 + 0x7u), (byte)c.V0);
        c.V0 = 0u | 0x0010u;
        mem.WriteU16((c.A1 + 0x1Au), (ushort)c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x80u));
        c.V1 = mem.ReadU16((c.S0 + 0x2u));
        c.V0 = c.V0 >> 1;
        c.V1 = c.V1 - c.V0;
        mem.WriteU16((c.A1 + 0x8u), (ushort)c.V1);
        c.V0 = mem.ReadU16((c.S0 + 0x82u));
        c.V1 = mem.ReadU16((c.S0 + 0x6u));
        mem.WriteU8((c.A1 + 0xCu), (byte)0u);
        mem.WriteU8((c.A1 + 0xDu), (byte)c.A0);
        c.V0 = c.V0 >> 1;
        c.V1 = c.V1 - c.V0;
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V1);
        c.V0 = mem.ReadU8((c.S0 + 0x80u));
        mem.WriteU8((c.A1 + 0x18u), (byte)c.V0);
        c.V1 = mem.ReadU8((c.S0 + 0x82u));
        c.V0 = 0u | 0x01A1u;
        mem.WriteU16((c.A1 + 0xEu), (ushort)c.V0);
        c.V0 = 0u | 0x01FDu;
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0008u;
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0);
        mem.WriteU8((c.A1 + 0x19u), (byte)c.V1);
        goto L801B0B10;
        L801B0A2C: ;
        mem.WriteU8((c.A1 + 0x7u), (byte)c.V0);
        c.V1 = mem.ReadU16((c.S0 + 0x80u));
        c.V0 = mem.ReadU16((c.S0 + 0x2u));
        c.V1 = c.V1 >> 1;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 - 0x4u;
        mem.WriteU16((c.A1 + 0x20u), (ushort)c.V0);
        mem.WriteU16((c.A1 + 0x8u), (ushort)c.V0);
        c.V1 = mem.ReadU16((c.S0 + 0x80u));
        c.V0 = mem.ReadU16((c.S0 + 0x2u));
        mem.WriteU8((c.A1 + 0x28u), (byte)0u);
        mem.WriteU8((c.A1 + 0x1Cu), (byte)0u);
        mem.WriteU8((c.A1 + 0x10u), (byte)0u);
        mem.WriteU8((c.A1 + 0x4u), (byte)0u);
        mem.WriteU8((c.A1 + 0x29u), (byte)0u);
        mem.WriteU8((c.A1 + 0x1Du), (byte)0u);
        mem.WriteU8((c.A1 + 0x11u), (byte)0u);
        mem.WriteU8((c.A1 + 0x5u), (byte)0u);
        mem.WriteU8((c.A1 + 0x2Au), (byte)0u);
        mem.WriteU8((c.A1 + 0x1Eu), (byte)0u);
        mem.WriteU8((c.A1 + 0x12u), (byte)0u);
        mem.WriteU8((c.A1 + 0x6u), (byte)0u);
        c.V1 = c.V1 >> 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 + 0x4u;
        mem.WriteU16((c.A1 + 0x2Cu), (ushort)c.V0);
        mem.WriteU16((c.A1 + 0x14u), (ushort)c.V0);
        c.V0 = 0u | 0x0001u;
        if (c.A2 != c.V0) {
            goto L801B0ACC;
        }
        c.V1 = mem.ReadU16((c.S0 + 0x82u));
        c.V0 = mem.ReadU16((c.S0 + 0x6u));
        mem.WriteU8((c.A1 + 0x2Au), (byte)c.A0);
        mem.WriteU8((c.A1 + 0x1Eu), (byte)c.A0);
        mem.WriteU8((c.A1 + 0x12u), (byte)c.A0);
        mem.WriteU8((c.A1 + 0x6u), (byte)c.A0);
        c.V1 = c.V1 >> 1;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 - 0x4u;
        goto L801B0AF0;
        L801B0ACC: ;
        c.V1 = mem.ReadU16((c.S0 + 0x82u));
        c.V0 = mem.ReadU16((c.S0 + 0x6u));
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0);
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0);
        mem.WriteU8((c.A1 + 0x11u), (byte)c.A0);
        mem.WriteU8((c.A1 + 0x5u), (byte)c.A0);
        c.V1 = c.V1 >> 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 + 0x4u;
        L801B0AF0: ;
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V0);
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V0);
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V0);
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0);
        c.V0 = 0u | 0x01FCu;
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0011u;
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0);
        L801B0B10: ;
        c.A1 = mem.ReadU32(c.A1);
        if (c.A1 != 0u) {
            c.A2 = c.A2 + 0x1u;
            goto L801B09B4;
        }
        c.A2 = c.A2 + 0x1u;
        L801B0B20: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        goto L801B0D00;
        L801B0B2C: ;
        c.V0 = mem.ReadU16((c.S0 + 0x80u));
        c.V1 = mem.ReadU16((c.S0 + 0x82u));
        c.V0 = c.V0 >> 1;
        { var _r = (long)(int)c.V0 * (int)c.V1; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.S2 = 0x80080000u;
        c.S2 = c.S2 - 0x101Cu;
        c.S1 = c.S2;
        c.A0 = mem.ReadU32((c.S0 + 0x7Cu));
        c.A2 = 0u;
        c.V0 = c.LO;
        if ((int)c.V0 <= 0) {
            mem.WriteU16((c.SP + 0x28u), (ushort)0u);
            goto L801B0B88;
        }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u);
        L801B0B5C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU8(c.S1, (byte)0u);
        c.V0 = mem.ReadU16((c.S0 + 0x80u));
        c.V1 = mem.ReadU16((c.S0 + 0x82u));
        c.V0 = c.V0 >> 1;
        { var _r = (long)(int)c.V0 * (int)c.V1; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.A2 = c.A2 + 0x1u;
        c.V0 = c.LO;
        c.V0 = (int)c.A2 < (int)c.V0 ? 1u : 0u;
        if (c.V0 != 0u) {
            c.S1 = c.S1 + 0x1u;
            goto L801B0B5C;
        }
        c.S1 = c.S1 + 0x1u;
        c.S1 = c.S2;
        L801B0B88: ;
        c.S3 = 0u | 0x0001u;
        L801B0B8C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.A0);
        if (c.V0 == 0u) {
            c.A1 = 0u;
            goto L801B0BD4;
        }
        c.A1 = 0u;
        if (c.V0 != c.S3) {
            c.A1 = c.SP + 0x28u;
            goto L801B0BBC;
        }
        c.A1 = c.SP + 0x28u;
        c.V0 = mem.ReadU16((c.S0 + 0x80u));
        c.A0 = c.A0 + 0x1u;
        mem.WriteU16((c.SP + 0x28u), (ushort)0u);
        c.V0 = c.V0 << 3;
        c.S1 = c.S2 + c.V0;
        goto L801B0B8C;
        L801B0BBC: ;
        c.A3 = mem.ReadU16((c.S0 + 0x80u));
        c.A2 = c.S1;
        c.A3 = c.A3 >> 1;
        c.RA = 0x801B0BCCu;
        SoTN_bo6.BlitChar_bo6(c, m);
        c.A0 = c.V0;
        goto L801B0B8C;
        L801B0BD4: ;
        c.A0 = c.S2;
        c.V0 = 0u | 0x0180u;
        mem.WriteU32((c.SP + 0x10u), c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x80u));
        c.A2 = 0u;
        mem.WriteU32((c.SP + 0x14u), c.V0);
        c.V0 = mem.ReadU16((c.S0 + 0x82u));
        c.A3 = 0u;
        mem.WriteU32((c.SP + 0x18u), c.V0);
        c.RA = 0x801B0BFCu;
        SoTN_main.LoadTPage(c, m);
        goto L801B0CF4;
        L801B0C04: ;
        c.V0 = mem.ReadU16((c.S0 + 0x86u));
        c.V1 = mem.ReadU32((c.S0 + 0x64u));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S0 + 0x86u), (ushort)c.V0);
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.At = 0x80080000u;
        c.At = c.At + c.V0;
        c.A1 = mem.ReadU32((c.At + 0x6FECu));
        if (c.A1 == 0u) {
            c.A2 = 0u;
            goto L801B0CE4;
        }
        c.A2 = 0u;
        L801B0C40: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            goto L801B0C90;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x82u));
        c.V1 = mem.ReadU16((c.S0 + 0x86u));
        c.V0 = c.V0 + 0x8u;
        { var _r = (long)(int)c.V0 * (int)c.V1; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V0 = c.LO;
        if ((int)c.V0 >= 0) {
            goto L801B0C68;
        }
        c.V0 = c.V0 + 0x7u;
        L801B0C68: ;
        c.V0 = (uint)((int)c.V0 >> 3);
        c.V1 = mem.ReadU16((c.A1 + 0xAu));
        c.A0 = mem.ReadU8((c.A1 + 0x12u));
        c.V1 = c.V1 + c.V0;
        c.A0 = c.A0 - 0x10u;
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V1);
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V1);
        mem.WriteU8((c.A1 + 0x12u), (byte)c.A0);
        mem.WriteU8((c.A1 + 0x6u), (byte)c.A0);
        goto L801B0CD4;
        L801B0C90: ;
        c.V0 = mem.ReadU16((c.S0 + 0x82u));
        c.V1 = mem.ReadU16((c.S0 + 0x86u));
        c.V0 = c.V0 + 0x8u;
        { var _r = (long)(int)c.V0 * (int)c.V1; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V0 = c.LO;
        if ((int)c.V0 >= 0) {
            goto L801B0CB0;
        }
        c.V0 = c.V0 + 0x7u;
        L801B0CB0: ;
        c.V0 = (uint)((int)c.V0 >> 3);
        c.V1 = mem.ReadU16((c.A1 + 0x22u));
        c.A0 = mem.ReadU8((c.A1 + 0x29u));
        c.V1 = c.V1 - c.V0;
        c.A0 = c.A0 - 0x10u;
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V1);
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V1);
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0);
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0);
        L801B0CD4: ;
        c.A1 = mem.ReadU32(c.A1);
        if (c.A1 != 0u) {
            c.A2 = c.A2 + 0x1u;
            goto L801B0C40;
        }
        c.A2 = c.A2 + 0x1u;
        L801B0CE4: ;
        c.V1 = mem.ReadU16((c.S0 + 0x86u));
        c.V0 = 0u | 0x0008u;
        if (c.V1 != c.V0) {
            goto L801B0D60;
        }
        L801B0CF4: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu));
        mem.WriteU16((c.S0 + 0x86u), (ushort)0u);
        c.V0 = c.V0 + 0x1u;
        L801B0D00: ;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        goto L801B0D60;
        L801B0D08: ;
        c.V1 = mem.ReadU32((c.S0 + 0x64u));
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.At = 0x80080000u;
        c.At = c.At + c.V0;
        mem.WriteU16((c.At + 0x701Eu), (ushort)0u);
        c.V0 = mem.ReadU16((c.S0 + 0x86u));
        c.V0 = c.V0 + 0x1u;
        mem.WriteU16((c.S0 + 0x86u), (ushort)c.V0);
        c.V1 = mem.ReadU16((c.S0 + 0x86u));
        c.V0 = mem.ReadU16((c.S0 + 0x30u));
        c.V0 = c.V0 < c.V1 ? 1u : 0u;
        if (c.V0 == 0u) {
            goto L801B0D60;
        }
        c.A0 = c.S0;
        c.RA = 0x801B0D60u;
        SoTN_bo6.DestroyEntity_bo6(c, m);
        L801B0D60: ;
        c.RA = mem.ReadU32((c.SP + 0x48u));
        c.S3 = mem.ReadU32((c.SP + 0x44u));
        c.S2 = mem.ReadU32((c.SP + 0x40u));
        c.S1 = mem.ReadU32((c.SP + 0x3Cu));
        c.S0 = mem.ReadU32((c.SP + 0x38u));
        c.SP = c.SP + 0x50u;
        return;
        }
}
