using Hexa.NET.ImGui;
using RecompOne.Runtime.Config;
using RecompOne.Runtime.Events;
using RecompOne.Runtime.Hle;
using RecompOne.UI;
using RecompOne.Runtime.Memory;

namespace Recompiled;

public enum RoomExtensionMode
{
    Custom,
    FillBlank,
    Disabled,
}

public static class RoomExtensionOptions
{
    public static readonly Option<RoomExtensionMode> Mode = Options.Enum("room_extension.mode", RoomExtensionMode.Custom);
    public static readonly Option<bool> Fade = Options.Bool("room_extension.fade", true);
    public static readonly Option<int> FadeWidth = Options.Int("room_extension.fade_width", 24)
        .Normalized(static v => Math.Clamp(v, 8, 64));
}

public static class RoomExtension
{
    const uint CurrentBufferPtr = 0x8006C37C;
    const uint BackbufferX = 0x8006C39C;
    const uint BackbufferY = 0x8006C3A0;
    const uint OtOfs = 0x474;
    const uint TilemapAddr = 0x80073084;
    const uint TilemapLeft = TilemapAddr + 0x2C;
    const uint BgLayersAddr = 0x800730D8;
    const uint BgLayerStride = 0x30;
    const int MaxBgLayers = 16;
    const uint LayerShow = 0x1;
    const uint LayerTpageId = 0x60;
    const uint LayerSemiTrans = 0x80;
    const uint OverlayHeader = 0x80180000;
    const uint ClutIdsAddr = 0x8003C104;
    const uint LayerTpageAlt = 0x100;
    const uint LayerClutAlt = 0x200;
    const int OtSize = 0x200;
    const int FillOrder = 1;
    const int FadeMaxOrder = 0x7F;
    const int ViewTop = WidescreenPatch.StageViewTop;
    const int ViewBottom = WidescreenPatch.StageViewTop + WidescreenPatch.StageViewHeight;
    const double DominantShare = 0.25;

    static (uint Overlay, int Left, int Top, int Right, int Bottom) _room;
    static uint _color;
    static bool _colorReady;
    static readonly Dictionary<uint, int> _solid = [];
    static readonly Dictionary<(uint Color, int Side, int Width), int> _fades = [];

    public static void Register()
    {
        Event.AddListener<TilemapRenderedEvent>(OnTilemapRendered);
        SettingsPages.Page(SettingsPages.Display)
            .Add("room_extension", Draw, 151, "settings.display.room_extension");
    }

    static void Draw()
    {
        SettingsWidgets.Combo(RoomExtensionOptions.Mode, "room_extension.mode",
        [
            (RoomExtensionMode.Custom, Localization.T("room_extension.custom")),
            (RoomExtensionMode.FillBlank, Localization.T("room_extension.fill")),
            (RoomExtensionMode.Disabled, Localization.T("room_extension.disabled")),
        ], "room_extension.mode_hint");

        bool fill = RoomExtensionOptions.Mode.Value == RoomExtensionMode.FillBlank;
        bool fade = RoomExtensionOptions.Fade.Value;
        if (!fill) ImGui.BeginDisabled();
        SettingsWidgets.Toggle(RoomExtensionOptions.Fade, "room_extension.fade", "room_extension.fade_hint");
        if (!fade) ImGui.BeginDisabled();
        SettingsWidgets.Slider(RoomExtensionOptions.FadeWidth, "room_extension.fade_width", 8, 64, "%d px");
        if (!fade) ImGui.EndDisabled();
        if (!fill) ImGui.EndDisabled();
    }

