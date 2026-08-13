using System;
using System.Collections.Generic;
using RecompOne.Runtime.Memory;

namespace Sotn;

public enum Stage
{
    MarbleGallery = 0x00,
    OuterWall = 0x01,
    LongLibrary = 0x02,
    Catacombs = 0x03,
    OlroxsQuarters = 0x04,
    AbandonedMine = 0x05,
    RoyalChapel = 0x06,
    CastleEntrance = 0x07,
    CenterCube = 0x08,
    UndergroundCaverns = 0x09,
    Colosseum = 0x0A,
    CastleKeep = 0x0B,
    AlchemyLaboratory = 0x0C,
    ClockTower = 0x0D,
    Warp = 0x0E,
    OuterWallAlt = 0x0F,
    MarbleGalleryAlt = 0x10,
    Nightmare = 0x12,
    AlchemyLaboratoryDemo = 0x13,
    ClockTowerDemo = 0x14,
    LongLibraryDemo = 0x15,
    Cerberus = 0x16,
    ClockRoomCutscene = 0x17,
    RichterFight = 0x18,
    Hippogryph = 0x19,
    Doppleganger10 = 0x1A,
    Scylla = 0x1B,
    MinotaurWerewolf = 0x1C,
    Granfaloon = 0x1D,
    Olrox = 0x1E,
    Prologue = 0x1F,

    SecondCastle = 0x20,

    BlackMarbleGallery = 0x20,
    ReverseOuterWall = 0x21,
    ForbiddenLibrary = 0x22,
    FloatingCatacombs = 0x23,
    DeathWingsLair = 0x24,
    Cave = 0x25,
    AntiChapel = 0x26,
    ReverseEntrance = 0x27,
    ReverseCenterCube = 0x28,
    ReverseCaverns = 0x29,
    ReverseColosseum = 0x2A,
    ReverseCastleKeep = 0x2B,
    NecromancyLaboratory = 0x2C,
    ReverseClockTower = 0x2D,
    ReverseWarp = 0x2E,

    ReverseClockTowerDemo = 0x35,
    Galamoth = 0x36,
    Akmodan = 0x37,
    ShaftDracula = 0x38,
    Doppleganger40 = 0x39,
    Creature = 0x3A,
    Medusa = 0x3B,
    Death = 0x3C,
    Beelzebub = 0x3D,
    Trio = 0x3E,
    DarkwingBat = 0x3F,

    Debug = 0x40,
    CastleEntranceFirstVisit = 0x41,
    IwaLoad = 0x42,
    IgaLoad = 0x43,
    HagiLoad = 0x44,
    TitleScreen = 0x45,
    Test1 = 0x46,
    Test2 = 0x47,
    Test3 = 0x48,
    Test4 = 0x49,
    Test5 = 0x4A,
    CastleKeepAlt = 0x4B,

    EuWarning = 0x70,
    Ending = 0xFE,
    MemoryCard = 0xFF,
}

public static class Stages
{
    public const int TilesetPalette = 0x000;
    public const int SharedPalette = 0x100;
    public const int EntityPalette = 0x200;
    public const int PaletteCount = 0x100;

    public const uint GameStepAddr = 0x80073060u;
    public const uint RoomXAddr = 0x800730B0u;
    public const uint RoomYAddr = 0x800730B4u;
    public const uint SetNextRoomToLoadAddr = 0x800F0BC0u;
    public const uint PrepareStageAddr = 0x800F223Cu;
    public const uint OverlayHeaderAddr = 0x80180000u;

    public const uint StageEntryModeAddr = 0x8003C730u;
    public const uint StageEntryFromRoom = 2u;

    public const uint RoomEntryXAddr = 0x801375C0u;
    public const uint RoomEntryYAddr = 0x801375C4u;


    public const uint EngineStepAddr = 0x8003C9A4u;
    public const uint TransitionStepAddr = 0x800978F8u;
    public const uint EngineStepRoomTransition = 3u;

