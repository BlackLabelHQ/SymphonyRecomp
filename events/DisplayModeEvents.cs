using RecompOne.Runtime.Context;
using RecompOne.Runtime.Events;
using RecompOne.Runtime.Memory;

namespace Recompiled;

public enum DisplayMode
{
    Unknown,
    Stage,
    Menu,
    Cutscene,
    Title,
}

public sealed class DisplayModeEvent : GameEvent
{
    public DisplayMode Mode;
    public DisplayMode Previous;
}

public static class DisplayModeHooks
{
    public static DisplayMode Current { get; private set; } = DisplayMode.Unknown;

    public static bool IsStage => Current == DisplayMode.Stage;
    public static bool IsMenu => Current == DisplayMode.Menu;
    public static bool IsCutscene => Current == DisplayMode.Cutscene;
    public static bool IsTitle => Current == DisplayMode.Title;

    public static void Set(DisplayMode mode, CpuContext c, IMemory m)
    {
        DisplayMode previous = Current;
        Current = mode;
        Event.Dispatch(new DisplayModeEvent
        {
            Context = c, Memory = m,
            Mode = mode,
            Previous = previous,
        });
    }

    public static void PreStageDisplayBuffer(CpuContext c, IMemory m) => Set(DisplayMode.Stage, c, m);

    public static void PreMenuDisplayBuffer(CpuContext c, IMemory m) => Set(DisplayMode.Menu, c, m);

    public static void PreCgiDisplayBuffer(CpuContext c, IMemory m) => Set(DisplayMode.Cutscene, c, m);

    public static void PreTitleDisplayBuffer(CpuContext c, IMemory m) => Set(DisplayMode.Title, c, m);
}
