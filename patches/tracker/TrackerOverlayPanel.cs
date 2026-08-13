using System.Collections.Generic;
using System.Numerics;
using ImGuiNET;
using RecompOne.Runtime.Host.Window;
using RecompOne.Runtime.Memory;

namespace Recompiled;

public sealed class TrackerOverlayPanel : IPanel
{
    public string Name => "Tracker overlay";
    public string TitleKey => "panel.tracker";
    public bool IsOpen { get; set; }

    const float FixedIconDim = 32f;
    const float IconPadding = 0.15f;

    bool _sectioning = true;
    bool _fixedIconSize;
    bool _showJp;
    bool _showHand;
    bool _showBody;
    bool _showProgress;
    int _columns = 5;
    bool _loaded;

    public void Draw()
    {
        EnsureLoaded();

        ImGui.SetNextWindowSize(new Vector2(300, 520), ImGuiCond.FirstUseEver);
        bool open = IsOpen;
        if (!ImGui.Begin(this.Title(), ref open, ImGuiWindowFlags.MenuBar))
        {
            IsOpen = open;
            ImGui.End();
            return;
        }

        DrawMenuBar();

        var m = RecompOne.Runtime.Runtime.Mem;
        if (m == null)
        {
            ImGui.TextDisabled("No game running.");
            IsOpen = open;
            ImGui.End();
            return;
        }

        var groups = ActiveGroups();
        if (_showProgress) DrawProgress(m, groups);

        if (_sectioning)
        {
            DrawSection(m, "Relics", Tracker.Relics);
            if (_showJp) DrawSection(m, "JP Relics", Tracker.JpRelics);
            DrawSection(m, "Vlad Relics", Tracker.VladRelics);
            DrawSection(m, "Key Items", Tracker.KeyItems);

            if (_showHand) DrawSection(m, "Hand Items", Tracker.HandItems);
            if (_showBody) DrawSection(m, "Body Items", Tracker.BodyItems);
        }
        else
        {
            DrawGrid(m, "AllIcons", groups.ToArray());
        }

        IsOpen = open;
        ImGui.End();
    }

    List<Tracker.Entry[]> ActiveGroups()
    {
        var g = new List<Tracker.Entry[]> { Tracker.Relics };
        if (_showJp) g.Add(Tracker.JpRelics);
        g.Add(Tracker.VladRelics);
        g.Add(Tracker.KeyItems);
        if (_showHand) g.Add(Tracker.HandItems);
        if (_showBody) g.Add(Tracker.BodyItems);
        return g;
    }

    void DrawProgress(IMemory m, List<Tracker.Entry[]> groups)
    {
        int owned = 0, total = 0;
        foreach (var g in groups)
        {
            owned += Tracker.CountOwned(m, g);
            total += g.Length;
        }
        float frac = total > 0 ? (float)owned / total : 0f;
        ImGui.ProgressBar(frac, new Vector2(-1f, 0f), $"{frac * 100f:0.#}%  ({owned}/{total})");
        ImGui.Spacing();
    }

    void DrawMenuBar()
    {
        if (!ImGui.BeginMenuBar()) return;
        if (ImGui.BeginMenu("View"))
        {
            if (ImGui.MenuItem("Progress bar", null, ref _showProgress)) Persist();
            if (ImGui.MenuItem("Sectioning", null, ref _sectioning)) Persist();
            if (ImGui.MenuItem("Fixed icon size", null, ref _fixedIconSize)) Persist();
            if (ImGui.MenuItem("JP exclusives", null, ref _showJp)) Persist();
            if (ImGui.MenuItem("Hand Items", null, ref _showHand)) Persist();
            if (ImGui.MenuItem("Body Items", null, ref _showBody)) Persist();
            ImGui.SetNextItemWidth(140f);
            if (ImGui.SliderInt("Columns", ref _columns, 1, 12))
                _columns = Math.Clamp(_columns, 1, 12);
            if (ImGui.IsItemDeactivatedAfterEdit()) Persist();
            ImGui.EndMenu();
        }
        ImGui.EndMenuBar();
    }

    void EnsureLoaded()
    {
        if (_loaded) return;
        _loaded = true;
        var v = RecompOne.Runtime.Runtime.View;
        _sectioning = v.GetBool("Tracker.Sectioning", true);
        _fixedIconSize = v.GetBool("Tracker.FixedIconSize", false);
        _showJp = v.GetBool("Tracker.ShowJp", false);
        _showHand = v.GetBool("Tracker.ShowHand", false);
        _showBody = v.GetBool("Tracker.ShowBody", false);
        _showProgress = v.GetBool("Tracker.ShowProgress", false);
        _columns = Math.Clamp(v.GetInt("Tracker.Columns", 5), 1, 12);
    }

    void Persist()
    {
        var v = RecompOne.Runtime.Runtime.View;
        v.SetBool("Tracker.Sectioning", _sectioning);
        v.SetBool("Tracker.FixedIconSize", _fixedIconSize);
        v.SetBool("Tracker.ShowJp", _showJp);
        v.SetBool("Tracker.ShowHand", _showHand);
        v.SetBool("Tracker.ShowBody", _showBody);
        v.SetBool("Tracker.ShowProgress", _showProgress);
        v.SetInt("Tracker.Columns", _columns);
        RecompOne.Runtime.Runtime.SaveView();
    }

    void DrawSection(IMemory m, string title, Tracker.Entry[] items)
    {
        int owned = Tracker.CountOwned(m, items);
        ImGui.SeparatorText($"{title}  ({owned}/{items.Length})");
        DrawGrid(m, title, items);
    }

    void DrawGrid(IMemory m, string id, params Tracker.Entry[][] groups)
    {
        if (!ImGui.BeginTable(id, _columns, ImGuiTableFlags.SizingStretchSame))
            return;

        int col = 0;
        bool first = true;
        foreach (var items in groups)
        {
            if (!first && col != 0)
            {
                ImGui.TableNextRow();
                col = 0;
            }
            first = false;

            foreach (var e in items)
            {
                ImGui.TableNextColumn();
                DrawEntry(m, e);
                col = (col + 1) % _columns;
            }
        }

        ImGui.EndTable();
    }

    void DrawEntry(IMemory m, Tracker.Entry e)
    {
        bool owned = Tracker.IsOwned(m, e);
        bool active = Tracker.IsActive(m, e);

        float cell = ImGui.GetContentRegionAvail().X;
        float dim = _fixedIconSize ? FixedIconDim : MathF.Max(8f, cell * (1f - IconPadding));
        var size = new Vector2(dim, dim);

        ImGui.BeginGroup();
        float pad = MathF.Max(0f, (cell - dim) * 0.5f);
        if (pad > 0f) ImGui.SetCursorPosX(ImGui.GetCursorPosX() + pad);
        TrackerIcons.DrawIcon(e, owned, size);
        ImGui.EndGroup();

        if (ImGui.IsItemHovered())
        {
            ImGui.BeginTooltip();
            ImGui.Text(e.Name);
            ImGui.TextDisabled($"addr 0x{e.Address:X8} raw 0x{Tracker.RawValue(m, e):X2}");
            ImGui.TextDisabled(owned ? (active ? "owned (active)" : "owned") : "missing");
            ImGui.EndTooltip();
        }
    }
}
