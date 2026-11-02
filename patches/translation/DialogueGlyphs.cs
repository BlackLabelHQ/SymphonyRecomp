using RecompOne.Runtime.Context;

namespace Recompiled.Translation;

internal static class DialogueGlyphs
{
    private sealed record Position(uint X, uint Y, uint Script, int Remainder);
    private static readonly Dictionary<uint, Position> Positions = [];
    private static DialogueFont? _font;
    internal static int LineSpacing => _font?.LineSpacing ?? 12;

    internal static void Reset() { Positions.Clear(); Layout.Clear(); _font = null; }
    internal static void Load(DialogueFont font) => _font = font;

    private const int LowerStaging = 0x180;
    private const int UpperStaging = 0x100;
    private const int NativeLine = 12;

    private static int StagingBase(int y) => y >= LowerStaging ? LowerStaging : UpperStaging;

    private static int StagingRow(int y)
    {
        int stagingBase = StagingBase(y);
        return stagingBase + (y - stagingBase) / NativeLine * LineSpacing;
    }

    private sealed record LayoutValue(uint Native, uint Rendered);
    private static readonly Dictionary<uint, LayoutValue> Layout = [];

    internal static int VisibleLines(ScriptMemory memory) => Math.Min(memory.Ferryman ? memory.LineCount : memory.LineCount - 1, _font?.VisibleLines ?? 4);

    internal static void BeginLine(ScriptMemory memory)
    {
        if (memory.Ferryman) return;
        
        uint row = memory.Native.ReadU16(memory.Cursor + 14);
        uint flags = memory.Cursor + 18;
        
        if (row >= VisibleLines(memory)) memory.Native.WriteU16(flags, (ushort)(memory.Native.ReadU16(flags) | 1));
    }

    internal static void PrepareLine(CpuContext context, ScriptMemory memory)
    {
        if (!memory.Ferryman) return;
        
        int rows = VisibleLines(memory);
        
        if (memory.Native.ReadU16(memory.Cursor + 6) < rows) return;
        
        var gpu = RecompOne.Runtime.Runtime.Gpu;
        
        if (gpu != null && rows > 1)
        {
            gpu.WriteGp0(0x01000000);
            gpu.WriteGp0(0x80000000);
            gpu.WriteGp0((uint)(384 + LineSpacing) << 16);
            gpu.WriteGp0(384u << 16);
            gpu.WriteGp0(64u | (uint)((rows - 1) * LineSpacing) << 16);
        }
        
        memory.Native.WriteU16(memory.Cursor + 6, (ushort)(rows - 1));
        context.A0 = (uint)((rows - 1) * 12);
        Positions.Remove(memory.Cursor);
    }

    private static int Field(ScriptMemory memory, uint address)
    {
        if (!memory.Layout) return 0;
        uint pointers = memory.Cursor + (memory.Ferryman ? 12u : 24u);
        
        for (int i = 0; i < memory.LineCount; i++)
        {
            uint primitive = memory.Native.ReadU32(pointers + (uint)i * 4);
            if (primitive < 0x80000000 || primitive >= 0x80200000) continue;
            uint offset = address - primitive;
            if (offset is 8 or 10 or 12 or 13 or 24 or 25) return (int)offset;
        }
        return 0;
    }

    internal static uint ReadLayout(ScriptMemory memory, uint address, uint value)
    {
        if (Field(memory, address) == 0) return value;
        if (Layout.TryGetValue(address, out var previous) && previous.Rendered == value) return previous.Native;
        
        Layout.Remove(address);
       
        return value;
    }

    internal static uint WriteLayout(ScriptMemory memory, uint address, uint value)
    {
        int field = Field(memory, address);
        
        if (field == 0) return value;
        
        int baseline = memory.Ferryman ? 44 : memory.Native.ReadU16(memory.Cursor + 8) + 20;
        uint rendered = field switch
        {
            8 => unchecked((ushort)((short)value + (_font?.TextLeft ?? 12) - 4)),
            24 => Math.Min(value, (uint)(_font?.TextWidth ?? 192)),
            10 => unchecked((ushort)(baseline + (_font?.TextTop ?? 20) - 20 + ((short)value - baseline) * LineSpacing / 12)),
            12 => unchecked((byte)(value + 8)),
            13 => unchecked((byte)(StagingRow(UpperStaging + (int)value) - UpperStaging)),
            _ => value * (uint)LineSpacing / 12
        };
        
        Layout[address] = new(value, rendered);
        return rendered;
    }

