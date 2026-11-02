using RecompOne.Runtime.Context;
using RecompOne.Runtime.Dispatch;
using RecompOne.Runtime.Memory;

namespace Recompiled.Translation;

public static class ScriptFunctions_rbo6
{
    public static bool func_801A1BE0(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rbo6", m, true)) return true;
        func_801A1BE0_Body(c, m);
        return false;
    }
    private static void func_801A1BE0_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rbo6", 0x801B3F50u);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x3F58u)); 
        { c.V1 = 0u | 0x0008u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x3F5Cu), (ushort)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x3F5Au), (ushort)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x3F5Eu), (ushort)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x3F62u), (ushort)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU8((c.At + 0x3F66u), (byte)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU8((c.At + 0x3F67u), (byte)c.V1); 
        { c.V0 = c.V0 + 0x14u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x3F56u), (ushort)c.V0); 
        return;
        }
    public static bool RBO6_SetCutsceneScript(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rbo6", m, true)) return true;
        RBO6_SetCutsceneScript_Body(c, m);
        return false;
    }
    private static void RBO6_SetCutsceneScript_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rbo6", 0x801B3F50u);
        c.A0 = DialogueScripts.ResolveStart("rbo6", c.A0, m);
        { c.SP = c.SP - 0x20u;  }
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        c.S1 = c.A0; 
        { c.A0 = 0u | 0x0006u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0007u;  }
        mem.WriteU32((c.SP + 0x18u), c.RA); 
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.RA = 0x801A1C58u;
        Dispatcher.Call(c, m, c.V0);
        c.S0 = 0x801B0000u; 
        { c.S0 = c.S0 + 0x3F88u;  }
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        c.V1 = 0xFFFFFFFFu; 
        if (c.V0 != c.V1) {
            mem.WriteU32(c.S0, c.V0); 
            goto L801A1C80;
        }
        mem.WriteU32(c.S0, c.V0); 
        mem.WriteU32(c.S0, 0u); 
        c.V0 = 0u; 
        goto L801A1DDC;
        L801A1C80: ;
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3F50u), c.S1); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x3F8Cu), (ushort)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3F84u), c.V1); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3F80u), c.V1); 
        c.RA = 0x801A1CA8u; 
        SoTN_rbo6.func_801A1BE0(c, m);
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
        mem.WriteU32((c.At + 0x3F68u), c.V0); 
        c.V1 = mem.ReadU32(c.V0); 
        { c.A2 = 0u | 0x0008u;  }
        mem.WriteU16((c.V0 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3F6Cu), c.V1); 
        c.V0 = mem.ReadU32(c.V1); 
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3F70u), c.V0); 
        c.V1 = mem.ReadU32(c.V0); 
        mem.WriteU16((c.V0 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3F74u), c.V1); 
        c.A0 = mem.ReadU32(c.V1); 
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3F78u), c.A0); 
        c.A1 = mem.ReadU32(c.A0); 
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.A0 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3F7Cu), c.A1); 
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
        L801A1DDC: ;
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool RBO6_CutsceneUnk3(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rbo6", m, false)) return true;
        RBO6_CutsceneUnk3_Body(c, m);
        return false;
    }
    private static void RBO6_CutsceneUnk3_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rbo6", 0x801B3F50u);
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
        c.RA = 0x801A1E40u; 
        DialogueGlyphs.Clear(c, mem);
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool RBO6_CutsceneUnk4(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rbo6", m, true)) return true;
        RBO6_CutsceneUnk4_Body(c, m);
        return false;
    }
    private static void RBO6_CutsceneUnk4_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rbo6", 0x801B3F50u);
        DialogueGlyphs.PrepareLine(c, mem);
        { c.SP = c.SP - 0x18u;  }
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.S0 = 0x801B0000u; 
        { c.S0 = c.S0 + 0x3F5Eu;  }
        mem.WriteU32((c.SP + 0x14u), c.RA); 
        c.A0 = (uint)(short)mem.ReadU16(c.S0); 
        c.RA = 0x801A1E70u; 
        SoTN_rbo6.RBO6_CutsceneUnk3(c, m);
        c.V0 = (uint)(short)mem.ReadU16(c.S0); 
        { c.V0 = c.V0 << 2;  }
        { var _s = c.S0; var _t = c.V0; c.V0 = _s + _t;  }
        c.V1 = mem.ReadU32((c.V0 + 0xAu)); 
        { c.V0 = 0u | 0x0010u;  }
        mem.WriteU16((c.V1 + 0x1Au), (ushort)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x3F64u)); 
        mem.WriteU16((c.V1 + 0xEu), (ushort)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x3F56u)); 
        mem.WriteU8((c.V1 + 0xCu), (byte)0u); 
        mem.WriteU16((c.V1 + 0xAu), (ushort)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x3F54u)); 
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
    public static bool RBO6_DrawCutsceneActorName(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rbo6", m, true)) return true;
        RBO6_DrawCutsceneActorName_Body(c, m);
        return false;
    }
    private static void RBO6_DrawCutsceneActorName_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rbo6", 0x801B3F50u);
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
        c.A2 = mem.ReadU32((c.At + 0x90Cu)); 
        { c.V0 = 0u | 0x00FFu;  }
        L801A1F3C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V1 = mem.ReadU8(c.A2); 
        if (c.V1 != c.V0) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801A1F5C;
        }
        { c.A2 = c.A2 + 0x1u;  }
        c.V1 = mem.ReadU8(c.A2); 
        if (c.V1 == 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801A1F6C;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801A1F5C: ;
        if (c.V1 == 0u) {
            goto L801A1F3C;
        }
        { c.A1 = c.A1 + 0x1u;  }
        goto L801A1F3C;
        L801A1F6C: ;
        { c.A0 = 0u | 0x0006u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = c.A1 & 0xFFFFu;  }
        c.RA = 0x801A1F84u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.A1 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.A1 != c.V0) {
            { c.V0 = c.A1 << 1;  }
            goto L801A1FA8;
        }
        { c.V0 = c.A1 << 1;  }
        c.A0 = c.S1; 
        c.RA = 0x801A1FA0u; 
        SoTN_rbo6.DestroyEntity_rbo6(c, m);
        goto L801A2074;
        L801A1FA8: ;
        { var _s = c.V0; var _t = c.A1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.A1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A0 = _s + _t;  }
        c.V1 = 0x801B0000u; 
        { c.V1 = c.V1 + 0x3F84u;  }
        { c.V0 = c.S0 & 0xFFFFu;  }
        { c.V0 = c.V0 << 2;  }
        mem.WriteU32(c.V1, c.A1); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A2 = mem.ReadU32((c.At + 0x90Cu)); 
        if (c.A0 == 0u) {
            { c.A3 = 0u | 0x0038u;  }
            goto L801A2074;
        }
        { c.A3 = 0u | 0x0038u;  }
        { c.T4 = 0u | 0x0006u;  }
        { c.T3 = 0u | 0x001Eu;  }
        { c.T2 = 0u | 0x0196u;  }
        { c.T1 = 0u | 0x01FFu;  }
        { c.A1 = 0u | 0x0008u;  }
        { c.T0 = c.V1 - 0x2Cu;  }
        L801A2004: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V1 = mem.ReadU8(c.A2); 
        if (c.V1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801A201C;
        }
        { c.A2 = c.A2 + 0x1u;  }
        { c.A3 = c.A3 + 0x4u;  }
        goto L801A2068;
        L801A201C: ;
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
        L801A2068: ;
        if (c.A0 != 0u) {
            goto L801A2004;
        }
        L801A2074: ;
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool func_801A208C(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rbo6", m, true)) return true;
        func_801A208C_Body(c, m);
        return false;
    }
    private static void func_801A208C_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rbo6", 0x801B3F50u);
        c.V0 = 0x00100000u; 
        { var _s = c.A0; var _t = c.V0; c.A0 = _s + _t;  }
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3F90u), c.A0); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x3F8Eu), (ushort)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x3F8Cu), (ushort)c.V0); 
        return;
        }
    public static bool RBO6_CutsceneRun(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rbo6", m, true)) return true;
        RBO6_CutsceneRun_Body(c, m);
        return false;
    }
    private static void RBO6_CutsceneRun_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rbo6", 0x801B3F50u);
        { c.SP = c.SP - 0x20u;  }
        c.A0 = 0x801B0000u; 
        { c.A0 = c.A0 + 0x3F8Eu;  }
        mem.WriteU32((c.SP + 0x1Cu), c.RA); 
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
            goto L801A2118;
        }
        { c.S1 = c.A0 + 0x2u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x3F8Cu), (ushort)0u); 
        goto L801A2378;
        L801A2108: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU32(c.S1, c.A1); 
        goto L801A2378;
        L801A2110: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU32(c.S1, c.A2); 
        goto L801A2378;
        L801A2118: ;
        { c.S2 = 0u | 0x0001u;  }
        L801A211C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A1 = mem.ReadU32(c.S1); 
        c.V1 = mem.ReadU16((c.S1 - 0x2u)); 
        { c.V0 = c.A1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.A0 = mem.ReadU8(c.A1); 
        { c.A2 = c.A1 + 0x2u;  }
        mem.WriteU32(c.S1, c.A2); 
        c.V0 = mem.ReadU8((c.A1 + 0x1u)); 
        { c.A0 = c.A0 << 8;  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s | _t;  }
        { var _s = c.V1; var _t = c.V0; c.V1 = _s < _t ? 1u : 0u;  }
        if (c.V1 != 0u) {
            { c.V0 = c.A1 + 0x3u;  }
            goto L801A2108;
        }
        { c.V0 = c.A1 + 0x3u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V1 = mem.ReadU8((c.A1 + 0x2u)); 
        if (c.V1 == c.S2) {
            { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
            goto L801A22C4;
        }
        { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A217C;
        }
        if (c.V1 == 0u) {
            { c.V0 = c.A1 + 0x4u;  }
            goto L801A2198;
        }
        { c.V0 = c.A1 + 0x4u;  }
        goto L801A211C;
        L801A217C: ;
        { c.V0 = 0u | 0x0002u;  }
        if (c.V1 == c.V0) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801A2300;
        }
        { c.V0 = 0u | 0x0003u;  }
        if (c.V1 == c.V0) {
            { c.V0 = c.A1 + 0x4u;  }
            goto L801A2350;
        }
        { c.V0 = c.A1 + 0x4u;  }
        goto L801A211C;
        L801A2198: ;
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
        c.RA = 0x801A21CCu; 
        SoTN_rbo6.DestroyEntity_rbo6(c, m);
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8(c.V1); 
        mem.WriteU16((c.S0 + 0x26u), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x26u)); 
        { c.V0 = c.V0 << 2;  }
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x3FCu)); 
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
        c.V0 = mem.ReadU16((c.S0 + 0x2u)); 
        c.V1 = 0x80070000u; 
        c.V1 = mem.ReadU16((c.V1 + 0x308Eu)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s - _t;  }
        mem.WriteU16((c.S0 + 0x2u), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x6u)); 
        c.V1 = 0x80070000u; 
        c.V1 = mem.ReadU16((c.V1 + 0x3092u)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s - _t;  }
        mem.WriteU16((c.S0 + 0x6u), (ushort)c.V0); 
        goto L801A211C;
        L801A22C4: ;
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
        c.RA = 0x801A22F8u; 
        SoTN_rbo6.DestroyEntity_rbo6(c, m);
        goto L801A211C;
        L801A2300: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3F90u)); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x3FFCu)); 
        c.V0 = mem.ReadU8(c.V0); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _t >> (int)(_s & 31u);  }
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.A1 + 0x4u;  }
            goto L801A2110;
        }
        { c.V0 = c.A1 + 0x4u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8((c.A1 + 0x3u)); 
        { var _s = c.V0; var _t = c.S2; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = 0u; var _t = c.V0; c.V0 = ~(_s | _t);  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s & _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3FFCu), c.V0); 
        goto L801A211C;
        L801A2350: ;
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8((c.A1 + 0x3u)); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x3FFCu)); 
        { var _s = c.V0; var _t = c.S2; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3FFCu), c.V0); 
        goto L801A211C;
        L801A2378: ;
        c.RA = mem.ReadU32((c.SP + 0x1Cu)); 
        c.S2 = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool RBO6_CutsceneSkip(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rbo6", m, true)) return true;
        RBO6_CutsceneSkip_Body(c, m);
        return false;
    }
    private static void RBO6_CutsceneSkip_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rbo6", 0x801B3F50u);
        { c.SP = c.SP - 0x20u;  }
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.S0 = c.A0; 
        c.V1 = 0x80090000u; 
        c.V1 = mem.ReadU16((c.V1 + 0x7494u)); 
        { c.V0 = 0u | 0x0800u;  }
        mem.WriteU32((c.SP + 0x1Cu), c.RA); 
        mem.WriteU32((c.SP + 0x18u), c.S2); 
        if (c.V1 != c.V0) {
            mem.WriteU32((c.SP + 0x14u), c.S1); 
            goto L801A2458;
        }
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        c.S1 = 0x80040000u; 
        { c.S1 = c.S1 - 0x384Cu;  }
        c.A0 = mem.ReadU32((c.S0 + 0x64u)); 
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3F4Cu), c.V0); 
        c.S2 = 0xFFFFFFFFu; 
        c.RA = 0x801A23E0u;
        Dispatcher.Call(c, m, c.V1);
        c.V0 = mem.ReadU32((c.S0 + 0x34u)); 
        c.V1 = 0x00800000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s ^ _t;  }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        c.A0 = 0x801B0000u; 
        c.A0 = mem.ReadU32((c.A0 + 0x3F84u)); 
        if (c.A0 == c.S2) {
            goto L801A2414;
        }
        c.V0 = mem.ReadU32(c.S1); 
        c.RA = 0x801A2414u;
        Dispatcher.Call(c, m, c.V0);
        L801A2414: ;
        c.A0 = 0x801B0000u; 
        c.A0 = mem.ReadU32((c.A0 + 0x3F80u)); 
        if (c.A0 == c.S2) {
            goto L801A2438;
        }
        c.V0 = mem.ReadU32(c.S1); 
        c.RA = 0x801A2438u;
        Dispatcher.Call(c, m, c.V0);
        L801A2438: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3824u)); 
        { c.A0 = 0u | 0x000Au;  }
        c.RA = 0x801A244Cu;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x2Eu), (ushort)0u); 
        L801A2458: ;
        c.RA = mem.ReadU32((c.SP + 0x1Cu)); 
        c.S2 = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool RBO6_ScaleCutsceneAvatar(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rbo6", m, true)) return true;
        RBO6_ScaleCutsceneAvatar_Body(c, m);
        return false;
    }
    private static void RBO6_ScaleCutsceneAvatar_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rbo6", 0x801B3F50u);
        c.V0 = 0x801B0000u; 
        c.V0 = (uint)(short)mem.ReadU16((c.V0 + 0x3F5Eu)); 
        { c.A3 = c.V0 + (uint)(mem.LineCount - DialogueGlyphs.VisibleLines(mem));  }
        { c.V0 = (int)c.A3 < 5 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A24A4;
        }
        { c.A3 = c.A3 - 0x5u;  }
        L801A2494: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (int)c.A3 < 5 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.A3 = c.A3 - 0x5u;  }
            goto L801A2494;
        }
        { c.A3 = c.A3 - 0x5u;  }
        { c.A3 = c.A3 + 0x5u;  }
        L801A24A4: ;
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C48u)); 
        c.V0 = mem.ReadU16((c.V0 + 0x2Eu)); 
        if (c.V0 != 0u) {
            c.V1 = 0u; 
            goto L801A2520;
        }
        c.V1 = 0u; 
        { c.V0 = c.A3 << 2;  }
        c.At = 0x801B0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A1 = mem.ReadU32((c.At + 0x3F68u)); 
        c.V0 = mem.ReadU8((c.A1 + 0x19u)); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s - _t;  }
        mem.WriteU8((c.A1 + 0x19u), (byte)c.V0); 
        c.V0 = mem.ReadU8((c.A1 + 0xDu)); 
        c.V1 = mem.ReadU8((c.A1 + 0x19u)); 
        { var _s = c.A0; var _t = c.V0; c.V0 = _s + _t;  }
        if (c.V1 != 0u) {
            mem.WriteU8((c.A1 + 0xDu), (byte)c.V0); 
            goto L801A251C;
        }
        mem.WriteU8((c.A1 + 0xDu), (byte)c.V0); 
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C48u)); 
        c.V1 = mem.ReadU16((c.V0 + 0x2Eu)); 
        { c.V1 = c.V1 + 0x1u;  }
        mem.WriteU16((c.V0 + 0x2Eu), (ushort)c.V1); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        L801A251C: ;
        c.V1 = 0u; 
        L801A2520: ;
        { c.A0 = c.A0 & 0x00FFu;  }
        c.A2 = 0x801B0000u; 
        { c.A2 = c.A2 + 0x3F68u;  }
        L801A252C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.V1 == c.A3) {
            goto L801A254C;
        }
        c.A1 = mem.ReadU32(c.A2); 
        c.V0 = mem.ReadU16((c.A1 + 0xAu)); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s - _t;  }
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0); 
        L801A254C: ;
        { c.V1 = c.V1 + 0x1u;  }
        { c.V0 = (int)c.V1 < 5 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.A2 = c.A2 + 0x4u;  }
            goto L801A252C;
        }
        { c.A2 = c.A2 + 0x4u;  }
        c.V1 = 0x801B0000u; 
        { c.V1 = c.V1 + 0x3F60u;  }
        c.V0 = mem.ReadU16(c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16(c.V1, (ushort)c.V0); 
        return;
        }
    public static bool func_801A2578(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rbo6", m, true)) return true;
        func_801A2578_Body(c, m);
        return false;
    }
    private static void func_801A2578_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rbo6", 0x801B3F50u);
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
            goto L801A2650;
        }
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x4000u)); 
        if (c.V0 == 0u) {
            goto L801A2624;
        }
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3F4Cu)); 
        if (c.V0 != 0u) {
            goto L801A2624;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x4220u)); 
        if (c.V0 != 0u) {
            goto L801A261C;
        }
        c.V0 = mem.ReadU16((c.S2 + 0x30u)); 
        if (c.V0 == 0u) {
            c.V1 = 0x00400000u; 
            goto L801A2604;
        }
        c.V1 = 0x00400000u; 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x34FCu)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s & _t;  }
        goto L801A2614;
        L801A2604: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x34FCu)); 
        c.V1 = 0x00200000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s & _t;  }
        L801A2614: ;
        if (c.V0 == 0u) {
            c.A0 = c.S2; 
            goto L801A2624;
        }
        c.A0 = c.S2; 
        L801A261C: ;
        c.RA = 0x801A2624u; 
        SoTN_rbo6.RBO6_CutsceneSkip(c, m);
        L801A2624: ;
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        if (c.V0 == 0u) {
            goto L801A2650;
        }
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x3F8Cu)); 
        if (c.V0 == 0u) {
            goto L801A2650;
        }
        c.RA = 0x801A2650u; 
        SoTN_rbo6.RBO6_CutsceneRun(c, m);
        L801A2650: ;
        c.V1 = mem.ReadU16((c.S2 + 0x2Cu)); 
        { c.V0 = c.V1 < 0x00000008u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801A3608;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x3154u)); 
        switch (c.V0)
        {
            case 0x801A267Cu: goto L801A267C;
            case 0x801A2760u: goto L801A2760;
            case 0x801A304Cu: goto L801A304C;
            case 0x801A3080u: goto L801A3080;
            case 0x801A3144u: goto L801A3144;
            case 0x801A31DCu: goto L801A31DC;
            case 0x801A34C4u: goto L801A34C4;
            case 0x801A356Cu: goto L801A356C;
            case 0x801A27D8u: goto L801A27D8;
            case 0x801A27E4u: goto L801A27E4;
            case 0x801A2890u: goto L801A2890;
            case 0x801A28BCu: goto L801A28BC;
            case 0x801A28ECu: goto L801A28EC;
            case 0x801A292Cu: goto L801A292C;
            case 0x801A2A64u: goto L801A2A64;
            case 0x801A2ADCu: goto L801A2ADC;
            case 0x801A2B70u: goto L801A2B70;
            case 0x801A2B98u: goto L801A2B98;
            case 0x801A2BC0u: goto L801A2BC0;
            case 0x801A2BF8u: goto L801A2BF8;
            case 0x801A2C40u: goto L801A2C40;
            case 0x801A2774u: goto L801A2774;
            case 0x801A2C9Cu: goto L801A2C9C;
            case 0x801A2D00u: goto L801A2D00;
            case 0x801A2D60u: goto L801A2D60;
            case 0x801A2DACu: goto L801A2DAC;
            case 0x801A2DE0u: goto L801A2DE0;
            case 0x801A2DF0u: goto L801A2DF0;
            case 0x801A2EB0u: goto L801A2EB0;
            case 0x801A2EECu: goto L801A2EEC;
            case 0x801A2F0Cu: goto L801A2F0C;
            case 0x801A3608u: goto L801A3608;
            case 0x801A2F48u: goto L801A2F48;
            case 0x801A4188u: goto L801A4188;
            case 0x801A42ACu: goto L801A42AC;
            case 0x801A4330u: goto L801A4330;
            case 0x801A435Cu: goto L801A435C;
            case 0x801A43C8u: goto L801A43C8;
            case 0x801A4454u: goto L801A4454;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801A267C: ;
        c.V0 = mem.ReadU16((c.S2 + 0x30u)); 
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0002u;  }
            goto L801A26A4;
        }
        { c.V0 = 0u | 0x0002u;  }
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x38C4u), c.V0); 
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x19E7u;  }
        goto L801A26AC;
        L801A26A4: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x1720u;  }
        L801A26AC: ;
        c.RA = 0x801A26B4u; 
        SoTN_rbo6.RBO6_SetCutsceneScript(c, m);
        { c.S0 = c.V0 & 0x00FFu;  }
        if (c.S0 == 0u) {
            c.V1 = 0x00800000u; 
            goto L801A3608;
        }
        c.V1 = 0x00800000u; 
        { c.V1 = c.V1 | 0x2000u;  }
        c.V0 = mem.ReadU32((c.S2 + 0x34u)); 
        { c.A1 = 0u | 0x0001u;  }
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x38FCu), c.A1); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3FFCu), 0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x4000u), 0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3F4Cu), 0u); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        mem.WriteU32((c.S2 + 0x34u), c.V0); 
        c.A0 = 0x801B0000u; 
        c.A0 = mem.ReadU32((c.A0 + 0x3F88u)); 
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        c.V1 = 0x80040000u; 
        c.V1 = mem.ReadU32((c.V1 - 0x3660u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        if (c.V1 != 0u) {
            mem.WriteU32((c.S2 + 0x64u), c.A0); 
            goto L801A272C;
        }
        mem.WriteU32((c.S2 + 0x64u), c.A0); 
        c.V0 = 0x80090000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x7914u)); 
        if (c.V0 == 0u) {
            goto L801A2760;
        }
        L801A272C: ;
        c.A0 = mem.ReadU32((c.S2 + 0x64u)); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x384Cu)); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x4000u), c.A1); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3F4Cu), c.A1); 
        c.RA = 0x801A2750u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = mem.ReadU32((c.S2 + 0x34u)); 
        c.V1 = 0x00800000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s ^ _t;  }
        mem.WriteU32((c.S2 + 0x34u), c.V0); 
        L801A2760: ;
        c.S4 = 0x801B0000u; 
        { c.S4 = c.S4 + 0x3F66u;  }
        { c.S1 = c.S4 - 0x16u;  }
        { c.S0 = c.S4 - 0xCu;  }
        { c.S3 = c.S4 - 0x12u;  }
        L801A2774: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.S4); 
        if (c.V0 == 0u) {
            goto L801A2798;
        }
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3F4Cu)); 
        if (c.V0 == 0u) {
            goto L801A35A4;
        }
        L801A2798: ;
        c.V0 = mem.ReadU32(c.S1); 
        { c.V1 = c.V0 + 0x1u;  }
        mem.WriteU32(c.S1, c.V1); 
        c.A2 = mem.ReadU8(c.V0); 
        { c.V1 = c.A2 & 0xFFFFu;  }
        { c.V0 = c.V1 < 0x00000019u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801A2F88;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x3134u)); 
        switch (c.V0)
        {
            case 0x801A27D8u: goto L801A27D8;
            case 0x801A27E4u: goto L801A27E4;
            case 0x801A2890u: goto L801A2890;
            case 0x801A28BCu: goto L801A28BC;
            case 0x801A28ECu: goto L801A28EC;
            case 0x801A292Cu: goto L801A292C;
            case 0x801A2A64u: goto L801A2A64;
            case 0x801A2ADCu: goto L801A2ADC;
            case 0x801A2B70u: goto L801A2B70;
            case 0x801A2B98u: goto L801A2B98;
            case 0x801A2BC0u: goto L801A2BC0;
            case 0x801A2BF8u: goto L801A2BF8;
            case 0x801A2C40u: goto L801A2C40;
            case 0x801A2774u: goto L801A2774;
            case 0x801A2C9Cu: goto L801A2C9C;
            case 0x801A2D00u: goto L801A2D00;
            case 0x801A2D60u: goto L801A2D60;
            case 0x801A2DACu: goto L801A2DAC;
            case 0x801A2DE0u: goto L801A2DE0;
            case 0x801A2DF0u: goto L801A2DF0;
            case 0x801A2EB0u: goto L801A2EB0;
            case 0x801A2EECu: goto L801A2EEC;
            case 0x801A2F0Cu: goto L801A2F0C;
            case 0x801A3608u: goto L801A3608;
            case 0x801A2F48u: goto L801A2F48;
            case 0x801A4188u: goto L801A4188;
            case 0x801A42ACu: goto L801A42AC;
            case 0x801A4330u: goto L801A4330;
            case 0x801A435Cu: goto L801A435C;
            case 0x801A43C8u: goto L801A43C8;
            case 0x801A4454u: goto L801A4454;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801A27D8: ;
        { c.V0 = 0u | 0x0007u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A3608;
        L801A27E4: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3F4Cu)); 
        if (c.V0 != 0u) {
            goto L801A2774;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x8u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x2u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 != 0u) {
            mem.WriteU16(c.S0, (ushort)c.V1); 
            goto L801A281C;
        }
        mem.WriteU16(c.S0, (ushort)c.V1); 
        c.V0 = mem.ReadU16((c.S0 - 0x4u)); 
        { c.V0 = c.V0 + 0xCu;  }
        mem.WriteU16((c.S0 - 0x4u), (ushort)c.V0); 
        L801A281C: ;
        c.V0 = mem.ReadU16((c.S0 + 0x4u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x4u), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 5 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A2844;
        }
        mem.WriteU16((c.S0 + 0x4u), (ushort)0u); 
        L801A2844: ;
        c.RA = 0x801A284Cu; 
        SoTN_rbo6.RBO6_CutsceneUnk4(c, m);
        c.V1 = mem.ReadU16((c.S0 + 0x8u)); 
        { c.V0 = c.V1 & 0x0001u;  }
        if (c.V0 != 0u) {
            goto L801A2878;
        }
        c.V0 = (uint)(short)mem.ReadU16((c.S0 + 0x4u)); 
        { c.V0 = (int)c.V0 < 4 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = c.V1 | 0x0001u;  }
            goto L801A2774;
        }
        { c.V0 = c.V1 | 0x0001u;  }
        mem.WriteU16((c.S0 + 0x8u), (ushort)c.V0); 
        L801A2878: ;
        mem.WriteU16((c.S0 + 0x6u), (ushort)0u); 
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        mem.WriteU16((c.S2 + 0x2Eu), (ushort)0u); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A3608;
        L801A2890: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3F50u)); 
        { c.V1 = c.V0 + 0x1u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3F50u), c.V1); 
        c.V0 = mem.ReadU8(c.V0); 
        c.At = 0x801B0000u; 
        mem.WriteU8((c.At + 0x3F67u), (byte)c.V0); 
        goto L801A2774;
        L801A28BC: ;
        c.V1 = mem.ReadU32((c.S4 - 0x16u)); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32((c.S4 - 0x16u), c.V0); 
        c.V0 = mem.ReadU8(c.V1); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x3F4Cu)); 
        if (c.V1 != 0u) {
            mem.WriteU8(c.S4, (byte)c.V0); 
            goto L801A2774;
        }
        mem.WriteU8(c.S4, (byte)c.V0); 
        goto L801A3608;
        L801A28EC: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3F4Cu)); 
        if (c.V0 != 0u) {
            { c.V1 = 0u | 0x0008u;  }
            goto L801A2774;
        }
        { c.V1 = 0u | 0x0008u;  }
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x3F68u)); 
        c.S0 = 0u; 
        { c.S0 = c.S0 + 0x1u;  }
        L801A2910: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (int)c.S0 < 5 ? 1u : 0u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V1); 
        c.S1 = mem.ReadU32(c.S1); 
        if (c.V0 != 0u) {
            { c.S0 = c.S0 + 0x1u;  }
            goto L801A2910;
        }
        { c.S0 = c.S0 + 0x1u;  }
        goto L801A3608;
        L801A292C: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3F4Cu)); 
        if (c.V0 != 0u) {
            goto L801A2BAC;
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
        c.A0 = mem.ReadU8((c.At + 0x864u)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A1 = mem.ReadU8((c.At + 0x868u)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V1; c.At = _s + _t;  }
        c.V1 = mem.ReadU16((c.At + 0x86Cu)); 
        { c.V0 = 0u | 0x0090u;  }
        mem.WriteU16((c.S1 + 0x1Au), (ushort)c.V0); 
        { c.V0 = c.T2 & 0x0080u;  }
        if (c.V0 == 0u) {
            mem.WriteU16((c.S1 + 0xEu), (ushort)c.V1); 
            goto L801A29B0;
        }
        mem.WriteU16((c.S1 + 0xEu), (ushort)c.V1); 
        c.V1 = c.A0; 
        { c.V0 = c.V1 + 0x2Fu;  }
        goto L801A29B8;
        L801A29B0: ;
        c.V0 = c.A0; 
        { c.V1 = c.V0 + 0x2Fu;  }
        L801A29B8: ;
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
        c.V0 = mem.ReadU16((c.At + 0x874u)); 
        mem.WriteU16((c.S3 + 0x10u), (ushort)c.V0); 
        c.RA = 0x801A2A30u; 
        SoTN_rbo6.func_801A1BE0(c, m);
        c.RA = 0x801A2A38u; 
        SoTN_rbo6.RBO6_CutsceneUnk4(c, m);
        { c.A0 = c.S0 & 0xFFFFu;  }
        c.A1 = c.S2; 
        { c.V0 = 0u | 0x01FEu;  }
        mem.WriteU16((c.S1 + 0x26u), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x32u), (ushort)0u); 
        c.RA = 0x801A2A50u; 
        Dispatcher.Call(c, m, 0x801A1F08u);
        { c.V0 = 0u | 0x0006u;  }
        mem.WriteU16((c.S3 + 0xCu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0003u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A3608;
        L801A2A64: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3F4Cu)); 
        if (c.V0 != 0u) {
            { c.V1 = 0u | 0x0008u;  }
            goto L801A2774;
        }
        { c.V1 = 0u | 0x0008u;  }
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x3F68u)); 
        c.S0 = 0u; 
        { c.S0 = c.S0 + 0x1u;  }
        L801A2A88: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (int)c.S0 < 5 ? 1u : 0u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V1); 
        c.S1 = mem.ReadU32(c.S1); 
        if (c.V0 != 0u) {
            { c.S0 = c.S0 + 0x1u;  }
            goto L801A2A88;
        }
        { c.S0 = c.S0 + 0x1u;  }
        c.S0 = 0x801B0000u; 
        { c.S0 = c.S0 + 0x3F84u;  }
        c.A0 = mem.ReadU32(c.S0); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x384Cu)); 
        c.RA = 0x801A2ABCu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0xFFFFFFFFu; 
        mem.WriteU32(c.S0, c.V0); 
        { c.V0 = 0u | 0x0006u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x3F60u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0004u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A3608;
        L801A2ADC: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3F4Cu)); 
        if (c.V0 != 0u) {
            goto L801A2BAC;
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
        goto L801A3608;
        L801A2B70: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3F4Cu)); 
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0018u;  }
            goto L801A2774;
        }
        { c.V0 = 0u | 0x0018u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x3F60u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0006u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A3608;
        L801A2B98: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3F4Cu)); 
        if (c.V0 == 0u) {
            goto L801A2EB0;
        }
        L801A2BAC: ;
        c.V0 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 + 0x2u;  }
        mem.WriteU32(c.S1, c.V0); 
        goto L801A2774;
        L801A2BC0: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3F4Cu)); 
        if (c.V0 != 0u) {
            goto L801A2774;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3808u)); 
        c.RA = 0x801A2BE8u;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 == 0u) {
            goto L801A2C2C;
        }
        goto L801A2774;
        L801A2BF8: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3F4Cu)); 
        if (c.V0 != 0u) {
            goto L801A2774;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3808u)); 
        c.RA = 0x801A2C20u;
        Dispatcher.Call(c, m, c.V0);
        { c.V1 = 0u | 0x0001u;  }
        if (c.V0 != c.V1) {
            goto L801A2774;
        }
        L801A2C2C: ;
        c.V0 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        goto L801A3608;
        L801A2C40: ;
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
        c.RA = 0x801A2C94u; 
        SoTN_rbo6.func_801A208C(c, m);
        goto L801A2774;
        L801A2C9C: ;
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
        goto L801A2D04;
        L801A2D00: ;
        c.V1 = mem.ReadU32(c.S1); 
        L801A2D04: ;
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
        c.V1 = mem.ReadU32((c.V1 + 0x3F50u)); 
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
        goto L801A2774;
        L801A2D60: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3F50u)); 
        c.A0 = 0x801B0000u; 
        c.A0 = mem.ReadU32((c.A0 + 0x3FFCu)); 
        c.V1 = mem.ReadU8(c.V0); 
        { var _s = c.V1; var _t = c.A0; c.V0 = _t >> (int)(_s & 31u);  }
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A35B8;
        }
        { c.V0 = 0u | 0x0001u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = 0u; var _t = c.V0; c.V0 = ~(_s | _t);  }
        c.V1 = mem.ReadU32(c.S1); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s & _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3FFCu), c.V0); 
        { c.V1 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V1); 
        goto L801A2774;
        L801A2DAC: ;
        c.V1 = mem.ReadU32(c.S1); 
        c.A0 = 0x801B0000u; 
        c.A0 = mem.ReadU32((c.A0 + 0x3FFCu)); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V1 = mem.ReadU8(c.V1); 
        { c.V0 = 0u | 0x0001u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s | _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3FFCu), c.V0); 
        goto L801A2774;
        L801A2DE0: ;
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x3F8Cu), (ushort)0u); 
        goto L801A2774;
        L801A2DF0: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3F4Cu)); 
        if (c.V0 == 0u) {
            { c.A1 = 0u | 0x0001u;  }
            goto L801A2E18;
        }
        { c.A1 = 0u | 0x0001u;  }
        c.V0 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 + 0x5u;  }
        mem.WriteU32(c.S1, c.V0); 
        goto L801A2774;
        L801A2E18: ;
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
        c.A3 = mem.ReadU16((c.At + 0x870u)); 
        { c.V0 = 0u | 0x0100u;  }
        mem.WriteU32((c.SP + 0x10u), c.V0); 
        { c.V0 = 0u | 0x0030u;  }
        mem.WriteU32((c.SP + 0x14u), c.V0); 
        { c.V0 = 0u | 0x0048u;  }
        mem.WriteU32((c.SP + 0x18u), c.V0); 
        c.RA = 0x801A2EA8u; 
        SoTN_main.LoadTPage(c, m);
        goto L801A2774;
        L801A2EB0: ;
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
        c.RA = 0x801A2EE4u;
        Dispatcher.Call(c, m, c.V0);
        goto L801A2774;
        L801A2EEC: ;
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3FFCu), 0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3F4Cu), 0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x4000u), 0u); 
        goto L801A2774;
        L801A2F0C: ;
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V1 = mem.ReadU8(c.V1); 
        { c.V0 = 0u | 0x0001u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _t << (int)(_s & 31u);  }
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x3FFCu)); 
        { var _s = 0u; var _t = c.V0; c.V0 = ~(_s | _t);  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s & _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3FFCu), c.V0); 
        goto L801A2774;
        L801A2F48: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3F50u)); 
        c.V1 = mem.ReadU8(c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3FFCu)); 
        { var _s = c.V1; var _t = c.V0; c.V0 = _t >> (int)(_s & 31u);  }
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            goto L801A35B8;
        }
        c.V0 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        goto L801A2774;
        L801A2F88: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3F4Cu)); 
        if (c.V0 != 0u) {
            goto L801A2774;
        }
        c.V0 = mem.ReadU8((c.S1 + 0x17u)); 
        mem.WriteU8((c.S1 + 0x16u), (byte)c.V0); 
        { c.V0 = 0u | 0x0020u;  }
        if (c.V1 != c.V0) {
            { c.A0 = c.SP + 0x20u;  }
            goto L801A2FD0;
        }
        { c.A0 = c.SP + 0x20u;  }
        c.V1 = 0x801B0000u; 
        { c.V1 = c.V1 + 0x3F5Au;  }
        c.V0 = mem.ReadU16(c.V1); 
        { c.V0 = c.V0 + 0x2u;  }
        mem.WriteU16(c.V1, (ushort)c.V0); 
        goto L801A3608;
        L801A2FD0: ;
        { c.V0 = c.A2 & 0x000Fu;  }
        { c.V0 = c.V0 << 1;  }
        { c.V0 = c.V0 + 0x380u;  }
        mem.WriteU16((c.SP + 0x20u), (ushort)c.V0); 
        { c.V0 = c.A2 & 0x00F0u;  }
        { c.V0 = c.V0 >> 1;  }
        c.A1 = 0x801B0000u; 
        c.A1 = (uint)(short)mem.ReadU16((c.A1 + 0x3F5Au)); 
        c.V1 = 0x801B0000u; 
        c.V1 = (uint)(short)mem.ReadU16((c.V1 + 0x3F5Eu)); 
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
        c.RA = 0x801A302Cu; 
        if (!DialogueGlyphs.Draw(c, mem)) SoTN_main.MoveImage(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x3F5Au)); 
        { c.V0 = c.V0 + 0x2u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x3F5Au), (ushort)c.V0); 
        goto L801A3608;
        L801A304C: ;
        { c.A0 = 0u | 0x0002u;  }
        c.RA = 0x801A3054u; 
        SoTN_rbo6.RBO6_ScaleCutsceneAvatar(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = (uint)(short)mem.ReadU16((c.V0 + 0x3F60u)); 
        { c.V0 = (int)c.V0 < 6 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A3608;
        }
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A3608;
        L801A3080: ;
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x3F7Cu)); 
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
        c.V0 = mem.ReadU16((c.V0 + 0x3F60u)); 
        { c.V0 = c.V0 - 0x1u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x3F60u), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A3608;
        }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x3F84u)); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.S1 = _s + _t;  }
        if (c.S1 == 0u) {
            goto L801A3608;
        }
        mem.WriteU16((c.S1 + 0x32u), (ushort)0u); 
        L801A312C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.S1 = mem.ReadU32(c.S1); 
        if (c.S1 == 0u) {
            goto L801A3608;
        }
        mem.WriteU16((c.S1 + 0x32u), (ushort)0u); 
        goto L801A312C;
        L801A3144: ;
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x3F7Cu)); 
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
        c.V1 = (uint)(short)mem.ReadU16((c.V1 + 0x3F54u)); 
        c.V0 = (uint)(short)mem.ReadU16((c.S1 + 0x14u)); 
        c.A0 = c.V1; 
        { c.V1 = c.V1 - 0x2u;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = (int)_s < (int)_t ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A31BC;
        }
        { c.V0 = c.A0 - 0x3u;  }
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x14u), (ushort)c.V0); 
        L801A31BC: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x3F60u)); 
        { c.V0 = c.V0 - 0x1u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x3F60u), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        goto L801A34B4;
        L801A31DC: ;
        c.V1 = mem.ReadU16((c.S2 + 0x2Eu)); 
        { c.A2 = 0u | 0x0001u;  }
        if (c.V1 == c.A2) {
            { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
            goto L801A3300;
        }
        { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A3204;
        }
        if (c.V1 == 0u) {
            { c.A0 = 0u | 0x0002u;  }
            goto L801A3218;
        }
        { c.A0 = 0u | 0x0002u;  }
        goto L801A3608;
        L801A3204: ;
        { c.V0 = 0u | 0x0002u;  }
        if (c.V1 == c.V0) {
            goto L801A33FC;
        }
        goto L801A3608;
        L801A3218: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x4000u), c.A2); 
        { c.A1 = 0u | 0x0048u;  }
        c.RA = 0x801A3230u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.V1 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.V1 != c.V0) {
            { c.V0 = c.V1 << 1;  }
            goto L801A324C;
        }
        { c.V0 = c.V1 << 1;  }
        c.A0 = c.S2; 
        goto L801A3600;
        L801A324C: ;
        c.A2 = 0x801B0000u; 
        { c.A2 = c.A2 + 0x3F80u;  }
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
            goto L801A33E8;
        }
        c.A0 = 0u; 
        { c.A1 = 0u | 0x007Fu;  }
        { c.V1 = 0u | 0x00F7u;  }
        { c.T1 = c.A2 - 0x28u;  }
        { c.T0 = 0u | 0x01FEu;  }
        c.A3 = 0x80180000u; 
        { c.A3 = c.A3 + 0x87Cu;  }
        { c.A2 = 0u | 0x0F70u;  }
        L801A3298: ;
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
            goto L801A3298;
        }
        { c.A0 = c.A0 + 0x1u;  }
        goto L801A33E8;
        L801A3300: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3F80u)); 
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
            goto L801A3394;
        }
        c.A0 = 0u; 
        { c.A1 = 0u | 0x0004u;  }
        L801A3338: ;
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
            goto L801A3364;
        }
        mem.WriteU16((c.S1 + 0x20u), (ushort)c.V0); 
        { c.V1 = c.V1 + 0xFu;  }
        L801A3364: ;
        { c.V0 = (uint)((int)c.V1 >> 4);  }
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.V0); 
        { c.V0 = (int)c.V0 < 5 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A3380;
        }
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.A1); 
        goto L801A3384;
        L801A3380: ;
        { c.A0 = 0u | 0x0001u;  }
        L801A3384: ;
        c.S1 = mem.ReadU32(c.S1); 
        if (c.S1 != 0u) {
            goto L801A3338;
        }
        L801A3394: ;
        { c.V0 = c.A0 << 16;  }
        if (c.V0 != 0u) {
            goto L801A3608;
        }
        c.S0 = 0x801B0000u; 
        { c.S0 = c.S0 + 0x3F80u;  }
        c.A0 = mem.ReadU32(c.S0); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x384Cu)); 
        c.RA = 0x801A33C0u;
        Dispatcher.Call(c, m, c.V0);
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x3F7Cu)); 
        c.V0 = 0xFFFFFFFFu; 
        mem.WriteU32(c.S0, c.V0); 
        c.S1 = mem.ReadU32(c.S1); 
        { c.V0 = 0u | 0x0011u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0); 
        c.S1 = mem.ReadU32(c.S1); 
        { c.V0 = 0u | 0x0051u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0); 
        L801A33E8: ;
        c.V0 = mem.ReadU16((c.S2 + 0x2Eu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S2 + 0x2Eu), (ushort)c.V0); 
        goto L801A3608;
        L801A33FC: ;
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x3F7Cu)); 
        c.S1 = mem.ReadU32(c.S1); 
        c.V0 = mem.ReadU8((c.S1 + 0x4u)); 
        { c.V0 = c.V0 < 0x00000010u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            c.A0 = 0u; 
            goto L801A3438;
        }
        c.A0 = 0u; 
        mem.WriteU8((c.S1 + 0x28u), (byte)0u); 
        mem.WriteU8((c.S1 + 0x1Cu), (byte)0u); 
        mem.WriteU8((c.S1 + 0x10u), (byte)0u); 
        mem.WriteU8((c.S1 + 0x4u), (byte)0u); 
        goto L801A3454;
        L801A3438: ;
        c.V0 = mem.ReadU8((c.S1 + 0x28u)); 
        { c.A0 = 0u | 0x0001u;  }
        { c.V0 = c.V0 - 0x10u;  }
        mem.WriteU8((c.S1 + 0x28u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x1Cu), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x10u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x4u), (byte)c.V0); 
        L801A3454: ;
        c.V0 = mem.ReadU8((c.S1 + 0x6u)); 
        { c.V0 = c.V0 < 0x0000007Fu ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x007Fu;  }
            goto L801A3474;
        }
        { c.V0 = 0u | 0x007Fu;  }
        c.V0 = mem.ReadU8((c.S1 + 0x12u)); 
        { c.A0 = 0u | 0x0001u;  }
        { c.V0 = c.V0 + 0x8u;  }
        L801A3474: ;
        mem.WriteU8((c.S1 + 0x12u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x6u), (byte)c.V0); 
        c.S1 = mem.ReadU32(c.S1); 
        c.V0 = mem.ReadU8((c.S1 + 0x4u)); 
        { c.V0 = c.V0 < 0x00000010u ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0010u;  }
            goto L801A34A4;
        }
        { c.V0 = 0u | 0x0010u;  }
        c.V0 = mem.ReadU8((c.S1 + 0x6u)); 
        { c.A0 = 0u | 0x0001u;  }
        { c.V0 = c.V0 - 0xFu;  }
        L801A34A4: ;
        mem.WriteU8((c.S1 + 0x6u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x5u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x4u), (byte)c.V0); 
        { c.V0 = c.A0 << 16;  }
        L801A34B4: ;
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A3608;
        }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A3608;
        L801A34C4: ;
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x3F7Cu)); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x3F60u)); 
        c.S1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 - 0x1u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x3F60u), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        { c.V1 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V1 < 12 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A3530;
        }
        c.S1 = mem.ReadU32(c.S1); 
        c.V1 = mem.ReadU8((c.S1 + 0xCu)); 
        { c.V0 = c.V1 + 0xECu;  }
        mem.WriteU8((c.S1 + 0xCu), (byte)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x3F60u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 - 0x15u;  }
            goto L801A3608;
        }
        { c.V0 = c.V1 - 0x15u;  }
        mem.WriteU8((c.S1 + 0xCu), (byte)c.V0); 
        goto L801A3608;
        L801A3530: ;
        if (c.V1 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A3548;
        }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0); 
        goto L801A355C;
        L801A3548: ;
        c.V0 = mem.ReadU16((c.S1 + 0x2Eu)); 
        { c.V0 = c.V0 - 0x6u;  }
        mem.WriteU16((c.S1 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x22u), (ushort)c.V0); 
        L801A355C: ;
        c.S1 = mem.ReadU32(c.S1); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0); 
        goto L801A3608;
        L801A356C: ;
        c.V0 = mem.ReadU16((c.S2 + 0x30u)); 
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x38FCu), 0u); 
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A35D4;
        }
        { c.V0 = 0u | 0x0001u;  }
        c.A0 = 0x80040000u; 
        { c.A0 = c.A0 - 0x34FCu;  }
        c.At = 0x80040000u; 
        mem.WriteU8((c.At - 0x4133u), (byte)c.V0); 
        c.V0 = mem.ReadU32(c.A0); 
        c.V1 = 0x00400000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        mem.WriteU32(c.A0, c.V0); 
        goto L801A35EC;
        L801A35A4: ;
        c.V0 = mem.ReadU8(c.S4); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU8(c.S4, (byte)c.V0); 
        goto L801A3608;
        L801A35B8: ;
        c.V1 = 0x801B0000u; 
        { c.V1 = c.V1 + 0x3F50u;  }
        c.V0 = mem.ReadU32(c.V1); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU32(c.V1, c.V0); 
        goto L801A3608;
        L801A35D4: ;
        c.V0 = 0x80040000u; 
        { c.V0 = c.V0 - 0x34FCu;  }
        c.V1 = mem.ReadU32(c.V0); 
        c.A0 = 0x00200000u; 
        { var _s = c.V1; var _t = c.A0; c.V1 = _s | _t;  }
        mem.WriteU32(c.V0, c.V1); 
        L801A35EC: ;
        c.A0 = c.S2; 
        c.At = 0x80090000u; 
        mem.WriteU32((c.At + 0x7420u), 0u); 
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x38C4u), 0u); 
        L801A3600: ;
        c.RA = 0x801A3608u; 
        SoTN_rbo6.DestroyEntity_rbo6(c, m);
        L801A3608: ;
        c.RA = mem.ReadU32((c.SP + 0x44u)); 
        c.S4 = mem.ReadU32((c.SP + 0x40u)); 
        c.S3 = mem.ReadU32((c.SP + 0x3Cu)); 
        c.S2 = mem.ReadU32((c.SP + 0x38u)); 
        c.S1 = mem.ReadU32((c.SP + 0x34u)); 
        c.S0 = mem.ReadU32((c.SP + 0x30u)); 
        { c.SP = c.SP + 0x48u;  }
        return;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x38C4u)); 
        { c.SP = c.SP - 0x18u;  }
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.S0 = c.A0; 
        if (c.V0 == 0u) {
            mem.WriteU32((c.SP + 0x14u), c.RA); 
            goto L801A3650;
        }
        mem.WriteU32((c.SP + 0x14u), c.RA); 
        c.RA = 0x801A3650u; 
        SoTN_rbo6.func_801A2578(c, m);
        L801A3650: ;
        c.V0 = mem.ReadU16((c.S0 + 0x26u)); 
        if (c.V0 == 0u) {
            goto L801A3668;
        }
        c.A0 = c.S0; 
        c.RA = 0x801A3668u; 
        SoTN_rbo6.func_801A2578(c, m);
        L801A3668: ;
        c.RA = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x18u;  }
        return;
        { c.SP = c.SP - 0x18u;  }
        mem.WriteU32((c.SP + 0x14u), c.RA); 
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.V1 = mem.ReadU16((c.A0 + 0x2Cu)); 
        { c.S0 = 0u | 0x0001u;  }
        if (c.V1 == c.S0) {
            { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
            goto L801A371C;
        }
        { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A36B0;
        }
        if (c.V1 == 0u) {
            goto L801A36C4;
        }
        goto L801A37A0;
        L801A36B0: ;
        { c.V0 = 0u | 0x0002u;  }
        if (c.V1 == c.V0) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A3780;
        }
        { c.V0 = 0u | 0x0001u;  }
        goto L801A37A0;
        L801A36C4: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x4F8u;  }
        c.RA = 0x801A36D4u; 
        SoTN_rbo6.RBO6_InitializeEntity(c, m);
        c.V1 = 0x80090000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x7C0Cu)); 
        { c.V0 = 0u | 0x0019u;  }
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x3748u), 0u); 
        c.At = 0x80090000u; 
        mem.WriteU32((c.At + 0x7400u), c.S0); 
        c.At = 0x80070000u; 
        mem.WriteU16((c.At + 0x33ECu), (ushort)0u); 
        if (c.V1 == c.V0) {
            { c.V0 = 0u | 0x00BFu;  }
            goto L801A3708;
        }
        { c.V0 = 0u | 0x00BFu;  }
        c.At = 0x80070000u; 
        mem.WriteU16((c.At + 0x342Eu), (ushort)c.V0); 
        L801A3708: ;
        { c.V0 = 0u | 0x0070u;  }
        c.At = 0x80070000u; 
        mem.WriteU16((c.At + 0x33DAu), (ushort)c.V0); 
        goto L801A3768;
        L801A371C: ;
        c.V1 = 0x80090000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x7C0Cu)); 
        { c.V0 = 0u | 0x0019u;  }
        if (c.V1 == c.V0) {
            { c.V0 = 0u | 0x00BFu;  }
            goto L801A3738;
        }
        { c.V0 = 0u | 0x00BFu;  }
        c.At = 0x80070000u; 
        mem.WriteU16((c.At + 0x342Eu), (ushort)c.V0); 
        L801A3738: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3FFCu)); 
        { c.V1 = 0u | 0x0070u;  }
        c.At = 0x80070000u; 
        mem.WriteU16((c.At + 0x33DAu), (ushort)c.V1); 
        { c.V0 = c.V0 & 0x0002u;  }
        if (c.V0 == 0u) {
            goto L801A3768;
        }
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.A0 + 0x2Cu), (ushort)c.V0); 
        L801A3768: ;
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EF4u), 0u); 
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EFCu), c.S0); 
        goto L801A37A0;
        L801A3780: ;
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x3748u), c.S0); 
        c.At = 0x80090000u; 
        mem.WriteU32((c.At + 0x7400u), 0u); 
        c.At = 0x80070000u; 
        mem.WriteU8((c.At + 0x3510u), (byte)c.V0); 
        c.RA = 0x801A37A0u; 
        SoTN_rbo6.DestroyEntity_rbo6(c, m);
        L801A37A0: ;
        c.RA = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x18u;  }
        return;
        { c.SP = c.SP - 0x20u;  }
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        c.S1 = c.A0; 
        mem.WriteU32((c.SP + 0x18u), c.RA); 
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.V1 = mem.ReadU16((c.S1 + 0x2Cu)); 
        { c.V0 = 0u | 0x0001u;  }
        if (c.V1 == c.V0) {
            { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
            goto L801A38E0;
        }
        { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A37F0;
        }
        if (c.V1 == 0u) {
            { c.A0 = 0u | 0x0003u;  }
            goto L801A3804;
        }
        { c.A0 = 0u | 0x0003u;  }
        goto L801A3984;
        L801A37F0: ;
        { c.V0 = 0u | 0x0002u;  }
        if (c.V1 == c.V0) {
            goto L801A38F4;
        }
        goto L801A3984;
        L801A3804: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0001u;  }
        c.RA = 0x801A3818u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.S0 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.S0 == c.V0) {
            goto L801A3984;
        }
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x4F8u;  }
        c.RA = 0x801A383Cu; 
        SoTN_rbo6.RBO6_InitializeEntity(c, m);
        { c.V0 = c.S0 << 1;  }
        { var _s = c.V0; var _t = c.S0; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.S0; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A0 = _s + _t;  }
        c.V0 = mem.ReadU32((c.S1 + 0x34u)); 
        c.V1 = 0x00800000u; 
        mem.WriteU32((c.S1 + 0x64u), c.S0); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        mem.WriteU32((c.S1 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0100u;  }
        mem.WriteU16((c.A0 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.A0 + 0x14u), (ushort)c.V0); 
        { c.V0 = 0u | 0x00F0u;  }
        mem.WriteU16((c.A0 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.A0 + 0x22u), (ushort)c.V0); 
        { c.V0 = 0u | 0x00FFu;  }
        mem.WriteU8((c.A0 + 0x2Au), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x1Eu), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x12u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x6u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x29u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x1Du), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x11u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x5u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x28u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x1Cu), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x10u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x4u), (byte)c.V0); 
        { c.V0 = 0u | 0x01F8u;  }
        mem.WriteU16((c.A0 + 0x26u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0055u;  }
        mem.WriteU16((c.A0 + 0x20u), (ushort)0u); 
        mem.WriteU16((c.A0 + 0x8u), (ushort)0u); 
        mem.WriteU16((c.A0 + 0x16u), (ushort)0u); 
        mem.WriteU16((c.A0 + 0xAu), (ushort)0u); 
        mem.WriteU16((c.A0 + 0x32u), (ushort)c.V0); 
        goto L801A3984;
        L801A38E0: ;
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        goto L801A3984;
        L801A38F4: ;
        c.V1 = mem.ReadU32((c.S1 + 0x64u)); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A0 = _s + _t;  }
        c.V0 = mem.ReadU8((c.A0 + 0x2Au)); 
        { c.V0 = c.V0 - 0x4u;  }
        mem.WriteU8((c.A0 + 0x4u), (byte)c.V0); 
        c.V1 = mem.ReadU8((c.A0 + 0x4u)); 
        mem.WriteU8((c.A0 + 0x2Au), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x1Eu), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x12u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x6u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x29u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x1Du), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x11u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x5u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x28u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x1Cu), (byte)c.V0); 
        { c.V1 = c.V1 < 0x00000004u ? 1u : 0u;  }
        if (c.V1 == 0u) {
            mem.WriteU8((c.A0 + 0x10u), (byte)c.V0); 
            goto L801A3984;
        }
        mem.WriteU8((c.A0 + 0x10u), (byte)c.V0); 
        c.A0 = c.S1; 
        c.RA = 0x801A396Cu; 
        SoTN_rbo6.DestroyEntity_rbo6(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3FFCu)); 
        { c.V0 = c.V0 | 0x0001u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3FFCu), c.V0); 
        L801A3984: ;
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        { c.SP = c.SP - 0x20u;  }
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        c.S1 = c.A0; 
        mem.WriteU32((c.SP + 0x18u), c.RA); 
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.V1 = mem.ReadU16((c.S1 + 0x2Cu)); 
        { c.V0 = 0u | 0x0001u;  }
        if (c.V1 == c.V0) {
            { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
            goto L801A3AC0;
        }
        { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A39D8;
        }
        if (c.V1 == 0u) {
            { c.A0 = 0u | 0x0003u;  }
            goto L801A39EC;
        }
        { c.A0 = 0u | 0x0003u;  }
        goto L801A3BC8;
        L801A39D8: ;
        { c.V0 = 0u | 0x0002u;  }
        if (c.V1 == c.V0) {
            goto L801A3B50;
        }
        goto L801A3BC8;
        L801A39EC: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0001u;  }
        c.RA = 0x801A3A00u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.S0 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.S0 == c.V0) {
            goto L801A3BC8;
        }
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x4F8u;  }
        c.RA = 0x801A3A24u; 
        SoTN_rbo6.RBO6_InitializeEntity(c, m);
        { c.V0 = c.S0 << 1;  }
        { var _s = c.V0; var _t = c.S0; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.S0; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A0 = _s + _t;  }
        c.V0 = mem.ReadU32((c.S1 + 0x34u)); 
        c.V1 = 0x00800000u; 
        mem.WriteU32((c.S1 + 0x64u), c.S0); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        mem.WriteU32((c.S1 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0100u;  }
        mem.WriteU16((c.A0 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.A0 + 0x14u), (ushort)c.V0); 
        { c.V0 = 0u | 0x00F0u;  }
        mem.WriteU16((c.A0 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.A0 + 0x22u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0080u;  }
        mem.WriteU16((c.A0 + 0x20u), (ushort)0u); 
        mem.WriteU16((c.A0 + 0x8u), (ushort)0u); 
        mem.WriteU16((c.A0 + 0x16u), (ushort)0u); 
        mem.WriteU16((c.A0 + 0xAu), (ushort)0u); 
        mem.WriteU8((c.A0 + 0x2Au), (byte)0u); 
        mem.WriteU8((c.A0 + 0x1Eu), (byte)0u); 
        mem.WriteU8((c.A0 + 0x12u), (byte)0u); 
        mem.WriteU8((c.A0 + 0x6u), (byte)0u); 
        mem.WriteU8((c.A0 + 0x29u), (byte)0u); 
        mem.WriteU8((c.A0 + 0x1Du), (byte)0u); 
        mem.WriteU8((c.A0 + 0x11u), (byte)0u); 
        mem.WriteU8((c.A0 + 0x5u), (byte)0u); 
        mem.WriteU8((c.A0 + 0x28u), (byte)0u); 
        mem.WriteU8((c.A0 + 0x1Cu), (byte)0u); 
        mem.WriteU8((c.A0 + 0x10u), (byte)0u); 
        mem.WriteU8((c.A0 + 0x4u), (byte)0u); 
        mem.WriteU16((c.A0 + 0x26u), (ushort)c.V0); 
        mem.WriteU16((c.A0 + 0x32u), (ushort)0u); 
        goto L801A3BC8;
        L801A3AC0: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3FFCu)); 
        { c.V0 = c.V0 & 0x0100u;  }
        if (c.V0 == 0u) {
            goto L801A3BC8;
        }
        c.V1 = mem.ReadU32((c.S1 + 0x64u)); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A0 = _s + _t;  }
        { c.V0 = 0u | 0x00FFu;  }
        mem.WriteU8((c.A0 + 0x2Au), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x1Eu), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x12u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x6u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x29u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x1Du), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x11u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x5u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x28u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x1Cu), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x10u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x4u), (byte)c.V0); 
        { c.V0 = 0u | 0x0055u;  }
        mem.WriteU16((c.A0 + 0x32u), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        goto L801A3BC8;
        L801A3B50: ;
        c.V1 = mem.ReadU32((c.S1 + 0x64u)); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A0 = _s + _t;  }
        c.V0 = mem.ReadU8((c.A0 + 0x2Au)); 
        { c.V0 = c.V0 - 0x4u;  }
        mem.WriteU8((c.A0 + 0x4u), (byte)c.V0); 
        c.V1 = mem.ReadU8((c.A0 + 0x4u)); 
        mem.WriteU8((c.A0 + 0x2Au), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x1Eu), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x12u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x6u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x29u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x1Du), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x11u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x5u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x28u), (byte)c.V0); 
        mem.WriteU8((c.A0 + 0x1Cu), (byte)c.V0); 
        { c.V1 = c.V1 < 0x00000004u ? 1u : 0u;  }
        if (c.V1 == 0u) {
            mem.WriteU8((c.A0 + 0x10u), (byte)c.V0); 
            goto L801A3BC8;
        }
        mem.WriteU8((c.A0 + 0x10u), (byte)c.V0); 
        c.A0 = c.S1; 
        c.RA = 0x801A3BC8u; 
        SoTN_rbo6.DestroyEntity_rbo6(c, m);
        L801A3BC8: ;
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        { c.SP = c.SP - 0x20u;  }
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        c.S1 = c.A0; 
        mem.WriteU32((c.SP + 0x18u), c.RA); 
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.V1 = mem.ReadU16((c.S1 + 0x2Cu)); 
        { c.V0 = 0u | 0x0001u;  }
        if (c.V1 == c.V0) {
            { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
            goto L801A3D1C;
        }
        { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A3C1C;
        }
        if (c.V1 == 0u) {
            { c.A0 = 0u | 0x0003u;  }
            goto L801A3C38;
        }
        { c.A0 = 0u | 0x0003u;  }
        goto L801A4010;
        L801A3C1C: ;
        { c.V0 = 0u | 0x0002u;  }
        if (c.V1 == c.V0) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801A3DEC;
        }
        { c.V0 = 0u | 0x0003u;  }
        if (c.V1 == c.V0) {
            goto L801A3EEC;
        }
        goto L801A4010;
        L801A3C38: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0001u;  }
        c.RA = 0x801A3C4Cu;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.S0 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.S0 == c.V0) {
            goto L801A4010;
        }
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x4F8u;  }
        c.RA = 0x801A3C70u; 
        SoTN_rbo6.RBO6_InitializeEntity(c, m);
        { c.V0 = c.S0 << 1;  }
        { var _s = c.V0; var _t = c.S0; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.S0; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A1 = _s + _t;  }
        c.V0 = mem.ReadU32((c.S1 + 0x34u)); 
        c.V1 = 0x00800000u; 
        mem.WriteU32((c.S1 + 0x64u), c.S0); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        mem.WriteU32((c.S1 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0060u;  }
        mem.WriteU16((c.A1 + 0x20u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x8u), (ushort)c.V0); 
        { c.V0 = 0u | 0x00A0u;  }
        mem.WriteU16((c.A1 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x14u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0040u;  }
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0); 
        { c.V0 = 0u | 0x00C0u;  }
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V0); 
        { c.V0 = 0u | 0x00FFu;  }
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
        { c.V0 = 0u | 0x0055u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0082u;  }
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0); 
        goto L801A4010;
        L801A3D1C: ;
        c.V1 = mem.ReadU32((c.S1 + 0x64u)); 
        c.A0 = 0x80080000u; 
        { c.A0 = c.A0 + 0x6FECu;  }
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x3FFCu)); 
        { c.V1 = c.V1 & 0x0004u;  }
        if (c.V1 == 0u) {
            { var _s = c.V0; var _t = c.A0; c.A1 = _s + _t;  }
            goto L801A4010;
        }
        { var _s = c.V0; var _t = c.A0; c.A1 = _s + _t;  }
        c.V0 = mem.ReadU8((c.A1 + 0x2Au)); 
        { c.V0 = c.V0 - 0x1u;  }
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
        if (c.V1 != 0u) {
            mem.WriteU8((c.A1 + 0x10u), (byte)c.V0); 
            goto L801A4010;
        }
        mem.WriteU8((c.A1 + 0x10u), (byte)c.V0); 
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
        c.V1 = mem.ReadU16((c.S1 + 0x2Cu)); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3FFCu)); 
        { c.V1 = c.V1 + 0x1u;  }
        { c.V0 = c.V0 | 0x0200u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3FFCu), c.V0); 
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V1); 
        goto L801A4010;
        L801A3DEC: ;
        c.V1 = mem.ReadU32((c.S1 + 0x64u)); 
        c.A0 = 0x80080000u; 
        { c.A0 = c.A0 + 0x6FECu;  }
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x3FFCu)); 
        { c.V1 = c.V1 & 0x0080u;  }
        if (c.V1 == 0u) {
            { var _s = c.V0; var _t = c.A0; c.A1 = _s + _t;  }
            goto L801A4010;
        }
        { var _s = c.V0; var _t = c.A0; c.A1 = _s + _t;  }
        c.V0 = mem.ReadU8((c.A1 + 0x2Au)); 
        mem.WriteU8((c.A1 + 0x4u), (byte)c.V0); 
        c.A0 = mem.ReadU8((c.A1 + 0x4u)); 
        { c.V1 = c.V0 + 0x1u;  }
        mem.WriteU8((c.A1 + 0x2Au), (byte)c.V1); 
        mem.WriteU8((c.A1 + 0x1Eu), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x12u), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x6u), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x29u), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x11u), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x5u), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x28u), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x1Cu), (byte)c.V0); 
        { c.A0 = c.A0 < 0x000000C1u ? 1u : 0u;  }
        if (c.A0 != 0u) {
            mem.WriteU8((c.A1 + 0x10u), (byte)c.V0); 
            goto L801A3E84;
        }
        mem.WriteU8((c.A1 + 0x10u), (byte)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3FFCu)); 
        { c.V0 = c.V0 | 0x0100u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3FFCu), c.V0); 
        L801A3E84: ;
        c.V0 = mem.ReadU8((c.A1 + 0x4u)); 
        { c.V0 = c.V0 < 0x000000FEu ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.A0 = 0u | 0x0003u;  }
            goto L801A4010;
        }
        { c.A0 = 0u | 0x0003u;  }
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
        mem.WriteU16((c.A1 + 0x32u), (ushort)0u); 
        c.RA = 0x801A3ED0u; 
        Dispatcher.Call(c, m, 0x801A9514u);
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3824u)); 
        { c.A0 = 0u | 0x062Fu;  }
        c.RA = 0x801A3EE4u;
        Dispatcher.Call(c, m, c.V0);
        goto L801A4010;
        L801A3EEC: ;
        c.V1 = mem.ReadU32((c.S1 + 0x64u)); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A1 = _s + _t;  }
        { c.V0 = 0u | 0x0003u;  }
        mem.WriteU8((c.A1 + 0x7u), (byte)c.V0); 
        { c.V0 = 0u | 0x0100u;  }
        mem.WriteU16((c.A1 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x14u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0010u;  }
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0); 
        { c.V0 = 0u | 0x00F0u;  }
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V0); 
        { c.V0 = 0u | 0x007Fu;  }
        mem.WriteU16((c.A1 + 0x20u), (ushort)0u); 
        mem.WriteU16((c.A1 + 0x8u), (ushort)0u); 
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x32u), (ushort)0u); 
        c.V0 = mem.ReadU16((c.S1 + 0x52u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0080u;  }
            goto L801A3F9C;
        }
        { c.V0 = 0u | 0x0080u;  }
        mem.WriteU8((c.A1 + 0x4u), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x5u), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x6u), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x10u), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x11u), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x12u), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x1Cu), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x1Eu), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x28u), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x29u), (byte)c.V0); 
        mem.WriteU8((c.A1 + 0x2Au), (byte)c.V0); 
        goto L801A3FCC;
        L801A3F9C: ;
        mem.WriteU8((c.A1 + 0x4u), (byte)0u); 
        mem.WriteU8((c.A1 + 0x5u), (byte)0u); 
        mem.WriteU8((c.A1 + 0x6u), (byte)0u); 
        mem.WriteU8((c.A1 + 0x10u), (byte)0u); 
        mem.WriteU8((c.A1 + 0x11u), (byte)0u); 
        mem.WriteU8((c.A1 + 0x12u), (byte)0u); 
        mem.WriteU8((c.A1 + 0x1Cu), (byte)0u); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)0u); 
        mem.WriteU8((c.A1 + 0x1Eu), (byte)0u); 
        mem.WriteU8((c.A1 + 0x28u), (byte)0u); 
        mem.WriteU8((c.A1 + 0x29u), (byte)0u); 
        mem.WriteU8((c.A1 + 0x2Au), (byte)0u); 
        L801A3FCC: ;
        c.V0 = mem.ReadU16((c.S1 + 0x52u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x52u), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 9 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A4010;
        }
        c.A0 = c.S1; 
        c.RA = 0x801A3FF8u; 
        SoTN_rbo6.DestroyEntity_rbo6(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3FFCu)); 
        { c.V0 = c.V0 | 0x0020u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x3FFCu), c.V0); 
        L801A4010: ;
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        { c.SP = c.SP - 0x18u;  }
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.S0 = c.A0; 
        mem.WriteU32((c.SP + 0x14u), c.RA); 
        c.V1 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = 0u | 0x0001u;  }
        if (c.V1 == c.V0) {
            { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
            goto L801A40C4;
        }
        { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A4060;
        }
        if (c.V1 == 0u) {
            goto L801A4074;
        }
        goto L801A4134;
        L801A4060: ;
        { c.V0 = 0u | 0x0002u;  }
        if (c.V1 == c.V0) {
            goto L801A40F0;
        }
        goto L801A4134;
        L801A4074: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x4F8u;  }
        c.RA = 0x801A4084u; 
        SoTN_rbo6.RBO6_InitializeEntity(c, m);
        c.V0 = 0xFFFF8001u; 
        mem.WriteU16((c.S0 + 0x54u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0200u;  }
        mem.WriteU16((c.S0 + 0x16u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0048u;  }
        mem.WriteU16((c.S0 + 0x5Au), (ushort)c.V0); 
        { c.V0 = 0u | 0x0035u;  }
        mem.WriteU16((c.S0 + 0x56u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0003u;  }
        mem.WriteU8((c.S0 + 0x19u), (byte)c.V0); 
        { c.V0 = 0u | 0x0100u;  }
        mem.WriteU16((c.S0 + 0x1Au), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x1Cu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0081u;  }
        mem.WriteU16((c.S0 + 0x24u), (ushort)c.V0); 
        goto L801A4134;
        L801A40C4: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3FFCu)); 
        { c.V0 = c.V0 & 0x0080u;  }
        if (c.V0 == 0u) {
            { c.V1 = 0u | 0x8000u;  }
            goto L801A4134;
        }
        { c.V1 = 0u | 0x8000u;  }
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        mem.WriteU32((c.S0 + 0xCu), c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        goto L801A4134;
        L801A40F0: ;
        c.V0 = mem.ReadU32((c.S0 + 0xCu)); 
        { c.V0 = c.V0 - 0x180u;  }
        mem.WriteU32((c.S0 + 0xCu), c.V0); 
        c.RA = 0x801A4104u; 
        SoTN_rbo6.func_801A8C88(c, m);
        c.V1 = mem.ReadU16((c.S0 + 0x1Au)); 
        c.V0 = mem.ReadU16((c.S0 + 0x1Cu)); 
        c.A0 = 0x801B0000u; 
        c.A0 = mem.ReadU32((c.A0 + 0x3FFCu)); 
        { c.V1 = c.V1 - 0x1u;  }
        { c.V0 = c.V0 - 0x1u;  }
        { c.A0 = c.A0 & 0x0100u;  }
        mem.WriteU16((c.S0 + 0x1Au), (ushort)c.V1); 
        if (c.A0 == 0u) {
            mem.WriteU16((c.S0 + 0x1Cu), (ushort)c.V0); 
            goto L801A4134;
        }
        mem.WriteU16((c.S0 + 0x1Cu), (ushort)c.V0); 
        c.A0 = c.S0; 
        c.RA = 0x801A4134u; 
        SoTN_rbo6.DestroyEntity_rbo6(c, m);
        L801A4134: ;
        c.RA = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x18u;  }
        return;
        { c.SP = c.SP - 0x20u;  }
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        c.S1 = c.A0; 
        mem.WriteU32((c.SP + 0x18u), c.RA); 
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.V1 = mem.ReadU16((c.S1 + 0x2Cu)); 
        { c.V0 = c.V1 < 0x00000006u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801A4518;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x30D0u)); 
        switch (c.V0)
        {
            case 0x801A4188u: goto L801A4188;
            case 0x801A42ACu: goto L801A42AC;
            case 0x801A4330u: goto L801A4330;
            case 0x801A435Cu: goto L801A435C;
            case 0x801A43C8u: goto L801A43C8;
            case 0x801A4454u: goto L801A4454;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801A4188: ;
        { c.A0 = 0u | 0x0003u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0001u;  }
        c.RA = 0x801A41A0u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.S0 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.S0 == c.V0) {
            goto L801A4298;
        }
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x4F8u;  }
        c.RA = 0x801A41C4u; 
        SoTN_rbo6.RBO6_InitializeEntity(c, m);
        { c.V0 = c.S0 << 1;  }
        { var _s = c.V0; var _t = c.S0; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.S0; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A1 = _s + _t;  }
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x80090000u; 
        mem.WriteU32((c.At + 0x7400u), c.V0); 
        c.V0 = mem.ReadU32((c.S1 + 0x34u)); 
        c.V1 = 0x00800000u; 
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x3748u), 0u); 
        mem.WriteU32((c.S1 + 0x64u), c.S0); 
        mem.WriteU16((c.S1 + 0x54u), (ushort)0u); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        mem.WriteU32((c.S1 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0100u;  }
        mem.WriteU16((c.A1 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x14u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0004u;  }
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0); 
        { c.V0 = 0u | 0x00E8u;  }
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0080u;  }
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
        { c.V0 = 0u | 0x01F8u;  }
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0035u;  }
        mem.WriteU16((c.A1 + 0x20u), (ushort)0u); 
        mem.WriteU16((c.A1 + 0x8u), (ushort)0u); 
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)0u); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3824u)); 
        { c.A0 = 0u | 0x062Fu;  }
        c.RA = 0x801A4298u;
        Dispatcher.Call(c, m, c.V0);
        L801A4298: ;
        c.V0 = mem.ReadU32((c.S1 + 0x34u)); 
        c.V1 = 0x00010000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        mem.WriteU32((c.S1 + 0x34u), c.V0); 
        goto L801A4518;
        L801A42AC: ;
        c.A0 = 0x80080000u; 
        { c.A0 = c.A0 + 0x6FECu;  }
        c.V0 = mem.ReadU16((c.S1 + 0x7Cu)); 
        c.V1 = mem.ReadU32((c.S1 + 0x64u)); 
        { c.A2 = c.V0 + 0x1u;  }
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)c.A2); 
        c.V1 = mem.ReadU16((c.S1 + 0x7Cu)); 
        { c.V1 = c.V1 < 0x00000009u ? 1u : 0u;  }
        if (c.V1 != 0u) {
            { var _s = c.V0; var _t = c.A0; c.A1 = _s + _t;  }
            goto L801A4310;
        }
        { var _s = c.V0; var _t = c.A0; c.A1 = _s + _t;  }
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        c.A0 = c.S1; 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.A0 + 0x2Cu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        c.RA = 0x801A4308u; 
        SoTN_rbo6.DestroyEntity_rbo6(c, m);
        goto L801A4518;
        L801A4310: ;
        { c.V0 = c.A2 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0008u;  }
            goto L801A4324;
        }
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        goto L801A4518;
        L801A4324: ;
        { c.V0 = 0u | 0x0035u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        goto L801A4518;
        L801A4330: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3FFCu)); 
        { c.V0 = c.V0 & 0x0100u;  }
        if (c.V0 == 0u) {
            goto L801A4518;
        }
        c.At = 0x80090000u; 
        mem.WriteU32((c.At + 0x7420u), 0u); 
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)0u); 
        goto L801A4444;
        L801A435C: ;
        c.V1 = mem.ReadU32((c.S1 + 0x64u)); 
        c.A0 = 0x80080000u; 
        { c.A0 = c.A0 + 0x6FECu;  }
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.A0; c.A1 = _s + _t;  }
        c.V1 = mem.ReadU16((c.S1 + 0x2Cu)); 
        { c.V0 = 0u | 0x0035u;  }
        { c.V1 = c.V1 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V1); 
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
        goto L801A4518;
        L801A43C8: ;
        c.V1 = mem.ReadU32((c.S1 + 0x64u)); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A1 = _s + _t;  }
        c.V0 = mem.ReadU8((c.A1 + 0x2Au)); 
        { c.V0 = c.V0 + 0x1u;  }
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
        { c.V0 = 0u | 0x00FFu;  }
        if (c.V1 != c.V0) {
            { c.V0 = 0u | 0x0004u;  }
            goto L801A4518;
        }
        { c.V0 = 0u | 0x0004u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        L801A4444: ;
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        goto L801A4518;
        L801A4454: ;
        c.V1 = mem.ReadU32((c.S1 + 0x64u)); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A1 = _s + _t;  }
        c.V0 = mem.ReadU8((c.A1 + 0x2Au)); 
        { c.V0 = c.V0 - 0x4u;  }
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
        { c.V1 = c.V1 < 0x00000005u ? 1u : 0u;  }
        if (c.V1 == 0u) {
            mem.WriteU8((c.A1 + 0x10u), (byte)c.V0); 
            goto L801A4518;
        }
        mem.WriteU8((c.A1 + 0x10u), (byte)c.V0); 
        c.V1 = 0x80040000u; 
        c.V1 = mem.ReadU32((c.V1 - 0x38A0u)); 
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x3748u), c.V0); 
        c.At = 0x80090000u; 
        mem.WriteU32((c.At + 0x7400u), 0u); 
        { c.V1 = c.V1 < 0x00000737u ? 1u : 0u;  }
        if (c.V1 != 0u) {
            { c.V0 = 0u | 0x0005u;  }
            goto L801A44F0;
        }
        { c.V0 = 0u | 0x0005u;  }
        { c.V0 = 0u | 0x0003u;  }
        L801A44F0: ;
        c.At = 0x80090000u; 
        mem.WriteU32((c.At + 0x78B4u), c.V0); 
        c.V1 = mem.ReadU16((c.S1 + 0x2Cu)); 
        { c.V0 = 0u | 0x0009u;  }
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x38CCu), c.V0); 
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x3060u), 0u); 
        { c.V1 = c.V1 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V1); 
        L801A4518: ;
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool func_801AB5A0_rbo6(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rbo6", m, false)) return true;
        func_801AB5A0_rbo6_Body(c, m);
        return false;
    }
    private static void func_801AB5A0_rbo6_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rbo6", 0x801B3F50u);
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
            goto L801AB5FC;
        }
        { c.S4 = c.V0 & 0x7FFFu;  }
        { c.V0 = c.V1 < 0x00000005u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 < 0x0000000Au ? 1u : 0u;  }
            goto L801AB604;
        }
        { c.V0 = c.V1 < 0x0000000Au ? 1u : 0u;  }
        c.V0 = mem.ReadU8((c.S1 + 0x48u)); 
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0005u;  }
            goto L801AB5FC;
        }
        { c.V0 = 0u | 0x0005u;  }
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        c.V1 = mem.ReadU16((c.S1 + 0x2Cu)); 
        L801AB5FC: ;
        { c.V0 = c.V1 < 0x0000000Au ? 1u : 0u;  }
        L801AB604: ;
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801ABE64;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x2FC4u)); 
        switch (c.V0)
        {
            case 0x801AB624u: goto L801AB624;
            case 0x801AB7F4u: goto L801AB7F4;
            case 0x801ABE64u: goto L801ABE64;
            case 0x801AB854u: goto L801AB854;
            case 0x801ABA14u: goto L801ABA14;
            case 0x801ABADCu: goto L801ABADC;
            case 0x801ABB8Cu: goto L801ABB8C;
            case 0x801ABD04u: goto L801ABD04;
            case 0x801AC1D4u: goto L801AC1D4;
            case 0x801AC2B8u: goto L801AC2B8;
            case 0x801AC494u: goto L801AC494;
            case 0x801AC56Cu: goto L801AC56C;
            case 0x801AC670u: goto L801AC670;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801AB624: ;
        { c.V0 = c.S4 & 0xFFFFu;  }
        c.At = 0x80090000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU8((c.At + 0x7964u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 != 0u) {
            goto L801ABD64;
        }
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x4D4u;  }
        c.S0 = 0u; 
        c.RA = 0x801AB654u; 
        SoTN_rbo6.RBO6_InitializeEntity(c, m);
        c.S5 = 0x801B0000u; 
        { c.S5 = c.S5 + 0x4004u;  }
        { c.V0 = c.S0 << 16;  }
        L801AB660: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (uint)((int)c.V0 >> 15);  }
        { var _s = c.V0; var _t = c.S5; c.V0 = _s + _t;  }
        c.V0 = mem.ReadU16(c.V0); 
        if (c.V0 == 0u) {
            { c.V0 = c.S0 + 0x1u;  }
            goto L801AB690;
        }
        { c.V0 = c.S0 + 0x1u;  }
        c.S0 = c.V0; 
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 31 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = c.S0 << 16;  }
            goto L801AB660;
        }
        { c.V0 = c.S0 << 16;  }
        L801AB690: ;
        { c.V0 = c.S0 << 16;  }
        { c.S3 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.S3 < 31 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.A0 = 0u | 0x0004u;  }
            goto L801AB6CC;
        }
        { c.A0 = 0u | 0x0004u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0007u;  }
        c.RA = 0x801AB6B8u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.S2 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.S2 != c.V0) {
            c.V1 = 0x00800000u; 
            goto L801AB6D4;
        }
        c.V1 = 0x00800000u; 
        L801AB6CC: ;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)0u); 
        goto L801AC048;
        L801AB6D4: ;
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
        c.RA = 0x801AB724u;
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
            goto L801AB7D0;
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
        L801AB77C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 == 0u) {
            { c.V0 = 0u | 0x0006u;  }
            goto L801AB78C;
        }
        { c.V0 = 0u | 0x0006u;  }
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.T3); 
        goto L801AB7B8;
        L801AB78C: ;
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
        L801AB7B8: ;
        { c.V0 = 0u | 0x007Eu;  }
        mem.WriteU16((c.A2 + 0x26u), (ushort)c.V0); 
        c.A2 = mem.ReadU32(c.A2); 
        if (c.A2 != 0u) {
            { c.A3 = c.A3 + 0x1u;  }
            goto L801AB77C;
        }
        { c.A3 = c.A3 + 0x1u;  }
        L801AB7D0: ;
        c.V0 = 0xFFFF8000u; 
        mem.WriteU16((c.S1 + 0x4u), (ushort)c.V0); 
        { c.V0 = 0u | 0x4000u;  }
        mem.WriteU32((c.S1 + 0xCu), c.V0); 
        { c.V0 = 0u | 0x0040u;  }
        mem.WriteU16((c.S1 + 0x90u), (ushort)c.V0); 
        c.V0 = 0xFFFFFE00u; 
        mem.WriteU32((c.S1 + 0x94u), c.V0); 
        goto L801ABE64;
        L801AB7F4: ;
        c.V0 = mem.ReadU32((c.S1 + 0xCu)); 
        c.V1 = mem.ReadU16((c.S1 + 0x90u)); 
        c.A0 = mem.ReadU32((c.S1 + 0x94u)); 
        { c.V1 = c.V1 - 0x1u;  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s + _t;  }
        mem.WriteU16((c.S1 + 0x90u), (ushort)c.V1); 
        { c.V1 = c.V1 & 0xFFFFu;  }
        if (c.V1 != 0u) {
            mem.WriteU32((c.S1 + 0xCu), c.V0); 
            goto L801AB82C;
        }
        mem.WriteU32((c.S1 + 0xCu), c.V0); 
        c.V0 = mem.ReadU32((c.S1 + 0x94u)); 
        { c.V1 = 0u | 0x0040u;  }
        mem.WriteU16((c.S1 + 0x90u), (ushort)c.V1); 
        { var _s = 0u; var _t = c.V0; c.V0 = _s - _t;  }
        mem.WriteU32((c.S1 + 0x94u), c.V0); 
        L801AB82C: ;
        c.RA = 0x801AB834u; 
        SoTN_rbo6.func_801A8C88(c, m);
        c.V0 = (uint)(short)mem.ReadU16((c.S1 + 0x8Cu)); 
        { c.V1 = 0u | 0x0010u;  }
        { c.V0 = c.V0 << 1;  }
        c.At = 0x801B0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        mem.WriteU16((c.At + 0x4004u), (ushort)c.V1); 
        goto L801ABE64;
        L801AB854: ;
        { c.S0 = c.S4 & 0xFFFFu;  }
        c.A0 = c.S0; 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x37B8u)); 
        { c.A1 = 0u | 0x2000u;  }
        c.RA = 0x801AB870u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.S0 < 0x00000016u ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.A0 = c.SP + 0x20u;  }
            goto L801AB8A8;
        }
        { c.A0 = c.SP + 0x20u;  }
        { c.V0 = c.S0 < 0x00000014u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            c.A1 = 0u; 
            goto L801AB8AC;
        }
        c.A1 = 0u; 
        c.At = 0x80090000u; 
        { var _s = c.At; var _t = c.S0; c.At = _s + _t;  }
        c.V0 = mem.ReadU8((c.At + 0x7964u)); 
        { c.V0 = c.V0 ^ 0x0002u;  }
        c.At = 0x80090000u; 
        { var _s = c.At; var _t = c.S0; c.At = _s + _t;  }
        mem.WriteU8((c.At + 0x7964u), (byte)c.V0); 
        L801AB8A8: ;
        c.A1 = 0u; 
        L801AB8AC: ;
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
        c.RA = 0x801AB8E4u; 
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
        L801AB918: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 != 0u) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801AB964;
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
        goto L801AB9F0;
        L801AB964: ;
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
            goto L801AB9D4;
        }
        mem.WriteU8((c.A2 + 0x6u), (byte)0u); 
        mem.WriteU8((c.A2 + 0x2Au), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x1Eu), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x12u), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x6u), (byte)c.A0); 
        goto L801AB9E4;
        L801AB9D4: ;
        mem.WriteU8((c.A2 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x1Du), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x11u), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x5u), (byte)c.A0); 
        L801AB9E4: ;
        { c.V0 = 0u | 0x01FDu;  }
        mem.WriteU16((c.A2 + 0x26u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0011u;  }
        L801AB9F0: ;
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.V0); 
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 + 0x1u;  }
        { c.V0 = (int)c.A3 < 3 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801AB918;
        }
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801ABCFC;
        L801ABA14: ;
        c.S0 = 0u; 
        c.S3 = 0x80080000u; 
        { c.S3 = c.S3 - 0x101Cu;  }
        c.V0 = c.S3; 
        c.A0 = 0x80180000u; 
        c.A0 = mem.ReadU32((c.A0 + 0x1238u)); 
        { c.A3 = 0u | 0x0BFFu;  }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        L801ABA34: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU8(c.V0, (byte)0u); 
        { c.A3 = c.A3 - 0x1u;  }
        if ((int)c.A3 >= 0) {
            { c.V0 = c.V0 + 0x1u;  }
            goto L801ABA34;
        }
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        { c.S2 = c.S4 << 4;  }
        L801ABA4C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.A0); 
        if (c.V0 != 0u) {
            { c.A1 = c.SP + 0x28u;  }
            goto L801ABA80;
        }
        { c.A1 = c.SP + 0x28u;  }
        if (c.S0 != 0u) {
            { c.S0 = 0u | 0x0001u;  }
            goto L801ABA94;
        }
        { c.S0 = 0u | 0x0001u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x37B0u)); 
        { var _s = c.S2; var _t = c.V0; c.V0 = _s + _t;  }
        c.A0 = mem.ReadU32(c.V0); 
        goto L801ABA4C;
        L801ABA80: ;
        c.A2 = c.S3; 
        { c.A3 = 0u | 0x00C0u;  }
        c.RA = 0x801ABA8Cu; 
        SoTN_rbo6.RBO6_BlitChar(c, m);
        c.A0 = c.V0; 
        goto L801ABA4C;
        L801ABA94: ;
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
        c.RA = 0x801ABAC0u; 
        SoTN_main.LoadTPage(c, m);
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)0u); 
        c.V1 = mem.ReadU16((c.SP + 0x28u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x7Eu), (ushort)c.V1); 
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        goto L801ABE64;
        L801ABADC: ;
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
        L801ABB04: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 != 0u) {
            goto L801ABB38;
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
        goto L801ABB60;
        L801ABB38: ;
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
        L801ABB60: ;
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.A0); 
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.A0); 
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.A1); 
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.A1); 
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 + 0x1u;  }
        { c.V0 = (int)c.A3 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801ABCD4;
        }
        goto L801ABB04;
        L801ABB8C: ;
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
        L801ABC04: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 != 0u) {
            { c.V0 = c.A0 + 0x68u;  }
            goto L801ABC64;
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
        c.V0 = mem.ReadU16((c.At + 0x124Cu)); 
        { c.V0 = c.V0 + 0xA7u;  }
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V0); 
        c.V0 = mem.ReadU8((c.A2 + 0x2Au)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.A1; c.At = _s + _t;  }
        c.V1 = mem.ReadU16((c.At + 0x123Cu)); 
        { c.V0 = c.V0 - 0x10u;  }
        { c.V1 = c.V1 + 0xA7u;  }
        mem.WriteU8((c.A2 + 0x2Au), (byte)c.V0); 
        mem.WriteU8((c.A2 + 0x1Eu), (byte)c.V0); 
        goto L801ABCB8;
        L801ABC64: ;
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0098u;  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s - _t;  }
        mem.WriteU16((c.A2 + 0x14u), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0x2Cu), (ushort)c.T1); 
        mem.WriteU16((c.A2 + 0x20u), (ushort)c.T0); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.A1; c.At = _s + _t;  }
        c.V0 = mem.ReadU16((c.At + 0x125Cu)); 
        { c.V0 = c.V0 + 0xA7u;  }
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V0); 
        c.V0 = mem.ReadU8((c.A2 + 0x11u)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.A1; c.At = _s + _t;  }
        c.V1 = mem.ReadU16((c.At + 0x126Cu)); 
        { c.V0 = c.V0 - 0x10u;  }
        { c.V1 = c.V1 + 0xA7u;  }
        mem.WriteU8((c.A2 + 0x11u), (byte)c.V0); 
        mem.WriteU8((c.A2 + 0x5u), (byte)c.V0); 
        L801ABCB8: ;
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.V1); 
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.V1); 
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 + 0x1u;  }
        { c.V0 = (int)c.A3 < 3 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801ABC04;
        }
        L801ABCD4: ;
        c.V0 = mem.ReadU16((c.S1 + 0x7Cu)); 
        { c.V1 = 0u | 0x0008u;  }
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)c.V0); 
        { c.V0 = c.V0 & 0xFFFFu;  }
        if (c.V0 != c.V1) {
            goto L801ABE64;
        }
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)0u); 
        { c.V0 = c.V0 + 0x1u;  }
        L801ABCFC: ;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        goto L801ABE64;
        L801ABD04: ;
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
            goto L801ABE64;
        }
        L801ABD64: ;
        c.A0 = c.S1; 
        c.RA = 0x801ABD6Cu; 
        SoTN_rbo6.DestroyEntity_rbo6(c, m);
        goto L801AC048;
        L801ABD74: ;
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
        c.V0 = mem.ReadU16((c.At + 0x127Cu)); 
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
        c.V1 = mem.ReadU16((c.At + 0x128Cu)); 
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
        goto L801ABEF4;
        L801ABE64: ;
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        { c.V0 = c.V0 < 0x00000002u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801ABF1C;
        }
        c.A1 = 0x80040000u; 
        c.A1 = mem.ReadU16((c.A1 - 0x3668u)); 
        c.A0 = c.S1; 
        c.RA = 0x801ABE88u; 
        SoTN_rbo6.RBO6_BlinkItem(c, m);
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
        L801ABEB0: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 - 0x1u;  }
        if ((int)c.A3 >= 0) {
            goto L801ABEB0;
        }
        c.V0 = mem.ReadU16((c.S1 + 0x80u)); 
        if (c.V0 != 0u) {
            c.A3 = 0u; 
            goto L801ABF0C;
        }
        c.A3 = 0u; 
        { c.V1 = 0u | 0x0008u;  }
        L801ABED4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU16((c.A2 + 0x32u)); 
        if (c.V0 == c.V1) {
            { c.A3 = c.A3 + 0x1u;  }
            goto L801ABD74;
        }
        { c.A3 = c.A3 + 0x1u;  }
        c.A2 = mem.ReadU32(c.A2); 
        { c.V0 = (int)c.A3 < 4 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801ABED4;
        }
        L801ABEF4: ;
        c.V0 = mem.ReadU16((c.S1 + 0x82u)); 
        { c.V1 = 0u | 0x0004u;  }
        mem.WriteU16((c.S1 + 0x80u), (ushort)c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x82u), (ushort)c.V0); 
        goto L801ABF1C;
        L801ABF0C: ;
        c.V0 = mem.ReadU16((c.S1 + 0x80u)); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU16((c.S1 + 0x80u), (ushort)c.V0); 
        L801ABF1C: ;
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
        L801ABF44: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 - 0x1u;  }
        if ((int)c.A3 >= 0) {
            goto L801ABF44;
        }
        if (c.A2 == 0u) {
            { c.A3 = 0u | 0x0008u;  }
            goto L801AC048;
        }
        { c.A3 = 0u | 0x0008u;  }
        L801ABF5C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU16((c.A2 + 0x32u)); 
        if (c.V0 == c.A3) {
            goto L801AC038;
        }
        c.V0 = mem.ReadU8((c.A2 + 0x13u)); 
        { c.V0 = c.V0 & 0x0003u;  }
        if (c.V0 == 0u) {
            goto L801ABFA4;
        }
        c.V1 = mem.ReadU16((c.A2 + 0x16u)); 
        c.A0 = mem.ReadU16((c.A2 + 0x2Eu)); 
        { c.V0 = c.V1 - 0x1u;  }
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.V0); 
        { c.V0 = c.A0 - 0x1u;  }
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V1); 
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.A0); 
        goto L801ABFD4;
        L801ABFA4: ;
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
        L801ABFD4: ;
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
            goto L801AC038;
        }
        mem.WriteU8((c.A2 + 0x6u), (byte)c.V0); 
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.A3); 
        L801AC038: ;
        c.A2 = mem.ReadU32(c.A2); 
        if (c.A2 != 0u) {
            goto L801ABF5C;
        }
        L801AC048: ;
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
            goto L801AC120;
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
            goto L801AC0D4;
        }
        { c.V0 = c.A0 << 1;  }
        c.A0 = c.A2; 
        c.RA = 0x801AC0CCu; 
        SoTN_rbo6.DestroyEntity_rbo6(c, m);
        goto L801AC17C;
        L801AC0D4: ;
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V1 = mem.ReadU16((c.At + 0x65Cu)); 
        { c.V0 = c.V1 < 0x00000080u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801AC100;
        }
        c.V0 = 0x801B0000u; 
        { c.V0 = c.V0 - 0x5D0Cu;  }
        mem.WriteU32((c.A2 + 0xB8u), c.V0); 
        goto L801AC110;
        L801AC100: ;
        c.V0 = 0x801B0000u; 
        { c.V0 = c.V0 - 0x52F8u;  }
        mem.WriteU32((c.A2 + 0xB8u), c.V0); 
        { c.V1 = c.V1 - 0x80u;  }
        L801AC110: ;
        { c.V0 = 0u | 0x8000u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _s + _t;  }
        mem.WriteU16((c.A2 + 0x30u), (ushort)c.V0); 
        goto L801AC16C;
        L801AC120: ;
        c.V1 = mem.ReadU16((c.A2 + 0xB4u)); 
        if (c.V0 == 0u) {
            goto L801AC16C;
        }
        c.V0 = mem.ReadU8((c.A2 + 0x48u)); 
        if (c.V0 == 0u) {
            { c.A1 = c.V1 >> 3;  }
            goto L801AC16C;
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
        L801AC16C: ;
        c.V0 = mem.ReadU32((c.A2 + 0xB8u)); 
        c.A0 = c.A2; 
        c.RA = 0x801AC17Cu;
        Dispatcher.Call(c, m, c.V0);
        L801AC17C: ;
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
            goto L801AC6C8;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x2F9Cu)); 
        switch (c.V0)
        {
            case 0x801AC1D4u: goto L801AC1D4;
            case 0x801AC2B8u: goto L801AC2B8;
            case 0x801AC494u: goto L801AC494;
            case 0x801AC56Cu: goto L801AC56C;
            case 0x801AC670u: goto L801AC670;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801AC1D4: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x4D4u;  }
        c.RA = 0x801AC1E4u; 
        SoTN_rbo6.RBO6_InitializeEntity(c, m);
        c.V1 = 0x00010000u; 
        c.V0 = mem.ReadU32((c.S0 + 0x34u)); 
        c.A0 = mem.ReadU16((c.S0 + 0x30u)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        c.V1 = 0x08000000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s ^ _t;  }
        if (c.A0 != 0u) {
            mem.WriteU32((c.S0 + 0x34u), c.V0); 
            goto L801AC20C;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0060u;  }
        mem.WriteU16((c.S0 + 0x30u), (ushort)c.V0); 
        L801AC20C: ;
        { c.A0 = 0u | 0x0004u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0003u;  }
        c.RA = 0x801AC224u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.A0 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.A0 != c.V0) {
            { c.V0 = c.A0 << 1;  }
            goto L801AC240;
        }
        { c.V0 = c.A0 << 1;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)0u); 
        goto L801AC6C8;
        L801AC240: ;
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
            goto L801AC28C;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        L801AC278: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            goto L801AC278;
        }
        L801AC28C: ;
        c.A0 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.V0 = mem.ReadU8(c.A0); 
        mem.WriteU16((c.S0 + 0x80u), (ushort)c.V0); 
        c.V0 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.V1 = mem.ReadU8((c.A0 + 0x1u)); 
        { c.V0 = c.V0 + 0x2u;  }
        mem.WriteU32((c.S0 + 0x7Cu), c.V0); 
        mem.WriteU16((c.S0 + 0x82u), (ushort)c.V1); 
        goto L801AC6C8;
        L801AC2B8: ;
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
        c.RA = 0x801AC2E8u; 
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
            goto L801AC488;
        }
        c.A2 = 0u; 
        { c.A0 = 0u | 0x0080u;  }
        L801AC31C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801AC394;
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
        goto L801AC478;
        L801AC394: ;
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
            goto L801AC434;
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
        goto L801AC458;
        L801AC434: ;
        c.V1 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V0 = mem.ReadU16((c.S0 + 0x6u)); 
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x11u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x5u), (byte)c.A0); 
        { c.V1 = c.V1 >> 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 + 0x4u;  }
        L801AC458: ;
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0); 
        { c.V0 = 0u | 0x01FCu;  }
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0011u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        L801AC478: ;
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801AC31C;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801AC488: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801AC668;
        L801AC494: ;
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
            goto L801AC4F0;
        }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        L801AC4C4: ;
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
            goto L801AC4C4;
        }
        { c.S1 = c.S1 + 0x1u;  }
        c.S1 = c.S2; 
        L801AC4F0: ;
        { c.S3 = 0u | 0x0001u;  }
        L801AC4F4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.A0); 
        if (c.V0 == 0u) {
            c.A1 = 0u; 
            goto L801AC53C;
        }
        c.A1 = 0u; 
        if (c.V0 != c.S3) {
            { c.A1 = c.SP + 0x28u;  }
            goto L801AC524;
        }
        { c.A1 = c.SP + 0x28u;  }
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        { c.A0 = c.A0 + 0x1u;  }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        { c.V0 = c.V0 << 3;  }
        { var _s = c.S2; var _t = c.V0; c.S1 = _s + _t;  }
        goto L801AC4F4;
        L801AC524: ;
        c.A3 = mem.ReadU16((c.S0 + 0x80u)); 
        c.A2 = c.S1; 
        { c.A3 = c.A3 >> 1;  }
        c.RA = 0x801AC534u; 
        SoTN_rbo6.RBO6_BlitChar(c, m);
        c.A0 = c.V0; 
        goto L801AC4F4;
        L801AC53C: ;
        c.A0 = c.S2; 
        { c.V0 = 0u | 0x0180u;  }
        mem.WriteU32((c.SP + 0x10u), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        c.A2 = 0u; 
        mem.WriteU32((c.SP + 0x14u), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.A3 = 0u; 
        mem.WriteU32((c.SP + 0x18u), c.V0); 
        c.RA = 0x801AC564u; 
        SoTN_main.LoadTPage(c, m);
        goto L801AC65C;
        L801AC56C: ;
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
            goto L801AC64C;
        }
        c.A2 = 0u; 
        L801AC5A8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            goto L801AC5F8;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = c.V0 + 0x8u;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        c.V0 = c.LO; 
        if ((int)c.V0 >= 0) {
            goto L801AC5D0;
        }
        { c.V0 = c.V0 + 0x7u;  }
        L801AC5D0: ;
        { c.V0 = (uint)((int)c.V0 >> 3);  }
        c.V1 = mem.ReadU16((c.A1 + 0xAu)); 
        c.A0 = mem.ReadU8((c.A1 + 0x12u)); 
        { var _s = c.V1; var _t = c.V0; c.V1 = _s + _t;  }
        { c.A0 = c.A0 - 0x10u;  }
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V1); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V1); 
        mem.WriteU8((c.A1 + 0x12u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x6u), (byte)c.A0); 
        goto L801AC63C;
        L801AC5F8: ;
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = c.V0 + 0x8u;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        c.V0 = c.LO; 
        if ((int)c.V0 >= 0) {
            goto L801AC618;
        }
        { c.V0 = c.V0 + 0x7u;  }
        L801AC618: ;
        { c.V0 = (uint)((int)c.V0 >> 3);  }
        c.V1 = mem.ReadU16((c.A1 + 0x22u)); 
        c.A0 = mem.ReadU8((c.A1 + 0x29u)); 
        { var _s = c.V1; var _t = c.V0; c.V1 = _s - _t;  }
        { c.A0 = c.A0 - 0x10u;  }
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V1); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V1); 
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0); 
        L801AC63C: ;
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801AC5A8;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801AC64C: ;
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = 0u | 0x0008u;  }
        if (c.V1 != c.V0) {
            goto L801AC6C8;
        }
        L801AC65C: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        mem.WriteU16((c.S0 + 0x86u), (ushort)0u); 
        { c.V0 = c.V0 + 0x1u;  }
        L801AC668: ;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        goto L801AC6C8;
        L801AC670: ;
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
            goto L801AC6C8;
        }
        c.A0 = c.S0; 
        c.RA = 0x801AC6C8u; 
        SoTN_rbo6.DestroyEntity_rbo6(c, m);
        L801AC6C8: ;
        c.RA = mem.ReadU32((c.SP + 0x48u)); 
        c.S3 = mem.ReadU32((c.SP + 0x44u)); 
        c.S2 = mem.ReadU32((c.SP + 0x40u)); 
        c.S1 = mem.ReadU32((c.SP + 0x3Cu)); 
        c.S0 = mem.ReadU32((c.SP + 0x38u)); 
        { c.SP = c.SP + 0x50u;  }
        return;
        }
    public static bool RBO6_EntityMessageBox(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rbo6", m, false)) return true;
        RBO6_EntityMessageBox_Body(c, m);
        return false;
    }
    private static void RBO6_EntityMessageBox_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rbo6", 0x801B3F50u);
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
            goto L801AC6C8;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x2F9Cu)); 
        switch (c.V0)
        {
            case 0x801AC1D4u: goto L801AC1D4;
            case 0x801AC2B8u: goto L801AC2B8;
            case 0x801AC494u: goto L801AC494;
            case 0x801AC56Cu: goto L801AC56C;
            case 0x801AC670u: goto L801AC670;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801AC1D4: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x4D4u;  }
        c.RA = 0x801AC1E4u; 
        SoTN_rbo6.RBO6_InitializeEntity(c, m);
        c.V1 = 0x00010000u; 
        c.V0 = mem.ReadU32((c.S0 + 0x34u)); 
        c.A0 = mem.ReadU16((c.S0 + 0x30u)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        c.V1 = 0x08000000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s ^ _t;  }
        if (c.A0 != 0u) {
            mem.WriteU32((c.S0 + 0x34u), c.V0); 
            goto L801AC20C;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0060u;  }
        mem.WriteU16((c.S0 + 0x30u), (ushort)c.V0); 
        L801AC20C: ;
        { c.A0 = 0u | 0x0004u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0003u;  }
        c.RA = 0x801AC224u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.A0 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.A0 != c.V0) {
            { c.V0 = c.A0 << 1;  }
            goto L801AC240;
        }
        { c.V0 = c.A0 << 1;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)0u); 
        goto L801AC6C8;
        L801AC240: ;
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
            goto L801AC28C;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        L801AC278: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            goto L801AC278;
        }
        L801AC28C: ;
        c.A0 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.V0 = mem.ReadU8(c.A0); 
        mem.WriteU16((c.S0 + 0x80u), (ushort)c.V0); 
        c.V0 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.V1 = mem.ReadU8((c.A0 + 0x1u)); 
        { c.V0 = c.V0 + 0x2u;  }
        mem.WriteU32((c.S0 + 0x7Cu), c.V0); 
        mem.WriteU16((c.S0 + 0x82u), (ushort)c.V1); 
        goto L801AC6C8;
        L801AC2B8: ;
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
        c.RA = 0x801AC2E8u; 
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
            goto L801AC488;
        }
        c.A2 = 0u; 
        { c.A0 = 0u | 0x0080u;  }
        L801AC31C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801AC394;
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
        goto L801AC478;
        L801AC394: ;
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
            goto L801AC434;
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
        goto L801AC458;
        L801AC434: ;
        c.V1 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V0 = mem.ReadU16((c.S0 + 0x6u)); 
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x11u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x5u), (byte)c.A0); 
        { c.V1 = c.V1 >> 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 + 0x4u;  }
        L801AC458: ;
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0); 
        { c.V0 = 0u | 0x01FCu;  }
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0011u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        L801AC478: ;
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801AC31C;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801AC488: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801AC668;
        L801AC494: ;
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
            goto L801AC4F0;
        }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        L801AC4C4: ;
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
            goto L801AC4C4;
        }
        { c.S1 = c.S1 + 0x1u;  }
        c.S1 = c.S2; 
        L801AC4F0: ;
        { c.S3 = 0u | 0x0001u;  }
        L801AC4F4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.A0); 
        if (c.V0 == 0u) {
            c.A1 = 0u; 
            goto L801AC53C;
        }
        c.A1 = 0u; 
        if (c.V0 != c.S3) {
            { c.A1 = c.SP + 0x28u;  }
            goto L801AC524;
        }
        { c.A1 = c.SP + 0x28u;  }
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        { c.A0 = c.A0 + 0x1u;  }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        { c.V0 = c.V0 << 3;  }
        { var _s = c.S2; var _t = c.V0; c.S1 = _s + _t;  }
        goto L801AC4F4;
        L801AC524: ;
        c.A3 = mem.ReadU16((c.S0 + 0x80u)); 
        c.A2 = c.S1; 
        { c.A3 = c.A3 >> 1;  }
        c.RA = 0x801AC534u; 
        SoTN_rbo6.RBO6_BlitChar(c, m);
        c.A0 = c.V0; 
        goto L801AC4F4;
        L801AC53C: ;
        c.A0 = c.S2; 
        { c.V0 = 0u | 0x0180u;  }
        mem.WriteU32((c.SP + 0x10u), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        c.A2 = 0u; 
        mem.WriteU32((c.SP + 0x14u), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.A3 = 0u; 
        mem.WriteU32((c.SP + 0x18u), c.V0); 
        c.RA = 0x801AC564u; 
        SoTN_main.LoadTPage(c, m);
        goto L801AC65C;
        L801AC56C: ;
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
            goto L801AC64C;
        }
        c.A2 = 0u; 
        L801AC5A8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            goto L801AC5F8;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = c.V0 + 0x8u;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        c.V0 = c.LO; 
        if ((int)c.V0 >= 0) {
            goto L801AC5D0;
        }
        { c.V0 = c.V0 + 0x7u;  }
        L801AC5D0: ;
        { c.V0 = (uint)((int)c.V0 >> 3);  }
        c.V1 = mem.ReadU16((c.A1 + 0xAu)); 
        c.A0 = mem.ReadU8((c.A1 + 0x12u)); 
        { var _s = c.V1; var _t = c.V0; c.V1 = _s + _t;  }
        { c.A0 = c.A0 - 0x10u;  }
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V1); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V1); 
        mem.WriteU8((c.A1 + 0x12u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x6u), (byte)c.A0); 
        goto L801AC63C;
        L801AC5F8: ;
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = c.V0 + 0x8u;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        c.V0 = c.LO; 
        if ((int)c.V0 >= 0) {
            goto L801AC618;
        }
        { c.V0 = c.V0 + 0x7u;  }
        L801AC618: ;
        { c.V0 = (uint)((int)c.V0 >> 3);  }
        c.V1 = mem.ReadU16((c.A1 + 0x22u)); 
        c.A0 = mem.ReadU8((c.A1 + 0x29u)); 
        { var _s = c.V1; var _t = c.V0; c.V1 = _s - _t;  }
        { c.A0 = c.A0 - 0x10u;  }
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V1); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V1); 
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0); 
        L801AC63C: ;
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801AC5A8;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801AC64C: ;
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = 0u | 0x0008u;  }
        if (c.V1 != c.V0) {
            goto L801AC6C8;
        }
        L801AC65C: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        mem.WriteU16((c.S0 + 0x86u), (ushort)0u); 
        { c.V0 = c.V0 + 0x1u;  }
        L801AC668: ;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        goto L801AC6C8;
        L801AC670: ;
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
            goto L801AC6C8;
        }
        c.A0 = c.S0; 
        c.RA = 0x801AC6C8u; 
        SoTN_rbo6.DestroyEntity_rbo6(c, m);
        L801AC6C8: ;
        c.RA = mem.ReadU32((c.SP + 0x48u)); 
        c.S3 = mem.ReadU32((c.SP + 0x44u)); 
        c.S2 = mem.ReadU32((c.SP + 0x40u)); 
        c.S1 = mem.ReadU32((c.SP + 0x3Cu)); 
        c.S0 = mem.ReadU32((c.SP + 0x38u)); 
        { c.SP = c.SP + 0x50u;  }
        return;
        }
}
