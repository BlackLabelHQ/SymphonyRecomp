using RecompOne.Runtime.Events;

namespace Recompiled;

public sealed class TilemapRenderedEvent : GameEvent
{
    public int ScrollX;
    public int ScrollY;
    public bool LayersVisible;
}

/// <summary>
/// is raised inside the tilemap renderer after a layer queued its tiles and before it queues the texture page stup,so this can be used to render tiles from the page in the correct position (as on i did on RoomFill)
/// </summary>
public sealed class TilemapLayerDrawnEvent : GameEvent
{
    public bool Foreground;
    public uint Layout;
    public int Columns;
    public int Rows;
    public bool Wrap;
    public uint Pool;
    public uint Ot;
    public uint GfxPage;
    public uint GfxIndex;
    public uint ClutTable;
    public uint Cmd;
    public uint ClutBase;
    public int Order;
    public int BackbufferX;
    public int BackbufferY;
    public int ScrollX;
    public int ScrollY;
    public uint Sprites;
    public int MaxSprites;
}
