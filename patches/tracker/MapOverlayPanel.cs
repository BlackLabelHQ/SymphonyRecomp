// ported from MottZilla's script, all credits to him (he is the GOAT)

using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using ImGuiNET;
using RecompOne.Runtime.Host;
using RecompOne.Runtime.Host.Window;
using RecompOne.Runtime.Memory;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Recompiled;

public sealed class MapOverlayPanel : IPanel
{
    public string Name => "Map overlay";
    public bool IsOpen { get; set; }

    const uint ZoneAddr = 0x800974A0;
    const uint CastleXAddr = 0x800730B0;
    const uint CastleYAddr = 0x800730B4;
    const uint RoomXAddr = 0x800973F0;
    const uint RoomYAddr = 0x800973F4;
    const uint TeleportFlagAddr = 0x80073404;
    const uint PlayStateAddr = 0x80073060;
    const uint MapModeAddr = 0x8003C9A4;
    const uint Teleport2Addr = 0x80097C98;

    const uint ColVisited = 0xFFE00000;
    const uint ColCurrent = 0xFFE000E0;
    const uint ColCheckDone = 0xFF00E000;
    const uint ColCheckPending = 0xFF3050FF;
    const uint ColWhite = 0xFFFFFFFF;

    readonly record struct Check(int CellX, int CellY, int Castle)
    {
        public int Key => Castle * 4096 + CellY * 64 + CellX;
    }

    readonly byte[] _map1 = new byte[64 * 64];
    readonly byte[] _map2 = new byte[64 * 64];
    readonly List<Check> _checks = new();
    readonly HashSet<int> _done = new();

    int _curCastle = 1;
    int _checkSet = 2;
    int _checkSetApplied = -1;
    bool _loaded;

    bool _curValid;
    int _curX, _curY;

    uint _tex1, _tex2;
    bool _texTried;

    static readonly string[] CheckSetNames =
        ["Relics", "Key Items", "Guarded", "Spread", "Equipment", "Tourist", "Wanderer"];

    public void Draw()
    {
        EnsureLoaded();

        ImGui.SetNextWindowSize(new Vector2(660, 560) * HostWindow.DpiScale, ImGuiCond.FirstUseEver);
        bool open = IsOpen;
        if (!ImGui.Begin(Name, ref open))
        {
            IsOpen = open;
            ImGui.End();
            return;
        }

        var m = RecompOne.Runtime.Runtime.Mem;
        if (m == null)
        {
            ImGui.TextDisabled("No game running.");
            IsOpen = open;
            ImGui.End();
            return;
        }

        if (_checkSetApplied != _checkSet)
        {
            BuildChecks();
            _checkSetApplied = _checkSet;
        }

        DrawToolbar();
        UpdateMap(m);
        AutoMark();
        DrawMap();

        IsOpen = open;
        ImGui.End();
    }

    void DrawToolbar()
    {
        ImGui.Text($"Castle {_curCastle}");
        ImGui.SameLine();
        ImGui.SetNextItemWidth(140f * HostWindow.DpiScale);
        if (ImGui.Combo("Checks", ref _checkSet, CheckSetNames, CheckSetNames.Length)) Persist();
        ImGui.SameLine();
        if (ImGui.SmallButton("Reset"))
        {
            Array.Clear(_map1);
            Array.Clear(_map2);
            _done.Clear();
        }
        ImGui.Separator();
    }

    void AutoMark()
    {
        foreach (var ch in _checks)
        {
            var map = ch.Castle == 2 ? _map2 : _map1;
            if (map[ch.CellY * 64 + ch.CellX] != 0)
                _done.Add(ch.Key);
        }
    }

    void UpdateMap(IMemory m)
    {
        _curValid = false;

        int zone = m.ReadU8(ZoneAddr);
        int castleX = m.ReadU8(CastleXAddr);
        int castleY = m.ReadU8(CastleYAddr);
        int roomX = m.ReadU16(RoomXAddr) / 256;
        int roomY = m.ReadU16(RoomYAddr) / 256;
        castleX += roomX;
        castleY += roomY;

        _curCastle = (m.ReadU8(ZoneAddr) & 0x20) == 0x20 ? 2 : 1;

        if (zone == 0x41 && (castleY > 41 || castleX < 2)) return;
        if (zone == 0x1F || zone == 0x38) return;
        if (m.ReadU8(TeleportFlagAddr) == 0x12) return;

        if (_curCastle == 2) castleY -= 7;

        if (m.ReadU8(PlayStateAddr) != 3) return;
        if (m.ReadU8(MapModeAddr) != 1) return;
        if (m.ReadU8(Teleport2Addr) != 0) return;

        if (castleX < 0 || castleX >= 64 || castleY < 0 || castleY >= 64) return;

        var map = _curCastle == 2 ? _map2 : _map1;
        map[castleX + castleY * 64] = 1;

        _curValid = true;
        _curX = castleX;
        _curY = castleY;
    }

