using RecompOne.Runtime.Context;
using RecompOne.Runtime.Dispatch;
using RecompOne.Runtime.Memory;

namespace Recompiled.Translation;

public static class ScriptFunctions_sel
{
    public static bool SEL_ClearBackbuffer(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("sel", m, false)) return true;
        SEL_ClearBackbuffer_Body(c, m);
        return false;
    }
    private static void SEL_ClearBackbuffer_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "sel", 0x801BC354u);
        { c.SP = c.SP - 0x18u;  }
        mem.WriteU32((c.SP + 0x10u), c.RA); 
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x25A4u;  }
        c.A1 = 0u; 
        c.A2 = 0u; 
        c.A3 = 0u; 
        c.RA = 0x801B1914u; 
        DialogueGlyphs.Clear(c, mem);
        c.RA = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x18u;  }
        return;
        }
    public static bool func_801B5A7C(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("sel", m, false)) return true;
        func_801B5A7C_Body(c, m);
        return false;
    }
    private static void func_801B5A7C_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "sel", 0x801BC354u);
        { c.SP = c.SP - 0x18u;  }
        c.V1 = 0x80070000u; 
        c.V1 = mem.ReadU16((c.V1 + 0x39E8u)); 
        c.A0 = 0x80070000u; 
        { c.A0 = c.A0 + 0x39B8u;  }
        { c.V0 = c.V1 < 0x00000005u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            mem.WriteU32((c.SP + 0x10u), c.RA); 
            goto L801B60C4;
        }
        mem.WriteU32((c.SP + 0x10u), c.RA); 
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x7C18u)); 
        switch (c.V0)
        {
            case 0x801B5AB8u: goto L801B5AB8;
            case 0x801B5BF4u: goto L801B5BF4;
            case 0x801B5D3Cu: goto L801B5D3C;
            case 0x801B5DF8u: goto L801B5DF8;
            case 0x801B5F68u: goto L801B5F68;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801B5AB8: ;
        c.V1 = mem.ReadU16((c.A0 + 0x2Cu)); 
        { c.V0 = c.V1 < 0x00000005u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801B5BE4;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x7C30u)); 
        switch (c.V0)
        {
            case 0x801B5AE4u: goto L801B5AE4;
            case 0x801B5AF8u: goto L801B5AF8;
            case 0x801B5B1Cu: goto L801B5B1C;
            case 0x801B5B90u: goto L801B5B90;
            case 0x801B5BC8u: goto L801B5BC8;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801B5AE4: ;
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        c.At = 0x80190000u; 
        mem.WriteU8((c.At - 0x43B6u), (byte)0u); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B5BC0;
        L801B5AF8: ;
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C1Cu), c.V0); 
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        c.V1 = 0xFFC00000u; 
        c.At = 0x801D0000u; 
        mem.WriteU32((c.At + 0x6B24u), c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B5BC0;
        L801B5B1C: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C1Cu)); 
        if (c.V0 != 0u) {
            goto L801B5B48;
        }
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C18u)); 
        { c.V0 = c.V0 | 0x0001u;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C18u), c.V0); 
        L801B5B48: ;
        c.V0 = 0x801D0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x6B24u)); 
        if (c.V0 == 0u) {
            { c.V0 = c.V0 + 0x2000u;  }
            goto L801B5B64;
        }
        { c.V0 = c.V0 + 0x2000u;  }
        c.At = 0x801D0000u; 
        mem.WriteU32((c.At + 0x6B24u), c.V0); 
        L801B5B64: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C18u)); 
        { c.V0 = c.V0 & 0x0002u;  }
        if (c.V0 == 0u) {
            { c.V1 = 0u | 0x0001u;  }
            goto L801B5BE4;
        }
        { c.V1 = 0u | 0x0001u;  }
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C1Cu), c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B5BC0;
        L801B5B90: ;
        { c.V0 = 0u | 0x0002u;  }
        c.At = 0x80070000u; 
        mem.WriteU16((c.At + 0x37B0u), (ushort)c.V0); 
        c.At = 0x80070000u; 
        mem.WriteU16((c.At + 0x37D4u), (ushort)0u); 
        c.At = 0x80070000u; 
        mem.WriteU16((c.At + 0x37D6u), (ushort)0u); 
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        { c.V1 = 0u | 0x0001u;  }
        c.At = 0x80070000u; 
        mem.WriteU16((c.At + 0x3798u), (ushort)c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        L801B5BC0: ;
        mem.WriteU16((c.A0 + 0x2Cu), (ushort)c.V0); 
        goto L801B5BE4;
        L801B5BC8: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C1Cu)); 
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801B5BE4;
        }
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x2FD0u), c.V0); 
        L801B5BE4: ;
        c.RA = 0x801B5BECu; 
        SoTN_sel.func_801B5350(c, m);
        goto L801B60C4;
        L801B5BF4: ;
        c.V1 = mem.ReadU16((c.A0 + 0x2Cu)); 
        { c.V0 = c.V1 < 0x00000006u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801B5D2C;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x7C48u)); 
        switch (c.V0)
        {
            case 0x801B5C20u: goto L801B5C20;
            case 0x801B5C38u: goto L801B5C38;
            case 0x801B5C5Cu: goto L801B5C5C;
            case 0x801B5CB4u: goto L801B5CB4;
            case 0x801B5CFCu: goto L801B5CFC;
            case 0x801B5D10u: goto L801B5D10;
            case 0x801B5D68u: goto L801B5D68;
            case 0x801B5D8Cu: goto L801B5D8C;
            case 0x801B5FDCu: goto L801B5FDC;
            case 0x801B5DB0u: goto L801B5DB0;
            case 0x801B607Cu: goto L801B607C;
            case 0x801B6090u: goto L801B6090;
            case 0x801B5E24u: goto L801B5E24;
            case 0x801B5E3Cu: goto L801B5E3C;
            case 0x801B5E60u: goto L801B5E60;
            case 0x801B5EA0u: goto L801B5EA0;
            case 0x801B5ED4u: goto L801B5ED4;
            case 0x801B5F0Cu: goto L801B5F0C;
            case 0x801B5F3Cu: goto L801B5F3C;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801B5C20: ;
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        { c.V1 = 0u | 0x0001u;  }
        c.At = 0x80190000u; 
        mem.WriteU8((c.At - 0x43B6u), (byte)c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B5D08;
        L801B5C38: ;
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C1Cu), c.V0); 
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        c.V1 = 0xFFC00000u; 
        c.At = 0x801D0000u; 
        mem.WriteU32((c.At + 0x6B24u), c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B5D08;
        L801B5C5C: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C1Cu)); 
        if (c.V0 != 0u) {
            goto L801B5C90;
        }
        c.V1 = mem.ReadU16((c.A0 + 0x2Cu)); 
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C18u)); 
        { c.V1 = c.V1 + 0x1u;  }
        { c.V0 = c.V0 | 0x0005u;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C18u), c.V0); 
        mem.WriteU16((c.A0 + 0x2Cu), (ushort)c.V1); 
        L801B5C90: ;
        c.V0 = 0x801D0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x6B24u)); 
        if (c.V0 == 0u) {
            { c.V0 = c.V0 + 0x4000u;  }
            goto L801B5D2C;
        }
        { c.V0 = c.V0 + 0x4000u;  }
        c.At = 0x801D0000u; 
        mem.WriteU32((c.At + 0x6B24u), c.V0); 
        goto L801B5D2C;
        L801B5CB4: ;
        c.V0 = 0x801D0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x6B24u)); 
        if (c.V0 == 0u) {
            { c.V0 = c.V0 + 0x4000u;  }
            goto L801B5CD0;
        }
        { c.V0 = c.V0 + 0x4000u;  }
        c.At = 0x801D0000u; 
        mem.WriteU32((c.At + 0x6B24u), c.V0); 
        L801B5CD0: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C18u)); 
        { c.V0 = c.V0 & 0x0002u;  }
        if (c.V0 == 0u) {
            { c.V1 = 0u | 0x0001u;  }
            goto L801B5D2C;
        }
        { c.V1 = 0u | 0x0001u;  }
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C1Cu), c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B5D08;
        L801B5CFC: ;
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        L801B5D08: ;
        mem.WriteU16((c.A0 + 0x2Cu), (ushort)c.V0); 
        goto L801B5D2C;
        L801B5D10: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C1Cu)); 
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801B5D2C;
        }
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x2FD0u), c.V0); 
        L801B5D2C: ;
        { c.A0 = 0u | 0x0001u;  }
        c.RA = 0x801B5D34u; 
        SoTN_sel.func_801B585C(c, m);
        goto L801B60BC;
        L801B5D3C: ;
        c.V1 = mem.ReadU16((c.A0 + 0x2Cu)); 
        { c.V0 = c.V1 < 0x00000006u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801B60AC;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x7C60u)); 
        switch (c.V0)
        {
            case 0x801B5D68u: goto L801B5D68;
            case 0x801B5D8Cu: goto L801B5D8C;
            case 0x801B5FDCu: goto L801B5FDC;
            case 0x801B5DB0u: goto L801B5DB0;
            case 0x801B607Cu: goto L801B607C;
            case 0x801B6090u: goto L801B6090;
            case 0x801B5E24u: goto L801B5E24;
            case 0x801B5E3Cu: goto L801B5E3C;
            case 0x801B5E60u: goto L801B5E60;
            case 0x801B5EA0u: goto L801B5EA0;
            case 0x801B5ED4u: goto L801B5ED4;
            case 0x801B5F0Cu: goto L801B5F0C;
            case 0x801B5F3Cu: goto L801B5F3C;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801B5D68: ;
        c.V1 = mem.ReadU16((c.A0 + 0x2Cu)); 
        { c.V0 = 0u | 0x0002u;  }
        c.At = 0x80190000u; 
        mem.WriteU8((c.At - 0x43B6u), (byte)c.V0); 
        c.At = 0x80190000u; 
        mem.WriteU8((c.At - 0x43B0u), (byte)0u); 
        { c.V1 = c.V1 + 0x1u;  }
        mem.WriteU16((c.A0 + 0x2Cu), (ushort)c.V1); 
        goto L801B60AC;
        L801B5D8C: ;
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C1Cu), c.V0); 
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        c.V1 = 0xFFC00000u; 
        c.At = 0x801D0000u; 
        mem.WriteU32((c.At + 0x6B24u), c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B6088;
        L801B5DB0: ;
        c.V0 = 0x801D0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x6B24u)); 
        if (c.V0 == 0u) {
            { c.V0 = c.V0 + 0x4000u;  }
            goto L801B5DCC;
        }
        { c.V0 = c.V0 + 0x4000u;  }
        c.At = 0x801D0000u; 
        mem.WriteU32((c.At + 0x6B24u), c.V0); 
        L801B5DCC: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C18u)); 
        { c.V0 = c.V0 & 0x0002u;  }
        if (c.V0 == 0u) {
            { c.V1 = 0u | 0x0001u;  }
            goto L801B60AC;
        }
        { c.V1 = 0u | 0x0001u;  }
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C1Cu), c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B6088;
        L801B5DF8: ;
        c.V1 = mem.ReadU16((c.A0 + 0x2Cu)); 
        { c.V0 = c.V1 < 0x00000007u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801B5F58;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x7C78u)); 
        switch (c.V0)
        {
            case 0x801B5E24u: goto L801B5E24;
            case 0x801B5E3Cu: goto L801B5E3C;
            case 0x801B5E60u: goto L801B5E60;
            case 0x801B5EA0u: goto L801B5EA0;
            case 0x801B5ED4u: goto L801B5ED4;
            case 0x801B5F0Cu: goto L801B5F0C;
            case 0x801B5F3Cu: goto L801B5F3C;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801B5E24: ;
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        { c.V1 = 0u | 0x0003u;  }
        c.At = 0x80190000u; 
        mem.WriteU8((c.At - 0x43B6u), (byte)c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B5F34;
        L801B5E3C: ;
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C1Cu), c.V0); 
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        c.V1 = 0xFFC00000u; 
        c.At = 0x801D0000u; 
        mem.WriteU32((c.At + 0x6B24u), c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B5F34;
        L801B5E60: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C1Cu)); 
        if (c.V0 != 0u) {
            goto L801B5E84;
        }
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.A0 + 0x2Cu), (ushort)c.V0); 
        L801B5E84: ;
        c.V0 = 0x801D0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x6B24u)); 
        if (c.V0 == 0u) {
            { c.V0 = c.V0 + 0x4000u;  }
            goto L801B5F58;
        }
        { c.V0 = c.V0 + 0x4000u;  }
        goto L801B5EB4;
        L801B5EA0: ;
        c.V0 = 0x801D0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x6B24u)); 
        if (c.V0 == 0u) {
            { c.V0 = c.V0 + 0x4000u;  }
            goto L801B5EC4;
        }
        { c.V0 = c.V0 + 0x4000u;  }
        L801B5EB4: ;
        c.At = 0x801D0000u; 
        mem.WriteU32((c.At + 0x6B24u), c.V0); 
        goto L801B5F58;
        L801B5EC4: ;
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        { c.V1 = 0u | 0x0080u;  }
        mem.WriteU32((c.A0 + 0x8Cu), c.V1); 
        goto L801B5F30;
        L801B5ED4: ;
        c.V0 = mem.ReadU32((c.A0 + 0x8Cu)); 
        { c.V0 = c.V0 - 0x1u;  }
        if (c.V0 != 0u) {
            mem.WriteU32((c.A0 + 0x8Cu), c.V0); 
            goto L801B5F58;
        }
        mem.WriteU32((c.A0 + 0x8Cu), c.V0); 
        c.V1 = mem.ReadU16((c.A0 + 0x2Cu)); 
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C18u)); 
        { c.V1 = c.V1 + 0x1u;  }
        { c.V0 = c.V0 | 0x0001u;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C18u), c.V0); 
        mem.WriteU16((c.A0 + 0x2Cu), (ushort)c.V1); 
        goto L801B5F58;
        L801B5F0C: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C18u)); 
        { c.V0 = c.V0 & 0x0002u;  }
        if (c.V0 == 0u) {
            { c.V1 = 0u | 0x0001u;  }
            goto L801B5F58;
        }
        { c.V1 = 0u | 0x0001u;  }
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C1Cu), c.V1); 
        L801B5F30: ;
        { c.V0 = c.V0 + 0x1u;  }
        L801B5F34: ;
        mem.WriteU16((c.A0 + 0x2Cu), (ushort)c.V0); 
        goto L801B5F58;
        L801B5F3C: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C1Cu)); 
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801B5F58;
        }
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x2FD0u), c.V0); 
        L801B5F58: ;
        c.RA = 0x801B5F60u; 
        SoTN_sel.func_801B5548(c, m);
        goto L801B60C4;
        L801B5F68: ;
        c.V1 = mem.ReadU16((c.A0 + 0x2Cu)); 
        { c.V0 = c.V1 < 0x00000006u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801B60AC;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x7C98u)); 
        switch (c.V0)
        {
            case 0x801B5F94u: goto L801B5F94;
            case 0x801B5FB8u: goto L801B5FB8;
            case 0x801B5FDCu: goto L801B5FDC;
            case 0x801B6034u: goto L801B6034;
            case 0x801B607Cu: goto L801B607C;
            case 0x801B6090u: goto L801B6090;
            case 0x801B6110u: goto L801B6110;
            case 0x801B6158u: goto L801B6158;
            case 0x801B61C4u: goto L801B61C4;
            case 0x801B6264u: goto L801B6264;
            case 0x801B6338u: goto L801B6338;
            case 0x801B6360u: goto L801B6360;
            case 0x801B6378u: goto L801B6378;
            case 0x801B639Cu: goto L801B639C;
            case 0x801B63E0u: goto L801B63E0;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801B5F94: ;
        { c.V0 = 0u | 0x0002u;  }
        c.At = 0x80190000u; 
        mem.WriteU8((c.At - 0x43B6u), (byte)c.V0); 
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        { c.V1 = 0u | 0x0001u;  }
        c.At = 0x80190000u; 
        mem.WriteU8((c.At - 0x43B0u), (byte)c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B6088;
        L801B5FB8: ;
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C1Cu), c.V0); 
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        c.V1 = 0xFFC00000u; 
        c.At = 0x801D0000u; 
        mem.WriteU32((c.At + 0x6B24u), c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B6088;
        L801B5FDC: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C1Cu)); 
        if (c.V0 != 0u) {
            goto L801B6010;
        }
        c.V1 = mem.ReadU16((c.A0 + 0x2Cu)); 
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C18u)); 
        { c.V1 = c.V1 + 0x1u;  }
        { c.V0 = c.V0 | 0x0005u;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C18u), c.V0); 
        mem.WriteU16((c.A0 + 0x2Cu), (ushort)c.V1); 
        L801B6010: ;
        c.V0 = 0x801D0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x6B24u)); 
        if (c.V0 == 0u) {
            { c.V0 = c.V0 + 0x4000u;  }
            goto L801B60AC;
        }
        { c.V0 = c.V0 + 0x4000u;  }
        c.At = 0x801D0000u; 
        mem.WriteU32((c.At + 0x6B24u), c.V0); 
        goto L801B60AC;
        L801B6034: ;
        c.V0 = 0x801D0000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x6B24u)); 
        if (c.V0 == 0u) {
            { c.V0 = c.V0 + 0x4000u;  }
            goto L801B6050;
        }
        { c.V0 = c.V0 + 0x4000u;  }
        c.At = 0x801D0000u; 
        mem.WriteU32((c.At + 0x6B24u), c.V0); 
        L801B6050: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C18u)); 
        { c.V0 = c.V0 & 0x0002u;  }
        if (c.V0 == 0u) {
            { c.V1 = 0u | 0x0001u;  }
            goto L801B60AC;
        }
        { c.V1 = 0u | 0x0001u;  }
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C1Cu), c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B6088;
        L801B607C: ;
        c.V0 = mem.ReadU16((c.A0 + 0x2Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        L801B6088: ;
        mem.WriteU16((c.A0 + 0x2Cu), (ushort)c.V0); 
        goto L801B60AC;
        L801B6090: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C1Cu)); 
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801B60AC;
        }
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x2FD0u), c.V0); 
        L801B60AC: ;
        c.A0 = 0u; 
        c.RA = 0x801B60B4u; 
        SoTN_sel.func_801B585C(c, m);
        c.RA = 0x801B60BCu; 
        SoTN_sel.func_801B54C8(c, m);
        L801B60BC: ;
        c.RA = 0x801B60C4u; 
        SoTN_sel.func_801B55C8(c, m);
        L801B60C4: ;
        c.RA = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x18u;  }
        return;
        c.V1 = 0x80040000u; 
        c.V1 = mem.ReadU32((c.V1 - 0x365Cu)); 
        { c.SP = c.SP - 0x20u;  }
        mem.WriteU32((c.SP + 0x18u), c.RA); 
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        { c.V0 = c.V1 < 0x00000009u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            mem.WriteU32((c.SP + 0x10u), c.S0); 
            goto L801B6414;
        }
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x7CB0u)); 
        switch (c.V0)
        {
            case 0x801B6110u: goto L801B6110;
            case 0x801B6158u: goto L801B6158;
            case 0x801B61C4u: goto L801B61C4;
            case 0x801B6264u: goto L801B6264;
            case 0x801B6338u: goto L801B6338;
            case 0x801B6360u: goto L801B6360;
            case 0x801B6378u: goto L801B6378;
            case 0x801B639Cu: goto L801B639C;
            case 0x801B63E0u: goto L801B63E0;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801B6110: ;
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C50u)); 
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801B6414;
        }
        { c.V0 = 0u | 0x0001u;  }
        c.V1 = 0x80090000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x78B4u)); 
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x38D8u), c.V0); 
        if (c.V1 != 0u) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801B6140;
        }
        { c.V0 = 0u | 0x0003u;  }
        { c.V0 = 0u | 0x0002u;  }
        L801B6140: ;
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x3F00u), c.V0); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x365Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B63D0;
        L801B6158: ;
        c.RA = 0x801B6160u; 
        SoTN_sel.func_801B9C80(c, m);
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x38D8u)); 
        if (c.V0 != 0u) {
            goto L801B6184;
        }
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x3F00u), 0u); 
        c.RA = 0x801B6184u; 
        SoTN_sel.SEL_ClearBackbuffer(c, m);
        L801B6184: ;
        c.V0 = 0x80090000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x78B4u)); 
        if (c.V0 == 0u) {
            goto L801B61A8;
        }
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3060u)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B61AC;
        L801B61A8: ;
        { c.V0 = 0u | 0x0100u;  }
        L801B61AC: ;
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x3060u), c.V0); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x365Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B63D0;
        L801B61C4: ;
        c.S0 = 0x80070000u; 
        { c.S0 = c.S0 + 0x33D8u;  }
        c.S1 = 0u; 
        L801B61D0: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A0 = c.S0; 
        c.RA = 0x801B61D8u; 
        Dispatcher.Call(c, m, 0x801B4B30u);
        { c.S1 = c.S1 + 0x1u;  }
        { c.V0 = (int)c.S1 < 9 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.S0 = c.S0 + 0xBCu;  }
            goto L801B61D0;
        }
        { c.S0 = c.S0 + 0xBCu;  }
        c.S0 = 0x80040000u; 
        { c.S0 = c.S0 - 0x382Cu;  }
        c.V0 = 0x80090000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x78B4u)); 
        c.V1 = mem.ReadU32(c.S0); 
        c.At = 0x801D0000u; 
        mem.WriteU32((c.At + 0x6B24u), 0u); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x2FD0u), 0u); 
        { c.V0 = c.V0 - 0x1u;  }
        c.At = 0x80070000u; 
        mem.WriteU16((c.At + 0x39E8u), (ushort)c.V0); 
        { c.A0 = 0u | 0x0016u;  }
        c.RA = 0x801B6220u;
        Dispatcher.Call(c, m, c.V1);
        c.V0 = mem.ReadU32(c.S0); 
        c.A0 = 0u; 
        c.RA = 0x801B6230u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = mem.ReadU32(c.S0); 
        { c.A0 = 0u | 0x8005u;  }
        c.RA = 0x801B6240u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3828u)); 
        { c.A0 = 0u | 0x8000u;  }
        c.RA = 0x801B6254u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x365Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B63D0;
        L801B6264: ;
        { c.S1 = 0u | 0x0003u;  }
        c.RA = 0x801B626Cu; 
        SoTN_sel.func_801B5A7C(c, m);
        c.S0 = 0x80070000u; 
        { c.S0 = c.S0 + 0x36C8u;  }
        c.A0 = c.S0; 
        c.RA = 0x801B627Cu; 
        SoTN_sel.SEL_EntityCutscene(c, m);
        c.RA = 0x801B6284u; 
        SoTN_sel.func_801B4C68(c, m);
        c.RA = 0x801B628Cu; 
        Dispatcher.Call(c, m, 0x801B519Cu);
        c.RA = 0x801B6294u; 
        SoTN_sel.func_801B4D78(c, m);
        c.RA = 0x801B629Cu; 
        SoTN_sel.func_801B4DE0(c, m);
        { c.A1 = 0u | 0xFFFFu;  }
        { c.A0 = c.S0 - 0xBAu;  }
        L801B62A4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V1 = 0x801D0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x6B24u)); 
        if ((int)c.V1 >= 0) {
            { c.S1 = c.S1 + 0x1u;  }
            goto L801B62BC;
        }
        { c.S1 = c.S1 + 0x1u;  }
        { var _s = c.V1; var _t = c.A1; c.V1 = _s + _t;  }
        L801B62BC: ;
        c.V0 = mem.ReadU16((c.A0 + 0x80u)); 
        { c.V1 = (uint)((int)c.V1 >> 16);  }
        { var _s = c.V1; var _t = c.V0; c.V1 = _s + _t;  }
        mem.WriteU16(c.A0, (ushort)c.V1); 
        { c.V0 = (int)c.S1 < 8 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.A0 = c.A0 + 0xBCu;  }
            goto L801B62A4;
        }
        { c.A0 = c.A0 + 0xBCu;  }
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2FD0u)); 
        if (c.V0 == 0u) {
            c.S1 = 0u; 
            goto L801B6414;
        }
        c.S1 = 0u; 
        c.S0 = 0x80070000u; 
        { c.S0 = c.S0 + 0x33D8u;  }
        L801B62F4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A0 = c.S0; 
        c.RA = 0x801B62FCu; 
        Dispatcher.Call(c, m, 0x801B4B30u);
        { c.S1 = c.S1 + 0x1u;  }
        { c.V0 = (int)c.S1 < 9 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.S0 = c.S0 + 0xBCu;  }
            goto L801B62F4;
        }
        { c.S0 = c.S0 + 0xBCu;  }
        c.S1 = 0u; 
        L801B6310: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A0 = c.S0; 
        c.RA = 0x801B6318u; 
        Dispatcher.Call(c, m, 0x801B4B30u);
        { c.S1 = c.S1 + 0x1u;  }
        { c.V0 = (int)c.S1 < 9 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.S0 = c.S0 + 0xBCu;  }
            goto L801B6310;
        }
        { c.S0 = c.S0 + 0xBCu;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x365Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B63D0;
        L801B6338: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x258Cu;  }
        c.A1 = 0u; 
        c.A2 = 0u; 
        c.A3 = 0u; 
        c.RA = 0x801B6350u; 
        DialogueGlyphs.Clear(c, mem);
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x365Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B63D0;
        L801B6360: ;
        c.RA = 0x801B6368u; 
        SoTN_sel.SetTitleDisplayBuffer256(c, m);
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x365Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B63D0;
        L801B6378: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x382Cu)); 
        c.A0 = 0u; 
        c.RA = 0x801B638Cu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x365Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B63D0;
        L801B639C: ;
        c.A0 = 0x80070000u; 
        { c.A0 = c.A0 + 0x36C8u;  }
        c.RA = 0x801B63ACu; 
        SoTN_sel.func_801B79D4(c, m);
        if (c.V0 == 0u) {
            { c.V1 = 0u | 0x0001u;  }
            goto L801B6414;
        }
        { c.V1 = 0u | 0x0001u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x365Cu)); 
        c.At = 0x80070000u; 
        mem.WriteU16((c.At + 0x34C0u), (ushort)0u); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C1Cu), c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        L801B63D0: ;
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x365Cu), c.V0); 
        goto L801B6414;
        L801B63E0: ;
        c.RA = 0x801B63E8u; 
        SoTN_sel.func_801B4FFC(c, m);
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C1Cu)); 
        if (c.V0 != 0u) {
            goto L801B6414;
        }
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3060u)); 
        { c.V0 = c.V0 + 0x1u;  }
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x3060u), c.V0); 
        L801B6414: ;
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool func_801B60D4(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("sel", m, false)) return true;
        func_801B60D4_Body(c, m);
        return false;
    }
    private static void func_801B60D4_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "sel", 0x801BC354u);
        c.V1 = 0x80040000u; 
        c.V1 = mem.ReadU32((c.V1 - 0x365Cu)); 
        { c.SP = c.SP - 0x20u;  }
        mem.WriteU32((c.SP + 0x18u), c.RA); 
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        { c.V0 = c.V1 < 0x00000009u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            mem.WriteU32((c.SP + 0x10u), c.S0); 
            goto L801B6414;
        }
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x7CB0u)); 
        switch (c.V0)
        {
            case 0x801B6110u: goto L801B6110;
            case 0x801B6158u: goto L801B6158;
            case 0x801B61C4u: goto L801B61C4;
            case 0x801B6264u: goto L801B6264;
            case 0x801B6338u: goto L801B6338;
            case 0x801B6360u: goto L801B6360;
            case 0x801B6378u: goto L801B6378;
            case 0x801B639Cu: goto L801B639C;
            case 0x801B63E0u: goto L801B63E0;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801B6110: ;
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C50u)); 
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801B6414;
        }
        { c.V0 = 0u | 0x0001u;  }
        c.V1 = 0x80090000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x78B4u)); 
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x38D8u), c.V0); 
        if (c.V1 != 0u) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801B6140;
        }
        { c.V0 = 0u | 0x0003u;  }
        { c.V0 = 0u | 0x0002u;  }
        L801B6140: ;
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x3F00u), c.V0); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x365Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B63D0;
        L801B6158: ;
        c.RA = 0x801B6160u; 
        SoTN_sel.func_801B9C80(c, m);
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x38D8u)); 
        if (c.V0 != 0u) {
            goto L801B6184;
        }
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x3F00u), 0u); 
        c.RA = 0x801B6184u; 
        SoTN_sel.SEL_ClearBackbuffer(c, m);
        L801B6184: ;
        c.V0 = 0x80090000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x78B4u)); 
        if (c.V0 == 0u) {
            goto L801B61A8;
        }
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3060u)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B61AC;
        L801B61A8: ;
        { c.V0 = 0u | 0x0100u;  }
        L801B61AC: ;
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x3060u), c.V0); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x365Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B63D0;
        L801B61C4: ;
        c.S0 = 0x80070000u; 
        { c.S0 = c.S0 + 0x33D8u;  }
        c.S1 = 0u; 
        L801B61D0: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A0 = c.S0; 
        c.RA = 0x801B61D8u; 
        Dispatcher.Call(c, m, 0x801B4B30u);
        { c.S1 = c.S1 + 0x1u;  }
        { c.V0 = (int)c.S1 < 9 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.S0 = c.S0 + 0xBCu;  }
            goto L801B61D0;
        }
        { c.S0 = c.S0 + 0xBCu;  }
        c.S0 = 0x80040000u; 
        { c.S0 = c.S0 - 0x382Cu;  }
        c.V0 = 0x80090000u; 
        c.V0 = mem.ReadU16((c.V0 + 0x78B4u)); 
        c.V1 = mem.ReadU32(c.S0); 
        c.At = 0x801D0000u; 
        mem.WriteU32((c.At + 0x6B24u), 0u); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x2FD0u), 0u); 
        { c.V0 = c.V0 - 0x1u;  }
        c.At = 0x80070000u; 
        mem.WriteU16((c.At + 0x39E8u), (ushort)c.V0); 
        { c.A0 = 0u | 0x0016u;  }
        c.RA = 0x801B6220u;
        Dispatcher.Call(c, m, c.V1);
        c.V0 = mem.ReadU32(c.S0); 
        c.A0 = 0u; 
        c.RA = 0x801B6230u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = mem.ReadU32(c.S0); 
        { c.A0 = 0u | 0x8005u;  }
        c.RA = 0x801B6240u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3828u)); 
        { c.A0 = 0u | 0x8000u;  }
        c.RA = 0x801B6254u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x365Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B63D0;
        L801B6264: ;
        { c.S1 = 0u | 0x0003u;  }
        c.RA = 0x801B626Cu; 
        SoTN_sel.func_801B5A7C(c, m);
        c.S0 = 0x80070000u; 
        { c.S0 = c.S0 + 0x36C8u;  }
        c.A0 = c.S0; 
        c.RA = 0x801B627Cu; 
        SoTN_sel.SEL_EntityCutscene(c, m);
        c.RA = 0x801B6284u; 
        SoTN_sel.func_801B4C68(c, m);
        c.RA = 0x801B628Cu; 
        Dispatcher.Call(c, m, 0x801B519Cu);
        c.RA = 0x801B6294u; 
        SoTN_sel.func_801B4D78(c, m);
        c.RA = 0x801B629Cu; 
        SoTN_sel.func_801B4DE0(c, m);
        { c.A1 = 0u | 0xFFFFu;  }
        { c.A0 = c.S0 - 0xBAu;  }
        L801B62A4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V1 = 0x801D0000u; 
        c.V1 = mem.ReadU32((c.V1 + 0x6B24u)); 
        if ((int)c.V1 >= 0) {
            { c.S1 = c.S1 + 0x1u;  }
            goto L801B62BC;
        }
        { c.S1 = c.S1 + 0x1u;  }
        { var _s = c.V1; var _t = c.A1; c.V1 = _s + _t;  }
        L801B62BC: ;
        c.V0 = mem.ReadU16((c.A0 + 0x80u)); 
        { c.V1 = (uint)((int)c.V1 >> 16);  }
        { var _s = c.V1; var _t = c.V0; c.V1 = _s + _t;  }
        mem.WriteU16(c.A0, (ushort)c.V1); 
        { c.V0 = (int)c.S1 < 8 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.A0 = c.A0 + 0xBCu;  }
            goto L801B62A4;
        }
        { c.A0 = c.A0 + 0xBCu;  }
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x2FD0u)); 
        if (c.V0 == 0u) {
            c.S1 = 0u; 
            goto L801B6414;
        }
        c.S1 = 0u; 
        c.S0 = 0x80070000u; 
        { c.S0 = c.S0 + 0x33D8u;  }
        L801B62F4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A0 = c.S0; 
        c.RA = 0x801B62FCu; 
        Dispatcher.Call(c, m, 0x801B4B30u);
        { c.S1 = c.S1 + 0x1u;  }
        { c.V0 = (int)c.S1 < 9 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.S0 = c.S0 + 0xBCu;  }
            goto L801B62F4;
        }
        { c.S0 = c.S0 + 0xBCu;  }
        c.S1 = 0u; 
        L801B6310: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A0 = c.S0; 
        c.RA = 0x801B6318u; 
        Dispatcher.Call(c, m, 0x801B4B30u);
        { c.S1 = c.S1 + 0x1u;  }
        { c.V0 = (int)c.S1 < 9 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.S0 = c.S0 + 0xBCu;  }
            goto L801B6310;
        }
        { c.S0 = c.S0 + 0xBCu;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x365Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B63D0;
        L801B6338: ;
        c.A0 = 0x80180000u; 
        { c.A0 = c.A0 + 0x258Cu;  }
        c.A1 = 0u; 
        c.A2 = 0u; 
        c.A3 = 0u; 
        c.RA = 0x801B6350u; 
        DialogueGlyphs.Clear(c, mem);
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x365Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B63D0;
        L801B6360: ;
        c.RA = 0x801B6368u; 
        SoTN_sel.SetTitleDisplayBuffer256(c, m);
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x365Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B63D0;
        L801B6378: ;
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x382Cu)); 
        c.A0 = 0u; 
        c.RA = 0x801B638Cu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x365Cu)); 
        { c.V0 = c.V0 + 0x1u;  }
        goto L801B63D0;
        L801B639C: ;
        c.A0 = 0x80070000u; 
        { c.A0 = c.A0 + 0x36C8u;  }
        c.RA = 0x801B63ACu; 
        SoTN_sel.func_801B79D4(c, m);
        if (c.V0 == 0u) {
            { c.V1 = 0u | 0x0001u;  }
            goto L801B6414;
        }
        { c.V1 = 0u | 0x0001u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x365Cu)); 
        c.At = 0x80070000u; 
        mem.WriteU16((c.At + 0x34C0u), (ushort)0u); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C1Cu), c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        L801B63D0: ;
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x365Cu), c.V0); 
        goto L801B6414;
        L801B63E0: ;
        c.RA = 0x801B63E8u; 
        SoTN_sel.func_801B4FFC(c, m);
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C1Cu)); 
        if (c.V0 != 0u) {
            goto L801B6414;
        }
        c.V0 = 0x80070000u; 
        c.V0 = mem.ReadU32((c.V0 + 0x3060u)); 
        { c.V0 = c.V0 + 0x1u;  }
        c.At = 0x80070000u; 
        mem.WriteU32((c.At + 0x3060u), c.V0); 
        L801B6414: ;
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool CutsceneUnk1_sel(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("sel", m, true)) return true;
        CutsceneUnk1_sel_Body(c, m);
        return false;
    }
    private static void CutsceneUnk1_sel_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "sel", 0x801BC354u);
        { c.V0 = 0u | 0x0002u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3CA0u), (ushort)c.V0); 
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3CA2u), (ushort)c.V0); 
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x3CA4u)); 
        { c.V1 = 0u | 0x0008u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C9Eu), (ushort)0u); 
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C9Au), (ushort)0u); 
        c.At = 0x801C0000u; 
        mem.WriteU8((c.At - 0x3C96u), (byte)0u); 
        c.At = 0x801C0000u; 
        mem.WriteU8((c.At - 0x3C95u), (byte)c.V1); 
        { c.V0 = c.V0 + 0x14u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3CA6u), (ushort)c.V0); 
        return;
        }
    public static bool SetCutsceneScript_sel(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("sel", m, true)) return true;
        SetCutsceneScript_sel_Body(c, m);
        return false;
    }
    private static void SetCutsceneScript_sel_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "sel", 0x801BC354u);
        c.A0 = DialogueScripts.ResolveStart("sel", c.A0, m);
        { c.SP = c.SP - 0x20u;  }
        mem.WriteU32((c.SP + 0x14u), c.S1); 
        c.S1 = c.A0; 
        { c.A0 = 0u | 0x0006u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = 0u | 0x0007u;  }
        mem.WriteU32((c.SP + 0x18u), c.RA); 
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.RA = 0x801B64A8u;
        Dispatcher.Call(c, m, c.V0);
        c.S0 = 0x801C0000u; 
        { c.S0 = c.S0 - 0x3C74u;  }
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        c.V1 = 0xFFFFFFFFu; 
        if (c.V0 != c.V1) {
            mem.WriteU32(c.S0, c.V0); 
            goto L801B64D0;
        }
        mem.WriteU32(c.S0, c.V0); 
        mem.WriteU32(c.S0, 0u); 
        c.V0 = 0u; 
        goto L801B6630;
        L801B64D0: ;
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3CACu), c.S1); 
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C70u), (ushort)0u); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C78u), c.V1); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C7Cu), c.V1); 
        c.RA = 0x801B64F8u; 
        SoTN_sel.CutsceneUnk1_sel(c, m);
        c.V1 = mem.ReadU32(c.S0); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C94u), c.V0); 
        c.V1 = mem.ReadU32(c.V0); 
        { c.A2 = 0u | 0x0008u;  }
        mem.WriteU16((c.V0 + 0x32u), (ushort)c.A2); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C90u), c.V1); 
        c.V0 = mem.ReadU32(c.V1); 
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A2); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C8Cu), c.V0); 
        c.V1 = mem.ReadU32(c.V0); 
        mem.WriteU16((c.V0 + 0x32u), (ushort)c.A2); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C88u), c.V1); 
        c.A0 = mem.ReadU32(c.V1); 
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A2); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C84u), c.A0); 
        c.A1 = mem.ReadU32(c.A0); 
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.A0 + 0x32u), (ushort)c.A2); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C80u), c.A1); 
        c.V1 = mem.ReadU32(c.A1); 
        { c.A0 = 0u | 0x0004u;  }
        mem.WriteU8((c.A1 + 0x7u), (byte)c.A0); 
        { c.A0 = 0u | 0x0003u;  }
        mem.WriteU16((c.A1 + 0x32u), (ushort)c.A2); 
        mem.WriteU8((c.V1 + 0x7u), (byte)c.A0); 
        { c.A0 = 0u | 0x007Fu;  }
        mem.WriteU8((c.V1 + 0x12u), (byte)c.A0); 
        mem.WriteU8((c.V1 + 0x6u), (byte)c.A0); 
        { c.A0 = 0u | 0x0004u;  }
        mem.WriteU8((c.V1 + 0x2Au), (byte)0u); 
        mem.WriteU8((c.V1 + 0x1Eu), (byte)0u); 
        mem.WriteU8((c.V1 + 0x29u), (byte)0u); 
        mem.WriteU8((c.V1 + 0x1Du), (byte)0u); 
        mem.WriteU8((c.V1 + 0x11u), (byte)0u); 
        mem.WriteU8((c.V1 + 0x5u), (byte)0u); 
        mem.WriteU8((c.V1 + 0x28u), (byte)0u); 
        mem.WriteU8((c.V1 + 0x1Cu), (byte)0u); 
        mem.WriteU8((c.V1 + 0x10u), (byte)0u); 
        mem.WriteU8((c.V1 + 0x4u), (byte)0u); 
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
        { c.A0 = 0u | 0x0010u;  }
        mem.WriteU8((c.V1 + 0x6u), (byte)c.A0); 
        mem.WriteU8((c.V1 + 0x5u), (byte)c.A0); 
        mem.WriteU8((c.V1 + 0x4u), (byte)c.A0); 
        { c.A0 = 0u | 0x0003u;  }
        mem.WriteU16((c.V1 + 0x8u), (ushort)c.A0); 
        { c.A0 = 0u | 0x002Fu;  }
        mem.WriteU16((c.V1 + 0xAu), (ushort)c.A0); 
        { c.A0 = 0u | 0x004Au;  }
        mem.WriteU8((c.V1 + 0xDu), (byte)c.A0); 
        { c.A0 = 0u | 0x01FCu;  }
        mem.WriteU16((c.V1 + 0x26u), (ushort)c.A0); 
        mem.WriteU16((c.V1 + 0x32u), (ushort)c.A2); 
        L801B6630: ;
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool CutsceneUnk3_sel(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("sel", m, false)) return true;
        CutsceneUnk3_sel_Body(c, m);
        return false;
    }
    private static void CutsceneUnk3_sel_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "sel", 0x801BC354u);
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
        c.RA = 0x801B6694u; 
        DialogueGlyphs.Clear(c, mem);
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool CutsceneUnk4_sel(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("sel", m, true)) return true;
        CutsceneUnk4_sel_Body(c, m);
        return false;
    }
    private static void CutsceneUnk4_sel_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "sel", 0x801BC354u);
        DialogueGlyphs.PrepareLine(c, mem);
        { c.SP = c.SP - 0x18u;  }
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.S0 = 0x801C0000u; 
        { c.S0 = c.S0 - 0x3C9Eu;  }
        mem.WriteU32((c.SP + 0x14u), c.RA); 
        c.A0 = (uint)(short)mem.ReadU16(c.S0); 
        c.RA = 0x801B66C4u; 
        SoTN_sel.CutsceneUnk3_sel(c, m);
        c.V0 = (uint)(short)mem.ReadU16(c.S0); 
        { c.V0 = c.V0 << 2;  }
        { var _s = c.S0; var _t = c.V0; c.V0 = _s + _t;  }
        c.V1 = mem.ReadU32((c.V0 + 0xAu)); 
        { c.V0 = 0u | 0x0010u;  }
        mem.WriteU16((c.V1 + 0x1Au), (ushort)c.V0); 
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x3C98u)); 
        mem.WriteU16((c.V1 + 0xEu), (ushort)c.V0); 
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x3CA6u)); 
        mem.WriteU8((c.V1 + 0xCu), (byte)0u); 
        mem.WriteU16((c.V1 + 0xAu), (ushort)c.V0); 
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x3CA8u)); 
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
    public static bool DrawCutsceneActorName_sel(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("sel", m, true)) return true;
        DrawCutsceneActorName_sel_Body(c, m);
        return false;
    }
    private static void DrawCutsceneActorName_sel_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "sel", 0x801BC354u);
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
        c.A2 = mem.ReadU32((c.At + 0x8D0u)); 
        { c.V0 = 0u | 0x00FFu;  }
        L801B6790: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V1 = mem.ReadU8(c.A2); 
        if (c.V1 != c.V0) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801B67B0;
        }
        { c.A2 = c.A2 + 0x1u;  }
        c.V1 = mem.ReadU8(c.A2); 
        if (c.V1 == 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801B67C0;
        }
        { c.A2 = c.A2 + 0x1u;  }
        L801B67B0: ;
        if (c.V1 == 0u) {
            goto L801B6790;
        }
        { c.A1 = c.A1 + 0x1u;  }
        goto L801B6790;
        L801B67C0: ;
        { c.A0 = 0u | 0x0006u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        { c.A1 = c.A1 & 0xFFFFu;  }
        c.RA = 0x801B67D8u;
        Dispatcher.Call(c, m, c.V0);
        { c.V0 = c.V0 << 16;  }
        { c.A1 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.A1 != c.V0) {
            { c.V0 = c.A1 << 1;  }
            goto L801B67FC;
        }
        { c.V0 = c.A1 << 1;  }
        c.A0 = c.S1; 
        c.RA = 0x801B67F4u; 
        Dispatcher.Call(c, m, 0x801B4B30u);
        goto L801B68C8;
        L801B67FC: ;
        { var _s = c.V0; var _t = c.A1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.A1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.A0 = _s + _t;  }
        c.V1 = 0x801C0000u; 
        { c.V1 = c.V1 - 0x3C78u;  }
        { c.V0 = c.S0 & 0xFFFFu;  }
        { c.V0 = c.V0 << 2;  }
        mem.WriteU32(c.V1, c.A1); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A2 = mem.ReadU32((c.At + 0x8D0u)); 
        if (c.A0 == 0u) {
            { c.A3 = 0u | 0x0038u;  }
            goto L801B68C8;
        }
        { c.A3 = 0u | 0x0038u;  }
        { c.T4 = 0u | 0x0006u;  }
        { c.T3 = 0u | 0x001Eu;  }
        { c.T2 = 0u | 0x0196u;  }
        { c.T1 = 0u | 0x01FFu;  }
        { c.A1 = 0u | 0x0008u;  }
        { c.T0 = c.V1 - 0x2Cu;  }
        L801B6858: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V1 = mem.ReadU8(c.A2); 
        if (c.V1 != 0u) {
            { c.A2 = c.A2 + 0x1u;  }
            goto L801B6870;
        }
        { c.A2 = c.A2 + 0x1u;  }
        { c.A3 = c.A3 + 0x4u;  }
        goto L801B68BC;
        L801B6870: ;
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
        L801B68BC: ;
        if (c.A0 != 0u) {
            goto L801B6858;
        }
        L801B68C8: ;
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        c.S1 = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool SetCutsceneEnd_sel(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("sel", m, true)) return true;
        SetCutsceneEnd_sel_Body(c, m);
        return false;
    }
    private static void SetCutsceneEnd_sel_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "sel", 0x801BC354u);
        c.V0 = 0x00100000u; 
        { var _s = c.A0; var _t = c.V0; c.A0 = _s + _t;  }
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C6Cu), c.A0); 
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C6Eu), (ushort)0u); 
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C70u), (ushort)c.V0); 
        return;
        }
    public static bool ScaleCutsceneAvatar_sel(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("sel", m, true)) return true;
        ScaleCutsceneAvatar_sel_Body(c, m);
        return false;
    }
    private static void ScaleCutsceneAvatar_sel_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "sel", 0x801BC354u);
        c.V0 = 0x801C0000u; 
        c.V0 = (uint)(short)mem.ReadU16((c.V0 - 0x3C9Eu)); 
        { c.A3 = c.V0 + (uint)(mem.LineCount - DialogueGlyphs.VisibleLines(mem));  }
        { c.V0 = (int)c.A3 < 5 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801B693C;
        }
        { c.A3 = c.A3 - 0x5u;  }
        L801B692C: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (int)c.A3 < 5 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.A3 = c.A3 - 0x5u;  }
            goto L801B692C;
        }
        { c.A3 = c.A3 - 0x5u;  }
        { c.A3 = c.A3 + 0x5u;  }
        L801B693C: ;
        c.V0 = mem.ReadU16((c.A1 + 0x2Eu)); 
        if (c.V0 != 0u) {
            c.V1 = 0u; 
            goto L801B69A0;
        }
        c.V1 = 0u; 
        { c.V0 = c.A3 << 2;  }
        c.At = 0x801C0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A2 = mem.ReadU32((c.At - 0x3C94u)); 
        c.V0 = mem.ReadU8((c.A2 + 0x19u)); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s - _t;  }
        mem.WriteU8((c.A2 + 0x19u), (byte)c.V0); 
        c.V0 = mem.ReadU8((c.A2 + 0xDu)); 
        c.V1 = mem.ReadU8((c.A2 + 0x19u)); 
        { var _s = c.A0; var _t = c.V0; c.V0 = _s + _t;  }
        if (c.V1 != 0u) {
            mem.WriteU8((c.A2 + 0xDu), (byte)c.V0); 
            goto L801B699C;
        }
        mem.WriteU8((c.A2 + 0xDu), (byte)c.V0); 
        c.V0 = mem.ReadU16((c.A1 + 0x2Eu)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.A1 + 0x2Eu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.A2 + 0x32u), (ushort)c.V0); 
        L801B699C: ;
        c.V1 = 0u; 
        L801B69A0: ;
        { c.A0 = c.A0 & 0x00FFu;  }
        c.A1 = 0x801C0000u; 
        { c.A1 = c.A1 - 0x3C94u;  }
        L801B69AC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.V1 == c.A3) {
            goto L801B69CC;
        }
        c.A2 = mem.ReadU32(c.A1); 
        c.V0 = mem.ReadU16((c.A2 + 0xAu)); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s - _t;  }
        mem.WriteU16((c.A2 + 0xAu), (ushort)c.V0); 
        L801B69CC: ;
        { c.V1 = c.V1 + 0x1u;  }
        { c.V0 = (int)c.V1 < 5 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.A1 = c.A1 + 0x4u;  }
            goto L801B69AC;
        }
        { c.A1 = c.A1 + 0x4u;  }
        c.V1 = 0x801C0000u; 
        { c.V1 = c.V1 - 0x3C9Cu;  }
        c.V0 = mem.ReadU16(c.V1); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16(c.V1, (ushort)c.V0); 
        return;
        }
    public static bool SEL_EntityCutscene(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("sel", m, true)) return true;
        SEL_EntityCutscene_Body(c, m);
        return false;
    }
    private static void SEL_EntityCutscene_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "sel", 0x801BC354u);
        { c.SP = c.SP - 0x48u;  }
        mem.WriteU32((c.SP + 0x30u), c.S2); 
        c.S2 = c.A0; 
        mem.WriteU32((c.SP + 0x40u), c.RA); 
        mem.WriteU32((c.SP + 0x3Cu), c.S5); 
        mem.WriteU32((c.SP + 0x38u), c.S4); 
        mem.WriteU32((c.SP + 0x34u), c.S3); 
        mem.WriteU32((c.SP + 0x2Cu), c.S1); 
        mem.WriteU32((c.SP + 0x28u), c.S0); 
        c.V1 = mem.ReadU16((c.S2 + 0x2Cu)); 
        { c.V0 = c.V1 < 0x00000008u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 << 2;  }
            goto L801B76C8;
        }
        { c.V0 = c.V1 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x7CF4u)); 
        switch (c.V0)
        {
            case 0x801B6A48u: goto L801B6A48;
            case 0x801B6AB4u: goto L801B6AB4;
            case 0x801B73B0u: goto L801B73B0;
            case 0x801B73E8u: goto L801B73E8;
            case 0x801B74ACu: goto L801B74AC;
            case 0x801B754Cu: goto L801B754C;
            case 0x801B75E8u: goto L801B75E8;
            case 0x801B76C0u: goto L801B76C0;
            case 0x801B6B2Cu: goto L801B6B2C;
            case 0x801B6B38u: goto L801B6B38;
            case 0x801B6BE4u: goto L801B6BE4;
            case 0x801B6C10u: goto L801B6C10;
            case 0x801B6C40u: goto L801B6C40;
            case 0x801B6C80u: goto L801B6C80;
            case 0x801B6DB8u: goto L801B6DB8;
            case 0x801B6E30u: goto L801B6E30;
            case 0x801B6EC4u: goto L801B6EC4;
            case 0x801B6EECu: goto L801B6EEC;
            case 0x801B6F14u: goto L801B6F14;
            case 0x801B6F4Cu: goto L801B6F4C;
            case 0x801B6F94u: goto L801B6F94;
            case 0x801B6AC8u: goto L801B6AC8;
            case 0x801B6FF0u: goto L801B6FF0;
            case 0x801B705Cu: goto L801B705C;
            case 0x801B70BCu: goto L801B70BC;
            case 0x801B7108u: goto L801B7108;
            case 0x801B713Cu: goto L801B713C;
            case 0x801B714Cu: goto L801B714C;
            case 0x801B720Cu: goto L801B720C;
            case 0x801B7248u: goto L801B7248;
            case 0x801B7268u: goto L801B7268;
            case 0x801B76C8u: goto L801B76C8;
            case 0x801B72A4u: goto L801B72A4;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801B6A48: ;
        c.A0 = 0x80190000u; 
        { c.A0 = c.A0 - 0x4CFCu;  }
        c.RA = 0x801B6A58u; 
        SoTN_sel.SetCutsceneScript_sel(c, m);
        { c.V0 = c.V0 & 0x00FFu;  }
        if (c.V0 == 0u) {
            c.A0 = 0x00800000u; 
            goto L801B76C8;
        }
        c.A0 = 0x00800000u; 
        { c.A0 = c.A0 | 0x2000u;  }
        c.V0 = mem.ReadU32((c.S2 + 0x34u)); 
        { c.V1 = 0u | 0x0001u;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C18u), 0u); 
        c.At = 0x801D0000u; 
        mem.WriteU32((c.At + 0x6B00u), 0u); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3CB0u), 0u); 
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x38FCu), c.V1); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s | _t;  }
        mem.WriteU32((c.S2 + 0x34u), c.V0); 
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        c.V1 = 0x801C0000u; 
        c.V1 = mem.ReadU32((c.V1 - 0x3C74u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU32((c.S2 + 0x64u), c.V1); 
        goto L801B76C8;
        L801B6AB4: ;
        c.S4 = 0x801C0000u; 
        { c.S4 = c.S4 - 0x3C96u;  }
        { c.S0 = c.S4 - 0x16u;  }
        { c.S1 = c.S4 - 0xCu;  }
        { c.S3 = c.S4 - 0x12u;  }
        L801B6AC8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU8(c.S4); 
        if (c.V0 == 0u) {
            goto L801B6AEC;
        }
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3CB0u)); 
        if (c.V0 == 0u) {
            goto L801B7690;
        }
        L801B6AEC: ;
        c.V1 = mem.ReadU32(c.S0); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.A1 = mem.ReadU8(c.V1); 
        { c.A0 = c.A1 & 0xFFFFu;  }
        { c.V0 = c.A0 < 0x00000019u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.A0 << 2;  }
            goto L801B72E4;
        }
        { c.V0 = c.A0 << 2;  }
        c.At = 0x801A0000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU32((c.At + 0x7D14u)); 
        switch (c.V0)
        {
            case 0x801B6B2Cu: goto L801B6B2C;
            case 0x801B6B38u: goto L801B6B38;
            case 0x801B6BE4u: goto L801B6BE4;
            case 0x801B6C10u: goto L801B6C10;
            case 0x801B6C40u: goto L801B6C40;
            case 0x801B6C80u: goto L801B6C80;
            case 0x801B6DB8u: goto L801B6DB8;
            case 0x801B6E30u: goto L801B6E30;
            case 0x801B6EC4u: goto L801B6EC4;
            case 0x801B6EECu: goto L801B6EEC;
            case 0x801B6F14u: goto L801B6F14;
            case 0x801B6F4Cu: goto L801B6F4C;
            case 0x801B6F94u: goto L801B6F94;
            case 0x801B6AC8u: goto L801B6AC8;
            case 0x801B6FF0u: goto L801B6FF0;
            case 0x801B705Cu: goto L801B705C;
            case 0x801B70BCu: goto L801B70BC;
            case 0x801B7108u: goto L801B7108;
            case 0x801B713Cu: goto L801B713C;
            case 0x801B714Cu: goto L801B714C;
            case 0x801B720Cu: goto L801B720C;
            case 0x801B7248u: goto L801B7248;
            case 0x801B7268u: goto L801B7268;
            case 0x801B76C8u: goto L801B76C8;
            case 0x801B72A4u: goto L801B72A4;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
        L801B6B2C: ;
        { c.V0 = 0u | 0x0007u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801B76C8;
        L801B6B38: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3CB0u)); 
        if (c.V0 != 0u) {
            goto L801B6AC8;
        }
        c.V0 = mem.ReadU16((c.S1 + 0x8u)); 
        c.V1 = mem.ReadU16((c.S1 + 0x2u)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 != 0u) {
            mem.WriteU16(c.S1, (ushort)c.V1); 
            goto L801B6B70;
        }
        mem.WriteU16(c.S1, (ushort)c.V1); 
        c.V0 = mem.ReadU16((c.S1 - 0x4u)); 
        { c.V0 = c.V0 + 0xCu;  }
        mem.WriteU16((c.S1 - 0x4u), (ushort)c.V0); 
        L801B6B70: ;
        c.V0 = mem.ReadU16((c.S1 + 0x4u)); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S1 + 0x4u), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 5 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801B6B98;
        }
        mem.WriteU16((c.S1 + 0x4u), (ushort)0u); 
        L801B6B98: ;
        c.RA = 0x801B6BA0u; 
        SoTN_sel.CutsceneUnk4_sel(c, m);
        c.V1 = mem.ReadU16((c.S1 + 0x8u)); 
        { c.V0 = c.V1 & 0x0001u;  }
        if (c.V0 != 0u) {
            goto L801B6BCC;
        }
        c.V0 = (uint)(short)mem.ReadU16((c.S1 + 0x4u)); 
        { c.V0 = (int)c.V0 < 4 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = c.V1 | 0x0001u;  }
            goto L801B6AC8;
        }
        { c.V0 = c.V1 | 0x0001u;  }
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.V0); 
        L801B6BCC: ;
        mem.WriteU16((c.S1 + 0x6u), (ushort)0u); 
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        mem.WriteU16((c.S2 + 0x2Eu), (ushort)0u); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801B76C8;
        L801B6BE4: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3CACu)); 
        { c.V1 = c.V0 + 0x1u;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3CACu), c.V1); 
        c.V0 = mem.ReadU8(c.V0); 
        c.At = 0x801C0000u; 
        mem.WriteU8((c.At - 0x3C95u), (byte)c.V0); 
        goto L801B6AC8;
        L801B6C10: ;
        c.V1 = mem.ReadU32((c.S4 - 0x16u)); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32((c.S4 - 0x16u), c.V0); 
        c.V0 = mem.ReadU8(c.V1); 
        c.V1 = 0x801C0000u; 
        c.V1 = mem.ReadU32((c.V1 - 0x3CB0u)); 
        if (c.V1 != 0u) {
            mem.WriteU8(c.S4, (byte)c.V0); 
            goto L801B6AC8;
        }
        mem.WriteU8(c.S4, (byte)c.V0); 
        goto L801B76C8;
        L801B6C40: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3CB0u)); 
        if (c.V0 != 0u) {
            { c.V1 = 0u | 0x0008u;  }
            goto L801B6AC8;
        }
        { c.V1 = 0u | 0x0008u;  }
        c.S0 = 0x801C0000u; 
        c.S0 = mem.ReadU32((c.S0 - 0x3C94u)); 
        c.S1 = 0u; 
        { c.S1 = c.S1 + 0x1u;  }
        L801B6C64: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (int)c.S1 < 5 ? 1u : 0u;  }
        mem.WriteU16((c.S0 + 0x32u), (ushort)c.V1); 
        c.S0 = mem.ReadU32(c.S0); 
        if (c.V0 != 0u) {
            { c.S1 = c.S1 + 0x1u;  }
            goto L801B6C64;
        }
        { c.S1 = c.S1 + 0x1u;  }
        goto L801B76C8;
        L801B6C80: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3CB0u)); 
        if (c.V0 != 0u) {
            goto L801B6F00;
        }
        c.V1 = mem.ReadU32(c.S0); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.S1 = mem.ReadU8(c.V1); 
        { c.V0 = c.V1 + 0x2u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.T2 = mem.ReadU8((c.V1 + 0x1u)); 
        c.S0 = mem.ReadU32((c.S0 + 0x2Cu)); 
        { c.V0 = c.T2 & 0x0001u;  }
        { c.V1 = c.S1 << 1;  }
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A0 = mem.ReadU8((c.At + 0x824u)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.A1 = mem.ReadU8((c.At + 0x828u)); 
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V1; c.At = _s + _t;  }
        c.V1 = mem.ReadU16((c.At + 0x82Cu)); 
        { c.V0 = 0u | 0x0090u;  }
        mem.WriteU16((c.S0 + 0x1Au), (ushort)c.V0); 
        { c.V0 = c.T2 & 0x0080u;  }
        if (c.V0 == 0u) {
            mem.WriteU16((c.S0 + 0xEu), (ushort)c.V1); 
            goto L801B6D04;
        }
        mem.WriteU16((c.S0 + 0xEu), (ushort)c.V1); 
        c.V1 = c.A0; 
        { c.V0 = c.V1 + 0x2Fu;  }
        goto L801B6D0C;
        L801B6D04: ;
        c.V0 = c.A0; 
        { c.V1 = c.V0 + 0x2Fu;  }
        L801B6D0C: ;
        mem.WriteU8((c.S0 + 0x24u), (byte)c.V0); 
        mem.WriteU8((c.S0 + 0xCu), (byte)c.V0); 
        mem.WriteU8((c.S0 + 0x30u), (byte)c.V1); 
        mem.WriteU8((c.S0 + 0x18u), (byte)c.V1); 
        c.V0 = c.A1; 
        { c.V1 = c.V0 + 0x48u;  }
        mem.WriteU8((c.S0 + 0x19u), (byte)c.V0); 
        mem.WriteU8((c.S0 + 0xDu), (byte)c.V0); 
        mem.WriteU8((c.S0 + 0x31u), (byte)c.V1); 
        mem.WriteU8((c.S0 + 0x25u), (byte)c.V1); 
        c.V0 = mem.ReadU16(c.S3); 
        { c.V0 = c.V0 - 0x1Eu;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x20u), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x14u), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x8u), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S3 + 0x4u)); 
        { c.V0 = c.V0 + 0x24u;  }
        mem.WriteU16((c.S0 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x22u), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0xAu), (ushort)c.V0); 
        { c.V0 = c.S1 << 1;  }
        c.At = 0x80180000u; 
        { var _s = c.At; var _t = c.V0; c.At = _s + _t;  }
        c.V0 = mem.ReadU16((c.At + 0x838u)); 
        mem.WriteU16((c.S3 + 0x10u), (ushort)c.V0); 
        c.RA = 0x801B6D84u; 
        SoTN_sel.CutsceneUnk1_sel(c, m);
        c.RA = 0x801B6D8Cu; 
        SoTN_sel.CutsceneUnk4_sel(c, m);
        { c.A0 = c.S1 & 0xFFFFu;  }
        c.A1 = c.S2; 
        { c.V0 = 0u | 0x01FEu;  }
        mem.WriteU16((c.S0 + 0x26u), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x32u), (ushort)0u); 
        c.RA = 0x801B6DA4u; 
        SoTN_sel.DrawCutsceneActorName_sel(c, m);
        { c.V0 = 0u | 0x0006u;  }
        mem.WriteU16((c.S3 + 0xCu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0003u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801B76C8;
        L801B6DB8: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3CB0u)); 
        if (c.V0 != 0u) {
            { c.V1 = 0u | 0x0008u;  }
            goto L801B6AC8;
        }
        { c.V1 = 0u | 0x0008u;  }
        c.S0 = 0x801C0000u; 
        c.S0 = mem.ReadU32((c.S0 - 0x3C94u)); 
        c.S1 = 0u; 
        { c.S1 = c.S1 + 0x1u;  }
        L801B6DDC: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        { c.V0 = (int)c.S1 < 5 ? 1u : 0u;  }
        mem.WriteU16((c.S0 + 0x32u), (ushort)c.V1); 
        c.S0 = mem.ReadU32(c.S0); 
        if (c.V0 != 0u) {
            { c.S1 = c.S1 + 0x1u;  }
            goto L801B6DDC;
        }
        { c.S1 = c.S1 + 0x1u;  }
        c.S0 = 0x801C0000u; 
        { c.S0 = c.S0 - 0x3C78u;  }
        c.A0 = mem.ReadU32(c.S0); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x384Cu)); 
        c.RA = 0x801B6E10u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0xFFFFFFFFu; 
        mem.WriteU32(c.S0, c.V0); 
        { c.V0 = 0u | 0x0006u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C9Cu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0004u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801B76C8;
        L801B6E30: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3CB0u)); 
        if (c.V0 != 0u) {
            goto L801B6F00;
        }
        c.V1 = mem.ReadU32((c.S3 - 0x4u)); 
        c.S0 = mem.ReadU32((c.S3 + 0x28u)); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32((c.S3 - 0x4u), c.V0); 
        c.A0 = mem.ReadU8(c.V1); 
        { c.V0 = c.V1 + 0x2u;  }
        mem.WriteU32((c.S3 - 0x4u), c.V0); 
        mem.WriteU16(c.S3, (ushort)c.A0); 
        c.V0 = mem.ReadU8((c.V1 + 0x1u)); 
        mem.WriteU16((c.S3 + 0x4u), (ushort)c.V0); 
        c.S0 = mem.ReadU32(c.S0); 
        mem.WriteU16((c.S0 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x22u), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x16u), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0xAu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0011u;  }
        mem.WriteU16((c.S0 + 0x32u), (ushort)c.V0); 
        c.V0 = mem.ReadU16((c.S3 + 0x4u)); 
        c.S0 = mem.ReadU32(c.S0); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU16((c.S0 + 0xAu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0051u;  }
        mem.WriteU16((c.S0 + 0x32u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0018u;  }
        mem.WriteU8((c.S0 + 0xCu), (byte)0u); 
        mem.WriteU16((c.S3 + 0xCu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0005u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.S2 + 0x2Eu), (ushort)0u); 
        goto L801B76C8;
        L801B6EC4: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3CB0u)); 
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0018u;  }
            goto L801B6AC8;
        }
        { c.V0 = 0u | 0x0018u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C9Cu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0006u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801B76C8;
        L801B6EEC: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3CB0u)); 
        if (c.V0 == 0u) {
            goto L801B720C;
        }
        L801B6F00: ;
        c.V0 = mem.ReadU32(c.S0); 
        { c.V0 = c.V0 + 0x2u;  }
        mem.WriteU32(c.S0, c.V0); 
        goto L801B6AC8;
        L801B6F14: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3CB0u)); 
        if (c.V0 != 0u) {
            goto L801B6AC8;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3808u)); 
        c.RA = 0x801B6F3Cu;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 == 0u) {
            goto L801B6F80;
        }
        goto L801B6AC8;
        L801B6F4C: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3CB0u)); 
        if (c.V0 != 0u) {
            goto L801B6AC8;
        }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3808u)); 
        c.RA = 0x801B6F74u;
        Dispatcher.Call(c, m, c.V0);
        { c.V1 = 0u | 0x0001u;  }
        if (c.V0 != c.V1) {
            goto L801B6AC8;
        }
        L801B6F80: ;
        c.V0 = mem.ReadU32(c.S0); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU32(c.S0, c.V0); 
        goto L801B76C8;
        L801B6F94: ;
        c.V1 = mem.ReadU32(c.S0); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.T1 = mem.ReadU8(c.V1); 
        { c.V0 = c.V1 + 0x2u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.A1 = mem.ReadU8((c.V1 + 0x1u)); 
        { c.V0 = c.V1 + 0x3u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.A0 = mem.ReadU8((c.V1 + 0x2u)); 
        { c.V0 = c.V1 + 0x4u;  }
        mem.WriteU32(c.S0, c.V0); 
        { c.T1 = c.T1 << 4;  }
        { var _s = c.T1; var _t = c.A1; c.T1 = _s | _t;  }
        { c.T1 = c.T1 << 4;  }
        { var _s = c.T1; var _t = c.A0; c.T1 = _s | _t;  }
        c.A0 = mem.ReadU8((c.V1 + 0x3u)); 
        { c.T1 = c.T1 << 4;  }
        { var _s = c.T1; var _t = c.A0; c.A0 = _s | _t;  }
        c.RA = 0x801B6FE8u; 
        SoTN_sel.SetCutsceneEnd_sel(c, m);
        goto L801B6AC8;
        L801B6FF0: ;
        c.V1 = mem.ReadU32(c.S0); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.T1 = mem.ReadU8(c.V1); 
        { c.V0 = c.V1 + 0x2u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.A0 = mem.ReadU8((c.V1 + 0x1u)); 
        { c.V0 = c.V1 + 0x3u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.V0 = mem.ReadU8((c.V1 + 0x2u)); 
        { c.A1 = c.V1 + 0x4u;  }
        mem.WriteU32(c.S0, c.A1); 
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
        goto L801B7060;
        L801B705C: ;
        c.V1 = mem.ReadU32(c.S0); 
        L801B7060: ;
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.T1 = mem.ReadU8(c.V1); 
        { c.V0 = c.V1 + 0x2u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.A0 = mem.ReadU8((c.V1 + 0x1u)); 
        { c.V0 = c.V1 + 0x3u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.V0 = mem.ReadU8((c.V1 + 0x2u)); 
        c.V1 = 0x801C0000u; 
        c.V1 = mem.ReadU32((c.V1 - 0x3CACu)); 
        { c.T1 = c.T1 << 4;  }
        { var _s = c.T1; var _t = c.A0; c.T1 = _s | _t;  }
        { c.T1 = c.T1 << 4;  }
        { var _s = c.T1; var _t = c.V0; c.T1 = _s | _t;  }
        c.V0 = mem.ReadU8(c.V1); 
        { c.T1 = c.T1 << 4;  }
        { var _s = c.T1; var _t = c.V0; c.T1 = _s | _t;  }
        c.V0 = 0x00100000u; 
        { var _s = c.T1; var _t = c.V0; c.V0 = _s + _t;  }
        mem.WriteU32(c.S0, c.V0); 
        goto L801B6AC8;
        L801B70BC: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3CACu)); 
        c.A0 = 0x801C0000u; 
        c.A0 = mem.ReadU32((c.A0 - 0x3C18u)); 
        c.V1 = mem.ReadU8(c.V0); 
        { var _s = c.V1; var _t = c.A0; c.V0 = _t >> (int)(_s & 31u);  }
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801B76A4;
        }
        { c.V0 = 0u | 0x0001u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = 0u; var _t = c.V0; c.V0 = ~(_s | _t);  }
        c.V1 = mem.ReadU32(c.S0); 
        { var _s = c.V0; var _t = c.A0; c.V0 = _s & _t;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C18u), c.V0); 
        { c.V1 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S0, c.V1); 
        goto L801B6AC8;
        L801B7108: ;
        c.V1 = mem.ReadU32(c.S0); 
        c.A0 = 0x801C0000u; 
        c.A0 = mem.ReadU32((c.A0 - 0x3C18u)); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.V1 = mem.ReadU8(c.V1); 
        { c.V0 = 0u | 0x0001u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _t << (int)(_s & 31u);  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s | _t;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C18u), c.V0); 
        goto L801B6AC8;
        L801B713C: ;
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C70u), (ushort)0u); 
        goto L801B6AC8;
        L801B714C: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3CB0u)); 
        if (c.V0 == 0u) {
            { c.A1 = 0u | 0x0001u;  }
            goto L801B7174;
        }
        { c.A1 = 0u | 0x0001u;  }
        c.V0 = mem.ReadU32(c.S0); 
        { c.V0 = c.V0 + 0x5u;  }
        mem.WriteU32(c.S0, c.V0); 
        goto L801B6AC8;
        L801B7174: ;
        c.V1 = mem.ReadU32(c.S0); 
        c.A2 = 0u; 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.T1 = mem.ReadU8(c.V1); 
        { c.V0 = c.V1 + 0x2u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.T0 = mem.ReadU8((c.V1 + 0x1u)); 
        { c.V0 = c.V1 + 0x3u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.A3 = mem.ReadU8((c.V1 + 0x2u)); 
        { c.V0 = c.V1 + 0x4u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.A0 = mem.ReadU8((c.V1 + 0x3u)); 
        { c.V0 = c.V1 + 0x5u;  }
        mem.WriteU32(c.S0, c.V0); 
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
        c.A3 = mem.ReadU16((c.At + 0x834u)); 
        { c.V0 = 0u | 0x0100u;  }
        mem.WriteU32((c.SP + 0x10u), c.V0); 
        { c.V0 = 0u | 0x0030u;  }
        mem.WriteU32((c.SP + 0x14u), c.V0); 
        { c.V0 = 0u | 0x0048u;  }
        mem.WriteU32((c.SP + 0x18u), c.V0); 
        c.RA = 0x801B7204u; 
        SoTN_main.LoadTPage(c, m);
        goto L801B6AC8;
        L801B720C: ;
        c.V1 = mem.ReadU32(c.S0); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.A1 = mem.ReadU8(c.V1); 
        { c.V0 = c.V1 + 0x2u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.A0 = mem.ReadU8((c.V1 + 0x1u)); 
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3824u)); 
        { c.A1 = c.A1 << 4;  }
        { var _s = c.A0; var _t = c.A1; c.A0 = _s | _t;  }
        c.RA = 0x801B7240u;
        Dispatcher.Call(c, m, c.V0);
        goto L801B6AC8;
        L801B7248: ;
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C18u), 0u); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3CB0u), 0u); 
        c.At = 0x801D0000u; 
        mem.WriteU32((c.At + 0x6B00u), 0u); 
        goto L801B6AC8;
        L801B7268: ;
        c.V1 = mem.ReadU32(c.S0); 
        { c.V0 = c.V1 + 0x1u;  }
        mem.WriteU32(c.S0, c.V0); 
        c.V1 = mem.ReadU8(c.V1); 
        { c.V0 = 0u | 0x0001u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _t << (int)(_s & 31u);  }
        c.V1 = 0x801C0000u; 
        c.V1 = mem.ReadU32((c.V1 - 0x3C18u)); 
        { var _s = 0u; var _t = c.V0; c.V0 = ~(_s | _t);  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s & _t;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C18u), c.V0); 
        goto L801B6AC8;
        L801B72A4: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3CACu)); 
        c.V1 = mem.ReadU8(c.V0); 
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3C18u)); 
        { var _s = c.V1; var _t = c.V0; c.V0 = _t >> (int)(_s & 31u);  }
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            goto L801B76A4;
        }
        c.V0 = mem.ReadU32(c.S0); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU32(c.S0, c.V0); 
        goto L801B6AC8;
        L801B72E4: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3CB0u)); 
        if (c.V0 == 0u) {
            { c.V0 = c.V1 + 0x2u;  }
            goto L801B7300;
        }
        { c.V0 = c.V1 + 0x2u;  }
        mem.WriteU32(c.S0, c.V0); 
        goto L801B6AC8;
        L801B7300: ;
        c.V0 = mem.ReadU8((c.S0 + 0x17u)); 
        mem.WriteU8((c.S0 + 0x16u), (byte)c.V0); 
        { c.V0 = 0u | 0x0020u;  }
        if (c.A0 != c.V0) {
            c.A0 = c.S5; 
            goto L801B7334;
        }
        c.A0 = c.S5; 
        c.V1 = 0x801C0000u; 
        { c.V1 = c.V1 - 0x3CA2u;  }
        c.V0 = mem.ReadU16(c.V1); 
        { c.V0 = c.V0 + 0x2u;  }
        mem.WriteU16(c.V1, (ushort)c.V0); 
        goto L801B76C8;
        L801B7334: ;
        { c.V0 = c.A1 & 0x000Fu;  }
        { c.V0 = c.V0 << 1;  }
        { c.V0 = c.V0 + 0x380u;  }
        mem.WriteU16(c.A0, (ushort)c.V0); 
        { c.V0 = c.A1 & 0x00F0u;  }
        { c.V0 = c.V0 >> 1;  }
        { c.V0 = c.V0 + 0xF0u;  }
        mem.WriteU16((c.A0 + 0x2u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0002u;  }
        mem.WriteU16((c.A0 + 0x4u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.A0 + 0x6u), (ushort)c.V0); 
        c.V0 = 0x801C0000u; 
        c.V0 = (uint)(short)mem.ReadU16((c.V0 - 0x3C9Eu)); 
        c.A1 = 0x801C0000u; 
        c.A1 = (uint)(short)mem.ReadU16((c.A1 - 0x3CA2u)); 
        { c.A2 = c.V0 << 1;  }
        { var _s = c.A2; var _t = c.V0; c.A2 = _s + _t;  }
        { c.A2 = c.A2 << 2;  }
        { c.A2 = c.A2 + 0x180u;  }
        { c.A2 = c.A2 << 16;  }
        { c.A2 = (uint)((int)c.A2 >> 16);  }
        c.RA = 0x801B7390u; 
        if (!DialogueGlyphs.Draw(c, mem)) SoTN_main.MoveImage(c, m);
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x3CA2u)); 
        { c.V0 = c.V0 + 0x2u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3CA2u), (ushort)c.V0); 
        goto L801B76C8;
        L801B73B0: ;
        { c.A0 = 0u | 0x0002u;  }
        c.A1 = c.S2; 
        c.RA = 0x801B73BCu; 
        SoTN_sel.ScaleCutsceneAvatar_sel(c, m);
        c.V0 = 0x801C0000u; 
        c.V0 = (uint)(short)mem.ReadU16((c.V0 - 0x3C9Cu)); 
        { c.V0 = (int)c.V0 < 6 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801B76C8;
        }
        c.V0 = mem.ReadU16((c.S2 + 0x2Cu)); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801B76C8;
        L801B73E8: ;
        c.S0 = 0x801C0000u; 
        c.S0 = mem.ReadU32((c.S0 - 0x3C80u)); 
        c.V0 = mem.ReadU16((c.S0 + 0x20u)); 
        c.A0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        c.V1 = mem.ReadU16((c.S0 + 0x16u)); 
        c.A1 = mem.ReadU16((c.S0 + 0x2Eu)); 
        { c.V0 = c.V0 - 0x4u;  }
        { c.A0 = c.A0 + 0x4u;  }
        { c.V1 = c.V1 - 0x6u;  }
        { c.A1 = c.A1 + 0x6u;  }
        mem.WriteU16((c.S0 + 0x20u), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x8u), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.A0); 
        mem.WriteU16((c.S0 + 0x14u), (ushort)c.A0); 
        mem.WriteU16((c.S0 + 0x16u), (ushort)c.V1); 
        mem.WriteU16((c.S0 + 0xAu), (ushort)c.V1); 
        mem.WriteU16((c.S0 + 0x2Eu), (ushort)c.A1); 
        mem.WriteU16((c.S0 + 0x22u), (ushort)c.A1); 
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x3C9Cu)); 
        { c.V0 = c.V0 - 0x1u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C9Cu), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801B76C8;
        }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        c.V1 = 0x801C0000u; 
        c.V1 = mem.ReadU32((c.V1 - 0x3C78u)); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.S0 = _s + _t;  }
        if (c.S0 == 0u) {
            goto L801B76C8;
        }
        mem.WriteU16((c.S0 + 0x32u), (ushort)0u); 
        L801B7494: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.S0 = mem.ReadU32(c.S0); 
        if (c.S0 == 0u) {
            goto L801B76C8;
        }
        mem.WriteU16((c.S0 + 0x32u), (ushort)0u); 
        goto L801B7494;
        L801B74AC: ;
        c.S0 = 0x801C0000u; 
        c.S0 = mem.ReadU32((c.S0 - 0x3C80u)); 
        c.V0 = mem.ReadU16((c.S0 + 0x20u)); 
        c.A0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        c.V1 = mem.ReadU16((c.S0 + 0x16u)); 
        c.A1 = mem.ReadU16((c.S0 + 0x2Eu)); 
        { c.V0 = c.V0 + 0x4u;  }
        { c.A0 = c.A0 - 0x4u;  }
        { c.V1 = c.V1 + 0x6u;  }
        { c.A1 = c.A1 - 0x6u;  }
        mem.WriteU16((c.S0 + 0x20u), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x8u), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.A0); 
        mem.WriteU16((c.S0 + 0x14u), (ushort)c.A0); 
        mem.WriteU16((c.S0 + 0x16u), (ushort)c.V1); 
        mem.WriteU16((c.S0 + 0xAu), (ushort)c.V1); 
        mem.WriteU16((c.S0 + 0x2Eu), (ushort)c.A1); 
        mem.WriteU16((c.S0 + 0x22u), (ushort)c.A1); 
        c.V1 = 0x801C0000u; 
        c.V1 = (uint)(short)mem.ReadU16((c.V1 - 0x3CA8u)); 
        c.V0 = (uint)(short)mem.ReadU16((c.S0 + 0x14u)); 
        c.A0 = c.V1; 
        { c.V1 = c.V1 - 0x2u;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = (int)_s < (int)_t ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = c.A0 - 0x3u;  }
            goto L801B7520;
        }
        { c.V0 = c.A0 - 0x3u;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x14u), (ushort)c.V0); 
        L801B7520: ;
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x3C9Cu)); 
        { c.V0 = c.V0 - 0x1u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C9Cu), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801B76C8;
        }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        goto L801B76C8;
        L801B754C: ;
        c.S0 = 0x801C0000u; 
        c.S0 = mem.ReadU32((c.S0 - 0x3C80u)); 
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801D0000u; 
        mem.WriteU32((c.At + 0x6B00u), c.V0); 
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x3C9Cu)); 
        c.S0 = mem.ReadU32(c.S0); 
        { c.V0 = c.V0 - 0x1u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C9Cu), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        { c.V1 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V1 < 12 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801B75C4;
        }
        c.S0 = mem.ReadU32(c.S0); 
        c.V1 = mem.ReadU8((c.S0 + 0xCu)); 
        { c.V0 = c.V1 + 0x14u;  }
        mem.WriteU8((c.S0 + 0xCu), (byte)c.V0); 
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x3C9Cu)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 + 0x15u;  }
            goto L801B76C8;
        }
        { c.V0 = c.V1 + 0x15u;  }
        mem.WriteU8((c.S0 + 0xCu), (byte)c.V0); 
        goto L801B76C8;
        L801B75C4: ;
        if (c.V1 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801B75D0;
        }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        L801B75D0: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Eu)); 
        { c.V0 = c.V0 + 0x6u;  }
        mem.WriteU16((c.S0 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x22u), (ushort)c.V0); 
        goto L801B76C8;
        L801B75E8: ;
        c.S0 = 0x801C0000u; 
        c.S0 = mem.ReadU32((c.S0 - 0x3C80u)); 
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x3C9Cu)); 
        c.S0 = mem.ReadU32(c.S0); 
        { c.V0 = c.V0 - 0x1u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C9Cu), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        { c.V1 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V1 < 12 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            goto L801B7654;
        }
        c.S0 = mem.ReadU32(c.S0); 
        c.V1 = mem.ReadU8((c.S0 + 0xCu)); 
        { c.V0 = c.V1 + 0xECu;  }
        mem.WriteU8((c.S0 + 0xCu), (byte)c.V0); 
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x3C9Cu)); 
        { c.V0 = c.V0 & 0x0001u;  }
        if (c.V0 == 0u) {
            { c.V0 = c.V1 - 0x15u;  }
            goto L801B76C8;
        }
        { c.V0 = c.V1 - 0x15u;  }
        mem.WriteU8((c.S0 + 0xCu), (byte)c.V0); 
        goto L801B76C8;
        L801B7654: ;
        if (c.V1 != 0u) {
            { c.V0 = 0u | 0x0001u;  }
            goto L801B766C;
        }
        { c.V0 = 0u | 0x0001u;  }
        mem.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.S0 + 0x32u), (ushort)c.V0); 
        goto L801B7680;
        L801B766C: ;
        c.V0 = mem.ReadU16((c.S0 + 0x2Eu)); 
        { c.V0 = c.V0 - 0x6u;  }
        mem.WriteU16((c.S0 + 0x2Eu), (ushort)c.V0); 
        mem.WriteU16((c.S0 + 0x22u), (ushort)c.V0); 
        L801B7680: ;
        c.S0 = mem.ReadU32(c.S0); 
        { c.V0 = 0u | 0x0008u;  }
        mem.WriteU16((c.S0 + 0x32u), (ushort)c.V0); 
        goto L801B76C8;
        L801B7690: ;
        c.V0 = mem.ReadU8(c.S4); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU8(c.S4, (byte)c.V0); 
        goto L801B76C8;
        L801B76A4: ;
        c.V1 = 0x801C0000u; 
        { c.V1 = c.V1 - 0x3CACu;  }
        c.V0 = mem.ReadU32(c.V1); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU32(c.V1, c.V0); 
        goto L801B76C8;
        L801B76C0: ;
        c.At = 0x80040000u; 
        mem.WriteU32((c.At - 0x38FCu), 0u); 
        L801B76C8: ;
        c.RA = mem.ReadU32((c.SP + 0x40u)); 
        c.S5 = mem.ReadU32((c.SP + 0x3Cu)); 
        c.S4 = mem.ReadU32((c.SP + 0x38u)); 
        c.S3 = mem.ReadU32((c.SP + 0x34u)); 
        c.S2 = mem.ReadU32((c.SP + 0x30u)); 
        c.S1 = mem.ReadU32((c.SP + 0x2Cu)); 
        c.S0 = mem.ReadU32((c.SP + 0x28u)); 
        { c.SP = c.SP + 0x48u;  }
        return;
        }
    public static bool func_801B76F0(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("sel", m, true)) return true;
        func_801B76F0_Body(c, m);
        return false;
    }
    private static void func_801B76F0_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "sel", 0x801BC354u, false);
        { c.SP = c.SP - 0x18u;  }
        mem.WriteU32((c.SP + 0x10u), c.S0); 
        c.S0 = c.A0; 
        { c.A0 = 0u | 0x0006u;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3848u)); 
        mem.WriteU32((c.SP + 0x14u), c.RA); 
        { c.A1 = 0u | 0x0020u;  }
        c.RA = 0x801B7714u;
        Dispatcher.Call(c, m, c.V0);
        c.A0 = 0x801C0000u; 
        { c.A0 = c.A0 - 0x3C90u;  }
        { c.V0 = c.V0 << 16;  }
        { c.V1 = (uint)((int)c.V0 >> 16);  }
        c.V0 = 0xFFFFFFFFu; 
        if (c.V1 == c.V0) {
            mem.WriteU32(c.A0, c.V1); 
            goto L801B7850;
        }
        mem.WriteU32(c.A0, c.V1); 
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        c.V1 = 0x80080000u; 
        { c.V1 = c.V1 + 0x6FECu;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        c.A1 = c.V0; 
        c.A2 = 0u; 
        { c.T1 = 0u | 0x0009u;  }
        { c.T0 = 0u | 0x00F8u;  }
        { c.A3 = 0u | 0x0008u;  }
        { c.V0 = 0u | 0x0200u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3CA2u), (ushort)c.V0); 
        { c.V0 = 0u | 0x020Bu;  }
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3CACu), c.S0); 
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3CA4u), (ushort)c.V0); 
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3CA0u), (ushort)0u); 
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C9Eu), (ushort)0u); 
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C9Cu), (ushort)0u); 
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C9Au), (ushort)0u); 
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C98u), (ushort)0u); 
        c.At = 0x801C0000u; 
        mem.WriteU32((c.At - 0x3C94u), c.A1); 
        { c.V0 = c.A2 & 0x0001u;  }
        L801B77B8: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        if (c.V0 == 0u) {
            { c.V0 = 0u | 0x0018u;  }
            goto L801B77CC;
        }
        { c.V0 = 0u | 0x0018u;  }
        mem.WriteU16((c.A1 + 0x1Au), (ushort)c.T1); 
        mem.WriteU16((c.A1 + 0x8u), (ushort)c.T0); 
        goto L801B77D4;
        L801B77CC: ;
        mem.WriteU16((c.A1 + 0x1Au), (ushort)c.A3); 
        mem.WriteU16((c.A1 + 0x8u), (ushort)c.V0); 
        L801B77D4: ;
        { c.V1 = c.A2 << 16;  }
        { c.A0 = c.A2 + 0x1u;  }
        c.A2 = c.A0; 
        { c.V1 = (uint)((int)c.V1 >> 17);  }
        { c.V0 = c.V1 << 4;  }
        mem.WriteU8((c.A1 + 0xDu), (byte)c.V0); 
        { c.V0 = 0u | 0x00F0u;  }
        mem.WriteU8((c.A1 + 0x18u), (byte)c.V0); 
        { c.V0 = 0u | 0x0010u;  }
        mem.WriteU8((c.A1 + 0x19u), (byte)c.V0); 
        { c.V0 = c.V1 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 2;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s - _t;  }
        { c.V0 = c.V0 + 0xF0u;  }
        mem.WriteU16((c.A1 + 0xAu), (ushort)c.V0); 
        { c.V0 = 0u | 0x01A1u;  }
        mem.WriteU16((c.A1 + 0xEu), (ushort)c.V0); 
        { c.V0 = 0u | 0x0003u;  }
        { c.A0 = c.A0 << 16;  }
        { c.A0 = (uint)((int)c.A0 >> 16);  }
        { c.A0 = (int)c.A0 < 32 ? 1u : 0u;  }
        mem.WriteU8((c.A1 + 0xCu), (byte)0u); 
        mem.WriteU16((c.A1 + 0x14u), (ushort)0u); 
        mem.WriteU16((c.A1 + 0x26u), (ushort)c.V0); 
        mem.WriteU16((c.A1 + 0x32u), (ushort)0u); 
        c.A1 = mem.ReadU32(c.A1); 
        if (c.A0 != 0u) {
            { c.V0 = c.A2 & 0x0001u;  }
            goto L801B77B8;
        }
        { c.V0 = c.A2 & 0x0001u;  }
        { c.V0 = 0u | 0x0001u;  }
        goto L801B7858;
        L801B7850: ;
        mem.WriteU32(c.A0, 0u); 
        c.V0 = 0u; 
        L801B7858: ;
        c.RA = mem.ReadU32((c.SP + 0x14u)); 
        c.S0 = mem.ReadU32((c.SP + 0x10u)); 
        { c.SP = c.SP + 0x18u;  }
        return;
        }
    public static bool func_801B786C(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("sel", m, false)) return true;
        func_801B786C_Body(c, m);
        return false;
    }
    private static void func_801B786C_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "sel", 0x801BC354u, false);
        { c.SP = c.SP - 0x20u;  }
        { c.V0 = 0u | 0x0200u;  }
        { c.A0 = c.A0 << 16;  }
        { c.A0 = (uint)((int)c.A0 >> 12);  }
        mem.WriteU16((c.SP + 0x10u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0080u;  }
        mem.WriteU16((c.SP + 0x14u), (ushort)c.V0); 
        { c.V0 = 0u | 0x0010u;  }
        mem.WriteU16((c.SP + 0x12u), (ushort)c.A0); 
        { c.A0 = c.SP + 0x10u;  }
        c.A1 = 0u; 
        c.A2 = 0u; 
        c.A3 = 0u; 
        mem.WriteU32((c.SP + 0x18u), c.RA); 
        mem.WriteU16((c.SP + 0x16u), (ushort)c.V0); 
        c.RA = 0x801B78ACu; 
        DialogueGlyphs.Clear(c, mem);
        c.RA = mem.ReadU32((c.SP + 0x18u)); 
        { c.SP = c.SP + 0x20u;  }
        return;
        }
    public static bool func_801B79D4(CpuContext c, IMemory m)
    {
        if (!ScriptMemory.Enabled("sel", m, true)) return true;
        func_801B79D4_Body(c, m);
        return false;
    }
    private static void func_801B79D4_Body(CpuContext c, IMemory m)
    {
        var mem = new ScriptMemory(m, "sel", 0x801BC354u, false);
        { c.SP = c.SP - 0x58u;  }
        mem.WriteU32((c.SP + 0x40u), c.S0); 
        c.S0 = c.A0; 
        mem.WriteU32((c.SP + 0x54u), c.RA); 
        mem.WriteU32((c.SP + 0x50u), c.S4); 
        mem.WriteU32((c.SP + 0x4Cu), c.S3); 
        mem.WriteU32((c.SP + 0x48u), c.S2); 
        mem.WriteU32((c.SP + 0x44u), c.S1); 
        c.V1 = mem.ReadU16((c.S0 + 0x2Cu)); 
        { c.V0 = 0u | 0x0001u;  }
        if (c.V1 == c.V0) {
            { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
            goto L801B7BA8;
        }
        { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801B7A1C;
        }
        if (c.V1 == 0u) {
            goto L801B7A38;
        }
        goto L801B7FF0;
        L801B7A1C: ;
        { c.V0 = 0u | 0x0002u;  }
        if (c.V1 == c.V0) {
            { c.V0 = 0u | 0x0007u;  }
            goto L801B7FF0;
        }
        { c.V0 = 0u | 0x0007u;  }
        if (c.V1 == c.V0) {
            goto L801B7FAC;
        }
        goto L801B7FF0;
        L801B7A38: ;
        c.A0 = 0x80190000u; 
        { c.A0 = c.A0 - 0x43ACu;  }
        c.RA = 0x801B7A48u; 
        SoTN_sel.func_801B76F0(c, m);
        { c.V0 = c.V0 & 0x00FFu;  }
        if (c.V0 == 0u) {
            c.A0 = 0u; 
            goto L801B7FF0;
        }
        c.A0 = 0u; 
        c.S2 = 0u; 
        c.A1 = 0x801C0000u; 
        { c.A1 = c.A1 - 0x3C90u;  }
        { c.S1 = c.A1 - 0x1Cu;  }
        c.S4 = 0x801C0000u; 
        { c.S4 = c.S4 - 0x4F08u;  }
        { c.S3 = c.A1 - 0x10u;  }
        c.V0 = mem.ReadU32((c.S0 + 0x34u)); 
        c.V1 = 0x00800000u; 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s | _t;  }
        mem.WriteU32((c.S0 + 0x34u), c.V0); 
        c.V0 = mem.ReadU16((c.S0 + 0x2Cu)); 
        c.V1 = mem.ReadU32(c.A1); 
        { c.V0 = c.V0 + 0x1u;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0); 
        mem.WriteU32((c.S0 + 0x64u), c.V1); 
        c.RA = 0x801B7A98u; 
        SoTN_sel.func_801B786C(c, m);
        L801B7A98: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A0 = mem.ReadU32(c.S1); 
        { c.V0 = c.A0 + 0x1u;  }
        mem.WriteU32(c.S1, c.V0); 
        c.A2 = mem.ReadU8(c.A0); 
        { c.V0 = 0u | 0x0001u;  }
        { c.V1 = c.A2 & 0xFFFFu;  }
        if (c.V1 == c.V0) {
            { c.V0 = c.A0 + 0x2u;  }
            goto L801B7FD0;
        }
        { c.V0 = c.A0 + 0x2u;  }
        c.A0 = c.A2; 
        c.RA = 0x801B7AC4u; 
        Dispatcher.Call(c, m, 0x801B78BCu);
        c.A0 = c.V0; 
        if (c.A0 == 0u) {
            { c.V1 = c.S2 & 0xFFFFu;  }
            goto L801B7B78;
        }
        { c.V1 = c.S2 & 0xFFFFu;  }
        { c.V0 = c.V1 << 1;  }
        { var _s = c.V0; var _t = c.V1; c.V0 = _s + _t;  }
        { c.V0 = c.V0 << 5;  }
        { var _s = c.V0; var _t = c.S4; c.V1 = _s + _t;  }
        c.A1 = 0u; 
        L801B7AE4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU16(c.A0); 
        { c.A0 = c.A0 + 0x2u;  }
        { c.A1 = c.A1 + 0x1u;  }
        mem.WriteU16(c.V1, (ushort)c.V0); 
        { c.V0 = (int)c.A1 < 48 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V1 = c.V1 + 0x2u;  }
            goto L801B7AE4;
        }
        { c.V1 = c.V1 + 0x2u;  }
        { c.V0 = c.S2 & 0xFFFFu;  }
        { c.A0 = c.V0 << 1;  }
        { var _s = c.A0; var _t = c.V0; c.A0 = _s + _t;  }
        { c.A0 = c.A0 << 5;  }
        { var _s = c.A0; var _t = c.S4; c.A0 = _s + _t;  }
        c.A1 = 0u; 
        c.A2 = 0u; 
        { c.V0 = 0u | 0x000Cu;  }
        mem.WriteU32((c.SP + 0x14u), c.V0); 
        { c.V0 = 0u | 0x0010u;  }
        mem.WriteU32((c.SP + 0x18u), c.V0); 
        c.V0 = (uint)(short)mem.ReadU16(c.S3); 
        c.A3 = (uint)(short)mem.ReadU16((c.S3 - 0x4u)); 
        { c.V0 = c.V0 << 4;  }
        { c.V0 = c.V0 & 0xFFFFu;  }
        mem.WriteU32((c.SP + 0x10u), c.V0); 
        c.RA = 0x801B7B44u; 
        SoTN_main.LoadTPage(c, m);
        c.A0 = mem.ReadU16((c.S3 - 0x4u)); 
        c.V0 = mem.ReadU16((c.S3 - 0x2u)); 
        { c.V1 = c.A0 + 0x3u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _s - _t;  }
        { c.V0 = c.V0 - 0x38u;  }
        { c.V0 = c.V0 & 0xFFFFu;  }
        { c.V0 = c.V0 < 0x00000008u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            mem.WriteU16((c.S3 - 0x4u), (ushort)c.V1); 
            goto L801B7B70;
        }
        mem.WriteU16((c.S3 - 0x4u), (ushort)c.V1); 
        { c.V0 = c.A0 + 0xBu;  }
        mem.WriteU16((c.S3 - 0x4u), (ushort)c.V0); 
        L801B7B70: ;
        { c.S2 = c.S2 + 0x1u;  }
        goto L801B7A98;
        L801B7B78: ;
        c.A0 = mem.ReadU16((c.S1 + 0x8u)); 
        c.V0 = mem.ReadU16((c.S1 + 0xAu)); 
        { c.V1 = c.A0 + 0x2u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _s - _t;  }
        { c.V0 = c.V0 - 0x38u;  }
        { c.V0 = c.V0 & 0xFFFFu;  }
        { c.V0 = c.V0 < 0x00000008u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            mem.WriteU16((c.S1 + 0x8u), (ushort)c.V1); 
            goto L801B7A98;
        }
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.V1); 
        { c.V0 = c.A0 + 0xAu;  }
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.V0); 
        goto L801B7A98;
        L801B7BA8: ;
        c.A3 = 0x801C0000u; 
        { c.A3 = c.A3 - 0x3C9Eu;  }
        c.V0 = mem.ReadU16(c.A3); 
        if (c.V0 == 0u) {
            goto L801B7FF0;
        }
        c.A0 = mem.ReadU32((c.A3 - 0xEu)); 
        { c.V0 = c.A0 + 0x1u;  }
        mem.WriteU32((c.A3 - 0xEu), c.V0); 
        c.A2 = mem.ReadU8(c.A0); 
        { c.V0 = 0u | 0x0001u;  }
        { c.V1 = c.A2 & 0xFFFFu;  }
        if (c.V1 == c.V0) {
            { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
            goto L801B7C2C;
        }
        { c.V0 = (int)c.V1 < 2 ? 1u : 0u;  }
        if (c.V0 == 0u) {
            goto L801B7BFC;
        }
        if (c.V1 == 0u) {
            c.V0 = 0u; 
            goto L801B7C18;
        }
        c.V0 = 0u; 
        { c.V0 = c.A2 & 0x0080u;  }
        goto L801B7E9C;
        L801B7BFC: ;
        { c.V0 = 0u | 0x0002u;  }
        if (c.V1 == c.V0) {
            { c.V0 = 0u | 0x0003u;  }
            goto L801B7C88;
        }
        { c.V0 = 0u | 0x0003u;  }
        if (c.V1 == c.V0) {
            { c.V0 = c.A0 + 0x2u;  }
            goto L801B7D8C;
        }
        { c.V0 = c.A0 + 0x2u;  }
        { c.V0 = c.A2 & 0x0080u;  }
        goto L801B7E9C;
        L801B7C18: ;
        { c.V1 = 0u | 0x0007u;  }
        mem.WriteU16((c.S0 + 0x2Cu), (ushort)c.V1); 
        { c.V1 = 0u | 0x0400u;  }
        mem.WriteU16((c.A3 + 0x4u), (ushort)c.V1); 
        goto L801B80CC;
        L801B7C2C: ;
        { c.V0 = c.A0 + 0x2u;  }
        mem.WriteU32((c.A3 - 0xEu), c.V0); 
        c.V1 = mem.ReadU8((c.A0 + 0x1u)); 
        c.A0 = 0x801C0000u; 
        c.A0 = mem.ReadU16((c.A0 - 0x3CA2u)); 
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x3CA0u)); 
        { var _s = c.V1; var _t = c.A0; c.V1 = _s + _t;  }
        { c.V0 = c.V0 + 0x1u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3CA0u), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 16 ? 1u : 0u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3CA4u), (ushort)c.V1); 
        if (c.V0 != 0u) {
            goto L801B7C7C;
        }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3CA0u), (ushort)0u); 
        L801B7C7C: ;
        mem.WriteU16(c.A3, (ushort)0u); 
        c.V0 = 0u; 
        goto L801B80CC;
        L801B7C88: ;
        { c.V0 = c.A0 + 0x2u;  }
        mem.WriteU32((c.A3 - 0xEu), c.V0); 
        c.V1 = mem.ReadU8((c.A0 + 0x1u)); 
        c.A0 = 0x801C0000u; 
        c.A0 = mem.ReadU16((c.A0 - 0x3CA2u)); 
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x3CA0u)); 
        { var _s = c.V1; var _t = c.A0; c.V1 = _s + _t;  }
        { c.V0 = c.V0 + 0x1u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3CA0u), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 16 ? 1u : 0u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3CA4u), (ushort)c.V1); 
        if (c.V0 != 0u) {
            c.A1 = 0u; 
            goto L801B7CD8;
        }
        c.A1 = 0u; 
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3CA0u), (ushort)0u); 
        L801B7CD8: ;
        c.V0 = 0x801C0000u; 
        c.V0 = (uint)(short)mem.ReadU16((c.V0 - 0x3CA0u)); 
        c.A0 = mem.ReadU32((c.A3 + 0xAu)); 
        if ((int)c.V0 <= 0) {
            goto L801B7D0C;
        }
        c.V1 = c.V0; 
        { c.A1 = c.A1 + 0x1u;  }
        L801B7CF4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A0 = mem.ReadU32(c.A0); 
        { var _s = c.A1; var _t = c.V1; c.V0 = (int)_s < (int)_t ? 1u : 0u;  }
        c.A0 = mem.ReadU32(c.A0); 
        if (c.V0 != 0u) {
            { c.A1 = c.A1 + 0x1u;  }
            goto L801B7CF4;
        }
        { c.A1 = c.A1 + 0x1u;  }
        { c.A1 = c.A1 - 0x1u;  }
        L801B7D0C: ;
        c.V0 = mem.ReadU16((c.A0 + 0xAu)); 
        c.A0 = 0x801C0000u; 
        c.A0 = mem.ReadU32((c.A0 - 0x3C94u)); 
        if (c.A0 == 0u) {
            { c.V0 = c.V0 << 16;  }
            goto L801B7D60;
        }
        { c.V0 = c.V0 << 16;  }
        { c.A2 = (uint)((int)c.V0 >> 16);  }
        L801B7D28: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = (uint)(short)mem.ReadU16((c.A0 + 0xAu)); 
        c.A1 = c.V0; 
        { var _s = c.V0; var _t = c.A2; c.V0 = (int)_s < (int)_t ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = c.A1 + 0x6u;  }
            goto L801B7D50;
        }
        { c.V0 = c.A1 + 0x6u;  }
        c.V1 = mem.ReadU16((c.A0 + 0x14u)); 
        mem.WriteU16((c.A0 + 0xAu), (ushort)c.V0); 
        { c.V1 = c.V1 + 0x6u;  }
        mem.WriteU16((c.A0 + 0x14u), (ushort)c.V1); 
        L801B7D50: ;
        c.A0 = mem.ReadU32(c.A0); 
        if (c.A0 != 0u) {
            goto L801B7D28;
        }
        L801B7D60: ;
        c.V0 = 0u; 
        c.V1 = 0x801C0000u; 
        { c.V1 = c.V1 - 0x3C9Au;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C9Eu), (ushort)0u); 
        c.A0 = mem.ReadU16(c.V1); 
        c.A1 = 0x801C0000u; 
        c.A1 = mem.ReadU16((c.A1 - 0x3C9Cu)); 
        { c.A0 = c.A0 + 0x18u;  }
        { c.A1 = c.A1 + 0x6u;  }
        goto L801B7E88;
        L801B7D8C: ;
        mem.WriteU32((c.A3 - 0xEu), c.V0); 
        c.V1 = mem.ReadU8((c.A0 + 0x1u)); 
        c.A0 = 0x801C0000u; 
        c.A0 = mem.ReadU16((c.A0 - 0x3CA2u)); 
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x3CA0u)); 
        { var _s = c.V1; var _t = c.A0; c.V1 = _s + _t;  }
        { c.V0 = c.V0 + 0x1u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3CA0u), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        { c.V0 = (int)c.V0 < 16 ? 1u : 0u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3CA4u), (ushort)c.V1); 
        if (c.V0 != 0u) {
            c.A1 = 0u; 
            goto L801B7DD8;
        }
        c.A1 = 0u; 
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3CA0u), (ushort)0u); 
        L801B7DD8: ;
        c.V0 = 0x801C0000u; 
        c.V0 = (uint)(short)mem.ReadU16((c.V0 - 0x3CA0u)); 
        c.A0 = mem.ReadU32((c.A3 + 0xAu)); 
        if ((int)c.V0 <= 0) {
            goto L801B7E0C;
        }
        c.V1 = c.V0; 
        { c.A1 = c.A1 + 0x1u;  }
        L801B7DF4: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.A0 = mem.ReadU32(c.A0); 
        { var _s = c.A1; var _t = c.V1; c.V0 = (int)_s < (int)_t ? 1u : 0u;  }
        c.A0 = mem.ReadU32(c.A0); 
        if (c.V0 != 0u) {
            { c.A1 = c.A1 + 0x1u;  }
            goto L801B7DF4;
        }
        { c.A1 = c.A1 + 0x1u;  }
        { c.A1 = c.A1 - 0x1u;  }
        L801B7E0C: ;
        c.V0 = mem.ReadU16((c.A0 + 0xAu)); 
        c.A0 = 0x801C0000u; 
        c.A0 = mem.ReadU32((c.A0 - 0x3C94u)); 
        if (c.A0 == 0u) {
            { c.V0 = c.V0 << 16;  }
            goto L801B7E60;
        }
        { c.V0 = c.V0 << 16;  }
        { c.A2 = (uint)((int)c.V0 >> 16);  }
        L801B7E28: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = (uint)(short)mem.ReadU16((c.A0 + 0xAu)); 
        c.A1 = c.V0; 
        { var _s = c.V0; var _t = c.A2; c.V0 = (int)_s < (int)_t ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = c.A1 + 0xCu;  }
            goto L801B7E50;
        }
        { c.V0 = c.A1 + 0xCu;  }
        c.V1 = mem.ReadU16((c.A0 + 0x14u)); 
        mem.WriteU16((c.A0 + 0xAu), (ushort)c.V0); 
        { c.V1 = c.V1 + 0xCu;  }
        mem.WriteU16((c.A0 + 0x14u), (ushort)c.V1); 
        L801B7E50: ;
        c.A0 = mem.ReadU32(c.A0); 
        if (c.A0 != 0u) {
            goto L801B7E28;
        }
        L801B7E60: ;
        c.V0 = 0u; 
        c.V1 = 0x801C0000u; 
        { c.V1 = c.V1 - 0x3C9Au;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C9Eu), (ushort)0u); 
        c.A0 = mem.ReadU16(c.V1); 
        c.A1 = 0x801C0000u; 
        c.A1 = mem.ReadU16((c.A1 - 0x3C9Cu)); 
        { c.A0 = c.A0 + 0x30u;  }
        { c.A1 = c.A1 + 0xCu;  }
        L801B7E88: ;
        mem.WriteU16(c.V1, (ushort)c.A0); 
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C9Cu), (ushort)c.A1); 
        goto L801B80CC;
        L801B7E9C: ;
        if (c.V0 == 0u) {
            c.A1 = 0u; 
            goto L801B7EE4;
        }
        c.A1 = 0u; 
        c.V1 = 0x801C0000u; 
        { c.V1 = c.V1 - 0x3CACu;  }
        c.A0 = mem.ReadU32(c.V1); 
        { c.V0 = c.A0 + 0x1u;  }
        mem.WriteU32(c.V1, c.V0); 
        c.V1 = mem.ReadU8(c.A0); 
        { c.V0 = c.A2 << 8;  }
        { var _s = c.V1; var _t = c.V0; c.A2 = _s | _t;  }
        c.V0 = 0x80040000u; 
        c.V0 = mem.ReadU32((c.V0 - 0x3800u)); 
        { c.A0 = c.A2 & 0xFFFFu;  }
        c.RA = 0x801B7EDCu;
        Dispatcher.Call(c, m, c.V0);
        c.A0 = c.V0; 
        goto L801B7EF0;
        L801B7EE4: ;
        { c.A0 = c.A2 & 0x00FFu;  }
        c.RA = 0x801B7EECu; 
        Dispatcher.Call(c, m, 0x801B78BCu);
        c.A0 = c.V0; 
        L801B7EF0: ;
        if (c.A0 == 0u) {
            c.A1 = 0u; 
            goto L801B7F70;
        }
        c.A1 = 0u; 
        c.A2 = 0u; 
        { c.V0 = 0u | 0x000Cu;  }
        mem.WriteU32((c.SP + 0x14u), c.V0); 
        { c.V0 = 0u | 0x0010u;  }
        mem.WriteU32((c.SP + 0x18u), c.V0); 
        c.V0 = 0x801C0000u; 
        c.V0 = (uint)(short)mem.ReadU16((c.V0 - 0x3CA0u)); 
        c.A3 = 0x801C0000u; 
        c.A3 = (uint)(short)mem.ReadU16((c.A3 - 0x3CA4u)); 
        { c.V0 = c.V0 << 4;  }
        { c.V0 = c.V0 & 0xFFFFu;  }
        mem.WriteU32((c.SP + 0x10u), c.V0); 
        c.RA = 0x801B7F2Cu; 
        SoTN_main.LoadTPage(c, m);
        c.A0 = 0x801C0000u; 
        c.A0 = mem.ReadU16((c.A0 - 0x3CA4u)); 
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x3CA2u)); 
        { c.V1 = c.A0 + 0x3u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _s - _t;  }
        { c.V0 = c.V0 - 0x38u;  }
        { c.V0 = c.V0 & 0xFFFFu;  }
        { c.V0 = c.V0 < 0x00000008u ? 1u : 0u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3CA4u), (ushort)c.V1); 
        if (c.V0 == 0u) {
            { c.V0 = c.A0 + 0xBu;  }
            goto L801B7FF0;
        }
        { c.V0 = c.A0 + 0xBu;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3CA4u), (ushort)c.V0); 
        goto L801B7FF0;
        L801B7F70: ;
        c.A1 = 0x801C0000u; 
        { c.A1 = c.A1 - 0x3CA4u;  }
        c.A0 = mem.ReadU16(c.A1); 
        c.V0 = 0x801C0000u; 
        c.V0 = mem.ReadU16((c.V0 - 0x3CA2u)); 
        { c.V1 = c.A0 + 0x3u;  }
        { var _s = c.V1; var _t = c.V0; c.V0 = _s - _t;  }
        { c.V0 = c.V0 - 0x38u;  }
        { c.V0 = c.V0 & 0xFFFFu;  }
        { c.V0 = c.V0 < 0x00000008u ? 1u : 0u;  }
        if (c.V0 == 0u) {
            mem.WriteU16(c.A1, (ushort)c.V1); 
            goto L801B7FF0;
        }
        mem.WriteU16(c.A1, (ushort)c.V1); 
        { c.V0 = c.A0 + 0xBu;  }
        mem.WriteU16(c.A1, (ushort)c.V0); 
        goto L801B7FF0;
        L801B7FAC: ;
        c.V1 = 0x801C0000u; 
        { c.V1 = c.V1 - 0x3C9Au;  }
        c.A0 = mem.ReadU16(c.V1); 
        { c.A0 = c.A0 - 0x1u;  }
        { c.V0 = c.A0 & 0xFFFFu;  }
        { c.V0 = c.V0 < 0x00000001u ? 1u : 0u;  }
        mem.WriteU16(c.V1, (ushort)c.A0); 
        goto L801B80CC;
        L801B7FD0: ;
        mem.WriteU32(c.S1, c.V0); 
        c.V0 = mem.ReadU8((c.A0 + 0x1u)); 
        c.V1 = mem.ReadU16((c.S1 + 0xCu)); 
        c.A0 = mem.ReadU16((c.S1 + 0xAu)); 
        { c.V1 = c.V1 + 0x1u;  }
        { var _s = c.V0; var _t = c.A0; c.V0 = _s + _t;  }
        mem.WriteU16((c.S1 + 0x8u), (ushort)c.V0); 
        mem.WriteU16((c.S1 + 0xCu), (ushort)c.V1); 
        L801B7FF0: ;
        c.S0 = 0x801C0000u; 
        { c.S0 = c.S0 - 0x3C9Au;  }
        c.V0 = mem.ReadU16(c.S0); 
        if (c.V0 != 0u) {
            goto L801B802C;
        }
        c.A0 = 0x801C0000u; 
        c.A0 = (uint)(short)mem.ReadU16((c.A0 - 0x3CA0u)); 
        c.RA = 0x801B8018u; 
        SoTN_sel.func_801B786C(c, m);
        { c.V0 = 0u | 0x0001u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C9Eu), (ushort)c.V0); 
        { c.V0 = 0u | 0x004Cu;  }
        mem.WriteU16(c.S0, (ushort)c.V0); 
        L801B802C: ;
        c.V0 = mem.ReadU16(c.S0); 
        c.V1 = 0x801C0000u; 
        c.V1 = mem.ReadU16((c.V1 - 0x3C98u)); 
        { c.V0 = c.V0 - 0x1u;  }
        if (c.V1 != 0u) {
            mem.WriteU16(c.S0, (ushort)c.V0); 
            goto L801B80B4;
        }
        mem.WriteU16(c.S0, (ushort)c.V0); 
        c.A0 = 0x801C0000u; 
        c.A0 = mem.ReadU32((c.A0 - 0x3C94u)); 
        c.A1 = 0u; 
        c.A3 = 0xFFFFFFEDu; 
        { c.A2 = c.S0 - 0x2u;  }
        L801B8058: ;
        RecompOne.Runtime.Interrupts.Poll(c, m);
        c.V0 = mem.ReadU16((c.A0 + 0xAu)); 
        { c.V0 = c.V0 - 0x1u;  }
        mem.WriteU16((c.A0 + 0xAu), (ushort)c.V0); 
        { c.V0 = c.V0 << 16;  }
        { c.V0 = (uint)((int)c.V0 >> 16);  }
        if (c.V0 != c.A3) {
            { c.A1 = c.A1 + 0x1u;  }
            goto L801B809C;
        }
        { c.A1 = c.A1 + 0x1u;  }
        c.V0 = mem.ReadU16(c.A2); 
        c.V1 = mem.ReadU16((c.A0 + 0x14u)); 
        { var _s = c.V0; var _t = c.V1; c.V0 = _s - _t;  }
        { c.V0 = c.V0 + 0x11Du;  }
        mem.WriteU16((c.A0 + 0xAu), (ushort)c.V0); 
        c.V0 = mem.ReadU16(c.A2); 
        mem.WriteU16((c.A0 + 0x14u), (ushort)c.V0); 
        L801B809C: ;
        c.A0 = mem.ReadU32(c.A0); 
        { c.V0 = (int)c.A1 < 32 ? 1u : 0u;  }
        if (c.V0 != 0u) {
            { c.V0 = 0u | 0x0004u;  }
            goto L801B8058;
        }
        { c.V0 = 0u | 0x0004u;  }
        c.At = 0x801C0000u; 
        mem.WriteU16((c.At - 0x3C98u), (ushort)c.V0); 
        L801B80B4: ;
        c.A0 = 0x801C0000u; 
        { c.A0 = c.A0 - 0x3C98u;  }
        c.V1 = mem.ReadU16(c.A0); 
        c.V0 = 0u; 
        { c.V1 = c.V1 - 0x1u;  }
        mem.WriteU16(c.A0, (ushort)c.V1); 
        L801B80CC: ;
        c.RA = mem.ReadU32((c.SP + 0x54u)); 
        c.S4 = mem.ReadU32((c.SP + 0x50u)); 
        c.S3 = mem.ReadU32((c.SP + 0x4Cu)); 
        c.S2 = mem.ReadU32((c.SP + 0x48u)); 
        c.S1 = mem.ReadU32((c.SP + 0x44u)); 
        c.S0 = mem.ReadU32((c.SP + 0x40u)); 
        { c.SP = c.SP + 0x58u;  }
        return;
        }
}
