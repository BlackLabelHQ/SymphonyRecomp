using System.Numerics;
using Hexa.NET.ImGui;
using RecompOne.UI;

namespace Recompiled;

public sealed class RandoPanel : IEmbeddedPanel
{
    public string Name => "Randomizer Options";
    public string TitleKey => "panel.randomizer";
    public bool IsOpen { get; set; }

    public void Draw()
    {
        ImGui.SetNextWindowSize(new Vector2(320, 420), ImGuiCond.FirstUseEver);
        bool open = IsOpen;
        if (!ImGui.Begin(this.Title(), ref open))
        {
            IsOpen = open;
            ImGui.End();
            return;
        }

        DrawContent();

        IsOpen = open;
        ImGui.End();
    }

    public void DrawContent()
    {
        Randomizer.EnsureSeedLoaded();

        bool AlreadyRandomized = false;
        byte CurrentPreset = 0;
        byte StageId;

        var m = RecompOne.Runtime.Runtime.Mem;
        if (m == null)
        {
            Ui.Note(Localization.T("rando.no_memory"));
            return;
        }

        CurrentPreset = m.ReadU8(0x8000C000);
        StageId = m.ReadU8(0x800974A0);

        if (CurrentPreset != (byte)PresetId.None && CurrentPreset != (byte)PresetId.Integrated)
        {
            Ui.Note(Localization.T("rando.external_seed"));
            return;
        }

        if (StageId != 0x45 && CurrentPreset == (byte)PresetId.None)
        {
            Ui.Note(Localization.T("rando.title_screen_only"));
            return;
        }

        if (CurrentPreset == (byte)PresetId.Integrated)
            AlreadyRandomized = true;

        if (AlreadyRandomized == true)
            ImGui.BeginDisabled();

        Ui.Section(Localization.T("rando.toggles"));
        Toggle("rando.items", ref Randomizer.RandomizeItems);
        Toggle("rando.drops", ref Randomizer.RandomizeDrops);
        Toggle("rando.relics", ref Randomizer.RandomizeRelics);
        Toggle("rando.starting_gear", ref Randomizer.RandomizeStartingGear);
        Toggle("rando.remove_death", ref Randomizer.RemoveDeathFromEntrance);
        Toggle("rando.skip_prologue", ref Randomizer.SkipPrologue);
        Ui.Separator();

        Ui.InputInt(Localization.T("rando.seed"), ref Randomizer.SeedNumber);
        if (Ui.Button(Localization.T("rando.generate_seed")))
            Randomizer.RandomizeSeedNumber();

        Ui.Separator();

        if (Ui.Button(Localization.T("rando.randomize")))
            Randomizer.RandomizeSeed();

        if (AlreadyRandomized == true)
        {
            Ui.Text(Localization.T("rando.applied"));
            Ui.Text(Localization.T("rando.applied_seed", Randomizer.SeedNumber));
            ImGui.EndDisabled();
        }

        Ui.Separator();
        Ui.Text(Localization.T("rando.credit"));
    }

    static void Toggle(string key, ref bool value)
    {
        Ui.Toggle(Localization.T(key), ref value, Localization.T(key + ".hint"));
    }
}