    public const uint CdStepAddr = 0x8006C398u;
    public const uint LoadFileAddr = 0x8006BAFCu;
    public const uint LoadOvlIdxAddr = 0x80097918u;
    public const uint IsUsingCdAddr = 0x8006C3B0u;
    public const uint CdStepLoadInit = 1u;
    public const uint CdFileStageChr = 3u;

    static IMemory M => RecompOne.Runtime.Runtime.Mem!;

    public static PlayStep Step
    {
        get => (PlayStep)M.ReadU32(GameStepAddr);
        set => M.WriteU32(GameStepAddr, (uint)value);
    }


    public static int RoomX
    {
        get => (int)M.ReadU32(RoomXAddr);
        set => M.WriteU32(RoomXAddr, (uint)value);
    }

    public static int RoomY
    {
        get => (int)M.ReadU32(RoomYAddr);
        set => M.WriteU32(RoomYAddr, (uint)value);
    }

    public static List<(int Left, int Top, int Right, int Bottom)> Rooms()
    {
        var list = new List<(int, int, int, int)>();
        uint table = M.ReadU32(OverlayHeaderAddr + 0x10);
        if (table == 0) return list;

        for (int i = 0; i < 256; i++)
        {
            uint e = table + (uint)(i * 8);
            int left = M.ReadU8(e);
            if (left == 0x40) break;
            list.Add((left, M.ReadU8(e + 1), M.ReadU8(e + 2), M.ReadU8(e + 3)));
        }
        return list;
    }
    public static List<(int Left, int Top, int Right, int Bottom)> Rooms(Stage stage)
    {
        if (stage == Current)
        {
            var live = Rooms();
            if (live.Count > 0) return live;
        }

        if (_discRooms.TryGetValue(stage, out var cached)) return cached;

        var list = new List<(int, int, int, int)>();
        var file = DiscFile(stage);
        if (file != null)
        {
            try
            {
                var ovl = RecompOne.Runtime.Runtime.Cd?.Fs.ReadFile(file);
                if (ovl != null) ReadRoomTable(ovl, list);
            }
            catch { }
        }

        _discRooms[stage] = list;
        return list;
    }

    static readonly Dictionary<Stage, List<(int, int, int, int)>> _discRooms = new();

    static void ReadRoomTable(byte[] ovl, List<(int, int, int, int)> list)
    {
        if (ovl.Length < 0x14) return;
        uint table = (uint)(ovl[0x10] | (ovl[0x11] << 8) | (ovl[0x12] << 16) | (ovl[0x13] << 24));
        if (table < OverlayHeaderAddr) return;

        long off = table - OverlayHeaderAddr;
        for (int i = 0; i < 256 && off + 8 <= ovl.Length; i++, off += 8)
        {
            int left = ovl[off];
            if (left == 0x40) break;
            list.Add((left, ovl[off + 1], ovl[off + 2], ovl[off + 3]));
        }
    }

    public static string? DiscFile(Stage stage) =>StageFiles.TryGetValue(stage, out var f) ? f : null;

    static readonly Dictionary<Stage, string> StageFiles = BuildStageFiles();