    static void OnTilemapRendered(TilemapRenderedEvent e)
    {
        if (RoomExtensionOptions.Mode.Value != RoomExtensionMode.FillBlank || WidescreenPatch.OriginalAspect || !e.LayersVisible) return;
        var m = e.Memory;
        if (m == null || m.ReadU32(TilemapAddr) == 0 || m.ReadU32(TilemapAddr + 0x20) == 0) return;

        var room = (m.ReadU32(OverlayHeader), (int)m.ReadU32(TilemapLeft), (int)m.ReadU32(TilemapLeft + 4),
            (int)m.ReadU32(TilemapLeft + 8), (int)m.ReadU32(TilemapLeft + 12));
        if (room != _room || !_colorReady)
        {
            _room = room;
            _colorReady = Dominant(m, out _color);
            if (!_colorReady) return;
        }

        int bx = (int)m.ReadU32(BackbufferX), by = (int)m.ReadU32(BackbufferY);
        int roomW = (int)m.ReadU32(TilemapAddr + 0x20) * 256, roomH = (int)m.ReadU32(TilemapAddr + 0x24) * 256;
        int x0 = -e.ScrollX, x1 = roomW - e.ScrollX, y0 = -e.ScrollY, y1 = roomH - e.ScrollY;
        int left = WidescreenPatch.ViewLeft, right = WidescreenPatch.ViewRight;

        bool gapLeft = x0 > left, gapRight = x1 < right, gapTop = y0 > ViewTop, gapBottom = y1 < ViewBottom;
        if (!gapLeft && !gapRight && !gapTop && !gapBottom) return;

        GpuPrims.SetOrderingTable(m.ReadU32(CurrentBufferPtr) + OtOfs, OtSize);
        int solid = Solid(_color);
        if (solid < 0) return;

        var images = RoomFills.ImageSides(m);
        int cover = Math.Min((int)m.ReadU32(TilemapAddr + 0x18) + 24, FadeMaxOrder);
        int height = ViewBottom - ViewTop;
        int innerLeft = Math.Max(left, x0), innerWidth = Math.Min(right, x1) - innerLeft;
        if (gapLeft) GpuPrims.Sprite(images.Left ? FillOrder : cover, solid, bx + left, by + ViewTop, x0 - left, height);
        if (gapRight) GpuPrims.Sprite(images.Right ? FillOrder : cover, solid, bx + x1, by + ViewTop, right - x1, height);
        if (gapTop && innerWidth > 0) GpuPrims.Sprite(images.Top ? FillOrder : cover, solid, bx + innerLeft, by + ViewTop, innerWidth, y0 - ViewTop);
        if (gapBottom && innerWidth > 0) GpuPrims.Sprite(images.Bottom ? FillOrder : cover, solid, bx + innerLeft, by + y1, innerWidth, ViewBottom - y1);

        if (!RoomExtensionOptions.Fade.Value) return;
        int fw = RoomExtensionOptions.FadeWidth.Value;
        if (gapLeft && !images.Left) FadeStrip(cover, 0, bx + x0, by + ViewTop, fw, height);
        if (gapRight && !images.Right) FadeStrip(cover, 1, bx + x1 - fw, by + ViewTop, fw, height);
        if (gapTop && !images.Top) FadeStrip(cover, 2, bx + innerLeft, by + y0, innerWidth, fw);
        if (gapBottom && !images.Bottom) FadeStrip(cover, 3, bx + innerLeft, by + y1 - fw, innerWidth, fw);
    }

    static void FadeStrip(int order, int side, int x, int y, int w, int h)
    {
        if (w <= 0 || h <= 0) return;
        int length = side < 2 ? w : h;
        var key = (_color, side, length);
        if (!_fades.TryGetValue(key, out int image))
        {
            byte[] rgba = new byte[length * 4];
            for (int i = 0; i < length; i++)
            {
                double t = i / (double)Math.Max(1, length - 1);
                if (side is 0 or 2) t = 1 - t;
                rgba[i * 4] = (byte)(_color >> 16);
                rgba[i * 4 + 1] = (byte)(_color >> 8);
                rgba[i * 4 + 2] = (byte)_color;
                rgba[i * 4 + 3] = (byte)Math.Clamp((int)Math.Round(255 * t * t), 0, 255);
            }
            image = GpuPrims.RegisterImage(rgba, side < 2 ? length : 1, side < 2 ? 1 : length);
            _fades[key] = image;
        }
        if (image >= 0) GpuPrims.Sprite(order, image, x, y, w, h, quantizedAlpha: true);
    }

    static int Solid(uint color)
    {
        if (_solid.TryGetValue(color, out int image)) return image;
        image = GpuPrims.RegisterImage([(byte)(color >> 16), (byte)(color >> 8), (byte)color, 255], 1, 1);
        _solid[color] = image;
        return image;
    }

