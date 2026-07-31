using System;
using System.Collections.Generic;
using RecompOne.Runtime.Memory;

namespace Sotn;

public enum EquipKind
{
    Hand = 0,
    Head = 1,
    Armor = 2,
    Cape = 3,
    Accessory = 4,
}

public static class Inventory
{
    static IMemory M => RecompOne.Runtime.Runtime.Mem!;
    static uint S => Game.StatusAddr;

    const uint RelicsOff = 0x000;
    const uint SpellsOff = 0x01E;
    const uint HandCountOff = 0x026;
    const uint BodyCountOff = 0x0CF;
    const uint HandOrderOff = 0x129;
    const uint BodyOrderOff = 0x1D2;
    const uint SaveNameOff = 0x22C;
    const uint SpellsLearntOff = 0x238;
    const uint SubWeaponOff = 0x298;
    const uint WornEquipOff = 0x29C;
    const uint AttackHandsOff = 0x2B8;
    const uint FamiliarStatsOff = 0x2E0;

    public const uint MenuNavAddr = 0x8003C9A8u;
    public const uint RelicCursorAddr = 0x8003C9ACu;
    public const uint EquipCategoryAddr = 0x8003C9B0u;
    public const uint HandCursorAddr = 0x8003C9B4u;
    public const uint EquipTypeCursorAddr = 0x8003C9B8u;

    public const int RelicCount = 30;
    public const int SpellCount = 8;
    public const int HandItemCount = 169;
    public const int BodyItemCount = 90;
    public const int WornEquipCount = 5;
    public const int FamiliarCount = 7;
    public const int SaveNameLength = 12;

    public const int ArmorNone = 0x00;
    public const int ArmorCount = 26;
    public const int HeadNone = 0x1A;
    public const int HeadCount = 22;
    public const int CapeNone = 0x30;
    public const int CapeCount = 9;
    public const int AccessoryNone = 0x39;
    public const int AccessoryCount = 32;

    //hnd
    public static int GetHandCount(int id) => M.ReadU8(S + HandCountOff + (uint)id);
    public static void SetHandCount(int id, int n) => M.WriteU8(S + HandCountOff + (uint)id, (byte)n);
    public static bool HasHandItem(int id) => GetHandCount(id) > 0;
    public static void AddHandItem(int id, int n = 1) => SetHandCount(id, System.Math.Clamp(GetHandCount(id) + n, 0, 255));
    public static void RemoveHandItem(int id, int n = 1) => AddHandItem(id, -n);

    public static int GetHandCount(HandItem item) => GetHandCount((int)item);
    public static void SetHandCount(HandItem item, int n) => SetHandCount((int)item, n);
    public static bool HasHandItem(HandItem item) => HasHandItem((int)item);
    public static void AddHandItem(HandItem item, int n = 1) => AddHandItem((int)item, n);
    public static void RemoveHandItem(HandItem item, int n = 1) => RemoveHandItem((int)item, n);

    //bdy
    public static int GetBodyCount(int id) => M.ReadU8(S + BodyCountOff + (uint)id);
    public static void SetBodyCount(int id, int n) => M.WriteU8(S + BodyCountOff + (uint)id, (byte)n);
    public static bool HasBodyItem(int id) => GetBodyCount(id) > 0;
    public static void AddBodyItem(int id, int n = 1) => SetBodyCount(id, System.Math.Clamp(GetBodyCount(id) + n, 0, 255));
    public static void RemoveBodyItem(int id, int n = 1) => AddBodyItem(id, -n);

    public static int GetBodyCount(BodyItem item) => GetBodyCount((int)item);
    public static void SetBodyCount(BodyItem item, int n) => SetBodyCount((int)item, n);
    public static bool HasBodyItem(BodyItem item) => HasBodyItem((int)item);
    public static void AddBodyItem(BodyItem item, int n = 1) => AddBodyItem((int)item, n);
    public static void RemoveBodyItem(BodyItem item, int n = 1) => RemoveBodyItem((int)item, n);

    //ordr
    public static int GetHandOrder(int slot) => M.ReadU8(S + HandOrderOff + (uint)slot);
    public static void SetHandOrder(int slot, int id) => M.WriteU8(S + HandOrderOff + (uint)slot, (byte)id);
    public static int GetBodyOrder(int slot) => M.ReadU8(S + BodyOrderOff + (uint)slot);
    public static void SetBodyOrder(int slot, int id) => M.WriteU8(S + BodyOrderOff + (uint)slot, (byte)id);

    public static EquipKind KindOf(BodyItem item) => KindOf((int)item);

