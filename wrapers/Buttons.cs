using System;

namespace Sotn;

[Flags]
public enum Button : ushort
{
    L2 = 0x0001,
    R2 = 0x0002,
    L1 = 0x0004,
    R1 = 0x0008,
    Triangle = 0x0010,
    Circle = 0x0020,
    Cross = 0x0040,
    Square = 0x0080,
    Select = 0x0100,
    L3 = 0x0200,
    R3 = 0x0400,
    Start = 0x0800,
    Up = 0x1000,
    Right = 0x2000,
    Down = 0x4000,
    Left = 0x8000,
}
