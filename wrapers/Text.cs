using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using RecompOne.Runtime.Memory;

namespace Sotn;

public static class Text
{
    public const int MaxLength = 31;
    public const byte Terminator = 0xFF;
    public const byte DigitPrefix = 0x82;
    public const byte SymbolPrefix = 0x81;

    static IMemory M => RecompOne.Runtime.Runtime.Mem!;

    static readonly Dictionary<byte, char> Symbols = new()
    {
        { 0x43, ',' }, { 0x44, '.' }, { 0x46, ':' }, { 0x47, ';' },
        { 0x48, '?' }, { 0x49, '!' }, { 0x4D, '`' }, { 0x4E, '"' },
        { 0x4F, '^' }, { 0x51, '_' }, { 0x60, '~' }, { 0x66, '\'' },
        { 0x69, '(' }, { 0x6A, ')' }, { 0x6D, '[' }, { 0x6E, ']' },
        { 0x6F, '{' }, { 0x70, '}' }, { 0x7B, '+' }, { 0x7C, '-' },
    };

    public static string Read(uint address, int maxLength = MaxLength)
    {
        var sb = new StringBuilder();
        bool digit = false;
        bool symbol = false;

        for (int i = 0; i < maxLength; i++)
        {
            byte b = M.ReadU8(address + (uint)i);
            if (b == Terminator || b == 0) break;

            if (b == DigitPrefix) { digit = true; continue; }
            if (b == SymbolPrefix) { symbol = true; continue; }

            if (digit)
            {
                digit = false;
                sb.Append((char)('0' + (b - 79)));
            }
            else if (symbol)
            {
                symbol = false;
                if (Symbols.TryGetValue(b, out var c)) sb.Append(c);
            }
            else
            {
                sb.Append((char)(b + 32));
            }
        }

        return sb.ToString();
    }

    public static string ReadPreset(uint address)
    {
        string preset = Read(address).Trim();
        var match = Regex.Match(preset, @" ([a-z.-]{2,15})( ){0,1}", RegexOptions.IgnoreCase);
        return match.Value.Trim();
    }

    public static void Write(uint address, string text, bool safe = false, int maxLength = MaxLength)
    {
        if (string.IsNullOrEmpty(text)) return;

        bool endReached = false;

        for (int i = 0; i < maxLength; i++)
        {
            bool atEnd = M.ReadU8(address + (uint)i) == Terminator;
            if (atEnd)
            {
                endReached = true;
                if (safe) return;
            }

            if (i < text.Length)
            {
                M.WriteU8(address + (uint)i, (byte)(text[i] - 32));
            }
            else if (endReached)
            {
                M.WriteU8(address + (uint)i, Terminator);
                break;
            }
            else if (!atEnd)
            {
                M.WriteU8(address + (uint)i, 0);
            }
        }
    }

    public static string ReadRaw(uint address, int length)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            byte b = M.ReadU8(address + (uint)i);
            if (b == 0 || b == Terminator) break;
            sb.Append((char)(b + 32));
        }
        return sb.ToString();
    }

    public static void WriteRaw(uint address, string text, int length)
    {
        for (int i = 0; i < length; i++)
            M.WriteU8(address + (uint)i, i < text.Length ? (byte)(text[i] - 32) : (byte)0);
    }
}
