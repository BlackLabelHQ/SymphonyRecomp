using System;
using System.Collections.Generic;
using RecompOne.Runtime.Hle;
using RecompOne.Runtime.Memory;

namespace Sotn;

public static class Palette
{
    public const uint ClutIdsAddr = 0x8003C104u;
    public const uint ClutRamAddr = 0x8006CBCCu;
    public const int Colors = 16;

    static IMemory M => RecompOne.Runtime.Runtime.Mem!;

    public static bool SyncRam { get; set; }

    static readonly Dictionary<int, ushort[]> _original = [];
    static readonly ushort[] _scratch = new ushort[Colors];
    static Stage _cachedStage = (Stage)0xFF;

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

        int n = Math.Min(colors.Length, Colors);
        if (SyncRam)
        {
            uint ram = ClutRamAddr + (uint)(id * Colors * 2);
            for (int i = 0; i < n; i++) M.WriteU16(ram + (uint)(i * 2), colors[i]);
        }

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
        for (int i = 0; i < Colors; i++) colors[i] = ShadeColor(colors[i], r, g, b);
        Write(id, colors);
    }

    public static ushort[] Original(int id)
    {
        var stage = Game.StageId;
        if (stage != _cachedStage)
        {
            _cachedStage = stage;
            _original.Clear();
        }

        if (!_original.TryGetValue(id, out var colors))
        {
            colors = Read(id);
            if (IsEmpty(colors)) return colors; //not uploaded yet, cache no
            _original[id] = colors;
        }
        return colors;
    }

    public static bool IsEmpty(ushort[] colors)
    {
        for (int i = 0; i < colors.Length; i++)
            if (colors[i] != 0) return false;
        return true;
    }

    public static void Shade(int id, float r, float g, float b)
    {
        var src = Original(id);
        if (IsEmpty(src)) return;
        for (int i = 0; i < Colors; i++) _scratch[i] = ShadeColor(src[i], r, g, b);
        Write(id, _scratch);
    }

    public static void Restore(int id)
    {
        var src = Original(id);
        if (IsEmpty(src)) return;
        Write(id, src);
    }

    public static void Fill(int id, ushort color, bool keepTransparent = true)
    {
        var src = Original(id);
        if (IsEmpty(src)) return;
        for (int i = 0; i < Colors; i++)
            _scratch[i] = keepTransparent && src[i] == 0 ? (ushort)0 : color;
        Write(id, _scratch);
    }

    public static ushort Rgb(int r, int g, int b, bool mask = false) => (ushort)(
        (Math.Clamp(r, 0, 31)) |
        (Math.Clamp(g, 0, 31) << 5) |
        (Math.Clamp(b, 0, 31) << 10) |
        (mask ? 0x8000 : 0));

    public static void Forget(int id) => _original.Remove(id);

    public static void ForgetAll() { _original.Clear(); _blocks.Clear(); _written.Clear(); }

    public const int BlockWidth = 256;
    public const int BlockHeight = 16;
    public const int BlockColors = BlockWidth * BlockHeight;

    static readonly Dictionary<int, ushort[]> _blocks = [];
    static readonly Dictionary<int, ushort[]> _written = [];
    static readonly ushort[] _blockScratch = new ushort[BlockColors];

    static bool Same(ushort[] a, ushort[] b)
    {
        if (a.Length != b.Length) return false;
        for (int i = 0; i < a.Length; i++)
            if (a[i] != b[i]) return false;
        return true;
    }

    static bool BlockRect(int baseId, out int x, out int y)
    {
        ushort clut = ClutOf(baseId);
        x = (clut & 0x3F) << 4;
        y = clut >> 6;
        return clut != 0;
    }

    public static ushort[]? OriginalBlock(int baseId)
    {
        var stage = Game.StageId;
        if (stage != _cachedStage)
        {
            _cachedStage = stage;
            _original.Clear();
            _blocks.Clear();
        }

        if (_blocks.TryGetValue(baseId, out var cached)) return cached;
        if (!BlockRect(baseId, out int x, out int y)) return null;

        var colors = new ushort[BlockColors];
        var backend = GpuHle.Backend;
        if (GpuHle.Active && backend is { Ready: true }) backend.ReadVram(x, y, BlockWidth, BlockHeight, colors);
        else
        {
            var gpu = RecompOne.Runtime.Runtime.Gpu;
            if (gpu == null) return null;
            for (int row = 0; row < BlockHeight; row++)
                for (int i = 0; i < BlockWidth; i++)
                    colors[row * BlockWidth + i] = gpu.Shadow[x + i, y + row];
        }

        if (IsEmpty(colors)) return null;
        if (_written.TryGetValue(baseId, out var written) && Same(colors, written)) return null;

        _blocks[baseId] = colors;
        return colors;
    }

    static void WriteBlock(int baseId, ushort[] colors) //better for performance
    {
        if (!BlockRect(baseId, out int x, out int y)) return;

        if (SyncRam)
        {
            uint ram = ClutRamAddr + (uint)(baseId * Colors * 2);
            for (int i = 0; i < BlockColors; i++) M.WriteU16(ram + (uint)(i * 2), colors[i]);
        }

        var gpu = RecompOne.Runtime.Runtime.Gpu;
        if (gpu != null)
            for (int row = 0; row < BlockHeight; row++)
                for (int i = 0; i < BlockWidth; i++)
                    gpu.Shadow[x + i, y + row] = colors[row * BlockWidth + i];

        var backend = GpuHle.Backend;
        if (GpuHle.Active && backend is { Ready: true })
            backend.WriteVram(x, y, BlockWidth, BlockHeight, colors);

        if (!_written.TryGetValue(baseId, out var last) || last.Length != BlockColors) _written[baseId] = last = new ushort[BlockColors];
        Array.Copy(colors, last, BlockColors);
    }

    public static void ShadeBlock(int baseId, float r, float g, float b)
    {
        var src = OriginalBlock(baseId);
        if (src == null) return;
        for (int i = 0; i < BlockColors; i++) _blockScratch[i] = ShadeColor(src[i], r, g, b);
        WriteBlock(baseId, _blockScratch);
    }

    public static void FillBlock(int baseId, ushort color, bool keepTransparent = true)
    {
        var src = OriginalBlock(baseId);
        if (src == null) return;
        for (int i = 0; i < BlockColors; i++)
            _blockScratch[i] = keepTransparent && src[i] == 0 ? (ushort)0 : color;
        WriteBlock(baseId, _blockScratch);
    }

    public static void RestoreBlock(int baseId)
    {
        var src = OriginalBlock(baseId);
        if (src == null) return;
        WriteBlock(baseId, src);
    }

    public static ushort ShadeColor(ushort color, float r, float g, float b)
    {
        if (color == 0) return 0;

        int sr = color & 0x1F, sg = (color >> 5) & 0x1F, sb = (color >> 10) & 0x1F;
        int mask = color & 0x8000;

        int cr = Math.Clamp((int)(sr * r), 0, 31);
        int cg = Math.Clamp((int)(sg * g), 0, 31);
        int cb = Math.Clamp((int)(sb * b), 0, 31);

        if ((cr | cg | cb) == 0)
        {
            if (sr >= sg && sr >= sb) cr = 1;
            else if (sg >= sb) cg = 1;
            else cb = 1;
        }

        return (ushort)(cr | (cg << 5) | (cb << 10) | mask);
    }
}
