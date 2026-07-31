using System;
using RecompOne.Runtime.Memory;

namespace Sotn;

[Flags]
public enum EntityFlags : uint
{
    Unk10 = 0x10,
    Unk20 = 0x20,
    Unk40 = 0x40,
    Unk80 = 0x80,
    Dead = 0x100,
    Unk200 = 0x200,
    Unk400 = 0x400,
    Unk800 = 0x800,
    Unk1000 = 0x1000,
    Unk2000 = 0x2000,
    Unk4000 = 0x4000,
    Unk8000 = 0x8000,
    Unk10000 = 0x10000,
    Unk20000 = 0x20000,
    PosPlayerLocked = 0x40000,
    Unk80000 = 0x80000,
    Unk100000 = 0x100000,
    Unk200000 = 0x200000,
    SuppressStun = 0x400000,
    HasPrims = 0x800000,
    NotAnEnemy = 0x01000000,
    Unk2000000 = 0x02000000,
    KeepAliveOffCamera = 0x04000000,
    PosCameraLocked = 0x08000000,
    Unk10000000 = 0x10000000,
    Unk20000000 = 0x20000000,
    DestroyIfBarelyOutOfCamera = 0x40000000,
    DestroyIfOutOfCamera = 0x80000000,
}

public enum BlendMode
{
    None = 0x00,
    Transparent = 0x10,
    Add = 0x20,
    Sub = 0x40,
    Quarter = 0x60,
}

public sealed class Entity
{
    public const int Stride = 0xBC;
    public const int ExtOffset = 0x7C;
    const int FlagDead = 0x100;
    const int FlagHasPrims = 0x800000;
    const int FlagNotAnEnemy = 0x01000000;

    public readonly uint Addr;
    public Entity(uint addr) => Addr = addr;

    static IMemory M => RecompOne.Runtime.Runtime.Mem!;

    public bool IsValid => Addr != 0;

    public int PosXRaw { get => (int)M.ReadU32(Addr + 0x00); set => M.WriteU32(Addr + 0x00, (uint)value); }
    public int PosYRaw { get => (int)M.ReadU32(Addr + 0x04); set => M.WriteU32(Addr + 0x04, (uint)value); }
    public int PosX { get => PosXRaw >> 16; set => PosXRaw = value << 16; }
    public int PosY { get => PosYRaw >> 16; set => PosYRaw = value << 16; }
    public int VelocityX { get => (int)M.ReadU32(Addr + 0x08); set => M.WriteU32(Addr + 0x08, (uint)value); }
    public int VelocityY { get => (int)M.ReadU32(Addr + 0x0C); set => M.WriteU32(Addr + 0x0C, (uint)value); }
    public short HitboxOffX { get => (short)M.ReadU16(Addr + 0x10); set => M.WriteU16(Addr + 0x10, (ushort)value); }
    public short HitboxOffY { get => (short)M.ReadU16(Addr + 0x12); set => M.WriteU16(Addr + 0x12, (ushort)value); }
    public ushort FacingLeft { get => M.ReadU16(Addr + 0x14); set => M.WriteU16(Addr + 0x14, value); }
    public ushort Palette { get => M.ReadU16(Addr + 0x16); set => M.WriteU16(Addr + 0x16, value); }
    public byte BlendMode { get => M.ReadU8(Addr + 0x18); set => M.WriteU8(Addr + 0x18, value); }
    public byte DrawFlags { get => M.ReadU8(Addr + 0x19); set => M.WriteU8(Addr + 0x19, value); }
    public short ScaleX { get => (short)M.ReadU16(Addr + 0x1A); set => M.WriteU16(Addr + 0x1A, (ushort)value); }
    public short ScaleY { get => (short)M.ReadU16(Addr + 0x1C); set => M.WriteU16(Addr + 0x1C, (ushort)value); }
    public short Rotate { get => (short)M.ReadU16(Addr + 0x1E); set => M.WriteU16(Addr + 0x1E, (ushort)value); }
    public short RotPivotX { get => (short)M.ReadU16(Addr + 0x20); set => M.WriteU16(Addr + 0x20, (ushort)value); }
    public short RotPivotY { get => (short)M.ReadU16(Addr + 0x22); set => M.WriteU16(Addr + 0x22, (ushort)value); }
    public ushort ZPriority { get => M.ReadU16(Addr + 0x24); set => M.WriteU16(Addr + 0x24, value); }
    public ushort EntityId => M.ReadU16(Addr + 0x26);
    public uint Update { get => M.ReadU32(Addr + 0x28); set => M.WriteU32(Addr + 0x28, value); }
    public ushort Step { get => M.ReadU16(Addr + 0x2C); set => M.WriteU16(Addr + 0x2C, value); }
    public ushort StepSub { get => M.ReadU16(Addr + 0x2E); set => M.WriteU16(Addr + 0x2E, value); }
    public ushort Params { get => M.ReadU16(Addr + 0x30); set => M.WriteU16(Addr + 0x30, value); }
    public ushort RoomIndex => M.ReadU16(Addr + 0x32);
    public int Flags { get => (int)M.ReadU32(Addr + 0x34); set => M.WriteU32(Addr + 0x34, (uint)value); }
    public ushort EnemyId => M.ReadU16(Addr + 0x3A);
    public ushort HitboxState { get => M.ReadU16(Addr + 0x3C); set => M.WriteU16(Addr + 0x3C, value); }
    public short HitPoints { get => (short)M.ReadU16(Addr + 0x3E); set => M.WriteU16(Addr + 0x3E, (ushort)value); }
    public short Attack { get => (short)M.ReadU16(Addr + 0x40); set => M.WriteU16(Addr + 0x40, (ushort)value); }
    public ushort AttackElement { get => M.ReadU16(Addr + 0x42); set => M.WriteU16(Addr + 0x42, value); }
    public ushort HitParams { get => M.ReadU16(Addr + 0x44); set => M.WriteU16(Addr + 0x44, value); }
    public byte HitboxWidth { get => M.ReadU8(Addr + 0x46); set => M.WriteU8(Addr + 0x46, value); }
    public byte HitboxHeight { get => M.ReadU8(Addr + 0x47); set => M.WriteU8(Addr + 0x47, value); }
    public byte HitFlags { get => M.ReadU8(Addr + 0x48); set => M.WriteU8(Addr + 0x48, value); }
    public byte NFramesInvincibility { get => M.ReadU8(Addr + 0x49); set => M.WriteU8(Addr + 0x49, value); }
    public uint Anim { get => M.ReadU32(Addr + 0x4C); set => M.WriteU32(Addr + 0x4C, value); }
    public ushort Pose { get => M.ReadU16(Addr + 0x50); set => M.WriteU16(Addr + 0x50, value); }
    public short PoseTimer { get => (short)M.ReadU16(Addr + 0x52); set => M.WriteU16(Addr + 0x52, (ushort)value); }
    public short AnimSet { get => (short)M.ReadU16(Addr + 0x54); set => M.WriteU16(Addr + 0x54, (ushort)value); }
    public short AnimCurFrame { get => (short)M.ReadU16(Addr + 0x56); set => M.WriteU16(Addr + 0x56, (ushort)value); }
    public short StunFrames { get => (short)M.ReadU16(Addr + 0x58); set => M.WriteU16(Addr + 0x58, (ushort)value); }
    public Entity Parent => new(M.ReadU32(Addr + 0x5C));
    public Entity NextPart => new(M.ReadU32(Addr + 0x60));
    public int PrimIndex { get => (int)M.ReadU32(Addr + 0x64); set => M.WriteU32(Addr + 0x64, (uint)value); }
    public ushort HitEffect { get => M.ReadU16(Addr + 0x6A); set => M.WriteU16(Addr + 0x6A, value); }
    public byte Opacity { get => M.ReadU8(Addr + 0x6C); set => M.WriteU8(Addr + 0x6C, value); }

