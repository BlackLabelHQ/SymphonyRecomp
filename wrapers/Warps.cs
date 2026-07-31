using System;

namespace Sotn;

[Flags]
public enum Warp
{
    Entrance = 0x01,
    Mines = 0x02,
    OuterWall = 0x04,
    Keep = 0x08,
    Olrox = 0x10,
}

public enum Shortcut
{
    FirstClockRoomDoor = 0x00,
    MarbleBlueDoor = 0x01,
    OuterWallElevator = 0x10,
    EntranceToCaverns = 0x30,
    EntranceToMarble = 0x31,
    EntranceWarp = 0x32,
    FirstDemonButton = 0x50,
    SecondDemonButton = 0x58,
    ChapelStatue = 0x60,
    AlchemyElevator = 0x83,
    KeepStairs = 0x94,
    ColosseumToChapel = 0xB1,
    ColosseumElevator = 0xB2,
    CavernsSwitchAndBridge = 0xC7,
    SecondClockRoomDoor = 0xE4,
}
