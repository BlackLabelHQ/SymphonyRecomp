using RecompOne.Runtime.Context;
using RecompOne.Runtime.Dispatch;
using RecompOne.Runtime.Memory;

namespace Recompiled.Translation;

public static class ScriptFunctions_bo5
{
    public static bool BO5_CutsceneUnk1(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo5", m, true)) return true;
        BO5_CutsceneUnk1_Body(c, m);
        return false;
    }
    private static void BO5_CutsceneUnk1_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo5", 0x801B553Cu);
        { c.V0 = 0u | 0x0002u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x5548u), (ushort)c.V0); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x5546u), (ushort)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x5544u)); 
        { c.V1 = 0u | 0x0008u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x554Au), (ushort)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x554Eu), (ushort)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU8((c.At + 0x5552u), (byte)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU8((c.At + 0x5553u), (byte)c.V1); 
        { c.V0 = c.V0 + 0x14u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x5542u), (ushort)c.V0); 
        return;
        }
    public static bool BO5_SetCutsceneScript(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo5", m, true)) return true;
        BO5_SetCutsceneScript_Body(c, m);
        return false;
    }
    private static void BO5_SetCutsceneScript_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo5", 0x801B553Cu);
        c.A0 = DialogueScripts.ResolveStart("bo5", c.A0, m);
        { c.SP = c.SP - 0x20u;  }
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        c.S1 = c.A0; 
        { c.A0 = 0u | 0x0006u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0007u;  }
        mem.WriteU32((c.SP + 0x18u), c.RA); 
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.RA = 0x801A4618u;
        Dispatcher.Call(c, m, c.V0);
        c.S0 = 0x801B0000u; 
        { c.S0 = c.S0 + 0x5574u;  }
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        c.V1 = 0xFFFFFFFFu; 
        if (c.V0 != c.V1) {
            mem.WriteU32(c.S0, c.V0); 
            goto L801A4640;
        }
        mem.WriteU32(c.S0, c.V0); 
        mem.WriteU32(c.S0, 0u); 
        c.V0 = 0u; 
        goto L801A479C;
        L801A4640: ;
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x553Cu), c.S1); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x5578u), (ushort)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x5570u), c.V1); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x556Cu), c.V1); 
        c.RA = 0x801A4668u; 
        SoTN_bo5.BO5_CutsceneUnk1(c, m);
        c.V1 = mem.ReadU32(c.S0); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x5554u), c.V0); 
        c.V1 = mem.ReadU32(c.V0); 
        { c.A2 = 0u | 0x0008u;  }
        mem.WriteU16((c.V0 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x5558u), c.V1); 
        c.V0 = mem.ReadU32(c.V1); 
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x555Cu), c.V0); 
        c.V1 = mem.ReadU32(c.V0); 
        mem.WriteU16((c.V0 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x5560u), c.V1); 
        c.A0 = mem.ReadU32(c.V1); 
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x5564u), c.A0); 
        c.A1 = mem.ReadU32(c.A0); 
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.A0 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x5568u), c.A1); 
        c.V1 = mem.ReadU32(c.A1); 
        { c.A0 = 0u | 0x0004u;  }
        mem.WriteU8((c.A1 + 0x7u), (byte)c.A0); 
        { c.A0 = 0u | 0x0003u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.A2); 
        { c.A1 = 0u | 0x00FFu;  }
        mem.WriteU8((c.V1 + 0x7u), (byte)c.A0); 
        { c.A0 = 0u | 0x0004u;  }
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
        { c.A0 = 0u | 0x00F8u;  }
        mem.WriteU16((c.V1 + 0x2Cu), (ushort)c.A0); 
        mem.WriteU16((c.V1 + 0x14u), (ushort)c.A0); 
        { c.A0 = 0u | 0x01FDu;  }
        mem.WriteU16((c.V1 + 0x26u), (ushort)c.A0); 
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A2); 
        c.V1 = mem.ReadU32(c.V1); 
        { c.A0 = 0u | 0x0001u;  }
        mem.WriteU8((c.V1 + 0x7u), (byte)c.A0); 
        { c.A0 = 0u | 0x0003u;  }
        mem.WriteU16((c.V1 + 0x8u), (ushort)c.A0); 
        { c.A0 = 0u | 0x002Fu;  }
        mem.WriteU16((c.V1 + 0xAu), (ushort)c.A0); 
        { c.A0 = 0u | 0x004Au;  }
        mem.WriteU8((c.V1 + 0xDu), (byte)c.A0); 
        { c.A0 = 0u | 0x01FCu;  }
        mem.WriteU8((c.V1 + 0x6u), (byte)c.A1); 
        mem.WriteU8((c.V1 + 0x5u), (byte)c.A1); 
        mem.WriteU8((c.V1 + 0x4u), (byte)c.A1); 
        mem.WriteU16((c.V1 + 0x26u), (ushort)c.A0); 
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A2); 
        L801A479C: ;
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool BO5_CutsceneUnk3(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo5", m, false)) return true;
        BO5_CutsceneUnk3_Body(c, m);
        return false;
    }
    private static void BO5_CutsceneUnk3_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo5", 0x801B553Cu);
        { c.SP = c.SP - 0x20u;  }
        { c.A0 = c.A0 << 16;  }
        { c.A0 = (uint)((int)c.A0 >> 16);  }
        { c.V0 = c.A0 << 1;  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { c.V0 = c.V0 + 0x180u;  }
        mem.WriteU16((c.SP + 0x12u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0040u;  }
        mem.WriteU16((c.SP + 0x14u), (ushort)c.V0); 
        { c.V0 = 0u | 0x000Cu;  }
        { c.A0 = c.SP + 0x10u;  }
        c.A1 = 0u; 
        c.A2 = 0u; 
        c.A3 = 0u; 
        mem.WriteU32((c.SP + 0x18u), c.RA); 
        mem.WriteU16((c.SP + 0x10u), (ushort)0u); 
        mem.WriteU16((c.SP + 0x16u), (ushort)c.V0); 
        c.RA = 0x801A4800u; 
        DialogueGlyphs.Clear(c, mem);
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool BO5_CutsceneUnk4(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo5", m, true)) return true;
        BO5_CutsceneUnk4_Body(c, m);
        return false;
    }
    private static void BO5_CutsceneUnk4_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo5", 0x801B553Cu);
        DialogueGlyphs.PrepareLine(c, mem);
        { c.SP = c.SP - 0x18u;  }
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.S0 = 0x801B0000u; 
        { c.S0 = c.S0 + 0x554Au;  }
        mem.WriteU32((c.SP + 0x14u), c.RA); 
        c.A0 = (uint)(short)mem.ReadU16(c.S0); 
        c.RA = 0x801A4830u; 
        SoTN_bo5.BO5_CutsceneUnk3(c, m);
        c.V0 = (uint)(short)mem.ReadU16(c.S0); 
        { c.V0 = c.V0 << 2;  }
        { var _s = c.S0; var _t = c.V0; c.V0 = _s + _t;  }
        c.V1 = mem.ReadU32((c.V0 + 0xAu)); 
        { c.V0 = 0u | 0x0010u;  }
        mem.WriteU16((c.V1 + 0x1Au), (ushort)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x5550u)); 
        mem.WriteU16((c.V1 + 0xEu), (ushort)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x5542u)); 
        mem.WriteU8((c.V1 + 0xCu), (byte)0u); 
        mem.WriteU16((c.V1 + 0xAu), (ushort)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x5540u)); 
        { c.V0 = c.V0 + 0x4u;  }
        mem.WriteU16((c.V1 + 0x8u), (ushort)c.V0); 
        c.A0 = (uint)(short)mem.ReadU16(c.S0); 
        { c.V0 = 0u | 0x00C0u;  }
        mem.WriteU8((c.V1 + 0x18u), (byte)c.V0); 
        { c.V0 = 0u | 0x000Cu;  }
        mem.WriteU8((c.V1 + 0x19u), (byte)c.V0); 
        { c.V0 = 0u | 0x01FFu;  }
        mem.WriteU16((c.V1 + 0x26u), (ushort)c.V0); 
        mem.WriteU16((c.V1 + 0x32u), (ushort)0u); 
        { c.V0 = c.A0 << 1;  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { c.V0 = c.V0 - 0x80u;  }
        mem.WriteU8((c.V1 + 0xDu), (byte)c.V0); 
        c.RA = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x18u;  }
        DialogueGlyphs.BeginLine(mem);
        return;
        }
    public static bool BO5_DrawCutsceneActorName(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo5", m, true)) return true;
        BO5_DrawCutsceneActorName_Body(c, m);
        return false;
    }
    private static void BO5_DrawCutsceneActorName_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo5", 0x801B553Cu);
        { c.SP = c.SP - 0x20u;  }
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        c.S1 = c.A1; 
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.S0 = c.A0; 
        { c.V0 = c.S0 & 0xFFFFu;  }
        { c.V0 = c.V0 << 2;  }
        c.A1 = 0u; 
        mem.WriteU32((c.SP + 0x18u), c.RA); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A2 = mem.ReadU32((c.At + 0xC1Cu)); 
        { c.V0 = 0u | 0x00FFu;  }
        L801A48FC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V1 = mem.ReadU8(c.A2); 
        if (c.V1 != c.V0) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801A491C;
        }
        { c.A2 = c.A2 + 0x1u;  }
        c.V1 = mem.ReadU8(c.A2); 
        if (c.V1 == 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801A492C;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801A491C: ;
        if (c.V1 == 0u) {
            goto L801A48FC;
        }
        { c.A1 = c.A1 + 0x1u;  }
        goto L801A48FC;
        L801A492C: ;
        { c.A0 = 0u | 0x0006u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = c.A1 & 0xFFFFu;  }
        c.RA = 0x801A4944u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.A1 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.A1 != c.V0) {
            { c.V0 = c.A1 << 1;  }
            goto L801A4968;
        }
        { c.V0 = c.A1 << 1;  }
        c.A0 = c.S1; 
        c.RA = 0x801A4960u; 
        SoTN_bo5.DestroyEntity_bo5(c, m);
        goto L801A4A34;
        L801A4968: ;
        { var _s = c.V0; var _t = c.A1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.A1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A0 = _s + _t;  }
        c.V1 = 0x801B0000u; 
        { c.V1 = c.V1 + 0x5570u;  }
        { c.V0 = c.S0 & 0xFFFFu;  }
        { c.V0 = c.V0 << 2;  }
        mem.WriteU32(c.V1, c.A1); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A2 = mem.ReadU32((c.At + 0xC1Cu)); 
        if (c.A0 == 0u) {
            { c.A3 = 0u | 0x0038u;  }
            goto L801A4A34;
        }
        { c.A3 = 0u | 0x0038u;  }
        { c.T4 = 0u | 0x0006u;  }
        { c.T3 = 0u | 0x001Eu;  }
        { c.T2 = 0u | 0x0196u;  }
        { c.T1 = 0u | 0x01FFu;  }
        { c.A1 = 0u | 0x0008u;  }
        { c.T0 = c.V1 - 0x2Cu;  }
        L801A49C4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V1 = mem.ReadU8(c.A2); 
        if (c.V1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801A49DC;
        }
        { c.A2 = c.A2 + 0x1u;  }
        { c.A3 = c.A3 + 0x4u;  }
        goto L801A4A28;
        L801A49DC: ;
        { c.V0 = c.V1 & 0x000Fu;  }
        { c.V0 = c.V0 << 3;  }
        mem.WriteU8((c.A0 + 0xCu), (byte)c.V0); 
        { c.V0 = c.V1 & 0x00F0u;  }
        { c.V0 = c.V0 >> 1;  }
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
        { c.A3 = c.A3 + 0x8u;  }
        { c.V0 = c.V0 + 0x6u;  }
        mem.WriteU16((c.A0 + 0xAu), (ushort)c.V0); 
        c.A0 = mem.ReadU32(c.A0); 
        L801A4A28: ;
        if (c.A0 != 0u) {
            goto L801A49C4;
        }
        L801A4A34: ;
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool func_801A4A4C(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo5", m, true)) return true;
        func_801A4A4C_Body(c, m);
        return false;
    }
    private static void func_801A4A4C_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo5", 0x801B553Cu);
        c.V0 = 0x00100000u; 
        { var _s = c.A0; var _t = c.V0; c.A0 = _s + _t;  }
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x557Cu), c.A0); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x557Au), (ushort)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x5578u), (ushort)c.V0); 
        return;
        }
    public static bool BO5_CutsceneRun(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo5", m, true)) return true;
        BO5_CutsceneRun_Body(c, m);
        return false;
    }
    private static void BO5_CutsceneRun_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo5", 0x801B553Cu);
        { c.SP = c.SP - 0x28u;  }
        c.A0 = 0x801B0000u; 
        { c.A0 = c.A0 + 0x557Au;  }
        mem.WriteU32((c.SP + 0x20u), c.RA); 
        mem.WriteU32((c.SP + 0x1Cu), c.S3); 
        mem.WriteU32((c.SP + 0x18u), c.S2); 
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.V0 = mem.ReadU16(c.A0); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16(c.A0, (ushort)c.V0); 
        c.V1 = mem.ReadU16(c.A0); 
        { c.V0 = 0u | 0xFFFEu;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s < _t ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.S1 = c.A0 + 0x2u;  }
            goto L801A4AE8;
        }
        { c.S1 = c.A0 + 0x2u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x5578u), (ushort)0u); 
        goto L801A4D1C;
        L801A4ACC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU32(c.S1, c.A1); 
        goto L801A4D1C;
        L801A4AD4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU16(c.S3); 
        mem.WriteU32(c.S1, c.A1); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU16(c.S3, (ushort)c.V0); 
        goto L801A4D1C;
        L801A4AE8: ;
        c.S3 = c.A0; 
        { c.S2 = 0u | 0x0001u;  }
        L801A4AF0: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A1 = mem.ReadU32(c.S1); 
        c.V1 = mem.ReadU16((c.S1 - 0x2u)); 
        { c.V0 = c.A1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.A0 = mem.ReadU8(c.A1); 
        { c.V0 = c.A1 + 0x2u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8((c.A1 + 0x1u)); 
        { c.A0 = c.A0 << 8;  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s | _t;  }
        { var _s = c.V1; var _t = c.V0; c.V1 = _s < _t ? 1u : 0u;  }
        if (c.V1 != 0u) {
            { c.V0 = c.A1 + 0x3u;  }
            goto L801A4ACC;
        }
        { c.V0 = c.A1 + 0x3u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V1 = mem.ReadU8((c.A1 + 0x2u)); 
        if (c.V1 == c.S2) {
            { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
            goto L801A4C68;
        }
        { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A4B50;
        }
        if (c.V1 == 0u) {
            { c.V0 = c.A1 + 0x4u;  }
            goto L801A4B6C;
        }
        { c.V0 = c.A1 + 0x4u;  }
        goto L801A4AF0;
        L801A4B50: ;
        { c.V0 = 0u | 0x0002u;  }
        if (c.V1 == c.V0) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801A4CA4;
        }
        { c.V0 = 0u | 0x0003u;  }
        if (c.V1 == c.V0) {
            { c.V0 = c.A1 + 0x4u;  }
            goto L801A4CF4;
        }
        { c.V0 = c.A1 + 0x4u;  }
        goto L801A4AF0;
        L801A4B6C: ;
        mem.WriteU32(c.S1, c.V0); 
        c.V1 = mem.ReadU8((c.A1 + 0x3u)); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 4;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s - _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80070000u; 
        { c.V1 = c.V1 + 0x62D8u;  }
        { var _s = c.V0; var _t = c.V1; c.S0 = _s + _t;  }
        c.A0 = c.S0; 
        c.RA = 0x801A4BA0u; 
        SoTN_bo5.DestroyEntity_bo5(c, m);
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8(c.V1); 
        mem.WriteU16((c.S0 + 0x26u), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x26u)); 
        { c.V0 = c.V0 << 2;  }
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x398u)); 
        mem.WriteU32((c.S0 + 0x28u), c.V0); 
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8(c.V1); 
        { c.V0 = c.V0 << 4;  }
        mem.WriteU16((c.S0 + 0x2u), (ushort)c.V0); 
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8(c.V1); 
        c.V1 = mem.ReadU16((c.S0 + 0x2u)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        mem.WriteU16((c.S0 + 0x2u), (ushort)c.V0); 
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8(c.V1); 
        { c.V0 = c.V0 << 4;  }
        mem.WriteU16((c.S0 + 0x6u), (ushort)c.V0); 
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8(c.V1); 
        c.V1 = mem.ReadU16((c.S0 + 0x6u)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        mem.WriteU16((c.S0 + 0x6u), (ushort)c.V0); 
        goto L801A4AF0;
        L801A4C68: ;
        { c.V0 = c.A1 + 0x4u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8((c.A1 + 0x3u)); 
        { c.A0 = c.V0 << 1;  }
        { var _s = c.A0; var _t = c.V0; c.A0 = _s + _t;  }
        { c.A0 = c.A0 << 4;  }
        { var _s = c.A0; var _t = c.V0; c.A0 = _s - _t;  }
        { c.A0 = c.A0 << 2;  }
        c.V0 = 0x80070000u; 
        { c.V0 = c.V0 + 0x62D8u;  }
        { var _s = c.A0; var _t = c.V0; c.A0 = _s + _t;  }
        c.RA = 0x801A4C9Cu; 
        SoTN_bo5.DestroyEntity_bo5(c, m);
        goto L801A4AF0;
        L801A4CA4: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x557Cu)); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x55E8u)); 
        c.V0 = mem.ReadU8(c.V0); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _t >> (int)(_s & 31u);  }
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.A1 + 0x4u;  }
            goto L801A4AD4;
        }
        { c.V0 = c.A1 + 0x4u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8((c.A1 + 0x3u)); 
        { var _s = c.V0; var _t = c.S2; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = 0u; var _t = c.V0; c.V0 = ~(_s | _t);  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s & _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x55E8u), c.V0); 
        goto L801A4AF0;
        L801A4CF4: ;
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8((c.A1 + 0x3u)); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x55E8u)); 
        { var _s = c.V0; var _t = c.S2; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x55E8u), c.V0); 
        goto L801A4AF0;
        L801A4D1C: ;
        c.RA = mem.ReadU32((c.SP + 0x20u)); 
        c.S3 = mem.ReadU32((c.SP + 0x1Cu)); 
        c.S2 = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x28u;  }
        return;
        }
    public static bool BO5_ScaleCutsceneAvatar(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo5", m, true)) return true;
        BO5_ScaleCutsceneAvatar_Body(c, m);
        return false;
    }
    private static void BO5_ScaleCutsceneAvatar_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo5", 0x801B553Cu);
        c.V0 = 0x801B0000u; 
        c.V0 = (uint)(short)mem.ReadU16((c.V0 + 0x554Au)); 
        { c.A3 = c.V0 + (uint)(mem.LineCount - DialogueGlyphs.VisibleLines(mem));  }
        { c.V0 = (int)c.A3 < 5 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A4D6C;
        }
        { c.A3 = c.A3 - 0x5u;  }
        L801A4D5C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (int)c.A3 < 5 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.A3 = c.A3 - 0x5u;  }
            goto L801A4D5C;
        }
        { c.A3 = c.A3 - 0x5u;  }
        { c.A3 = c.A3 + 0x5u;  }
        L801A4D6C: ;
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C48u)); 
        c.V0 = mem.ReadU16((c.V0 + 0x2Eu)); 
        if (c.V0 != 0u) {
            c.V1 = 0u; 
            goto L801A4DE8;
        }
        c.V1 = 0u; 
        { c.V0 = c.A3 << 2;  }
        c.At = 0x801B0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A1 = mem.ReadU32((c.At + 0x5554u)); 
        c.V0 = mem.ReadU8((c.A1 + 0x19u)); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s - _t;  }
        mem.WriteU8((c.A1 + 0x19u), (byte)c.V0); 
        c.V0 = mem.ReadU8((c.A1 + 0xDu)); 
        c.V1 = mem.ReadU8((c.A1 + 0x19u)); 
        { var _s = c.A0; var _t = c.V0; c.V0 = _s + _t;  }
        if (c.V1 != 0u) {
            mem.WriteU8((c.A1 + 0xDu), (byte)c.V0); 
            goto L801A4DE4;
        }
        mem.WriteU8((c.A1 + 0xDu), (byte)c.V0); 
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C48u)); 
        c.V1 = mem.ReadU16((c.V0 + 0x2Eu)); 
        { c.V1 = c.V1 + 0x1u;  }
        mem.WriteU16((c.V0 + 0x2Eu), (ushort)c.V1); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        L801A4DE4: ;
        c.V1 = 0u; 
        L801A4DE8: ;
        { c.A0 = c.A0 & 0x00FFu;  }
        c.A2 = 0x801B0000u; 
        { c.A2 = c.A2 + 0x5554u;  }
        L801A4DF4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.V1 == c.A3) {
            goto L801A4E14;
        }
        c.A1 = mem.ReadU32(c.A2); 
        c.V0 = mem.ReadU16((c.A1 + 0xAu)); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s - _t;  }
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0); 
        L801A4E14: ;
        { c.V1 = c.V1 + 0x1u;  }
        { c.V0 = (int)c.V1 < 5 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.A2 = c.A2 + 0x4u;  }
            goto L801A4DF4;
        }
        { c.A2 = c.A2 + 0x4u;  }
        c.V1 = 0x801B0000u; 
        { c.V1 = c.V1 + 0x554Cu;  }
        c.V0 = mem.ReadU16(c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16(c.V1, (ushort)c.V0); 
        return;
        }
    public static bool func_801A4E40(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo5", m, true)) return true;
        func_801A4E40_Body(c, m);
        return false;
    }
    private static void func_801A4E40_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo5", 0x801B553Cu);
        { c.SP = c.SP - 0x48u;  }
        mem.WriteU32((c.SP + 0x38u), c.S2); 
        c.S2 = c.A0; 
        mem.WriteU32((c.SP + 0x44u), c.RA); 
        mem.WriteU32((c.SP + 0x40u), c.S4); 
        mem.WriteU32((c.SP + 0x3Cu), c.S3); 
        mem.WriteU32((c.SP + 0x34u), c.S1); 
        mem.WriteU32((c.SP + 0x30u), c.S0); 
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        if (c.V0 == 0u) {
            goto L801A4F9C;
        }
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x55ECu)); 
        if (c.V0 == 0u) {
            goto L801A4F70;
        }
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x5538u)); 
        if (c.V0 != 0u) {
            goto L801A4F70;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x34FCu)); 
        { c.V0 = c.V0 & 0x0200u;  }
        if (c.V0 != 0u) {
            goto L801A4EC4;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x4220u)); 
        if (c.V0 == 0u) {
            goto L801A4F70;
        }
        L801A4EC4: ;
        c.V1 = 0x80090000u; 
        c.V1 = mem.ReadU16((c.V1 + 0x7494u)); 
        { c.V0 = 0u | 0x0800u;  }
        if (c.V1 != c.V0) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A4F70;
        }
        { c.V0 = 0u | 0x0001u;  }
        c.S0 = 0x80040000u; 
        { c.S0 = c.S0 - 0x384Cu;  }
        c.A0 = mem.ReadU32((c.S2 + 0x64u)); 
        c.V1 = mem.ReadU32(c.S0); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x5538u), c.V0); 
        c.S1 = 0xFFFFFFFFu; 
        c.RA = 0x801A4EF8u;
        Dispatcher.Call(c, m, c.V1);
        c.V0 = mem.ReadU32((c.S2 + 0x34u)); 
        c.V1 = 0x00800000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s ^ _t;  }
        mem.WriteU32((c.S2 + 0x34u), c.V0); 
        c.A0 = 0x801B0000u; 
        c.A0 = mem.ReadU32((c.A0 + 0x5570u)); 
        if (c.A0 == c.S1) {
            goto L801A4F2C;
        }
        c.V0 = mem.ReadU32(c.S0); 
        c.RA = 0x801A4F2Cu;
        Dispatcher.Call(c, m, c.V0);
        L801A4F2C: ;
        c.A0 = 0x801B0000u; 
        c.A0 = mem.ReadU32((c.A0 + 0x556Cu)); 
        if (c.A0 == c.S1) {
            goto L801A4F50;
        }
        c.V0 = mem.ReadU32(c.S0); 
        c.RA = 0x801A4F50u;
        Dispatcher.Call(c, m, c.V0);
        L801A4F50: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3824u)); 
        { c.A0 = 0u | 0x000Au;  }
        c.RA = 0x801A4F64u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.S2 + 0x2Eu), (ushort)0u); 
        L801A4F70: ;
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        if (c.V0 == 0u) {
            goto L801A4F9C;
        }
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x5578u)); 
        if (c.V0 == 0u) {
            goto L801A4F9C;
        }
        c.RA = 0x801A4F9Cu; 
        SoTN_bo5.BO5_CutsceneRun(c, m);
        L801A4F9C: ;
        c.V1 = mem.ReadU16((c.S2 + 0x2Cu)); 
        { c.V0 = c.V1 < 0x00000008u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801A5F04;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x79Cu)); 
        switch (c.V0)
        {
            case 0x801A4FC8u: goto L801A4FC8;
            case 0x801A5080u: goto L801A5080;
            case 0x801A596Cu: goto L801A596C;
            case 0x801A59A0u: goto L801A59A0;
            case 0x801A5A64u: goto L801A5A64;
            case 0x801A5AFCu: goto L801A5AFC;
            case 0x801A5DECu: goto L801A5DEC;
            case 0x801A5EC4u: goto L801A5EC4;
            case 0x801A50F8u: goto L801A50F8;
            case 0x801A5104u: goto L801A5104;
            case 0x801A51B0u: goto L801A51B0;
            case 0x801A51DCu: goto L801A51DC;
            case 0x801A520Cu: goto L801A520C;
            case 0x801A524Cu: goto L801A524C;
            case 0x801A5384u: goto L801A5384;
            case 0x801A53FCu: goto L801A53FC;
            case 0x801A5490u: goto L801A5490;
            case 0x801A54B8u: goto L801A54B8;
            case 0x801A54E0u: goto L801A54E0;
            case 0x801A5518u: goto L801A5518;
            case 0x801A5560u: goto L801A5560;
            case 0x801A5094u: goto L801A5094;
            case 0x801A55BCu: goto L801A55BC;
            case 0x801A5620u: goto L801A5620;
            case 0x801A5680u: goto L801A5680;
            case 0x801A56CCu: goto L801A56CC;
            case 0x801A5700u: goto L801A5700;
            case 0x801A5710u: goto L801A5710;
            case 0x801A57D0u: goto L801A57D0;
            case 0x801A580Cu: goto L801A580C;
            case 0x801A582Cu: goto L801A582C;
            case 0x801A5F04u: goto L801A5F04;
            case 0x801A5868u: goto L801A5868;
            case 0x801A5FD0u: goto L801A5FD0;
            case 0x801A5FE8u: goto L801A5FE8;
            case 0x801A60CCu: goto L801A60CC;
            case 0x801A6124u: goto L801A6124;
            case 0x801A6214u: goto L801A6214;
            case 0x801A6248u: goto L801A6248;
            case 0x801A6324u: goto L801A6324;
            case 0x801A636Cu: goto L801A636C;
            case 0x801A637Cu: goto L801A637C;
            case 0x801A63ACu: goto L801A63AC;
            case 0x801A63E4u: goto L801A63E4;
            case 0x801A641Cu: goto L801A641C;
            case 0x801A6454u: goto L801A6454;
            case 0x801A648Cu: goto L801A648C;
            case 0x801A64C4u: goto L801A64C4;
            case 0x801A64FCu: goto L801A64FC;
            case 0x801A6534u: goto L801A6534;
            case 0x801A656Cu: goto L801A656C;
            case 0x801A65A4u: goto L801A65A4;
            case 0x801A65DCu: goto L801A65DC;
            case 0x801A6608u: goto L801A6608;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801A4FC8: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU8((c.V0 - 0x41B2u)); 
        if (c.V0 != 0u) {
            goto L801A5004;
        }
        c.V0 = 0x80090000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x7914u)); 
        if (c.V0 != 0u) {
            goto L801A5004;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3660u)); 
        if (c.V0 == 0u) {
            goto L801A5014;
        }
        L801A5004: ;
        c.At = 0x80180000u; 
        mem.WriteU32((c.At + 0x6DCu), 0u); 
        goto L801A5B64;
        L801A5014: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x1D5Cu;  }
        c.RA = 0x801A5024u; 
        SoTN_bo5.BO5_SetCutsceneScript(c, m);
        { c.V0 = c.V0 & 0x00FFu;  }
        if (c.V0 == 0u) {
            c.A0 = 0x00800000u; 
            goto L801A5F04;
        }
        c.A0 = 0x00800000u; 
        { c.A0 = c.A0 | 0x2000u;  }
        c.V0 = mem.ReadU32((c.S2 + 0x34u)); 
        { c.V1 = 0u | 0x0001u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x55E8u), 0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x55ECu), 0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x5538u), 0u); 
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x38FCu), c.V1); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s | _t;  }
        mem.WriteU32((c.S2 + 0x34u), c.V0); 
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x5574u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU32((c.S2 + 0x64u), c.V1); 
        goto L801A5F04;
        L801A5080: ;
        c.S4 = 0x801B0000u; 
        { c.S4 = c.S4 + 0x5552u;  }
        { c.S1 = c.S4 - 0x16u;  }
        { c.S0 = c.S4 - 0xCu;  }
        { c.S3 = c.S4 - 0x12u;  }
        L801A5094: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.S4); 
        if (c.V0 == 0u) {
            goto L801A50B8;
        }
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x5538u)); 
        if (c.V0 == 0u) {
            goto L801A5E94;
        }
        L801A50B8: ;
        c.V0 = mem.ReadU32(c.S1); 
        { c.V1 = c.V0 + 0x1u;  }
        mem.WriteU32(c.S1, c.V1); 
        c.A2 = mem.ReadU8(c.V0); 
        { c.V1 = c.A2 & 0xFFFFu;  }
        { c.V0 = c.V1 < 0x00000019u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801A58A8;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x77Cu)); 
        switch (c.V0)
        {
            case 0x801A50F8u: goto L801A50F8;
            case 0x801A5104u: goto L801A5104;
            case 0x801A51B0u: goto L801A51B0;
            case 0x801A51DCu: goto L801A51DC;
            case 0x801A520Cu: goto L801A520C;
            case 0x801A524Cu: goto L801A524C;
            case 0x801A5384u: goto L801A5384;
            case 0x801A53FCu: goto L801A53FC;
            case 0x801A5490u: goto L801A5490;
            case 0x801A54B8u: goto L801A54B8;
            case 0x801A54E0u: goto L801A54E0;
            case 0x801A5518u: goto L801A5518;
            case 0x801A5560u: goto L801A5560;
            case 0x801A5094u: goto L801A5094;
            case 0x801A55BCu: goto L801A55BC;
            case 0x801A5620u: goto L801A5620;
            case 0x801A5680u: goto L801A5680;
            case 0x801A56CCu: goto L801A56CC;
            case 0x801A5700u: goto L801A5700;
            case 0x801A5710u: goto L801A5710;
            case 0x801A57D0u: goto L801A57D0;
            case 0x801A580Cu: goto L801A580C;
            case 0x801A582Cu: goto L801A582C;
            case 0x801A5F04u: goto L801A5F04;
            case 0x801A5868u: goto L801A5868;
            case 0x801A5FD0u: goto L801A5FD0;
            case 0x801A5FE8u: goto L801A5FE8;
            case 0x801A60CCu: goto L801A60CC;
            case 0x801A6124u: goto L801A6124;
            case 0x801A6214u: goto L801A6214;
            case 0x801A6248u: goto L801A6248;
            case 0x801A6324u: goto L801A6324;
            case 0x801A636Cu: goto L801A636C;
            case 0x801A637Cu: goto L801A637C;
            case 0x801A63ACu: goto L801A63AC;
            case 0x801A63E4u: goto L801A63E4;
            case 0x801A641Cu: goto L801A641C;
            case 0x801A6454u: goto L801A6454;
            case 0x801A648Cu: goto L801A648C;
            case 0x801A64C4u: goto L801A64C4;
            case 0x801A64FCu: goto L801A64FC;
            case 0x801A6534u: goto L801A6534;
            case 0x801A656Cu: goto L801A656C;
            case 0x801A65A4u: goto L801A65A4;
            case 0x801A65DCu: goto L801A65DC;
            case 0x801A6608u: goto L801A6608;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801A50F8: ;
        { c.V0 = 0u | 0x0007u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A5F04;
        L801A5104: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x5538u)); 
        if (c.V0 != 0u) {
            goto L801A5094;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x8u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x2u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 != 0u) {
            mem.WriteU16(c.S0, (ushort)c.V1); 
            goto L801A513C;
        }
        mem.WriteU16(c.S0, (ushort)c.V1); 
        c.V0 = mem.ReadU16((c.S0 - 0x4u)); 
        { c.V0 = c.V0 + 0xCu;  }
        mem.WriteU16((c.S0 - 0x4u), (ushort)c.V0); 
        L801A513C: ;
        c.V0 = mem.ReadU16((c.S0 + 0x4u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x4u), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 5 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A5164;
        }
        mem.WriteU16((c.S0 + 0x4u), (ushort)0u); 
        L801A5164: ;
        c.RA = 0x801A516Cu; 
        SoTN_bo5.BO5_CutsceneUnk4(c, m);
        c.V1 = mem.ReadU16((c.S0 + 0x8u)); 
        { c.V0 = c.V1 & 0x0001u;  }
        if (c.V0 != 0u) {
            goto L801A5198;
        }
        c.V0 = (uint)(short)mem.ReadU16((c.S0 + 0x4u)); 
        { c.V0 = (int)c.V0 < 4 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = c.V1 | 0x0001u;  }
            goto L801A5094;
        }
        { c.V0 = c.V1 | 0x0001u;  }
        mem.WriteU16((c.S0 + 0x8u), (ushort)c.V0); 
        L801A5198: ;
        mem.WriteU16((c.S0 + 0x6u), (ushort)0u); 
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        mem.WriteU16((c.S2 + 0x2Eu), (ushort)0u); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A5F04;
        L801A51B0: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x553Cu)); 
        { c.V1 = c.V0 + 0x1u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x553Cu), c.V1); 
        c.V0 = mem.ReadU8(c.V0); 
        c.At = 0x801B0000u; 
        mem.WriteU8((c.At + 0x5553u), (byte)c.V0); 
        goto L801A5094;
        L801A51DC: ;
        c.V1 = mem.ReadU32((c.S4 - 0x16u)); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32((c.S4 - 0x16u), c.V0); 
        c.V0 = mem.ReadU8(c.V1); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x5538u)); 
        if (c.V1 != 0u) {
            mem.WriteU8(c.S4, (byte)c.V0); 
            goto L801A5094;
        }
        mem.WriteU8(c.S4, (byte)c.V0); 
        goto L801A5F04;
        L801A520C: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x5538u)); 
        if (c.V0 != 0u) {
            { c.V1 = 0u | 0x0008u;  }
            goto L801A5094;
        }
        { c.V1 = 0u | 0x0008u;  }
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x5554u)); 
        c.S0 = 0u; 
        { c.S0 = c.S0 + 0x1u;  }
        L801A5230: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (int)c.S0 < 5 ? 1u : 0u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V1); 
        c.S1 = mem.ReadU32(c.S1); 
        if (c.V0 != 0u) {
            { c.S0 = c.S0 + 0x1u;  }
            goto L801A5230;
        }
        { c.S0 = c.S0 + 0x1u;  }
        goto L801A5F04;
        L801A524C: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x5538u)); 
        if (c.V0 != 0u) {
            goto L801A54CC;
        }
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.S0 = mem.ReadU8(c.V1); 
        { c.V0 = c.V1 + 0x2u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.T2 = mem.ReadU8((c.V1 + 0x1u)); 
        c.S1 = mem.ReadU32((c.S1 + 0x2Cu)); 
        { c.V0 = c.T2 & 0x0001u;  }
        { c.V1 = c.S0 << 1;  }
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A0 = mem.ReadU8((c.At + 0xB78u)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A1 = mem.ReadU8((c.At + 0xB7Cu)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V1; c.At = _s + _t;  }
        c.V1 = mem.ReadU16((c.At + 0xB80u)); 
        { c.V0 = 0u | 0x0090u;  }
        mem.WriteU16((c.S1 + 0x1Au), (ushort)c.V0); 
        { c.V0 = c.T2 & 0x0080u;  }
        if (c.V0 == 0u) {
            mem.WriteU16((c.S1 + 0xEu), (ushort)c.V1); 
            goto L801A52D0;
        }
        mem.WriteU16((c.S1 + 0xEu), (ushort)c.V1); 
        c.V1 = c.A0; 
        { c.V0 = c.V1 + 0x2Fu;  }
        goto L801A52D8;
        L801A52D0: ;
        c.V0 = c.A0; 
        { c.V1 = c.V0 + 0x2Fu;  }
        L801A52D8: ;
        mem.WriteU8((c.S1 + 0x24u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0xCu), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x30u), (byte)c.V1); 
        mem.WriteU8((c.S1 + 0x18u), (byte)c.V1); 
        c.V0 = c.A1; 
        { c.V1 = c.V0 + 0x48u;  }
        mem.WriteU8((c.S1 + 0x19u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0xDu), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x31u), (byte)c.V1); 
        mem.WriteU8((c.S1 + 0x25u), (byte)c.V1); 
        c.V0 = mem.ReadU16(c.S3); 
        { c.V0 = c.V0 - 0x1Eu;  }
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x20u), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x14u), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S3 + 0x4u)); 
        { c.V0 = c.V0 + 0x24u;  }
        mem.WriteU16((c.S1 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x22u), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0xAu), (ushort)c.V0); 
        { c.V0 = c.S0 << 1;  }
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU16((c.At + 0xB88u)); 
        mem.WriteU16((c.S3 + 0x10u), (ushort)c.V0); 
        c.RA = 0x801A5350u; 
        SoTN_bo5.BO5_CutsceneUnk1(c, m);
        c.RA = 0x801A5358u; 
        SoTN_bo5.BO5_CutsceneUnk4(c, m);
        { c.A0 = c.S0 & 0xFFFFu;  }
        c.A1 = c.S2; 
        { c.V0 = 0u | 0x01FEu;  }
        mem.WriteU16((c.S1 + 0x26u), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x32u), (ushort)0u); 
        c.RA = 0x801A5370u; 
        SoTN_bo5.BO5_DrawCutsceneActorName(c, m);
        { c.V0 = 0u | 0x0006u;  }
        mem.WriteU16((c.S3 + 0xCu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0003u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A5F04;
        L801A5384: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x5538u)); 
        if (c.V0 != 0u) {
            { c.V1 = 0u | 0x0008u;  }
            goto L801A5094;
        }
        { c.V1 = 0u | 0x0008u;  }
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x5554u)); 
        c.S0 = 0u; 
        { c.S0 = c.S0 + 0x1u;  }
        L801A53A8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (int)c.S0 < 5 ? 1u : 0u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V1); 
        c.S1 = mem.ReadU32(c.S1); 
        if (c.V0 != 0u) {
            { c.S0 = c.S0 + 0x1u;  }
            goto L801A53A8;
        }
        { c.S0 = c.S0 + 0x1u;  }
        c.S0 = 0x801B0000u; 
        { c.S0 = c.S0 + 0x5570u;  }
        c.A0 = mem.ReadU32(c.S0); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x384Cu)); 
        c.RA = 0x801A53DCu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0xFFFFFFFFu; 
        mem.WriteU32(c.S0, c.V0); 
        { c.V0 = 0u | 0x0006u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x554Cu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0004u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A5F04;
        L801A53FC: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x5538u)); 
        if (c.V0 != 0u) {
            goto L801A54CC;
        }
        c.V1 = mem.ReadU32((c.S3 - 0x4u)); 
        c.S1 = mem.ReadU32((c.S3 + 0x28u)); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32((c.S3 - 0x4u), c.V0); 
        c.A0 = mem.ReadU8(c.V1); 
        { c.V0 = c.V1 + 0x2u;  }
        mem.WriteU32((c.S3 - 0x4u), c.V0); 
        mem.WriteU16(c.S3, (ushort)c.A0); 
        c.V0 = mem.ReadU8((c.V1 + 0x1u)); 
        mem.WriteU16((c.S3 + 0x4u), (ushort)c.V0); 
        c.S1 = mem.ReadU32(c.S1); 
        mem.WriteU16((c.S1 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0xAu), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S3 + 0x4u)); 
        { c.V0 = c.V0 + 0x48u;  }
        mem.WriteU16((c.S1 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x22u), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S3 + 0x4u)); 
        c.S1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU16((c.S1 + 0xAu), (ushort)c.V0); 
        { c.V0 = 0u | 0x00F6u;  }
        mem.WriteU8((c.S1 + 0xCu), (byte)c.V0); 
        { c.V0 = 0u | 0x0018u;  }
        mem.WriteU16((c.S3 + 0xCu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0005u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.S2 + 0x2Eu), (ushort)0u); 
        goto L801A5F04;
        L801A5490: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x5538u)); 
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0018u;  }
            goto L801A5094;
        }
        { c.V0 = 0u | 0x0018u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x554Cu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0006u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A5F04;
        L801A54B8: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x5538u)); 
        if (c.V0 == 0u) {
            goto L801A57D0;
        }
        L801A54CC: ;
        c.V0 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 + 0x2u;  }
        mem.WriteU32(c.S1, c.V0); 
        goto L801A5094;
        L801A54E0: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x5538u)); 
        if (c.V0 != 0u) {
            goto L801A5094;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3808u)); 
        c.RA = 0x801A5508u;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 == 0u) {
            goto L801A554C;
        }
        goto L801A5094;
        L801A5518: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x5538u)); 
        if (c.V0 != 0u) {
            goto L801A5094;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3808u)); 
        c.RA = 0x801A5540u;
        Dispatcher.Call(c, m, c.V0);
        { c.V1 = 0u | 0x0001u;  }
        if (c.V0 != c.V1) {
            goto L801A5094;
        }
        L801A554C: ;
        c.V0 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        goto L801A5F04;
        L801A5560: ;
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.T1 = mem.ReadU8(c.V1); 
        { c.V0 = c.V1 + 0x2u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.A1 = mem.ReadU8((c.V1 + 0x1u)); 
        { c.V0 = c.V1 + 0x3u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.A0 = mem.ReadU8((c.V1 + 0x2u)); 
        { c.V0 = c.V1 + 0x4u;  }
        mem.WriteU32(c.S1, c.V0); 
        { c.T1 = c.T1 << 4;  }
        { var _s = c.T1; var _t = c.A1; c.T1 = _s | _t;  }
        { c.T1 = c.T1 << 4;  }
        { var _s = c.T1; var _t = c.A0; c.T1 = _s | _t;  }
        c.A0 = mem.ReadU8((c.V1 + 0x3u)); 
        { c.T1 = c.T1 << 4;  }
        { var _s = c.T1; var _t = c.A0; c.A0 = _s | _t;  }
        c.RA = 0x801A55B4u; 
        SoTN_bo5.func_801A4A4C(c, m);
        goto L801A5094;
        L801A55BC: ;
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.T1 = mem.ReadU8(c.V1); 
        { c.V0 = c.V1 + 0x2u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.A0 = mem.ReadU8((c.V1 + 0x1u)); 
        { c.V0 = c.V1 + 0x3u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8((c.V1 + 0x2u)); 
        { c.A1 = c.V1 + 0x4u;  }
        mem.WriteU32(c.S1, c.A1); 
        { c.T1 = c.T1 << 4;  }
        { var _s = c.T1; var _t = c.A0; c.T1 = _s | _t;  }
        { c.T1 = c.T1 << 4;  }
        { var _s = c.T1; var _t = c.V0; c.T1 = _s | _t;  }
        c.V0 = mem.ReadU8((c.V1 + 0x3u)); 
        { c.T1 = c.T1 << 4;  }
        { var _s = c.T1; var _t = c.V0; c.T1 = _s | _t;  }
        c.V1 = mem.ReadU16(c.T1); 
        { c.V1 = c.V1 << 2;  }
        { var _s = c.V1; var _t = c.A1; c.V1 = _s + _t;  }
        goto L801A5624;
        L801A5620: ;
        c.V1 = mem.ReadU32(c.S1); 
        L801A5624: ;
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.T1 = mem.ReadU8(c.V1); 
        { c.V0 = c.V1 + 0x2u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.A0 = mem.ReadU8((c.V1 + 0x1u)); 
        { c.V0 = c.V1 + 0x3u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8((c.V1 + 0x2u)); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x553Cu)); 
        { c.T1 = c.T1 << 4;  }
        { var _s = c.T1; var _t = c.A0; c.T1 = _s | _t;  }
        { c.T1 = c.T1 << 4;  }
        { var _s = c.T1; var _t = c.V0; c.T1 = _s | _t;  }
        c.V0 = mem.ReadU8(c.V1); 
        { c.T1 = c.T1 << 4;  }
        { var _s = c.T1; var _t = c.V0; c.T1 = _s | _t;  }
        c.V0 = 0x00100000u; 
        { var _s = c.T1; var _t = c.V0; c.V0 = _s + _t;  }
        mem.WriteU32(c.S1, c.V0); 
        goto L801A5094;
        L801A5680: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x553Cu)); 
        c.A0 = 0x801B0000u; 
        c.A0 = mem.ReadU32((c.A0 + 0x55E8u)); 
        c.V1 = mem.ReadU8(c.V0); 
        { var _s = c.V1; var _t = c.A0; c.V0 = _t >> (int)(_s & 31u);  }
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A5EA8;
        }
        { c.V0 = 0u | 0x0001u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = 0u; var _t = c.V0; c.V0 = ~(_s | _t);  }
        c.V1 = mem.ReadU32(c.S1); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s & _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x55E8u), c.V0); 
        { c.V1 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V1); 
        goto L801A5094;
        L801A56CC: ;
        c.V1 = mem.ReadU32(c.S1); 
        c.A0 = 0x801B0000u; 
        c.A0 = mem.ReadU32((c.A0 + 0x55E8u)); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V1 = mem.ReadU8(c.V1); 
        { c.V0 = 0u | 0x0001u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s | _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x55E8u), c.V0); 
        goto L801A5094;
        L801A5700: ;
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x5578u), (ushort)0u); 
        goto L801A5094;
        L801A5710: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x5538u)); 
        if (c.V0 == 0u) {
            { c.A1 = 0u | 0x0001u;  }
            goto L801A5738;
        }
        { c.A1 = 0u | 0x0001u;  }
        c.V0 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 + 0x5u;  }
        mem.WriteU32(c.S1, c.V0); 
        goto L801A5094;
        L801A5738: ;
        c.V1 = mem.ReadU32(c.S1); 
        c.A2 = 0u; 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.T1 = mem.ReadU8(c.V1); 
        { c.V0 = c.V1 + 0x2u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.T0 = mem.ReadU8((c.V1 + 0x1u)); 
        { c.V0 = c.V1 + 0x3u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.A3 = mem.ReadU8((c.V1 + 0x2u)); 
        { c.V0 = c.V1 + 0x4u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.A0 = mem.ReadU8((c.V1 + 0x3u)); 
        { c.V0 = c.V1 + 0x5u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.T2 = mem.ReadU8((c.V1 + 0x4u)); 
        { c.T1 = c.T1 << 4;  }
        { var _s = c.T1; var _t = c.T0; c.T1 = _s | _t;  }
        { c.T1 = c.T1 << 4;  }
        { var _s = c.T1; var _t = c.A3; c.T1 = _s | _t;  }
        { c.T1 = c.T1 << 4;  }
        { var _s = c.T1; var _t = c.A0; c.T1 = _s | _t;  }
        c.A0 = 0x00100000u; 
        { var _s = c.T1; var _t = c.A0; c.A0 = _s + _t;  }
        { c.V0 = c.T2 << 1;  }
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A3 = mem.ReadU16((c.At + 0xB84u)); 
        { c.V0 = 0u | 0x0100u;  }
        mem.WriteU32((c.SP + 0x10u), c.V0); 
        { c.V0 = 0u | 0x0030u;  }
        mem.WriteU32((c.SP + 0x14u), c.V0); 
        { c.V0 = 0u | 0x0048u;  }
        mem.WriteU32((c.SP + 0x18u), c.V0); 
        c.RA = 0x801A57C8u; 
        SoTN_main.LoadTPage(c, m);
        goto L801A5094;
        L801A57D0: ;
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.A2 = mem.ReadU8(c.V1); 
        { c.V0 = c.V1 + 0x2u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.A0 = mem.ReadU8((c.V1 + 0x1u)); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3824u)); 
        { c.A1 = c.A2 << 4;  }
        { var _s = c.A0; var _t = c.A1; c.A0 = _s | _t;  }
        c.RA = 0x801A5804u;
        Dispatcher.Call(c, m, c.V0);
        goto L801A5094;
        L801A580C: ;
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x55E8u), 0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x5538u), 0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x55ECu), 0u); 
        goto L801A5094;
        L801A582C: ;
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V1 = mem.ReadU8(c.V1); 
        { c.V0 = 0u | 0x0001u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _t << (int)(_s & 31u);  }
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x55E8u)); 
        { var _s = 0u; var _t = c.V0; c.V0 = ~(_s | _t);  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s & _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x55E8u), c.V0); 
        goto L801A5094;
        L801A5868: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x553Cu)); 
        c.V1 = mem.ReadU8(c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x55E8u)); 
        { var _s = c.V1; var _t = c.V0; c.V0 = _t >> (int)(_s & 31u);  }
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            goto L801A5EA8;
        }
        c.V0 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        goto L801A5094;
        L801A58A8: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x5538u)); 
        if (c.V0 != 0u) {
            goto L801A5094;
        }
        c.V0 = mem.ReadU8((c.S1 + 0x17u)); 
        mem.WriteU8((c.S1 + 0x16u), (byte)c.V0); 
        { c.V0 = 0u | 0x0020u;  }
        if (c.V1 != c.V0) {
            { c.A0 = c.SP + 0x20u;  }
            goto L801A58F0;
        }
        { c.A0 = c.SP + 0x20u;  }
        c.V1 = 0x801B0000u; 
        { c.V1 = c.V1 + 0x5546u;  }
        c.V0 = mem.ReadU16(c.V1); 
        { c.V0 = c.V0 + 0x2u;  }
        mem.WriteU16(c.V1, (ushort)c.V0); 
        goto L801A5F04;
        L801A58F0: ;
        { c.V0 = c.A2 & 0x000Fu;  }
        { c.V0 = c.V0 << 1;  }
        { c.V0 = c.V0 + 0x380u;  }
        mem.WriteU16((c.SP + 0x20u), (ushort)c.V0); 
        { c.V0 = c.A2 & 0x00F0u;  }
        { c.V0 = c.V0 >> 1;  }
        c.A1 = 0x801B0000u; 
        c.A1 = (uint)(short)mem.ReadU16((c.A1 + 0x5546u)); 
        c.V1 = 0x801B0000u; 
        c.V1 = (uint)(short)mem.ReadU16((c.V1 + 0x554Au)); 
        { c.V0 = c.V0 + 0xF0u;  }
        mem.WriteU16((c.SP + 0x22u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0002u;  }
        mem.WriteU16((c.SP + 0x24u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.SP + 0x26u), (ushort)c.V0); 
        { c.A2 = c.V1 << 1;  }
        { var _s = c.A2; var _t = c.V1; c.A2 = _s + _t;  }
        { c.A2 = c.A2 << 2;  }
        { c.A2 = c.A2 + 0x180u;  }
        { c.A2 = c.A2 << 16;  }
        { c.A2 = (uint)((int)c.A2 >> 16);  }
        c.RA = 0x801A594Cu; 
        if (!DialogueGlyphs.Draw(c, mem)) SoTN_main.MoveImage(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x5546u)); 
        { c.V0 = c.V0 + 0x2u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x5546u), (ushort)c.V0); 
        goto L801A5F04;
        L801A596C: ;
        { c.A0 = 0u | 0x0002u;  }
        c.RA = 0x801A5974u; 
        SoTN_bo5.BO5_ScaleCutsceneAvatar(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = (uint)(short)mem.ReadU16((c.V0 + 0x554Cu)); 
        { c.V0 = (int)c.V0 < 6 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A5F04;
        }
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A5F04;
        L801A59A0: ;
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x5568u)); 
        c.V0 = mem.ReadU16((c.S1 + 0x20u)); 
        c.A0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        c.V1 = mem.ReadU16((c.S1 + 0x16u)); 
        c.A1 = mem.ReadU16((c.S1 + 0x2Eu)); 
        { c.V0 = c.V0 - 0x4u;  }
        { c.A0 = c.A0 + 0x4u;  }
        { c.V1 = c.V1 - 0x6u;  }
        { c.A1 = c.A1 + 0x6u;  }
        mem.WriteU16((c.S1 + 0x20u), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.A0); 
        mem.WriteU16((c.S1 + 0x14u), (ushort)c.A0); 
        mem.WriteU16((c.S1 + 0x16u), (ushort)c.V1); 
        mem.WriteU16((c.S1 + 0xAu), (ushort)c.V1); 
        mem.WriteU16((c.S1 + 0x2Eu), (ushort)c.A1); 
        mem.WriteU16((c.S1 + 0x22u), (ushort)c.A1); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x554Cu)); 
        { c.V0 = c.V0 - 0x1u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x554Cu), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A5F04;
        }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x5570u)); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.S1 = _s + _t;  }
        if (c.S1 == 0u) {
            goto L801A5F04;
        }
        mem.WriteU16((c.S1 + 0x32u), (ushort)0u); 
        L801A5A4C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.S1 = mem.ReadU32(c.S1); 
        if (c.S1 == 0u) {
            goto L801A5F04;
        }
        mem.WriteU16((c.S1 + 0x32u), (ushort)0u); 
        goto L801A5A4C;
        L801A5A64: ;
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x5568u)); 
        c.V0 = mem.ReadU16((c.S1 + 0x20u)); 
        c.A0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        c.V1 = mem.ReadU16((c.S1 + 0x16u)); 
        c.A1 = mem.ReadU16((c.S1 + 0x2Eu)); 
        { c.V0 = c.V0 + 0x4u;  }
        { c.A0 = c.A0 - 0x4u;  }
        { c.V1 = c.V1 + 0x6u;  }
        { c.A1 = c.A1 - 0x6u;  }
        mem.WriteU16((c.S1 + 0x20u), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.A0); 
        mem.WriteU16((c.S1 + 0x14u), (ushort)c.A0); 
        mem.WriteU16((c.S1 + 0x16u), (ushort)c.V1); 
        mem.WriteU16((c.S1 + 0xAu), (ushort)c.V1); 
        mem.WriteU16((c.S1 + 0x2Eu), (ushort)c.A1); 
        mem.WriteU16((c.S1 + 0x22u), (ushort)c.A1); 
        c.V1 = 0x801B0000u; 
        c.V1 = (uint)(short)mem.ReadU16((c.V1 + 0x5540u)); 
        c.V0 = (uint)(short)mem.ReadU16((c.S1 + 0x14u)); 
        c.A0 = c.V1; 
        { c.V1 = c.V1 - 0x2u;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = (int)_s < (int)_t ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A5ADC;
        }
        { c.V0 = c.A0 - 0x3u;  }
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x14u), (ushort)c.V0); 
        L801A5ADC: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x554Cu)); 
        { c.V0 = c.V0 - 0x1u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x554Cu), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        goto L801A5DDC;
        L801A5AFC: ;
        c.V1 = mem.ReadU16((c.S2 + 0x2Eu)); 
        { c.A2 = 0u | 0x0001u;  }
        if (c.V1 == c.A2) {
            { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
            goto L801A5C28;
        }
        { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A5B24;
        }
        if (c.V1 == 0u) {
            { c.A0 = 0u | 0x0002u;  }
            goto L801A5B38;
        }
        { c.A0 = 0u | 0x0002u;  }
        goto L801A5F04;
        L801A5B24: ;
        { c.V0 = 0u | 0x0002u;  }
        if (c.V1 == c.V0) {
            goto L801A5D24;
        }
        goto L801A5F04;
        L801A5B38: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x55ECu), c.A2); 
        { c.A1 = 0u | 0x0048u;  }
        c.RA = 0x801A5B50u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.V1 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.V1 != c.V0) {
            { c.V0 = c.V1 << 1;  }
            goto L801A5B74;
        }
        { c.V0 = c.V1 << 1;  }
        L801A5B64: ;
        c.A0 = c.S2; 
        c.RA = 0x801A5B6Cu; 
        SoTN_bo5.DestroyEntity_bo5(c, m);
        goto L801A5F04;
        L801A5B74: ;
        c.A2 = 0x801B0000u; 
        { c.A2 = c.A2 + 0x556Cu;  }
        mem.WriteU32(c.A2, c.V1); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.S1 = _s + _t;  }
        if (c.S1 == 0u) {
            c.A0 = 0u; 
            goto L801A5D10;
        }
        c.A0 = 0u; 
        { c.A1 = 0u | 0x007Fu;  }
        { c.V1 = 0u | 0x00F7u;  }
        { c.T1 = c.A2 - 0x28u;  }
        { c.T0 = 0u | 0x01FEu;  }
        c.A3 = 0x80180000u; 
        { c.A3 = c.A3 + 0xB8Cu;  }
        { c.A2 = 0u | 0x0F70u;  }
        L801A5BC0: ;
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
        { var _s = c.A0; var _t = c.V0; c.V0 = _s + _t;  }
        mem.WriteU16((c.S1 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0xAu), (ushort)c.V0); 
        { c.V0 = c.A0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 15);  }
        { var _s = c.V0; var _t = c.A3; c.V0 = _s + _t;  }
        c.V0 = mem.ReadU16(c.V0); 
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.A2); 
        mem.WriteU16((c.S1 + 0x20u), (ushort)c.V0); 
        c.S1 = mem.ReadU32(c.S1); 
        if (c.S1 != 0u) {
            { c.A0 = c.A0 + 0x1u;  }
            goto L801A5BC0;
        }
        { c.A0 = c.A0 + 0x1u;  }
        goto L801A5D10;
        L801A5C28: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x556Cu)); 
        { c.V1 = c.V0 << 1;  }
        { var _s = c.V1; var _t = c.V0; c.V1 = _s + _t;  }
        { c.V1 = c.V1 << 2;  }
        { var _s = c.V1; var _t = c.V0; c.V1 = _s + _t;  }
        { c.V1 = c.V1 << 2;  }
        c.V0 = 0x80080000u; 
        { c.V0 = c.V0 + 0x6FECu;  }
        { var _s = c.V1; var _t = c.V0; c.S1 = _s + _t;  }
        if (c.S1 == 0u) {
            c.A0 = 0u; 
            goto L801A5CBC;
        }
        c.A0 = 0u; 
        { c.A1 = 0u | 0x0004u;  }
        L801A5C60: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        c.V1 = mem.ReadU16((c.S1 + 0x20u)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s - _t;  }
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S1 + 0x20u)); 
        c.V1 = (uint)(short)mem.ReadU16((c.S1 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x2u;  }
        if ((int)c.V1 >= 0) {
            mem.WriteU16((c.S1 + 0x20u), (ushort)c.V0); 
            goto L801A5C8C;
        }
        mem.WriteU16((c.S1 + 0x20u), (ushort)c.V0); 
        { c.V1 = c.V1 + 0xFu;  }
        L801A5C8C: ;
        { c.V0 = (uint)((int)c.V1 >> 4);  }
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.V0); 
        { c.V0 = (int)c.V0 < 5 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A5CA8;
        }
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.A1); 
        goto L801A5CAC;
        L801A5CA8: ;
        { c.A0 = 0u | 0x0001u;  }
        L801A5CAC: ;
        c.S1 = mem.ReadU32(c.S1); 
        if (c.S1 != 0u) {
            goto L801A5C60;
        }
        L801A5CBC: ;
        { c.V0 = c.A0 << 16;  }
        if (c.V0 != 0u) {
            goto L801A5F04;
        }
        c.S0 = 0x801B0000u; 
        { c.S0 = c.S0 + 0x556Cu;  }
        c.A0 = mem.ReadU32(c.S0); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x384Cu)); 
        c.RA = 0x801A5CE8u;
        Dispatcher.Call(c, m, c.V0);
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x5568u)); 
        c.V0 = 0xFFFFFFFFu; 
        mem.WriteU32(c.S0, c.V0); 
        c.S1 = mem.ReadU32(c.S1); 
        { c.V0 = 0u | 0x0011u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0); 
        c.S1 = mem.ReadU32(c.S1); 
        { c.V0 = 0u | 0x0051u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0); 
        L801A5D10: ;
        c.V0 = mem.ReadU16((c.S2 + 0x2Eu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S2 + 0x2Eu), (ushort)c.V0); 
        goto L801A5F04;
        L801A5D24: ;
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x5568u)); 
        c.S1 = mem.ReadU32(c.S1); 
        c.V0 = mem.ReadU8((c.S1 + 0x4u)); 
        { c.V0 = c.V0 < 0x00000010u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            c.A0 = 0u; 
            goto L801A5D60;
        }
        c.A0 = 0u; 
        mem.WriteU8((c.S1 + 0x28u), (byte)0u); 
        mem.WriteU8((c.S1 + 0x1Cu), (byte)0u); 
        mem.WriteU8((c.S1 + 0x10u), (byte)0u); 
        mem.WriteU8((c.S1 + 0x4u), (byte)0u); 
        goto L801A5D7C;
        L801A5D60: ;
        c.V0 = mem.ReadU8((c.S1 + 0x28u)); 
        { c.A0 = 0u | 0x0001u;  }
        { c.V0 = c.V0 - 0x10u;  }
        mem.WriteU8((c.S1 + 0x28u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x1Cu), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x10u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x4u), (byte)c.V0); 
        L801A5D7C: ;
        c.V0 = mem.ReadU8((c.S1 + 0x6u)); 
        { c.V0 = c.V0 < 0x0000007Fu ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x007Fu;  }
            goto L801A5D9C;
        }
        { c.V0 = 0u | 0x007Fu;  }
        c.V0 = mem.ReadU8((c.S1 + 0x12u)); 
        { c.A0 = 0u | 0x0001u;  }
        { c.V0 = c.V0 + 0x8u;  }
        L801A5D9C: ;
        mem.WriteU8((c.S1 + 0x12u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x6u), (byte)c.V0); 
        c.S1 = mem.ReadU32(c.S1); 
        c.V0 = mem.ReadU8((c.S1 + 0x4u)); 
        { c.V0 = c.V0 < 0x00000010u ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0010u;  }
            goto L801A5DCC;
        }
        { c.V0 = 0u | 0x0010u;  }
        c.V0 = mem.ReadU8((c.S1 + 0x6u)); 
        { c.A0 = 0u | 0x0001u;  }
        { c.V0 = c.V0 - 0xFu;  }
        L801A5DCC: ;
        mem.WriteU8((c.S1 + 0x6u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x5u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x4u), (byte)c.V0); 
        { c.V0 = c.A0 << 16;  }
        L801A5DDC: ;
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A5F04;
        }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A5F04;
        L801A5DEC: ;
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x5568u)); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x554Cu)); 
        c.S1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 - 0x1u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x554Cu), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        { c.V1 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V1 < 12 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A5E58;
        }
        c.S1 = mem.ReadU32(c.S1); 
        c.V1 = mem.ReadU8((c.S1 + 0xCu)); 
        { c.V0 = c.V1 + 0xECu;  }
        mem.WriteU8((c.S1 + 0xCu), (byte)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x554Cu)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 - 0x15u;  }
            goto L801A5F04;
        }
        { c.V0 = c.V1 - 0x15u;  }
        mem.WriteU8((c.S1 + 0xCu), (byte)c.V0); 
        goto L801A5F04;
        L801A5E58: ;
        if (c.V1 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A5E70;
        }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0); 
        goto L801A5E84;
        L801A5E70: ;
        c.V0 = mem.ReadU16((c.S1 + 0x2Eu)); 
        { c.V0 = c.V0 - 0x6u;  }
        mem.WriteU16((c.S1 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x22u), (ushort)c.V0); 
        L801A5E84: ;
        c.S1 = mem.ReadU32(c.S1); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0); 
        goto L801A5F04;
        L801A5E94: ;
        c.V0 = mem.ReadU8(c.S4); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU8(c.S4, (byte)c.V0); 
        goto L801A5F04;
        L801A5EA8: ;
        c.V1 = 0x801B0000u; 
        { c.V1 = c.V1 + 0x553Cu;  }
        c.V0 = mem.ReadU32(c.V1); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU32(c.V1, c.V0); 
        goto L801A5F04;
        L801A5EC4: ;
        c.A0 = c.S2; 
        c.RA = 0x801A5ECCu; 
        SoTN_bo5.DestroyEntity_bo5(c, m);
        c.A0 = 0x80040000u; 
        { c.A0 = c.A0 - 0x34FCu;  }
        c.V1 = mem.ReadU32(c.A0); 
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x80040000u; 
        mem.WriteU8((c.At - 0x41B2u), (byte)c.V0); 
        c.At = 0x80040000u; 
        mem.WriteU8((c.At - 0x418Fu), (byte)c.V0); 
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x38FCu), 0u); 
        { c.V0 = c.V1 | 0x0200u;  }
        { c.V1 = c.V1 | 0x0300u;  }
        mem.WriteU32(c.A0, c.V0); 
        mem.WriteU32(c.A0, c.V1); 
        L801A5F04: ;
        c.RA = mem.ReadU32((c.SP + 0x44u)); 
        c.S4 = mem.ReadU32((c.SP + 0x40u)); 
        c.S3 = mem.ReadU32((c.SP + 0x3Cu)); 
        c.S2 = mem.ReadU32((c.SP + 0x38u)); 
        c.S1 = mem.ReadU32((c.SP + 0x34u)); 
        c.S0 = mem.ReadU32((c.SP + 0x30u)); 
        { c.SP = c.SP + 0x48u;  }
        return;
        { c.V0 = 0u | 0x0180u;  }
        c.A1 = 0x80090000u; 
        { c.A1 = c.A1 + 0x740Cu;  }
        c.V1 = mem.ReadU16(c.A1); 
        { var _s = c.V0; var _t = c.A0; c.A0 = _s - _t;  }
        { var _s = c.A0; var _t = c.V1; c.V1 = _s - _t;  }
        { c.V1 = c.V1 << 16;  }
        { c.V1 = (uint)((int)c.V1 >> 16);  }
        { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A5F60;
        }
        c.V0 = mem.ReadU32(c.A1); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801A5F80;
        L801A5F60: ;
        { c.V0 = (int)c.V1 < -1 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A5F78;
        }
        c.V0 = mem.ReadU32(c.A1); 
        { c.V0 = c.V0 - 0x1u;  }
        goto L801A5F80;
        L801A5F78: ;
        { c.V0 = c.A0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        L801A5F80: ;
        mem.WriteU32(c.A1, c.V0); 
        return;
        { c.SP = c.SP - 0x18u;  }
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.S0 = c.A0; 
        c.A1 = 0x80070000u; 
        { c.A1 = c.A1 + 0x33D8u;  }
        mem.WriteU32((c.SP + 0x14u), c.RA); 
        c.V1 = mem.ReadU16((c.S0 + 0x2Cu)); 
        c.A3 = 0x80070000u; 
        { c.A3 = c.A3 + 0x3084u;  }
        { c.V0 = c.V1 < 0x00000006u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801A62A0;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x718u)); 
        switch (c.V0)
        {
            case 0x801A5FD0u: goto L801A5FD0;
            case 0x801A5FE8u: goto L801A5FE8;
            case 0x801A60CCu: goto L801A60CC;
            case 0x801A6124u: goto L801A6124;
            case 0x801A6214u: goto L801A6214;
            case 0x801A6248u: goto L801A6248;
            case 0x801A6324u: goto L801A6324;
            case 0x801A636Cu: goto L801A636C;
            case 0x801A637Cu: goto L801A637C;
            case 0x801A63ACu: goto L801A63AC;
            case 0x801A63E4u: goto L801A63E4;
            case 0x801A641Cu: goto L801A641C;
            case 0x801A6454u: goto L801A6454;
            case 0x801A648Cu: goto L801A648C;
            case 0x801A64C4u: goto L801A64C4;
            case 0x801A64FCu: goto L801A64FC;
            case 0x801A6534u: goto L801A6534;
            case 0x801A656Cu: goto L801A656C;
            case 0x801A65A4u: goto L801A65A4;
            case 0x801A65DCu: goto L801A65DC;
            case 0x801A6608u: goto L801A6608;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801A5FD0: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x478u;  }
        c.RA = 0x801A5FE0u; 
        SoTN_bo5.BO5_InitializeEntity(c, m);
        goto L801A62A0;
        L801A5FE8: ;
        c.V1 = mem.ReadU16((c.S0 + 0x2Eu)); 
        { c.V0 = 0u | 0x0001u;  }
        if (c.V1 == c.V0) {
            { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
            goto L801A6074;
        }
        { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A6010;
        }
        if (c.V1 == 0u) {
            goto L801A6024;
        }
        goto L801A62A0;
        L801A6010: ;
        { c.V0 = 0u | 0x0002u;  }
        if (c.V1 == c.V0) {
            goto L801A60BC;
        }
        goto L801A62A0;
        L801A6024: ;
        c.A1 = 0x80080000u; 
        { c.A1 = c.A1 - 0x56A8u;  }
        { c.V1 = c.A1 + 0x1780u;  }
        { var _s = c.A1; var _t = c.V1; c.V0 = _s < _t ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.A0 = 0u | 0x0003u;  }
            goto L801A62A0;
        }
        { c.A0 = 0u | 0x0003u;  }
        L801A603C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU16((c.A1 + 0x26u)); 
        if (c.V0 != c.A0) {
            goto L801A605C;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x2Eu)); 
        mem.WriteU32((c.S0 + 0x7Cu), c.A1); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x2Eu), (ushort)c.V0); 
        L801A605C: ;
        { c.A1 = c.A1 + 0xBCu;  }
        { var _s = c.A1; var _t = c.V1; c.V0 = _s < _t ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A62A0;
        }
        goto L801A603C;
        L801A6074: ;
        c.A1 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.V1 = mem.ReadU16((c.A1 + 0x26u)); 
        { c.V0 = 0u | 0x0003u;  }
        if (c.V1 == c.V0) {
            goto L801A62A0;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x2Eu)); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x55E8u)); 
        c.At = 0x80180000u; 
        mem.WriteU32((c.At + 0x6DCu), 0u); 
        { c.V0 = c.V0 + 0x1u;  }
        { c.V1 = c.V1 | 0x8000u;  }
        mem.WriteU16((c.S0 + 0x2Eu), (ushort)c.V0); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x55E8u), c.V1); 
        goto L801A62A0;
        L801A60BC: ;
        { c.A0 = 0u | 0x0002u;  }
        c.RA = 0x801A60C4u; 
        SoTN_bo5.func_801AAC0C(c, m);
        goto L801A62A0;
        L801A60CC: ;
        { c.A1 = 0u | 0x0001u;  }
        c.A0 = 0x80070000u; 
        { c.A0 = c.A0 + 0x2EF4u;  }
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x2F2Cu)); 
        { c.V1 = 0u | 0x2000u;  }
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x3748u), 0u); 
        c.At = 0x80090000u; 
        mem.WriteU32((c.At + 0x7400u), c.A1); 
        { c.V0 = c.V0 & 0x0004u;  }
        if (c.V0 == 0u) {
            mem.WriteU32(c.A0, c.V1); 
            goto L801A6108;
        }
        mem.WriteU32(c.A0, c.V1); 
        { c.V0 = 0u | 0x0002u;  }
        mem.WriteU32(c.A0, c.V0); 
        L801A6108: ;
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EFCu), c.A1); 
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        goto L801A62A0;
        L801A6124: ;
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x2F2Cu)); 
        c.V1 = 0x80070000u; 
        { c.V1 = c.V1 + 0x2EF4u;  }
        { c.V0 = c.V0 & 0x0004u;  }
        if (c.V0 == 0u) {
            mem.WriteU32(c.V1, 0u); 
            goto L801A6160;
        }
        mem.WriteU32(c.V1, 0u); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3668u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0002u;  }
            goto L801A620C;
        }
        { c.V0 = 0u | 0x0002u;  }
        mem.WriteU32(c.V1, c.V0); 
        goto L801A620C;
        L801A6160: ;
        { c.V0 = 0u | 0x2000u;  }
        mem.WriteU32(c.V1, c.V0); 
        c.V0 = mem.ReadU16((c.A1 + 0x2u)); 
        c.V1 = mem.ReadU16((c.A3 + 0xAu)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        c.A0 = c.V0; 
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 289 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = c.A0 << 16;  }
            goto L801A61B8;
        }
        { c.V0 = c.A0 << 16;  }
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x55E8u)); 
        { c.V0 = c.V1 & 0x0001u;  }
        if (c.V0 != 0u) {
            { c.V0 = c.A0 << 16;  }
            goto L801A61B8;
        }
        { c.V0 = c.A0 << 16;  }
        { c.V0 = c.V1 | 0x0001u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x55E8u), c.V0); 
        { c.V0 = c.A0 << 16;  }
        L801A61B8: ;
        { c.A2 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.A2 < 321 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = (int)c.A2 < 256 ? 1u : 0u;  }
            goto L801A61F4;
        }
        { c.V0 = (int)c.A2 < 256 ? 1u : 0u;  }
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EF4u), 0u); 
        c.V0 = mem.ReadU16((c.A3 + 0xAu)); 
        { c.V1 = 0u | 0x0140u;  }
        { var _s = c.V1; var _t = c.V0; c.V1 = _s - _t;  }
        mem.WriteU16((c.A1 + 0x2u), (ushort)c.V1); 
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        { c.V0 = (int)c.A2 < 256 ? 1u : 0u;  }
        L801A61F4: ;
        if (c.V0 == 0u) {
            goto L801A6200;
        }
        { c.A0 = 0u | 0x0100u;  }
        L801A6200: ;
        { c.A0 = c.A0 << 16;  }
        { c.A0 = (uint)((int)c.A0 >> 16);  }
        c.RA = 0x801A620Cu; 
        SoTN_bo5.func_801A5F28(c, m);
        L801A620C: ;
        { c.V0 = 0u | 0x0001u;  }
        goto L801A6298;
        L801A6214: ;
        { c.A0 = 0u | 0x0140u;  }
        c.RA = 0x801A621Cu; 
        SoTN_bo5.func_801A5F28(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x55E8u)); 
        { c.V0 = c.V0 & 0x0002u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A6290;
        }
        { c.V0 = 0u | 0x0001u;  }
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        goto L801A628C;
        L801A6248: ;
        { c.A0 = 0u | 0x0100u;  }
        c.RA = 0x801A6250u; 
        SoTN_bo5.func_801A5F28(c, m);
        c.V1 = 0x80090000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x740Cu)); 
        { c.V0 = 0u | 0x0080u;  }
        if (c.V1 != c.V0) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A6290;
        }
        { c.V0 = 0u | 0x0001u;  }
        c.V1 = 0x80090000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x7400u)); 
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x3748u), c.V0); 
        if (c.V1 == 0u) {
            goto L801A6284;
        }
        c.At = 0x80090000u; 
        mem.WriteU32((c.At + 0x7400u), 0u); 
        L801A6284: ;
        c.A0 = c.S0; 
        c.RA = 0x801A628Cu; 
        SoTN_bo5.DestroyEntity_bo5(c, m);
        L801A628C: ;
        { c.V0 = 0u | 0x0001u;  }
        L801A6290: ;
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EF4u), 0u); 
        L801A6298: ;
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EFCu), c.V0); 
        L801A62A0: ;
        c.RA = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x18u;  }
        return;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x5538u)); 
        { c.SP = c.SP - 0x20u;  }
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.S0 = c.A0; 
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        c.S1 = 0x80070000u; 
        { c.S1 = c.S1 + 0x3084u;  }
        if (c.V0 == 0u) {
            mem.WriteU32((c.SP + 0x18u), c.RA); 
            goto L801A62F8;
        }
        mem.WriteU32((c.SP + 0x18u), c.RA); 
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 < 0x0000000Du ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A62F8;
        }
        { c.A0 = 0u | 0x000Du;  }
        c.RA = 0x801A62F8u; 
        SoTN_bo5.func_801AAC0C(c, m);
        L801A62F8: ;
        c.V1 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V1 < 0x0000000Fu ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801A6654;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x700u)); 
        switch (c.V0)
        {
            case 0x801A6324u: goto L801A6324;
            case 0x801A636Cu: goto L801A636C;
            case 0x801A637Cu: goto L801A637C;
            case 0x801A63ACu: goto L801A63AC;
            case 0x801A63E4u: goto L801A63E4;
            case 0x801A641Cu: goto L801A641C;
            case 0x801A6454u: goto L801A6454;
            case 0x801A648Cu: goto L801A648C;
            case 0x801A64C4u: goto L801A64C4;
            case 0x801A64FCu: goto L801A64FC;
            case 0x801A6534u: goto L801A6534;
            case 0x801A656Cu: goto L801A656C;
            case 0x801A65A4u: goto L801A65A4;
            case 0x801A65DCu: goto L801A65DC;
            case 0x801A6608u: goto L801A6608;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801A6324: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x460u;  }
        c.RA = 0x801A6334u; 
        SoTN_bo5.BO5_InitializeEntity(c, m);
        c.V0 = 0xFFFF8004u; 
        mem.WriteU16((c.S0 + 0x54u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S0 + 0x56u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0050u;  }
        mem.WriteU16((c.S0 + 0x5Au), (ushort)c.V0); 
        { c.V0 = 0u | 0x0220u;  }
        mem.WriteU16((c.S0 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x3Cu), (ushort)0u); 
        c.V1 = mem.ReadU16((c.S1 + 0xAu)); 
        { c.V0 = 0u | 0x01C0u;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s - _t;  }
        mem.WriteU16((c.S0 + 0x2u), (ushort)c.V0); 
        goto L801A6654;
        L801A636C: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x55E8u)); 
        { c.V0 = c.V0 & 0x0004u;  }
        goto L801A6390;
        L801A637C: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x55E8u)); 
        { c.V1 = 0u | 0x0002u;  }
        mem.WriteU16((c.S0 + 0x56u), (ushort)c.V1); 
        { c.V0 = c.V0 & 0x0008u;  }
        L801A6390: ;
        if (c.V0 == 0u) {
            goto L801A6654;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        goto L801A6654;
        L801A63AC: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xC24u;  }
        c.A1 = c.S0; 
        c.RA = 0x801A63BCu; 
        SoTN_bo5.BO5_AnimateEntity(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x55E8u)); 
        { c.V0 = c.V0 & 0x0010u;  }
        if (c.V0 == 0u) {
            goto L801A6654;
        }
        { c.A0 = 0u | 0x0004u;  }
        c.RA = 0x801A63DCu; 
        SoTN_bo5.func_801AAC0C(c, m);
        goto L801A6654;
        L801A63E4: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xC48u;  }
        c.A1 = c.S0; 
        c.RA = 0x801A63F4u; 
        SoTN_bo5.BO5_AnimateEntity(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x55E8u)); 
        { c.V0 = c.V0 & 0x0020u;  }
        if (c.V0 == 0u) {
            goto L801A6654;
        }
        { c.A0 = 0u | 0x0005u;  }
        c.RA = 0x801A6414u; 
        SoTN_bo5.func_801AAC0C(c, m);
        goto L801A6654;
        L801A641C: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xC5Cu;  }
        c.A1 = c.S0; 
        c.RA = 0x801A642Cu; 
        SoTN_bo5.BO5_AnimateEntity(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x55E8u)); 
        { c.V0 = c.V0 & 0x0040u;  }
        if (c.V0 == 0u) {
            goto L801A6654;
        }
        { c.A0 = 0u | 0x0006u;  }
        c.RA = 0x801A644Cu; 
        SoTN_bo5.func_801AAC0C(c, m);
        goto L801A6654;
        L801A6454: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xC6Cu;  }
        c.A1 = c.S0; 
        c.RA = 0x801A6464u; 
        SoTN_bo5.BO5_AnimateEntity(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x55E8u)); 
        { c.V0 = c.V0 & 0x0080u;  }
        if (c.V0 == 0u) {
            goto L801A6654;
        }
        { c.A0 = 0u | 0x0007u;  }
        c.RA = 0x801A6484u; 
        SoTN_bo5.func_801AAC0C(c, m);
        goto L801A6654;
        L801A648C: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xC74u;  }
        c.A1 = c.S0; 
        c.RA = 0x801A649Cu; 
        SoTN_bo5.BO5_AnimateEntity(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x55E8u)); 
        { c.V0 = c.V0 & 0x0100u;  }
        if (c.V0 == 0u) {
            goto L801A6654;
        }
        { c.A0 = 0u | 0x0008u;  }
        c.RA = 0x801A64BCu; 
        SoTN_bo5.func_801AAC0C(c, m);
        goto L801A6654;
        L801A64C4: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xC84u;  }
        c.A1 = c.S0; 
        c.RA = 0x801A64D4u; 
        SoTN_bo5.BO5_AnimateEntity(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x55E8u)); 
        { c.V0 = c.V0 & 0x0200u;  }
        if (c.V0 == 0u) {
            goto L801A6654;
        }
        { c.A0 = 0u | 0x0009u;  }
        c.RA = 0x801A64F4u; 
        SoTN_bo5.func_801AAC0C(c, m);
        goto L801A6654;
        L801A64FC: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xC90u;  }
        c.A1 = c.S0; 
        c.RA = 0x801A650Cu; 
        SoTN_bo5.BO5_AnimateEntity(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x55E8u)); 
        { c.V0 = c.V0 & 0x0400u;  }
        if (c.V0 == 0u) {
            goto L801A6654;
        }
        { c.A0 = 0u | 0x000Au;  }
        c.RA = 0x801A652Cu; 
        SoTN_bo5.func_801AAC0C(c, m);
        goto L801A6654;
        L801A6534: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xC98u;  }
        c.A1 = c.S0; 
        c.RA = 0x801A6544u; 
        SoTN_bo5.BO5_AnimateEntity(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x55E8u)); 
        { c.V0 = c.V0 & 0x0800u;  }
        if (c.V0 == 0u) {
            goto L801A6654;
        }
        { c.A0 = 0u | 0x000Bu;  }
        c.RA = 0x801A6564u; 
        SoTN_bo5.func_801AAC0C(c, m);
        goto L801A6654;
        L801A656C: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xCA8u;  }
        c.A1 = c.S0; 
        c.RA = 0x801A657Cu; 
        SoTN_bo5.BO5_AnimateEntity(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x55E8u)); 
        { c.V0 = c.V0 & 0x1000u;  }
        if (c.V0 == 0u) {
            goto L801A6654;
        }
        { c.A0 = 0u | 0x000Cu;  }
        c.RA = 0x801A659Cu; 
        SoTN_bo5.func_801AAC0C(c, m);
        goto L801A6654;
        L801A65A4: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xCC0u;  }
        c.A1 = c.S0; 
        c.RA = 0x801A65B4u; 
        SoTN_bo5.BO5_AnimateEntity(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x55E8u)); 
        { c.V0 = c.V0 & 0x2000u;  }
        if (c.V0 == 0u) {
            goto L801A6654;
        }
        { c.A0 = 0u | 0x000Du;  }
        c.RA = 0x801A65D4u; 
        SoTN_bo5.func_801AAC0C(c, m);
        goto L801A6654;
        L801A65DC: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xCCCu;  }
        c.A1 = c.S0; 
        c.RA = 0x801A65ECu; 
        SoTN_bo5.BO5_AnimateEntity(c, m);
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0xC000u;  }
            goto L801A6654;
        }
        { c.V0 = 0u | 0xC000u;  }
        mem.WriteU32((c.S0 + 0x8u), c.V0); 
        { c.A0 = 0u | 0x000Eu;  }
        c.RA = 0x801A6600u; 
        SoTN_bo5.func_801AAC0C(c, m);
        goto L801A6654;
        L801A6608: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xCD4u;  }
        c.A1 = c.S0; 
        c.RA = 0x801A6618u; 
        SoTN_bo5.BO5_AnimateEntity(c, m);
        c.RA = 0x801A6620u; 
        SoTN_bo5.func_801AA380(c, m);
        c.V0 = (uint)(short)mem.ReadU16((c.S0 + 0x2u)); 
        { c.V0 = (int)c.V0 < 273 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A6654;
        }
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x55E8u)); 
        { c.V0 = c.V0 | 0x0002u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x55E8u), c.V0); 
        c.A0 = c.S0; 
        c.RA = 0x801A6654u; 
        SoTN_bo5.DestroyEntity_bo5(c, m);
        L801A6654: ;
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool BO5_EntityRelicOrb(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo5", m, false)) return true;
        BO5_EntityRelicOrb_Body(c, m);
        return false;
    }
    private static void BO5_EntityRelicOrb_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo5", 0x801B553Cu);
        { c.SP = c.SP - 0x50u;  }
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
            { c.S4 = c.V0 & 0x7FFFu;  }
            goto L801ACCF4;
        }
        { c.S4 = c.V0 & 0x7FFFu;  }
        { c.V0 = c.V1 < 0x00000005u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 < 0x0000000Au ? 1u : 0u;  }
            goto L801ACCFC;
        }
        { c.V0 = c.V1 < 0x0000000Au ? 1u : 0u;  }
        c.V0 = mem.ReadU8((c.S1 + 0x48u)); 
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0005u;  }
            goto L801ACCF4;
        }
        { c.V0 = 0u | 0x0005u;  }
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        c.V1 = mem.ReadU16((c.S1 + 0x2Cu)); 
        L801ACCF4: ;
        { c.V0 = c.V1 < 0x0000000Au ? 1u : 0u;  }
        L801ACCFC: ;
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801AD55C;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x5E8u)); 
        switch (c.V0)
        {
            case 0x801ACD1Cu: goto L801ACD1C;
            case 0x801ACEECu: goto L801ACEEC;
            case 0x801AD55Cu: goto L801AD55C;
            case 0x801ACF4Cu: goto L801ACF4C;
            case 0x801AD10Cu: goto L801AD10C;
            case 0x801AD1D4u: goto L801AD1D4;
            case 0x801AD284u: goto L801AD284;
            case 0x801AD3FCu: goto L801AD3FC;
            case 0x801AD8CCu: goto L801AD8CC;
            case 0x801AD9B0u: goto L801AD9B0;
            case 0x801ADB8Cu: goto L801ADB8C;
            case 0x801ADC64u: goto L801ADC64;
            case 0x801ADD68u: goto L801ADD68;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801ACD1C: ;
        { c.V0 = c.S4 & 0xFFFFu;  }
        c.At = 0x80090000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU8((c.At + 0x7964u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 != 0u) {
            goto L801AD45C;
        }
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x460u;  }
        c.S0 = 0u; 
        c.RA = 0x801ACD4Cu; 
        SoTN_bo5.BO5_InitializeEntity(c, m);
        c.S5 = 0x801B0000u; 
        { c.S5 = c.S5 + 0x55F0u;  }
        { c.V0 = c.S0 << 16;  }
        L801ACD58: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (uint)((int)c.V0 >> 15);  }
        { var _s = c.V0; var _t = c.S5; c.V0 = _s + _t;  }
        c.V0 = mem.ReadU16(c.V0); 
        if (c.V0 == 0u) {
            { c.V0 = c.S0 + 0x1u;  }
            goto L801ACD88;
        }
        { c.V0 = c.S0 + 0x1u;  }
        c.S0 = c.V0; 
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 31 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = c.S0 << 16;  }
            goto L801ACD58;
        }
        { c.V0 = c.S0 << 16;  }
        L801ACD88: ;
        { c.V0 = c.S0 << 16;  }
        { c.S3 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.S3 < 31 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.A0 = 0u | 0x0004u;  }
            goto L801ACDC4;
        }
        { c.A0 = 0u | 0x0004u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0007u;  }
        c.RA = 0x801ACDB0u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.S2 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.S2 != c.V0) {
            c.V1 = 0x00800000u; 
            goto L801ACDCC;
        }
        c.V1 = 0x00800000u; 
        L801ACDC4: ;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)0u); 
        goto L801AD740;
        L801ACDCC: ;
        c.V0 = mem.ReadU32((c.S1 + 0x34u)); 
        mem.WriteU32((c.S1 + 0x64u), c.S2); 
        mem.WriteU16((c.S1 + 0x8Cu), (ushort)c.S0); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        mem.WriteU32((c.S1 + 0x34u), c.V0); 
        { c.V0 = c.S3 << 1;  }
        { var _s = c.V0; var _t = c.S5; c.V0 = _s + _t;  }
        { c.V1 = 0u | 0x0010u;  }
        mem.WriteU16(c.V0, (ushort)c.V1); 
        c.V1 = 0x80040000u; 
        c.V1 = mem.ReadU32((c.V1 - 0x37B0u)); 
        { c.V0 = c.S4 << 4;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        c.A0 = mem.ReadU16((c.V0 + 0x8u)); 
        c.A1 = mem.ReadU16((c.V0 + 0xAu)); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x37D4u)); 
        c.A2 = c.S3; 
        c.RA = 0x801ACE1Cu;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.S2 << 1;  }
        { var _s = c.V0; var _t = c.S2; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.S2; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A2 = _s + _t;  }
        if (c.A2 == 0u) {
            c.A3 = 0u; 
            goto L801ACEC8;
        }
        c.A3 = 0u; 
        { c.T3 = 0u | 0x0008u;  }
        { c.T2 = 0u | 0x001Au;  }
        { c.T1 = c.S0 + 0x1D0u;  }
        c.V1 = c.S0; 
        { c.V0 = c.V1 & 0x0007u;  }
        { c.V0 = c.V0 << 4;  }
        { c.T0 = c.V0 | 0x0001u;  }
        { c.A1 = c.V0 | 0x000Fu;  }
        { c.V1 = c.V1 & 0x0018u;  }
        { c.V1 = c.V1 << 1;  }
        { c.A0 = c.V1 | 0x0081u;  }
        { c.V1 = c.V1 | 0x008Fu;  }
        L801ACE74: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 == 0u) {
            { c.V0 = 0u | 0x0006u;  }
            goto L801ACE84;
        }
        { c.V0 = 0u | 0x0006u;  }
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.T3); 
        goto L801ACEB0;
        L801ACE84: ;
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
        L801ACEB0: ;
        { c.V0 = 0u | 0x007Eu;  }
        mem.WriteU16((c.A2 + 0x26u), (ushort)c.V0); 
        c.A2 = mem.ReadU32(c.A2); 
        if (c.A2 != 0u) {
            { c.A3 = c.A3 + 0x1u;  }
            goto L801ACE74;
        }
        { c.A3 = c.A3 + 0x1u;  }
        L801ACEC8: ;
        c.V0 = 0xFFFF8000u; 
        mem.WriteU16((c.S1 + 0x4u), (ushort)c.V0); 
        { c.V0 = 0u | 0x4000u;  }
        mem.WriteU32((c.S1 + 0xCu), c.V0); 
        { c.V0 = 0u | 0x0040u;  }
        mem.WriteU16((c.S1 + 0x90u), (ushort)c.V0); 
        c.V0 = 0xFFFFFE00u; 
        mem.WriteU32((c.S1 + 0x94u), c.V0); 
        goto L801AD55C;
        L801ACEEC: ;
        c.V0 = mem.ReadU32((c.S1 + 0xCu)); 
        c.V1 = mem.ReadU16((c.S1 + 0x90u)); 
        c.A0 = mem.ReadU32((c.S1 + 0x94u)); 
        { c.V1 = c.V1 - 0x1u;  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s + _t;  }
        mem.WriteU16((c.S1 + 0x90u), (ushort)c.V1); 
        { c.V1 = c.V1 & 0xFFFFu;  }
        if (c.V1 != 0u) {
            mem.WriteU32((c.S1 + 0xCu), c.V0); 
            goto L801ACF24;
        }
        mem.WriteU32((c.S1 + 0xCu), c.V0); 
        c.V0 = mem.ReadU32((c.S1 + 0x94u)); 
        { c.V1 = 0u | 0x0040u;  }
        mem.WriteU16((c.S1 + 0x90u), (ushort)c.V1); 
        { var _s = 0u; var _t = c.V0; c.V0 = _s - _t;  }
        mem.WriteU32((c.S1 + 0x94u), c.V0); 
        L801ACF24: ;
        c.RA = 0x801ACF2Cu; 
        SoTN_bo5.func_801AA380(c, m);
        c.V0 = (uint)(short)mem.ReadU16((c.S1 + 0x8Cu)); 
        { c.V1 = 0u | 0x0010u;  }
        { c.V0 = c.V0 << 1;  }
        c.At = 0x801B0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        mem.WriteU16((c.At + 0x55F0u), (ushort)c.V1); 
        goto L801AD55C;
        L801ACF4C: ;
        { c.S0 = c.S4 & 0xFFFFu;  }
        c.A0 = c.S0; 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x37B8u)); 
        { c.A1 = 0u | 0x2000u;  }
        c.RA = 0x801ACF68u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.S0 < 0x00000016u ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.A0 = c.SP + 0x20u;  }
            goto L801ACFA0;
        }
        { c.A0 = c.SP + 0x20u;  }
        { c.V0 = c.S0 < 0x00000014u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            c.A1 = 0u; 
            goto L801ACFA4;
        }
        c.A1 = 0u; 
        c.At = 0x80090000u; 
        { var _s = c.At; var _t = c.S0; c.At = _s + _t;  }
        c.V0 = mem.ReadU8((c.At + 0x7964u)); 
        { c.V0 = c.V0 ^ 0x0002u;  }
        c.At = 0x80090000u; 
        { var _s = c.At; var _t = c.S0; c.At = _s + _t;  }
        mem.WriteU8((c.At + 0x7964u), (byte)c.V0); 
        L801ACFA0: ;
        c.A1 = 0u; 
        L801ACFA4: ;
        c.A2 = 0u; 
        c.A3 = 0u; 
        c.V0 = mem.ReadU32((c.S1 + 0x34u)); 
        c.V1 = 0x00010000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        mem.WriteU32((c.S1 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0100u;  }
        mem.WriteU16((c.SP + 0x22u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0040u;  }
        mem.WriteU16((c.SP + 0x24u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0010u;  }
        mem.WriteU16((c.SP + 0x20u), (ushort)0u); 
        mem.WriteU16((c.SP + 0x26u), (ushort)c.V0); 
        c.RA = 0x801ACFDCu; 
        DialogueGlyphs.Clear(c, mem);
        c.A3 = 0u; 
        { c.T0 = 0u | 0x0010u;  }
        { c.A0 = 0u | 0x0080u;  }
        c.V1 = mem.ReadU32((c.S1 + 0x64u)); 
        { c.A1 = 0u | 0x00A7u;  }
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A2 = _s + _t;  }
        L801AD010: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 != 0u) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801AD05C;
        }
        { c.V0 = 0u | 0x0003u;  }
        { c.V0 = 0u | 0x0006u;  }
        mem.WriteU8((c.A2 + 0x7u), (byte)c.V0); 
        { c.V0 = 0u | 0x00A0u;  }
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V0); 
        { c.V0 = 0u | 0x00F0u;  }
        mem.WriteU8((c.A2 + 0x18u), (byte)c.V0); 
        { c.V0 = 0u | 0x01A1u;  }
        mem.WriteU16((c.A2 + 0xEu), (ushort)c.V0); 
        { c.V0 = 0u | 0x01FEu;  }
        mem.WriteU16((c.A2 + 0x26u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.A2 + 0x1Au), (ushort)c.T0); 
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.T0); 
        mem.WriteU8((c.A2 + 0xCu), (byte)0u); 
        mem.WriteU8((c.A2 + 0xDu), (byte)0u); 
        mem.WriteU8((c.A2 + 0x19u), (byte)c.T0); 
        goto L801AD0E8;
        L801AD05C: ;
        mem.WriteU8((c.A2 + 0x7u), (byte)c.V0); 
        { c.V0 = 0u | 0x0001u;  }
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
            goto L801AD0CC;
        }
        mem.WriteU8((c.A2 + 0x6u), (byte)0u); 
        mem.WriteU8((c.A2 + 0x2Au), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x1Eu), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x12u), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x6u), (byte)c.A0); 
        goto L801AD0DC;
        L801AD0CC: ;
        mem.WriteU8((c.A2 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x1Du), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x11u), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x5u), (byte)c.A0); 
        L801AD0DC: ;
        { c.V0 = 0u | 0x01FDu;  }
        mem.WriteU16((c.A2 + 0x26u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0011u;  }
        L801AD0E8: ;
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.V0); 
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 + 0x1u;  }
        { c.V0 = (int)c.A3 < 3 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801AD010;
        }
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801AD3F4;
        L801AD10C: ;
        c.S0 = 0u; 
        c.S3 = 0x80080000u; 
        { c.S3 = c.S3 - 0x101Cu;  }
        c.V0 = c.S3; 
        c.A0 = 0x80180000u; 
        c.A0 = mem.ReadU32((c.A0 + 0x1654u)); 
        { c.A3 = 0u | 0x0BFFu;  }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        L801AD12C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU8(c.V0, (byte)0u); 
        { c.A3 = c.A3 - 0x1u;  }
        if ((int)c.A3 >= 0) {
            { c.V0 = c.V0 + 0x1u;  }
            goto L801AD12C;
        }
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        { c.S2 = c.S4 << 4;  }
        L801AD144: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.A0); 
        if (c.V0 != 0u) {
            { c.A1 = c.SP + 0x28u;  }
            goto L801AD178;
        }
        { c.A1 = c.SP + 0x28u;  }
        if (c.S0 != 0u) {
            { c.S0 = 0u | 0x0001u;  }
            goto L801AD18C;
        }
        { c.S0 = 0u | 0x0001u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x37B0u)); 
        { var _s = c.S2; var _t = c.V0; c.V0 = _s + _t;  }
        c.A0 = mem.ReadU32(c.V0); 
        goto L801AD144;
        L801AD178: ;
        c.A2 = c.S3; 
        { c.A3 = 0u | 0x00C0u;  }
        c.RA = 0x801AD184u; 
        SoTN_bo5.BO5_BlitChar(c, m);
        c.A0 = c.V0; 
        goto L801AD144;
        L801AD18C: ;
        c.A0 = c.S3; 
        c.A1 = 0u; 
        c.A2 = 0u; 
        c.A3 = 0u; 
        { c.V0 = 0u | 0x0100u;  }
        mem.WriteU32((c.SP + 0x10u), c.V0); 
        { c.V0 = 0u | 0x0180u;  }
        mem.WriteU32((c.SP + 0x14u), c.V0); 
        { c.V0 = 0u | 0x0010u;  }
        mem.WriteU32((c.SP + 0x18u), c.V0); 
        c.RA = 0x801AD1B8u; 
        SoTN_main.LoadTPage(c, m);
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)0u); 
        c.V1 = mem.ReadU16((c.SP + 0x28u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x7Eu), (ushort)c.V1); 
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        goto L801AD55C;
        L801AD1D4: ;
        c.V1 = mem.ReadU32((c.S1 + 0x64u)); 
        c.A3 = 0u; 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.At = 0x80080000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A2 = mem.ReadU32((c.At + 0x6FECu)); 
        L801AD1FC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 != 0u) {
            goto L801AD230;
        }
        c.A0 = mem.ReadU16((c.A2 + 0x16u)); 
        c.A1 = mem.ReadU16((c.A2 + 0x2Eu)); 
        c.V0 = mem.ReadU16((c.A2 + 0x20u)); 
        c.V1 = mem.ReadU16((c.A2 + 0x2Cu)); 
        { c.A0 = c.A0 - 0x4u;  }
        { c.A1 = c.A1 + 0x2u;  }
        { c.V0 = c.V0 - 0x3u;  }
        { c.V1 = c.V1 + 0x3u;  }
        mem.WriteU16((c.A2 + 0x20u), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0x2Cu), (ushort)c.V1); 
        goto L801AD258;
        L801AD230: ;
        c.A0 = mem.ReadU16((c.A2 + 0x16u)); 
        c.A1 = mem.ReadU16((c.A2 + 0x2Eu)); 
        c.V0 = mem.ReadU16((c.A2 + 0x8u)); 
        c.V1 = mem.ReadU16((c.A2 + 0x14u)); 
        { c.A0 = c.A0 - 0x2u;  }
        { c.A1 = c.A1 + 0x4u;  }
        { c.V0 = c.V0 - 0x3u;  }
        { c.V1 = c.V1 + 0x3u;  }
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0x14u), (ushort)c.V1); 
        L801AD258: ;
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.A0); 
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.A0); 
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.A1); 
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.A1); 
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 + 0x1u;  }
        { c.V0 = (int)c.A3 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801AD3CC;
        }
        goto L801AD1FC;
        L801AD284: ;
        c.A3 = 0u; 
        c.A2 = 0x92490000u; 
        c.A1 = mem.ReadU16((c.S1 + 0x7Cu)); 
        { c.A2 = c.A2 | 0x2493u;  }
        { c.V0 = c.A1 + 0x1u;  }
        { c.V1 = c.V0 << 1;  }
        { var _s = c.V1; var _t = c.V0; c.V1 = _s + _t;  }
        { c.V1 = c.V1 << 2;  }
        { c.A0 = c.A1 << 4;  }
        { var _s = c.A0; var _t = c.A1; c.A0 = _s - _t;  }
        { c.A0 = c.A0 << 3;  }
        { var _s = c.A0; var _t = c.A2; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        { c.V0 = 0u | 0x0080u;  }
        { var _s = c.V0; var _t = c.V1; c.T1 = _s - _t;  }
        { c.T0 = c.V1 + 0x80u;  }
        c.V1 = mem.ReadU32((c.S1 + 0x64u)); 
        { c.A1 = c.A1 << 1;  }
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.At = 0x80080000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A2 = mem.ReadU32((c.At + 0x6FECu)); 
        c.V0 = c.HI; 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s + _t;  }
        { c.V0 = (uint)((int)c.V0 >> 2);  }
        { c.A0 = (uint)((int)c.A0 >> 31);  }
        { var _s = c.V0; var _t = c.A0; c.A0 = _s - _t;  }
        L801AD2FC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 != 0u) {
            { c.V0 = c.A0 + 0x68u;  }
            goto L801AD35C;
        }
        { c.V0 = c.A0 + 0x68u;  }
        mem.WriteU16((c.A2 + 0x20u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0098u;  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s - _t;  }
        mem.WriteU16((c.A2 + 0x14u), (ushort)c.T1); 
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.T0); 
        mem.WriteU16((c.A2 + 0x2Cu), (ushort)c.V0); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.A1; c.At = _s + _t;  }
        c.V0 = mem.ReadU16((c.At + 0x1668u)); 
        { c.V0 = c.V0 + 0xA7u;  }
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V0); 
        c.V0 = mem.ReadU8((c.A2 + 0x2Au)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.A1; c.At = _s + _t;  }
        c.V1 = mem.ReadU16((c.At + 0x1658u)); 
        { c.V0 = c.V0 - 0x10u;  }
        { c.V1 = c.V1 + 0xA7u;  }
        mem.WriteU8((c.A2 + 0x2Au), (byte)c.V0); 
        mem.WriteU8((c.A2 + 0x1Eu), (byte)c.V0); 
        goto L801AD3B0;
        L801AD35C: ;
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0098u;  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s - _t;  }
        mem.WriteU16((c.A2 + 0x14u), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0x2Cu), (ushort)c.T1); 
        mem.WriteU16((c.A2 + 0x20u), (ushort)c.T0); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.A1; c.At = _s + _t;  }
        c.V0 = mem.ReadU16((c.At + 0x1678u)); 
        { c.V0 = c.V0 + 0xA7u;  }
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V0); 
        c.V0 = mem.ReadU8((c.A2 + 0x11u)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.A1; c.At = _s + _t;  }
        c.V1 = mem.ReadU16((c.At + 0x1688u)); 
        { c.V0 = c.V0 - 0x10u;  }
        { c.V1 = c.V1 + 0xA7u;  }
        mem.WriteU8((c.A2 + 0x11u), (byte)c.V0); 
        mem.WriteU8((c.A2 + 0x5u), (byte)c.V0); 
        L801AD3B0: ;
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.V1); 
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.V1); 
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 + 0x1u;  }
        { c.V0 = (int)c.A3 < 3 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801AD2FC;
        }
        L801AD3CC: ;
        c.V0 = mem.ReadU16((c.S1 + 0x7Cu)); 
        { c.V1 = 0u | 0x0008u;  }
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)c.V0); 
        { c.V0 = c.V0 & 0xFFFFu;  }
        if (c.V0 != c.V1) {
            goto L801AD55C;
        }
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)0u); 
        { c.V0 = c.V0 + 0x1u;  }
        L801AD3F4: ;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        goto L801AD55C;
        L801AD3FC: ;
        c.V1 = mem.ReadU32((c.S1 + 0x64u)); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A2 = _s + _t;  }
        c.V1 = mem.ReadU16((c.S1 + 0x7Eu)); 
        { c.V0 = 0u | 0x0080u;  }
        mem.WriteU16((c.A2 + 0x32u), (ushort)0u); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s - _t;  }
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S1 + 0x7Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S1 + 0x7Cu)); 
        { c.V0 = c.V0 < 0x00000061u ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801AD55C;
        }
        L801AD45C: ;
        c.A0 = c.S1; 
        c.RA = 0x801AD464u; 
        SoTN_bo5.DestroyEntity_bo5(c, m);
        goto L801AD740;
        L801AD46C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = 0u | 0x001Au;  }
        mem.WriteU16((c.A2 + 0x1Au), (ushort)c.V0); 
        { c.V0 = 0u | 0x01B1u;  }
        mem.WriteU16((c.A2 + 0xEu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0010u;  }
        mem.WriteU8((c.A2 + 0x30u), (byte)c.V0); 
        mem.WriteU8((c.A2 + 0x18u), (byte)c.V0); 
        { c.V0 = 0u | 0x0050u;  }
        mem.WriteU8((c.A2 + 0x19u), (byte)c.V0); 
        mem.WriteU8((c.A2 + 0xDu), (byte)c.V0); 
        { c.V0 = 0u | 0x0060u;  }
        mem.WriteU8((c.A2 + 0x24u), (byte)0u); 
        mem.WriteU8((c.A2 + 0xCu), (byte)0u); 
        mem.WriteU8((c.A2 + 0x31u), (byte)c.V0); 
        mem.WriteU8((c.A2 + 0x25u), (byte)c.V0); 
        c.V1 = mem.ReadU16((c.S1 + 0x82u)); 
        c.S0 = mem.ReadU16((c.S1 + 0x2u)); 
        { c.V1 = c.V1 & 0x0007u;  }
        { c.V1 = c.V1 << 1;  }
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V1; c.At = _s + _t;  }
        c.V0 = mem.ReadU16((c.At + 0x1698u)); 
        { var _s = c.S0; var _t = c.V0; c.V0 = _s + _t;  }
        { c.A0 = c.V0 - 0x6u;  }
        { c.V0 = c.V0 + 0x6u;  }
        mem.WriteU16((c.A2 + 0x20u), (ushort)c.A0); 
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.A0); 
        mem.WriteU16((c.A2 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0x14u), (ushort)c.V0); 
        c.S0 = mem.ReadU16((c.S1 + 0x6u)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V1; c.At = _s + _t;  }
        c.V1 = mem.ReadU16((c.At + 0x16A8u)); 
        { c.V0 = 0u | 0x0080u;  }
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
        { var _s = c.S0; var _t = c.V1; c.V1 = _s + _t;  }
        { c.A0 = c.V1 - 0x6u;  }
        { c.V1 = c.V1 + 0x6u;  }
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.A0); 
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.A0); 
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.V1); 
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.V1); 
        mem.WriteU8((c.A2 + 0x12u), (byte)c.V0); 
        mem.WriteU8((c.A2 + 0x6u), (byte)c.V0); 
        { c.V0 = 0u | 0x007Fu;  }
        mem.WriteU16((c.A2 + 0x26u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0037u;  }
        mem.WriteU8((c.A2 + 0x13u), (byte)0u); 
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.V0); 
        goto L801AD5EC;
        L801AD55C: ;
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        { c.V0 = c.V0 < 0x00000002u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801AD614;
        }
        c.A1 = 0x80040000u; 
        c.A1 = mem.ReadU16((c.A1 - 0x3668u)); 
        c.A0 = c.S1; 
        c.RA = 0x801AD580u; 
        SoTN_bo5.BO5_BlinkItem(c, m);
        c.V0 = mem.ReadU32((c.S1 + 0x64u)); 
        { c.A3 = 0u | 0x0002u;  }
        { c.V1 = c.V0 << 1;  }
        { var _s = c.V1; var _t = c.V0; c.V1 = _s + _t;  }
        { c.V1 = c.V1 << 2;  }
        { var _s = c.V1; var _t = c.V0; c.V1 = _s + _t;  }
        { c.V1 = c.V1 << 2;  }
        c.V0 = 0x80080000u; 
        { c.V0 = c.V0 + 0x6FECu;  }
        { var _s = c.V1; var _t = c.V0; c.A2 = _s + _t;  }
        L801AD5A8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 - 0x1u;  }
        if ((int)c.A3 >= 0) {
            goto L801AD5A8;
        }
        c.V0 = mem.ReadU16((c.S1 + 0x80u)); 
        if (c.V0 != 0u) {
            c.A3 = 0u; 
            goto L801AD604;
        }
        c.A3 = 0u; 
        { c.V1 = 0u | 0x0008u;  }
        L801AD5CC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU16((c.A2 + 0x32u)); 
        if (c.V0 == c.V1) {
            { c.A3 = c.A3 + 0x1u;  }
            goto L801AD46C;
        }
        { c.A3 = c.A3 + 0x1u;  }
        c.A2 = mem.ReadU32(c.A2); 
        { c.V0 = (int)c.A3 < 4 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801AD5CC;
        }
        L801AD5EC: ;
        c.V0 = mem.ReadU16((c.S1 + 0x82u)); 
        { c.V1 = 0u | 0x0004u;  }
        mem.WriteU16((c.S1 + 0x80u), (ushort)c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x82u), (ushort)c.V0); 
        goto L801AD614;
        L801AD604: ;
        c.V0 = mem.ReadU16((c.S1 + 0x80u)); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU16((c.S1 + 0x80u), (ushort)c.V0); 
        L801AD614: ;
        c.V0 = mem.ReadU32((c.S1 + 0x64u)); 
        { c.A3 = 0u | 0x0002u;  }
        { c.V1 = c.V0 << 1;  }
        { var _s = c.V1; var _t = c.V0; c.V1 = _s + _t;  }
        { c.V1 = c.V1 << 2;  }
        { var _s = c.V1; var _t = c.V0; c.V1 = _s + _t;  }
        { c.V1 = c.V1 << 2;  }
        c.V0 = 0x80080000u; 
        { c.V0 = c.V0 + 0x6FECu;  }
        { var _s = c.V1; var _t = c.V0; c.A2 = _s + _t;  }
        L801AD63C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 - 0x1u;  }
        if ((int)c.A3 >= 0) {
            goto L801AD63C;
        }
        if (c.A2 == 0u) {
            { c.A3 = 0u | 0x0008u;  }
            goto L801AD740;
        }
        { c.A3 = 0u | 0x0008u;  }
        L801AD654: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU16((c.A2 + 0x32u)); 
        if (c.V0 == c.A3) {
            goto L801AD730;
        }
        c.V0 = mem.ReadU8((c.A2 + 0x13u)); 
        { c.V0 = c.V0 & 0x0003u;  }
        if (c.V0 == 0u) {
            goto L801AD69C;
        }
        c.V1 = mem.ReadU16((c.A2 + 0x16u)); 
        c.A0 = mem.ReadU16((c.A2 + 0x2Eu)); 
        { c.V0 = c.V1 - 0x1u;  }
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.V0); 
        { c.V0 = c.A0 - 0x1u;  }
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V1); 
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.A0); 
        goto L801AD6CC;
        L801AD69C: ;
        c.V0 = mem.ReadU16((c.A2 + 0x2Eu)); 
        c.V1 = mem.ReadU16((c.A2 + 0x20u)); 
        c.A0 = mem.ReadU16((c.A2 + 0x2Cu)); 
        { c.V0 = c.V0 - 0x2u;  }
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.V0); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU16((c.A2 + 0x20u), (ushort)c.V0); 
        { c.V0 = c.A0 - 0x1u;  }
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.V1); 
        mem.WriteU16((c.A2 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0x14u), (ushort)c.A0); 
        L801AD6CC: ;
        c.V1 = mem.ReadU8((c.A2 + 0x28u)); 
        c.A0 = mem.ReadU8((c.A2 + 0x29u)); 
        c.V0 = mem.ReadU8((c.A2 + 0x13u)); 
        { c.V1 = c.V1 - 0x6u;  }
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU8((c.A2 + 0x13u), (byte)c.V0); 
        c.V0 = mem.ReadU8((c.A2 + 0x2Au)); 
        c.A1 = mem.ReadU8((c.A2 + 0x13u)); 
        { c.A0 = c.A0 - 0x6u;  }
        mem.WriteU8((c.A2 + 0x28u), (byte)c.V1); 
        mem.WriteU8((c.A2 + 0x1Cu), (byte)c.V1); 
        mem.WriteU8((c.A2 + 0x10u), (byte)c.V1); 
        mem.WriteU8((c.A2 + 0x4u), (byte)c.V1); 
        mem.WriteU8((c.A2 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x1Du), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x11u), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x5u), (byte)c.A0); 
        { c.V0 = c.V0 - 0x6u;  }
        { c.A1 = c.A1 < 0x00000011u ? 1u : 0u;  }
        mem.WriteU8((c.A2 + 0x2Au), (byte)c.V0); 
        mem.WriteU8((c.A2 + 0x1Eu), (byte)c.V0); 
        mem.WriteU8((c.A2 + 0x12u), (byte)c.V0); 
        if (c.A1 != 0u) {
            mem.WriteU8((c.A2 + 0x6u), (byte)c.V0); 
            goto L801AD730;
        }
        mem.WriteU8((c.A2 + 0x6u), (byte)c.V0); 
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.A3); 
        L801AD730: ;
        c.A2 = mem.ReadU32(c.A2); 
        if (c.A2 != 0u) {
            goto L801AD654;
        }
        L801AD740: ;
        c.RA = mem.ReadU32((c.SP + 0x48u)); 
        c.S5 = mem.ReadU32((c.SP + 0x44u)); 
        c.S4 = mem.ReadU32((c.SP + 0x40u)); 
        c.S3 = mem.ReadU32((c.SP + 0x3Cu)); 
        c.S2 = mem.ReadU32((c.SP + 0x38u)); 
        c.S1 = mem.ReadU32((c.SP + 0x34u)); 
        c.S0 = mem.ReadU32((c.SP + 0x30u)); 
        { c.SP = c.SP + 0x50u;  }
        return;
        { c.SP = c.SP - 0x18u;  }
        c.A2 = c.A0; 
        mem.WriteU32((c.SP + 0x10u), c.RA); 
        c.V0 = mem.ReadU16((c.A2 + 0x2Cu)); 
        if (c.V0 != 0u) {
            { c.V0 = c.V0 < 0x00000005u ? 1u : 0u;  }
            goto L801AD818;
        }
        { c.V0 = c.V0 < 0x00000005u ? 1u : 0u;  }
        c.V0 = mem.ReadU16((c.A2 + 0x30u)); 
        c.V1 = c.V0; 
        { c.A0 = c.V1 & 0xFFFFu;  }
        { c.V0 = c.A0 >> 3;  }
        mem.WriteU16((c.A2 + 0xB4u), (ushort)c.V1); 
        c.At = 0x80040000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU8((c.At - 0x4114u)); 
        { c.V1 = c.V1 & 0x0007u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = (uint)((int)_t >> (int)(_s & 31u));  }
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.A0 << 1;  }
            goto L801AD7CC;
        }
        { c.V0 = c.A0 << 1;  }
        c.A0 = c.A2; 
        c.RA = 0x801AD7C4u; 
        SoTN_bo5.DestroyEntity_bo5(c, m);
        goto L801AD874;
        L801AD7CC: ;
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V1 = mem.ReadU16((c.At + 0xB78u)); 
        { c.V0 = c.V1 < 0x00000080u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801AD7F8;
        }
        c.V0 = 0x801B0000u; 
        { c.V0 = c.V0 - 0x4614u;  }
        mem.WriteU32((c.A2 + 0xB8u), c.V0); 
        goto L801AD808;
        L801AD7F8: ;
        c.V0 = 0x801B0000u; 
        { c.V0 = c.V0 - 0x3C00u;  }
        mem.WriteU32((c.A2 + 0xB8u), c.V0); 
        { c.V1 = c.V1 - 0x80u;  }
        L801AD808: ;
        { c.V0 = 0u | 0x8000u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _s + _t;  }
        mem.WriteU16((c.A2 + 0x30u), (ushort)c.V0); 
        goto L801AD864;
        L801AD818: ;
        c.V1 = mem.ReadU16((c.A2 + 0xB4u)); 
        if (c.V0 == 0u) {
            goto L801AD864;
        }
        c.V0 = mem.ReadU8((c.A2 + 0x48u)); 
        if (c.V0 == 0u) {
            { c.A1 = c.V1 >> 3;  }
            goto L801AD864;
        }
        { c.A1 = c.V1 >> 3;  }
        { c.A0 = c.V1 & 0x0007u;  }
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x80040000u; 
        { var _s = c.At; var _t = c.A1; c.At = _s + _t;  }
        c.V1 = mem.ReadU8((c.At - 0x4114u)); 
        { var _s = c.A0; var _t = c.V0; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = c.V1; var _t = c.V0; c.V1 = _s | _t;  }
        { c.V0 = 0u | 0x0005u;  }
        c.At = 0x80040000u; 
        { var _s = c.At; var _t = c.A1; c.At = _s + _t;  }
        mem.WriteU8((c.At - 0x4114u), (byte)c.V1); 
        mem.WriteU16((c.A2 + 0x2Cu), (ushort)c.V0); 
        L801AD864: ;
        c.V0 = mem.ReadU32((c.A2 + 0xB8u)); 
        c.A0 = c.A2; 
        c.RA = 0x801AD874u;
        Dispatcher.Call(c, m, c.V0);
        L801AD874: ;
        c.RA = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x18u;  }
        return;
        { c.SP = c.SP - 0x50u;  }
        mem.WriteU32((c.SP + 0x38u), c.S0); 
        c.S0 = c.A0; 
        mem.WriteU32((c.SP + 0x48u), c.RA); 
        mem.WriteU32((c.SP + 0x44u), c.S3); 
        mem.WriteU32((c.SP + 0x40u), c.S2); 
        mem.WriteU32((c.SP + 0x3Cu), c.S1); 
        c.V1 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V1 < 0x00000005u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801ADDC0;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x5C0u)); 
        switch (c.V0)
        {
            case 0x801AD8CCu: goto L801AD8CC;
            case 0x801AD9B0u: goto L801AD9B0;
            case 0x801ADB8Cu: goto L801ADB8C;
            case 0x801ADC64u: goto L801ADC64;
            case 0x801ADD68u: goto L801ADD68;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801AD8CC: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x460u;  }
        c.RA = 0x801AD8DCu; 
        SoTN_bo5.BO5_InitializeEntity(c, m);
        c.V1 = 0x00010000u; 
        c.V0 = mem.ReadU32((c.S0 + 0x34u)); 
        c.A0 = mem.ReadU16((c.S0 + 0x30u)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        c.V1 = 0x08000000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s ^ _t;  }
        if (c.A0 != 0u) {
            mem.WriteU32((c.S0 + 0x34u), c.V0); 
            goto L801AD904;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0060u;  }
        mem.WriteU16((c.S0 + 0x30u), (ushort)c.V0); 
        L801AD904: ;
        { c.A0 = 0u | 0x0004u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0003u;  }
        c.RA = 0x801AD91Cu;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.A0 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.A0 != c.V0) {
            { c.V0 = c.A0 << 1;  }
            goto L801AD938;
        }
        { c.V0 = c.A0 << 1;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)0u); 
        goto L801ADDC0;
        L801AD938: ;
        { var _s = c.V0; var _t = c.A0; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A1 = _s + _t;  }
        c.V0 = mem.ReadU32((c.S0 + 0x34u)); 
        c.V1 = 0x00800000u; 
        mem.WriteU32((c.S0 + 0x64u), c.A0); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        if (c.A1 == 0u) {
            mem.WriteU32((c.S0 + 0x34u), c.V0); 
            goto L801AD984;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        L801AD970: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            goto L801AD970;
        }
        L801AD984: ;
        c.A0 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.V0 = mem.ReadU8(c.A0); 
        mem.WriteU16((c.S0 + 0x80u), (ushort)c.V0); 
        c.V0 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.V1 = mem.ReadU8((c.A0 + 0x1u)); 
        { c.V0 = c.V0 + 0x2u;  }
        mem.WriteU32((c.S0 + 0x7Cu), c.V0); 
        mem.WriteU16((c.S0 + 0x82u), (ushort)c.V1); 
        goto L801ADDC0;
        L801AD9B0: ;
        { c.A0 = c.SP + 0x20u;  }
        c.A1 = 0u; 
        c.A2 = 0u; 
        { c.V0 = 0u | 0x0180u;  }
        mem.WriteU16((c.SP + 0x22u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0040u;  }
        mem.WriteU16((c.SP + 0x20u), (ushort)0u); 
        mem.WriteU16((c.SP + 0x24u), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.A3 = 0u; 
        mem.WriteU16((c.SP + 0x26u), (ushort)c.V0); 
        c.RA = 0x801AD9E0u; 
        DialogueGlyphs.Clear(c, mem);
        c.V1 = mem.ReadU32((c.S0 + 0x64u)); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A1 = _s + _t;  }
        if (c.A1 == 0u) {
            c.A2 = 0u; 
            goto L801ADB80;
        }
        c.A2 = 0u; 
        { c.A0 = 0u | 0x0080u;  }
        L801ADA14: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801ADA8C;
        }
        { c.V0 = 0u | 0x0003u;  }
        { c.V0 = 0u | 0x0006u;  }
        mem.WriteU8((c.A1 + 0x7u), (byte)c.V0); 
        { c.V0 = 0u | 0x0010u;  }
        mem.WriteU16((c.A1 + 0x1Au), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x2u)); 
        { c.V0 = c.V0 >> 1;  }
        { var _s = c.V1; var _t = c.V0; c.V1 = _s - _t;  }
        mem.WriteU16((c.A1 + 0x8u), (ushort)c.V1); 
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x6u)); 
        mem.WriteU8((c.A1 + 0xCu), (byte)0u); 
        mem.WriteU8((c.A1 + 0xDu), (byte)c.A0); 
        { c.V0 = c.V0 >> 1;  }
        { var _s = c.V1; var _t = c.V0; c.V1 = _s - _t;  }
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V1); 
        c.V0 = mem.ReadU8((c.S0 + 0x80u)); 
        mem.WriteU8((c.A1 + 0x18u), (byte)c.V0); 
        c.V1 = mem.ReadU8((c.S0 + 0x82u)); 
        { c.V0 = 0u | 0x01A1u;  }
        mem.WriteU16((c.A1 + 0xEu), (ushort)c.V0); 
        { c.V0 = 0u | 0x01FDu;  }
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        mem.WriteU8((c.A1 + 0x19u), (byte)c.V1); 
        goto L801ADB70;
        L801ADA8C: ;
        mem.WriteU8((c.A1 + 0x7u), (byte)c.V0); 
        c.V1 = mem.ReadU16((c.S0 + 0x80u)); 
        c.V0 = mem.ReadU16((c.S0 + 0x2u)); 
        { c.V1 = c.V1 >> 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s - _t;  }
        { c.V0 = c.V0 - 0x4u;  }
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
        { c.V1 = c.V1 >> 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 + 0x4u;  }
        mem.WriteU16((c.A1 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x14u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0001u;  }
        if (c.A2 != c.V0) {
            goto L801ADB2C;
        }
        c.V1 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V0 = mem.ReadU16((c.S0 + 0x6u)); 
        mem.WriteU8((c.A1 + 0x2Au), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x1Eu), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x12u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x6u), (byte)c.A0); 
        { c.V1 = c.V1 >> 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s - _t;  }
        { c.V0 = c.V0 - 0x4u;  }
        goto L801ADB50;
        L801ADB2C: ;
        c.V1 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V0 = mem.ReadU16((c.S0 + 0x6u)); 
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x11u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x5u), (byte)c.A0); 
        { c.V1 = c.V1 >> 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 + 0x4u;  }
        L801ADB50: ;
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0); 
        { c.V0 = 0u | 0x01FCu;  }
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0011u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        L801ADB70: ;
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801ADA14;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801ADB80: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801ADD60;
        L801ADB8C: ;
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x82u)); 
        { c.V0 = c.V0 >> 1;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        c.S2 = 0x80080000u; 
        { c.S2 = c.S2 - 0x101Cu;  }
        c.S1 = c.S2; 
        c.A0 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.A2 = 0u; 
        c.V0 = c.LO; 
        if ((int)c.V0 <= 0) {
            mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
            goto L801ADBE8;
        }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        L801ADBBC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU8(c.S1, (byte)0u); 
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x82u)); 
        { c.V0 = c.V0 >> 1;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        { c.A2 = c.A2 + 0x1u;  }
        c.V0 = c.LO; 
        { var _s = c.A2; var _t = c.V0; c.V0 = (int)_s < (int)_t ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.S1 = c.S1 + 0x1u;  }
            goto L801ADBBC;
        }
        { c.S1 = c.S1 + 0x1u;  }
        c.S1 = c.S2; 
        L801ADBE8: ;
        { c.S3 = 0u | 0x0001u;  }
        L801ADBEC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.A0); 
        if (c.V0 == 0u) {
            c.A1 = 0u; 
            goto L801ADC34;
        }
        c.A1 = 0u; 
        if (c.V0 != c.S3) {
            { c.A1 = c.SP + 0x28u;  }
            goto L801ADC1C;
        }
        { c.A1 = c.SP + 0x28u;  }
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        { c.A0 = c.A0 + 0x1u;  }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        { c.V0 = c.V0 << 3;  }
        { var _s = c.S2; var _t = c.V0; c.S1 = _s + _t;  }
        goto L801ADBEC;
        L801ADC1C: ;
        c.A3 = mem.ReadU16((c.S0 + 0x80u)); 
        c.A2 = c.S1; 
        { c.A3 = c.A3 >> 1;  }
        c.RA = 0x801ADC2Cu; 
        SoTN_bo5.BO5_BlitChar(c, m);
        c.A0 = c.V0; 
        goto L801ADBEC;
        L801ADC34: ;
        c.A0 = c.S2; 
        { c.V0 = 0u | 0x0180u;  }
        mem.WriteU32((c.SP + 0x10u), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        c.A2 = 0u; 
        mem.WriteU32((c.SP + 0x14u), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.A3 = 0u; 
        mem.WriteU32((c.SP + 0x18u), c.V0); 
        c.RA = 0x801ADC5Cu; 
        SoTN_main.LoadTPage(c, m);
        goto L801ADD54;
        L801ADC64: ;
        c.V0 = mem.ReadU16((c.S0 + 0x86u)); 
        c.V1 = mem.ReadU32((c.S0 + 0x64u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x86u), (ushort)c.V0); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.At = 0x80080000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A1 = mem.ReadU32((c.At + 0x6FECu)); 
        if (c.A1 == 0u) {
            c.A2 = 0u; 
            goto L801ADD44;
        }
        c.A2 = 0u; 
        L801ADCA0: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            goto L801ADCF0;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = c.V0 + 0x8u;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        c.V0 = c.LO; 
        if ((int)c.V0 >= 0) {
            goto L801ADCC8;
        }
        { c.V0 = c.V0 + 0x7u;  }
        L801ADCC8: ;
        { c.V0 = (uint)((int)c.V0 >> 3);  }
        c.V1 = mem.ReadU16((c.A1 + 0xAu)); 
        c.A0 = mem.ReadU8((c.A1 + 0x12u)); 
        { var _s = c.V1; var _t = c.V0; c.V1 = _s + _t;  }
        { c.A0 = c.A0 - 0x10u;  }
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V1); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V1); 
        mem.WriteU8((c.A1 + 0x12u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x6u), (byte)c.A0); 
        goto L801ADD34;
        L801ADCF0: ;
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = c.V0 + 0x8u;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        c.V0 = c.LO; 
        if ((int)c.V0 >= 0) {
            goto L801ADD10;
        }
        { c.V0 = c.V0 + 0x7u;  }
        L801ADD10: ;
        { c.V0 = (uint)((int)c.V0 >> 3);  }
        c.V1 = mem.ReadU16((c.A1 + 0x22u)); 
        c.A0 = mem.ReadU8((c.A1 + 0x29u)); 
        { var _s = c.V1; var _t = c.V0; c.V1 = _s - _t;  }
        { c.A0 = c.A0 - 0x10u;  }
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V1); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V1); 
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0); 
        L801ADD34: ;
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801ADCA0;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801ADD44: ;
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = 0u | 0x0008u;  }
        if (c.V1 != c.V0) {
            goto L801ADDC0;
        }
        L801ADD54: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        mem.WriteU16((c.S0 + 0x86u), (ushort)0u); 
        { c.V0 = c.V0 + 0x1u;  }
        L801ADD60: ;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        goto L801ADDC0;
        L801ADD68: ;
        c.V1 = mem.ReadU32((c.S0 + 0x64u)); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.At = 0x80080000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        mem.WriteU16((c.At + 0x701Eu), (ushort)0u); 
        c.V0 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x86u), (ushort)c.V0); 
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        c.V0 = mem.ReadU16((c.S0 + 0x30u)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s < _t ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801ADDC0;
        }
        c.A0 = c.S0; 
        c.RA = 0x801ADDC0u; 
        SoTN_bo5.DestroyEntity_bo5(c, m);
        L801ADDC0: ;
        c.RA = mem.ReadU32((c.SP + 0x48u)); 
        c.S3 = mem.ReadU32((c.SP + 0x44u)); 
        c.S2 = mem.ReadU32((c.SP + 0x40u)); 
        c.S1 = mem.ReadU32((c.SP + 0x3Cu)); 
        c.S0 = mem.ReadU32((c.SP + 0x38u)); 
        { c.SP = c.SP + 0x50u;  }
        return;
        }
    public static bool BO5_EntityMessageBox(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo5", m, false)) return true;
        BO5_EntityMessageBox_Body(c, m);
        return false;
    }
    private static void BO5_EntityMessageBox_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo5", 0x801B553Cu);
        { c.SP = c.SP - 0x50u;  }
        mem.WriteU32((c.SP + 0x38u), c.S0); 
        c.S0 = c.A0; 
        mem.WriteU32((c.SP + 0x48u), c.RA); 
        mem.WriteU32((c.SP + 0x44u), c.S3); 
        mem.WriteU32((c.SP + 0x40u), c.S2); 
        mem.WriteU32((c.SP + 0x3Cu), c.S1); 
        c.V1 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V1 < 0x00000005u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801ADDC0;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x5C0u)); 
        switch (c.V0)
        {
            case 0x801AD8CCu: goto L801AD8CC;
            case 0x801AD9B0u: goto L801AD9B0;
            case 0x801ADB8Cu: goto L801ADB8C;
            case 0x801ADC64u: goto L801ADC64;
            case 0x801ADD68u: goto L801ADD68;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801AD8CC: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x460u;  }
        c.RA = 0x801AD8DCu; 
        SoTN_bo5.BO5_InitializeEntity(c, m);
        c.V1 = 0x00010000u; 
        c.V0 = mem.ReadU32((c.S0 + 0x34u)); 
        c.A0 = mem.ReadU16((c.S0 + 0x30u)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        c.V1 = 0x08000000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s ^ _t;  }
        if (c.A0 != 0u) {
            mem.WriteU32((c.S0 + 0x34u), c.V0); 
            goto L801AD904;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0060u;  }
        mem.WriteU16((c.S0 + 0x30u), (ushort)c.V0); 
        L801AD904: ;
        { c.A0 = 0u | 0x0004u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0003u;  }
        c.RA = 0x801AD91Cu;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.A0 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.A0 != c.V0) {
            { c.V0 = c.A0 << 1;  }
            goto L801AD938;
        }
        { c.V0 = c.A0 << 1;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)0u); 
        goto L801ADDC0;
        L801AD938: ;
        { var _s = c.V0; var _t = c.A0; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A1 = _s + _t;  }
        c.V0 = mem.ReadU32((c.S0 + 0x34u)); 
        c.V1 = 0x00800000u; 
        mem.WriteU32((c.S0 + 0x64u), c.A0); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        if (c.A1 == 0u) {
            mem.WriteU32((c.S0 + 0x34u), c.V0); 
            goto L801AD984;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        L801AD970: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            goto L801AD970;
        }
        L801AD984: ;
        c.A0 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.V0 = mem.ReadU8(c.A0); 
        mem.WriteU16((c.S0 + 0x80u), (ushort)c.V0); 
        c.V0 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.V1 = mem.ReadU8((c.A0 + 0x1u)); 
        { c.V0 = c.V0 + 0x2u;  }
        mem.WriteU32((c.S0 + 0x7Cu), c.V0); 
        mem.WriteU16((c.S0 + 0x82u), (ushort)c.V1); 
        goto L801ADDC0;
        L801AD9B0: ;
        { c.A0 = c.SP + 0x20u;  }
        c.A1 = 0u; 
        c.A2 = 0u; 
        { c.V0 = 0u | 0x0180u;  }
        mem.WriteU16((c.SP + 0x22u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0040u;  }
        mem.WriteU16((c.SP + 0x20u), (ushort)0u); 
        mem.WriteU16((c.SP + 0x24u), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.A3 = 0u; 
        mem.WriteU16((c.SP + 0x26u), (ushort)c.V0); 
        c.RA = 0x801AD9E0u; 
        DialogueGlyphs.Clear(c, mem);
        c.V1 = mem.ReadU32((c.S0 + 0x64u)); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A1 = _s + _t;  }
        if (c.A1 == 0u) {
            c.A2 = 0u; 
            goto L801ADB80;
        }
        c.A2 = 0u; 
        { c.A0 = 0u | 0x0080u;  }
        L801ADA14: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801ADA8C;
        }
        { c.V0 = 0u | 0x0003u;  }
        { c.V0 = 0u | 0x0006u;  }
        mem.WriteU8((c.A1 + 0x7u), (byte)c.V0); 
        { c.V0 = 0u | 0x0010u;  }
        mem.WriteU16((c.A1 + 0x1Au), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x2u)); 
        { c.V0 = c.V0 >> 1;  }
        { var _s = c.V1; var _t = c.V0; c.V1 = _s - _t;  }
        mem.WriteU16((c.A1 + 0x8u), (ushort)c.V1); 
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x6u)); 
        mem.WriteU8((c.A1 + 0xCu), (byte)0u); 
        mem.WriteU8((c.A1 + 0xDu), (byte)c.A0); 
        { c.V0 = c.V0 >> 1;  }
        { var _s = c.V1; var _t = c.V0; c.V1 = _s - _t;  }
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V1); 
        c.V0 = mem.ReadU8((c.S0 + 0x80u)); 
        mem.WriteU8((c.A1 + 0x18u), (byte)c.V0); 
        c.V1 = mem.ReadU8((c.S0 + 0x82u)); 
        { c.V0 = 0u | 0x01A1u;  }
        mem.WriteU16((c.A1 + 0xEu), (ushort)c.V0); 
        { c.V0 = 0u | 0x01FDu;  }
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        mem.WriteU8((c.A1 + 0x19u), (byte)c.V1); 
        goto L801ADB70;
        L801ADA8C: ;
        mem.WriteU8((c.A1 + 0x7u), (byte)c.V0); 
        c.V1 = mem.ReadU16((c.S0 + 0x80u)); 
        c.V0 = mem.ReadU16((c.S0 + 0x2u)); 
        { c.V1 = c.V1 >> 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s - _t;  }
        { c.V0 = c.V0 - 0x4u;  }
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
        { c.V1 = c.V1 >> 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 + 0x4u;  }
        mem.WriteU16((c.A1 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x14u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0001u;  }
        if (c.A2 != c.V0) {
            goto L801ADB2C;
        }
        c.V1 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V0 = mem.ReadU16((c.S0 + 0x6u)); 
        mem.WriteU8((c.A1 + 0x2Au), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x1Eu), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x12u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x6u), (byte)c.A0); 
        { c.V1 = c.V1 >> 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s - _t;  }
        { c.V0 = c.V0 - 0x4u;  }
        goto L801ADB50;
        L801ADB2C: ;
        c.V1 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V0 = mem.ReadU16((c.S0 + 0x6u)); 
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x11u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x5u), (byte)c.A0); 
        { c.V1 = c.V1 >> 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 + 0x4u;  }
        L801ADB50: ;
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0); 
        { c.V0 = 0u | 0x01FCu;  }
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0011u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        L801ADB70: ;
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801ADA14;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801ADB80: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801ADD60;
        L801ADB8C: ;
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x82u)); 
        { c.V0 = c.V0 >> 1;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        c.S2 = 0x80080000u; 
        { c.S2 = c.S2 - 0x101Cu;  }
        c.S1 = c.S2; 
        c.A0 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.A2 = 0u; 
        c.V0 = c.LO; 
        if ((int)c.V0 <= 0) {
            mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
            goto L801ADBE8;
        }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        L801ADBBC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU8(c.S1, (byte)0u); 
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x82u)); 
        { c.V0 = c.V0 >> 1;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        { c.A2 = c.A2 + 0x1u;  }
        c.V0 = c.LO; 
        { var _s = c.A2; var _t = c.V0; c.V0 = (int)_s < (int)_t ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.S1 = c.S1 + 0x1u;  }
            goto L801ADBBC;
        }
        { c.S1 = c.S1 + 0x1u;  }
        c.S1 = c.S2; 
        L801ADBE8: ;
        { c.S3 = 0u | 0x0001u;  }
        L801ADBEC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.A0); 
        if (c.V0 == 0u) {
            c.A1 = 0u; 
            goto L801ADC34;
        }
        c.A1 = 0u; 
        if (c.V0 != c.S3) {
            { c.A1 = c.SP + 0x28u;  }
            goto L801ADC1C;
        }
        { c.A1 = c.SP + 0x28u;  }
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        { c.A0 = c.A0 + 0x1u;  }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        { c.V0 = c.V0 << 3;  }
        { var _s = c.S2; var _t = c.V0; c.S1 = _s + _t;  }
        goto L801ADBEC;
        L801ADC1C: ;
        c.A3 = mem.ReadU16((c.S0 + 0x80u)); 
        c.A2 = c.S1; 
        { c.A3 = c.A3 >> 1;  }
        c.RA = 0x801ADC2Cu; 
        SoTN_bo5.BO5_BlitChar(c, m);
        c.A0 = c.V0; 
        goto L801ADBEC;
        L801ADC34: ;
        c.A0 = c.S2; 
        { c.V0 = 0u | 0x0180u;  }
        mem.WriteU32((c.SP + 0x10u), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        c.A2 = 0u; 
        mem.WriteU32((c.SP + 0x14u), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.A3 = 0u; 
        mem.WriteU32((c.SP + 0x18u), c.V0); 
        c.RA = 0x801ADC5Cu; 
        SoTN_main.LoadTPage(c, m);
        goto L801ADD54;
        L801ADC64: ;
        c.V0 = mem.ReadU16((c.S0 + 0x86u)); 
        c.V1 = mem.ReadU32((c.S0 + 0x64u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x86u), (ushort)c.V0); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.At = 0x80080000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A1 = mem.ReadU32((c.At + 0x6FECu)); 
        if (c.A1 == 0u) {
            c.A2 = 0u; 
            goto L801ADD44;
        }
        c.A2 = 0u; 
        L801ADCA0: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            goto L801ADCF0;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = c.V0 + 0x8u;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        c.V0 = c.LO; 
        if ((int)c.V0 >= 0) {
            goto L801ADCC8;
        }
        { c.V0 = c.V0 + 0x7u;  }
        L801ADCC8: ;
        { c.V0 = (uint)((int)c.V0 >> 3);  }
        c.V1 = mem.ReadU16((c.A1 + 0xAu)); 
        c.A0 = mem.ReadU8((c.A1 + 0x12u)); 
        { var _s = c.V1; var _t = c.V0; c.V1 = _s + _t;  }
        { c.A0 = c.A0 - 0x10u;  }
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V1); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V1); 
        mem.WriteU8((c.A1 + 0x12u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x6u), (byte)c.A0); 
        goto L801ADD34;
        L801ADCF0: ;
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = c.V0 + 0x8u;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        c.V0 = c.LO; 
        if ((int)c.V0 >= 0) {
            goto L801ADD10;
        }
        { c.V0 = c.V0 + 0x7u;  }
        L801ADD10: ;
        { c.V0 = (uint)((int)c.V0 >> 3);  }
        c.V1 = mem.ReadU16((c.A1 + 0x22u)); 
        c.A0 = mem.ReadU8((c.A1 + 0x29u)); 
        { var _s = c.V1; var _t = c.V0; c.V1 = _s - _t;  }
        { c.A0 = c.A0 - 0x10u;  }
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V1); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V1); 
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0); 
        L801ADD34: ;
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801ADCA0;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801ADD44: ;
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = 0u | 0x0008u;  }
        if (c.V1 != c.V0) {
            goto L801ADDC0;
        }
        L801ADD54: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        mem.WriteU16((c.S0 + 0x86u), (ushort)0u); 
        { c.V0 = c.V0 + 0x1u;  }
        L801ADD60: ;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        goto L801ADDC0;
        L801ADD68: ;
        c.V1 = mem.ReadU32((c.S0 + 0x64u)); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.At = 0x80080000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        mem.WriteU16((c.At + 0x701Eu), (ushort)0u); 
        c.V0 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x86u), (ushort)c.V0); 
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        c.V0 = mem.ReadU16((c.S0 + 0x30u)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s < _t ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801ADDC0;
        }
        c.A0 = c.S0; 
        c.RA = 0x801ADDC0u; 
        SoTN_bo5.DestroyEntity_bo5(c, m);
        L801ADDC0: ;
        c.RA = mem.ReadU32((c.SP + 0x48u)); 
        c.S3 = mem.ReadU32((c.SP + 0x44u)); 
        c.S2 = mem.ReadU32((c.SP + 0x40u)); 
        c.S1 = mem.ReadU32((c.SP + 0x3Cu)); 
        c.S0 = mem.ReadU32((c.SP + 0x38u)); 
        { c.SP = c.SP + 0x50u;  }
        return;
        }
}
