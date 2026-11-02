using RecompOne.Runtime.Context;
using RecompOne.Runtime.Memory;

namespace Recompiled;

//makes so alucard shield and wepon dont automatically despawn for being outside the view area
public static partial class WidescreenPatch
{
    const uint PlayerEntityTable = 0x800733D8;
    const int PlayerEntityStride = 0xBC;
    const uint PedPosXHi = 0x02;
    const uint PedPosYHi = 0x06;
    const uint PedEntityId = 0x26;

    const uint PedDespawnReturn = 0x8011A6B0;
    const uint RicDespawnReturn = 0x801604C8;
    const int PedBoundRight = 288;
    const int PedBoundLeft = -32;
    const int PedBoundBottom = 256;
    const int PedBoundTop = -16;

    static uint PedAt(int index) => PlayerEntityTable + (uint)(index * PlayerEntityStride);

    public static bool PreDestroyPlayerEntity(CpuContext c, IMemory m)
    {
        if (OriginalAspect) return true;
        if (c.RA != PedDespawnReturn && c.RA != RicDespawnReturn) return true;

        int margin = StageMargin();
        if (margin == 0) return true;

        uint entity = c.A0;
        if (entity == 0) return true;

        int y = (short)m.ReadU16(entity + PedPosYHi);
        if (y > PedBoundBottom || y < PedBoundTop) return true;

        int x = (short)m.ReadU16(entity + PedPosXHi);
        if (x > PedBoundRight + margin || x < PedBoundLeft - margin) return true;

        return false;
    }
}
