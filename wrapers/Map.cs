using System.Collections.Generic;
using RecompOne.Runtime.Memory;

namespace Sotn;

public static class Map
{
    public const uint CastleMapAddr = 0x8006BB74u;
    public const int Length = 0x800;
    public const int Width = 64;
    public const byte Visited = 0xFF;

    static IMemory M => RecompOne.Runtime.Runtime.Mem!;

    public static byte GetRoom(int index) => M.ReadU8(CastleMapAddr + (uint)index);
    public static void SetRoom(int index, byte value) => M.WriteU8(CastleMapAddr + (uint)index, value);

    public static byte GetRoom(int x, int y) => GetRoom(y * Width + x);
    public static void SetRoom(int x, int y, byte value) => SetRoom(y * Width + x, value);

    public static bool IsVisited(int index) => GetRoom(index) != 0;
    public static void SetVisited(int index) => SetRoom(index, Visited);
    public static void SetUnvisited(int index) => SetRoom(index, 0);

    public static void RevealAll()
    {
        for (int i = 0; i < Length; i++) SetRoom(i, Visited);
    }

    public static void HideAll()
    {
        for (int i = 0; i < Length; i++) SetRoom(i, 0);
    }

    public static IEnumerable<int> VisitedRooms()
    {
        for (int i = 0; i < Length; i++)
            if (GetRoom(i) != 0) yield return i;
    }

    public static int VisitedCount
    {
        get
        {
            int n = 0;
            for (int i = 0; i < Length; i++)
                if (GetRoom(i) != 0) n++;
            return n;
        }
    }
}