    public EntityFlags FlagBits { get => (EntityFlags)M.ReadU32(Addr + 0x34); set => M.WriteU32(Addr + 0x34, (uint)value); }
    public bool HasFlag(EntityFlags flag) => (FlagBits & flag) != 0;
    public void SetFlag(EntityFlags flag, bool on) => FlagBits = on ? FlagBits | flag : FlagBits & ~flag;

    public byte ExtU8(int offset) => M.ReadU8(Addr + (uint)(ExtOffset + offset));
    public ushort ExtU16(int offset) => M.ReadU16(Addr + (uint)(ExtOffset + offset));
    public uint ExtU32(int offset) => M.ReadU32(Addr + (uint)(ExtOffset + offset));
    public void SetExtU8(int offset, byte value) => M.WriteU8(Addr + (uint)(ExtOffset + offset), value);
    public void SetExtU16(int offset, ushort value) => M.WriteU16(Addr + (uint)(ExtOffset + offset), value);
    public void SetExtU32(int offset, uint value) => M.WriteU32(Addr + (uint)(ExtOffset + offset), value);

    public bool IsAlive => Update != 0;
    public bool IsDead => (Flags & FlagDead) != 0;
    public bool IsEnemy => IsAlive && (Flags & FlagNotAnEnemy) == 0;

    public void Kill() => HitPoints = 0; //not sure if this is right

    public byte[] Read()
    {
        var data = new byte[Stride];
        for (int i = 0; i < Stride; i++) data[i] = M.ReadU8(Addr + (uint)i);
        return data;
    }

    public void Write(byte[] data)
    {
        if (Addr == 0) return;
        int n = System.Math.Min(data.Length, Stride);
        for (int i = 0; i < n; i++) M.WriteU8(Addr + (uint)i, data[i]);
    }

    //game has various copies of Destroy entity, so use my own its simpler since its all duplicates
    public void Destroy()
    {
        if (Addr == 0) return;
        if ((Flags & FlagHasPrims) != 0)
            GameApi.FreePrimitives(PrimIndex);
        for (uint i = 0; i < Stride; i += 4)
            M.WriteU32(Addr + i, 0);
    }
}