    public static EquipKind KindOf(int bodyId) => bodyId switch
    {
        >= HeadNone and < CapeNone => EquipKind.Head,
        >= CapeNone and < AccessoryNone => EquipKind.Cape,
        >= AccessoryNone and < AccessoryNone + AccessoryCount - 1 => EquipKind.Accessory,
        _ => EquipKind.Armor,
    };

    static (int start, int count) BodyRegion(EquipKind kind) => kind switch
    {
        EquipKind.Head => (HeadNone, HeadCount),
        EquipKind.Cape => (CapeNone, CapeCount),
        EquipKind.Accessory => (AccessoryNone, AccessoryCount),
        _ => (ArmorNone, ArmorCount),
    };

    public static void GrantHandItem(HandItem item, int n = 1) => GrantHandItem((int)item, n);

    public static void GrantHandItem(int id, int n = 1)
    {
        bool wasEmpty = GetHandCount(id) == 0;
        AddHandItem(id, n);
        if (wasEmpty && GetHandCount(id) > 0) SortHandOrder(id);
    }

    public static void GrantBodyItem(BodyItem item, int n = 1) => GrantBodyItem((int)item, n);

    public static void GrantBodyItem(int id, int n = 1)
    {
        bool wasEmpty = GetBodyCount(id) == 0;
        AddBodyItem(id, n);
        if (wasEmpty && GetBodyCount(id) > 0) SortBodyOrder(id);
    }

    static void SortHandOrder(int id)
    {
        for (int i = 0; i < HandItemCount; i++)
        {
            if (GetHandCount(GetHandOrder(i)) != 0) continue;
            int slot = FindOrderSlot(GetHandOrder, 0, HandItemCount, id);
            if (slot < 0) return;
            int previous = GetHandOrder(i);
            SetHandOrder(i, id);
            SetHandOrder(slot, previous);
            return;
        }
    }

    static void SortBodyOrder(int id)
    {
        var (start, count) = BodyRegion(KindOf(id));
        for (int i = start; i < start + count; i++)
        {
            if (GetBodyCount(GetBodyOrder(i)) != 0) continue;
            int slot = FindOrderSlot(GetBodyOrder, start, count, id);
            if (slot < 0) return;
            int previous = GetBodyOrder(i);
            SetBodyOrder(i, id);
            SetBodyOrder(slot, previous);
            return;
        }
    }

    static int FindOrderSlot(Func<int, int> read, int start, int count, int id)
    {
        for (int i = start; i < start + count; i++)
            if (read(i) == id) return i;
        return -1;
    }

    public static void ClearInventory()
    {
        for (int i = 1; i < HandItemCount; i++) SetHandCount(i, 0);
        for (int i = 0; i < BodyItemCount; i++)
            if (i != ArmorNone && i != HeadNone && i != CapeNone && i != AccessoryNone) SetBodyCount(i, 0);
    }

    public static int SelectedCategory => (int)M.ReadU32(EquipCategoryAddr);
    public static int RelicCursor => (int)M.ReadU32(RelicCursorAddr);
    public static int HandCursor => (int)M.ReadU32(HandCursorAddr);
    public static int GetEquipTypeCursor(EquipKind kind) => (int)M.ReadU32(EquipTypeCursorAddr + (uint)(((int)kind - 1) * 4));

    public static Relic SelectedRelic
    {
        get
        {
            int cursor = RelicCursor;
            if (cursor > 22) cursor += 2;
            return (Relic)cursor;
        }
    }

    public static int SelectedHandItem => GetHandOrder(HandCursor);

    public static int GetSelectedBodyItem(EquipKind kind)
    {
        var (start, _) = BodyRegion(kind);
        return GetBodyOrder(start + GetEquipTypeCursor(kind));
    }

    //rlc
    public static byte GetRelic(int id) => M.ReadU8(S + RelicsOff + (uint)id);
    public static void SetRelic(int id, byte value) => M.WriteU8(S + RelicsOff + (uint)id, value);
    public static bool HasRelic(int id) => GetRelic(id) != 0;

    public static bool IsRelicActive(int id) => (GetRelic(id) & 2) != 0;

    public static byte GetRelic(Relic relic) => GetRelic((int)relic);
    public static bool HasRelic(Relic relic) => HasRelic((int)relic);
    public static bool IsRelicActive(Relic relic) => IsRelicActive((int)relic);
    public static void GiveRelic(Relic relic, bool on) => SetRelic((int)relic, (byte)(on ? 3 : 0));
    public static void TakeRelic(Relic relic) => SetRelic((int)relic, 0);

