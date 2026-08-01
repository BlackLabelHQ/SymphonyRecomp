using System;
using RecompOne.Runtime.Hle;
using RecompOne.Runtime.Memory;

namespace Sotn;

public static class Palette
{
    public const uint ClutIdsAddr = 0x8003C104u;
    public const uint ClutRamAddr = 0x8006CBCCu;
    public const int Colors = 16;

    static IMemory M => RecompOne.Runtime.Runtime.Mem!;

    public static ushort ClutOf(int id) => M.ReadU16(ClutIdsAddr + (uint)(id * 2));

    public static ushort[] Read(int id)
    {
        var colors = new ushort[Colors];
        ushort clut = ClutOf(id);
        if (clut == 0) return colors;

        int x = (clut & 0x3F) << 4, y = clut >> 6;
        var backend = GpuHle.Backend;
        if (GpuHle.Active && backend is { Ready: true })
        {
            backend.ReadVram(x, y, Colors, 1, colors);
            return colors;
        }

        var gpu = RecompOne.Runtime.Runtime.Gpu;
        if (gpu != null)
            for (int i = 0; i < Colors; i++) colors[i] = gpu.Shadow[x + i, y];
        return colors;
    }

    public static void Write(int id, ReadOnlySpan<ushort> colors)
    {
        ushort clut = ClutOf(id);
        if (clut == 0) return;

        uint ram = ClutRamAddr + (uint)(id * Colors * 2);
        int n = Math.Min(colors.Length, Colors);
        for (int i = 0; i < n; i++) M.WriteU16(ram + (uint)(i * 2), colors[i]);

        int x = (clut & 0x3F) << 4, y = clut >> 6;
        var gpu = RecompOne.Runtime.Runtime.Gpu;
        if (gpu != null)
            for (int i = 0; i < n; i++) gpu.Shadow[x + i, y] = colors[i];

        var backend = GpuHle.Backend;
        if (GpuHle.Active && backend is { Ready: true })
            backend.WriteVram(x, y, n, 1, colors[..n]);
    }

    public static void Tint(int id, float r, float g, float b)
    {
        var colors = Read(id);
        for (int i = 0; i < Colors; i++)
        {
            if (colors[i] == 0) continue;
            int cr = Math.Clamp((int)((colors[i] & 0x1F) * r), 0, 31);
            int cg = Math.Clamp((int)(((colors[i] >> 5) & 0x1F) * g), 0, 31);
            int cb = Math.Clamp((int)(((colors[i] >> 10) & 0x1F) * b), 0, 31);
            colors[i] = (ushort)(cr | (cg << 5) | (cb << 10) | (colors[i] & 0x8000));
        }
        Write(id, colors);
    }
}