    static Dictionary<Stage, string> BuildStageFiles()
    {
        var st = new (Stage Id, string Name)[]
        {
            (Stage.MarbleGallery, "NO0"), (Stage.OuterWall, "NO1"), (Stage.LongLibrary, "LIB"),
            (Stage.Catacombs, "CAT"), (Stage.OlroxsQuarters, "NO2"), (Stage.AbandonedMine, "CHI"),
            (Stage.RoyalChapel, "DAI"), (Stage.CastleEntrance, "NP3"), (Stage.CenterCube, "CEN"),
            (Stage.UndergroundCaverns, "NO4"), (Stage.Colosseum, "ARE"), (Stage.CastleKeep, "TOP"),
            (Stage.AlchemyLaboratory, "NZ0"), (Stage.ClockTower, "NZ1"), (Stage.Warp, "WRP"),
            (Stage.OuterWallAlt, "NO1"), (Stage.MarbleGalleryAlt, "NO0"), (Stage.Nightmare, "DRE"),
            (Stage.AlchemyLaboratoryDemo, "NZ0"), (Stage.ClockTowerDemo, "NZ1"),
            (Stage.LongLibraryDemo, "LIB"), (Stage.Prologue, "ST0"),
            (Stage.BlackMarbleGallery, "RNO0"), (Stage.ReverseOuterWall, "RNO1"),
            (Stage.ForbiddenLibrary, "RLIB"), (Stage.FloatingCatacombs, "RCAT"),
            (Stage.DeathWingsLair, "RNO2"), (Stage.Cave, "RCHI"), (Stage.AntiChapel, "RDAI"),
            (Stage.ReverseEntrance, "RNO3"), (Stage.ReverseCenterCube, "RCEN"),
            (Stage.ReverseCaverns, "RNO4"), (Stage.ReverseColosseum, "RARE"),
            (Stage.ReverseCastleKeep, "RTOP"), (Stage.NecromancyLaboratory, "RNZ0"),
            (Stage.ReverseClockTower, "RNZ1"), (Stage.ReverseWarp, "RWRP"),
            (Stage.ReverseClockTowerDemo, "RNZ1"), (Stage.Debug, "MAD"),
            (Stage.CastleEntranceFirstVisit, "NO3"), (Stage.TitleScreen, "SEL"),
            (Stage.Test1, "TE1"), (Stage.Test2, "TE2"), (Stage.Test3, "TE3"),
            (Stage.Test4, "TE4"),
        };

        var boss = new (Stage Id, string Name)[]
        {
            (Stage.Cerberus, "BO7"), (Stage.ClockRoomCutscene, "MAR"), (Stage.RichterFight, "BO6"),
            (Stage.Hippogryph, "BO5"), (Stage.Doppleganger10, "BO4"), (Stage.Scylla, "BO3"),
            (Stage.MinotaurWerewolf, "BO2"), (Stage.Granfaloon, "BO1"), (Stage.Olrox, "BO0"),
            (Stage.Galamoth, "RBO8"), (Stage.Akmodan, "RBO7"), (Stage.ShaftDracula, "RBO6"),
            (Stage.Doppleganger40, "RBO5"), (Stage.Creature, "RBO4"), (Stage.Medusa, "RBO3"),
            (Stage.Death, "RBO2"), (Stage.Beelzebub, "RBO1"), (Stage.Trio, "RBO0"),
        };

        var map = new Dictionary<Stage, string>();
        foreach (var (id, name) in st) map[id] = $"ST/{name}/{name}.BIN";
        foreach (var (id, name) in boss) map[id] = $"BOSS/{name}/{name}.BIN";
        return map;
    }

    public static bool LoadRoom(int roomX, int roomY, int offsetX = 128, int offsetY = 128)
    {
        if (GameApi.Call(SetNextRoomToLoadAddr, (uint)roomX, (uint)roomY) == 0) return false;

        M.WriteU32(RoomEntryXAddr, (uint)offsetX);
        M.WriteU32(RoomEntryYAddr, (uint)offsetY);
        M.WriteU32(TransitionStepAddr, 0);
        M.WriteU32(EngineStepAddr, EngineStepRoomTransition);
        return true;
    }

    static bool _pending;
    static bool _ticking;
    static bool _needChr;
    static Stage _pendingStage;
    static int _pendingX, _pendingY, _pendingOffsetX, _pendingOffsetY;

    public static bool RequestStageGfx(Stage stage)
    {
        if (M.ReadU32(IsUsingCdAddr) != 0 || M.ReadU32(CdStepAddr) != 0) return false;

        M.WriteU32(LoadOvlIdxAddr, (uint)stage);
        M.WriteU32(LoadFileAddr, CdFileStageChr);
        M.WriteU32(CdStepAddr, CdStepLoadInit);
        return true;
    }

