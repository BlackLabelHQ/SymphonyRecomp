using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using RecompOne.Runtime.Dispatch;
using RecompOne.Runtime.Events;
using RecompOne.Runtime.Hle;
using RecompOne.Runtime.Memory;
using StbImageSharp;
using Sotn;

namespace Recompiled;

public sealed class RoomFillRect
{
    public int Left { get; set; }
    public int Top { get; set; }
    public int Right { get; set; }
    public int Bottom { get; set; }
}

public sealed class RoomFadeRange
{
    public int Start { get; set; }
    public int End { get; set; } = 100;
}

public sealed class RoomFadeArea
{
    public int Width { get; set; }
    public int Height { get; set; }
    public double? CenterX { get; set; }
    public double? CenterY { get; set; }
    public RoomFadeRange? Left { get; set; }
    public RoomFadeRange? Right { get; set; }
    public RoomFadeRange? Top { get; set; }
    public RoomFadeRange? Bottom { get; set; }
    public string Direction { get; set; } = "Top to bottom";
    public string Color { get; set; } = "#000000";
    public int Start { get; set; }
    public int End { get; set; } = 100;
    public int Strength { get; set; } = 100;
    public int Softness { get; set; } = 50;
    public bool Inverted { get; set; }

    [JsonIgnore] public int ImageId = -1;

    public void Normalize()
    {
        if (CenterX != null) return;
        var range = new RoomFadeRange { Start = Start, End = End };
        switch (Direction.Trim().Replace('_', ' ').ToLowerInvariant())
        {
            case "left to right": CenterX = 0; CenterY = .5; Right = range; break;
            case "right to left": CenterX = 1; CenterY = .5; Left = range; break;
            case "bottom to top": CenterX = .5; CenterY = 1; Top = range; break;
            default: CenterX = .5; CenterY = 0; Bottom = range; break;
        }
    }
}

public sealed class RoomFill
{
    public string Overlay { get; set; } = "";
    public int Room { get; set; }
    public RoomFillRect? Rect { get; set; }
    public int TileX { get; set; }
    public int TileY { get; set; }
    public string Image { get; set; } = "";
    public string? Tiles { get; set; }
    public string? RawTiles { get; set; }
    public string Layer { get; set; } = "foreground";
    public bool InOriginalAspect { get; set; }
    public string? HideWhenEntity { get; set; }
    public string? Note { get; set; }
    public RoomFadeArea? FadeArea { get; set; }

    [JsonIgnore] public uint HideEntityAddr;
    [JsonIgnore] public ushort[]? TileData;
    [JsonIgnore] public uint[]? RawData;

    [JsonIgnore] public bool HasTiles => TileData != null || RawData != null;
}

public static class RoomFills
{
    const uint CurrentBufferPtr = 0x8006C37C;
    const uint BackbufferX = 0x8006C39C;
    const uint BackbufferY = 0x8006C3A0;
    const uint OtOfs = 0x474;
    const uint TilemapAddr = 0x80073084;
    const uint OverlayHeader = 0x80180000;
    const uint TilemapLeft = TilemapAddr + 0x2C;
    const int OtSize = 0x200;
    const int FadeOrder = 0x1EC;
    const int FadeTextureMax = 512;
    const int ImageMaxOrder = 0x7F;
    const int Screen = 256;
    const int TilesPerScreen = 16;

    const uint ClutIdsAddr = 0x8003C104;

    sealed class Loaded
    {
        public int Image = -1;
        public int Width, Height;
        public byte[] Rgba = [];
        public bool Failed;
    }

    static readonly List<RoomFill> _fills = [];
    static readonly Dictionary<string, Loaded> _cache = [];
    static readonly Dictionary<string, string> _files = [];
    static readonly Dictionary<string, (byte[] Rgba, int Width, int Height)> _raw = [];
    static bool _loaded;
    static uint _overlayKey;
    static string? _overlayName;

    public static Func<RoomFill, string?>? ImageResolver;
    public static Func<RoomFill, bool>? Visible;

    public static IReadOnlyList<RoomFill> Fills
    {
        get
        {
            EnsureConfig();
            return _fills;
        }
    }

