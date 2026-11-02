using System.Text.Json;

namespace Recompiled.Translation;

internal sealed record TextWrite(string Key, uint Address, byte[] Original, byte[] Replacement)
{
    internal string? Reader { get; init; }
    internal uint[] Pointers { get; init; } = [];
    public bool Relocated { get; init; }
    public bool Changed => Relocated || !Original.AsSpan().SequenceEqual(Replacement);
}

internal sealed class TextPack
{
    public Dictionary<string, List<TextWrite>> Overlays { get; } = new(StringComparer.OrdinalIgnoreCase);
    public List<string> Errors { get; } = [];
    public List<ScriptWrite> Scripts { get; } = [];
    internal uint NextLabel { get; private set; }
    public int StringCount { get; private set; }
    public int ScriptCount { get; private set; }
    internal const string EventsName = "cutscene_events";

    public static TextPack Load(string root)
    {
        var pack = new TextPack();
        using var manifest = JsonDocument.Parse(File.ReadAllText(Path.Combine(root, "manifest.json")));
        var header = manifest.RootElement;

        var codec = new TextCodec(root);
        header.TryGetProperty("overlays", out var overlays);
        using var allStrings = header.TryGetProperty("strings", out var stringFile)
            ? JsonDocument.Parse(File.ReadAllText(Resolve(root, stringFile.GetString()!))) : JsonDocument.Parse("{}");
        pack.LoadStrings(allStrings.RootElement, codec);
        if (header.TryGetProperty("scripts", out var scriptFolder))
            pack.LoadScripts(Resolve(root, scriptFolder.GetString()!), overlays, codec);
        pack.ResolveOverlaps();
        return pack;
    }

    private void LoadStrings(JsonElement strings, TextCodec codec)
    {
        foreach (var property in strings.EnumerateObject())
        {
            if (!TranslationBindings.Root.GetProperty("strings").TryGetProperty(property.Name, out var locations))
            {
                Errors.Add($"{property.Name}: unknown string ID.");
                continue;
            }
            try
            {
                string text = property.Value.GetString() ?? throw new InvalidDataException("Expected a text value.");
                var pending = new List<(string Overlay, TextWrite Write)>();
                foreach (var location in locations.EnumerateArray())
                {
                    string overlay = location.GetProperty("overlay").GetString()!;
                    uint address = Convert.ToUInt32(location.GetProperty("address").GetString(), 16);

                    byte[] original = Convert.FromHexString(location.GetProperty("originalHex").GetString()!);
                    bool itemDescription = property.Name.StartsWith("item.", StringComparison.Ordinal) && property.Name.EndsWith(".description", StringComparison.Ordinal);
                    byte[] replacement = codec.Encode(text, location.GetProperty("encoding").GetString()!, itemDescription);
                    
                    uint[] pointers = location.TryGetProperty("pointers", out var referencePointers)? referencePointers.EnumerateArray().Select(p => p.GetUInt32()).ToArray() : [];
                    string? reader = location.TryGetProperty("reader", out var consumer) ? consumer.GetString() : null;
                    
                    if (replacement.Length > original.Length && pointers.Length == 0 && reader is not ("menu" or "message"))
                        throw new InvalidDataException($"Text exceeds its {original.Length}-byte slot and has no relocatable pointers.");
                    
                    if (replacement.Length <= original.Length) Array.Resize(ref replacement, original.Length);
                    
                    pending.Add((overlay, new(property.Name, address, original, replacement) { Pointers = pointers, Reader = reader, Relocated = replacement.Length > original.Length }));
                }
                
                foreach (var write in pending) Add(write.Overlay, write.Write);
                StringCount++;
            }
            catch (Exception error) when (error is InvalidDataException or System.Text.EncoderFallbackException)
            {
                Errors.Add($"{property.Name}: {error.Message}");
            }
        }
    }

