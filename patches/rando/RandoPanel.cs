using System.Numerics;
using ImGuiNET;
using RecompOne.Runtime.Host.Window;

namespace Recompiled;

public sealed class RandoPanel : IPanel
{
    public string Name => "Randomizer Options";
    public bool IsOpen { get; set; }

    public void Draw()
    {
        Randomizer.EnsureSeedLoaded();

        bool AlreadyRandomized = false;
        byte CurrentPreset = 0;
        byte StageId;

        ImGui.SetNextWindowSize(new Vector2(320, 420), ImGuiCond.FirstUseEver);
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
            ImGui.TextDisabled("Missing memory context.");
            IsOpen = open;
            ImGui.End();
            return;
        }

        CurrentPreset = m.ReadU8(0x8000C000);
        StageId = m.ReadU8(0x800974A0);

        if (CurrentPreset != (byte)PresetId.None && CurrentPreset != (byte)PresetId.Integrated)
        {
            ImGui.TextDisabled("Built in randomizer cannot be combined\nwith externally randomized seeds.");
            IsOpen = open;
            ImGui.End();
            return;
        }

        if (StageId != 0x45 && CurrentPreset == (byte)PresetId.None)
        {
            ImGui.TextDisabled("Return to the title screen\nto use the randomizer.");
            IsOpen = open;
            ImGui.End();
            return;
        }

        if (CurrentPreset == (byte)PresetId.Integrated)
            AlreadyRandomized = true;

        if (AlreadyRandomized == true)
            ImGui.BeginDisabled();

        ImGui.SeparatorText("Toggles");
        ImGui.Checkbox("Randomize Items", ref Randomizer.RandomizeItems);
        ImGui.SetItemTooltip("Clever tooltip here.");
        ImGui.Checkbox("Randomize Drops", ref Randomizer.RandomizeDrops);
        ImGui.SetItemTooltip("Clever tooltip here.");
        ImGui.Checkbox("Randomize Relics", ref Randomizer.RandomizeRelics);
        ImGui.SetItemTooltip("Clever tooltip here.");
        ImGui.Checkbox("Randomize Starting Gear", ref Randomizer.RandomizeStartingGear);
        ImGui.SetItemTooltip("Clever tooltip here.");
        ImGui.Checkbox("Remove Death at Entrance", ref Randomizer.RemoveDeathFromEntrance);
        ImGui.SetItemTooltip("Clever tooltip here.");
        ImGui.SeparatorText("");


        ImGui.InputInt("Seed Number", ref Randomizer.SeedNumber);
        if (ImGui.Button("Generate New Random Seed Number"))
            Randomizer.RandomizeSeedNumber();

        ImGui.SeparatorText("");

        if (ImGui.Button("Randomize Game"))
            Randomizer.RandomizeSeed();

        if (AlreadyRandomized == true)
        {
            ImGui.Text("Randomization Applied!");
            ImGui.Text("Seed Number:" + Randomizer.SeedNumber);
            ImGui.EndDisabled();
        }

        ImGui.SeparatorText("");
        ImGui.Text("Randomizer By: MottZilla");

        IsOpen = open;
        ImGui.End();
    }
}