    public static void Register()
    {
        Event.AddListener<TilemapRenderedEvent>(OnTilemapRendered);
        Event.AddListener<TilemapLayerDrawnEvent>(OnLayerDrawn);
        RoomExtension.Register();
    }

    public static void SetImage(string name, string path)
    {
        _files[name] = path;
        _raw.Remove(name);
        _cache.Remove(name);
    }

    public static void SetImage(string name, byte[] rgba, int width, int height)
    {
        _raw[name] = (rgba, width, height);
        _files.Remove(name);
        _cache.Remove(name);
    }

    public static void ResetImage(string name)
    {
        _files.Remove(name);
        _raw.Remove(name);
        _cache.Remove(name);
    }

    static bool Custom => RoomExtensionOptions.Mode.Value == RoomExtensionMode.Custom;
    static bool Images => RoomExtensionOptions.Mode.Value != RoomExtensionMode.Disabled;

    static void OnTilemapRendered(TilemapRenderedEvent e)
    {
        if (!Images) return;
        EnsureConfig();
        if (_fills.Count == 0 || !e.LayersVisible) return;

        var m = e.Memory;
        if (m == null) return;
        if (m.ReadU32(TilemapAddr + 0x20) == 0 || m.ReadU32(TilemapAddr + 0x00) == 0) return;

        int left = (int)m.ReadU32(TilemapLeft);
        int top = (int)m.ReadU32(TilemapLeft + 4);
        int right = (int)m.ReadU32(TilemapLeft + 8);
        int bottom = (int)m.ReadU32(TilemapLeft + 12);

        bool candidate = false;
        foreach (var fill in _fills)
            if (InRoom(fill, left, top, right, bottom)) { candidate = true; break; }
        if (!candidate) return;

        string? overlay = CurrentOverlay(m);
        bool table = false;
        int order = (int)m.ReadU32(TilemapAddr + 0x18);
        int bx = (int)m.ReadU32(BackbufferX);
        int by = (int)m.ReadU32(BackbufferY);

        foreach (var fill in _fills)
        {
            if (!InRoom(fill, left, top, right, bottom)) continue;
            if (!string.Equals(fill.Overlay, overlay, StringComparison.OrdinalIgnoreCase)) continue;
            if (fill.HideEntityAddr != 0 && EntityActive(fill.HideEntityAddr)) continue;
            if (Visible != null && !Visible(fill)) continue;

            if (fill.HasTiles || !LayerMatches(fill, true) || string.IsNullOrWhiteSpace(fill.Image)) continue;

            var image = Resolve(fill);
            if (image == null) continue;

            if (!table)
            {
                GpuPrims.SetOrderingTable(m.ReadU32(CurrentBufferPtr) + OtOfs, OtSize);
                table = true;
            }

            int x = bx + fill.TileX * Screen - e.ScrollX;
            int y = by + fill.TileY * Screen - e.ScrollY;
            GpuPrims.Sprite(Math.Min(order, ImageMaxOrder), image.Image, x, y, image.Width, image.Height);
        }
    }

    internal static List<(byte[] Rgba, int Width, int Height, int X, int Y)> ForegroundImages(IMemory m, int scrollX, int scrollY)
    {
        var images = new List<(byte[], int, int, int, int)>();
        if (!Images) return images;
        EnsureConfig();
        if (_fills.Count == 0 || m.ReadU32(TilemapAddr + 0x20) == 0 || m.ReadU32(TilemapAddr + 0x00) == 0) return images;

        int left = (int)m.ReadU32(TilemapLeft);
        int top = (int)m.ReadU32(TilemapLeft + 4);
        int right = (int)m.ReadU32(TilemapLeft + 8);
        int bottom = (int)m.ReadU32(TilemapLeft + 12);
        string? overlay = CurrentOverlay(m);

        foreach (var fill in _fills)
        {
            if (!InRoom(fill, left, top, right, bottom)) continue;
            if (!string.Equals(fill.Overlay, overlay, StringComparison.OrdinalIgnoreCase)) continue;
            if (Visible != null && !Visible(fill)) continue;
            if (fill.HasTiles || !LayerMatches(fill, true) || string.IsNullOrWhiteSpace(fill.Image)) continue;
            if (Resolve(fill) is not { } image) continue;
            images.Add((image.Rgba, image.Width, image.Height, fill.TileX * Screen - scrollX, fill.TileY * Screen - scrollY));
        }

        return images;
    }

