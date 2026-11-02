using System.Text;
using System.Text.Json;

namespace Recompiled.Translation;

internal sealed class TextCodec
{
    private readonly Dictionary<string, byte> _glyphs = new(StringComparer.Ordinal);
    private readonly Encoding _sjis;

    public TextCodec(string root)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        _sjis = Encoding.GetEncoding(932, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);
        string path = Path.Combine(root, "fonts", "encodings.json");

        if (!File.Exists(path)) return;

        using var json = JsonDocument.Parse(File.ReadAllText(path));
        int index = 0;

        foreach (var value in json.RootElement.GetProperty("sotn-glyph-8").GetProperty("characters").EnumerateArray())
            _glyphs[value.GetString()!] = checked((byte)index++);
    }

    public byte[] Encode(string text, string encoding, bool itemDescription = false)
    {
        var bytes = new List<byte>();
        switch (encoding)
        {
            case "sotn-glyph-8":
                foreach (var rune in text.EnumerateRunes())
                {
                    string character = rune.ToString();
                    string parts = character.Normalize(NormalizationForm.FormD);

                    if (parts.Length == 2 && parts[1] is '\u3099' or '\u309A')
                    {
                        bytes.Add(Glyph(parts[..1]));
                        bytes.Add(255);
                        bytes.Add(parts[1] == '\u3099' ? (byte)158 : (byte)159);
                    }
                    else if (Glyph(character) == 255) 
                        bytes.AddRange([255, 255]);
                    else 
                        bytes.Add(Glyph(character));
                }
                bytes.AddRange([255, 0]);
                break;
            case "sotn-menu-stat":
                foreach (char c in text)
                {
                    int index = "\uFF21\uFF34\uFF24\uFF25\uFF26".IndexOf(c);
                    if (index < 0) throw new InvalidDataException($"Unsupported stat glyph: {c}");
                    bytes.Add((byte)index);
                }
                bytes.AddRange([255, 0]);
                break;
            case "ascii":
            case "shift-jis":
                if (itemDescription) text = text.Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\n', '@').Replace('\r', '@');
                AddText(bytes, text);
                bytes.Add(0);
                break;
            case "credits-mixed":
                foreach (var rune in text.EnumerateRunes())
                {
                    if (rune.Value == 0xA9) bytes.AddRange([0x9F, 0x54]);
                    else if (rune.Value < 32) throw new InvalidDataException("Use script events for text controls.");
                    else bytes.AddRange(_sjis.GetBytes(Cp932Aliases(rune.ToString())));
                }
                break;
            case "cutscene-ascii":
            case "prologue-ascii":
                foreach (char c in text)
                {
                    if (c < 32 || c > 126) throw new InvalidDataException("Dialogue characters must use the original printable byte range.");
                    bytes.Add((byte)c);
                }
                break;
            default: throw new InvalidDataException($"Unknown text encoding: {encoding}");
        }
        return bytes.ToArray();
    }

    private void AddText(List<byte> bytes, string text)
    {
        int start = 0;
        for (int i = 0; i <= text.Length; i++)
        {
            if (i < text.Length && text[i] is < '\uE000' or > '\uE0FF') continue;
            if (i > start) bytes.AddRange(_sjis.GetBytes(Cp932Aliases(text[start..i])));
            if (i < text.Length) bytes.Add((byte)(text[i] - '\uE000'));
            start = i + 1;
        }
    }

    private byte Glyph(string character) => _glyphs.TryGetValue(character, out var code)
        ? code : throw new InvalidDataException($"Missing glyph mapping: {character}");

    internal static string Cp932Aliases(string value) => value.Replace('\u2212', '\uFF0D').Replace('\u301C', '\uFF5E').Replace('\u2016', '\u2225').Replace('\u00A2', '\uFFE0').Replace('\u00A3', '\uFFE1').Replace('\u00AC', '\uFFE2');
}
