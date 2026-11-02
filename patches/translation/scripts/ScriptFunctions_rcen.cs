using RecompOne.Runtime.Context;
using RecompOne.Runtime.Dispatch;
using RecompOne.Runtime.Memory;

namespace Recompiled.Translation;

public static class ScriptFunctions_rcen
{
    public static bool RCEN_CutsceneUnk1(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rcen", m, true)) return true;
        RCEN_CutsceneUnk1_Body(c, m);
        return false;
    }
    private static void RCEN_CutsceneUnk1_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rcen", 0x801AD2E0u);
        { c.V0 = 0u | 0x0002u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At - 0x2D14u), (ushort)c.V0); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At - 0x2D16u), (ushort)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x2D18u)); 
        { c.V1 = 0u | 0x0008u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At - 0x2D12u), (ushort)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At - 0x2D0Eu), (ushort)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU8((c.At - 0x2D0Au), (byte)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU8((c.At - 0x2D09u), (byte)c.V1); 
        { c.V0 = c.V0 + 0x14u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At - 0x2D1Au), (ushort)c.V0); 
        return;
        }
    public static bool RCEN_SetCutsceneScript(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rcen", m, true)) return true;
        RCEN_SetCutsceneScript_Body(c, m);
        return false;
    }
    private static void RCEN_SetCutsceneScript_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rcen", 0x801AD2E0u);
        c.A0 = DialogueScripts.ResolveStart("rcen", c.A0, m);
        { c.SP = c.SP - 0x20u;  }
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        c.S1 = c.A0; 
        { c.A0 = 0u | 0x0006u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0007u;  }
        mem.WriteU32((c.SP + 0x18u), c.RA); 
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.RA = 0x8019D744u;
        Dispatcher.Call(c, m, c.V0);
        c.S0 = 0x801B0000u; 
        { c.S0 = c.S0 - 0x2CE8u;  }
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        c.V1 = 0xFFFFFFFFu; 
        if (c.V0 != c.V1) {
            mem.WriteU32(c.S0, c.V0); 
            goto L8019D76C;
        }
        mem.WriteU32(c.S0, c.V0); 
        mem.WriteU32(c.S0, 0u); 
        c.V0 = 0u; 
        goto L8019D8C8;
        L8019D76C: ;
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2D20u), c.S1); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At - 0x2CE4u), (ushort)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2CECu), c.V1); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2CF0u), c.V1); 
        c.RA = 0x8019D794u; 
        SoTN_rcen.RCEN_CutsceneUnk1(c, m);
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
        mem.WriteU32((c.At - 0x2D08u), c.V0); 
        c.V1 = mem.ReadU32(c.V0); 
        { c.A2 = 0u | 0x0008u;  }
        mem.WriteU16((c.V0 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2D04u), c.V1); 
        c.V0 = mem.ReadU32(c.V1); 
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2D00u), c.V0); 
        c.V1 = mem.ReadU32(c.V0); 
        mem.WriteU16((c.V0 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2CFCu), c.V1); 
        c.A0 = mem.ReadU32(c.V1); 
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2CF8u), c.A0); 
        c.A1 = mem.ReadU32(c.A0); 
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.A0 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2CF4u), c.A1); 
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
        L8019D8C8: ;
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool RCEN_CutsceneUnk3(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rcen", m, false)) return true;
        RCEN_CutsceneUnk3_Body(c, m);
        return false;
    }
    private static void RCEN_CutsceneUnk3_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rcen", 0x801AD2E0u);
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
        c.RA = 0x8019D92Cu; 
        DialogueGlyphs.Clear(c, mem);
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool RCEN_CutsceneUnk4(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rcen", m, true)) return true;
        RCEN_CutsceneUnk4_Body(c, m);
        return false;
    }
    private static void RCEN_CutsceneUnk4_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rcen", 0x801AD2E0u);
        DialogueGlyphs.PrepareLine(c, mem);
        { c.SP = c.SP - 0x18u;  }
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.S0 = 0x801B0000u; 
        { c.S0 = c.S0 - 0x2D12u;  }
        mem.WriteU32((c.SP + 0x14u), c.RA); 
        c.A0 = (uint)(short)mem.ReadU16(c.S0); 
        c.RA = 0x8019D95Cu; 
        SoTN_rcen.RCEN_CutsceneUnk3(c, m);
        c.V0 = (uint)(short)mem.ReadU16(c.S0); 
        { c.V0 = c.V0 << 2;  }
        { var _s = c.S0; var _t = c.V0; c.V0 = _s + _t;  }
        c.V1 = mem.ReadU32((c.V0 + 0xAu)); 
        { c.V0 = 0u | 0x0010u;  }
        mem.WriteU16((c.V1 + 0x1Au), (ushort)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x2D0Cu)); 
        mem.WriteU16((c.V1 + 0xEu), (ushort)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x2D1Au)); 
        mem.WriteU8((c.V1 + 0xCu), (byte)0u); 
        mem.WriteU16((c.V1 + 0xAu), (ushort)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x2D1Cu)); 
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
    public static bool RCEN_DrawCutsceneActorName(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rcen", m, true)) return true;
        RCEN_DrawCutsceneActorName_Body(c, m);
        return false;
    }
    private static void RCEN_DrawCutsceneActorName_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rcen", 0x801AD2E0u);
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
        c.A2 = mem.ReadU32((c.At + 0x9F4u)); 
        { c.V0 = 0u | 0x00FFu;  }
        L8019DA28: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V1 = mem.ReadU8(c.A2); 
        if (c.V1 != c.V0) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L8019DA48;
        }
        { c.A2 = c.A2 + 0x1u;  }
        c.V1 = mem.ReadU8(c.A2); 
        if (c.V1 == 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L8019DA58;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L8019DA48: ;
        if (c.V1 == 0u) {
            goto L8019DA28;
        }
        { c.A1 = c.A1 + 0x1u;  }
        goto L8019DA28;
        L8019DA58: ;
        { c.A0 = 0u | 0x0006u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = c.A1 & 0xFFFFu;  }
        c.RA = 0x8019DA70u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.A1 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.A1 != c.V0) {
            { c.V0 = c.A1 << 1;  }
            goto L8019DA94;
        }
        { c.V0 = c.A1 << 1;  }
        c.A0 = c.S1; 
        c.RA = 0x8019DA8Cu; 
        SoTN_rcen.DestroyEntity_rcen(c, m);
        goto L8019DB60;
        L8019DA94: ;
        { var _s = c.V0; var _t = c.A1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.A1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A0 = _s + _t;  }
        c.V1 = 0x801B0000u; 
        { c.V1 = c.V1 - 0x2CECu;  }
        { c.V0 = c.S0 & 0xFFFFu;  }
        { c.V0 = c.V0 << 2;  }
        mem.WriteU32(c.V1, c.A1); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A2 = mem.ReadU32((c.At + 0x9F4u)); 
        if (c.A0 == 0u) {
            { c.A3 = 0u | 0x0038u;  }
            goto L8019DB60;
        }
        { c.A3 = 0u | 0x0038u;  }
        { c.T4 = 0u | 0x0006u;  }
        { c.T3 = 0u | 0x001Eu;  }
        { c.T2 = 0u | 0x0196u;  }
        { c.T1 = 0u | 0x01FFu;  }
        { c.A1 = 0u | 0x0008u;  }
        { c.T0 = c.V1 - 0x2Cu;  }
        L8019DAF0: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V1 = mem.ReadU8(c.A2); 
        if (c.V1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L8019DB08;
        }
        { c.A2 = c.A2 + 0x1u;  }
        { c.A3 = c.A3 + 0x4u;  }
        goto L8019DB54;
        L8019DB08: ;
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
        L8019DB54: ;
        if (c.A0 != 0u) {
            goto L8019DAF0;
        }
        L8019DB60: ;
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool func_8019DB78(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rcen", m, true)) return true;
        func_8019DB78_Body(c, m);
        return false;
    }
    private static void func_8019DB78_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rcen", 0x801AD2E0u);
        c.V0 = 0x00100000u; 
        { var _s = c.A0; var _t = c.V0; c.A0 = _s + _t;  }
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2CE0u), c.A0); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At - 0x2CE2u), (ushort)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At - 0x2CE4u), (ushort)c.V0); 
        return;
        }
    public static bool func_8019DBA4_rcen(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rcen", m, true)) return true;
        func_8019DBA4_rcen_Body(c, m);
        return false;
    }
    private static void func_8019DBA4_rcen_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rcen", 0x801AD2E0u);
        { c.SP = c.SP - 0x20u;  }
        c.A0 = 0x801B0000u; 
        { c.A0 = c.A0 - 0x2CE2u;  }
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
            goto L8019DC04;
        }
        { c.S1 = c.A0 + 0x2u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At - 0x2CE4u), (ushort)0u); 
        goto L8019DE64;
        L8019DBF4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU32(c.S1, c.A1); 
        goto L8019DE64;
        L8019DBFC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU32(c.S1, c.A2); 
        goto L8019DE64;
        L8019DC04: ;
        { c.S2 = 0u | 0x0001u;  }
        L8019DC08: ;
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
            goto L8019DBF4;
        }
        { c.V0 = c.A1 + 0x3u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V1 = mem.ReadU8((c.A1 + 0x2u)); 
        if (c.V1 == c.S2) {
            { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
            goto L8019DDB0;
        }
        { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L8019DC68;
        }
        if (c.V1 == 0u) {
            { c.V0 = c.A1 + 0x4u;  }
            goto L8019DC84;
        }
        { c.V0 = c.A1 + 0x4u;  }
        goto L8019DC08;
        L8019DC68: ;
        { c.V0 = 0u | 0x0002u;  }
        if (c.V1 == c.V0) {
            { c.V0 = 0u | 0x0003u;  }
            goto L8019DDEC;
        }
        { c.V0 = 0u | 0x0003u;  }
        if (c.V1 == c.V0) {
            { c.V0 = c.A1 + 0x4u;  }
            goto L8019DE3C;
        }
        { c.V0 = c.A1 + 0x4u;  }
        goto L8019DC08;
        L8019DC84: ;
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
        c.RA = 0x8019DCB8u; 
        SoTN_rcen.DestroyEntity_rcen(c, m);
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8(c.V1); 
        mem.WriteU16((c.S0 + 0x26u), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x26u)); 
        { c.V0 = c.V0 << 2;  }
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x454u)); 
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
        goto L8019DC08;
        L8019DDB0: ;
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
        c.RA = 0x8019DDE4u; 
        SoTN_rcen.DestroyEntity_rcen(c, m);
        goto L8019DC08;
        L8019DDEC: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2CE0u)); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 - 0x2C74u)); 
        c.V0 = mem.ReadU8(c.V0); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _t >> (int)(_s & 31u);  }
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.A1 + 0x4u;  }
            goto L8019DBFC;
        }
        { c.V0 = c.A1 + 0x4u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8((c.A1 + 0x3u)); 
        { var _s = c.V0; var _t = c.S2; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = 0u; var _t = c.V0; c.V0 = ~(_s | _t);  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s & _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2C74u), c.V0); 
        goto L8019DC08;
        L8019DE3C: ;
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8((c.A1 + 0x3u)); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 - 0x2C74u)); 
        { var _s = c.V0; var _t = c.S2; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2C74u), c.V0); 
        goto L8019DC08;
        L8019DE64: ;
        c.RA = mem.ReadU32((c.SP + 0x1Cu)); 
        c.S2 = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool RCEN_CutsceneSkip(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rcen", m, true)) return true;
        RCEN_CutsceneSkip_Body(c, m);
        return false;
    }
    private static void RCEN_CutsceneSkip_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rcen", 0x801AD2E0u);
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
            goto L8019DF44;
        }
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        c.S1 = 0x80040000u; 
        { c.S1 = c.S1 - 0x384Cu;  }
        c.A0 = mem.ReadU32((c.S0 + 0x64u)); 
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2D24u), c.V0); 
        c.S2 = 0xFFFFFFFFu; 
        c.RA = 0x8019DECCu;
        Dispatcher.Call(c, m, c.V1);
        c.V0 = mem.ReadU32((c.S0 + 0x34u)); 
        c.V1 = 0x00800000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s ^ _t;  }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        c.A0 = 0x801B0000u; 
        c.A0 = mem.ReadU32((c.A0 - 0x2CECu)); 
        if (c.A0 == c.S2) {
            goto L8019DF00;
        }
        c.V0 = mem.ReadU32(c.S1); 
        c.RA = 0x8019DF00u;
        Dispatcher.Call(c, m, c.V0);
        L8019DF00: ;
        c.A0 = 0x801B0000u; 
        c.A0 = mem.ReadU32((c.A0 - 0x2CF0u)); 
        if (c.A0 == c.S2) {
            goto L8019DF24;
        }
        c.V0 = mem.ReadU32(c.S1); 
        c.RA = 0x8019DF24u;
        Dispatcher.Call(c, m, c.V0);
        L8019DF24: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3824u)); 
        { c.A0 = 0u | 0x000Au;  }
        c.RA = 0x8019DF38u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x2Eu), (ushort)0u); 
        L8019DF44: ;
        c.RA = mem.ReadU32((c.SP + 0x1Cu)); 
        c.S2 = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool RCEN_ScaleCutsceneAvatar(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rcen", m, true)) return true;
        RCEN_ScaleCutsceneAvatar_Body(c, m);
        return false;
    }
    private static void RCEN_ScaleCutsceneAvatar_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rcen", 0x801AD2E0u);
        c.V0 = 0x801B0000u; 
        c.V0 = (uint)(short)mem.ReadU16((c.V0 - 0x2D12u)); 
        { c.A3 = c.V0 + (uint)(mem.LineCount - DialogueGlyphs.VisibleLines(mem));  }
        { c.V0 = (int)c.A3 < 5 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L8019DF90;
        }
        { c.A3 = c.A3 - 0x5u;  }
        L8019DF80: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (int)c.A3 < 5 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.A3 = c.A3 - 0x5u;  }
            goto L8019DF80;
        }
        { c.A3 = c.A3 - 0x5u;  }
        { c.A3 = c.A3 + 0x5u;  }
        L8019DF90: ;
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C48u)); 
        c.V0 = mem.ReadU16((c.V0 + 0x2Eu)); 
        if (c.V0 != 0u) {
            c.V1 = 0u; 
            goto L8019E00C;
        }
        c.V1 = 0u; 
        { c.V0 = c.A3 << 2;  }
        c.At = 0x801B0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A1 = mem.ReadU32((c.At - 0x2D08u)); 
        c.V0 = mem.ReadU8((c.A1 + 0x19u)); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s - _t;  }
        mem.WriteU8((c.A1 + 0x19u), (byte)c.V0); 
        c.V0 = mem.ReadU8((c.A1 + 0xDu)); 
        c.V1 = mem.ReadU8((c.A1 + 0x19u)); 
        { var _s = c.A0; var _t = c.V0; c.V0 = _s + _t;  }
        if (c.V1 != 0u) {
            mem.WriteU8((c.A1 + 0xDu), (byte)c.V0); 
            goto L8019E008;
        }
        mem.WriteU8((c.A1 + 0xDu), (byte)c.V0); 
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C48u)); 
        c.V1 = mem.ReadU16((c.V0 + 0x2Eu)); 
        { c.V1 = c.V1 + 0x1u;  }
        mem.WriteU16((c.V0 + 0x2Eu), (ushort)c.V1); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        L8019E008: ;
        c.V1 = 0u; 
        L8019E00C: ;
        { c.A0 = c.A0 & 0x00FFu;  }
        c.A2 = 0x801B0000u; 
        { c.A2 = c.A2 - 0x2D08u;  }
        L8019E018: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.V1 == c.A3) {
            goto L8019E038;
        }
        c.A1 = mem.ReadU32(c.A2); 
        c.V0 = mem.ReadU16((c.A1 + 0xAu)); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s - _t;  }
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0); 
        L8019E038: ;
        { c.V1 = c.V1 + 0x1u;  }
        { c.V0 = (int)c.V1 < 5 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.A2 = c.A2 + 0x4u;  }
            goto L8019E018;
        }
        { c.A2 = c.A2 + 0x4u;  }
        c.V1 = 0x801B0000u; 
        { c.V1 = c.V1 - 0x2D10u;  }
        c.V0 = mem.ReadU16(c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16(c.V1, (ushort)c.V0); 
        return;
        }
    public static bool func_8019E064(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rcen", m, true)) return true;
        func_8019E064_Body(c, m);
        return false;
    }
    private static void func_8019E064_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rcen", 0x801AD2E0u);
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
            goto L8019E13C;
        }
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2C70u)); 
        if (c.V0 == 0u) {
            goto L8019E110;
        }
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2D24u)); 
        if (c.V0 != 0u) {
            goto L8019E110;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x4220u)); 
        if (c.V0 != 0u) {
            goto L8019E108;
        }
        c.V0 = mem.ReadU16((c.S2 + 0x30u)); 
        if (c.V0 == 0u) {
            c.V1 = 0x00100000u; 
            goto L8019E0F0;
        }
        c.V1 = 0x00100000u; 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x34FCu)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s & _t;  }
        goto L8019E100;
        L8019E0F0: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x34FCu)); 
        c.V1 = 0x00080000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s & _t;  }
        L8019E100: ;
        if (c.V0 == 0u) {
            c.A0 = c.S2; 
            goto L8019E110;
        }
        c.A0 = c.S2; 
        L8019E108: ;
        c.RA = 0x8019E110u; 
        Dispatcher.Call(c, m, 0x8019DE80u);
        L8019E110: ;
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        if (c.V0 == 0u) {
            goto L8019E13C;
        }
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x2CE4u)); 
        if (c.V0 == 0u) {
            goto L8019E13C;
        }
        c.RA = 0x8019E13Cu; 
        Dispatcher.Call(c, m, 0x8019DBA4u);
        L8019E13C: ;
        c.V1 = mem.ReadU16((c.S2 + 0x2Cu)); 
        { c.V0 = c.V1 < 0x00000008u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L8019F0B4;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x5F5Cu)); 
        switch (c.V0)
        {
            case 0x8019E168u: goto L8019E168;
            case 0x8019E22Cu: goto L8019E22C;
            case 0x8019EB20u: goto L8019EB20;
            case 0x8019EB54u: goto L8019EB54;
            case 0x8019EC18u: goto L8019EC18;
            case 0x8019ECB0u: goto L8019ECB0;
            case 0x8019EF90u: goto L8019EF90;
            case 0x8019F038u: goto L8019F038;
            case 0x8019E2A4u: goto L8019E2A4;
            case 0x8019E2B0u: goto L8019E2B0;
            case 0x8019E35Cu: goto L8019E35C;
            case 0x8019E388u: goto L8019E388;
            case 0x8019E3B8u: goto L8019E3B8;
            case 0x8019E3F8u: goto L8019E3F8;
            case 0x8019E530u: goto L8019E530;
            case 0x8019E5A8u: goto L8019E5A8;
            case 0x8019E63Cu: goto L8019E63C;
            case 0x8019E664u: goto L8019E664;
            case 0x8019E68Cu: goto L8019E68C;
            case 0x8019E6C4u: goto L8019E6C4;
            case 0x8019E70Cu: goto L8019E70C;
            case 0x8019E240u: goto L8019E240;
            case 0x8019E768u: goto L8019E768;
            case 0x8019E7D4u: goto L8019E7D4;
            case 0x8019E834u: goto L8019E834;
            case 0x8019E880u: goto L8019E880;
            case 0x8019E8B4u: goto L8019E8B4;
            case 0x8019E8C4u: goto L8019E8C4;
            case 0x8019E984u: goto L8019E984;
            case 0x8019E9C0u: goto L8019E9C0;
            case 0x8019E9E0u: goto L8019E9E0;
            case 0x8019F0B4u: goto L8019F0B4;
            case 0x8019EA1Cu: goto L8019EA1C;
            case 0x8019F1CCu: goto L8019F1CC;
            case 0x8019F22Cu: goto L8019F22C;
            case 0x8019F318u: goto L8019F318;
            case 0x8019F3C0u: goto L8019F3C0;
            case 0x8019F474u: goto L8019F474;
            case 0x8019F4FCu: goto L8019F4FC;
            case 0x8019F540u: goto L8019F540;
            case 0x8019F584u: goto L8019F584;
            case 0x8019F5A8u: goto L8019F5A8;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L8019E168: ;
        c.V0 = mem.ReadU16((c.S2 + 0x30u)); 
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x18D5u;  }
        if (c.V0 == 0u) {
            goto L8019E184;
        }
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x17ACu;  }
        L8019E184: ;
        c.RA = 0x8019E18Cu; 
        SoTN_rcen.RCEN_SetCutsceneScript(c, m);
        { c.S0 = c.V0 & 0x00FFu;  }
        if (c.S0 == 0u) {
            c.V1 = 0x00800000u; 
            goto L8019F0B4;
        }
        c.V1 = 0x00800000u; 
        c.V0 = mem.ReadU32((c.S2 + 0x34u)); 
        { c.V1 = c.V1 | 0x2000u;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        mem.WriteU32((c.S2 + 0x34u), c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2CE8u)); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2C74u), 0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2C70u), 0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2D24u), 0u); 
        mem.WriteU32((c.S2 + 0x64u), c.V0); 
        c.V1 = 0x80040000u; 
        c.V1 = mem.ReadU8((c.V1 - 0x4134u)); 
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x38FCu), c.V0); 
        if (c.V1 != 0u) {
            goto L8019E210;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3660u)); 
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L8019E210;
        }
        { c.V0 = 0u | 0x0001u;  }
        c.V0 = 0x80090000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x7914u)); 
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L8019E220;
        }
        { c.V0 = 0u | 0x0001u;  }
        L8019E210: ;
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2D24u), c.V0); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2C70u), c.V0); 
        L8019E220: ;
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L8019E354;
        L8019E22C: ;
        c.S4 = 0x801B0000u; 
        { c.S4 = c.S4 - 0x2D0Au;  }
        { c.S1 = c.S4 - 0x16u;  }
        { c.S0 = c.S4 - 0xCu;  }
        { c.S3 = c.S4 - 0x12u;  }
        L8019E240: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.S4); 
        if (c.V0 == 0u) {
            goto L8019E264;
        }
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2D24u)); 
        if (c.V0 == 0u) {
            goto L8019F05C;
        }
        L8019E264: ;
        c.V0 = mem.ReadU32(c.S1); 
        { c.V1 = c.V0 + 0x1u;  }
        mem.WriteU32(c.S1, c.V1); 
        c.A2 = mem.ReadU8(c.V0); 
        { c.V1 = c.A2 & 0xFFFFu;  }
        { c.V0 = c.V1 < 0x00000019u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L8019EA5C;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x5F3Cu)); 
        switch (c.V0)
        {
            case 0x8019E2A4u: goto L8019E2A4;
            case 0x8019E2B0u: goto L8019E2B0;
            case 0x8019E35Cu: goto L8019E35C;
            case 0x8019E388u: goto L8019E388;
            case 0x8019E3B8u: goto L8019E3B8;
            case 0x8019E3F8u: goto L8019E3F8;
            case 0x8019E530u: goto L8019E530;
            case 0x8019E5A8u: goto L8019E5A8;
            case 0x8019E63Cu: goto L8019E63C;
            case 0x8019E664u: goto L8019E664;
            case 0x8019E68Cu: goto L8019E68C;
            case 0x8019E6C4u: goto L8019E6C4;
            case 0x8019E70Cu: goto L8019E70C;
            case 0x8019E240u: goto L8019E240;
            case 0x8019E768u: goto L8019E768;
            case 0x8019E7D4u: goto L8019E7D4;
            case 0x8019E834u: goto L8019E834;
            case 0x8019E880u: goto L8019E880;
            case 0x8019E8B4u: goto L8019E8B4;
            case 0x8019E8C4u: goto L8019E8C4;
            case 0x8019E984u: goto L8019E984;
            case 0x8019E9C0u: goto L8019E9C0;
            case 0x8019E9E0u: goto L8019E9E0;
            case 0x8019F0B4u: goto L8019F0B4;
            case 0x8019EA1Cu: goto L8019EA1C;
            case 0x8019F1CCu: goto L8019F1CC;
            case 0x8019F22Cu: goto L8019F22C;
            case 0x8019F318u: goto L8019F318;
            case 0x8019F3C0u: goto L8019F3C0;
            case 0x8019F474u: goto L8019F474;
            case 0x8019F4FCu: goto L8019F4FC;
            case 0x8019F540u: goto L8019F540;
            case 0x8019F584u: goto L8019F584;
            case 0x8019F5A8u: goto L8019F5A8;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L8019E2A4: ;
        { c.V0 = 0u | 0x0007u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L8019F0B4;
        L8019E2B0: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2D24u)); 
        if (c.V0 != 0u) {
            goto L8019E240;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x8u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x2u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 != 0u) {
            mem.WriteU16(c.S0, (ushort)c.V1); 
            goto L8019E2E8;
        }
        mem.WriteU16(c.S0, (ushort)c.V1); 
        c.V0 = mem.ReadU16((c.S0 - 0x4u)); 
        { c.V0 = c.V0 + 0xCu;  }
        mem.WriteU16((c.S0 - 0x4u), (ushort)c.V0); 
        L8019E2E8: ;
        c.V0 = mem.ReadU16((c.S0 + 0x4u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x4u), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 5 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L8019E310;
        }
        mem.WriteU16((c.S0 + 0x4u), (ushort)0u); 
        L8019E310: ;
        c.RA = 0x8019E318u; 
        SoTN_rcen.RCEN_CutsceneUnk4(c, m);
        c.V1 = mem.ReadU16((c.S0 + 0x8u)); 
        { c.V0 = c.V1 & 0x0001u;  }
        if (c.V0 != 0u) {
            goto L8019E344;
        }
        c.V0 = (uint)(short)mem.ReadU16((c.S0 + 0x4u)); 
        { c.V0 = (int)c.V0 < 4 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = c.V1 | 0x0001u;  }
            goto L8019E240;
        }
        { c.V0 = c.V1 | 0x0001u;  }
        mem.WriteU16((c.S0 + 0x8u), (ushort)c.V0); 
        L8019E344: ;
        mem.WriteU16((c.S0 + 0x6u), (ushort)0u); 
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        mem.WriteU16((c.S2 + 0x2Eu), (ushort)0u); 
        { c.V0 = c.V0 + 0x1u;  }
        L8019E354: ;
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L8019F0B4;
        L8019E35C: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2D20u)); 
        { c.V1 = c.V0 + 0x1u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2D20u), c.V1); 
        c.V0 = mem.ReadU8(c.V0); 
        c.At = 0x801B0000u; 
        mem.WriteU8((c.At - 0x2D09u), (byte)c.V0); 
        goto L8019E240;
        L8019E388: ;
        c.V1 = mem.ReadU32((c.S4 - 0x16u)); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32((c.S4 - 0x16u), c.V0); 
        c.V0 = mem.ReadU8(c.V1); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 - 0x2D24u)); 
        if (c.V1 != 0u) {
            mem.WriteU8(c.S4, (byte)c.V0); 
            goto L8019E240;
        }
        mem.WriteU8(c.S4, (byte)c.V0); 
        goto L8019F0B4;
        L8019E3B8: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2D24u)); 
        if (c.V0 != 0u) {
            { c.V1 = 0u | 0x0008u;  }
            goto L8019E240;
        }
        { c.V1 = 0u | 0x0008u;  }
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 - 0x2D08u)); 
        c.S0 = 0u; 
        { c.S0 = c.S0 + 0x1u;  }
        L8019E3DC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (int)c.S0 < 5 ? 1u : 0u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V1); 
        c.S1 = mem.ReadU32(c.S1); 
        if (c.V0 != 0u) {
            { c.S0 = c.S0 + 0x1u;  }
            goto L8019E3DC;
        }
        { c.S0 = c.S0 + 0x1u;  }
        goto L8019F0B4;
        L8019E3F8: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2D24u)); 
        if (c.V0 != 0u) {
            goto L8019E678;
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
        c.A0 = mem.ReadU8((c.At + 0x94Cu)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A1 = mem.ReadU8((c.At + 0x950u)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V1; c.At = _s + _t;  }
        c.V1 = mem.ReadU16((c.At + 0x954u)); 
        { c.V0 = 0u | 0x0090u;  }
        mem.WriteU16((c.S1 + 0x1Au), (ushort)c.V0); 
        { c.V0 = c.T2 & 0x0080u;  }
        if (c.V0 == 0u) {
            mem.WriteU16((c.S1 + 0xEu), (ushort)c.V1); 
            goto L8019E47C;
        }
        mem.WriteU16((c.S1 + 0xEu), (ushort)c.V1); 
        c.V1 = c.A0; 
        { c.V0 = c.V1 + 0x2Fu;  }
        goto L8019E484;
        L8019E47C: ;
        c.V0 = c.A0; 
        { c.V1 = c.V0 + 0x2Fu;  }
        L8019E484: ;
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
        c.V0 = mem.ReadU16((c.At + 0x95Cu)); 
        mem.WriteU16((c.S3 + 0x10u), (ushort)c.V0); 
        c.RA = 0x8019E4FCu; 
        SoTN_rcen.RCEN_CutsceneUnk1(c, m);
        c.RA = 0x8019E504u; 
        SoTN_rcen.RCEN_CutsceneUnk4(c, m);
        { c.A0 = c.S0 & 0xFFFFu;  }
        c.A1 = c.S2; 
        { c.V0 = 0u | 0x01FEu;  }
        mem.WriteU16((c.S1 + 0x26u), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x32u), (ushort)0u); 
        c.RA = 0x8019E51Cu; 
        SoTN_rcen.RCEN_DrawCutsceneActorName(c, m);
        { c.V0 = 0u | 0x0006u;  }
        mem.WriteU16((c.S3 + 0xCu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0003u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L8019F0B4;
        L8019E530: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2D24u)); 
        if (c.V0 != 0u) {
            { c.V1 = 0u | 0x0008u;  }
            goto L8019E240;
        }
        { c.V1 = 0u | 0x0008u;  }
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 - 0x2D08u)); 
        c.S0 = 0u; 
        { c.S0 = c.S0 + 0x1u;  }
        L8019E554: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (int)c.S0 < 5 ? 1u : 0u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V1); 
        c.S1 = mem.ReadU32(c.S1); 
        if (c.V0 != 0u) {
            { c.S0 = c.S0 + 0x1u;  }
            goto L8019E554;
        }
        { c.S0 = c.S0 + 0x1u;  }
        c.S0 = 0x801B0000u; 
        { c.S0 = c.S0 - 0x2CECu;  }
        c.A0 = mem.ReadU32(c.S0); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x384Cu)); 
        c.RA = 0x8019E588u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0xFFFFFFFFu; 
        mem.WriteU32(c.S0, c.V0); 
        { c.V0 = 0u | 0x0006u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At - 0x2D10u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0004u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L8019F0B4;
        L8019E5A8: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2D24u)); 
        if (c.V0 != 0u) {
            goto L8019E678;
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
        goto L8019F0B4;
        L8019E63C: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2D24u)); 
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0018u;  }
            goto L8019E240;
        }
        { c.V0 = 0u | 0x0018u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At - 0x2D10u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0006u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L8019F0B4;
        L8019E664: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2D24u)); 
        if (c.V0 == 0u) {
            goto L8019E984;
        }
        L8019E678: ;
        c.V0 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 + 0x2u;  }
        mem.WriteU32(c.S1, c.V0); 
        goto L8019E240;
        L8019E68C: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2D24u)); 
        if (c.V0 != 0u) {
            goto L8019E240;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3808u)); 
        c.RA = 0x8019E6B4u;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 == 0u) {
            goto L8019E6F8;
        }
        goto L8019E240;
        L8019E6C4: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2D24u)); 
        if (c.V0 != 0u) {
            goto L8019E240;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3808u)); 
        c.RA = 0x8019E6ECu;
        Dispatcher.Call(c, m, c.V0);
        { c.V1 = 0u | 0x0001u;  }
        if (c.V0 != c.V1) {
            goto L8019E240;
        }
        L8019E6F8: ;
        c.V0 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        goto L8019F0B4;
        L8019E70C: ;
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
        c.RA = 0x8019E760u; 
        SoTN_rcen.func_8019DB78(c, m);
        goto L8019E240;
        L8019E768: ;
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
        c.At = 0x00100000u; 
        { var _s = c.T1; var _t = c.At; c.At = _s + _t;  }
        c.V1 = mem.ReadU8(c.At); 
        { c.V1 = c.V1 << 2;  }
        { var _s = c.V1; var _t = c.A1; c.V1 = _s + _t;  }
        goto L8019E7D8;
        L8019E7D4: ;
        c.V1 = mem.ReadU32(c.S1); 
        L8019E7D8: ;
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
        c.V1 = mem.ReadU32((c.V1 - 0x2D20u)); 
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
        goto L8019E240;
        L8019E834: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2D20u)); 
        c.A0 = 0x801B0000u; 
        c.A0 = mem.ReadU32((c.A0 - 0x2C74u)); 
        c.V1 = mem.ReadU8(c.V0); 
        { var _s = c.V1; var _t = c.A0; c.V0 = _t >> (int)(_s & 31u);  }
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L8019F070;
        }
        { c.V0 = 0u | 0x0001u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = 0u; var _t = c.V0; c.V0 = ~(_s | _t);  }
        c.V1 = mem.ReadU32(c.S1); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s & _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2C74u), c.V0); 
        { c.V1 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V1); 
        goto L8019E240;
        L8019E880: ;
        c.V1 = mem.ReadU32(c.S1); 
        c.A0 = 0x801B0000u; 
        c.A0 = mem.ReadU32((c.A0 - 0x2C74u)); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V1 = mem.ReadU8(c.V1); 
        { c.V0 = 0u | 0x0001u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s | _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2C74u), c.V0); 
        goto L8019E240;
        L8019E8B4: ;
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At - 0x2CE4u), (ushort)0u); 
        goto L8019E240;
        L8019E8C4: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2D24u)); 
        if (c.V0 == 0u) {
            { c.A1 = 0u | 0x0001u;  }
            goto L8019E8EC;
        }
        { c.A1 = 0u | 0x0001u;  }
        c.V0 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 + 0x5u;  }
        mem.WriteU32(c.S1, c.V0); 
        goto L8019E240;
        L8019E8EC: ;
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
        c.A3 = mem.ReadU16((c.At + 0x958u)); 
        { c.V0 = 0u | 0x0100u;  }
        mem.WriteU32((c.SP + 0x10u), c.V0); 
        { c.V0 = 0u | 0x0030u;  }
        mem.WriteU32((c.SP + 0x14u), c.V0); 
        { c.V0 = 0u | 0x0048u;  }
        mem.WriteU32((c.SP + 0x18u), c.V0); 
        c.RA = 0x8019E97Cu; 
        SoTN_main.LoadTPage(c, m);
        goto L8019E240;
        L8019E984: ;
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
        c.RA = 0x8019E9B8u;
        Dispatcher.Call(c, m, c.V0);
        goto L8019E240;
        L8019E9C0: ;
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2C74u), 0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2D24u), 0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2C70u), 0u); 
        goto L8019E240;
        L8019E9E0: ;
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V1 = mem.ReadU8(c.V1); 
        { c.V0 = 0u | 0x0001u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _t << (int)(_s & 31u);  }
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 - 0x2C74u)); 
        { var _s = 0u; var _t = c.V0; c.V0 = ~(_s | _t);  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s & _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2C74u), c.V0); 
        goto L8019E240;
        L8019EA1C: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2D20u)); 
        c.V1 = mem.ReadU8(c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2C74u)); 
        { var _s = c.V1; var _t = c.V0; c.V0 = _t >> (int)(_s & 31u);  }
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            goto L8019F070;
        }
        c.V0 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        goto L8019E240;
        L8019EA5C: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2D24u)); 
        if (c.V0 != 0u) {
            goto L8019E240;
        }
        c.V0 = mem.ReadU8((c.S1 + 0x17u)); 
        mem.WriteU8((c.S1 + 0x16u), (byte)c.V0); 
        { c.V0 = 0u | 0x0020u;  }
        if (c.V1 != c.V0) {
            { c.A0 = c.SP + 0x20u;  }
            goto L8019EAA4;
        }
        { c.A0 = c.SP + 0x20u;  }
        c.V1 = 0x801B0000u; 
        { c.V1 = c.V1 - 0x2D16u;  }
        c.V0 = mem.ReadU16(c.V1); 
        { c.V0 = c.V0 + 0x2u;  }
        mem.WriteU16(c.V1, (ushort)c.V0); 
        goto L8019F0B4;
        L8019EAA4: ;
        { c.V0 = c.A2 & 0x000Fu;  }
        { c.V0 = c.V0 << 1;  }
        { c.V0 = c.V0 + 0x380u;  }
        mem.WriteU16((c.SP + 0x20u), (ushort)c.V0); 
        { c.V0 = c.A2 & 0x00F0u;  }
        { c.V0 = c.V0 >> 1;  }
        c.A1 = 0x801B0000u; 
        c.A1 = (uint)(short)mem.ReadU16((c.A1 - 0x2D16u)); 
        c.V1 = 0x801B0000u; 
        c.V1 = (uint)(short)mem.ReadU16((c.V1 - 0x2D12u)); 
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
        c.RA = 0x8019EB00u; 
        if (!DialogueGlyphs.Draw(c, mem)) SoTN_main.MoveImage(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x2D16u)); 
        { c.V0 = c.V0 + 0x2u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At - 0x2D16u), (ushort)c.V0); 
        goto L8019F0B4;
        L8019EB20: ;
        { c.A0 = 0u | 0x0002u;  }
        c.RA = 0x8019EB28u; 
        SoTN_rcen.RCEN_ScaleCutsceneAvatar(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = (uint)(short)mem.ReadU16((c.V0 - 0x2D10u)); 
        { c.V0 = (int)c.V0 < 6 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L8019F0B4;
        }
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L8019F0B4;
        L8019EB54: ;
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 - 0x2CF4u)); 
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
        c.V0 = mem.ReadU16((c.V0 - 0x2D10u)); 
        { c.V0 = c.V0 - 0x1u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At - 0x2D10u), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L8019F0B4;
        }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 - 0x2CECu)); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.S1 = _s + _t;  }
        if (c.S1 == 0u) {
            goto L8019F0B4;
        }
        mem.WriteU16((c.S1 + 0x32u), (ushort)0u); 
        L8019EC00: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.S1 = mem.ReadU32(c.S1); 
        if (c.S1 == 0u) {
            goto L8019F0B4;
        }
        mem.WriteU16((c.S1 + 0x32u), (ushort)0u); 
        goto L8019EC00;
        L8019EC18: ;
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 - 0x2CF4u)); 
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
        c.V1 = (uint)(short)mem.ReadU16((c.V1 - 0x2D1Cu)); 
        c.V0 = (uint)(short)mem.ReadU16((c.S1 + 0x14u)); 
        c.A0 = c.V1; 
        { c.V1 = c.V1 - 0x2u;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = (int)_s < (int)_t ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L8019EC90;
        }
        { c.V0 = c.A0 - 0x3u;  }
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x14u), (ushort)c.V0); 
        L8019EC90: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x2D10u)); 
        { c.V0 = c.V0 - 0x1u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At - 0x2D10u), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        goto L8019EF80;
        L8019ECB0: ;
        c.V1 = mem.ReadU16((c.S2 + 0x2Eu)); 
        { c.A2 = 0u | 0x0001u;  }
        if (c.V1 == c.A2) {
            { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
            goto L8019EDCC;
        }
        { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L8019ECD8;
        }
        if (c.V1 == 0u) {
            { c.A0 = 0u | 0x0002u;  }
            goto L8019ECEC;
        }
        { c.A0 = 0u | 0x0002u;  }
        goto L8019F0B4;
        L8019ECD8: ;
        { c.V0 = 0u | 0x0002u;  }
        if (c.V1 == c.V0) {
            goto L8019EEC8;
        }
        goto L8019F0B4;
        L8019ECEC: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2C70u), c.A2); 
        { c.A1 = 0u | 0x0048u;  }
        c.RA = 0x8019ED04u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.V1 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.V1 == c.V0) {
            { c.V0 = c.V1 << 1;  }
            goto L8019F0AC;
        }
        { c.V0 = c.V1 << 1;  }
        c.A2 = 0x801B0000u; 
        { c.A2 = c.A2 - 0x2CF0u;  }
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
            goto L8019EEB4;
        }
        c.A0 = 0u; 
        { c.A1 = 0u | 0x007Fu;  }
        { c.V1 = 0u | 0x00F7u;  }
        { c.T1 = c.A2 - 0x28u;  }
        { c.T0 = 0u | 0x01FEu;  }
        c.A3 = 0x80180000u; 
        { c.A3 = c.A3 + 0x964u;  }
        { c.A2 = 0u | 0x0F70u;  }
        L8019ED64: ;
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
            goto L8019ED64;
        }
        { c.A0 = c.A0 + 0x1u;  }
        goto L8019EEB4;
        L8019EDCC: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2CF0u)); 
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
            goto L8019EE60;
        }
        c.A0 = 0u; 
        { c.A1 = 0u | 0x0004u;  }
        L8019EE04: ;
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
            goto L8019EE30;
        }
        mem.WriteU16((c.S1 + 0x20u), (ushort)c.V0); 
        { c.V1 = c.V1 + 0xFu;  }
        L8019EE30: ;
        { c.V0 = (uint)((int)c.V1 >> 4);  }
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.V0); 
        { c.V0 = (int)c.V0 < 5 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L8019EE4C;
        }
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.A1); 
        goto L8019EE50;
        L8019EE4C: ;
        { c.A0 = 0u | 0x0001u;  }
        L8019EE50: ;
        c.S1 = mem.ReadU32(c.S1); 
        if (c.S1 != 0u) {
            goto L8019EE04;
        }
        L8019EE60: ;
        { c.V0 = c.A0 << 16;  }
        if (c.V0 != 0u) {
            goto L8019F0B4;
        }
        c.S0 = 0x801B0000u; 
        { c.S0 = c.S0 - 0x2CF0u;  }
        c.A0 = mem.ReadU32(c.S0); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x384Cu)); 
        c.RA = 0x8019EE8Cu;
        Dispatcher.Call(c, m, c.V0);
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 - 0x2CF4u)); 
        c.V0 = 0xFFFFFFFFu; 
        mem.WriteU32(c.S0, c.V0); 
        c.S1 = mem.ReadU32(c.S1); 
        { c.V0 = 0u | 0x0011u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0); 
        c.S1 = mem.ReadU32(c.S1); 
        { c.V0 = 0u | 0x0051u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0); 
        L8019EEB4: ;
        c.V0 = mem.ReadU16((c.S2 + 0x2Eu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S2 + 0x2Eu), (ushort)c.V0); 
        goto L8019F0B4;
        L8019EEC8: ;
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 - 0x2CF4u)); 
        c.S1 = mem.ReadU32(c.S1); 
        c.V0 = mem.ReadU8((c.S1 + 0x4u)); 
        { c.V0 = c.V0 < 0x00000010u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            c.A0 = 0u; 
            goto L8019EF04;
        }
        c.A0 = 0u; 
        mem.WriteU8((c.S1 + 0x28u), (byte)0u); 
        mem.WriteU8((c.S1 + 0x1Cu), (byte)0u); 
        mem.WriteU8((c.S1 + 0x10u), (byte)0u); 
        mem.WriteU8((c.S1 + 0x4u), (byte)0u); 
        goto L8019EF20;
        L8019EF04: ;
        c.V0 = mem.ReadU8((c.S1 + 0x28u)); 
        { c.A0 = 0u | 0x0001u;  }
        { c.V0 = c.V0 - 0x10u;  }
        mem.WriteU8((c.S1 + 0x28u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x1Cu), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x10u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x4u), (byte)c.V0); 
        L8019EF20: ;
        c.V0 = mem.ReadU8((c.S1 + 0x6u)); 
        { c.V0 = c.V0 < 0x0000007Fu ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x007Fu;  }
            goto L8019EF40;
        }
        { c.V0 = 0u | 0x007Fu;  }
        c.V0 = mem.ReadU8((c.S1 + 0x12u)); 
        { c.A0 = 0u | 0x0001u;  }
        { c.V0 = c.V0 + 0x8u;  }
        L8019EF40: ;
        mem.WriteU8((c.S1 + 0x12u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x6u), (byte)c.V0); 
        c.S1 = mem.ReadU32(c.S1); 
        c.V0 = mem.ReadU8((c.S1 + 0x4u)); 
        { c.V0 = c.V0 < 0x00000010u ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0010u;  }
            goto L8019EF70;
        }
        { c.V0 = 0u | 0x0010u;  }
        c.V0 = mem.ReadU8((c.S1 + 0x6u)); 
        { c.A0 = 0u | 0x0001u;  }
        { c.V0 = c.V0 - 0xFu;  }
        L8019EF70: ;
        mem.WriteU8((c.S1 + 0x6u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x5u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x4u), (byte)c.V0); 
        { c.V0 = c.A0 << 16;  }
        L8019EF80: ;
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L8019F0B4;
        }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L8019F0B4;
        L8019EF90: ;
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 - 0x2CF4u)); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x2D10u)); 
        c.S1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 - 0x1u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At - 0x2D10u), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        { c.V1 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V1 < 12 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L8019EFFC;
        }
        c.S1 = mem.ReadU32(c.S1); 
        c.V1 = mem.ReadU8((c.S1 + 0xCu)); 
        { c.V0 = c.V1 + 0xECu;  }
        mem.WriteU8((c.S1 + 0xCu), (byte)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x2D10u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 - 0x15u;  }
            goto L8019F0B4;
        }
        { c.V0 = c.V1 - 0x15u;  }
        mem.WriteU8((c.S1 + 0xCu), (byte)c.V0); 
        goto L8019F0B4;
        L8019EFFC: ;
        if (c.V1 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L8019F014;
        }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0); 
        goto L8019F028;
        L8019F014: ;
        c.V0 = mem.ReadU16((c.S1 + 0x2Eu)); 
        { c.V0 = c.V0 - 0x6u;  }
        mem.WriteU16((c.S1 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x22u), (ushort)c.V0); 
        L8019F028: ;
        c.S1 = mem.ReadU32(c.S1); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0); 
        goto L8019F0B4;
        L8019F038: ;
        c.V0 = mem.ReadU16((c.S2 + 0x30u)); 
        if (c.V0 == 0u) {
            goto L8019F08C;
        }
        c.V0 = 0x80040000u; 
        { c.V0 = c.V0 - 0x34FCu;  }
        c.V1 = mem.ReadU32(c.V0); 
        c.A0 = 0x00100000u; 
        goto L8019F09C;
        L8019F05C: ;
        c.V0 = mem.ReadU8(c.S4); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU8(c.S4, (byte)c.V0); 
        goto L8019F0B4;
        L8019F070: ;
        c.V1 = 0x801B0000u; 
        { c.V1 = c.V1 - 0x2D20u;  }
        c.V0 = mem.ReadU32(c.V1); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU32(c.V1, c.V0); 
        goto L8019F0B4;
        L8019F08C: ;
        c.V0 = 0x80040000u; 
        { c.V0 = c.V0 - 0x34FCu;  }
        c.V1 = mem.ReadU32(c.V0); 
        c.A0 = 0x00080000u; 
        L8019F09C: ;
        { var _s = c.V1; var _t = c.A0; c.V1 = _s | _t;  }
        mem.WriteU32(c.V0, c.V1); 
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x38FCu), 0u); 
        L8019F0AC: ;
        c.A0 = c.S2; 
        c.RA = 0x8019F0B4u; 
        SoTN_rcen.DestroyEntity_rcen(c, m);
        L8019F0B4: ;
        c.RA = mem.ReadU32((c.SP + 0x44u)); 
        c.S4 = mem.ReadU32((c.SP + 0x40u)); 
        c.S3 = mem.ReadU32((c.SP + 0x3Cu)); 
        c.S2 = mem.ReadU32((c.SP + 0x38u)); 
        c.S1 = mem.ReadU32((c.SP + 0x34u)); 
        c.S0 = mem.ReadU32((c.SP + 0x30u)); 
        { c.SP = c.SP + 0x48u;  }
        return;
        c.T2 = 0x80070000u; 
        { c.T2 = c.T2 + 0x3084u;  }
        { c.T0 = 0u | 0x0316u;  }
        { c.A0 = c.A0 & 0xFFFFu;  }
        { c.A0 = c.A0 << 4;  }
        c.V0 = 0x80180000u; 
        { c.V0 = c.V0 + 0x9FCu;  }
        { var _s = c.A0; var _t = c.V0; c.A3 = _s + _t;  }
        c.T1 = 0u; 
        L8019F0FC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A1 = 0u; 
        L8019F100: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A0 = mem.ReadU16(c.A3); 
        { c.A3 = c.A3 + 0x2u;  }
        { c.V0 = c.T0 & 0xFFFFu;  }
        { c.A2 = c.T0 + 0x1u;  }
        { c.A1 = c.A1 + 0x1u;  }
        c.V1 = mem.ReadU32(c.T2); 
        { c.V0 = c.V0 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        mem.WriteU16(c.V0, (ushort)c.A0); 
        { c.V0 = (int)c.A1 < 4 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            c.T0 = c.A2; 
            goto L8019F100;
        }
        c.T0 = c.A2; 
        { c.T1 = c.T1 + 0x1u;  }
        { c.V0 = (int)c.T1 < 2 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.T0 = c.A2 + 0x2Cu;  }
            goto L8019F0FC;
        }
        { c.T0 = c.A2 + 0x2Cu;  }
        return;
        { c.SP = c.SP - 0x20u;  }
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.S0 = c.A0; 
        { c.A1 = 0u | 0x0020u;  }
        { c.A2 = 0u | 0x0011u;  }
        { c.A3 = 0u | 0x0002u;  }
        mem.WriteU32((c.SP + 0x1Cu), c.RA); 
        mem.WriteU32((c.SP + 0x18u), c.S2); 
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        c.RA = 0x8019F170u; 
        SoTN_rcen.RCEN_GetPlayerCollisionWith(c, m);
        c.S2 = 0x80070000u; 
        { c.S2 = c.S2 + 0x33D8u;  }
        c.V1 = 0x80070000u; 
        c.V1 = mem.ReadU16((c.V1 + 0x33DAu)); 
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x308Eu)); 
        c.A0 = 0x80070000u; 
        c.A0 = mem.ReadU16((c.A0 + 0x3092u)); 
        { var _s = c.V1; var _t = c.V0; c.V1 = _s + _t;  }
        c.S1 = c.V1; 
        c.V0 = mem.ReadU16((c.S0 + 0x6u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s + _t;  }
        c.A0 = c.V0; 
        { c.V0 = c.V1 < 0x00000009u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L8019F5D4;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x5ED8u)); 
        switch (c.V0)
        {
            case 0x8019F1CCu: goto L8019F1CC;
            case 0x8019F22Cu: goto L8019F22C;
            case 0x8019F318u: goto L8019F318;
            case 0x8019F3C0u: goto L8019F3C0;
            case 0x8019F474u: goto L8019F474;
            case 0x8019F4FCu: goto L8019F4FC;
            case 0x8019F540u: goto L8019F540;
            case 0x8019F584u: goto L8019F584;
            case 0x8019F5A8u: goto L8019F5A8;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L8019F1CC: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x528u;  }
        c.RA = 0x8019F1DCu; 
        Dispatcher.Call(c, m, 0x801A4594u);
        c.A0 = 0u; 
        c.RA = 0x8019F1E4u; 
        SoTN_rcen.func_8019F0D8(c, m);
        { c.A0 = 0u | 0x001Bu;  }
        c.V0 = 0xFFFF8001u; 
        mem.WriteU16((c.S0 + 0x54u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0009u;  }
        mem.WriteU16((c.S0 + 0x56u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0080u;  }
        mem.WriteU16((c.S0 + 0x24u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0004u;  }
        mem.WriteU8((c.S0 + 0x19u), (byte)c.V0); 
        { c.V0 = 0u | 0x0800u;  }
        mem.WriteU16((c.S0 + 0x1Eu), (ushort)c.V0); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x37C0u)); 
        { c.A1 = 0u | 0x0002u;  }
        c.RA = 0x8019F224u;
        Dispatcher.Call(c, m, c.V0);
        goto L8019F5D4;
        L8019F22C: ;
        c.RA = 0x8019F234u; 
        SoTN_rcen.RCEN_GetDistanceToPlayerX(c, m);
        { c.V0 = (int)c.V0 < 32 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L8019F5D4;
        }
        c.V0 = (uint)(short)mem.ReadU16((c.S2 + 0x6u)); 
        c.V1 = (uint)(short)mem.ReadU16((c.S0 + 0x6u)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s - _t;  }
        { c.V0 = (int)c.V0 < 112 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L8019F5D4;
        }
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x80090000u; 
        mem.WriteU32((c.At + 0x7400u), c.V0); 
        { c.V0 = c.S1 << 16;  }
        { c.V1 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V1 < 385 ? 1u : 0u;  }
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x3748u), 0u); 
        if (c.V0 != 0u) {
            { c.V0 = (int)c.V1 < 384 ? 1u : 0u;  }
            goto L8019F294;
        }
        { c.V0 = (int)c.V1 < 384 ? 1u : 0u;  }
        { c.V0 = 0u | 0x8000u;  }
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EF4u), c.V0); 
        goto L8019F2B4;
        L8019F294: ;
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x2000u;  }
            goto L8019F2AC;
        }
        { c.V0 = 0u | 0x2000u;  }
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EF4u), c.V0); 
        goto L8019F2B4;
        L8019F2AC: ;
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EF4u), 0u); 
        L8019F2B4: ;
        c.V1 = 0x80070000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x2F2Cu)); 
        { c.V0 = c.V1 & 0x0001u;  }
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0008u;  }
            goto L8019F2E4;
        }
        { c.V0 = 0u | 0x0008u;  }
        { c.V0 = c.V1 & 0x0002u;  }
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0004u;  }
            goto L8019F2E4;
        }
        { c.V0 = 0u | 0x0004u;  }
        { c.V0 = c.V1 & 0x0004u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0002u;  }
            goto L8019F2EC;
        }
        { c.V0 = 0u | 0x0002u;  }
        L8019F2E4: ;
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EF4u), c.V0); 
        L8019F2EC: ;
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EFCu), c.V0); 
        { c.V0 = 0u | 0x0200u;  }
        c.At = 0x80070000u; 
        mem.WriteU8((c.At + 0x3510u), (byte)0u); 
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x30CCu), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L8019F5D0;
        L8019F318: ;
        c.V1 = 0x80070000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x2F2Cu)); 
        c.A0 = 0x80070000u; 
        { c.A0 = c.A0 + 0x2EF4u;  }
        { c.V0 = c.V1 & 0x0007u;  }
        if (c.V0 == 0u) {
            mem.WriteU32(c.A0, 0u); 
            goto L8019F384;
        }
        mem.WriteU32(c.A0, 0u); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3668u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 & 0x0001u;  }
            goto L8019F460;
        }
        { c.V0 = c.V1 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0008u;  }
            goto L8019F35C;
        }
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU32(c.A0, c.V0); 
        goto L8019F460;
        L8019F35C: ;
        { c.V0 = c.V1 & 0x0002u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0004u;  }
            goto L8019F370;
        }
        { c.V0 = 0u | 0x0004u;  }
        mem.WriteU32(c.A0, c.V0); 
        goto L8019F460;
        L8019F370: ;
        { c.V0 = c.V1 & 0x0004u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0002u;  }
            goto L8019F460;
        }
        { c.V0 = 0u | 0x0002u;  }
        mem.WriteU32(c.A0, c.V0); 
        goto L8019F460;
        L8019F384: ;
        { c.V0 = c.S1 << 16;  }
        { c.V1 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V1 < 385 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = (int)c.V1 < 384 ? 1u : 0u;  }
            goto L8019F3A0;
        }
        { c.V0 = (int)c.V1 < 384 ? 1u : 0u;  }
        { c.V0 = 0u | 0x8000u;  }
        goto L8019F3A8;
        L8019F3A0: ;
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x2000u;  }
            goto L8019F3AC;
        }
        { c.V0 = 0u | 0x2000u;  }
        L8019F3A8: ;
        mem.WriteU32(c.A0, c.V0); 
        L8019F3AC: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        goto L8019F460;
        L8019F3C0: ;
        c.A0 = 0x80070000u; 
        { c.A0 = c.A0 + 0x2EF4u;  }
        c.V1 = mem.ReadU32(c.A0); 
        { c.V0 = 0u | 0x8000u;  }
        if (c.V1 != c.V0) {
            { c.V0 = 0u | 0x2000u;  }
            goto L8019F3F4;
        }
        { c.V0 = 0u | 0x2000u;  }
        { c.V0 = c.S1 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 385 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L8019F410;
        }
        mem.WriteU32(c.A0, 0u); 
        goto L8019F410;
        L8019F3F4: ;
        if (c.V1 != c.V0) {
            { c.V0 = c.S1 << 16;  }
            goto L8019F410;
        }
        { c.V0 = c.S1 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 384 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L8019F410;
        }
        mem.WriteU32(c.A0, 0u); 
        L8019F410: ;
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x2EF4u)); 
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L8019F464;
        }
        { c.V0 = 0u | 0x0001u;  }
        c.V1 = 0x80070000u; 
        c.V1 = mem.ReadU16((c.V1 + 0x308Eu)); 
        { c.V0 = 0u | 0x0180u;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s - _t;  }
        mem.WriteU16((c.S2 + 0x2u), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0100u;  }
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x30C0u), c.V0); 
        { c.V0 = 0u | 0x0200u;  }
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x30C8u), c.V0); 
        L8019F460: ;
        { c.V0 = 0u | 0x0001u;  }
        L8019F464: ;
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EFCu), c.V0); 
        goto L8019F5D4;
        L8019F474: ;
        c.A1 = 0x80070000u; 
        { c.A1 = c.A1 + 0x2EF4u;  }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU32(c.A1, 0u); 
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EFCu), c.V0); 
        { c.V0 = 0u | 0x0180u;  }
        c.A0 = 0x80070000u; 
        c.A0 = mem.ReadU16((c.A0 + 0x308Eu)); 
        c.V1 = 0x80070000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x2F20u)); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s - _t;  }
        { c.V1 = c.V1 & 0x0001u;  }
        if (c.V1 == 0u) {
            mem.WriteU16((c.S2 + 0x2u), (ushort)c.V0); 
            goto L8019F5D4;
        }
        mem.WriteU16((c.S2 + 0x2u), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S2 + 0x14u)); 
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x2000u;  }
            goto L8019F4C4;
        }
        { c.V0 = 0u | 0x2000u;  }
        mem.WriteU32(c.A1, c.V0); 
        L8019F4C4: ;
        { c.A0 = 0u | 0x0001u;  }
        c.RA = 0x8019F4CCu; 
        SoTN_rcen.func_8019F0D8(c, m);
        { c.V0 = 0u | 0x0100u;  }
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x30C4u), c.V0); 
        c.V1 = mem.ReadU16((c.S0 + 0x2Cu)); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2C74u)); 
        { c.V1 = c.V1 + 0x1u;  }
        { c.V0 = c.V0 | 0x0001u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At - 0x2C74u), c.V0); 
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V1); 
        goto L8019F5D4;
        L8019F4FC: ;
        { c.V0 = c.A0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 264 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L8019F524;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x6u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x6u), (ushort)c.V0); 
        goto L8019F568;
        L8019F524: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3824u)); 
        { c.A0 = 0u | 0x060Du;  }
        c.RA = 0x8019F538u;
        Dispatcher.Call(c, m, c.V0);
        goto L8019F558;
        L8019F540: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2C74u)); 
        { c.V0 = c.V0 & 0x0002u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L8019F56C;
        }
        { c.V0 = 0u | 0x0001u;  }
        L8019F558: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        L8019F568: ;
        { c.V0 = 0u | 0x0001u;  }
        L8019F56C: ;
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EF4u), 0u); 
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EFCu), c.V0); 
        goto L8019F5D4;
        L8019F584: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2C74u)); 
        { c.V0 = c.V0 & 0x0004u;  }
        if (c.V0 == 0u) {
            goto L8019F5D4;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L8019F5D0;
        L8019F5A8: ;
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x80090000u; 
        mem.WriteU32((c.At + 0x7400u), 0u); 
        c.At = 0x80070000u; 
        mem.WriteU8((c.At + 0x3510u), (byte)c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V1 = 0u | 0x0001u;  }
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x3748u), c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        L8019F5D0: ;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        L8019F5D4: ;
        c.RA = mem.ReadU32((c.SP + 0x1Cu)); 
        c.S2 = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool RCEN_EntityRelicOrb(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rcen", m, false)) return true;
        RCEN_EntityRelicOrb_Body(c, m);
        return false;
    }
    private static void RCEN_EntityRelicOrb_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rcen", 0x801AD2E0u);
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
            goto L801A65AC;
        }
        { c.S4 = c.V0 & 0x7FFFu;  }
        { c.V0 = c.V1 < 0x00000005u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 < 0x0000000Au ? 1u : 0u;  }
            goto L801A65B4;
        }
        { c.V0 = c.V1 < 0x0000000Au ? 1u : 0u;  }
        c.V0 = mem.ReadU8((c.S1 + 0x48u)); 
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0005u;  }
            goto L801A65AC;
        }
        { c.V0 = 0u | 0x0005u;  }
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        c.V1 = mem.ReadU16((c.S1 + 0x2Cu)); 
        L801A65AC: ;
        { c.V0 = c.V1 < 0x0000000Au ? 1u : 0u;  }
        L801A65B4: ;
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801A6E14;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x5DCCu)); 
        switch (c.V0)
        {
            case 0x801A65D4u: goto L801A65D4;
            case 0x801A67A4u: goto L801A67A4;
            case 0x801A6E14u: goto L801A6E14;
            case 0x801A6804u: goto L801A6804;
            case 0x801A69C4u: goto L801A69C4;
            case 0x801A6A8Cu: goto L801A6A8C;
            case 0x801A6B3Cu: goto L801A6B3C;
            case 0x801A6CB4u: goto L801A6CB4;
            case 0x801A7190u: goto L801A7190;
            case 0x801A7274u: goto L801A7274;
            case 0x801A7450u: goto L801A7450;
            case 0x801A7528u: goto L801A7528;
            case 0x801A762Cu: goto L801A762C;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801A65D4: ;
        { c.V0 = c.S4 & 0xFFFFu;  }
        c.At = 0x80090000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU8((c.At + 0x7964u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 != 0u) {
            goto L801A6D14;
        }
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x504u;  }
        c.S0 = 0u; 
        c.RA = 0x801A6604u; 
        Dispatcher.Call(c, m, 0x801A4594u);
        c.S5 = 0x801B0000u; 
        { c.S5 = c.S5 - 0x2C6Cu;  }
        { c.V0 = c.S0 << 16;  }
        L801A6610: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (uint)((int)c.V0 >> 15);  }
        { var _s = c.V0; var _t = c.S5; c.V0 = _s + _t;  }
        c.V0 = mem.ReadU16(c.V0); 
        if (c.V0 == 0u) {
            { c.V0 = c.S0 + 0x1u;  }
            goto L801A6640;
        }
        { c.V0 = c.S0 + 0x1u;  }
        c.S0 = c.V0; 
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 31 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = c.S0 << 16;  }
            goto L801A6610;
        }
        { c.V0 = c.S0 << 16;  }
        L801A6640: ;
        { c.V0 = c.S0 << 16;  }
        { c.S3 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.S3 < 31 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.A0 = 0u | 0x0004u;  }
            goto L801A667C;
        }
        { c.A0 = 0u | 0x0004u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0007u;  }
        c.RA = 0x801A6668u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.S2 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.S2 != c.V0) {
            c.V1 = 0x00800000u; 
            goto L801A6684;
        }
        c.V1 = 0x00800000u; 
        L801A667C: ;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)0u); 
        goto L801A6FF8;
        L801A6684: ;
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
        c.RA = 0x801A66D4u;
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
            goto L801A6780;
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
        L801A672C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 == 0u) {
            { c.V0 = 0u | 0x0006u;  }
            goto L801A673C;
        }
        { c.V0 = 0u | 0x0006u;  }
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.T3); 
        goto L801A6768;
        L801A673C: ;
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
        L801A6768: ;
        { c.V0 = 0u | 0x007Eu;  }
        mem.WriteU16((c.A2 + 0x26u), (ushort)c.V0); 
        c.A2 = mem.ReadU32(c.A2); 
        if (c.A2 != 0u) {
            { c.A3 = c.A3 + 0x1u;  }
            goto L801A672C;
        }
        { c.A3 = c.A3 + 0x1u;  }
        L801A6780: ;
        c.V0 = 0xFFFF8000u; 
        mem.WriteU16((c.S1 + 0x4u), (ushort)c.V0); 
        { c.V0 = 0u | 0x4000u;  }
        mem.WriteU32((c.S1 + 0xCu), c.V0); 
        { c.V0 = 0u | 0x0040u;  }
        mem.WriteU16((c.S1 + 0x90u), (ushort)c.V0); 
        c.V0 = 0xFFFFFE00u; 
        mem.WriteU32((c.S1 + 0x94u), c.V0); 
        goto L801A6E14;
        L801A67A4: ;
        c.V0 = mem.ReadU32((c.S1 + 0xCu)); 
        c.V1 = mem.ReadU16((c.S1 + 0x90u)); 
        c.A0 = mem.ReadU32((c.S1 + 0x94u)); 
        { c.V1 = c.V1 - 0x1u;  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s + _t;  }
        mem.WriteU16((c.S1 + 0x90u), (ushort)c.V1); 
        { c.V1 = c.V1 & 0xFFFFu;  }
        if (c.V1 != 0u) {
            mem.WriteU32((c.S1 + 0xCu), c.V0); 
            goto L801A67DC;
        }
        mem.WriteU32((c.S1 + 0xCu), c.V0); 
        c.V0 = mem.ReadU32((c.S1 + 0x94u)); 
        { c.V1 = 0u | 0x0040u;  }
        mem.WriteU16((c.S1 + 0x90u), (ushort)c.V1); 
        { var _s = 0u; var _t = c.V0; c.V0 = _s - _t;  }
        mem.WriteU32((c.S1 + 0x94u), c.V0); 
        L801A67DC: ;
        c.RA = 0x801A67E4u; 
        SoTN_rcen.func_801A3C38(c, m);
        c.V0 = (uint)(short)mem.ReadU16((c.S1 + 0x8Cu)); 
        { c.V1 = 0u | 0x0010u;  }
        { c.V0 = c.V0 << 1;  }
        c.At = 0x801B0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        mem.WriteU16((c.At - 0x2C6Cu), (ushort)c.V1); 
        goto L801A6E14;
        L801A6804: ;
        { c.S0 = c.S4 & 0xFFFFu;  }
        c.A0 = c.S0; 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x37B8u)); 
        { c.A1 = 0u | 0x2000u;  }
        c.RA = 0x801A6820u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.S0 < 0x00000016u ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.A0 = c.SP + 0x20u;  }
            goto L801A6858;
        }
        { c.A0 = c.SP + 0x20u;  }
        { c.V0 = c.S0 < 0x00000014u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            c.A1 = 0u; 
            goto L801A685C;
        }
        c.A1 = 0u; 
        c.At = 0x80090000u; 
        { var _s = c.At; var _t = c.S0; c.At = _s + _t;  }
        c.V0 = mem.ReadU8((c.At + 0x7964u)); 
        { c.V0 = c.V0 ^ 0x0002u;  }
        c.At = 0x80090000u; 
        { var _s = c.At; var _t = c.S0; c.At = _s + _t;  }
        mem.WriteU8((c.At + 0x7964u), (byte)c.V0); 
        L801A6858: ;
        c.A1 = 0u; 
        L801A685C: ;
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
        c.RA = 0x801A6894u; 
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
        L801A68C8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 != 0u) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801A6914;
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
        goto L801A69A0;
        L801A6914: ;
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
            goto L801A6984;
        }
        mem.WriteU8((c.A2 + 0x6u), (byte)0u); 
        mem.WriteU8((c.A2 + 0x2Au), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x1Eu), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x12u), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x6u), (byte)c.A0); 
        goto L801A6994;
        L801A6984: ;
        mem.WriteU8((c.A2 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x1Du), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x11u), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x5u), (byte)c.A0); 
        L801A6994: ;
        { c.V0 = 0u | 0x01FDu;  }
        mem.WriteU16((c.A2 + 0x26u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0011u;  }
        L801A69A0: ;
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.V0); 
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 + 0x1u;  }
        { c.V0 = (int)c.A3 < 3 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A68C8;
        }
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801A6CAC;
        L801A69C4: ;
        c.S0 = 0u; 
        c.S3 = 0x80080000u; 
        { c.S3 = c.S3 - 0x101Cu;  }
        c.V0 = c.S3; 
        c.A0 = 0x80180000u; 
        c.A0 = mem.ReadU32((c.A0 + 0x1380u)); 
        { c.A3 = 0u | 0x0BFFu;  }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        L801A69E4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU8(c.V0, (byte)0u); 
        { c.A3 = c.A3 - 0x1u;  }
        if ((int)c.A3 >= 0) {
            { c.V0 = c.V0 + 0x1u;  }
            goto L801A69E4;
        }
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        { c.S2 = c.S4 << 4;  }
        L801A69FC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.A0); 
        if (c.V0 != 0u) {
            { c.A1 = c.SP + 0x28u;  }
            goto L801A6A30;
        }
        { c.A1 = c.SP + 0x28u;  }
        if (c.S0 != 0u) {
            { c.S0 = 0u | 0x0001u;  }
            goto L801A6A44;
        }
        { c.S0 = 0u | 0x0001u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x37B0u)); 
        { var _s = c.S2; var _t = c.V0; c.V0 = _s + _t;  }
        c.A0 = mem.ReadU32(c.V0); 
        goto L801A69FC;
        L801A6A30: ;
        c.A2 = c.S3; 
        { c.A3 = 0u | 0x00C0u;  }
        c.RA = 0x801A6A3Cu; 
        Dispatcher.Call(c, m, 0x801A62A0u);
        c.A0 = c.V0; 
        goto L801A69FC;
        L801A6A44: ;
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
        c.RA = 0x801A6A70u; 
        SoTN_main.LoadTPage(c, m);
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)0u); 
        c.V1 = mem.ReadU16((c.SP + 0x28u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x7Eu), (ushort)c.V1); 
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        goto L801A6E14;
        L801A6A8C: ;
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
        L801A6AB4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 != 0u) {
            goto L801A6AE8;
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
        goto L801A6B10;
        L801A6AE8: ;
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
        L801A6B10: ;
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.A0); 
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.A0); 
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.A1); 
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.A1); 
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 + 0x1u;  }
        { c.V0 = (int)c.A3 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A6C84;
        }
        goto L801A6AB4;
        L801A6B3C: ;
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
        L801A6BB4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 != 0u) {
            { c.V0 = c.A0 + 0x68u;  }
            goto L801A6C14;
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
        c.V0 = mem.ReadU16((c.At + 0x1394u)); 
        { c.V0 = c.V0 + 0xA7u;  }
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V0); 
        c.V0 = mem.ReadU8((c.A2 + 0x2Au)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.A1; c.At = _s + _t;  }
        c.V1 = mem.ReadU16((c.At + 0x1384u)); 
        { c.V0 = c.V0 - 0x10u;  }
        { c.V1 = c.V1 + 0xA7u;  }
        mem.WriteU8((c.A2 + 0x2Au), (byte)c.V0); 
        mem.WriteU8((c.A2 + 0x1Eu), (byte)c.V0); 
        goto L801A6C68;
        L801A6C14: ;
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0098u;  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s - _t;  }
        mem.WriteU16((c.A2 + 0x14u), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0x2Cu), (ushort)c.T1); 
        mem.WriteU16((c.A2 + 0x20u), (ushort)c.T0); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.A1; c.At = _s + _t;  }
        c.V0 = mem.ReadU16((c.At + 0x13A4u)); 
        { c.V0 = c.V0 + 0xA7u;  }
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V0); 
        c.V0 = mem.ReadU8((c.A2 + 0x11u)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.A1; c.At = _s + _t;  }
        c.V1 = mem.ReadU16((c.At + 0x13B4u)); 
        { c.V0 = c.V0 - 0x10u;  }
        { c.V1 = c.V1 + 0xA7u;  }
        mem.WriteU8((c.A2 + 0x11u), (byte)c.V0); 
        mem.WriteU8((c.A2 + 0x5u), (byte)c.V0); 
        L801A6C68: ;
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.V1); 
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.V1); 
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 + 0x1u;  }
        { c.V0 = (int)c.A3 < 3 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A6BB4;
        }
        L801A6C84: ;
        c.V0 = mem.ReadU16((c.S1 + 0x7Cu)); 
        { c.V1 = 0u | 0x0008u;  }
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)c.V0); 
        { c.V0 = c.V0 & 0xFFFFu;  }
        if (c.V0 != c.V1) {
            goto L801A6E14;
        }
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)0u); 
        { c.V0 = c.V0 + 0x1u;  }
        L801A6CAC: ;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        goto L801A6E14;
        L801A6CB4: ;
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
            goto L801A6E14;
        }
        L801A6D14: ;
        c.A0 = c.S1; 
        c.RA = 0x801A6D1Cu; 
        SoTN_rcen.DestroyEntity_rcen(c, m);
        goto L801A6FF8;
        L801A6D24: ;
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
        c.V0 = mem.ReadU16((c.At + 0x13C4u)); 
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
        c.V1 = mem.ReadU16((c.At + 0x13D4u)); 
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
        goto L801A6EA4;
        L801A6E14: ;
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        { c.V0 = c.V0 < 0x00000002u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A6ECC;
        }
        c.A1 = 0x80040000u; 
        c.A1 = mem.ReadU16((c.A1 - 0x3668u)); 
        c.A0 = c.S1; 
        c.RA = 0x801A6E38u; 
        SoTN_rcen.RCEN_BlinkItem(c, m);
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
        L801A6E60: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 - 0x1u;  }
        if ((int)c.A3 >= 0) {
            goto L801A6E60;
        }
        c.V0 = mem.ReadU16((c.S1 + 0x80u)); 
        if (c.V0 != 0u) {
            c.A3 = 0u; 
            goto L801A6EBC;
        }
        c.A3 = 0u; 
        { c.V1 = 0u | 0x0008u;  }
        L801A6E84: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU16((c.A2 + 0x32u)); 
        if (c.V0 == c.V1) {
            { c.A3 = c.A3 + 0x1u;  }
            goto L801A6D24;
        }
        { c.A3 = c.A3 + 0x1u;  }
        c.A2 = mem.ReadU32(c.A2); 
        { c.V0 = (int)c.A3 < 4 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A6E84;
        }
        L801A6EA4: ;
        c.V0 = mem.ReadU16((c.S1 + 0x82u)); 
        { c.V1 = 0u | 0x0004u;  }
        mem.WriteU16((c.S1 + 0x80u), (ushort)c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x82u), (ushort)c.V0); 
        goto L801A6ECC;
        L801A6EBC: ;
        c.V0 = mem.ReadU16((c.S1 + 0x80u)); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU16((c.S1 + 0x80u), (ushort)c.V0); 
        L801A6ECC: ;
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
        L801A6EF4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 - 0x1u;  }
        if ((int)c.A3 >= 0) {
            goto L801A6EF4;
        }
        if (c.A2 == 0u) {
            { c.A3 = 0u | 0x0008u;  }
            goto L801A6FF8;
        }
        { c.A3 = 0u | 0x0008u;  }
        L801A6F0C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU16((c.A2 + 0x32u)); 
        if (c.V0 == c.A3) {
            goto L801A6FE8;
        }
        c.V0 = mem.ReadU8((c.A2 + 0x13u)); 
        { c.V0 = c.V0 & 0x0003u;  }
        if (c.V0 == 0u) {
            goto L801A6F54;
        }
        c.V1 = mem.ReadU16((c.A2 + 0x16u)); 
        c.A0 = mem.ReadU16((c.A2 + 0x2Eu)); 
        { c.V0 = c.V1 - 0x1u;  }
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.V0); 
        { c.V0 = c.A0 - 0x1u;  }
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V1); 
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.A0); 
        goto L801A6F84;
        L801A6F54: ;
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
        L801A6F84: ;
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
            goto L801A6FE8;
        }
        mem.WriteU8((c.A2 + 0x6u), (byte)c.V0); 
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.A3); 
        L801A6FE8: ;
        c.A2 = mem.ReadU32(c.A2); 
        if (c.A2 != 0u) {
            goto L801A6F0C;
        }
        L801A6FF8: ;
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
            goto L801A70DC;
        }
        { c.V0 = c.V0 < 0x00000005u ? 1u : 0u;  }
        c.V0 = mem.ReadU16((c.A2 + 0x30u)); 
        { c.A0 = c.V0 + 0x258u;  }
        c.V0 = c.A0; 
        mem.WriteU16((c.A2 + 0xB4u), (ushort)c.V0); 
        { c.V0 = c.V0 & 0xFFFFu;  }
        { c.V0 = c.V0 >> 3;  }
        c.At = 0x80040000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU8((c.At - 0x4114u)); 
        { c.V1 = c.A0 & 0x0007u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = (uint)((int)_t >> (int)(_s & 31u));  }
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.A0 = c.A0 - 0x258u;  }
            goto L801A7088;
        }
        { c.A0 = c.A0 - 0x258u;  }
        c.A0 = c.A2; 
        c.RA = 0x801A7080u; 
        SoTN_rcen.DestroyEntity_rcen(c, m);
        goto L801A7138;
        L801A7088: ;
        { c.V0 = c.A0 & 0xFFFFu;  }
        { c.V0 = c.V0 << 1;  }
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A0 = mem.ReadU16((c.At + 0x68Cu)); 
        { c.V0 = c.A0 < 0x00000080u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A70BC;
        }
        c.V0 = 0x801A0000u; 
        { c.V0 = c.V0 + 0x52A4u;  }
        mem.WriteU32((c.A2 + 0xB8u), c.V0); 
        goto L801A70CC;
        L801A70BC: ;
        c.V0 = 0x801A0000u; 
        { c.V0 = c.V0 + 0x5CB8u;  }
        mem.WriteU32((c.A2 + 0xB8u), c.V0); 
        { c.A0 = c.A0 - 0x80u;  }
        L801A70CC: ;
        { c.V0 = 0u | 0x8000u;  }
        { var _s = c.A0; var _t = c.V0; c.V0 = _s + _t;  }
        mem.WriteU16((c.A2 + 0x30u), (ushort)c.V0); 
        goto L801A7128;
        L801A70DC: ;
        c.A0 = mem.ReadU16((c.A2 + 0xB4u)); 
        if (c.V0 == 0u) {
            goto L801A7128;
        }
        c.V0 = mem.ReadU8((c.A2 + 0x48u)); 
        if (c.V0 == 0u) {
            { c.A1 = c.A0 >> 3;  }
            goto L801A7128;
        }
        { c.A1 = c.A0 >> 3;  }
        { c.A0 = c.A0 & 0x0007u;  }
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
        L801A7128: ;
        c.V0 = mem.ReadU32((c.A2 + 0xB8u)); 
        c.A0 = c.A2; 
        c.RA = 0x801A7138u;
        Dispatcher.Call(c, m, c.V0);
        L801A7138: ;
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
            goto L801A7684;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x5DA4u)); 
        switch (c.V0)
        {
            case 0x801A7190u: goto L801A7190;
            case 0x801A7274u: goto L801A7274;
            case 0x801A7450u: goto L801A7450;
            case 0x801A7528u: goto L801A7528;
            case 0x801A762Cu: goto L801A762C;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801A7190: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x504u;  }
        c.RA = 0x801A71A0u; 
        Dispatcher.Call(c, m, 0x801A4594u);
        c.V1 = 0x00010000u; 
        c.V0 = mem.ReadU32((c.S0 + 0x34u)); 
        c.A0 = mem.ReadU16((c.S0 + 0x30u)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        c.V1 = 0x08000000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s ^ _t;  }
        if (c.A0 != 0u) {
            mem.WriteU32((c.S0 + 0x34u), c.V0); 
            goto L801A71C8;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0060u;  }
        mem.WriteU16((c.S0 + 0x30u), (ushort)c.V0); 
        L801A71C8: ;
        { c.A0 = 0u | 0x0004u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0003u;  }
        c.RA = 0x801A71E0u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.A0 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.A0 != c.V0) {
            { c.V0 = c.A0 << 1;  }
            goto L801A71FC;
        }
        { c.V0 = c.A0 << 1;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)0u); 
        goto L801A7684;
        L801A71FC: ;
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
            goto L801A7248;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        L801A7234: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            goto L801A7234;
        }
        L801A7248: ;
        c.A0 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.V0 = mem.ReadU8(c.A0); 
        mem.WriteU16((c.S0 + 0x80u), (ushort)c.V0); 
        c.V0 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.V1 = mem.ReadU8((c.A0 + 0x1u)); 
        { c.V0 = c.V0 + 0x2u;  }
        mem.WriteU32((c.S0 + 0x7Cu), c.V0); 
        mem.WriteU16((c.S0 + 0x82u), (ushort)c.V1); 
        goto L801A7684;
        L801A7274: ;
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
        c.RA = 0x801A72A4u; 
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
            goto L801A7444;
        }
        c.A2 = 0u; 
        { c.A0 = 0u | 0x0080u;  }
        L801A72D8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801A7350;
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
        goto L801A7434;
        L801A7350: ;
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
            goto L801A73F0;
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
        goto L801A7414;
        L801A73F0: ;
        c.V1 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V0 = mem.ReadU16((c.S0 + 0x6u)); 
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x11u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x5u), (byte)c.A0); 
        { c.V1 = c.V1 >> 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 + 0x4u;  }
        L801A7414: ;
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0); 
        { c.V0 = 0u | 0x01FCu;  }
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0011u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        L801A7434: ;
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801A72D8;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801A7444: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801A7624;
        L801A7450: ;
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
            goto L801A74AC;
        }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        L801A7480: ;
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
            goto L801A7480;
        }
        { c.S1 = c.S1 + 0x1u;  }
        c.S1 = c.S2; 
        L801A74AC: ;
        { c.S3 = 0u | 0x0001u;  }
        L801A74B0: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.A0); 
        if (c.V0 == 0u) {
            c.A1 = 0u; 
            goto L801A74F8;
        }
        c.A1 = 0u; 
        if (c.V0 != c.S3) {
            { c.A1 = c.SP + 0x28u;  }
            goto L801A74E0;
        }
        { c.A1 = c.SP + 0x28u;  }
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        { c.A0 = c.A0 + 0x1u;  }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        { c.V0 = c.V0 << 3;  }
        { var _s = c.S2; var _t = c.V0; c.S1 = _s + _t;  }
        goto L801A74B0;
        L801A74E0: ;
        c.A3 = mem.ReadU16((c.S0 + 0x80u)); 
        c.A2 = c.S1; 
        { c.A3 = c.A3 >> 1;  }
        c.RA = 0x801A74F0u; 
        Dispatcher.Call(c, m, 0x801A62A0u);
        c.A0 = c.V0; 
        goto L801A74B0;
        L801A74F8: ;
        c.A0 = c.S2; 
        { c.V0 = 0u | 0x0180u;  }
        mem.WriteU32((c.SP + 0x10u), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        c.A2 = 0u; 
        mem.WriteU32((c.SP + 0x14u), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.A3 = 0u; 
        mem.WriteU32((c.SP + 0x18u), c.V0); 
        c.RA = 0x801A7520u; 
        SoTN_main.LoadTPage(c, m);
        goto L801A7618;
        L801A7528: ;
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
            goto L801A7608;
        }
        c.A2 = 0u; 
        L801A7564: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            goto L801A75B4;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = c.V0 + 0x8u;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        c.V0 = c.LO; 
        if ((int)c.V0 >= 0) {
            goto L801A758C;
        }
        { c.V0 = c.V0 + 0x7u;  }
        L801A758C: ;
        { c.V0 = (uint)((int)c.V0 >> 3);  }
        c.V1 = mem.ReadU16((c.A1 + 0xAu)); 
        c.A0 = mem.ReadU8((c.A1 + 0x12u)); 
        { var _s = c.V1; var _t = c.V0; c.V1 = _s + _t;  }
        { c.A0 = c.A0 - 0x10u;  }
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V1); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V1); 
        mem.WriteU8((c.A1 + 0x12u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x6u), (byte)c.A0); 
        goto L801A75F8;
        L801A75B4: ;
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = c.V0 + 0x8u;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        c.V0 = c.LO; 
        if ((int)c.V0 >= 0) {
            goto L801A75D4;
        }
        { c.V0 = c.V0 + 0x7u;  }
        L801A75D4: ;
        { c.V0 = (uint)((int)c.V0 >> 3);  }
        c.V1 = mem.ReadU16((c.A1 + 0x22u)); 
        c.A0 = mem.ReadU8((c.A1 + 0x29u)); 
        { var _s = c.V1; var _t = c.V0; c.V1 = _s - _t;  }
        { c.A0 = c.A0 - 0x10u;  }
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V1); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V1); 
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0); 
        L801A75F8: ;
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801A7564;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801A7608: ;
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = 0u | 0x0008u;  }
        if (c.V1 != c.V0) {
            goto L801A7684;
        }
        L801A7618: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        mem.WriteU16((c.S0 + 0x86u), (ushort)0u); 
        { c.V0 = c.V0 + 0x1u;  }
        L801A7624: ;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        goto L801A7684;
        L801A762C: ;
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
            goto L801A7684;
        }
        c.A0 = c.S0; 
        c.RA = 0x801A7684u; 
        SoTN_rcen.DestroyEntity_rcen(c, m);
        L801A7684: ;
        c.RA = mem.ReadU32((c.SP + 0x48u)); 
        c.S3 = mem.ReadU32((c.SP + 0x44u)); 
        c.S2 = mem.ReadU32((c.SP + 0x40u)); 
        c.S1 = mem.ReadU32((c.SP + 0x3Cu)); 
        c.S0 = mem.ReadU32((c.SP + 0x38u)); 
        { c.SP = c.SP + 0x50u;  }
        return;
        }
    public static bool RCEN_EntityMessageBox(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("rcen", m, false)) return true;
        RCEN_EntityMessageBox_Body(c, m);
        return false;
    }
    private static void RCEN_EntityMessageBox_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "rcen", 0x801AD2E0u);
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
            goto L801A7684;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At - 0x5DA4u)); 
        switch (c.V0)
        {
            case 0x801A7190u: goto L801A7190;
            case 0x801A7274u: goto L801A7274;
            case 0x801A7450u: goto L801A7450;
            case 0x801A7528u: goto L801A7528;
            case 0x801A762Cu: goto L801A762C;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801A7190: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x504u;  }
        c.RA = 0x801A71A0u; 
        Dispatcher.Call(c, m, 0x801A4594u);
        c.V1 = 0x00010000u; 
        c.V0 = mem.ReadU32((c.S0 + 0x34u)); 
        c.A0 = mem.ReadU16((c.S0 + 0x30u)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        c.V1 = 0x08000000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s ^ _t;  }
        if (c.A0 != 0u) {
            mem.WriteU32((c.S0 + 0x34u), c.V0); 
            goto L801A71C8;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0060u;  }
        mem.WriteU16((c.S0 + 0x30u), (ushort)c.V0); 
        L801A71C8: ;
        { c.A0 = 0u | 0x0004u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0003u;  }
        c.RA = 0x801A71E0u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.A0 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.A0 != c.V0) {
            { c.V0 = c.A0 << 1;  }
            goto L801A71FC;
        }
        { c.V0 = c.A0 << 1;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)0u); 
        goto L801A7684;
        L801A71FC: ;
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
            goto L801A7248;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        L801A7234: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            goto L801A7234;
        }
        L801A7248: ;
        c.A0 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.V0 = mem.ReadU8(c.A0); 
        mem.WriteU16((c.S0 + 0x80u), (ushort)c.V0); 
        c.V0 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.V1 = mem.ReadU8((c.A0 + 0x1u)); 
        { c.V0 = c.V0 + 0x2u;  }
        mem.WriteU32((c.S0 + 0x7Cu), c.V0); 
        mem.WriteU16((c.S0 + 0x82u), (ushort)c.V1); 
        goto L801A7684;
        L801A7274: ;
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
        c.RA = 0x801A72A4u; 
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
            goto L801A7444;
        }
        c.A2 = 0u; 
        { c.A0 = 0u | 0x0080u;  }
        L801A72D8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801A7350;
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
        goto L801A7434;
        L801A7350: ;
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
            goto L801A73F0;
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
        goto L801A7414;
        L801A73F0: ;
        c.V1 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V0 = mem.ReadU16((c.S0 + 0x6u)); 
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x11u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x5u), (byte)c.A0); 
        { c.V1 = c.V1 >> 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 + 0x4u;  }
        L801A7414: ;
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0); 
        { c.V0 = 0u | 0x01FCu;  }
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0011u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        L801A7434: ;
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801A72D8;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801A7444: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801A7624;
        L801A7450: ;
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
            goto L801A74AC;
        }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        L801A7480: ;
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
            goto L801A7480;
        }
        { c.S1 = c.S1 + 0x1u;  }
        c.S1 = c.S2; 
        L801A74AC: ;
        { c.S3 = 0u | 0x0001u;  }
        L801A74B0: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.A0); 
        if (c.V0 == 0u) {
            c.A1 = 0u; 
            goto L801A74F8;
        }
        c.A1 = 0u; 
        if (c.V0 != c.S3) {
            { c.A1 = c.SP + 0x28u;  }
            goto L801A74E0;
        }
        { c.A1 = c.SP + 0x28u;  }
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        { c.A0 = c.A0 + 0x1u;  }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        { c.V0 = c.V0 << 3;  }
        { var _s = c.S2; var _t = c.V0; c.S1 = _s + _t;  }
        goto L801A74B0;
        L801A74E0: ;
        c.A3 = mem.ReadU16((c.S0 + 0x80u)); 
        c.A2 = c.S1; 
        { c.A3 = c.A3 >> 1;  }
        c.RA = 0x801A74F0u; 
        Dispatcher.Call(c, m, 0x801A62A0u);
        c.A0 = c.V0; 
        goto L801A74B0;
        L801A74F8: ;
        c.A0 = c.S2; 
        { c.V0 = 0u | 0x0180u;  }
        mem.WriteU32((c.SP + 0x10u), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        c.A2 = 0u; 
        mem.WriteU32((c.SP + 0x14u), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.A3 = 0u; 
        mem.WriteU32((c.SP + 0x18u), c.V0); 
        c.RA = 0x801A7520u; 
        SoTN_main.LoadTPage(c, m);
        goto L801A7618;
        L801A7528: ;
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
            goto L801A7608;
        }
        c.A2 = 0u; 
        L801A7564: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            goto L801A75B4;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = c.V0 + 0x8u;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        c.V0 = c.LO; 
        if ((int)c.V0 >= 0) {
            goto L801A758C;
        }
        { c.V0 = c.V0 + 0x7u;  }
        L801A758C: ;
        { c.V0 = (uint)((int)c.V0 >> 3);  }
        c.V1 = mem.ReadU16((c.A1 + 0xAu)); 
        c.A0 = mem.ReadU8((c.A1 + 0x12u)); 
        { var _s = c.V1; var _t = c.V0; c.V1 = _s + _t;  }
        { c.A0 = c.A0 - 0x10u;  }
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V1); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V1); 
        mem.WriteU8((c.A1 + 0x12u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x6u), (byte)c.A0); 
        goto L801A75F8;
        L801A75B4: ;
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = c.V0 + 0x8u;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        c.V0 = c.LO; 
        if ((int)c.V0 >= 0) {
            goto L801A75D4;
        }
        { c.V0 = c.V0 + 0x7u;  }
        L801A75D4: ;
        { c.V0 = (uint)((int)c.V0 >> 3);  }
        c.V1 = mem.ReadU16((c.A1 + 0x22u)); 
        c.A0 = mem.ReadU8((c.A1 + 0x29u)); 
        { var _s = c.V1; var _t = c.V0; c.V1 = _s - _t;  }
        { c.A0 = c.A0 - 0x10u;  }
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V1); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V1); 
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0); 
        L801A75F8: ;
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801A7564;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801A7608: ;
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = 0u | 0x0008u;  }
        if (c.V1 != c.V0) {
            goto L801A7684;
        }
        L801A7618: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        mem.WriteU16((c.S0 + 0x86u), (ushort)0u); 
        { c.V0 = c.V0 + 0x1u;  }
        L801A7624: ;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        goto L801A7684;
        L801A762C: ;
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
            goto L801A7684;
        }
        c.A0 = c.S0; 
        c.RA = 0x801A7684u; 
        SoTN_rcen.DestroyEntity_rcen(c, m);
        L801A7684: ;
        c.RA = mem.ReadU32((c.SP + 0x48u)); 
        c.S3 = mem.ReadU32((c.SP + 0x44u)); 
        c.S2 = mem.ReadU32((c.SP + 0x40u)); 
        c.S1 = mem.ReadU32((c.SP + 0x3Cu)); 
        c.S0 = mem.ReadU32((c.SP + 0x38u)); 
        { c.SP = c.SP + 0x50u;  }
        return;
        }
}