    internal static (bool Left, bool Right, bool Top, bool Bottom) ImageSides(IMemory m)
    {
        (bool Left, bool Right, bool Top, bool Bottom) sides = default;
        if (!Images) return sides;
        EnsureConfig();
        if (_fills.Count == 0) return sides;

        int left = (int)m.ReadU32(TilemapLeft);
        int top = (int)m.ReadU32(TilemapLeft + 4);
        int right = (int)m.ReadU32(TilemapLeft + 8);
        int bottom = (int)m.ReadU32(TilemapLeft + 12);
        int screensX = (int)m.ReadU32(TilemapAddr + 0x20);
        int screensY = (int)m.ReadU32(TilemapAddr + 0x24);
        string? overlay = CurrentOverlay(m);

        foreach (var fill in _fills)
        {
            if (fill.HasTiles || string.IsNullOrWhiteSpace(fill.Image)) continue;
            if (!InRoom(fill, left, top, right, bottom)) continue;
            if (!string.Equals(fill.Overlay, overlay, StringComparison.OrdinalIgnoreCase)) continue;
            if (Visible != null && !Visible(fill)) continue;
            sides.Left |= fill.TileX < 0;
            sides.Right |= fill.TileX >= screensX;
            sides.Top |= fill.TileY < 0;
            sides.Bottom |= fill.TileY >= screensY;
        }

        return sides;
    }

    static void OnLayerDrawn(TilemapLayerDrawnEvent e)
    {
        if (!Images) return;
        EnsureConfig();
        if (_fills.Count == 0) return;

        var m = e.Memory;
        if (m == null) return;

        int left = (int)m.ReadU32(TilemapLeft);
        int top = (int)m.ReadU32(TilemapLeft + 4);
        int right = (int)m.ReadU32(TilemapLeft + 8);
        int bottom = (int)m.ReadU32(TilemapLeft + 12);

        bool candidate = false;
        foreach (var fill in _fills)
            if (InRoom(fill, left, top, right, bottom)) { candidate = true; break; }
        if (!candidate) return;

        string? overlay = CurrentOverlay(m);

        foreach (var fill in _fills)
        {
            if (!LayerMatches(fill, e.Foreground)) continue;
            if (!InRoom(fill, left, top, right, bottom)) continue;
            if (!string.Equals(fill.Overlay, overlay, StringComparison.OrdinalIgnoreCase)) continue;
            if (fill.HideEntityAddr != 0 && EntityActive(fill.HideEntityAddr)) continue;
            if (Visible != null && !Visible(fill)) continue;

            if (!e.Foreground && !fill.HasTiles && !string.IsNullOrWhiteSpace(fill.Image)) DrawImage(e, fill);
            if (!Custom) continue;
            if (fill.HasTiles) DrawTiles(m, e, fill);
            if (fill.FadeArea != null) DrawFade(e, fill, fill.FadeArea);
        }
    }

    static void DrawImage(TilemapLayerDrawnEvent e, RoomFill fill)
    {
        var image = Resolve(fill);
        if (image == null) return;
        GpuPrims.SetOrderingTable(e.Ot, OtSize);
        GpuPrims.Sprite(Math.Clamp(e.Order, 0, ImageMaxOrder), image.Image,
            e.BackbufferX + fill.TileX * Screen - e.ScrollX,
            e.BackbufferY + fill.TileY * Screen - e.ScrollY, image.Width, image.Height);
    }

