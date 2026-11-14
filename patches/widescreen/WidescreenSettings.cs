using Hexa.NET.ImGui;
using RecompOne.Runtime.Config;
using RecompOne.UI;

namespace Recompiled;

public static class WidescreenOptions
{
    public static readonly Option<float> Aspect = Options.Float("widescreen.aspect", 16f / 9f)
        .Normalized(static v => Math.Clamp(v, 1f, 3f));
    public static readonly Option<bool> OriginalAspect = Options.Bool("widescreen.original_aspect", false);
    public static readonly Option<bool> Letterbox = Options.Bool("widescreen.letterbox", false);
    public static readonly Option<bool> Unstretch = Options.Bool("widescreen.unstretch", true);
}

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
        SettingsPages.Page(SettingsPages.Display)
            .Add("widescreen", Draw, 150, "settings.display.widescreen");
    }

    static void Draw()
    {
        float aspect = WidescreenOptions.Aspect;
        bool original = WidescreenOptions.OriginalAspect;
        int selected = MatchPreset(aspect);
        var originalLabel = Localization.T("widescreen.original");
        var customLabel = Localization.T("settings.interface.custom");
        string label = original ? originalLabel : selected >= 0 ? _presets[selected].Label : customLabel;

        string[] items = [originalLabel, .. _presets.Select(static p => p.Label), customLabel];
        int index = original ? 0 : selected >= 0 ? selected + 1 : items.Length - 1;

        Ui.Text(Localization.T("widescreen.aspect"));
        if (Ui.Combo("##aspect-preset", label, items, ref index))
        {
            if (index == 0) WidescreenOptions.OriginalAspect.Value = true;
            else Apply(index <= _presets.Length ? _presets[index - 1].Value : aspect);
        }

        if (original) ImGui.BeginDisabled();
        float custom = aspect;
        if (Ui.Slider("##aspect-custom", ref custom, 1.0f, 2.5f, "%.3f : 1"))
            Apply(custom);
        if (original) ImGui.EndDisabled();

        //ImGui.TextWrapped("");

        ImGui.Spacing();
        SettingsWidgets.Toggle(WidescreenOptions.Unstretch, "widescreen.unstretch", "widescreen.unstretch_hint");

        if (original) ImGui.BeginDisabled();
        SettingsWidgets.Toggle(WidescreenOptions.Letterbox, "widescreen.letterboxing", "widescreen.letterboxing_hint"); //pillar is on the sides dumass
        if (original) ImGui.EndDisabled();
    }

    static int MatchPreset(float aspect)
    {
        for (int i = 0; i < _presets.Length; i++)
            if (MathF.Abs(_presets[i].Value - aspect) < 0.001f) return i;
        return -1;
    }

    static void Apply(float aspect)
    {
        WidescreenOptions.Aspect.Value = aspect;
        WidescreenOptions.OriginalAspect.Value = false;
    }
}
