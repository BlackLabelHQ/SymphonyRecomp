using RecompOne.Runtime.Context;
using RecompOne.Runtime.Dispatch;
using RecompOne.Runtime.Memory;

namespace Recompiled.Translation;

public static class ScriptFunctions_bo2
{
    public static bool BO2_CutsceneUnk1(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo2", m, true)) return true;
        BO2_CutsceneUnk1_Body(c, m);
        return false;
    }
    private static void BO2_CutsceneUnk1_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo2", 0x801B6840u);
        { c.V0 = 0u | 0x0002u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x684Cu), (ushort)c.V0); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x684Au), (ushort)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x6848u)); 
        { c.V1 = 0u | 0x0008u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x684Eu), (ushort)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x6852u), (ushort)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU8((c.At + 0x6856u), (byte)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU8((c.At + 0x6857u), (byte)c.V1); 
        { c.V0 = c.V0 + 0x14u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x6846u), (ushort)c.V0); 
        return;
        }
    public static bool BO2_SetCutsceneScript(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo2", m, true)) return true;
        BO2_SetCutsceneScript_Body(c, m);
        return false;
    }
    private static void BO2_SetCutsceneScript_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo2", 0x801B6840u);
        c.A0 = DialogueScripts.ResolveStart("bo2", c.A0, m);
        { c.SP = c.SP - 0x20u;  }
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        c.S1 = c.A0; 
        { c.A0 = 0u | 0x0006u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0007u;  }
        mem.WriteU32((c.SP + 0x18u), c.RA); 
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.RA = 0x801A54DCu;
        Dispatcher.Call(c, m, c.V0);
        c.S0 = 0x801B0000u; 
        { c.S0 = c.S0 + 0x6878u;  }
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        c.V1 = 0xFFFFFFFFu; 
        if (c.V0 != c.V1) {
            mem.WriteU32(c.S0, c.V0); 
            goto L801A5504;
        }
        mem.WriteU32(c.S0, c.V0); 
        mem.WriteU32(c.S0, 0u); 
        c.V0 = 0u; 
        goto L801A5660;
        L801A5504: ;
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x6840u), c.S1); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x687Cu), (ushort)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x6874u), c.V1); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x6870u), c.V1); 
        c.RA = 0x801A552Cu; 
        SoTN_bo2.BO2_CutsceneUnk1(c, m);
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
        mem.WriteU32((c.At + 0x6858u), c.V0); 
        c.V1 = mem.ReadU32(c.V0); 
        { c.A2 = 0u | 0x0008u;  }
        mem.WriteU16((c.V0 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x685Cu), c.V1); 
        c.V0 = mem.ReadU32(c.V1); 
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x6860u), c.V0); 
        c.V1 = mem.ReadU32(c.V0); 
        mem.WriteU16((c.V0 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x6864u), c.V1); 
        c.A0 = mem.ReadU32(c.V1); 
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x6868u), c.A0); 
        c.A1 = mem.ReadU32(c.A0); 
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.A0 + 0x32u), (ushort)c.A2); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x686Cu), c.A1); 
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
        L801A5660: ;
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool BO2_CutsceneUnk3(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo2", m, false)) return true;
        BO2_CutsceneUnk3_Body(c, m);
        return false;
    }
    private static void BO2_CutsceneUnk3_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo2", 0x801B6840u);
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
        c.RA = 0x801A56C4u; 
        DialogueGlyphs.Clear(c, mem);
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool BO2_CutsceneUnk4(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo2", m, true)) return true;
        BO2_CutsceneUnk4_Body(c, m);
        return false;
    }
    private static void BO2_CutsceneUnk4_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo2", 0x801B6840u);
        DialogueGlyphs.PrepareLine(c, mem);
        { c.SP = c.SP - 0x18u;  }
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.S0 = 0x801B0000u; 
        { c.S0 = c.S0 + 0x684Eu;  }
        mem.WriteU32((c.SP + 0x14u), c.RA); 
        c.A0 = (uint)(short)mem.ReadU16(c.S0); 
        c.RA = 0x801A56F4u; 
        SoTN_bo2.BO2_CutsceneUnk3(c, m);
        c.V0 = (uint)(short)mem.ReadU16(c.S0); 
        { c.V0 = c.V0 << 2;  }
        { var _s = c.S0; var _t = c.V0; c.V0 = _s + _t;  }
        c.V1 = mem.ReadU32((c.V0 + 0xAu)); 
        { c.V0 = 0u | 0x0010u;  }
        mem.WriteU16((c.V1 + 0x1Au), (ushort)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x6854u)); 
        mem.WriteU16((c.V1 + 0xEu), (ushort)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x6846u)); 
        mem.WriteU8((c.V1 + 0xCu), (byte)0u); 
        mem.WriteU16((c.V1 + 0xAu), (ushort)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x6844u)); 
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
    public static bool BO2_DrawCutsceneActorName(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo2", m, true)) return true;
        BO2_DrawCutsceneActorName_Body(c, m);
        return false;
    }
    private static void BO2_DrawCutsceneActorName_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo2", 0x801B6840u);
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
        c.A2 = mem.ReadU32((c.At + 0xC20u)); 
        { c.V0 = 0u | 0x00FFu;  }
        L801A57C0: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V1 = mem.ReadU8(c.A2); 
        if (c.V1 != c.V0) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801A57E0;
        }
        { c.A2 = c.A2 + 0x1u;  }
        c.V1 = mem.ReadU8(c.A2); 
        if (c.V1 == 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801A57F0;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801A57E0: ;
        if (c.V1 == 0u) {
            goto L801A57C0;
        }
        { c.A1 = c.A1 + 0x1u;  }
        goto L801A57C0;
        L801A57F0: ;
        { c.A0 = 0u | 0x0006u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = c.A1 & 0xFFFFu;  }
        c.RA = 0x801A5808u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.A1 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.A1 != c.V0) {
            { c.V0 = c.A1 << 1;  }
            goto L801A582C;
        }
        { c.V0 = c.A1 << 1;  }
        c.A0 = c.S1; 
        c.RA = 0x801A5824u; 
        Dispatcher.Call(c, m, 0x801AAA88u);
        goto L801A58F8;
        L801A582C: ;
        { var _s = c.V0; var _t = c.A1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.A1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A0 = _s + _t;  }
        c.V1 = 0x801B0000u; 
        { c.V1 = c.V1 + 0x6874u;  }
        { c.V0 = c.S0 & 0xFFFFu;  }
        { c.V0 = c.V0 << 2;  }
        mem.WriteU32(c.V1, c.A1); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A2 = mem.ReadU32((c.At + 0xC20u)); 
        if (c.A0 == 0u) {
            { c.A3 = 0u | 0x0038u;  }
            goto L801A58F8;
        }
        { c.A3 = 0u | 0x0038u;  }
        { c.T4 = 0u | 0x0006u;  }
        { c.T3 = 0u | 0x001Eu;  }
        { c.T2 = 0u | 0x0196u;  }
        { c.T1 = 0u | 0x01FFu;  }
        { c.A1 = 0u | 0x0008u;  }
        { c.T0 = c.V1 - 0x2Cu;  }
        L801A5888: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V1 = mem.ReadU8(c.A2); 
        if (c.V1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801A58A0;
        }
        { c.A2 = c.A2 + 0x1u;  }
        { c.A3 = c.A3 + 0x4u;  }
        goto L801A58EC;
        L801A58A0: ;
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
        L801A58EC: ;
        if (c.A0 != 0u) {
            goto L801A5888;
        }
        L801A58F8: ;
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool func_801A5910(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo2", m, true)) return true;
        func_801A5910_Body(c, m);
        return false;
    }
    private static void func_801A5910_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo2", 0x801B6840u);
        c.V0 = 0x00100000u; 
        { var _s = c.A0; var _t = c.V0; c.A0 = _s + _t;  }
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x6880u), c.A0); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x687Eu), (ushort)0u); 
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x687Cu), (ushort)c.V0); 
        return;
        }
    public static bool func_801A593C_bo2(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo2", m, true)) return true;
        func_801A593C_bo2_Body(c, m);
        return false;
    }
    private static void func_801A593C_bo2_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo2", 0x801B6840u);
        { c.SP = c.SP - 0x20u;  }
        c.A0 = 0x801B0000u; 
        { c.A0 = c.A0 + 0x687Eu;  }
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
            goto L801A599C;
        }
        { c.S1 = c.A0 + 0x2u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x687Cu), (ushort)0u); 
        goto L801A5BCC;
        L801A598C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU32(c.S1, c.A1); 
        goto L801A5BCC;
        L801A5994: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU32(c.S1, c.A2); 
        goto L801A5BCC;
        L801A599C: ;
        { c.S2 = 0u | 0x0001u;  }
        L801A59A0: ;
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
            goto L801A598C;
        }
        { c.V0 = c.A1 + 0x3u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V1 = mem.ReadU8((c.A1 + 0x2u)); 
        if (c.V1 == c.S2) {
            { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
            goto L801A5B18;
        }
        { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A5A00;
        }
        if (c.V1 == 0u) {
            { c.V0 = c.A1 + 0x4u;  }
            goto L801A5A1C;
        }
        { c.V0 = c.A1 + 0x4u;  }
        goto L801A59A0;
        L801A5A00: ;
        { c.V0 = 0u | 0x0002u;  }
        if (c.V1 == c.V0) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801A5B54;
        }
        { c.V0 = 0u | 0x0003u;  }
        if (c.V1 == c.V0) {
            { c.V0 = c.A1 + 0x4u;  }
            goto L801A5BA4;
        }
        { c.V0 = c.A1 + 0x4u;  }
        goto L801A59A0;
        L801A5A1C: ;
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
        c.RA = 0x801A5A50u; 
        Dispatcher.Call(c, m, 0x801AAA88u);
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8(c.V1); 
        mem.WriteU16((c.S0 + 0x26u), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x26u)); 
        { c.V0 = c.V0 << 2;  }
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x410u)); 
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
        goto L801A59A0;
        L801A5B18: ;
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
        c.RA = 0x801A5B4Cu; 
        Dispatcher.Call(c, m, 0x801AAA88u);
        goto L801A59A0;
        L801A5B54: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x6880u)); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x68ECu)); 
        c.V0 = mem.ReadU8(c.V0); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _t >> (int)(_s & 31u);  }
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.A1 + 0x4u;  }
            goto L801A5994;
        }
        { c.V0 = c.A1 + 0x4u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8((c.A1 + 0x3u)); 
        { var _s = c.V0; var _t = c.S2; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = 0u; var _t = c.V0; c.V0 = ~(_s | _t);  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s & _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x68ECu), c.V0); 
        goto L801A59A0;
        L801A5BA4: ;
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8((c.A1 + 0x3u)); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x68ECu)); 
        { var _s = c.V0; var _t = c.S2; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x68ECu), c.V0); 
        goto L801A59A0;
        L801A5BCC: ;
        c.RA = mem.ReadU32((c.SP + 0x1Cu)); 
        c.S2 = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool BO2_CutsceneSkip(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo2", m, true)) return true;
        BO2_CutsceneSkip_Body(c, m);
        return false;
    }
    private static void BO2_CutsceneSkip_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo2", 0x801B6840u);
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
            goto L801A5CAC;
        }
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        c.S1 = 0x80040000u; 
        { c.S1 = c.S1 - 0x384Cu;  }
        c.A0 = mem.ReadU32((c.S0 + 0x64u)); 
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x683Cu), c.V0); 
        c.S2 = 0xFFFFFFFFu; 
        c.RA = 0x801A5C34u;
        Dispatcher.Call(c, m, c.V1);
        c.V0 = mem.ReadU32((c.S0 + 0x34u)); 
        c.V1 = 0x00800000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s ^ _t;  }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        c.A0 = 0x801B0000u; 
        c.A0 = mem.ReadU32((c.A0 + 0x6874u)); 
        if (c.A0 == c.S2) {
            goto L801A5C68;
        }
        c.V0 = mem.ReadU32(c.S1); 
        c.RA = 0x801A5C68u;
        Dispatcher.Call(c, m, c.V0);
        L801A5C68: ;
        c.A0 = 0x801B0000u; 
        c.A0 = mem.ReadU32((c.A0 + 0x6870u)); 
        if (c.A0 == c.S2) {
            goto L801A5C8C;
        }
        c.V0 = mem.ReadU32(c.S1); 
        c.RA = 0x801A5C8Cu;
        Dispatcher.Call(c, m, c.V0);
        L801A5C8C: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3824u)); 
        { c.A0 = 0u | 0x000Au;  }
        c.RA = 0x801A5CA0u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x2Eu), (ushort)0u); 
        L801A5CAC: ;
        c.RA = mem.ReadU32((c.SP + 0x1Cu)); 
        c.S2 = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool BO2_ScaleCutsceneAvatar(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo2", m, true)) return true;
        BO2_ScaleCutsceneAvatar_Body(c, m);
        return false;
    }
    private static void BO2_ScaleCutsceneAvatar_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo2", 0x801B6840u);
        c.V0 = 0x801B0000u; 
        c.V0 = (uint)(short)mem.ReadU16((c.V0 + 0x684Eu)); 
        { c.A3 = c.V0 + (uint)(mem.LineCount - DialogueGlyphs.VisibleLines(mem));  }
        { c.V0 = (int)c.A3 < 5 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A5CF8;
        }
        { c.A3 = c.A3 - 0x5u;  }
        L801A5CE8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (int)c.A3 < 5 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.A3 = c.A3 - 0x5u;  }
            goto L801A5CE8;
        }
        { c.A3 = c.A3 - 0x5u;  }
        { c.A3 = c.A3 + 0x5u;  }
        L801A5CF8: ;
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C48u)); 
        c.V0 = mem.ReadU16((c.V0 + 0x2Eu)); 
        if (c.V0 != 0u) {
            c.V1 = 0u; 
            goto L801A5D74;
        }
        c.V1 = 0u; 
        { c.V0 = c.A3 << 2;  }
        c.At = 0x801B0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A1 = mem.ReadU32((c.At + 0x6858u)); 
        c.V0 = mem.ReadU8((c.A1 + 0x19u)); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s - _t;  }
        mem.WriteU8((c.A1 + 0x19u), (byte)c.V0); 
        c.V0 = mem.ReadU8((c.A1 + 0xDu)); 
        c.V1 = mem.ReadU8((c.A1 + 0x19u)); 
        { var _s = c.A0; var _t = c.V0; c.V0 = _s + _t;  }
        if (c.V1 != 0u) {
            mem.WriteU8((c.A1 + 0xDu), (byte)c.V0); 
            goto L801A5D70;
        }
        mem.WriteU8((c.A1 + 0xDu), (byte)c.V0); 
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C48u)); 
        c.V1 = mem.ReadU16((c.V0 + 0x2Eu)); 
        { c.V1 = c.V1 + 0x1u;  }
        mem.WriteU16((c.V0 + 0x2Eu), (ushort)c.V1); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        L801A5D70: ;
        c.V1 = 0u; 
        L801A5D74: ;
        { c.A0 = c.A0 & 0x00FFu;  }
        c.A2 = 0x801B0000u; 
        { c.A2 = c.A2 + 0x6858u;  }
        L801A5D80: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.V1 == c.A3) {
            goto L801A5DA0;
        }
        c.A1 = mem.ReadU32(c.A2); 
        c.V0 = mem.ReadU16((c.A1 + 0xAu)); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s - _t;  }
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0); 
        L801A5DA0: ;
        { c.V1 = c.V1 + 0x1u;  }
        { c.V0 = (int)c.V1 < 5 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.A2 = c.A2 + 0x4u;  }
            goto L801A5D80;
        }
        { c.A2 = c.A2 + 0x4u;  }
        c.V1 = 0x801B0000u; 
        { c.V1 = c.V1 + 0x6850u;  }
        c.V0 = mem.ReadU16(c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16(c.V1, (ushort)c.V0); 
        return;
        }
    public static bool func_801A5DCC(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo2", m, true)) return true;
        func_801A5DCC_Body(c, m);
        return false;
    }
    private static void func_801A5DCC_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo2", 0x801B6840u);
        { c.SP = c.SP - 0x48u;  }
        mem.WriteU32((c.SP + 0x38u), c.S2); 
        c.S2 = c.A0; 
        c.A1 = 0x801B0000u; 
        c.A1 = mem.ReadU32((c.A1 + 0x6840u)); 
        c.A0 = 0x801A0000u; 
        { c.A0 = c.A0 + 0x1F84u;  }
        mem.WriteU32((c.SP + 0x44u), c.RA); 
        mem.WriteU32((c.SP + 0x40u), c.S4); 
        mem.WriteU32((c.SP + 0x3Cu), c.S3); 
        mem.WriteU32((c.SP + 0x34u), c.S1); 
        mem.WriteU32((c.SP + 0x30u), c.S0); 
        c.RA = 0x801A5E00u; 
        SoTN_main.FntPrint(c, m);
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        if (c.V0 == 0u) {
            goto L801A5EB8;
        }
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x68F0u)); 
        if (c.V0 == 0u) {
            goto L801A5E8C;
        }
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x683Cu)); 
        if (c.V0 != 0u) {
            goto L801A5E8C;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x4220u)); 
        if (c.V0 != 0u) {
            goto L801A5E84;
        }
        c.V0 = mem.ReadU16((c.S2 + 0x30u)); 
        if (c.V0 == 0u) {
            goto L801A5E6C;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x34FCu)); 
        { c.V0 = c.V0 & 0x4000u;  }
        goto L801A5E7C;
        L801A5E6C: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x34FCu)); 
        { c.V0 = c.V0 & 0x0010u;  }
        L801A5E7C: ;
        if (c.V0 == 0u) {
            goto L801A5E8C;
        }
        L801A5E84: ;
        c.A0 = c.S2; 
        c.RA = 0x801A5E8Cu; 
        SoTN_bo2.BO2_CutsceneSkip(c, m);
        L801A5E8C: ;
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        if (c.V0 == 0u) {
            goto L801A5EB8;
        }
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x687Cu)); 
        if (c.V0 == 0u) {
            goto L801A5EB8;
        }
        c.RA = 0x801A5EB8u; 
        Dispatcher.Call(c, m, 0x801A593Cu);
        L801A5EB8: ;
        c.V1 = mem.ReadU16((c.S2 + 0x2Cu)); 
        { c.V0 = c.V1 < 0x00000008u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801A6ED4;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x1F94u)); 
        switch (c.V0)
        {
            case 0x801A5EE4u: goto L801A5EE4;
            case 0x801A5FFCu: goto L801A5FFC;
            case 0x801A68E8u: goto L801A68E8;
            case 0x801A691Cu: goto L801A691C;
            case 0x801A69E0u: goto L801A69E0;
            case 0x801A6A78u: goto L801A6A78;
            case 0x801A6D68u: goto L801A6D68;
            case 0x801A6E10u: goto L801A6E10;
            case 0x801A6074u: goto L801A6074;
            case 0x801A6080u: goto L801A6080;
            case 0x801A612Cu: goto L801A612C;
            case 0x801A6158u: goto L801A6158;
            case 0x801A6188u: goto L801A6188;
            case 0x801A61C8u: goto L801A61C8;
            case 0x801A6300u: goto L801A6300;
            case 0x801A6378u: goto L801A6378;
            case 0x801A640Cu: goto L801A640C;
            case 0x801A6434u: goto L801A6434;
            case 0x801A645Cu: goto L801A645C;
            case 0x801A6494u: goto L801A6494;
            case 0x801A64DCu: goto L801A64DC;
            case 0x801A6010u: goto L801A6010;
            case 0x801A6538u: goto L801A6538;
            case 0x801A659Cu: goto L801A659C;
            case 0x801A65FCu: goto L801A65FC;
            case 0x801A6648u: goto L801A6648;
            case 0x801A667Cu: goto L801A667C;
            case 0x801A668Cu: goto L801A668C;
            case 0x801A674Cu: goto L801A674C;
            case 0x801A6788u: goto L801A6788;
            case 0x801A67A8u: goto L801A67A8;
            case 0x801A6ED4u: goto L801A6ED4;
            case 0x801A67E4u: goto L801A67E4;
            case 0x801A6F5Cu: goto L801A6F5C;
            case 0x801A6F74u: goto L801A6F74;
            case 0x801A7078u: goto L801A7078;
            case 0x801A70E8u: goto L801A70E8;
            case 0x801A7164u: goto L801A7164;
            case 0x801A71CCu: goto L801A71CC;
            case 0x801A7230u: goto L801A7230;
            case 0x801A72A4u: goto L801A72A4;
            case 0x801A73A8u: goto L801A73A8;
            case 0x801A73E8u: goto L801A73E8;
            case 0x801A7410u: goto L801A7410;
            case 0x801A7470u: goto L801A7470;
            case 0x801A7498u: goto L801A7498;
            case 0x801A74D0u: goto L801A74D0;
            case 0x801A750Cu: goto L801A750C;
            case 0x801A7564u: goto L801A7564;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801A5EE4: ;
        c.V0 = mem.ReadU16((c.S2 + 0x30u)); 
        if (c.V0 == 0u) {
            goto L801A5F40;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU8((c.V0 - 0x4160u)); 
        if (c.V0 != 0u) {
            goto L801A6AE0;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3660u)); 
        if (c.V0 != 0u) {
            goto L801A6AE0;
        }
        c.V0 = 0x80090000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x7914u)); 
        if (c.V0 != 0u) {
            goto L801A6AE0;
        }
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x1DC5u;  }
        goto L801A5F98;
        L801A5F40: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU8((c.V0 - 0x4160u)); 
        if (c.V0 != 0u) {
            goto L801A6AE0;
        }
        c.V0 = 0x80180000u; 
        c.V0 = mem.ReadU32((c.V0 + 0xB78u)); 
        if (c.V0 != 0u) {
            goto L801A6AE0;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3660u)); 
        if (c.V0 != 0u) {
            goto L801A6AE0;
        }
        c.V0 = 0x80090000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x7914u)); 
        if (c.V0 != 0u) {
            goto L801A6AE0;
        }
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x1CCCu;  }
        L801A5F98: ;
        c.RA = 0x801A5FA0u; 
        SoTN_bo2.BO2_SetCutsceneScript(c, m);
        { c.S0 = c.V0 & 0x00FFu;  }
        if (c.S0 == 0u) {
            c.A0 = 0x00800000u; 
            goto L801A6ED4;
        }
        c.A0 = 0x00800000u; 
        { c.A0 = c.A0 | 0x2000u;  }
        c.V0 = mem.ReadU32((c.S2 + 0x34u)); 
        { c.V1 = 0u | 0x0001u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x68ECu), 0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x68F0u), 0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x683Cu), 0u); 
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x38FCu), c.V1); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s | _t;  }
        mem.WriteU32((c.S2 + 0x34u), c.V0); 
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x6878u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU32((c.S2 + 0x64u), c.V1); 
        goto L801A6ED4;
        L801A5FFC: ;
        c.S4 = 0x801B0000u; 
        { c.S4 = c.S4 + 0x6856u;  }
        { c.S1 = c.S4 - 0x16u;  }
        { c.S0 = c.S4 - 0xCu;  }
        { c.S3 = c.S4 - 0x12u;  }
        L801A6010: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.S4); 
        if (c.V0 == 0u) {
            goto L801A6034;
        }
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x683Cu)); 
        if (c.V0 == 0u) {
            goto L801A6E60;
        }
        L801A6034: ;
        c.V0 = mem.ReadU32(c.S1); 
        { c.V1 = c.V0 + 0x1u;  }
        mem.WriteU32(c.S1, c.V1); 
        c.A2 = mem.ReadU8(c.V0); 
        { c.V1 = c.A2 & 0xFFFFu;  }
        { c.V0 = c.V1 < 0x00000019u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801A6824;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x1FB4u)); 
        switch (c.V0)
        {
            case 0x801A6074u: goto L801A6074;
            case 0x801A6080u: goto L801A6080;
            case 0x801A612Cu: goto L801A612C;
            case 0x801A6158u: goto L801A6158;
            case 0x801A6188u: goto L801A6188;
            case 0x801A61C8u: goto L801A61C8;
            case 0x801A6300u: goto L801A6300;
            case 0x801A6378u: goto L801A6378;
            case 0x801A640Cu: goto L801A640C;
            case 0x801A6434u: goto L801A6434;
            case 0x801A645Cu: goto L801A645C;
            case 0x801A6494u: goto L801A6494;
            case 0x801A64DCu: goto L801A64DC;
            case 0x801A6010u: goto L801A6010;
            case 0x801A6538u: goto L801A6538;
            case 0x801A659Cu: goto L801A659C;
            case 0x801A65FCu: goto L801A65FC;
            case 0x801A6648u: goto L801A6648;
            case 0x801A667Cu: goto L801A667C;
            case 0x801A668Cu: goto L801A668C;
            case 0x801A674Cu: goto L801A674C;
            case 0x801A6788u: goto L801A6788;
            case 0x801A67A8u: goto L801A67A8;
            case 0x801A6ED4u: goto L801A6ED4;
            case 0x801A67E4u: goto L801A67E4;
            case 0x801A6F5Cu: goto L801A6F5C;
            case 0x801A6F74u: goto L801A6F74;
            case 0x801A7078u: goto L801A7078;
            case 0x801A70E8u: goto L801A70E8;
            case 0x801A7164u: goto L801A7164;
            case 0x801A71CCu: goto L801A71CC;
            case 0x801A7230u: goto L801A7230;
            case 0x801A72A4u: goto L801A72A4;
            case 0x801A73A8u: goto L801A73A8;
            case 0x801A73E8u: goto L801A73E8;
            case 0x801A7410u: goto L801A7410;
            case 0x801A7470u: goto L801A7470;
            case 0x801A7498u: goto L801A7498;
            case 0x801A74D0u: goto L801A74D0;
            case 0x801A750Cu: goto L801A750C;
            case 0x801A7564u: goto L801A7564;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801A6074: ;
        { c.V0 = 0u | 0x0007u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A6ED4;
        L801A6080: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x683Cu)); 
        if (c.V0 != 0u) {
            goto L801A6010;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x8u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x2u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 != 0u) {
            mem.WriteU16(c.S0, (ushort)c.V1); 
            goto L801A60B8;
        }
        mem.WriteU16(c.S0, (ushort)c.V1); 
        c.V0 = mem.ReadU16((c.S0 - 0x4u)); 
        { c.V0 = c.V0 + 0xCu;  }
        mem.WriteU16((c.S0 - 0x4u), (ushort)c.V0); 
        L801A60B8: ;
        c.V0 = mem.ReadU16((c.S0 + 0x4u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x4u), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 5 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A60E0;
        }
        mem.WriteU16((c.S0 + 0x4u), (ushort)0u); 
        L801A60E0: ;
        c.RA = 0x801A60E8u; 
        SoTN_bo2.BO2_CutsceneUnk4(c, m);
        c.V1 = mem.ReadU16((c.S0 + 0x8u)); 
        { c.V0 = c.V1 & 0x0001u;  }
        if (c.V0 != 0u) {
            goto L801A6114;
        }
        c.V0 = (uint)(short)mem.ReadU16((c.S0 + 0x4u)); 
        { c.V0 = (int)c.V0 < 4 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = c.V1 | 0x0001u;  }
            goto L801A6010;
        }
        { c.V0 = c.V1 | 0x0001u;  }
        mem.WriteU16((c.S0 + 0x8u), (ushort)c.V0); 
        L801A6114: ;
        mem.WriteU16((c.S0 + 0x6u), (ushort)0u); 
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        mem.WriteU16((c.S2 + 0x2Eu), (ushort)0u); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A6ED4;
        L801A612C: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x6840u)); 
        { c.V1 = c.V0 + 0x1u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x6840u), c.V1); 
        c.V0 = mem.ReadU8(c.V0); 
        c.At = 0x801B0000u; 
        mem.WriteU8((c.At + 0x6857u), (byte)c.V0); 
        goto L801A6010;
        L801A6158: ;
        c.V1 = mem.ReadU32((c.S4 - 0x16u)); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32((c.S4 - 0x16u), c.V0); 
        c.V0 = mem.ReadU8(c.V1); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x683Cu)); 
        if (c.V1 != 0u) {
            mem.WriteU8(c.S4, (byte)c.V0); 
            goto L801A6010;
        }
        mem.WriteU8(c.S4, (byte)c.V0); 
        goto L801A6ED4;
        L801A6188: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x683Cu)); 
        if (c.V0 != 0u) {
            { c.V1 = 0u | 0x0008u;  }
            goto L801A6010;
        }
        { c.V1 = 0u | 0x0008u;  }
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x6858u)); 
        c.S0 = 0u; 
        { c.S0 = c.S0 + 0x1u;  }
        L801A61AC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (int)c.S0 < 5 ? 1u : 0u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V1); 
        c.S1 = mem.ReadU32(c.S1); 
        if (c.V0 != 0u) {
            { c.S0 = c.S0 + 0x1u;  }
            goto L801A61AC;
        }
        { c.S0 = c.S0 + 0x1u;  }
        goto L801A6ED4;
        L801A61C8: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x683Cu)); 
        if (c.V0 != 0u) {
            goto L801A6448;
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
        c.A0 = mem.ReadU8((c.At + 0xB7Cu)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A1 = mem.ReadU8((c.At + 0xB80u)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V1; c.At = _s + _t;  }
        c.V1 = mem.ReadU16((c.At + 0xB84u)); 
        { c.V0 = 0u | 0x0090u;  }
        mem.WriteU16((c.S1 + 0x1Au), (ushort)c.V0); 
        { c.V0 = c.T2 & 0x0080u;  }
        if (c.V0 == 0u) {
            mem.WriteU16((c.S1 + 0xEu), (ushort)c.V1); 
            goto L801A624C;
        }
        mem.WriteU16((c.S1 + 0xEu), (ushort)c.V1); 
        c.V1 = c.A0; 
        { c.V0 = c.V1 + 0x2Fu;  }
        goto L801A6254;
        L801A624C: ;
        c.V0 = c.A0; 
        { c.V1 = c.V0 + 0x2Fu;  }
        L801A6254: ;
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
        c.V0 = mem.ReadU16((c.At + 0xB8Cu)); 
        mem.WriteU16((c.S3 + 0x10u), (ushort)c.V0); 
        c.RA = 0x801A62CCu; 
        SoTN_bo2.BO2_CutsceneUnk1(c, m);
        c.RA = 0x801A62D4u; 
        SoTN_bo2.BO2_CutsceneUnk4(c, m);
        { c.A0 = c.S0 & 0xFFFFu;  }
        c.A1 = c.S2; 
        { c.V0 = 0u | 0x01FEu;  }
        mem.WriteU16((c.S1 + 0x26u), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x32u), (ushort)0u); 
        c.RA = 0x801A62ECu; 
        SoTN_bo2.BO2_DrawCutsceneActorName(c, m);
        { c.V0 = 0u | 0x0006u;  }
        mem.WriteU16((c.S3 + 0xCu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0003u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A6ED4;
        L801A6300: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x683Cu)); 
        if (c.V0 != 0u) {
            { c.V1 = 0u | 0x0008u;  }
            goto L801A6010;
        }
        { c.V1 = 0u | 0x0008u;  }
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x6858u)); 
        c.S0 = 0u; 
        { c.S0 = c.S0 + 0x1u;  }
        L801A6324: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (int)c.S0 < 5 ? 1u : 0u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V1); 
        c.S1 = mem.ReadU32(c.S1); 
        if (c.V0 != 0u) {
            { c.S0 = c.S0 + 0x1u;  }
            goto L801A6324;
        }
        { c.S0 = c.S0 + 0x1u;  }
        c.S0 = 0x801B0000u; 
        { c.S0 = c.S0 + 0x6874u;  }
        c.A0 = mem.ReadU32(c.S0); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x384Cu)); 
        c.RA = 0x801A6358u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0xFFFFFFFFu; 
        mem.WriteU32(c.S0, c.V0); 
        { c.V0 = 0u | 0x0006u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x6850u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0004u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A6ED4;
        L801A6378: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x683Cu)); 
        if (c.V0 != 0u) {
            goto L801A6448;
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
        goto L801A6ED4;
        L801A640C: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x683Cu)); 
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0018u;  }
            goto L801A6010;
        }
        { c.V0 = 0u | 0x0018u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x6850u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0006u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A6ED4;
        L801A6434: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x683Cu)); 
        if (c.V0 == 0u) {
            goto L801A674C;
        }
        L801A6448: ;
        c.V0 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 + 0x2u;  }
        mem.WriteU32(c.S1, c.V0); 
        goto L801A6010;
        L801A645C: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x683Cu)); 
        if (c.V0 != 0u) {
            goto L801A6010;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3808u)); 
        c.RA = 0x801A6484u;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 == 0u) {
            goto L801A64C8;
        }
        goto L801A6010;
        L801A6494: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x683Cu)); 
        if (c.V0 != 0u) {
            goto L801A6010;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3808u)); 
        c.RA = 0x801A64BCu;
        Dispatcher.Call(c, m, c.V0);
        { c.V1 = 0u | 0x0001u;  }
        if (c.V0 != c.V1) {
            goto L801A6010;
        }
        L801A64C8: ;
        c.V0 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        goto L801A6ED4;
        L801A64DC: ;
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
        c.RA = 0x801A6530u; 
        Dispatcher.Call(c, m, 0x801A5910u);
        goto L801A6010;
        L801A6538: ;
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
        goto L801A65A0;
        L801A659C: ;
        c.V1 = mem.ReadU32(c.S1); 
        L801A65A0: ;
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
        c.V1 = mem.ReadU32((c.V1 + 0x6840u)); 
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
        goto L801A6010;
        L801A65FC: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x6840u)); 
        c.A0 = 0x801B0000u; 
        c.A0 = mem.ReadU32((c.A0 + 0x68ECu)); 
        c.V1 = mem.ReadU8(c.V0); 
        { var _s = c.V1; var _t = c.A0; c.V0 = _t >> (int)(_s & 31u);  }
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A6E74;
        }
        { c.V0 = 0u | 0x0001u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = 0u; var _t = c.V0; c.V0 = ~(_s | _t);  }
        c.V1 = mem.ReadU32(c.S1); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s & _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x68ECu), c.V0); 
        { c.V1 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V1); 
        goto L801A6010;
        L801A6648: ;
        c.V1 = mem.ReadU32(c.S1); 
        c.A0 = 0x801B0000u; 
        c.A0 = mem.ReadU32((c.A0 + 0x68ECu)); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V1 = mem.ReadU8(c.V1); 
        { c.V0 = 0u | 0x0001u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s | _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x68ECu), c.V0); 
        goto L801A6010;
        L801A667C: ;
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x687Cu), (ushort)0u); 
        goto L801A6010;
        L801A668C: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x683Cu)); 
        if (c.V0 == 0u) {
            { c.A1 = 0u | 0x0001u;  }
            goto L801A66B4;
        }
        { c.A1 = 0u | 0x0001u;  }
        c.V0 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 + 0x5u;  }
        mem.WriteU32(c.S1, c.V0); 
        goto L801A6010;
        L801A66B4: ;
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
        c.A3 = mem.ReadU16((c.At + 0xB88u)); 
        { c.V0 = 0u | 0x0100u;  }
        mem.WriteU32((c.SP + 0x10u), c.V0); 
        { c.V0 = 0u | 0x0030u;  }
        mem.WriteU32((c.SP + 0x14u), c.V0); 
        { c.V0 = 0u | 0x0048u;  }
        mem.WriteU32((c.SP + 0x18u), c.V0); 
        c.RA = 0x801A6744u; 
        SoTN_main.LoadTPage(c, m);
        goto L801A6010;
        L801A674C: ;
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
        c.RA = 0x801A6780u;
        Dispatcher.Call(c, m, c.V0);
        goto L801A6010;
        L801A6788: ;
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x68ECu), 0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x683Cu), 0u); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x68F0u), 0u); 
        goto L801A6010;
        L801A67A8: ;
        c.V1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.V1 = mem.ReadU8(c.V1); 
        { c.V0 = 0u | 0x0001u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _t << (int)(_s & 31u);  }
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x68ECu)); 
        { var _s = 0u; var _t = c.V0; c.V0 = ~(_s | _t);  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s & _t;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x68ECu), c.V0); 
        goto L801A6010;
        L801A67E4: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x6840u)); 
        c.V1 = mem.ReadU8(c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x68ECu)); 
        { var _s = c.V1; var _t = c.V0; c.V0 = _t >> (int)(_s & 31u);  }
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            goto L801A6E74;
        }
        c.V0 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        goto L801A6010;
        L801A6824: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x683Cu)); 
        if (c.V0 != 0u) {
            goto L801A6010;
        }
        c.V0 = mem.ReadU8((c.S1 + 0x17u)); 
        mem.WriteU8((c.S1 + 0x16u), (byte)c.V0); 
        { c.V0 = 0u | 0x0020u;  }
        if (c.V1 != c.V0) {
            { c.A0 = c.SP + 0x20u;  }
            goto L801A686C;
        }
        { c.A0 = c.SP + 0x20u;  }
        c.V1 = 0x801B0000u; 
        { c.V1 = c.V1 + 0x684Au;  }
        c.V0 = mem.ReadU16(c.V1); 
        { c.V0 = c.V0 + 0x2u;  }
        mem.WriteU16(c.V1, (ushort)c.V0); 
        goto L801A6ED4;
        L801A686C: ;
        { c.V0 = c.A2 & 0x000Fu;  }
        { c.V0 = c.V0 << 1;  }
        { c.V0 = c.V0 + 0x380u;  }
        mem.WriteU16((c.SP + 0x20u), (ushort)c.V0); 
        { c.V0 = c.A2 & 0x00F0u;  }
        { c.V0 = c.V0 >> 1;  }
        c.A1 = 0x801B0000u; 
        c.A1 = (uint)(short)mem.ReadU16((c.A1 + 0x684Au)); 
        c.V1 = 0x801B0000u; 
        c.V1 = (uint)(short)mem.ReadU16((c.V1 + 0x684Eu)); 
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
        c.RA = 0x801A68C8u; 
        if (!DialogueGlyphs.Draw(c, mem)) SoTN_main.MoveImage(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x684Au)); 
        { c.V0 = c.V0 + 0x2u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x684Au), (ushort)c.V0); 
        goto L801A6ED4;
        L801A68E8: ;
        { c.A0 = 0u | 0x0002u;  }
        c.RA = 0x801A68F0u; 
        SoTN_bo2.BO2_ScaleCutsceneAvatar(c, m);
        c.V0 = 0x801B0000u; 
        c.V0 = (uint)(short)mem.ReadU16((c.V0 + 0x6850u)); 
        { c.V0 = (int)c.V0 < 6 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A6ED4;
        }
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A6ED4;
        L801A691C: ;
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x686Cu)); 
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
        c.V0 = mem.ReadU16((c.V0 + 0x6850u)); 
        { c.V0 = c.V0 - 0x1u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x6850u), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A6ED4;
        }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        c.V1 = 0x801B0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x6874u)); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.S1 = _s + _t;  }
        if (c.S1 == 0u) {
            goto L801A6ED4;
        }
        mem.WriteU16((c.S1 + 0x32u), (ushort)0u); 
        L801A69C8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.S1 = mem.ReadU32(c.S1); 
        if (c.S1 == 0u) {
            goto L801A6ED4;
        }
        mem.WriteU16((c.S1 + 0x32u), (ushort)0u); 
        goto L801A69C8;
        L801A69E0: ;
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x686Cu)); 
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
        c.V1 = (uint)(short)mem.ReadU16((c.V1 + 0x6844u)); 
        c.V0 = (uint)(short)mem.ReadU16((c.S1 + 0x14u)); 
        c.A0 = c.V1; 
        { c.V1 = c.V1 - 0x2u;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = (int)_s < (int)_t ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A6A58;
        }
        { c.V0 = c.A0 - 0x3u;  }
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x14u), (ushort)c.V0); 
        L801A6A58: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x6850u)); 
        { c.V0 = c.V0 - 0x1u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x6850u), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        goto L801A6D58;
        L801A6A78: ;
        c.V1 = mem.ReadU16((c.S2 + 0x2Eu)); 
        { c.A2 = 0u | 0x0001u;  }
        if (c.V1 == c.A2) {
            { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
            goto L801A6BA4;
        }
        { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A6AA0;
        }
        if (c.V1 == 0u) {
            { c.A0 = 0u | 0x0002u;  }
            goto L801A6AB4;
        }
        { c.A0 = 0u | 0x0002u;  }
        goto L801A6ED4;
        L801A6AA0: ;
        { c.V0 = 0u | 0x0002u;  }
        if (c.V1 == c.V0) {
            goto L801A6CA0;
        }
        goto L801A6ED4;
        L801A6AB4: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x68F0u), c.A2); 
        { c.A1 = 0u | 0x0048u;  }
        c.RA = 0x801A6ACCu;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.V1 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.V1 != c.V0) {
            { c.V0 = c.V1 << 1;  }
            goto L801A6AF0;
        }
        { c.V0 = c.V1 << 1;  }
        L801A6AE0: ;
        c.A0 = c.S2; 
        c.RA = 0x801A6AE8u; 
        Dispatcher.Call(c, m, 0x801AAA88u);
        goto L801A6ED4;
        L801A6AF0: ;
        c.A2 = 0x801B0000u; 
        { c.A2 = c.A2 + 0x6870u;  }
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
            goto L801A6C8C;
        }
        c.A0 = 0u; 
        { c.A1 = 0u | 0x007Fu;  }
        { c.V1 = 0u | 0x00F7u;  }
        { c.T1 = c.A2 - 0x28u;  }
        { c.T0 = 0u | 0x01FEu;  }
        c.A3 = 0x80180000u; 
        { c.A3 = c.A3 + 0xB90u;  }
        { c.A2 = 0u | 0x0F70u;  }
        L801A6B3C: ;
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
            goto L801A6B3C;
        }
        { c.A0 = c.A0 + 0x1u;  }
        goto L801A6C8C;
        L801A6BA4: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x6870u)); 
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
            goto L801A6C38;
        }
        c.A0 = 0u; 
        { c.A1 = 0u | 0x0004u;  }
        L801A6BDC: ;
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
            goto L801A6C08;
        }
        mem.WriteU16((c.S1 + 0x20u), (ushort)c.V0); 
        { c.V1 = c.V1 + 0xFu;  }
        L801A6C08: ;
        { c.V0 = (uint)((int)c.V1 >> 4);  }
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.V0); 
        { c.V0 = (int)c.V0 < 5 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A6C24;
        }
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.A1); 
        goto L801A6C28;
        L801A6C24: ;
        { c.A0 = 0u | 0x0001u;  }
        L801A6C28: ;
        c.S1 = mem.ReadU32(c.S1); 
        if (c.S1 != 0u) {
            goto L801A6BDC;
        }
        L801A6C38: ;
        { c.V0 = c.A0 << 16;  }
        if (c.V0 != 0u) {
            goto L801A6ED4;
        }
        c.S0 = 0x801B0000u; 
        { c.S0 = c.S0 + 0x6870u;  }
        c.A0 = mem.ReadU32(c.S0); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x384Cu)); 
        c.RA = 0x801A6C64u;
        Dispatcher.Call(c, m, c.V0);
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x686Cu)); 
        c.V0 = 0xFFFFFFFFu; 
        mem.WriteU32(c.S0, c.V0); 
        c.S1 = mem.ReadU32(c.S1); 
        { c.V0 = 0u | 0x0011u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0); 
        c.S1 = mem.ReadU32(c.S1); 
        { c.V0 = 0u | 0x0051u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0); 
        L801A6C8C: ;
        c.V0 = mem.ReadU16((c.S2 + 0x2Eu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S2 + 0x2Eu), (ushort)c.V0); 
        goto L801A6ED4;
        L801A6CA0: ;
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x686Cu)); 
        c.S1 = mem.ReadU32(c.S1); 
        c.V0 = mem.ReadU8((c.S1 + 0x4u)); 
        { c.V0 = c.V0 < 0x00000010u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            c.A0 = 0u; 
            goto L801A6CDC;
        }
        c.A0 = 0u; 
        mem.WriteU8((c.S1 + 0x28u), (byte)0u); 
        mem.WriteU8((c.S1 + 0x1Cu), (byte)0u); 
        mem.WriteU8((c.S1 + 0x10u), (byte)0u); 
        mem.WriteU8((c.S1 + 0x4u), (byte)0u); 
        goto L801A6CF8;
        L801A6CDC: ;
        c.V0 = mem.ReadU8((c.S1 + 0x28u)); 
        { c.A0 = 0u | 0x0001u;  }
        { c.V0 = c.V0 - 0x10u;  }
        mem.WriteU8((c.S1 + 0x28u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x1Cu), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x10u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x4u), (byte)c.V0); 
        L801A6CF8: ;
        c.V0 = mem.ReadU8((c.S1 + 0x6u)); 
        { c.V0 = c.V0 < 0x0000007Fu ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x007Fu;  }
            goto L801A6D18;
        }
        { c.V0 = 0u | 0x007Fu;  }
        c.V0 = mem.ReadU8((c.S1 + 0x12u)); 
        { c.A0 = 0u | 0x0001u;  }
        { c.V0 = c.V0 + 0x8u;  }
        L801A6D18: ;
        mem.WriteU8((c.S1 + 0x12u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x6u), (byte)c.V0); 
        c.S1 = mem.ReadU32(c.S1); 
        c.V0 = mem.ReadU8((c.S1 + 0x4u)); 
        { c.V0 = c.V0 < 0x00000010u ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0010u;  }
            goto L801A6D48;
        }
        { c.V0 = 0u | 0x0010u;  }
        c.V0 = mem.ReadU8((c.S1 + 0x6u)); 
        { c.A0 = 0u | 0x0001u;  }
        { c.V0 = c.V0 - 0xFu;  }
        L801A6D48: ;
        mem.WriteU8((c.S1 + 0x6u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x5u), (byte)c.V0); 
        mem.WriteU8((c.S1 + 0x4u), (byte)c.V0); 
        { c.V0 = c.A0 << 16;  }
        L801A6D58: ;
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A6ED4;
        }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801A6ED4;
        L801A6D68: ;
        c.S1 = 0x801B0000u; 
        c.S1 = mem.ReadU32((c.S1 + 0x686Cu)); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x6850u)); 
        c.S1 = mem.ReadU32(c.S1); 
        { c.V0 = c.V0 - 0x1u;  }
        c.At = 0x801B0000u; 
        mem.WriteU16((c.At + 0x6850u), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        { c.V1 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V1 < 12 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801A6DD4;
        }
        c.S1 = mem.ReadU32(c.S1); 
        c.V1 = mem.ReadU8((c.S1 + 0xCu)); 
        { c.V0 = c.V1 + 0xECu;  }
        mem.WriteU8((c.S1 + 0xCu), (byte)c.V0); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x6850u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 - 0x15u;  }
            goto L801A6ED4;
        }
        { c.V0 = c.V1 - 0x15u;  }
        mem.WriteU8((c.S1 + 0xCu), (byte)c.V0); 
        goto L801A6ED4;
        L801A6DD4: ;
        if (c.V1 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A6DEC;
        }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0); 
        goto L801A6E00;
        L801A6DEC: ;
        c.V0 = mem.ReadU16((c.S1 + 0x2Eu)); 
        { c.V0 = c.V0 - 0x6u;  }
        mem.WriteU16((c.S1 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0x22u), (ushort)c.V0); 
        L801A6E00: ;
        c.S1 = mem.ReadU32(c.S1); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.S1 + 0x32u), (ushort)c.V0); 
        goto L801A6ED4;
        L801A6E10: ;
        c.V0 = mem.ReadU16((c.S2 + 0x30u)); 
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A6E90;
        }
        { c.V0 = 0u | 0x0001u;  }
        c.A0 = 0x80040000u; 
        { c.A0 = c.A0 - 0x34FCu;  }
        c.V1 = mem.ReadU32(c.A0); 
        c.At = 0x80040000u; 
        mem.WriteU8((c.At - 0x4160u), (byte)c.V0); 
        c.At = 0x80040000u; 
        mem.WriteU8((c.At - 0x418Fu), (byte)c.V0); 
        c.At = 0x80040000u; 
        mem.WriteU8((c.At - 0x41B2u), (byte)c.V0); 
        { c.V0 = c.V1 | 0x4000u;  }
        mem.WriteU32(c.A0, c.V0); 
        { c.V0 = c.V1 | 0x4100u;  }
        { c.V1 = c.V1 | 0x4300u;  }
        mem.WriteU32(c.A0, c.V0); 
        mem.WriteU32(c.A0, c.V1); 
        goto L801A6EC4;
        L801A6E60: ;
        c.V0 = mem.ReadU8(c.S4); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU8(c.S4, (byte)c.V0); 
        goto L801A6ED4;
        L801A6E74: ;
        c.V1 = 0x801B0000u; 
        { c.V1 = c.V1 + 0x6840u;  }
        c.V0 = mem.ReadU32(c.V1); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU32(c.V1, c.V0); 
        goto L801A6ED4;
        L801A6E90: ;
        { c.V0 = 0u | 0x0001u;  }
        c.A0 = 0x80040000u; 
        { c.A0 = c.A0 - 0x34FCu;  }
        c.At = 0x80180000u; 
        mem.WriteU32((c.At + 0xB78u), c.V0); 
        c.V0 = mem.ReadU32(c.A0); 
        { c.V1 = 0u | 0x031Du;  }
        c.At = 0x80090000u; 
        mem.WriteU32((c.At + 0x7928u), 0u); 
        c.At = 0x80090000u; 
        mem.WriteU32((c.At + 0x7910u), c.V1); 
        { c.V0 = c.V0 | 0x0010u;  }
        mem.WriteU32(c.A0, c.V0); 
        L801A6EC4: ;
        c.A0 = c.S2; 
        c.RA = 0x801A6ECCu; 
        Dispatcher.Call(c, m, 0x801AAA88u);
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x38FCu), 0u); 
        L801A6ED4: ;
        c.RA = mem.ReadU32((c.SP + 0x44u)); 
        c.S4 = mem.ReadU32((c.SP + 0x40u)); 
        c.S3 = mem.ReadU32((c.SP + 0x3Cu)); 
        c.S2 = mem.ReadU32((c.SP + 0x38u)); 
        c.S1 = mem.ReadU32((c.SP + 0x34u)); 
        c.S0 = mem.ReadU32((c.SP + 0x30u)); 
        { c.SP = c.SP + 0x48u;  }
        return;
        { c.SP = c.SP - 0x20u;  }
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.S0 = c.A0; 
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x33DAu)); 
        c.V1 = 0x80070000u; 
        c.V1 = mem.ReadU16((c.V1 + 0x308Eu)); 
        c.A1 = 0x80070000u; 
        { c.A1 = c.A1 + 0x3084u;  }
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        c.S1 = 0x80070000u; 
        { c.S1 = c.S1 + 0x33D8u;  }
        mem.WriteU32((c.SP + 0x18u), c.RA); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        c.V1 = mem.ReadU16((c.S0 + 0x2Cu)); 
        c.A0 = c.V0; 
        { c.V0 = c.V1 < 0x00000008u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801A7328;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x2018u)); 
        switch (c.V0)
        {
            case 0x801A6F5Cu: goto L801A6F5C;
            case 0x801A6F74u: goto L801A6F74;
            case 0x801A7078u: goto L801A7078;
            case 0x801A70E8u: goto L801A70E8;
            case 0x801A7164u: goto L801A7164;
            case 0x801A71CCu: goto L801A71CC;
            case 0x801A7230u: goto L801A7230;
            case 0x801A72A4u: goto L801A72A4;
            case 0x801A73A8u: goto L801A73A8;
            case 0x801A73E8u: goto L801A73E8;
            case 0x801A7410u: goto L801A7410;
            case 0x801A7470u: goto L801A7470;
            case 0x801A7498u: goto L801A7498;
            case 0x801A74D0u: goto L801A74D0;
            case 0x801A750Cu: goto L801A750C;
            case 0x801A7564u: goto L801A7564;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801A6F5C: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x4DCu;  }
        c.RA = 0x801A6F6Cu; 
        SoTN_bo2.BO2_InitializeEntity(c, m);
        mem.WriteU16((c.S0 + 0x54u), (ushort)0u); 
        goto L801A7328;
        L801A6F74: ;
        { c.V0 = c.A0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 97 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A6F98;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x30u)); 
        if (c.V0 == 0u) {
            goto L801A7328;
        }
        L801A6F98: ;
        c.V0 = mem.ReadU16((c.S0 + 0x30u)); 
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A6FB8;
        }
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x80180000u; 
        mem.WriteU32((c.At + 0xB74u), c.V0); 
        { c.V0 = 0u | 0x0060u;  }
        mem.WriteU16((c.S1 + 0x2u), (ushort)c.V0); 
        L801A6FB8: ;
        c.V1 = 0x80070000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x2F2Cu)); 
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x3748u), 0u); 
        c.At = 0x80090000u; 
        mem.WriteU32((c.At + 0x7400u), c.V0); 
        { c.V0 = c.V1 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0008u;  }
            goto L801A6FF0;
        }
        { c.V0 = 0u | 0x0008u;  }
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EF4u), c.V0); 
        { c.V0 = 0u | 0x0001u;  }
        goto L801A7034;
        L801A6FF0: ;
        { c.V0 = c.V1 & 0x0002u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0004u;  }
            goto L801A700C;
        }
        { c.V0 = 0u | 0x0004u;  }
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EF4u), c.V0); 
        { c.V0 = 0u | 0x0001u;  }
        goto L801A7034;
        L801A700C: ;
        { c.V0 = c.V1 & 0x0004u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0002u;  }
            goto L801A7028;
        }
        { c.V0 = 0u | 0x0002u;  }
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EF4u), c.V0); 
        { c.V0 = 0u | 0x0001u;  }
        goto L801A7034;
        L801A7028: ;
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EF4u), 0u); 
        { c.V0 = 0u | 0x0001u;  }
        L801A7034: ;
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EFCu), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x30u)); 
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0006u;  }
            goto L801A7054;
        }
        { c.V0 = 0u | 0x0006u;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        goto L801A7328;
        L801A7054: ;
        c.V1 = mem.ReadU16((c.S0 + 0x2Cu)); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x68ECu)); 
        { c.V1 = c.V1 + 0x1u;  }
        { c.V0 = c.V0 | 0x0001u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x68ECu), c.V0); 
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V1); 
        goto L801A7328;
        L801A7078: ;
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x2F2Cu)); 
        c.V1 = 0x80070000u; 
        { c.V1 = c.V1 + 0x2EF4u;  }
        { c.V0 = c.V0 & 0x0004u;  }
        if (c.V0 == 0u) {
            mem.WriteU32(c.V1, 0u); 
            goto L801A70B4;
        }
        mem.WriteU32(c.V1, 0u); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3668u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0002u;  }
            goto L801A70E0;
        }
        { c.V0 = 0u | 0x0002u;  }
        mem.WriteU32(c.V1, c.V0); 
        goto L801A70E0;
        L801A70B4: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x68ECu)); 
        { c.V0 = c.V0 & 0x0080u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x2000u;  }
            goto L801A70E0;
        }
        { c.V0 = 0u | 0x2000u;  }
        mem.WriteU32(c.V1, c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        L801A70E0: ;
        { c.V0 = 0u | 0x0001u;  }
        goto L801A7320;
        L801A70E8: ;
        { c.V0 = c.A0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 232 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x2000u;  }
            goto L801A710C;
        }
        { c.V0 = 0u | 0x2000u;  }
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EF4u), c.V0); 
        { c.V0 = 0u | 0x0001u;  }
        goto L801A7320;
        L801A710C: ;
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EF4u), 0u); 
        c.V1 = mem.ReadU16((c.A1 + 0xAu)); 
        { c.V0 = 0u | 0x00E8u;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s - _t;  }
        mem.WriteU16((c.S1 + 0x2u), (ushort)c.V0); 
        c.V1 = 0x80090000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x7C0Cu)); 
        { c.V0 = 0u | 0x0019u;  }
        if (c.V1 == c.V0) {
            goto L801A7154;
        }
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xC8Cu;  }
        c.A1 = c.S0; 
        c.RA = 0x801A7148u; 
        Dispatcher.Call(c, m, 0x801AABB8u);
        c.V0 = mem.ReadU16((c.S0 + 0x56u)); 
        mem.WriteU16((c.S1 + 0x56u), (ushort)c.V0); 
        L801A7154: ;
        { c.V0 = 0u | 0x0004u;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0001u;  }
        goto L801A7320;
        L801A7164: ;
        c.V1 = 0x80090000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x7C0Cu)); 
        { c.V0 = 0u | 0x0019u;  }
        if (c.V1 == c.V0) {
            goto L801A7194;
        }
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xC8Cu;  }
        c.A1 = c.S0; 
        c.RA = 0x801A7188u; 
        Dispatcher.Call(c, m, 0x801AABB8u);
        c.V0 = mem.ReadU16((c.S0 + 0x56u)); 
        mem.WriteU16((c.S1 + 0x56u), (ushort)c.V0); 
        L801A7194: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x68ECu)); 
        { c.V1 = 0u | 0x0001u;  }
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EF4u), 0u); 
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EFCu), c.V1); 
        { c.V0 = c.V0 & 0x0040u;  }
        if (c.V0 == 0u) {
            goto L801A7328;
        }
        { c.A0 = 0u | 0x0005u;  }
        c.RA = 0x801A71C4u; 
        SoTN_bo2.func_801AB6C8(c, m);
        goto L801A7328;
        L801A71CC: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xC98u;  }
        c.A1 = c.S0; 
        c.RA = 0x801A71DCu; 
        Dispatcher.Call(c, m, 0x801AABB8u);
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0019u;  }
            goto L801A7210;
        }
        { c.V0 = 0u | 0x0019u;  }
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x3748u), c.V0); 
        c.At = 0x80090000u; 
        mem.WriteU32((c.At + 0x7400u), 0u); 
        c.At = 0x80180000u; 
        mem.WriteU32((c.At + 0xAACu), c.V0); 
        c.A0 = c.S0; 
        c.RA = 0x801A7208u; 
        Dispatcher.Call(c, m, 0x801AAA88u);
        goto L801A7328;
        L801A7210: ;
        c.V1 = 0x80090000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x7C0Cu)); 
        if (c.V1 == c.V0) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A7318;
        }
        { c.V0 = 0u | 0x0001u;  }
        c.V0 = mem.ReadU16((c.S0 + 0x56u)); 
        mem.WriteU16((c.S1 + 0x56u), (ushort)c.V0); 
        goto L801A7314;
        L801A7230: ;
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x2F2Cu)); 
        { c.V0 = c.V0 & 0x0004u;  }
        if (c.V0 == 0u) {
            goto L801A7268;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3668u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0002u;  }
            goto L801A7268;
        }
        { c.V0 = 0u | 0x0002u;  }
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EF4u), c.V0); 
        L801A7268: ;
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x2F20u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A7318;
        }
        { c.V0 = 0u | 0x0001u;  }
        c.V1 = mem.ReadU16((c.S0 + 0x2Cu)); 
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x68ECu)); 
        { c.V1 = c.V1 + 0x1u;  }
        { c.V0 = c.V0 | 0x0001u;  }
        c.At = 0x801B0000u; 
        mem.WriteU32((c.At + 0x68ECu), c.V0); 
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V1); 
        goto L801A7314;
        L801A72A4: ;
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x2F2Cu)); 
        { c.V0 = c.V0 & 0x0004u;  }
        if (c.V0 == 0u) {
            goto L801A72DC;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3668u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0002u;  }
            goto L801A72DC;
        }
        { c.V0 = 0u | 0x0002u;  }
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EF4u), c.V0); 
        L801A72DC: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x68ECu)); 
        { c.V0 = c.V0 & 0x1000u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A7314;
        }
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x3748u), c.V0); 
        c.At = 0x80090000u; 
        mem.WriteU32((c.At + 0x7400u), 0u); 
        c.A0 = c.S0; 
        c.RA = 0x801A730Cu; 
        Dispatcher.Call(c, m, 0x801AAA88u);
        goto L801A7328;
        L801A7314: ;
        { c.V0 = 0u | 0x0001u;  }
        L801A7318: ;
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EF4u), 0u); 
        L801A7320: ;
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x2EFCu), c.V0); 
        L801A7328: ;
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x683Cu)); 
        { c.SP = c.SP - 0x18u;  }
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.S0 = c.A0; 
        if (c.V0 == 0u) {
            mem.WriteU32((c.SP + 0x14u), c.RA); 
            goto L801A737C;
        }
        mem.WriteU32((c.SP + 0x14u), c.RA); 
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 - 0x3u;  }
        { c.V0 = c.V0 < 0x00000003u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A737C;
        }
        { c.A0 = 0u | 0x0006u;  }
        c.RA = 0x801A737Cu; 
        SoTN_bo2.func_801AB6C8(c, m);
        L801A737C: ;
        c.V1 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V1 < 0x00000008u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801A75A0;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x2038u)); 
        switch (c.V0)
        {
            case 0x801A73A8u: goto L801A73A8;
            case 0x801A73E8u: goto L801A73E8;
            case 0x801A7410u: goto L801A7410;
            case 0x801A7470u: goto L801A7470;
            case 0x801A7498u: goto L801A7498;
            case 0x801A74D0u: goto L801A74D0;
            case 0x801A750Cu: goto L801A750C;
            case 0x801A7564u: goto L801A7564;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801A73A8: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x50Cu;  }
        c.RA = 0x801A73B8u; 
        SoTN_bo2.BO2_InitializeEntity(c, m);
        c.V0 = 0xFFFF8005u; 
        mem.WriteU16((c.S0 + 0x54u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S0 + 0x56u), (ushort)c.V0); 
        { c.V0 = 0u | 0x005Au;  }
        mem.WriteU16((c.S0 + 0x5Au), (ushort)c.V0); 
        { c.V0 = 0u | 0x022Au;  }
        mem.WriteU16((c.S0 + 0x16u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU8((c.S0 + 0x19u), (byte)c.V0); 
        mem.WriteU8((c.S0 + 0x6Cu), (byte)0u); 
        goto L801A75A0;
        L801A73E8: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x68ECu)); 
        { c.V0 = c.V0 & 0x0080u;  }
        if (c.V0 == 0u) {
            goto L801A75A0;
        }
        { c.A0 = 0u | 0x0002u;  }
        c.RA = 0x801A7408u; 
        SoTN_bo2.func_801AB6C8(c, m);
        goto L801A75A0;
        L801A7410: ;
        c.V0 = mem.ReadU8((c.S0 + 0x6Cu)); 
        { c.V0 = c.V0 < 0x00000080u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801A7434;
        }
        c.V0 = mem.ReadU8((c.S0 + 0x6Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU8((c.S0 + 0x6Cu), (byte)c.V0); 
        L801A7434: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xC28u;  }
        c.A1 = c.S0; 
        c.RA = 0x801A7444u; 
        Dispatcher.Call(c, m, 0x801AABB8u);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x68ECu)); 
        { c.V0 = c.V0 & 0x0004u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A75A0;
        }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S0 + 0x56u), (ushort)c.V0); 
        { c.A0 = 0u | 0x0003u;  }
        c.RA = 0x801A7468u; 
        SoTN_bo2.func_801AB6C8(c, m);
        goto L801A75A0;
        L801A7470: ;
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x68ECu)); 
        { c.V0 = c.V0 & 0x0008u;  }
        if (c.V0 == 0u) {
            goto L801A75A0;
        }
        { c.A0 = 0u | 0x0004u;  }
        c.RA = 0x801A7490u; 
        SoTN_bo2.func_801AB6C8(c, m);
        goto L801A75A0;
        L801A7498: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xC30u;  }
        c.A1 = c.S0; 
        c.RA = 0x801A74A8u; 
        Dispatcher.Call(c, m, 0x801AABB8u);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x68ECu)); 
        { c.V0 = c.V0 & 0x0020u;  }
        if (c.V0 == 0u) {
            goto L801A75A0;
        }
        { c.A0 = 0u | 0x0005u;  }
        c.RA = 0x801A74C8u; 
        SoTN_bo2.func_801AB6C8(c, m);
        goto L801A75A0;
        L801A74D0: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xC50u;  }
        c.A1 = c.S0; 
        c.RA = 0x801A74E0u; 
        Dispatcher.Call(c, m, 0x801AABB8u);
        c.V0 = 0x801B0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x68ECu)); 
        { c.V0 = c.V0 & 0x0100u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801A75A0;
        }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S0 + 0x56u), (ushort)c.V0); 
        { c.A0 = 0u | 0x0006u;  }
        c.RA = 0x801A7504u; 
        SoTN_bo2.func_801AB6C8(c, m);
        goto L801A75A0;
        L801A750C: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xC28u;  }
        c.A1 = c.S0; 
        c.RA = 0x801A751Cu; 
        Dispatcher.Call(c, m, 0x801AABB8u);
        c.V0 = mem.ReadU8((c.S0 + 0x6Cu)); 
        { c.V0 = c.V0 < 0x00000009u ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0007u;  }
            goto L801A7544;
        }
        { c.V0 = 0u | 0x0007u;  }
        c.V0 = mem.ReadU8((c.S0 + 0x6Cu)); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU8((c.S0 + 0x6Cu), (byte)c.V0); 
        goto L801A75A0;
        L801A7544: ;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        { c.V0 = 0u | 0x015Fu;  }
        mem.WriteU16((c.S0 + 0x16u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0050u;  }
        mem.WriteU8((c.S0 + 0x18u), (byte)c.V0); 
        { c.V0 = 0u | 0x0060u;  }
        mem.WriteU8((c.S0 + 0x6Cu), (byte)c.V0); 
        goto L801A75A0;
        L801A7564: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0xC28u;  }
        c.A1 = c.S0; 
        c.RA = 0x801A7574u; 
        Dispatcher.Call(c, m, 0x801AABB8u);
        c.V0 = mem.ReadU8((c.S0 + 0x6Cu)); 
        if (c.V0 == 0u) {
            goto L801A7598;
        }
        c.V0 = mem.ReadU8((c.S0 + 0x6Cu)); 
        { c.V0 = c.V0 + 0xFEu;  }
        mem.WriteU8((c.S0 + 0x6Cu), (byte)c.V0); 
        goto L801A75A0;
        L801A7598: ;
        c.A0 = c.S0; 
        c.RA = 0x801A75A0u; 
        Dispatcher.Call(c, m, 0x801AAA88u);
        L801A75A0: ;
        c.RA = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x18u;  }
        return;
        }
    public static bool BO2_EntityRelicOrb(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo2", m, false)) return true;
        BO2_EntityRelicOrb_Body(c, m);
        return false;
    }
    private static void BO2_EntityRelicOrb_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo2", 0x801B6840u);
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
            goto L801AD7B0;
        }
        { c.S4 = c.V0 & 0x7FFFu;  }
        { c.V0 = c.V1 < 0x00000005u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 < 0x0000000Au ? 1u : 0u;  }
            goto L801AD7B8;
        }
        { c.V0 = c.V1 < 0x0000000Au ? 1u : 0u;  }
        c.V0 = mem.ReadU8((c.S1 + 0x48u)); 
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0005u;  }
            goto L801AD7B0;
        }
        { c.V0 = 0u | 0x0005u;  }
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        c.V1 = mem.ReadU16((c.S1 + 0x2Cu)); 
        L801AD7B0: ;
        { c.V0 = c.V1 < 0x0000000Au ? 1u : 0u;  }
        L801AD7B8: ;
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801AE018;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x2108u)); 
        switch (c.V0)
        {
            case 0x801AD7D8u: goto L801AD7D8;
            case 0x801AD9A8u: goto L801AD9A8;
            case 0x801AE018u: goto L801AE018;
            case 0x801ADA08u: goto L801ADA08;
            case 0x801ADBC8u: goto L801ADBC8;
            case 0x801ADC90u: goto L801ADC90;
            case 0x801ADD40u: goto L801ADD40;
            case 0x801ADEB8u: goto L801ADEB8;
            case 0x801AE388u: goto L801AE388;
            case 0x801AE46Cu: goto L801AE46C;
            case 0x801AE648u: goto L801AE648;
            case 0x801AE720u: goto L801AE720;
            case 0x801AE824u: goto L801AE824;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801AD7D8: ;
        { c.V0 = c.S4 & 0xFFFFu;  }
        c.At = 0x80090000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU8((c.At + 0x7964u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 != 0u) {
            goto L801ADF18;
        }
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x4C4u;  }
        c.S0 = 0u; 
        c.RA = 0x801AD808u; 
        SoTN_bo2.BO2_InitializeEntity(c, m);
        c.S5 = 0x801B0000u; 
        { c.S5 = c.S5 + 0x68F4u;  }
        { c.V0 = c.S0 << 16;  }
        L801AD814: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (uint)((int)c.V0 >> 15);  }
        { var _s = c.V0; var _t = c.S5; c.V0 = _s + _t;  }
        c.V0 = mem.ReadU16(c.V0); 
        if (c.V0 == 0u) {
            { c.V0 = c.S0 + 0x1u;  }
            goto L801AD844;
        }
        { c.V0 = c.S0 + 0x1u;  }
        c.S0 = c.V0; 
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 31 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = c.S0 << 16;  }
            goto L801AD814;
        }
        { c.V0 = c.S0 << 16;  }
        L801AD844: ;
        { c.V0 = c.S0 << 16;  }
        { c.S3 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.S3 < 31 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.A0 = 0u | 0x0004u;  }
            goto L801AD880;
        }
        { c.A0 = 0u | 0x0004u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0007u;  }
        c.RA = 0x801AD86Cu;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.S2 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.S2 != c.V0) {
            c.V1 = 0x00800000u; 
            goto L801AD888;
        }
        c.V1 = 0x00800000u; 
        L801AD880: ;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)0u); 
        goto L801AE1FC;
        L801AD888: ;
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
        c.RA = 0x801AD8D8u;
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
            goto L801AD984;
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
        L801AD930: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 == 0u) {
            { c.V0 = 0u | 0x0006u;  }
            goto L801AD940;
        }
        { c.V0 = 0u | 0x0006u;  }
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.T3); 
        goto L801AD96C;
        L801AD940: ;
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
        L801AD96C: ;
        { c.V0 = 0u | 0x007Eu;  }
        mem.WriteU16((c.A2 + 0x26u), (ushort)c.V0); 
        c.A2 = mem.ReadU32(c.A2); 
        if (c.A2 != 0u) {
            { c.A3 = c.A3 + 0x1u;  }
            goto L801AD930;
        }
        { c.A3 = c.A3 + 0x1u;  }
        L801AD984: ;
        c.V0 = 0xFFFF8000u; 
        mem.WriteU16((c.S1 + 0x4u), (ushort)c.V0); 
        { c.V0 = 0u | 0x4000u;  }
        mem.WriteU32((c.S1 + 0xCu), c.V0); 
        { c.V0 = 0u | 0x0040u;  }
        mem.WriteU16((c.S1 + 0x90u), (ushort)c.V0); 
        c.V0 = 0xFFFFFE00u; 
        mem.WriteU32((c.S1 + 0x94u), c.V0); 
        goto L801AE018;
        L801AD9A8: ;
        c.V0 = mem.ReadU32((c.S1 + 0xCu)); 
        c.V1 = mem.ReadU16((c.S1 + 0x90u)); 
        c.A0 = mem.ReadU32((c.S1 + 0x94u)); 
        { c.V1 = c.V1 - 0x1u;  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s + _t;  }
        mem.WriteU16((c.S1 + 0x90u), (ushort)c.V1); 
        { c.V1 = c.V1 & 0xFFFFu;  }
        if (c.V1 != 0u) {
            mem.WriteU32((c.S1 + 0xCu), c.V0); 
            goto L801AD9E0;
        }
        mem.WriteU32((c.S1 + 0xCu), c.V0); 
        c.V0 = mem.ReadU32((c.S1 + 0x94u)); 
        { c.V1 = 0u | 0x0040u;  }
        mem.WriteU16((c.S1 + 0x90u), (ushort)c.V1); 
        { var _s = 0u; var _t = c.V0; c.V0 = _s - _t;  }
        mem.WriteU32((c.S1 + 0x94u), c.V0); 
        L801AD9E0: ;
        c.RA = 0x801AD9E8u; 
        SoTN_bo2.func_801AAE3C(c, m);
        c.V0 = (uint)(short)mem.ReadU16((c.S1 + 0x8Cu)); 
        { c.V1 = 0u | 0x0010u;  }
        { c.V0 = c.V0 << 1;  }
        c.At = 0x801B0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        mem.WriteU16((c.At + 0x68F4u), (ushort)c.V1); 
        goto L801AE018;
        L801ADA08: ;
        { c.S0 = c.S4 & 0xFFFFu;  }
        c.A0 = c.S0; 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x37B8u)); 
        { c.A1 = 0u | 0x2000u;  }
        c.RA = 0x801ADA24u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.S0 < 0x00000016u ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.A0 = c.SP + 0x20u;  }
            goto L801ADA5C;
        }
        { c.A0 = c.SP + 0x20u;  }
        { c.V0 = c.S0 < 0x00000014u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            c.A1 = 0u; 
            goto L801ADA60;
        }
        c.A1 = 0u; 
        c.At = 0x80090000u; 
        { var _s = c.At; var _t = c.S0; c.At = _s + _t;  }
        c.V0 = mem.ReadU8((c.At + 0x7964u)); 
        { c.V0 = c.V0 ^ 0x0002u;  }
        c.At = 0x80090000u; 
        { var _s = c.At; var _t = c.S0; c.At = _s + _t;  }
        mem.WriteU8((c.At + 0x7964u), (byte)c.V0); 
        L801ADA5C: ;
        c.A1 = 0u; 
        L801ADA60: ;
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
        c.RA = 0x801ADA98u; 
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
        L801ADACC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 != 0u) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801ADB18;
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
        goto L801ADBA4;
        L801ADB18: ;
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
            goto L801ADB88;
        }
        mem.WriteU8((c.A2 + 0x6u), (byte)0u); 
        mem.WriteU8((c.A2 + 0x2Au), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x1Eu), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x12u), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x6u), (byte)c.A0); 
        goto L801ADB98;
        L801ADB88: ;
        mem.WriteU8((c.A2 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x1Du), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x11u), (byte)c.A0); 
        mem.WriteU8((c.A2 + 0x5u), (byte)c.A0); 
        L801ADB98: ;
        { c.V0 = 0u | 0x01FDu;  }
        mem.WriteU16((c.A2 + 0x26u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0011u;  }
        L801ADBA4: ;
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.V0); 
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 + 0x1u;  }
        { c.V0 = (int)c.A3 < 3 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801ADACC;
        }
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801ADEB0;
        L801ADBC8: ;
        c.S0 = 0u; 
        c.S3 = 0x80080000u; 
        { c.S3 = c.S3 - 0x101Cu;  }
        c.V0 = c.S3; 
        c.A0 = 0x80180000u; 
        c.A0 = mem.ReadU32((c.A0 + 0x15B8u)); 
        { c.A3 = 0u | 0x0BFFu;  }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        L801ADBE8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU8(c.V0, (byte)0u); 
        { c.A3 = c.A3 - 0x1u;  }
        if ((int)c.A3 >= 0) {
            { c.V0 = c.V0 + 0x1u;  }
            goto L801ADBE8;
        }
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        { c.S2 = c.S4 << 4;  }
        L801ADC00: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.A0); 
        if (c.V0 != 0u) {
            { c.A1 = c.SP + 0x28u;  }
            goto L801ADC34;
        }
        { c.A1 = c.SP + 0x28u;  }
        if (c.S0 != 0u) {
            { c.S0 = 0u | 0x0001u;  }
            goto L801ADC48;
        }
        { c.S0 = 0u | 0x0001u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x37B0u)); 
        { var _s = c.S2; var _t = c.V0; c.V0 = _s + _t;  }
        c.A0 = mem.ReadU32(c.V0); 
        goto L801ADC00;
        L801ADC34: ;
        c.A2 = c.S3; 
        { c.A3 = 0u | 0x00C0u;  }
        c.RA = 0x801ADC40u; 
        SoTN_bo2.BO2_BlitChar(c, m);
        c.A0 = c.V0; 
        goto L801ADC00;
        L801ADC48: ;
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
        c.RA = 0x801ADC74u; 
        SoTN_main.LoadTPage(c, m);
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)0u); 
        c.V1 = mem.ReadU16((c.SP + 0x28u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x7Eu), (ushort)c.V1); 
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        goto L801AE018;
        L801ADC90: ;
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
        L801ADCB8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 != 0u) {
            goto L801ADCEC;
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
        goto L801ADD14;
        L801ADCEC: ;
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
        L801ADD14: ;
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.A0); 
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.A0); 
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.A1); 
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.A1); 
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 + 0x1u;  }
        { c.V0 = (int)c.A3 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801ADE88;
        }
        goto L801ADCB8;
        L801ADD40: ;
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
        L801ADDB8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A3 != 0u) {
            { c.V0 = c.A0 + 0x68u;  }
            goto L801ADE18;
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
        c.V0 = mem.ReadU16((c.At + 0x15CCu)); 
        { c.V0 = c.V0 + 0xA7u;  }
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V0); 
        c.V0 = mem.ReadU8((c.A2 + 0x2Au)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.A1; c.At = _s + _t;  }
        c.V1 = mem.ReadU16((c.At + 0x15BCu)); 
        { c.V0 = c.V0 - 0x10u;  }
        { c.V1 = c.V1 + 0xA7u;  }
        mem.WriteU8((c.A2 + 0x2Au), (byte)c.V0); 
        mem.WriteU8((c.A2 + 0x1Eu), (byte)c.V0); 
        goto L801ADE6C;
        L801ADE18: ;
        mem.WriteU16((c.A2 + 0x8u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0098u;  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s - _t;  }
        mem.WriteU16((c.A2 + 0x14u), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0x2Cu), (ushort)c.T1); 
        mem.WriteU16((c.A2 + 0x20u), (ushort)c.T0); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.A1; c.At = _s + _t;  }
        c.V0 = mem.ReadU16((c.At + 0x15DCu)); 
        { c.V0 = c.V0 + 0xA7u;  }
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V0); 
        c.V0 = mem.ReadU8((c.A2 + 0x11u)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.A1; c.At = _s + _t;  }
        c.V1 = mem.ReadU16((c.At + 0x15ECu)); 
        { c.V0 = c.V0 - 0x10u;  }
        { c.V1 = c.V1 + 0xA7u;  }
        mem.WriteU8((c.A2 + 0x11u), (byte)c.V0); 
        mem.WriteU8((c.A2 + 0x5u), (byte)c.V0); 
        L801ADE6C: ;
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.V1); 
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.V1); 
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 + 0x1u;  }
        { c.V0 = (int)c.A3 < 3 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801ADDB8;
        }
        L801ADE88: ;
        c.V0 = mem.ReadU16((c.S1 + 0x7Cu)); 
        { c.V1 = 0u | 0x0008u;  }
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)c.V0); 
        { c.V0 = c.V0 & 0xFFFFu;  }
        if (c.V0 != c.V1) {
            goto L801AE018;
        }
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        mem.WriteU16((c.S1 + 0x7Cu), (ushort)0u); 
        { c.V0 = c.V0 + 0x1u;  }
        L801ADEB0: ;
        mem.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0); 
        goto L801AE018;
        L801ADEB8: ;
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
            goto L801AE018;
        }
        L801ADF18: ;
        c.A0 = c.S1; 
        c.RA = 0x801ADF20u; 
        Dispatcher.Call(c, m, 0x801AAA88u);
        goto L801AE1FC;
        L801ADF28: ;
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
        c.V0 = mem.ReadU16((c.At + 0x15FCu)); 
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
        c.V1 = mem.ReadU16((c.At + 0x160Cu)); 
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
        goto L801AE0A8;
        L801AE018: ;
        c.V0 = mem.ReadU16((c.S1 + 0x2Cu)); 
        { c.V0 = c.V0 < 0x00000002u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801AE0D0;
        }
        c.A1 = 0x80040000u; 
        c.A1 = mem.ReadU16((c.A1 - 0x3668u)); 
        c.A0 = c.S1; 
        c.RA = 0x801AE03Cu; 
        SoTN_bo2.BO2_BlinkItem(c, m);
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
        L801AE064: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 - 0x1u;  }
        if ((int)c.A3 >= 0) {
            goto L801AE064;
        }
        c.V0 = mem.ReadU16((c.S1 + 0x80u)); 
        if (c.V0 != 0u) {
            c.A3 = 0u; 
            goto L801AE0C0;
        }
        c.A3 = 0u; 
        { c.V1 = 0u | 0x0008u;  }
        L801AE088: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU16((c.A2 + 0x32u)); 
        if (c.V0 == c.V1) {
            { c.A3 = c.A3 + 0x1u;  }
            goto L801ADF28;
        }
        { c.A3 = c.A3 + 0x1u;  }
        c.A2 = mem.ReadU32(c.A2); 
        { c.V0 = (int)c.A3 < 4 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801AE088;
        }
        L801AE0A8: ;
        c.V0 = mem.ReadU16((c.S1 + 0x82u)); 
        { c.V1 = 0u | 0x0004u;  }
        mem.WriteU16((c.S1 + 0x80u), (ushort)c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x82u), (ushort)c.V0); 
        goto L801AE0D0;
        L801AE0C0: ;
        c.V0 = mem.ReadU16((c.S1 + 0x80u)); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU16((c.S1 + 0x80u), (ushort)c.V0); 
        L801AE0D0: ;
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
        L801AE0F8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A2 = mem.ReadU32(c.A2); 
        { c.A3 = c.A3 - 0x1u;  }
        if ((int)c.A3 >= 0) {
            goto L801AE0F8;
        }
        if (c.A2 == 0u) {
            { c.A3 = 0u | 0x0008u;  }
            goto L801AE1FC;
        }
        { c.A3 = 0u | 0x0008u;  }
        L801AE110: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU16((c.A2 + 0x32u)); 
        if (c.V0 == c.A3) {
            goto L801AE1EC;
        }
        c.V0 = mem.ReadU8((c.A2 + 0x13u)); 
        { c.V0 = c.V0 & 0x0003u;  }
        if (c.V0 == 0u) {
            goto L801AE158;
        }
        c.V1 = mem.ReadU16((c.A2 + 0x16u)); 
        c.A0 = mem.ReadU16((c.A2 + 0x2Eu)); 
        { c.V0 = c.V1 - 0x1u;  }
        mem.WriteU16((c.A2 + 0x16u), (ushort)c.V0); 
        { c.V0 = c.A0 - 0x1u;  }
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V1); 
        mem.WriteU16((c.A2 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.A2 + 0x22u), (ushort)c.A0); 
        goto L801AE188;
        L801AE158: ;
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
        L801AE188: ;
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
            goto L801AE1EC;
        }
        mem.WriteU8((c.A2 + 0x6u), (byte)c.V0); 
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.A3); 
        L801AE1EC: ;
        c.A2 = mem.ReadU32(c.A2); 
        if (c.A2 != 0u) {
            goto L801AE110;
        }
        L801AE1FC: ;
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
            goto L801AE2D4;
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
            goto L801AE288;
        }
        { c.V0 = c.A0 << 1;  }
        c.A0 = c.A2; 
        c.RA = 0x801AE280u; 
        Dispatcher.Call(c, m, 0x801AAA88u);
        goto L801AE330;
        L801AE288: ;
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V1 = mem.ReadU16((c.At + 0xCA4u)); 
        { c.V0 = c.V1 < 0x00000080u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801AE2B4;
        }
        c.V0 = 0x801B0000u; 
        { c.V0 = c.V0 - 0x3B58u;  }
        mem.WriteU32((c.A2 + 0xB8u), c.V0); 
        goto L801AE2C4;
        L801AE2B4: ;
        c.V0 = 0x801B0000u; 
        { c.V0 = c.V0 - 0x3144u;  }
        mem.WriteU32((c.A2 + 0xB8u), c.V0); 
        { c.V1 = c.V1 - 0x80u;  }
        L801AE2C4: ;
        { c.V0 = 0u | 0x8000u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _s + _t;  }
        mem.WriteU16((c.A2 + 0x30u), (ushort)c.V0); 
        goto L801AE320;
        L801AE2D4: ;
        c.V1 = mem.ReadU16((c.A2 + 0xB4u)); 
        if (c.V0 == 0u) {
            goto L801AE320;
        }
        c.V0 = mem.ReadU8((c.A2 + 0x48u)); 
        if (c.V0 == 0u) {
            { c.A1 = c.V1 >> 3;  }
            goto L801AE320;
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
        L801AE320: ;
        c.V0 = mem.ReadU32((c.A2 + 0xB8u)); 
        c.A0 = c.A2; 
        c.RA = 0x801AE330u;
        Dispatcher.Call(c, m, c.V0);
        L801AE330: ;
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
            goto L801AE87C;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x2130u)); 
        switch (c.V0)
        {
            case 0x801AE388u: goto L801AE388;
            case 0x801AE46Cu: goto L801AE46C;
            case 0x801AE648u: goto L801AE648;
            case 0x801AE720u: goto L801AE720;
            case 0x801AE824u: goto L801AE824;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801AE388: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x4C4u;  }
        c.RA = 0x801AE398u; 
        SoTN_bo2.BO2_InitializeEntity(c, m);
        c.V1 = 0x00010000u; 
        c.V0 = mem.ReadU32((c.S0 + 0x34u)); 
        c.A0 = mem.ReadU16((c.S0 + 0x30u)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        c.V1 = 0x08000000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s ^ _t;  }
        if (c.A0 != 0u) {
            mem.WriteU32((c.S0 + 0x34u), c.V0); 
            goto L801AE3C0;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0060u;  }
        mem.WriteU16((c.S0 + 0x30u), (ushort)c.V0); 
        L801AE3C0: ;
        { c.A0 = 0u | 0x0004u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0003u;  }
        c.RA = 0x801AE3D8u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.A0 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.A0 != c.V0) {
            { c.V0 = c.A0 << 1;  }
            goto L801AE3F4;
        }
        { c.V0 = c.A0 << 1;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)0u); 
        goto L801AE87C;
        L801AE3F4: ;
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
            goto L801AE440;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        L801AE42C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            goto L801AE42C;
        }
        L801AE440: ;
        c.A0 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.V0 = mem.ReadU8(c.A0); 
        mem.WriteU16((c.S0 + 0x80u), (ushort)c.V0); 
        c.V0 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.V1 = mem.ReadU8((c.A0 + 0x1u)); 
        { c.V0 = c.V0 + 0x2u;  }
        mem.WriteU32((c.S0 + 0x7Cu), c.V0); 
        mem.WriteU16((c.S0 + 0x82u), (ushort)c.V1); 
        goto L801AE87C;
        L801AE46C: ;
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
        c.RA = 0x801AE49Cu; 
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
            goto L801AE63C;
        }
        c.A2 = 0u; 
        { c.A0 = 0u | 0x0080u;  }
        L801AE4D0: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801AE548;
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
        goto L801AE62C;
        L801AE548: ;
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
            goto L801AE5E8;
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
        goto L801AE60C;
        L801AE5E8: ;
        c.V1 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V0 = mem.ReadU16((c.S0 + 0x6u)); 
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x11u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x5u), (byte)c.A0); 
        { c.V1 = c.V1 >> 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 + 0x4u;  }
        L801AE60C: ;
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0); 
        { c.V0 = 0u | 0x01FCu;  }
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0011u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        L801AE62C: ;
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801AE4D0;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801AE63C: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801AE81C;
        L801AE648: ;
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
            goto L801AE6A4;
        }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        L801AE678: ;
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
            goto L801AE678;
        }
        { c.S1 = c.S1 + 0x1u;  }
        c.S1 = c.S2; 
        L801AE6A4: ;
        { c.S3 = 0u | 0x0001u;  }
        L801AE6A8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.A0); 
        if (c.V0 == 0u) {
            c.A1 = 0u; 
            goto L801AE6F0;
        }
        c.A1 = 0u; 
        if (c.V0 != c.S3) {
            { c.A1 = c.SP + 0x28u;  }
            goto L801AE6D8;
        }
        { c.A1 = c.SP + 0x28u;  }
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        { c.A0 = c.A0 + 0x1u;  }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        { c.V0 = c.V0 << 3;  }
        { var _s = c.S2; var _t = c.V0; c.S1 = _s + _t;  }
        goto L801AE6A8;
        L801AE6D8: ;
        c.A3 = mem.ReadU16((c.S0 + 0x80u)); 
        c.A2 = c.S1; 
        { c.A3 = c.A3 >> 1;  }
        c.RA = 0x801AE6E8u; 
        SoTN_bo2.BO2_BlitChar(c, m);
        c.A0 = c.V0; 
        goto L801AE6A8;
        L801AE6F0: ;
        c.A0 = c.S2; 
        { c.V0 = 0u | 0x0180u;  }
        mem.WriteU32((c.SP + 0x10u), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        c.A2 = 0u; 
        mem.WriteU32((c.SP + 0x14u), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.A3 = 0u; 
        mem.WriteU32((c.SP + 0x18u), c.V0); 
        c.RA = 0x801AE718u; 
        SoTN_main.LoadTPage(c, m);
        goto L801AE810;
        L801AE720: ;
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
            goto L801AE800;
        }
        c.A2 = 0u; 
        L801AE75C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            goto L801AE7AC;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = c.V0 + 0x8u;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        c.V0 = c.LO; 
        if ((int)c.V0 >= 0) {
            goto L801AE784;
        }
        { c.V0 = c.V0 + 0x7u;  }
        L801AE784: ;
        { c.V0 = (uint)((int)c.V0 >> 3);  }
        c.V1 = mem.ReadU16((c.A1 + 0xAu)); 
        c.A0 = mem.ReadU8((c.A1 + 0x12u)); 
        { var _s = c.V1; var _t = c.V0; c.V1 = _s + _t;  }
        { c.A0 = c.A0 - 0x10u;  }
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V1); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V1); 
        mem.WriteU8((c.A1 + 0x12u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x6u), (byte)c.A0); 
        goto L801AE7F0;
        L801AE7AC: ;
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = c.V0 + 0x8u;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        c.V0 = c.LO; 
        if ((int)c.V0 >= 0) {
            goto L801AE7CC;
        }
        { c.V0 = c.V0 + 0x7u;  }
        L801AE7CC: ;
        { c.V0 = (uint)((int)c.V0 >> 3);  }
        c.V1 = mem.ReadU16((c.A1 + 0x22u)); 
        c.A0 = mem.ReadU8((c.A1 + 0x29u)); 
        { var _s = c.V1; var _t = c.V0; c.V1 = _s - _t;  }
        { c.A0 = c.A0 - 0x10u;  }
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V1); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V1); 
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0); 
        L801AE7F0: ;
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801AE75C;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801AE800: ;
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = 0u | 0x0008u;  }
        if (c.V1 != c.V0) {
            goto L801AE87C;
        }
        L801AE810: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        mem.WriteU16((c.S0 + 0x86u), (ushort)0u); 
        { c.V0 = c.V0 + 0x1u;  }
        L801AE81C: ;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        goto L801AE87C;
        L801AE824: ;
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
            goto L801AE87C;
        }
        c.A0 = c.S0; 
        c.RA = 0x801AE87Cu; 
        Dispatcher.Call(c, m, 0x801AAA88u);
        L801AE87C: ;
        c.RA = mem.ReadU32((c.SP + 0x48u)); 
        c.S3 = mem.ReadU32((c.SP + 0x44u)); 
        c.S2 = mem.ReadU32((c.SP + 0x40u)); 
        c.S1 = mem.ReadU32((c.SP + 0x3Cu)); 
        c.S0 = mem.ReadU32((c.SP + 0x38u)); 
        { c.SP = c.SP + 0x50u;  }
        return;
        }
    public static bool BO2_EntityMessageBox(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("bo2", m, false)) return true;
        BO2_EntityMessageBox_Body(c, m);
        return false;
    }
    private static void BO2_EntityMessageBox_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "bo2", 0x801B6840u);
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
            goto L801AE87C;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x2130u)); 
        switch (c.V0)
        {
            case 0x801AE388u: goto L801AE388;
            case 0x801AE46Cu: goto L801AE46C;
            case 0x801AE648u: goto L801AE648;
            case 0x801AE720u: goto L801AE720;
            case 0x801AE824u: goto L801AE824;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801AE388: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x4C4u;  }
        c.RA = 0x801AE398u; 
        SoTN_bo2.BO2_InitializeEntity(c, m);
        c.V1 = 0x00010000u; 
        c.V0 = mem.ReadU32((c.S0 + 0x34u)); 
        c.A0 = mem.ReadU16((c.S0 + 0x30u)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        c.V1 = 0x08000000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s ^ _t;  }
        if (c.A0 != 0u) {
            mem.WriteU32((c.S0 + 0x34u), c.V0); 
            goto L801AE3C0;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0060u;  }
        mem.WriteU16((c.S0 + 0x30u), (ushort)c.V0); 
        L801AE3C0: ;
        { c.A0 = 0u | 0x0004u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0003u;  }
        c.RA = 0x801AE3D8u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.A0 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.A0 != c.V0) {
            { c.V0 = c.A0 << 1;  }
            goto L801AE3F4;
        }
        { c.V0 = c.A0 << 1;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)0u); 
        goto L801AE87C;
        L801AE3F4: ;
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
            goto L801AE440;
        }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        L801AE42C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            goto L801AE42C;
        }
        L801AE440: ;
        c.A0 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.V0 = mem.ReadU8(c.A0); 
        mem.WriteU16((c.S0 + 0x80u), (ushort)c.V0); 
        c.V0 = mem.ReadU32((c.S0 + 0x7Cu)); 
        c.V1 = mem.ReadU8((c.A0 + 0x1u)); 
        { c.V0 = c.V0 + 0x2u;  }
        mem.WriteU32((c.S0 + 0x7Cu), c.V0); 
        mem.WriteU16((c.S0 + 0x82u), (ushort)c.V1); 
        goto L801AE87C;
        L801AE46C: ;
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
        c.RA = 0x801AE49Cu; 
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
            goto L801AE63C;
        }
        c.A2 = 0u; 
        { c.A0 = 0u | 0x0080u;  }
        L801AE4D0: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801AE548;
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
        goto L801AE62C;
        L801AE548: ;
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
            goto L801AE5E8;
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
        goto L801AE60C;
        L801AE5E8: ;
        c.V1 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V0 = mem.ReadU16((c.S0 + 0x6u)); 
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x11u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x5u), (byte)c.A0); 
        { c.V1 = c.V1 >> 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 + 0x4u;  }
        L801AE60C: ;
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0); 
        { c.V0 = 0u | 0x01FCu;  }
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0011u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.V0); 
        L801AE62C: ;
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801AE4D0;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801AE63C: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801AE81C;
        L801AE648: ;
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
            goto L801AE6A4;
        }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        L801AE678: ;
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
            goto L801AE678;
        }
        { c.S1 = c.S1 + 0x1u;  }
        c.S1 = c.S2; 
        L801AE6A4: ;
        { c.S3 = 0u | 0x0001u;  }
        L801AE6A8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.A0); 
        if (c.V0 == 0u) {
            c.A1 = 0u; 
            goto L801AE6F0;
        }
        c.A1 = 0u; 
        if (c.V0 != c.S3) {
            { c.A1 = c.SP + 0x28u;  }
            goto L801AE6D8;
        }
        { c.A1 = c.SP + 0x28u;  }
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        { c.A0 = c.A0 + 0x1u;  }
        mem.WriteU16((c.SP + 0x28u), (ushort)0u); 
        { c.V0 = c.V0 << 3;  }
        { var _s = c.S2; var _t = c.V0; c.S1 = _s + _t;  }
        goto L801AE6A8;
        L801AE6D8: ;
        c.A3 = mem.ReadU16((c.S0 + 0x80u)); 
        c.A2 = c.S1; 
        { c.A3 = c.A3 >> 1;  }
        c.RA = 0x801AE6E8u; 
        SoTN_bo2.BO2_BlitChar(c, m);
        c.A0 = c.V0; 
        goto L801AE6A8;
        L801AE6F0: ;
        c.A0 = c.S2; 
        { c.V0 = 0u | 0x0180u;  }
        mem.WriteU32((c.SP + 0x10u), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x80u)); 
        c.A2 = 0u; 
        mem.WriteU32((c.SP + 0x14u), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.A3 = 0u; 
        mem.WriteU32((c.SP + 0x18u), c.V0); 
        c.RA = 0x801AE718u; 
        SoTN_main.LoadTPage(c, m);
        goto L801AE810;
        L801AE720: ;
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
            goto L801AE800;
        }
        c.A2 = 0u; 
        L801AE75C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.A2 != 0u) {
            goto L801AE7AC;
        }
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = c.V0 + 0x8u;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        c.V0 = c.LO; 
        if ((int)c.V0 >= 0) {
            goto L801AE784;
        }
        { c.V0 = c.V0 + 0x7u;  }
        L801AE784: ;
        { c.V0 = (uint)((int)c.V0 >> 3);  }
        c.V1 = mem.ReadU16((c.A1 + 0xAu)); 
        c.A0 = mem.ReadU8((c.A1 + 0x12u)); 
        { var _s = c.V1; var _t = c.V0; c.V1 = _s + _t;  }
        { c.A0 = c.A0 - 0x10u;  }
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V1); 
        mem.WriteU16((c.A1 + 0x22u), (ushort)c.V1); 
        mem.WriteU8((c.A1 + 0x12u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x6u), (byte)c.A0); 
        goto L801AE7F0;
        L801AE7AC: ;
        c.V0 = mem.ReadU16((c.S0 + 0x82u)); 
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = c.V0 + 0x8u;  }
        { var _s = c.V0; var _t = c.V1; var _r = (long)(int)_s * (int)_t; c.LO = (uint)_r; c.HI = (uint)(_r >> 32);  }
        c.V0 = c.LO; 
        if ((int)c.V0 >= 0) {
            goto L801AE7CC;
        }
        { c.V0 = c.V0 + 0x7u;  }
        L801AE7CC: ;
        { c.V0 = (uint)((int)c.V0 >> 3);  }
        c.V1 = mem.ReadU16((c.A1 + 0x22u)); 
        c.A0 = mem.ReadU8((c.A1 + 0x29u)); 
        { var _s = c.V1; var _t = c.V0; c.V1 = _s - _t;  }
        { c.A0 = c.A0 - 0x10u;  }
        mem.WriteU16((c.A1 + 0x16u), (ushort)c.V1); 
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V1); 
        mem.WriteU8((c.A1 + 0x29u), (byte)c.A0); 
        mem.WriteU8((c.A1 + 0x1Du), (byte)c.A0); 
        L801AE7F0: ;
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801AE75C;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801AE800: ;
        c.V1 = mem.ReadU16((c.S0 + 0x86u)); 
        { c.V0 = 0u | 0x0008u;  }
        if (c.V1 != c.V0) {
            goto L801AE87C;
        }
        L801AE810: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        mem.WriteU16((c.S0 + 0x86u), (ushort)0u); 
        { c.V0 = c.V0 + 0x1u;  }
        L801AE81C: ;
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        goto L801AE87C;
        L801AE824: ;
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
            goto L801AE87C;
        }
        c.A0 = c.S0; 
        c.RA = 0x801AE87Cu; 
        Dispatcher.Call(c, m, 0x801AAA88u);
        L801AE87C: ;
        c.RA = mem.ReadU32((c.SP + 0x48u)); 
        c.S3 = mem.ReadU32((c.SP + 0x44u)); 
        c.S2 = mem.ReadU32((c.SP + 0x40u)); 
        c.S1 = mem.ReadU32((c.SP + 0x3Cu)); 
        c.S0 = mem.ReadU32((c.SP + 0x38u)); 
        { c.SP = c.SP + 0x50u;  }
        return;
        }
}
