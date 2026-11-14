using Hexa.NET.ImGui;
using RecompOne.UI;

namespace Recompiled.Translation;

internal static class TranslationSettings
{
    private static IReadOnlyList<LanguagePackInfo>? _packs;
    private static string? _scanError;

    public static void Register()
    {
        SettingsPages.Page(SettingsPages.Language)
            .Add("game", Draw, 100, "settings.language.game");
    }

    private static void Draw()
    {
        if (_packs == null) Rescan();

        string selected = GameLanguage.Pack;
        string original = Localization.T("translation.original");
        string label = selected == GameLanguage.OriginalPack
            ? original
            : _packs!.FirstOrDefault(p => p.Id == selected)?.Name ?? selected;

        var packs = _packs!.ToList();
        string[] items = [original, .. packs.Select(static p => p.Name)];
        int index = selected == GameLanguage.OriginalPack ? 0 : packs.FindIndex(p => p.Id == selected) + 1;
        if (index == 0 && selected != GameLanguage.OriginalPack) index = -1;

        ImGui.SetNextItemWidth(-1f);
        if (Ui.Combo("##game-language", label, items, ref index))
            GameLanguage.Select(index == 0 ? GameLanguage.OriginalPack : packs[index - 1].Id);

        SettingsWidgets.Note("translation.hint");

        if (GameLanguage.Pending)
            ImGui.TextWrapped(Localization.T("translation.applying"));
        if (Ui.Button(Localization.T("translation.rescan"))) Rescan();
        if (_scanError != null) ImGui.TextWrapped(_scanError);
        if (TextPatches.LoadError != null) ImGui.TextWrapped(TextPatches.LoadError);
        if (GameLanguage.Error != null)
        {
            ImGui.TextWrapped(Localization.T("translation.load_error"));
            ImGui.TextWrapped(GameLanguage.Error);
        }
    }

    private static void Rescan()
    {
        _packs = LanguagePacks.Scan(out _scanError);
    }
}
