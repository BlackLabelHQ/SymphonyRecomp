using System;
using System.Numerics;
using System.Text;
using ImGuiNET;
using RecompOne.Runtime.Host.Window;
using Sotn;

namespace Recompiled;

public sealed class InventoryCheatPanel : IPanel
{
    public string Name => "Inventory";
    public string TitleKey => "panel.cheats.inventory";
    public bool IsOpen { get; set; }

    string _search = "";

    static readonly (string Display, int Id)[] HandList = Build<HandItem>();
    static readonly (string Display, int Id)[] BodyList = Build<BodyItem>();

    static (string, int)[] Build<T>() where T : struct, Enum
    {
        var vals = Enum.GetValues<T>();
        var arr = new (string, int)[vals.Length];
        for (int i = 0; i < vals.Length; i++)
            arr[i] = (Spaced(vals[i].ToString()!), Convert.ToInt32(vals[i]));
        return arr;
    }



    public void Draw()
    {
        ImGui.SetNextWindowSize(new Vector2(430, 560), ImGuiCond.FirstUseEver);
        bool open = IsOpen;
        if (!ImGui.Begin(this.Title(), ref open))
        {
            IsOpen = open;
            ImGui.End();
            return;
        }

        if (RecompOne.Runtime.Runtime.Mem == null || !Cheats.InPlay())
        {
            ImGui.TextDisabled(Localization.T("common.not_in_play"));
            
            IsOpen = open;
            ImGui.End();
            return;
        }


        ImGui.SetNextItemWidth(-1f);
        ImGui.InputTextWithHint("##search", "Search items...", ref _search, 64);

        if (ImGui.BeginTabBar("invtabs"))
        {
            if (ImGui.BeginTabItem("Hand Items"))
            {
                DrawItems("hand", HandList, id => Inventory.GetHandCount(id), (id, n) => Inventory.SetHandCount(id, n));
                ImGui.EndTabItem();
            }
            if (ImGui.BeginTabItem("Body Items"))
            {
                DrawItems("body", BodyList, id => Inventory.GetBodyCount(id), (id, n) => Inventory.SetBodyCount(id, n));
                ImGui.EndTabItem();
            }
            if (ImGui.BeginTabItem("Relics"))
            {
                DrawRelics();
                ImGui.EndTabItem();
            }
            if (ImGui.BeginTabItem("Spells"))
            {
                DrawSpells();
                ImGui.EndTabItem();
            }
            ImGui.EndTabBar();
        }

        IsOpen = open;
        ImGui.End();
    }

    void DrawItems(string id, (string Display, int Id)[] list, Func<int, int> get, Action<int, int> set)
    {
        if (ImGui.SmallButton($"Gve all##{id}"))
            foreach (var it in list) if (it.Id != 0) set(it.Id, 1);
        ImGui.SameLine();
        if (ImGui.SmallButton($"Clear all##{id}"))
            foreach (var it in list) set(it.Id, 0);

        if (ImGui.BeginTable($"tbl_{id}", 2,
            ImGuiTableFlags.RowBg | ImGuiTableFlags.BordersInnerH | ImGuiTableFlags.ScrollY))
        {
            ImGui.TableSetupColumn("Item", ImGuiTableColumnFlags.WidthStretch);
            ImGui.TableSetupColumn("Count", ImGuiTableColumnFlags.WidthFixed, 110f);
            ImGui.TableSetupScrollFreeze(0, 1);
            ImGui.TableHeadersRow();

            foreach (var it in list)
            {
                if (it.Id == 0) continue;
                if (_search.Length > 0 && !it.Display.Contains(_search, StringComparison.OrdinalIgnoreCase))
                    continue;

                ImGui.TableNextRow();
                ImGui.TableNextColumn();
                int count = get(it.Id);
                if (count > 0)
                    ImGui.TextColored(new Vector4(0.5f, 1f, 0.6f, 1f), it.Display);
                else
                    ImGui.TextUnformatted(it.Display);

                ImGui.TableNextColumn();
                ImGui.PushID(it.Id);
                ImGui.SetNextItemWidth(-1f);
                if (ImGui.InputInt("##c", ref count))
                    set(it.Id, Math.Clamp(count, 0, 255));
                ImGui.PopID();
            }
            ImGui.EndTable();
        }
    }

    void DrawRelics()
    {
        if (ImGui.SmallButton("give all##relic"))
            foreach (Relic r in Enum.GetValues<Relic>()) Inventory.GiveRelic(r, true);
        ImGui.SameLine();
        if (ImGui.SmallButton("Clear all##relic"))
            foreach (Relic r in Enum.GetValues<Relic>()) Inventory.GiveRelic(r, false);

        ImGui.SeparatorText("Familiars");
        RelicRow(Relic.BatCard);
        RelicRow(Relic.GhostCard);
        RelicRow(Relic.FaerieCard);
        RelicRow(Relic.DemonCard);
        RelicRow(Relic.SwordCard);
        RelicRow(Relic.Jp0);
        RelicRow(Relic.Jp1);

        ImGui.SeparatorText("Relics");
        foreach (Relic r in Enum.GetValues<Relic>())
        {
            int i = (int)r;
            if (i >= (int)Relic.BatCard && i <= (int)Relic.Jp1) continue;
            RelicRow(r);
        }
    }

    void RelicRow(Relic r)
    {
        string name = r switch
        {
            Relic.Jp0 => "Sprite Card (JP)",
            Relic.Jp1 => "Nosedevil Card (JP)",

            _ => Spaced(r.ToString())
        };
        if (_search.Length > 0 && !name.Contains(_search, StringComparison.OrdinalIgnoreCase))
            return;
        bool has = Inventory.HasRelic(r);
        if (ImGui.Checkbox(name, ref has))
            Inventory.GiveRelic(r, has);
    }

    static void DrawSpells()
    {
        if (ImGui.SmallButton("Learn all"))
            foreach (Spell s in Enum.GetValues<Spell>()) Inventory.SetSpellLearned(s, true);
        ImGui.SameLine();
        if (ImGui.SmallButton("Forget all"))
            foreach (Spell s in Enum.GetValues<Spell>()) Inventory.SetSpellLearned(s, false);
        ImGui.Separator();

        foreach (Spell s in Enum.GetValues<Spell>())
        {
            bool has = Inventory.HasSpell(s);
            if (ImGui.Checkbox(Spaced(s.ToString()), ref has))
                Inventory.SetSpellLearned(s, has);
        }
    }

    static string Spaced(string name)
    {
        var sb = new StringBuilder(name.Length + 8);
        for (int i = 0; i < name.Length; i++)
        {
            char ch = name[i];
            if (i > 0 && char.IsUpper(ch) && (!char.IsUpper(name[i - 1]) || (i + 1 < name.Length && char.IsLower(name[i + 1]))))
                sb.Append(' ');
            sb.Append(ch);
        }
        return sb.ToString();
    }
}
