using System;
using RecompOne.Runtime.Memory;

namespace Sotn;

public static class Player
{
    public const uint StateAddr = 0x80072BD0u;
    public const uint TimersAddr = 0x80072F00u;
    public const uint DemoTimerAddr = 0x80072EFCu;
    public const uint StatusFlagsAddr = 0x80072F2Cu;
    public const uint VramFlagAddr = 0x80072F20u;
    public const uint WarpFlagAddr = 0x80072F3Cu;
    public const uint StepAddr = 0x80073404u;
    public const uint StepSubAddr = 0x80073406u;
    public const uint ContactDamageAddr = 0x80073418u;
    public const uint HealTriggerAddr = 0x80072F76u;
    public const uint HealAmountAddr = 0x80072F78u;
    public const uint DamageTakenAddr = 0x80072F7Au;
    public const uint DamagePaletteAddr = 0x80072F60u;
    public const uint EffectActivatorAddr = 0x80074B7Eu;
    public const uint EffectIndexAddr = 0x80074B89u;
    public const uint SubWeaponTimerAddr = 0x80074BD4u;
    public const uint FreezeInvincibilityAddr = 0x80097420u;
    public const uint KnockbackInvincibilityAddr = 0x8013B5E8u;
    public const uint AttackPotionTimerAddr = 0x8013982Du;
    public const uint DefencePotionTimerAddr = 0x80139829u;
    public const uint StrengthPotionTimerAddr = 0x80139838u;

    static IMemory M => RecompOne.Runtime.Runtime.Mem!;
    static uint S => Game.StatusAddr;

    public static PlayableCharacter Character => Game.Character;
    public static bool IsAlucard => Character == PlayableCharacter.Alucard;
    public static bool IsRichter => Character == PlayableCharacter.Richter;

    public static Entity Entity => new(Game.EntitiesAddr);

    public static int PosX { get => Entity.PosX; set { var e = Entity; e.PosX = value; } }
    public static int PosY { get => Entity.PosY; set { var e = Entity; e.PosY = value; } }
    public static int ScreenX => Game.PlayerScreenX;
    public static int ScreenY => Game.PlayerScreenY;
    public static int MapX => Game.RoomX;
    public static int MapY => Game.RoomY;

    public static bool FacingLeft { get => Entity.FacingLeft != 0; set { var e = Entity; e.FacingLeft = (ushort)(value ? 1 : 0); } }
    public static int VelocityX { get => Entity.VelocityX; set { var e = Entity; e.VelocityX = value; } }
    public static int VelocityY { get => Entity.VelocityY; set { var e = Entity; e.VelocityY = value; } }

    public static int Hp { get => (int)M.ReadU32(S + 0x23C); set => M.WriteU32(S + 0x23C, (uint)value); }
    public static int HpMax { get => (int)M.ReadU32(S + 0x240); set => M.WriteU32(S + 0x240, (uint)value); }
    public static int Hearts { get => (int)M.ReadU32(S + 0x244); set => M.WriteU32(S + 0x244, (uint)value); }
    public static int HeartsMax { get => (int)M.ReadU32(S + 0x248); set => M.WriteU32(S + 0x248, (uint)value); }
    public static int Mp { get => (int)M.ReadU32(S + 0x24C); set => M.WriteU32(S + 0x24C, (uint)value); }
    public static int MpMax { get => (int)M.ReadU32(S + 0x250); set => M.WriteU32(S + 0x250, (uint)value); }
    public static int Strength { get => (int)M.ReadU32(S + 0x254); set => M.WriteU32(S + 0x254, (uint)value); }
    public static int Constitution { get => (int)M.ReadU32(S + 0x258); set => M.WriteU32(S + 0x258, (uint)value); }
    public static int Intelligence { get => (int)M.ReadU32(S + 0x25C); set => M.WriteU32(S + 0x25C, (uint)value); }
    public static int Luck { get => (int)M.ReadU32(S + 0x260); set => M.WriteU32(S + 0x260, (uint)value); }
    public static int StrengthEquip => (int)M.ReadU32(S + 0x264);
    public static int ConstitutionEquip => (int)M.ReadU32(S + 0x268);
    public static int IntelligenceEquip => (int)M.ReadU32(S + 0x26C);
    public static int LuckEquip => (int)M.ReadU32(S + 0x270);
    public static int StrengthTotal => (int)M.ReadU32(S + 0x274);
    public static int ConstitutionTotal => (int)M.ReadU32(S + 0x278);
    public static int IntelligenceTotal => (int)M.ReadU32(S + 0x27C);
    public static int LuckTotal => (int)M.ReadU32(S + 0x280);
    public static int Level { get => (int)M.ReadU32(S + 0x284); set => M.WriteU32(S + 0x284, (uint)value); }
    public static int Exp { get => (int)M.ReadU32(S + 0x288); set => M.WriteU32(S + 0x288, (uint)value); }
    public static int Gold { get => (int)M.ReadU32(S + 0x28C); set => M.WriteU32(S + 0x28C, (uint)value); }
    public static int KillCount { get => (int)M.ReadU32(S + 0x290); set => M.WriteU32(S + 0x290, (uint)value); }
    public static int SubWeapon { get => (int)M.ReadU32(S + 0x298); set => M.WriteU32(S + 0x298, (uint)value); }
    public static int Defense { get => (int)M.ReadU32(S + 0x2C0); set => M.WriteU32(S + 0x2C0, (uint)value); }
    public static ushort ElementsWeakTo { get => M.ReadU16(S + 0x2C4); set => M.WriteU16(S + 0x2C4, value); }
    public static ushort ElementsResist { get => M.ReadU16(S + 0x2C6); set => M.WriteU16(S + 0x2C6, value); }
    public static ushort ElementsImmune { get => M.ReadU16(S + 0x2C8); set => M.WriteU16(S + 0x2C8, value); }
    public static ushort ElementsAbsorb { get => M.ReadU16(S + 0x2CA); set => M.WriteU16(S + 0x2CA, value); }

