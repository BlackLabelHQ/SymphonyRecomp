using System.Collections.Generic;
using RecompOne.Runtime.Memory;

namespace Sotn;

public enum PlayableCharacter { Alucard = 0, Richter = 1 }

public enum GameState
{
    Init = 0,
    Title = 1,
    Play = 2,
    GameOver = 3,
    NowLoading = 4,
    VideoPlayback = 5,
    Unk6 = 6,
    PrologueEnd = 7,
    MainMenu = 8,
    Ending = 9,
    Boot = 10,
}

public enum PlayStep
{
    Reset = 0,
    Init = 1,
    PrepareDemo = 2,
    Default = 3,
    PrepareNextStage = 4,
    LoadStageChr = 5,
    WaitStageChr = 6,
    LoadStageSfx = 7,
    WaitStageSfx = 8,
    LoadStagePrg = 9,
    WaitStagePrg = 10,
    Unk11 = 11,
    Unk12 = 12,
    Unk13 = 13,
    Unk14 = 14,
    Unk15 = 15,
    Prologue = 16,
}

public enum MenuCategory
{
    Equip = 0,
    Spells = 1,
    Relics = 2,
    System = 3,
    Familiars = 4,
}

public static class Game
{
    public const uint EntitiesAddr = 0x800733D8u;
    public const uint StatusAddr = 0x80097964u;
    public const uint PlayerStateAddr = 0x80072BD0u;
    public const uint PlayableCharacterAddr = 0x8003C9A0u;
    public const uint CurrentEntityAddr = 0x8006C3B8u;
    public const int EntityCount = 256;

    public const uint GameStateAddr = 0x8003C734u;
    public const uint EngineStepAddr = 0x8003C9A4u;
    public const uint LoadingAddr = 0x8003CF7Cu;
    public const uint StageIdAddr = 0x800974A0u;
    public const uint MenuOpenAddr = 0x800973ECu;
    public const uint MapOpenAddr = 0x800974A4u;
    public const uint MenuNavAddr = 0x8003C9A8u;
    public const uint SettingsMenuOpenAddr = 0x8003D04Eu;
    public const uint EquipMenuOpenAddr = 0x80137948u;
    public const uint RelicMenuOpenAddr = 0x801FFF6Au;
    public const uint CanSaveAddr = 0x8003C708u;
    public const uint CanWarpAddr = 0x8003C710u;
    public const uint CameraAddr = 0x8007308Cu;
    public const uint MapCursorAddr = 0x800730B0u;
    public const uint PlayerScreenPosAddr = 0x800973F0u;
    public const uint RoomAddr = 0x801375BCu;
    public const uint AreaAddr = 0x801375BDu;
    public const uint CameraAdjustAddr = 0x801375B4u;
    public const uint PadsAddr = 0x80097490u;
    public const uint QcfCounterAddr = 0x80138FC4u;
    public const uint UnderwaterAddr = 0x80097448u;
    public const uint MusicTrackAddr = 0x8013901Cu;
    public const uint LastLoadedTrackAddr = 0x80138458u;
    public const uint TrackVolumeAddr = 0x80139820u;
    public const uint MusicVolumeAddr = 0x8013B668u;
    public const uint LibraryCardDestAddr = 0x800A25E2u;
    public const uint SeedNameAddr = 0x801A78B4u;
    public const uint PresetNameAddr = 0x801A78D4u;
    public const uint RandoGoalAddr = 0x800988B0u;

    public const uint UnderwaterOn = 0x0090u;
    public const uint CanSaveMask = 0x20u;
    public const uint CanWarpMask = 0x0Eu;
    public const uint InvertedCastleFlag = 0x20u;

    internal static IMemory M => RecompOne.Runtime.Runtime.Mem;

    public static bool Available => RecompOne.Runtime.Runtime.Mem != null; //game is only available when mem is instantiated, adding this so no one tries to do stuff before it can

    public static PlayableCharacter Character => (PlayableCharacter)(int)M.ReadU32(PlayableCharacterAddr);

    public static Entity CurrentEntity => new(M.ReadU32(CurrentEntityAddr));

