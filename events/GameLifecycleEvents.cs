using RecompOne.Runtime.Context;
using RecompOne.Runtime.Events;
using RecompOne.Runtime.Memory;
using Sotn;

namespace Recompiled;

public sealed class PlayerLoadedEvent : GameEvent
{
    public PlayableCharacter Character;
}

public sealed class SaveLoadedEvent : GameEvent
{
    public uint Block;
}

public sealed class SaveCreatedEvent : GameEvent
{
    public uint Block;
}

public static class GameHooks
{
    const uint NewGameFlag = 0x8003C730;
    const uint DemoModeAddr = 0x80097914;

    static uint _storeBlock;
    static uint _applyBlock;

    public static void OnInitStatsAndGear(CpuContext c, IMemory m)
    {
        if (m.ReadU32(NewGameFlag) != 0)
            return;
        Event.Dispatch(new PlayerLoadedEvent { Context = c, Memory = m, Character = Game.Character });
    }

    public static void PreApplySaveData(CpuContext c, IMemory m) => _applyBlock = c.A0;

    public static void OnApplySaveData(CpuContext c, IMemory m)
    {
        uint block = _applyBlock;
        _applyBlock = 0;

        if (m.ReadU8(DemoModeAddr) != 0)
            return;
        Event.Dispatch(new PlayerLoadedEvent { Context = c, Memory = m, Character = Game.Character });
        Event.Dispatch(new SaveLoadedEvent { Context = c, Memory = m, Block = block });
    }

    public static void PreStoreSaveData(CpuContext c, IMemory m) => _storeBlock = c.A0;

    public static void OnStoreSaveData(CpuContext c, IMemory m)
    {
        uint block = _storeBlock;
        _storeBlock = 0;

        if (m.ReadU8(DemoModeAddr) != 0)
            return;
        Event.Dispatch(new SaveCreatedEvent { Context = c, Memory = m, Block = block });
    }
}