    //WHY THIS FUCK DONT FUCKING WORK I HATE MYSELF
    static void DrawFade(TilemapLayerDrawnEvent e, RoomFill fill, RoomFadeArea fade)
    {
        int width = Math.Max(0, fade.Width) * 16;
        int height = Math.Max(0, fade.Height) * 16;
        if (width == 0 || height == 0) return;
        int x = e.BackbufferX + fill.TileX * 16 - e.ScrollX;
        int y = e.BackbufferY + fill.TileY * 16 - e.ScrollY;

        if (x + width <= e.BackbufferX + WidescreenPatch.ViewLeft ||
            x >= e.BackbufferX + WidescreenPatch.ViewRight ||
            y + height <= e.BackbufferY || y >= e.BackbufferY + 240) return;

        if (fade.ImageId < 0)
        {
            fade.Normalize();
            double cx = Math.Clamp(fade.CenterX ?? .5, 0, 1), cy = Math.Clamp(fade.CenterY ?? .5, 0, 1);
            double exponent = 1 + (100 - Math.Clamp(fade.Softness, 0, 100)) / 50.0;
            double strength = Math.Clamp(fade.Strength, 0, 100) / 100.0;

            double Side(RoomFadeRange? range, double distance) => range == null ? 0
                : Math.Clamp((distance * 100 - range.Start) / Math.Max(1.0, range.End - range.Start), 0, 1);

            int tw = Math.Clamp(width, 2, FadeTextureMax), th = Math.Clamp(height, 2, FadeTextureMax);
            byte[] rgba = new byte[checked(tw * th * 4)];
            var (r, g, b) = ParseColor(fade.Color);
            for (int ty = 0; ty < th; ty++)
            {
                double t = ty / (double)(th - 1);
                double vertical = t < cy ? Side(fade.Top, (cy - t) / cy) : t > cy ? Side(fade.Bottom, (t - cy) / (1 - cy)) : 0;
                for (int tx = 0; tx < tw; tx++)
                {
                    double s = tx / (double)(tw - 1);
                    double horizontal = s < cx ? Side(fade.Left, (cx - s) / cx) : s > cx ? Side(fade.Right, (s - cx) / (1 - cx)) : 0;
                    double value = Math.Max(horizontal, vertical);
                    if (fade.Inverted) value = 1 - value;
                    int i = (ty * tw + tx) * 4;
                    rgba[i] = r;
                    rgba[i + 1] = g;
                    rgba[i + 2] = b;
                    rgba[i + 3] = (byte)Math.Clamp((int)Math.Floor(255 * Math.Pow(value, exponent) * strength + 0.5), 0, 255);
                }
            }

            fade.ImageId = GpuPrims.RegisterImage(rgba, tw, th);
        }
        if (fade.ImageId < 0) return;
        GpuPrims.SetOrderingTable(e.Ot, OtSize);
        GpuPrims.Sprite(FadeOrder, fade.ImageId,
            x, y, width, height, quantizedAlpha: true);
    }