    public static GameState State => (GameState)M.ReadU8(GameStateAddr);
    public static PlayStep EngineStep => (PlayStep)M.ReadU8(EngineStepAddr);
    public static Stage StageId => (Stage)M.ReadU8(StageIdAddr);
    public static bool SecondCastle => (M.ReadU8(StageIdAddr) & InvertedCastleFlag) != 0;

    public static bool InGame => State == GameState.Play;
    public static bool InMainMenu => State == GameState.MainMenu;
    public static bool IsLoading => State == GameState.NowLoading || M.ReadU8(LoadingAddr) == 0x88;
    public static bool CanMenu => InGame && !IsLoading;

    public static bool MenuOpen => M.ReadU8(MenuOpenAddr) != 0;
    public static bool MapOpen => M.ReadU8(MapOpenAddr) != 0;
    public static bool SettingsMenuOpen => M.ReadU8(SettingsMenuOpenAddr) != 0;
    public static MenuCategory Category => (MenuCategory)M.ReadU8(MenuNavAddr);
    public static bool EquipMenuOpen => M.ReadU8(EquipMenuOpenAddr) != 0 && Category == MenuCategory.Equip;
    public static bool RelicMenuOpen => M.ReadU8(RelicMenuOpenAddr) != 0 && Category == MenuCategory.Relics;

    public static bool CanSave => (M.ReadU8(CanSaveAddr) & CanSaveMask) == CanSaveMask;
    public static bool CanWarp => (M.ReadU8(CanWarpAddr) & CanWarpMask) == CanWarpMask;

    public static int Area { get => M.ReadU8(AreaAddr); set => M.WriteU8(AreaAddr, (byte)value); }
    public static int Room { get => M.ReadU8(RoomAddr); set => M.WriteU8(RoomAddr, (byte)value); }
    public static int RoomX => M.ReadU8(MapCursorAddr);
    public static int RoomY => M.ReadU8(MapCursorAddr + 4);

    public static int CameraX { get => (int)M.ReadU32(CameraAddr); set => M.WriteU32(CameraAddr, (uint)value); }
    public static int CameraY { get => (int)M.ReadU32(CameraAddr + 4); set => M.WriteU32(CameraAddr + 4, (uint)value); }
    public static int CameraAdjustX => (int)M.ReadU32(CameraAdjustAddr);
    public static int CameraAdjustY => (int)M.ReadU32(CameraAdjustAddr + 4);

    public static int PlayerScreenX => (int)M.ReadU32(PlayerScreenPosAddr);
    public static int PlayerScreenY => (int)M.ReadU32(PlayerScreenPosAddr + 4);

    public static int Hours { get => (int)M.ReadU32(StatusAddr + 0x2CC); set => M.WriteU32(StatusAddr + 0x2CC, (uint)value); }
    public static int Minutes { get => (int)M.ReadU32(StatusAddr + 0x2D0); set => M.WriteU32(StatusAddr + 0x2D0, (uint)value); }
    public static int Seconds { get => (int)M.ReadU32(StatusAddr + 0x2D4); set => M.WriteU32(StatusAddr + 0x2D4, (uint)value); }
    public static int Frames { get => (int)M.ReadU32(StatusAddr + 0x2D8); set => M.WriteU32(StatusAddr + 0x2D8, (uint)value); }

    public static bool UnderwaterPhysics
    {
        get => M.ReadU16(UnderwaterAddr) != 0;
        set => M.WriteU16(UnderwaterAddr, (ushort)(value ? UnderwaterOn : 0));
    }

    public static int MusicTrack { get => M.ReadU8(MusicTrackAddr); set => M.WriteU8(MusicTrackAddr, (byte)value); }
    public static int LastLoadedTrack => M.ReadU8(LastLoadedTrackAddr);
    public static int TrackVolume { get => M.ReadU8(TrackVolumeAddr); set => M.WriteU8(TrackVolumeAddr, (byte)value); }
    public static int MusicVolume { get => M.ReadU8(MusicVolumeAddr); set => M.WriteU8(MusicVolumeAddr, (byte)value); }