    private void LoadScripts(string folder, JsonElement overlays, TextCodec codec)
    {
        uint nextLabelValue = 0x80100000;
        if (!Directory.Exists(folder))
        {
            NextLabel = nextLabelValue;
            return;
        }

        var all = Directory.EnumerateFiles(folder, "*.yaml").Order(StringComparer.Ordinal)
            .Select(file => (File: file, Name: Path.GetFileNameWithoutExtension(file).Split('_', 2))).ToList();
        foreach (var (file, _) in all.Where(entry => entry.Name.Length != 2))
            Errors.Add($"{Path.GetFileName(file)}: script files must be named <overlay>_<name>.yaml.");
        var files = all.Where(entry => entry.Name.Length == 2).GroupBy(entry => entry.Name[0]);

        foreach (var group in files)
        {
            string overlay = group.Key;
            ScriptEvents? events = null;
            try { events = TranslationBindings.Events(overlay, Path.Combine(folder, $"{overlay}_{EventsName}.yaml")); }
            catch (InvalidDataException error) { Errors.Add($"{overlay}:{EventsName}: {error.Message}"); }

            foreach (var (file, name) in group)
            {
                if (name[1] == EventsName) continue;
                string key = $"{overlay}:{name[1]}";
                try
                {
                    var region = overlays.GetProperty(overlay);
                    uint overlayBase = region.GetProperty("base").GetUInt32();
                    int loadOffset = region.GetProperty("loadOffset").GetInt32();
                    var binding = TranslationBindings.Script(key);
                    uint address = binding?.Address ?? overlayBase;
                    var rows = ScriptYaml.Read(File.ReadAllText(file), "script");
                    var write = ScriptData.Compile(key, overlay, binding?.Kind ?? "dialogue", rows, address, binding?.Original ?? [], events, codec, ref nextLabelValue);

                    uint end = checked(overlayBase + (uint)(region.GetProperty("end").GetInt32() - loadOffset));
                    if (write.Address < overlayBase || write.Address + write.Original.Length > end)
                        throw new InvalidDataException("script is outside its overlay!");

                    Scripts.Add(write);
                    ScriptCount++;
                }
                catch (Exception error) when (error is InvalidDataException or KeyNotFoundException or FormatException or OverflowException or IOException or System.Text.EncoderFallbackException)
                {
                    Errors.Add($"{key}: {error.Message}");
                }
            }
        }
        NextLabel = nextLabelValue;
    }

    private void ResolveOverlaps()
    {
        foreach (var writes in Overlays.Values)
        {
            var changed = writes.OrderBy(w => w.Address).ToArray();
            for (int i = 0; i < changed.Length; i++)
            for (int j = i + 1; j < changed.Length && changed[j].Address < changed[i].Address + changed[i].Replacement.Length; j++)
            {
                var a = changed[i];
                var b = changed[j];
                
                if (a.Relocated || b.Relocated || !a.Changed && !b.Changed) continue;
                
                uint end = Math.Min(a.Address + (uint)a.Replacement.Length, b.Address + (uint)b.Replacement.Length);
                
                for (uint address = b.Address; address < end; address++)
                    if (a.Replacement[address - a.Address] != b.Replacement[address - b.Address])
                    {
                        if (a.Pointers.Length == 0 || b.Pointers.Length == 0)
                            throw new InvalidDataException($"Conflicting overlapping texts: {a.Key} and {b.Key}.");
                            
                        writes[writes.IndexOf(a)] = changed[i] = a with { Relocated = true };
                        writes[writes.IndexOf(b)] = changed[j] = b with { Relocated = true };
                        break;
                    }
            }
        }
    }

    private void Add(string overlay, TextWrite write)
    {
        if (!Overlays.TryGetValue(overlay, out var list)) Overlays.Add(overlay, list = []);
        list.Add(write);
    }

    private static string Resolve(string root, string path)
    {
        string directory = Path.GetFullPath(root) + Path.DirectorySeparatorChar;
        string full = Path.GetFullPath(Path.Combine(root, path));
        if (!full.StartsWith(directory, StringComparison.Ordinal)) throw new InvalidDataException("Text path is outside the pack.");
        return full;
    }
}
