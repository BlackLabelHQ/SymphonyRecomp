using System;

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