    public static ushort Pressed => M.ReadU16(PadsAddr);
    public static ushort Released => M.ReadU16(PadsAddr + 2);
    public static ushort Tapped => M.ReadU16(PadsAddr + 4);
    public static ushort Repeat => M.ReadU16(PadsAddr + 6);
    public static ushort Pressed2 => M.ReadU16(PadsAddr + 8);
    public static ushort Tapped2 => M.ReadU16(PadsAddr + 12);
    public static bool IsPressed(Button b) => (Pressed & (ushort)b) != 0;
    public static bool IsTapped(Button b) => (Tapped & (ushort)b) != 0;

    public static ushort QcfCounter { get => M.ReadU16(QcfCounterAddr); set => M.WriteU16(QcfCounterAddr, value); }

    public static bool AllBossesGoal => (M.ReadU8(RandoGoalAddr) & 1) != 0;
    public static string SeedName => Text.Read(SeedNameAddr);
    public static string PresetName => Text.ReadPreset(PresetNameAddr);

    public static void SetLibraryCardDestination(int x, int y, int room)
    {
        M.WriteU16(LibraryCardDestAddr, (ushort)x);
        M.WriteU16(LibraryCardDestAddr + 2, (ushort)y);
        M.WriteU16(LibraryCardDestAddr + 4, (ushort)room);
    }

    public static bool InAlucardMode() =>
        InGame && Character == PlayableCharacter.Alucard && !InPrologue();

    public static bool InPrologue() =>
        InGame && Character == PlayableCharacter.Alucard && (StageId & ~Stage.SecondCastle) == Stage.Prologue &&
        Progress.GetTimeAttack(TimeAttackEvent.DraculaDefeat) == 0 && Player.HpMax == 50;
}

public static class Entities
{
    public const int Count = Game.EntityCount;
    public const int StageStart = 64;
    public const int ServantStart = 0xD0;

    public static Entity At(int slot) => new(Game.EntitiesAddr + (uint)(slot * Entity.Stride));

    public static Entity Player => At(0);
    public static Entity Current => Game.CurrentEntity;

    public static Entity GetFree(int start, int end) => GameApi.GetFreeEntity(start, end);
    public static Entity GetFreeStage() => GameApi.GetFreeEntity(StageStart, Count);

    public static int SlotOf(Entity e) =>
        e.Addr < Game.EntitiesAddr ? -1 : (int)((e.Addr - Game.EntitiesAddr) / Entity.Stride);

    public static IEnumerable<Entity> All()
    {
        for (int i = 0; i < Count; i++)
        {
            var e = At(i);
            if (e.IsAlive) yield return e;
        }
    }

    public static IEnumerable<Entity> Enemies()
    {
        for (int i = 0; i < Count; i++)
        {
            var e = At(i);
            if (e.IsEnemy) yield return e;
        }
    }

    public static IEnumerable<Entity> Range(int start, int end)
    {
        for (int i = start; i < end && i < Count; i++)
        {
            var e = At(i);
            if (e.IsAlive) yield return e;
        }
    }

    public static Entity FindByEnemyId(ushort enemyId, int start = StageStart)
    {
        for (int i = start; i < Count; i++)
        {
            var e = At(i);
            if (e.IsAlive && e.EnemyId == enemyId) return e;
        }
        return new Entity(0);
    }

    public static Entity FindByEntityId(ushort entityId, int start = StageStart)
    {
        for (int i = start; i < Count; i++)
        {
            var e = At(i);
            if (e.IsAlive && e.EntityId == entityId) return e;
        }
        return new Entity(0);
    }

    public static IEnumerable<Entity> AllByEnemyId(ushort enemyId, int start = StageStart)
    {
        for (int i = start; i < Count; i++)
        {
            var e = At(i);
            if (e.IsAlive && e.EnemyId == enemyId) yield return e;
        }
    }

    public static Entity Spawn(byte[] data, int start = StageStart, int end = Count)
    {
        var e = GetFree(start, end);
        if (e.IsValid) e.Write(data);
        return e;
    }
}
