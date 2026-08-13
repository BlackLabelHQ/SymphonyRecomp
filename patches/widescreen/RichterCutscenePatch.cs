using RecompOne.Runtime.Context;
using RecompOne.Runtime.Memory;

namespace Recompiled;

public static class RichterCutscenePatch
{
    const uint TilemapX = 0x800730C0;
    const uint TilemapWidth = 0x800730C8;
    const uint TilemapScrollXHi = 0x8007308E;
    const uint PosXHi = 0x02;

    public static bool Enabled = true;
    public static int WorldOffset = 0;

    public static void ForceCenter(CpuContext c, IMemory m)
    {
        if (!Enabled) return;

        uint entity = c.A0;
        if (entity == 0) return;

        int left = (int)m.ReadU32(TilemapX);
        int right = (int)m.ReadU32(TilemapWidth);
        if (right <= left) return;

        int world = (left + right) / 2 + WorldOffset;
        int scroll = (short)m.ReadU16(TilemapScrollXHi);

        m.WriteU16(entity + PosXHi, (ushort)(short)(world - scroll));
    }
}