    public static PlayerStatus Status { get => (PlayerStatus)M.ReadU32(StatusFlagsAddr); set => M.WriteU32(StatusFlagsAddr, (uint)value); }
    public static bool HasStatus(PlayerStatus flag) => (Status & flag) != 0;
    public static uint VramFlag => M.ReadU32(VramFlagAddr);
    public static uint WarpFlag { get => M.ReadU32(WarpFlagAddr); set => M.WriteU32(WarpFlagAddr, value); }

    public static PlayerStep Step { get => (PlayerStep)M.ReadU16(StepAddr); set => M.WriteU16(StepAddr, (ushort)value); }
    public static ushort StepSub { get => M.ReadU16(StepSubAddr); set => M.WriteU16(StepSubAddr, value); }

    public static ushort ContactDamage { get => M.ReadU16(ContactDamageAddr); set => M.WriteU16(ContactDamageAddr, value); }
    public static ushort DamageTaken => M.ReadU16(DamageTakenAddr);
    public static ushort DamagePalette { get => M.ReadU16(DamagePaletteAddr); set => M.WriteU16(DamagePaletteAddr, value); }

    public static int PaletteId { get => Entity.PaletteId; set { var e = Entity; e.PaletteId = value; } }
    public static void SetPalette(int id) { var e = Entity; e.SetPalette(id); }
    public static ushort[] ReadPalette() => Entity.ReadPalette();
    public static void WritePalette(ReadOnlySpan<ushort> colors) => Entity.WritePalette(colors);
    public static void TintPalette(float r, float g, float b) => Entity.TintPalette(r, g, b);

    public static short GetTimer(AluTimer timer) => (short)M.ReadU16(TimersAddr + (uint)((int)timer * 2));
    public static void SetTimer(AluTimer timer, short value) => M.WriteU16(TimersAddr + (uint)((int)timer * 2), (ushort)value);

    public static short PoisonTimer { get => GetTimer(AluTimer.Poison); set => SetTimer(AluTimer.Poison, value); }
    public static short CurseTimer { get => GetTimer(AluTimer.Curse); set => SetTimer(AluTimer.Curse, value); }
    public static short HitEffectTimer { get => GetTimer(AluTimer.HitEffect); set => SetTimer(AluTimer.HitEffect, value); }
    public static short DarkMetamorphosisTimer { get => GetTimer(AluTimer.DarkMetamorphosis); set => SetTimer(AluTimer.DarkMetamorphosis, value); }
    public static short InvincibilityTimer { get => GetTimer(AluTimer.Invincible); set => SetTimer(AluTimer.Invincible, value); }
    public static short PotionInvincibilityTimer { get => GetTimer(AluTimer.InvincibleConsumables); set => SetTimer(AluTimer.InvincibleConsumables, value); }
    public static short UseSpellTimer { get => GetTimer(AluTimer.UseSpell); set => SetTimer(AluTimer.UseSpell, value); }

    public static byte SubWeaponTimer { get => M.ReadU8(SubWeaponTimerAddr); set => M.WriteU8(SubWeaponTimerAddr, value); }
    public static byte FreezeInvincibilityTimer { get => M.ReadU8(FreezeInvincibilityAddr); set => M.WriteU8(FreezeInvincibilityAddr, value); }
    public static byte KnockbackInvincibilityTimer { get => M.ReadU8(KnockbackInvincibilityAddr); set => M.WriteU8(KnockbackInvincibilityAddr, value); }
    public static byte AttackPotionTimer { get => M.ReadU8(AttackPotionTimerAddr); set => M.WriteU8(AttackPotionTimerAddr, value); }
    public static byte DefencePotionTimer { get => M.ReadU8(DefencePotionTimerAddr); set => M.WriteU8(DefencePotionTimerAddr, value); }
    public static byte StrengthPotionTimer { get => M.ReadU8(StrengthPotionTimerAddr); set => M.WriteU8(StrengthPotionTimerAddr, value); }

    public static int DemoTimer { get => (int)M.ReadU32(DemoTimerAddr); set => M.WriteU32(DemoTimerAddr, (uint)value); }
    public static bool HasControl => DemoTimer == 0;

    public static bool IsInvincible =>
        InvincibilityTimer > 0 || PotionInvincibilityTimer > 0 ||
        KnockbackInvincibilityTimer > 0 || FreezeInvincibilityTimer > 0;

    public static bool HasHitbox => Entity.HitboxWidth > 0 && Entity.HitboxHeight > 0;

    public static bool EffectActive => M.ReadU8(EffectActivatorAddr) != 0;

    public static void TriggerEffect(uint effect) => M.WriteU8(EffectActivatorAddr, (byte)effect);

    public static void ActivatePotion(Potion potion)
    {
        M.WriteU8(EffectActivatorAddr, (byte)Effect.Potion);
        M.WriteU8(EffectIndexAddr, (byte)potion);
    }

    public static void ActivateStopwatch() => TriggerEffect(Effect.Stopwatch);
    public static void ForceLibraryCard() => TriggerEffect(Effect.LibraryCard);

    public static void Heal(int amount)
    {
        M.WriteU16(HealAmountAddr, (ushort)amount);
        M.WriteU8(HealTriggerAddr, 1);
    }

    public static void FullHeal()
    {
        Hp = HpMax;
        Mp = MpMax;
        Hearts = HeartsMax;
    }

    public static void InstantDeath()
    {
        Step = PlayerStep.Death;
        StepSub = 0;
    }
}