    void DrawMap()
    {
        EnsureTextures();

        var avail = ImGui.GetContentRegionAvail();
        float s = MathF.Min(avail.X / 320f, avail.Y / 255f);
        if (s < 0.05f) s = 0.05f;

        var origin = ImGui.GetCursorScreenPos();
        var dl = ImGui.GetWindowDrawList();
        var map = _curCastle == 2 ? _map2 : _map1;
        float cell = 5f * s;

        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == 0) continue;
            int x = i % 64, y = i / 64;
            int cy = y * 5 - 15;
            if (cy < 0) continue;
            var p = new Vector2(origin.X + x * 5 * s, origin.Y + cy * s);
            dl.AddRectFilled(p, new Vector2(p.X + cell, p.Y + cell), ColVisited);
        }

        uint tex = _curCastle == 2 ? _tex2 : _tex1;
        if (tex != 0)
            dl.AddImage((nint)tex, origin, new Vector2(origin.X + 320 * s, origin.Y + 255 * s));

        DrawChecks(origin, s, cell);

        if (_curValid)
        {
            int cy = _curY * 5 - 15;
            if (cy >= 0)
            {
                var p = new Vector2(origin.X + _curX * 5 * s, origin.Y + cy * s);
                dl.AddRectFilled(p, new Vector2(p.X + cell, p.Y + cell), ColCurrent);
            }
        }

        ImGui.SetCursorScreenPos(origin);
        ImGui.Dummy(new Vector2(320 * s, 255 * s));
    }

    void DrawChecks(Vector2 origin, float s, float cell)
    {
        var dl = ImGui.GetWindowDrawList();
        float btn = MathF.Max(14f, cell);
        float r = MathF.Max(4f, cell * 0.6f);
        int id = 0;

        foreach (var c in _checks)
        {
            if (c.Castle != _curCastle) continue;
            int cy = c.CellY * 5 - 15;
            if (cy < 0) continue;

            var center = new Vector2(origin.X + c.CellX * 5 * s + cell * 0.5f, origin.Y + cy * s + cell * 0.5f);

            ImGui.SetCursorScreenPos(new Vector2(center.X - btn * 0.5f, center.Y - btn * 0.5f));
            ImGui.PushID(id++);
            if (ImGui.InvisibleButton("chk", new Vector2(btn, btn)))
            {
                if (!_done.Remove(c.Key)) _done.Add(c.Key);
            }
            bool hovered = ImGui.IsItemHovered();
            ImGui.PopID();

            bool done = _done.Contains(c.Key);
            if (done)
            {
                dl.AddCircleFilled(center, r, ColCheckDone);
                dl.AddLine(new Vector2(center.X - r * 0.45f, center.Y),
                           new Vector2(center.X - r * 0.1f, center.Y + r * 0.45f), ColWhite, MathF.Max(1.5f, s));
                dl.AddLine(new Vector2(center.X - r * 0.1f, center.Y + r * 0.45f),
                           new Vector2(center.X + r * 0.55f, center.Y - r * 0.5f), ColWhite, MathF.Max(1.5f, s));
            }
            else
            {
                dl.AddCircleFilled(center, r, 0x80000000);
                dl.AddCircle(center, r, ColCheckPending, 14, MathF.Max(1.5f, s));
            }

            if (hovered) dl.AddCircle(center, r + 2f, ColWhite, 16, 1.5f);
        }
    }

    void AddCheck(List<Check> list, HashSet<int> seen, int px, int py, int castle)
    {
        int x = px / 5;
        int y = (py + 15) / 5;
        if (x < 0 || x >= 64 || y < 0 || y >= 64) return;
        var c = new Check(x, y, castle);
        if (seen.Add(c.Key)) list.Add(c);
    }

    void BuildChecks()
    {
        _checks.Clear();
        var seen = new HashSet<int>();

        Add(MapChecks.Relics);
        if (_checkSet >= 1) Add(MapChecks.KeyItems);
        if (_checkSet >= 2) Add(MapChecks.Guarded);
        if (_checkSet >= 3) Add(MapChecks.Spread);
        if (_checkSet >= 4 && _checkSet != 6) Add(MapChecks.Equipment);
        if (_checkSet >= 5) Add(MapChecks.Tourist);
        if (_checkSet >= 6) Add(MapChecks.Wanderer);

        void Add((int X, int Y, int C)[] arr)
        {
            foreach (var (px, py, c) in arr) AddCheck(_checks, seen, px, py, c);
        }
    }

    void EnsureLoaded()
    {
        if (_loaded) return;
        _loaded = true;
        var v = RecompOne.Runtime.Runtime.View;
        _checkSet = Math.Clamp(v.GetInt("Map.CheckSet", 2), 0, 6);
    }

    void Persist()
    {
        var v = RecompOne.Runtime.Runtime.View;
        v.SetInt("Map.CheckSet", _checkSet);
        RecompOne.Runtime.Runtime.SaveView();
    }

    void EnsureTextures()
    {
        if (_texTried) return;
        _texTried = true;
        _tex1 = LoadTexture("Castle1");
        _tex2 = LoadTexture("Castle2");
    }

    static uint LoadTexture(string name)
    {
        var asm = Assembly.GetExecutingAssembly();
        string suffix = ".map." + name + ".png";
        foreach (var res in asm.GetManifestResourceNames())
        {
            if (!res.EndsWith(suffix, StringComparison.OrdinalIgnoreCase)) continue;
            using var st = asm.GetManifestResourceStream(res);
            if (st == null) return 0;
            using var ms = new MemoryStream();
            st.CopyTo(ms);
            try
            {
                using var img = Image.Load<Rgba32>(ms.ToArray());
                var rgba = new byte[img.Width * img.Height * 4];
                img.CopyPixelDataTo(rgba);
                return HostWindow.UploadTexture(rgba, img.Width, img.Height);
            }
            catch { return 0; }
        }
        return 0;
    }
}
