using RecompOne.Runtime.Context;
using RecompOne.Runtime.Memory;

namespace Recompiled;

public static partial class WidescreenPatch
{
    const uint EntBlendMode = 0x18;
    const uint EntDrawFlags = 0x19;
    const byte DrawOpacity = 0x08;
    const byte BlendAdd = 0x20;
    const int OpacityFull = 0x80;
    const int ActorFadeStep = 4;
    const int ActorFadeAdditive = 0x50;
    const int ActorFadeBounds = 32;

    static readonly string[] FadingActors = ["EntityMariaNZ0", "DAI_EntityCutsceneMaria"];

    public static bool PreDestroyFadingActor(CpuContext c, IMemory m)
    {
        uint entity = c.A0;
        if (OriginalAspect || entity == 0 || !FadingActor(m, m.ReadU32(entity + EntPfnUpdate))) return true;

        int x = (short)m.ReadU16(entity + PedPosXHi);
        if (x < ViewLeft - ActorFadeBounds || x > ViewRight + ActorFadeBounds) return true;

        byte flags = m.ReadU8(entity + EntDrawFlags);
        int opacity = ((flags & DrawOpacity) != 0 ? m.ReadU8(entity + EntOpacity) : OpacityFull) - ActorFadeStep;
        if (opacity <= 0) return true;

        m.WriteU8(entity + EntDrawFlags, (byte)(flags | DrawOpacity));
        m.WriteU8(entity + EntOpacity, (byte)opacity);
        if (opacity <= ActorFadeAdditive) m.WriteU8(entity + EntBlendMode, BlendAdd);
        return false;
    }

    static bool FadingActor(IMemory m, uint update)
    {
        if (update == 0) return false;
        foreach (var name in FadingActors)
            if (StageSym(m, name).Addr == update) return true;
        return false;
    }
}
