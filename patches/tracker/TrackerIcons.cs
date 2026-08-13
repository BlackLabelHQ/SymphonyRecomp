using System.Numerics;
using System.Reflection;
using System.Text;
using ImGuiNET;
using RecompOne.Runtime.Host;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Recompiled;

public static class TrackerIcons
{
    static readonly Dictionary<string, uint> _color = new();
    static readonly Dictionary<string, uint> _gray = new();
    static readonly HashSet<string> _missing = new();
    static string[]? _resourceNames;

    public static bool TryGetTexture(Tracker.Entry entry, out uint color, out uint gray, out Vector2 uv0, out Vector2 uv1)
    {
        uv0 = Vector2.Zero;
        uv1 = Vector2.One;
        return LoadPng(entry.Icon, out color, out gray);
    }

    static bool LoadPng(string name, out uint color, out uint gray)
    {
        string key = "png:" + name;
        if (_color.TryGetValue(key, out color)) { gray = _gray[key]; return true; }
        color = 0;
        gray = 0;
        if (_missing.Contains(key)) return false;

        var bytes = ReadAsset(name);
        if (bytes == null) { _missing.Add(key); return false; }

        try
        {
            using var img = Image.Load<Rgba32>(bytes);
            var rgba = new byte[img.Width * img.Height * 4];
            img.CopyPixelDataTo(rgba);
            return Store(key, rgba, img.Width, img.Height, out color, out gray);
        }
        catch
        {
            _missing.Add(key);
            return false;
        }
    }

    static bool Store(string key, byte[] rgba, int w, int h, out uint color, out uint gray)
    {
        color = HostWindow.UploadTexture(rgba, w, h);
        gray = 0;
        if (color == 0) { _missing.Add(key); return false; }

        var g = (byte[])rgba.Clone();
        Grayscale(g);
        gray = HostWindow.UploadTexture(g, w, h);

        _color[key] = color;
        _gray[key] = gray;
        return true;
    }

    static void Grayscale(byte[] rgba)
    {
        for (int i = 0; i + 3 < rgba.Length; i += 4)
        {
            int l = (rgba[i] * 77 + rgba[i + 1] * 150 + rgba[i + 2] * 29) >> 8;
            rgba[i] = rgba[i + 1] = rgba[i + 2] = (byte)l;
        }
    }

    static byte[]? ReadAsset(string name)
    {
        var asm = Assembly.GetExecutingAssembly();
        _resourceNames ??= asm.GetManifestResourceNames();
        string suffix = ".tracker." + Pascal(name) + ".png";

        foreach (var res in _resourceNames)
        {
            if (!res.EndsWith(suffix, StringComparison.OrdinalIgnoreCase)) continue;
            using var s = asm.GetManifestResourceStream(res);
            if (s == null) return null;
            using var ms = new MemoryStream();
            s.CopyTo(ms);
            return ms.ToArray();
        }
        return null;
    }

    static string Pascal(string name)
    {
        var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var sb = new StringBuilder();
        foreach (var p in parts)
            sb.Append(char.ToUpperInvariant(p[0])).Append(p.AsSpan(1));
        return sb.ToString();
    }

    public static bool TryGetTexture(string icon, out uint color) => LoadPng(icon, out color, out _);

    public static string[] Names()
    {
        var asm = Assembly.GetExecutingAssembly();
        _resourceNames ??= asm.GetManifestResourceNames();

        const string prefix = ".tracker.";
        var names = new List<string>();
        foreach (var res in _resourceNames)
        {
            if (!res.EndsWith(".png", StringComparison.OrdinalIgnoreCase)) continue;
            int at = res.IndexOf(prefix, StringComparison.OrdinalIgnoreCase);
            if (at < 0) continue;

            int start = at + prefix.Length;
            int length = res.Length - start - 4;
            if (length > 0) names.Add(res.Substring(start, length));
        }
        return names.ToArray();
    }

    public static void DrawIcon(Tracker.Entry entry, bool owned, Vector2 size)
    {
        var pos = ImGui.GetCursorScreenPos();
        var dl = ImGui.GetWindowDrawList();

        if (TryGetTexture(entry, out uint color, out uint gray, out var uv0, out var uv1) && color != 0)
        {
            uint tex = owned ? color : (gray != 0 ? gray : color);
            uint tint = owned ? 0xFFFFFFFFu : 0xCCFFFFFFu;
            dl.AddImage((nint)tex, pos, pos + size, uv0, uv1, tint);
            ImGui.Dummy(size);
            return;
        }

        uint fill = owned ? 0xFF3C3C3Cu : 0xFF1C1C1Cu;
        uint border = owned ? 0xFF9A9A9Au : 0xFF3A3A3Au;
        dl.AddRectFilled(pos, pos + size, fill, 3f);
        dl.AddRect(pos, pos + size, border, 3f);
        ImGui.Dummy(size);
    }
}
