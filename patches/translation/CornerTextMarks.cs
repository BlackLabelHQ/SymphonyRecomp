using RecompOne.Runtime.Memory;

namespace Recompiled.Translation;

internal static class CornerTextMarks
{
    private const uint TimerAddr = 0x80097410;
    private const uint PrimsAddr = 0x80097414;
    private const uint PrimBufAddr = 0x80086FEC;
    private const uint PrimStride = 0x34;
    private const byte LeftBlue = 0xAF;
    private const ushort TextPage = 0x1E;
    private const byte MarkV = 0x48;
    private const byte DakutenU = 0x70;
    private const byte HandakutenU = 0x78;
    private const short MarkY = 0xCC;
    private const int Advance = 8;
    private const int BoxPrims = 4;
    private static readonly uint[] XFields = [0x08, 0x14, 0x20, 0x2C];

    internal static void Combine(TilemapRenderedEvent e)
    {
        var m = e.Memory;
        if (m.ReadU32(TimerAddr) == 0) return;

        int index = (int)m.ReadU32(PrimsAddr);
        if (!Pending(m, PrimBufAddr + (uint)index * PrimStride)) return;

        var chain = new List<uint>();
        for (uint p = PrimBufAddr + (uint)index * PrimStride; p != 0 && chain.Count < 0x100; p = m.ReadU32(p))
            chain.Add(p);
        if (chain.Count <= BoxPrims) return;

        int marks = 0;
        uint previous = 0;
        for (int i = BoxPrims; i < chain.Count; i++)
        {
            uint p = chain[i];
            if (m.ReadU16(p + 0x1A) != TextPage) continue;
            if (previous != 0 && IsMark(m, p))
            {
                m.WriteU16(p + 0x08, m.ReadU16(previous + 0x08));
                m.WriteU16(p + 0x0A, unchecked((ushort)MarkY));
                marks++;
                continue;
            }
            Move(m, p + 0x08, -marks * Advance);
            previous = p;
        }
        if (marks == 0) return;

        int shrink = marks * Advance;
        Move(m, chain[0] + 0x14, -shrink);
        Move(m, chain[0] + 0x2C, -shrink);
        Move(m, chain[2] + 0x08, -shrink);
        Move(m, chain[3] + 0x14, -shrink);
        Move(m, chain[3] + 0x2C, -shrink);

        if (m.ReadU8(chain[0] + 0x06) == LeftBlue) return;
        foreach (uint p in chain)
            foreach (uint field in XFields)
                Move(m, p + field, shrink);
    }

    private static bool Pending(IMemory m, uint box)
    {
        int i = 0;
        bool glyph = false;
        for (uint p = box; p != 0 && i < 0x100; p = m.ReadU32(p), i++)
        {
            if (i < BoxPrims || m.ReadU16(p + 0x1A) != TextPage) continue;
            if (glyph && IsMark(m, p) && (short)m.ReadU16(p + 0x0A) != MarkY) return true;
            glyph = true;
        }
        return false;
    }

    private static bool IsMark(IMemory m, uint p) => m.ReadU8(p + 0x0D) == MarkV && m.ReadU8(p + 0x0C) is DakutenU or HandakutenU;

    private static void Move(IMemory m, uint field, int dx) => m.WriteU16(field, unchecked((ushort)((short)m.ReadU16(field) + dx)));
}
