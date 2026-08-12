using System.Numerics;
using ImGuiNET;
using RecompOne.Runtime.Host.Window;

namespace Recompiled;

public sealed class RandoPanel : IPanel
{
    public string Name => "Randomizer Options";
    public string TitleKey => "panel.randomizer";
    public bool IsOpen { get; set; }

    public void Draw()
    {
        Randomizer.EnsureSeedLoaded();

        bool AlreadyRandomized = false;
        byte CurrentPreset = 0;
        byte StageId;

        ImGui.SetNextWindowSize(new Vector2(320, 420), ImGuiCond.FirstUseEver);
        bool open = IsOpen;
        if (!ImGui.Begin(this.Title(), ref open))
        {
            IsOpen = open;
            ImGui.End();
            return;
        }

        var m = RecompOne.Runtime.Runtime.Mem;
        if (m == null)
        {
            ImGui.TextDisabled(Localization.T("rando.no_memory"));
            IsOpen = open;
            ImGui.End();
            return;
        }

        CurrentPreset = m.ReadU8(0x8000C000);
        StageId = m.ReadU8(0x800974A0);

        if (CurrentPreset != (byte)PresetId.None && CurrentPreset != (byte)PresetId.Integrated)
        {
            ImGui.TextDisabled(Localization.T("rando.external_seed"));
            IsOpen = open;
            ImGui.End();
            return;
        }

        if (StageId != 0x45 && CurrentPreset == (byte)PresetId.None)
        {
            ImGui.TextDisabled(Localization.T("rando.title_screen_only"));
            IsOpen = open;
            ImGui.End();
            return;
        }

        if (CurrentPreset == (byte)PresetId.Integrated)
            AlreadyRandomized = true;

        if (AlreadyRandomized == true)
            ImGui.BeginDisabled();

        ImGui.SeparatorText(Localization.T("rando.toggles"));
        Toggle("rando.items", ref Randomizer.RandomizeItems);
        Toggle("rando.drops", ref Randomizer.RandomizeDrops);
        Toggle("rando.relics", ref Randomizer.RandomizeRelics);
        Toggle("rando.starting_gear", ref Randomizer.RandomizeStartingGear);
        Toggle("rando.remove_death", ref Randomizer.RemoveDeathFromEntrance);
        Toggle("rando.skip_prologue", ref Randomizer.SkipPrologue);
        ImGui.SeparatorText("");

        ImGui.InputInt(Localization.T("rando.seed"), ref Randomizer.SeedNumber);
        if (ImGui.Button(Localization.T("rando.generate_seed")))
            Randomizer.RandomizeSeedNumber();

        ImGui.SeparatorText("");

        if (ImGui.Button(Localization.T("rando.randomize")))
            Randomizer.RandomizeSeed();

        if (AlreadyRandomized == true)
        {
            ImGui.Text(Localization.T("rando.applied"));
            ImGui.Text(Localization.T("rando.applied_seed", Randomizer.SeedNumber));
            ImGui.EndDisabled();
        }

        ImGui.SeparatorText("");
        ImGui.Text(Localization.T("rando.credit"));

        IsOpen = open;
        ImGui.End();
    }

    static void Toggle(string key, ref bool value)
    {
        ImGui.Checkbox(Localization.T(key), ref value);
        ImGui.SetItemTooltip(Localization.T(key + ".hint"));
    }
}
