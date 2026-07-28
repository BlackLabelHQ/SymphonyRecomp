using RecompOne.Runtime.Context;
using RecompOne.Runtime.Events;
using RecompOne.Runtime.Memory;

namespace Recompiled;

/// <summary>room layer is being loaded</summary>
public sealed class RoomLayerLoadEvent : GameEvent
{
    public int StageId;
    public int LayerIndex;
}

/// <summary>the room foreground tilemap is seted</summary>
public sealed class ForegroundLayerLoadEvent : GameEvent
{
    public int StageId;
    public uint LayerDef;
}

/// <summary>a room background layer is setted</summary>
public sealed class BackgroundLayerLoadEvent : GameEvent
{
    public int StageId;
    public int Index;
    public uint LayerDef;
}

public static class LevelEventHooks
{
    const uint GStageId = 0x800974A0;

    static int _layerIndex;
    static uint _fgDef;
    static int _bgIndex;
    static uint _bgDef;

    public static void PreLoadRoomLayer(CpuContext c, IMemory m) => _layerIndex = (int)c.A0;

    public static void PostLoadRoomLayer(CpuContext c, IMemory m)
    {
        Event.Dispatch(new RoomLayerLoadEvent
        {
            Context = c, Memory = m,
            StageId = m.ReadU16(GStageId),
            LayerIndex = _layerIndex,
        });
    }

    public static void PreForegroundLayer(CpuContext c, IMemory m) {
        _fgDef = c.A0;
    }

    public static void PostForegroundLayer(CpuContext c, IMemory m)
    {
        Event.Dispatch(new ForegroundLayerLoadEvent
        {
            Context = c, Memory = m,
            StageId = m.ReadU16(GStageId),
            LayerDef = _fgDef,
        });
    }

    public static void PreBackgroundLayer(CpuContext c, IMemory m)
    {
        _bgIndex = (int)c.A0;
        _bgDef = c.A1;
    }

    public static void PostBackgroundLayer(CpuContext c, IMemory m)
    {
        Event.Dispatch(new BackgroundLayerLoadEvent
        {
            Context = c, Memory = m,
            StageId = m.ReadU16(GStageId),
            Index = _bgIndex,
            LayerDef = _bgDef,
        });
    }
}
