using System;

namespace Sotn;

[Flags]
public enum PlayerStatus : uint
{
    BatForm = 0x1,
    MistForm = 0x2,
    WolfForm = 0x4,
    Transform = BatForm | MistForm | WolfForm,
    Unk8 = 0x8,
    Unk10 = 0x10,
    Crouch = 0x20,
    Unk40 = 0x40,
    Stone = 0x80,
    Invincible = 0x100,
    Unk200 = 0x200,
    Unk400 = 0x400,
    SubWeapon = 0x800,
    SpellCast = 0x1000,
    Unk2000 = 0x2000,
    Poison = 0x4000,
    Curse = 0x8000,
    Unk10000 = 0x10000,
    Unk20000 = 0x20000,
    Dead = 0x40000,
    AxeArmor = 0x1000000,
    AbsorbBlood = 0x2000000,
    NoAfterImage = 0x8000000,
}

public enum PlayerStep
{
    Standing = 0x00,
    Walking = 0x01,
    Crouching = 0x02,
    Aerial = 0x04,
    Bat = 0x05,
    Mist = 0x07,
    UntransformBat = 0x09,
    Poison = 0x0A,
    Stone = 0x0B,
    UntransformMist = 0x0E,
    Death = 0x10,
    Wolf = 0x18,
    UntransformWolf = 0x19,
    DarkMetamorphosis = 0x20,
    SummonSpirit = 0x21,
    Hellfire = 0x22,
    TetraSpirit = 0x23,
    Unk30 = 0x30,
}

public enum AluTimer
{
    Poison = 0,
    Curse = 1,
    HitEffect = 2,
    Unk3 = 3,
    Unk4 = 4,
    Unk5 = 5,
    Unk6 = 6,
    Unk7 = 7,
    Unk8 = 8,
    Unk9 = 9,
    UseSubWeapon = 10,
    DarkMetamorphosis = 11,
    UseSpell = 12,
    Invincible = 13,
    InvincibleConsumables = 14,
    Unk15 = 15,
}

public static class Effect
{
    public const uint Invincibility = 0x0B;
    public const uint LevelUp = 0x15;
    public const uint Dissolve = 0x20;
    public const uint Potion = 0x27;
    public const uint Stopwatch = 0x29;
    public const uint Bible = 0x2B;
    public const uint AutoSummonSpirit = 0x3D;
    public const uint SummonSpirit = 0x3F;
    public const uint LibraryCard = 0x41;
}
