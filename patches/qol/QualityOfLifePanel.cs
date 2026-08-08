using System.Numerics;
using ImGuiNET;
using RecompOne.Runtime.Host;
using RecompOne.Runtime.Host.Window;

namespace Recompiled;

public sealed class QualityOfLifePanel : IPanel
{
    public string Name => "Quality Of Life Options";
    public bool IsOpen { get; set; }

    public void Draw()
    {
        ImGui.SetNextWindowSize(new Vector2(320, 420) * HostWindow.DpiScale, ImGuiCond.FirstUseEver);
        bool open = IsOpen;
        if (!ImGui.Begin(Name, ref open))
        {
            IsOpen = open;
            ImGui.End();
            return;
        }

        var m = RecompOne.Runtime.Runtime.Mem;
        if (m == null || !Cheats.InPlay())
        {
            ImGui.TextDisabled("Not in gameplay.");
            IsOpen = open;
            ImGui.End();
            return;
        }

        ImGui.SeparatorText("Toggles");
        bool dirty = false;
        dirty |= ImGui.Checkbox("Color Blind Fixes", ref QualityOfLife.ColorBlind);
        ImGui.SetItemTooltip("Makes relics easier to distinguish for colorblind players.");
        dirty |= ImGui.Checkbox("Remove Screen Flashes", ref QualityOfLife.RemoveFlashing);
        ImGui.SetItemTooltip("Removes flashing screens from certain items and effects.");
        dirty |= ImGui.Checkbox("Bug Fixes", ref QualityOfLife.BugFixes);
        ImGui.SetItemTooltip("Various bug fixes and crash prevention items.");
        dirty |= ImGui.Checkbox("Clear File", ref QualityOfLife.ClearFile);
        ImGui.SetItemTooltip("Removes the need for a clear file already on the memory card.");
        dirty |= ImGui.Checkbox("No Screen Freeze", ref QualityOfLife.AntiFreeze);
        ImGui.SetItemTooltip("Removes the screen freeze on level-up or relic acquisitions.");
        dirty |= ImGui.Checkbox("Infinite Wing Smash", ref QualityOfLife.InfiniteWingSmash);
        ImGui.SetItemTooltip("Make Wing Smash continue forever like in the Saturn version.");
        dirty |= ImGui.Checkbox("Easy Mode", ref QualityOfLife.EasyMode);
        ImGui.SetItemTooltip("Increases the invincibility frames by 4 frames on everything \nwhich already gives them. Also makes spell inputs, gravity \njumps and Wing Smashes all easier to input through use of L2: \n- L2: Gravity Jump\n- L2 + Up + Square: Soul Steal\n- L2 + Dn + Square: Tetra Spirit\n- L2 + Lf or Ri + Square: Hellfire\n- L2 in Bat: Wing Smash");

        /* Enhancements */
        ImGui.SeparatorText("Enhancements");
        dirty |= ImGui.Checkbox("Restore Fairy Nocturne Song", ref QualityOfLife.RestoreFairySong);
        ImGui.SetItemTooltip("If you have the Sprite familiar, otherwise known as the Pixie familiar, summoned and you're\n sitting in a chair idle for 1 minute, it will make her sing the song 'Nocturne,' in Japanese.");

        if (dirty) QualityOfLife.Save();

        IsOpen = open;
        ImGui.End();
    }
}
