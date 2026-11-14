using System.Numerics;
using Hexa.NET.ImGui;
using RecompOne.Runtime.Config;
using RecompOne.UI;

namespace Recompiled;

public sealed class QualityOfLifePanel : IEmbeddedPanel
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

        DrawContent();

        IsOpen = open;
        ImGui.End();
    }

    public void DrawContent()
    {
        Ui.Section(Localization.T("qol.toggles"));

        /* Toggles */
        Toggle(QualityOfLifeOptions.ColorBlind, "qol.color_blind");
        Toggle(QualityOfLifeOptions.RemoveFlashing, "qol.remove_flashing");
        Toggle(QualityOfLifeOptions.BugFixes, "qol.bug_fixes");
        Toggle(QualityOfLifeOptions.ClearFile, "qol.clear_file");
        Toggle(QualityOfLifeOptions.AntiFreeze, "qol.anti_freeze");
        Toggle(QualityOfLifeOptions.InfiniteWingSmash, "qol.infinite_wing_smash");
        Toggle(QualityOfLifeOptions.UseEasySpellInput, "qol.easy_spell_input");
        Toggle(QualityOfLifeOptions.IncreaseInvincibilityFrames, "qol.invincibility_frames");
        Toggle(QualityOfLifeOptions.SkipPrologue, "qol.skip_prologue");

        /* Enhancements */
        Ui.Section(Localization.T("qol.enhancements"));
        Toggle(QualityOfLifeOptions.RestoreFairySong, "qol.fairy_song");
    }

    static void Toggle(Option<bool> option, string key)
    {
        SettingsWidgets.Toggle(option, key, key + ".hint");
    }
}