    static bool Dominant(IMemory m, out uint color)
    {
        color = 0;
        var gpu = RecompOne.Runtime.Runtime.Gpu;
        if (gpu == null) return false;

        var histogram = new Dictionary<ushort, long>();
        Accumulate(m, gpu.Vram, TilemapAddr, histogram);
        for (int i = 0; i < MaxBgLayers; i++)
        {
            uint layer = BgLayersAddr + (uint)i * BgLayerStride;
            if ((m.ReadU32(layer + 0x1C) & LayerShow) != 0) Accumulate(m, gpu.Vram, layer, histogram);
        }
        if (histogram.Count == 0) return false;

        long most = histogram.Values.Max();
        ushort best = 0;
        double darkest = double.MaxValue;
        foreach (var (rgb, count) in histogram)
        {
            if (count < most * DominantShare) continue;
            double luma = 0.299 * (rgb & 31) + 0.587 * (rgb >> 5 & 31) + 0.114 * (rgb >> 10 & 31);
            if (luma < darkest)
            {
                darkest = luma;
                best = rgb;
            }
        }

        uint r = (uint)(best & 31) * 255 / 31, gr = (uint)(best >> 5 & 31) * 255 / 31, b = (uint)(best >> 10 & 31) * 255 / 31;
        color = r << 16 | gr << 8 | b;
        return true;
    }

    static void Accumulate(IMemory m, ushort[] vram, uint layer, Dictionary<ushort, long> histogram)
    {
        uint layout = m.ReadU32(layer), tileDef = m.ReadU32(layer + 0x04);
        if (layout == 0 || tileDef == 0) return;
        uint gfxPage = m.ReadU32(tileDef), gfxIndex = m.ReadU32(tileDef + 4), clutTable = m.ReadU32(tileDef + 8);
        uint flags = m.ReadU32(layer + 0x1C);
        int clutBase = (flags & LayerClutAlt) != 0 ? 0x100 : 0;
        int tpageBase = 8 + ((flags & LayerTpageAlt) != 0 ? 0x80 : 0) + ((flags & LayerSemiTrans) != 0 ? (int)(flags & LayerTpageId) : 0);
        int tiles = (int)m.ReadU32(layer + 0x20) * 16 * (int)m.ReadU32(layer + 0x24) * 16;
        if (tiles <= 0) return;

        var usage = new Dictionary<ushort, int>();
        for (int i = 0; i < tiles; i++)
        {
            ushort tile = m.ReadU16(layout + (uint)i * 2);
            if (tile != 0) usage[tile] = usage.GetValueOrDefault(tile) + 1;
        }

        foreach (var (tile, count) in usage)
        {
            int tpage = m.ReadU8(gfxPage + tile) + tpageBase;
            byte g = m.ReadU8(gfxIndex + tile);
            ushort clut = m.ReadU16(ClutIdsAddr + (uint)(clutBase + m.ReadU8(clutTable + tile)) * 2);
            int baseX = (tpage & 0xF) * 64, baseY = (tpage >> 4 & 1) * 256;
            bool eight = (tpage >> 7 & 3) == 1;
            int u = (g & 0xF) * 16, v = g & 0xF0;
            int clutX = (clut & 0x3F) * 16, clutY = clut >> 6 & 0x1FF;

            for (int y = 0; y < 16; y++)
            {
                int row = ((baseY + v + y) & 511) * 1024;
                for (int x = 0; x < 16; x++)
                {
                    int texel = u + x, index;
                    if (eight)
                    {
                        ushort word = vram[row + ((baseX + texel / 2) & 1023)];
                        index = word >> (texel % 2 * 8) & 0xFF;
                    }
                    else
                    {
                        ushort word = vram[row + ((baseX + texel / 4) & 1023)];
                        index = word >> (texel % 4 * 4) & 0xF;
                    }
                    ushort pixel = vram[(clutY & 511) * 1024 + ((clutX + index) & 1023)];
                    if (pixel == 0) continue;
                    ushort rgb = (ushort)(pixel & 0x7FFF);
                    histogram[rgb] = histogram.GetValueOrDefault(rgb) + count;
                }
            }
        }
    }
}