    public static void Load(Stage stage, int roomX, int roomY, int offsetX = 128, int offsetY = 128)
    {
        if (!_ticking)
        {
            RecompOne.Runtime.Events.Event.AddListener<RecompOne.Runtime.Events.VSyncEvent>(_ => Tick());
            _ticking = true;
        }

        _pending = true;
        _pendingStage = stage;
        _pendingX = roomX;
        _pendingY = roomY;
        _pendingOffsetX = offsetX;
        _pendingOffsetY = offsetY;

        RoomX = roomX;
        RoomY = roomY;
        M.WriteU32(StageEntryModeAddr, StageEntryFromRoom);
        M.WriteU32(Game.StageIdAddr, (uint)stage);
        GameApi.Call(PrepareStageAddr);
        Step = PlayStep.PrepareNextStage;

        _needChr = !RequestStageGfx(stage);
    }

    public static bool LoadPending => _pending;

    public static void Tick()
    {
        if (!_pending) return;

        if (_needChr)
        {
            if (Step > PlayStep.LoadStageChr) _needChr = false;
            else if (RequestStageGfx(_pendingStage)) _needChr = false;
        }

        if (!Game.Available || Game.IsLoading) return;
        if (Current != _pendingStage) return;
        if (Step != PlayStep.Default) return;

        _pending = false;
        LoadRoom(_pendingX, _pendingY, _pendingOffsetX, _pendingOffsetY);
    }

    public static Stage Current => Game.StageId;
    public static bool SecondCastle => Game.SecondCastle;
    public static int Area => Game.Area;
    public static int Room => Game.Room;

    public static ushort[] ReadPalette(int index) => Palette.Read(TilesetPalette + index);
    public static void WritePalette(int index, ReadOnlySpan<ushort> colors) => Palette.Write(TilesetPalette + index, colors);
    public static void TintPalette(int index, float r, float g, float b) => Palette.Tint(TilesetPalette + index, r, g, b);

    public static void Tint(float r, float g, float b)
    {
        for (int i = 0; i < PaletteCount; i++) Palette.Tint(TilesetPalette + i, r, g, b);
    }

    public static void Shade(float r, float g, float b) => Palette.ShadeBlock(TilesetPalette, r, g, b);

    public static void ShadePalette(int index, float r, float g, float b) => Palette.Shade(TilesetPalette + index, r, g, b);

    public static void Restore() //shold rename this to shadeLevel RestoreLevel?
    {
        Palette.RestoreBlock(TilesetPalette);
    }

    public static void WriteAllPalettes(ushort color, bool keepTransparent = true)
    {
        Palette.FillBlock(TilesetPalette, color, keepTransparent);
    }

    public static void WriteAllPalettes(int r, int g, int b, bool keepTransparent = true) => WriteAllPalettes(Palette.Rgb(r, g, b), keepTransparent);

    //entities in this stage
    public static ushort[] ReadEntityPalette(int index) => Palette.Read(EntityPalette + index);
    public static void WriteEntityPalette(int index, ReadOnlySpan<ushort> colors) => Palette.Write(EntityPalette + index, colors);
    public static void TintEntityPalette(int index, float r, float g, float b) => Palette.Tint(EntityPalette + index, r, g, b);
    public static void ShadeEntityPalette(int index, float r, float g, float b) => Palette.Shade(EntityPalette + index, r, g, b);

    public static void TintEntities(float r, float g, float b)
    {
        for (int i = 0; i < PaletteCount; i++) Palette.Tint(EntityPalette + i, r, g, b);
    }

    public static void ShadeEntities(float r, float g, float b) => Palette.ShadeBlock(EntityPalette, r, g, b);

    public static void RestoreEntities()
    {
        Palette.RestoreBlock(EntityPalette);
    }

    public static void WriteAllEntityPalettes(ushort color, bool keepTransparent = true)
    {
        Palette.FillBlock(EntityPalette, color, keepTransparent);
    }

    public static void WriteAllEntityPalettes(int r, int g, int b, bool keepTransparent = true) =>WriteAllEntityPalettes(Palette.Rgb(r, g, b), keepTransparent);

    public static void Hide() => WriteAllPalettes(0x0000, false);

    public static void HideEntities() => WriteAllEntityPalettes(0x0000, false);
}