    static (byte r, byte g, byte b) ParseColor(string text)
    {
        string value = text.Trim().TrimStart('#');
        if (value.Length == 3) value = string.Concat(value[0], value[0], value[1], value[1], value[2], value[2]);
        if (value.Length != 6 || !uint.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out uint rgb)) return (0, 0, 0);
        return ((byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
    }

    static void DrawTiles(IMemory m, TilemapLayerDrawnEvent e, RoomFill fill)
    {
        if (e.GfxPage == 0 || e.GfxIndex == 0 || e.ClutTable == 0) return;

        int baseX = e.BackbufferX + fill.TileX * Screen - e.ScrollX;
        int baseY = e.BackbufferY + fill.TileY * Screen - e.ScrollY;
        var tiles = fill.TileData;
        var raw = fill.RawData;
        uint sprites = e.Sprites;

        for (int ty = 0; ty < TilesPerScreen; ty++)
        {
            int y0 = baseY + ty * 16;
            for (int tx = 0; tx < TilesPerScreen; tx++)
            {
                if (Covered(m, e, fill.TileX * TilesPerScreen + tx, fill.TileY * TilesPerScreen + ty)) continue;
                byte g, page, clutIdx;
                if (raw != null)
                {
                    uint value = raw[ty * TilesPerScreen + tx];
                    if (value == 0) continue;
                    page = (byte)(value >> 16);
                    g = (byte)(value >> 8);
                    clutIdx = (byte)value;
                }
                else
                {
                    ushort tile = tiles![ty * TilesPerScreen + tx];
                    if (tile == 0) continue;
                    g = m.ReadU8(e.GfxIndex + tile);
                    page = m.ReadU8(e.GfxPage + tile);
                    clutIdx = m.ReadU8(e.ClutTable + tile);
                }
                if (sprites >= e.MaxSprites) break;

                ushort clut = m.ReadU16(ClutIdsAddr + (e.ClutBase + clutIdx) * 2);

                uint prim = e.Pool + sprites * 16;
                m.WriteU32(prim + 4, e.Cmd << 24 | 0x808080);
                m.WriteU32(prim + 8, (uint)(ushort)y0 << 16 | (ushort)(baseX + tx * 16));
                m.WriteU32(prim + 12, (uint)clut << 16 | (uint)(byte)(g & 0xF0) << 8 | (byte)(g << 4));
                sprites++;

                if (e.Order + page <= ImageMaxOrder) AddPrim(m, e.Ot, e.Order + page, prim, 3);
                else _lifted.Add((page, prim));
            }
        }

        e.Sprites = sprites;
        LiftBehindEntities(m, e);
    }

    static readonly List<(byte Page, uint Prim)> _lifted = [];

    static void LiftBehindEntities(IMemory m, TilemapLayerDrawnEvent e) //esta PORRA agora vai funcionar direito, sem pegar a porcaria do DR_MODE errado 
    {
        if (_lifted.Count == 0) return;
        _lifted.Sort((a, b) => a.Page.CompareTo(b.Page));

        for (int start = 0; start < _lifted.Count;)
        {
            byte page = _lifted[start].Page;
            int end = start;
            while (end < _lifted.Count && _lifted[end].Page == page) end++;

            if (e.DrawModes < e.MaxDrawModes)
            {
                for (int i = start; i < end; i++) AddPrim(m, e.Ot, ImageMaxOrder, _lifted[i].Prim, 3);
                uint mode = e.DrawModesAddr + e.DrawModes * 12;
                AddPrim(m, e.Ot, ImageMaxOrder, mode, 2);
                m.WriteU32(mode + 4, 0xE1000000u | ((uint)(e.TpageBase + page) & 0x9FF));
                m.WriteU32(mode + 8, 0xE2000000u);
                e.DrawModes++;
            }

            start = end;
        }

        _lifted.Clear();
    }

    static bool Covered(IMemory m, TilemapLayerDrawnEvent e, int column, int row)
    {
        if (e.Wrap || e.Layout == 0 || column < 0 || row < 0 || column >= e.Columns || row >= e.Rows) return false;
        return m.ReadU16(e.Layout + (uint)(row * e.Columns + column) * 2) != 0;
    }

    static bool LayerMatches(RoomFill fill, bool foreground)
    {
        string layer = fill.Layer?.Trim().ToLowerInvariant() ?? "foreground";
        return foreground ? layer != "background" : layer == "background";
    }

    static void AddPrim(IMemory m, uint ot, int index, uint prim, uint length)
    {
        if ((uint)index >= OtSize) return;
        uint entry = ot + (uint)index * 4;
        uint old = m.ReadU32(entry);
        m.WriteU32(prim, length << 24 | (old & 0xFFFFFFu));
        m.WriteU32(entry, (old & 0xFF000000u) | (prim & 0xFFFFFFu));
    }

    static bool InRoom(RoomFill fill, int left, int top, int right, int bottom)
    {
        if (!fill.InOriginalAspect && WidescreenPatch.OriginalAspect) return false;
        if (fill.Rect is not { } r) return true;
        return r.Left == left && r.Top == top && r.Right == right && r.Bottom == bottom;
    }

    static string? CurrentOverlay(IMemory m)
    {
        uint key = m.ReadU32(OverlayHeader);
        if (key == _overlayKey) return _overlayName;

        _overlayKey = key;
        _overlayName = null;
        var active = Dispatcher.ActiveNames;
        for (int i = active.Length - 1; i >= 0; i--)
        {
            if (!Dispatcher.Overlays.TryGetValue(active[i], out var overlay)) continue;
            if (!overlay.Functions.ContainsKey(key)) continue;
            _overlayName = overlay.Name;
            break;
        }
        return _overlayName;
    }

    static bool EntityActive(uint update)
    {
        foreach (var ent in Entities.All())
            if (ent.Update == update) return true;
        return false;
    }

    static Loaded? Resolve(RoomFill fill)
    {
        string name = ImageResolver?.Invoke(fill) ?? fill.Image;
        if (string.IsNullOrEmpty(name)) return null;

        if (_cache.TryGetValue(name, out var cached))
            return cached.Failed ? null : cached;

        var loaded = new Loaded();
        _cache[name] = loaded;
        try
        {
            byte[] rgba;
            int width, height;

            if (_raw.TryGetValue(name, out var raw))
            {
                (rgba, width, height) = raw;
            }
            else
            {
                using var stream = Open(name);
                var img = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                rgba = img.Data;
                width = img.Width;
                height = img.Height;
            }

            loaded.Image = GpuPrims.RegisterImage(rgba, width, height);
            loaded.Rgba = rgba;
            loaded.Width = width;
            loaded.Height = height;
            if (loaded.Image < 0) loaded.Failed = true;
        }
        catch (Exception ex)
        {
            loaded.Failed = true;
            Console.Error.WriteLine($"[RoomFills] Cannot load '{name}': {ex.Message}");
        }
        return loaded.Failed ? null : loaded;
    }

    static Stream Open(string name)
    {
        if (_files.TryGetValue(name, out var path))
            return File.OpenRead(path);

        var asm = Assembly.GetExecutingAssembly();
        string suffix = ".extension." + name;
        var res = Array.Find(asm.GetManifestResourceNames(),
                      n => n.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
                  ?? throw new FileNotFoundException($"{name} is not embedded");
        return asm.GetManifestResourceStream(res)
               ?? throw new FileNotFoundException($"cannot open {res}");
    }

    static void EnsureConfig()
    {
        if (_loaded) return;
        _loaded = true;

        try
        {
            using var stream = Open("room_fills.json");
            var doc = JsonSerializer.Deserialize<Config>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
            });

            foreach (var fill in doc?.Fills ?? [])
            {
                fill.TileData = ParseTiles(fill.Tiles);
                fill.RawData = ParseRawTiles(fill.RawTiles);
                if (!fill.HasTiles && fill.FadeArea == null && string.IsNullOrWhiteSpace(fill.Image)) continue;
                if (!string.IsNullOrWhiteSpace(fill.HideWhenEntity))
                {
                    string text = fill.HideWhenEntity!.Trim();
                    if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase)) text = text[2..];
                    uint.TryParse(text, System.Globalization.NumberStyles.HexNumber, null, out fill.HideEntityAddr);
                }
                _fills.Add(fill);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[RoomFills] Cannot read room_fills.json: {ex.Message}");
        }
    }

    static ushort[]? ParseTiles(string? text)
    {
        if (string.IsNullOrWhiteSpace(text)) return null;
        const int count = TilesPerScreen * TilesPerScreen;
        if (text!.Length < count * 4) return null;

        var data = new ushort[count];
        for (int i = 0; i < count; i++)
        {
            if (!ushort.TryParse(text.AsSpan(i * 4, 4), System.Globalization.NumberStyles.HexNumber,
                    null, out data[i]))
                return null;
        }
        return data;
    }

    static uint[]? ParseRawTiles(string? text)
    {
        if (string.IsNullOrWhiteSpace(text)) return null;
        const int count = TilesPerScreen * TilesPerScreen;
        if (text!.Length < count * 8) return null;

        var data = new uint[count];
        for (int i = 0; i < count; i++)
        {
            if (!uint.TryParse(text.AsSpan(i * 8, 8), System.Globalization.NumberStyles.HexNumber,
                    null, out data[i]))
                return null;
        }
        return data;
    }

    sealed class Config
    {
        public List<RoomFill>? Fills { get; set; }
    }
}
