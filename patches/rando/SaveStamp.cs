using RecompOne.Runtime.Memory;

namespace Recompiled;

public readonly struct SaveStamp : IEquatable<SaveStamp>
{
    public const int Size = 16;
    public const uint BlockSize = 0x2000;
    public const uint Offset = BlockSize - Size;

    const byte Magic0 = (byte)'R'; //stands for Randomized Save, there is no real need to do that but i like prefixing stuff
    const byte Magic1 = (byte)'S';

    const int FlagItems = 1 << 0;
    const int FlagDrops = 1 << 1;
    const int FlagRelics = 1 << 2;
    const int FlagStartingGear = 1 << 3;
    const int FlagRemoveDeath = 1 << 4;

    public readonly int Seed;
    public readonly bool Items;
    public readonly bool Drops;
    public readonly bool Relics;
    public readonly bool StartingGear;
    public readonly bool RemoveDeath;

    public SaveStamp(int seed, bool items, bool drops, bool relics, bool startingGear, bool removeDeath)
    {
        Seed = seed;
        Items = items;
        Drops = drops;
        Relics = relics;
        StartingGear = startingGear;
        RemoveDeath = removeDeath;
    }

    public static SaveStamp FromRandomizer() => new(
        Randomizer.SeedNumber,
        Randomizer.RandomizeItems,
        Randomizer.RandomizeDrops,
        Randomizer.RandomizeRelics,
        Randomizer.RandomizeStartingGear,
        Randomizer.RemoveDeathFromEntrance);

    public void ApplyToRandomizer()
    {
        Randomizer.SeedNumber = Seed;
        Randomizer.RandomizeItems = Items;
        Randomizer.RandomizeDrops = Drops;
        Randomizer.RandomizeRelics = Relics;
        Randomizer.RandomizeStartingGear = StartingGear;
        Randomizer.RemoveDeathFromEntrance = RemoveDeath;
    }

    public byte Flags
    {
        get
        {
            int flags = 0;
            if (Items) flags |= FlagItems;
            if (Drops) flags |= FlagDrops;
            if (Relics) flags |= FlagRelics;
            if (StartingGear) flags |= FlagStartingGear;
            if (RemoveDeath) flags |= FlagRemoveDeath;
            return (byte)flags;
        }
    }

    public static bool TryRead(IMemory m, uint block, out SaveStamp stamp)
    {
        stamp = default;
        if (block == 0) return false;

        uint at = block + Offset;
        if (m.ReadU8(at) != Magic0 || m.ReadU8(at + 1) != Magic1) return false;

        int seed = (int)m.ReadU32(at + 2);
        byte flags = m.ReadU8(at + 6);

        stamp = new SaveStamp(
            seed,
            (flags & FlagItems) != 0,
            (flags & FlagDrops) != 0,
            (flags & FlagRelics) != 0,
            (flags & FlagStartingGear) != 0,
            (flags & FlagRemoveDeath) != 0);
        return true;
    }

    public void Write(IMemory m, uint block)
    {
        if (block == 0) return;

        uint at = block + Offset;
        m.WriteU8(at, Magic0);
        m.WriteU8(at + 1, Magic1);
        m.WriteU32(at + 2, (uint)Seed);
        m.WriteU8(at + 6, Flags);
        for (uint i = 7; i < Size; i++) m.WriteU8(at + i, 0);
    }

    public static void Clear(IMemory m, uint block)
    {
        if (block == 0) return;

        uint at = block + Offset;
        for (uint i = 0; i < Size; i++) m.WriteU8(at + i, 0);
    }

    public bool Equals(SaveStamp other) => Seed == other.Seed && Flags == other.Flags;

    public override bool Equals(object? obj) => obj is SaveStamp other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(Seed, Flags);

    public override string ToString() => $"seed {Seed}, flags 0x{Flags:X2}";
}