    public static bool IsFamiliarCard(Relic relic) => relic >= Relic.BatCard && relic <= Relic.Jp1;

    public static void GrantRelic(Relic relic, bool allowSpawn = false)
    {
        byte on = (byte)(allowSpawn ? 6 : 3);
        byte off = (byte)(allowSpawn ? 5 : 1);
        SetRelic((int)relic, IsFamiliarCard(relic) ? off : on);
    }

    //spll/
    public static byte GetSpell(int id) => M.ReadU8(S + SpellsOff + (uint)id);
    public static void SetSpell(int id, byte value) => M.WriteU8(S + SpellsOff + (uint)id, value);
    public static uint SpellsLearnt { get => M.ReadU32(S + SpellsLearntOff); set => M.WriteU32(S + SpellsLearntOff, value); }
    public static bool HasSpell(Spell spell) => (SpellsLearnt & (1u << (int)spell)) != 0;

    public static void SetSpellLearned(Spell spell, bool on)
    {
        uint mask = 1u << (int)spell;
        SpellsLearnt = on ? SpellsLearnt | mask : SpellsLearnt & ~mask;

        uint learnt = SpellsLearnt;
        int slot = 0;
        for (int id = 0; id < SpellCount; id++)
            if ((learnt & (1u << id)) != 0)
                SetSpell(slot++, (byte)(id | 0x80));
        for (; slot < SpellCount; slot++)
            SetSpell(slot, 0);
    } //fixed

    public static uint GetWornEquipment(int slot) => M.ReadU32(S + WornEquipOff + (uint)(slot * 4));
    public static void SetWornEquipment(int slot, uint id) => M.WriteU32(S + WornEquipOff + (uint)(slot * 4), id);
    public static uint GetWornEquipment(ItemSlot slot) => GetWornEquipment((int)slot);
    public static void SetWornEquipment(ItemSlot slot, uint id) => SetWornEquipment((int)slot, id);
    public static uint RightHand { get => M.ReadU32(S + AttackHandsOff); set => M.WriteU32(S + AttackHandsOff, value); }
    public static uint LeftHand { get => M.ReadU32(S + AttackHandsOff + 4); set => M.WriteU32(S + AttackHandsOff + 4, value); }
    public static uint SubWeapon { get => M.ReadU32(S + SubWeaponOff); set => M.WriteU32(S + SubWeaponOff, value); }
    public static Subweapon HeldSubweapon { get => (Subweapon)SubWeapon; set => SubWeapon = (uint)value; }

    public static uint Head { get => GetWornEquipment(ItemSlot.Head); set => SetWornEquipment(ItemSlot.Head, value); }
    public static uint Armor { get => GetWornEquipment(ItemSlot.Armor); set => SetWornEquipment(ItemSlot.Armor, value); }
    public static uint Cape { get => GetWornEquipment(ItemSlot.Cape); set => SetWornEquipment(ItemSlot.Cape, value); }
    public static uint Accessory1 { get => GetWornEquipment(ItemSlot.Accessory1); set => SetWornEquipment(ItemSlot.Accessory1, value); }
    public static uint Accessory2 { get => GetWornEquipment(ItemSlot.Accessory2); set => SetWornEquipment(ItemSlot.Accessory2, value); }

    public static int GetFamiliarExp(Relic card)
    {
        int index = (int)card - (int)Relic.BatCard;
        return index < 0 || index >= FamiliarCount ? 0 : (int)M.ReadU32(S + FamiliarStatsOff + (uint)(index * 12) + 4);
    }

    public static void SetFamiliarExp(Relic card, int exp)
    {
        int index = (int)card - (int)Relic.BatCard;
        if (index < 0 || index >= FamiliarCount) return;
        M.WriteU32(S + FamiliarStatsOff + (uint)(index * 12) + 4, (uint)exp);
    }

    public static string SaveName
    {
        get => Text.ReadRaw(S + SaveNameOff, SaveNameLength);
        set => Text.WriteRaw(S + SaveNameOff, value, SaveNameLength);
    }

    public static IEnumerable<HandItem> HeldHandItems()
    {
        for (int i = 1; i < HandItemCount; i++)
            if (GetHandCount(i) > 0) yield return (HandItem)i;
    }

    public static IEnumerable<BodyItem> HeldBodyItems()
    {
        for (int i = 0; i < BodyItemCount; i++)
            if (GetBodyCount(i) > 0) yield return (BodyItem)i;
    }

    public static IEnumerable<Relic> HeldRelics()
    {
        for (int i = 0; i < RelicCount; i++)
            if (HasRelic(i)) yield return (Relic)i;
    }
}
