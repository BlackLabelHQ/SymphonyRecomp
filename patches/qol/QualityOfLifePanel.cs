using System.Numerics;
using ImGuiNET;
using RecompOne.Runtime.Host.Window;

namespace Recompiled;

public sealed class QualityOfLifePanel : IPanel
{
    public string Name => "Quality Of Life Options";
    public string TitleKey => "panel.qol";
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

        var m = RecompOne.Runtime.Runtime.Mem;
        if (m == null || !Cheats.InPlay())
        {
            ImGui.TextDisabled(Localization.T("common.not_in_play"));

            IsOpen = open;
            ImGui.End();
            return;
        }

        ImGui.SeparatorText(Localization.T("qol.toggles"));

        /* Toggles */
        bool dirty = false;
        dirty |= Toggle("qol.color_blind", ref QualityOfLife.ColorBlind);
        dirty |= Toggle("qol.remove_flashing", ref QualityOfLife.RemoveFlashing);
        dirty |= Toggle("qol.bug_fixes", ref QualityOfLife.BugFixes);
        dirty |= Toggle("qol.clear_file", ref QualityOfLife.ClearFile);
        dirty |= Toggle("qol.anti_freeze", ref QualityOfLife.AntiFreeze);
        dirty |= Toggle("qol.infinite_wing_smash", ref QualityOfLife.InfiniteWingSmash);
        dirty |= Toggle("qol.easy_spell_input", ref QualityOfLife.UseEasySpellInput);
        dirty |= Toggle("qol.invincibility_frames", ref QualityOfLife.IncreaseInvincibilityFrames);

        /* Enhancements */
        ImGui.SeparatorText(Localization.T("qol.enhancements"));
        dirty |= Toggle("qol.fairy_song", ref QualityOfLife.RestoreFairySong);

        if (dirty) QualityOfLife.Save();

        IsOpen = open;
        ImGui.End();
    }

    static bool Toggle(string key, ref bool value)
    {
        bool changed = ImGui.Checkbox(Localization.T(key), ref value);
        ImGui.SetItemTooltip(Localization.T(key + ".hint"));
        return changed;
    }
}
