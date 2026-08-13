using ImGuiNET;
using RecompOne.Runtime.Hle;
using RecompOne.Runtime.Host.Window;

namespace Recompiled;

public static class WidescreenSettings
{
    static readonly (string Label, float Value)[] _presets =
    [
        ("4:3", 4f / 3f),
        ("16:10", 16f / 10f),
        ("16:9", 16f / 9f),
        ("21:9", 21f / 9f),
    ];

    public static void Register()
    {
        SettingsRegistry.Extend("display", Draw);
    }

    static void Draw()
    {
        ImGui.Spacing();

        float aspect = RecompOne.Runtime.Runtime.View.GetFloat("WidescreenAspect", 16f / 9f);
        bool original = WidescreenPatch.OriginalAspect;
        int selected = MatchPreset(aspect);
        var originalLabel = Localization.T("widescreen.original");
        var customLabel = Localization.T("settings.interface.custom");
        string label = original ? originalLabel : selected >= 0 ? _presets[selected].Label : customLabel;

        ImGui.TextUnformatted(Localization.T("widescreen.aspect"));
        if (ImGui.BeginCombo("##aspect-preset", label))
        {
            if (ImGui.Selectable(originalLabel, original)) ApplyOriginal(true);
            for (int i = 0; i < _presets.Length; i++)
            {
                if (ImGui.Selectable(_presets[i].Label, !original && selected == i))
                    Apply(_presets[i].Value);
            }
            if (ImGui.Selectable(customLabel, !original && selected < 0))
                Apply(aspect);
            ImGui.EndCombo();
        }

        if (original) ImGui.BeginDisabled();
        float custom = aspect;
        if (ImGui.SliderFloat("##aspect-custom", ref custom, 1.0f, 2.5f, "%.3f : 1"))
            Apply(custom);
        if (original) ImGui.EndDisabled();

        ImGui.Spacing();
        ImGui.PushStyleColor(ImGuiCol.Text, ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled]);
        //ImGui.TextWrapped("");
        ImGui.PopStyleColor();

        ImGui.Spacing();
        bool pillarBoxing = WidescreenPatch.PillarBoxing;
        if (ImGui.Checkbox(Localization.T("widescreen.letterboxing"), ref pillarBoxing)) //pillar is on the sides dumass
            ApplyPillarBoxing(pillarBoxing);

        ImGui.PushStyleColor(ImGuiCol.Text, ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled]);
        ImGui.TextWrapped(Localization.T("widescreen.letterboxing_hint"));
        ImGui.PopStyleColor();

        ImGui.Spacing();
        bool unstretch = WidescreenPatch.Unstretch;
        if (ImGui.Checkbox(Localization.T("widescreen.unstretch"), ref unstretch))
            ApplyUnstretch(unstretch);

        ImGui.PushStyleColor(ImGuiCol.Text, ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled]);
        ImGui.TextWrapped(Localization.T("widescreen.unstretch_hint"));
        ImGui.PopStyleColor();
    }

    static void ApplyUnstretch(bool unstretch)
    {
        WidescreenPatch.Unstretch = unstretch;
        RecompOne.Runtime.Runtime.View.SetBool("WidescreenUnstretch", unstretch);
        WidescreenPatch.Refresh();
        RecompOne.Runtime.Runtime.SaveView();
    }

    static void ApplyPillarBoxing(bool pillarBoxing)
    {
        WidescreenPatch.PillarBoxing = pillarBoxing;
        RecompOne.Runtime.Runtime.View.SetBool("WidescreenPillarBoxing", pillarBoxing);
        RecompOne.Runtime.Runtime.SaveView();
    }

    static int MatchPreset(float aspect)
    {
        for (int i = 0; i < _presets.Length; i++)
            if (MathF.Abs(_presets[i].Value - aspect) < 0.001f) return i;
        return -1;
    }


    static void ApplyOriginal(bool original)
    {
        WidescreenPatch.OriginalAspect = original;
        RecompOne.Runtime.Runtime.View.SetBool("WidescreenOriginalAspect", original);
        WidescreenPatch.Refresh();
        RecompOne.Runtime.Runtime.SaveView();
    }

    static void Apply(float aspect)
    {
        aspect = Math.Clamp(aspect, 1.0f, 3.0f);

        if (WidescreenPatch.OriginalAspect)
            RecompOne.Runtime.Runtime.ShowNotice(Localization.T("widescreen.warning"));

        WidescreenPatch.OriginalAspect = false;
        RecompOne.Runtime.Runtime.View.SetBool("WidescreenOriginalAspect", false);

        RecompOne.Runtime.Runtime.View.SetFloat("WidescreenAspect", aspect);
        Display.TargetAspect = aspect;
        WidescreenPatch.StageAspect = aspect;
        WidescreenPatch.Refresh();
        RecompOne.Runtime.Runtime.SaveView();
    }
}