    internal static void Clear(CpuContext context, ScriptMemory memory)
    {
        uint rectangle = context.A0;
        ushort y = memory.Native.ReadU16(rectangle + 2), height = memory.Native.ReadU16(rectangle + 6);
        int stagingBase = StagingBase(y);
        bool dialogue = LineSpacing != NativeLine && y >= UpperStaging && y < stagingBase + memory.LineCount * NativeLine &&
                        (y - stagingBase) % NativeLine == 0 && height == NativeLine && memory.Native.ReadU16(rectangle + 4) == 64;
        
        if (!dialogue) { SoTN_main.ClearImage(context, memory.Native); return; }
        
        memory.Native.WriteU16(rectangle + 2, (ushort)StagingRow(y));
        memory.Native.WriteU16(rectangle + 6, (ushort)LineSpacing);
        
        try { SoTN_main.ClearImage(context, memory.Native); }
        finally { memory.Native.WriteU16(rectangle + 2, y); memory.Native.WriteU16(rectangle + 6, height); }
    }

    internal static bool Draw(CpuContext context, ScriptMemory memory)
    {
        if (!memory.TryGlyph(out int code)) return false;
        if (memory.ReadU16(context.A0 + 4) != 2 || memory.ReadU16(context.A0 + 6) != 8) return false;
        if (memory.ReadU16(context.A0) != 0x382 || memory.ReadU16(context.A0 + 2) != 0x100) return false;
        
        var glyph = _font?.Find(code, memory.Richter);
        
        if (glyph == null && code < 128)
        {
            memory.WriteU16(context.A0, (ushort)(0x380 + (code & 15) * 2));
            memory.WriteU16(context.A0 + 2, (ushort)(0xF0 + (code & 0xF0) / 2));
            return false;
        }
        
        if (glyph == null) throw new InvalidDataException($"missing dialogue glyph {code:X4}.");
        
        var gpu = RecompOne.Runtime.Runtime.Gpu;
        
        
        if (gpu == null) { context.V0 = uint.MaxValue; return true; }
        
        uint script = memory.ReadU32(memory.Cursor);
        
        int carry = Positions.TryGetValue(memory.Cursor, out var previous) && previous.X == context.A1 && previous.Y == context.A2 && previous.Script + 1 == script ? previous.Remainder : 0;
        int pixelX = (int)context.A1 * 4 + carry + glyph.X;
        int firstWord = (int)Math.Floor(pixelX / 4.0);
        int inset = pixelX - firstWord * 4;
        int width = (inset + glyph.Width + 3) / 4;
        int destinationY = StagingRow((int)context.A2) + glyph.Y;
        
        var words = new ushort[width * glyph.Height];
        
        for (int y = 0; y < glyph.Height; y++)
        {
            for (int word = 0; word < width; word++) words[y * width + word] = gpu.Vram[((destinationY + y) & 511) * 1024 + ((firstWord + word) & 1023)];
            for (int x = 0; x < glyph.Width; x++)
            {
                int pixel = inset + x, word = y * width + pixel / 4, shift = pixel % 4 * 4;
                words[word] = (ushort)((words[word] & ~(15 << shift)) | glyph.Pixels[y * glyph.Width + x] << shift);
            }
        
        }
        
        gpu.WriteGp0(0x01000000);
        gpu.WriteGp0(0xA0000000);
        gpu.WriteGp0((uint)(firstWord & 65535) | (uint)destinationY << 16);
        gpu.WriteGp0((uint)width | (uint)glyph.Height << 16);
        
        for (int i = 0; i < words.Length; i += 2) gpu.WriteGp0(words[i] | (i + 1 < words.Length ? (uint)words[i + 1] << 16 : 0));
        
        int advance = carry + glyph.Advance;
        uint nextX = context.A1 + (uint)(advance / 4);
        uint xAddress = memory.Cursor + (memory.Ferryman ? 4u : 10u);
        
        memory.WriteU16(xAddress, unchecked((ushort)(nextX - 2)));
        Positions[memory.Cursor] = new(nextX, context.A2, script, advance % 4);
        context.V0 = 0;
        
        return true;
    }
}
