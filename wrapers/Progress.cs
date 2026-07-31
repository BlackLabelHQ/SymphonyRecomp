using System;
using System.Collections.Generic;
using RecompOne.Runtime.Memory;

namespace Sotn;

public static class Progress
{
    public const uint CastleFlagsAddr = 0x8003BDECu;
    public const uint TimeAttackAddr = 0x8003CA28u;

    public const int WarpsFirstCastleIndex = 0xD0;
    public const int WarpsSecondCastleIndex = 0xD1;
    public const int ItemsCollectedIndex = 0xF4;
    public const int ItemsCollectedCount = 143;
    public const int CastleFlagCount = 0x300;
    public const int TimeAttackCount = 27;

    static IMemory M => RecompOne.Runtime.Runtime.Mem!;

    public static byte GetFlag(int index) => M.ReadU8(CastleFlagsAddr + (uint)index);
    public static void SetFlag(int index, byte value) => M.WriteU8(CastleFlagsAddr + (uint)index, value);

    public static bool GetShortcut(Shortcut shortcut) => GetFlag((int)shortcut) != 0;
    public static void SetShortcut(Shortcut shortcut, bool on) => SetFlag((int)shortcut, (byte)(on ? 1 : 0));

    public static Warp WarpsFirstCastle
    {
        get => (Warp)GetFlag(WarpsFirstCastleIndex);
        set => SetFlag(WarpsFirstCastleIndex, (byte)value);
    }

    public static Warp WarpsSecondCastle
    {
        get => (Warp)GetFlag(WarpsSecondCastleIndex);
        set => SetFlag(WarpsSecondCastleIndex, (byte)value);
    }

    public static bool HasWarp(Warp warp, bool secondCastle = false) =>
        ((secondCastle ? WarpsSecondCastle : WarpsFirstCastle) & warp) == warp;

    public static void GrantWarp(Warp warp, bool secondCastle = false)
    {
        if (secondCastle) WarpsSecondCastle |= warp;
        else WarpsFirstCastle |= warp;
    }

    public static void TakeWarp(Warp warp, bool secondCastle = false)
    {
        if (secondCastle) WarpsSecondCastle &= ~warp;
        else WarpsFirstCastle &= ~warp;
    }

    public static void GrantAllWarps()
    {
        WarpsFirstCastle = Warp.Entrance | Warp.Mines | Warp.OuterWall | Warp.Keep | Warp.Olrox;
        WarpsSecondCastle = Warp.Entrance | Warp.Mines | Warp.OuterWall | Warp.Keep | Warp.Olrox;
    }

    public static int GetTimeAttack(TimeAttackEvent time) => (int)M.ReadU32(TimeAttackAddr + (uint)((int)time * 4));
    public static void SetTimeAttack(TimeAttackEvent time, int value) => M.WriteU32(TimeAttackAddr + (uint)((int)time * 4), (uint)value);
    public static bool IsDefeated(TimeAttackEvent time) => GetTimeAttack(time) != 0;

    public static IEnumerable<TimeAttackEvent> Bosses
    {
        get
        {
            for (int i = (int)TimeAttackEvent.OlroxDefeat; i <= (int)TimeAttackEvent.GalamothDefeat; i++)
                yield return (TimeAttackEvent)i;
        }
    }

    public static void RespawnBosses()
    {
        foreach (var boss in Bosses) SetTimeAttack(boss, 0);
    }

    public static void RespawnItems()
    {
        for (int i = 0; i < ItemsCollectedCount; i++)
            SetFlag(ItemsCollectedIndex + i, 0);
    }
}
