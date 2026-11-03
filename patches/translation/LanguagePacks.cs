using System.Text.Json;

namespace Recompiled.Translation;

public sealed record LanguagePackInfo(string Id, string Name);

internal sealed record LanguagePack(
    string Id,
    FontPack Fonts,
    TextPack Text,
    DialogueFont Dialogue,
    Dictionary<int, string> Audio,
    string? Root)
{
    public static LanguagePack Original(string id) => new(id, new FontPack(), new TextPack(), new DialogueFont(), [], null);
}

public static class LanguagePacks
{
    private const string Manifest = "manifest.json";

    public static IEnumerable<string> Roots => new[]
    {
        Path.GetFullPath("translations"), Path.Combine(AppContext.BaseDirectory, "translations")
    }.Distinct(StringComparer.Ordinal);

    public static void EnsureRoot()
    {
        if (Roots.Any(Directory.Exists)) return;
        try
        {
            Directory.CreateDirectory(Path.GetFullPath("translations"));
        }
        catch (Exception e) when (e is IOException or UnauthorizedAccessException)
        {
            Console.Error.WriteLine($"[translation] cannot create the translations folder: {e.Message}");
        }
    }

    public static IReadOnlyList<LanguagePackInfo> Scan(out string? error)
    {
        error = null;
        var packs = new List<LanguagePackInfo>();
        var seen = new HashSet<string>(StringComparer.Ordinal);
        foreach (string root in Roots)
        {
            if (!Directory.Exists(root)) continue;
            try
            {
                foreach (string directory in Directory.EnumerateDirectories(root))
                {
                    string id = Path.GetFileName(directory);
                    string manifest = Path.Combine(directory, Manifest);
                    if (!File.Exists(manifest) || !seen.Add(id)) continue;
                    try
                    {
                        using var document = JsonDocument.Parse(File.ReadAllText(manifest));
                        var entry = document.RootElement;
                        string name = entry.TryGetProperty("language", out var language) ? language.GetString() ?? id : id;
                        packs.Add(new(id, name));
                    }
                    catch (Exception e) when (e is IOException or JsonException or InvalidOperationException)
                    {
                        error = $"{id}: {e.Message}";
                    }
                }
            }
            catch (Exception e) when (e is IOException or UnauthorizedAccessException)
            {
                error = e.Message;
            }
        }

        packs.Sort((a, b) => StringComparer.OrdinalIgnoreCase.Compare(a.Name, b.Name));
        return packs;
    }

    internal static LanguagePack Read(string id)
    {
        if (id == GameLanguage.OriginalPack) return LanguagePack.Original(id);
        if (id != Path.GetFileName(id) || id is "." or "..") throw new InvalidDataException("Invalid language pack directory.");

        string? root = Roots.Select(r => Path.Combine(r, id)).FirstOrDefault(r => File.Exists(Path.Combine(r, Manifest)));
        if (root == null)
        {
            if (id == GameLanguage.DefaultPack) return LanguagePack.Original(id);
            throw new FileNotFoundException($"Language pack not found: {id}");
        }

        var fonts = FontPack.Load(root);
        var dialogue = DialogueFont.Load(root);
        var text = TextPack.Load(root);
        if (text.Scripts.Count > 32 || text.Scripts.Select(s => s.Key).Distinct().Count() != text.Scripts.Count)
            throw new InvalidDataException("Invalid script registry.");
        foreach (var script in text.Scripts)
            foreach (int code in script.Glyphs.Values.Distinct())
                if (code >= 128 && dialogue.Find(code, script.Overlay == "st0") == null)
                    throw new InvalidDataException($"{script.Key}: no dialogue font has a glyph for {char.ConvertFromUtf32(code)} (U+{code:X4}).");
        return new(id, fonts, text, dialogue, DialogueAudio.Read(root), root);
    }
}
