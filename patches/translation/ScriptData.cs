using System.Text;

namespace Recompiled.Translation;

internal sealed record ScriptWrite(string Key, string Overlay, uint Address, byte[] Original, byte[] Replacement, Dictionary<uint, int> Labels)
{
    internal Dictionary<int, int> Glyphs { get; init; } = [];
    public bool Changed => Original.Length == 0 || !Original.AsSpan().SequenceEqual(Replacement);
}

internal sealed record ScriptEvents(uint Address, byte[] Data, HashSet<uint> Timelines);

internal static class ScriptData
{
    private sealed record Command(string Name, int[] Sizes);

    private static readonly Command[] Dialogue =
    [
        new("END_CUTSCENE", []), new("LINE_BREAK", []), new("SET_SPEED", [1]), new("SET_WAIT", [1]),
        new("HIDE_DIALOG", []), new("SET_PORTRAIT", [1, 1]), new("NEXT_DIALOG", []), new("SET_POS", [1, 1]),
        new("CLOSE_DIALOG", []), new("PLAY_SOUND", [2]), new("WAIT_FOR_SOUND", []), new("SCRIPT_UNKNOWN_11", []),
        new("SET_EVENTS", [4]), new("SCRIPT_UNKNOWN_13", []), new("SCRIPT_SWITCH", [4, 4, 4]),
        new("SCRIPT_UNKNOWN_15", [4]), new("WAIT_FOR_FLAG", [1]), new("SET_FLAG", [1]),
        new("SCRIPT_UNKNOWN_18", []), new("LOAD_PORTRAIT", [4, 1]), new("SCRIPT_UNKNOWN_20", [2]),
        new("SCRIPT_UNKNOWN_21", []), new("RESET_FLAG", [1]), new("SCRIPT_UNKNOWN_23", []), new("WAIT_FOR_FLAG_RESET", [1])
    ];
    private static readonly Command[] Prologue = [new("END", []), new("LINE_BREAK", [1]), new("PARAGRAPH_BREAK", [1])];
    private static readonly Command[] Credits = [new("END_CREDITS", []), new("CREDITS_SUBTEXT", [1]), new("CREDITS_ENTRY", [1]), new("CREDITS_SECTION", [1])];
    private static readonly string[] Timeline = ["SPAWN_ENTITY", "DESTROY_ENTITY", "WAIT_FOR_FLAG", "SET_FLAG"];

    private const int Switch = 14;
    private const int Jump = 15;
    private const int SetEvents = 12;

    public static ScriptWrite Compile(string key, string overlay, string kind, IReadOnlyList<string[]> rows, uint address,
        byte[] original, ScriptEvents? events, TextCodec codec, ref uint nextLabel)
    {
        var commands = Commands(kind);
        var layout = new int[rows.Count];
        var rowAt = new Dictionary<uint, int>();
        int length = 0;
        for (int i = 0; i < rows.Count; i++)
        {
            layout[i] = length;
            rowAt.TryAdd(address + (uint)length, i);
            length += Size(key, kind, commands, rows[i], codec, i);
        }

        var replacement = new List<byte>(length);
        var labels = new Dictionary<uint, int>();
        var glyphs = new Dictionary<int, int>();
        var targets = new Dictionary<int, uint>();
        var timelines = new Dictionary<uint, uint>();
        uint counter = nextLabel;

        uint Label(int position)
        {
            if (counter >= 0x80180000) throw new InvalidDataException("Too many script labels.");
            uint label = counter++;
            labels.Add(label, position);
            return label;
        }

        uint Target(uint value, int row)
        {
            if (rowAt.TryGetValue(value, out int target))
            {
                if (!targets.TryGetValue(target, out uint label)) targets[target] = label = Label(layout[target]);
                return label;
            }
            if (value >= address && value < address + (uint)length)
                throw Error(key, row, $"jump target 0x{value:x8} is not at the start of row.");
            return value;
        }

        uint Events(uint value, int row)
        {
            if (events == null || value < events.Address || value >= events.Address + (uint)events.Data.Length) return value;
            if (!events.Timelines.Contains(value)) throw Error(key, row, $"0x{value:x8} is not the start of an event timeline");
            if (!timelines.TryGetValue(value, out uint label))
                timelines[value] = label = Label(length + (int)(value - events.Address));
            return label;
        }

        for (int i = 0; i < rows.Count; i++)
        {
            var row = rows[i];
            string name = row[0];
            if (name == "TEXT")
            {
                Text(key, kind, row, i, codec, replacement, glyphs);
                continue;
            }
            if (name == "BYTE")
            {
                replacement.Add(Byte(key, row, i));
                continue;
            }

            int op = Opcode(commands, name);
            int[] sizes = Sizes(kind, commands, op, row.Length - 1);
            replacement.Add((byte)op);
            for (int a = 0; a < sizes.Length; a++)
            {
                long parsed = Value(key, row, i, a + 1);
                uint value = unchecked((uint)parsed);
                if (kind == "dialogue" && (op == Switch && a > 0 || op == Jump)) value = Target(value, i);
                else if (kind == "dialogue" && op == SetEvents) value = Events(value, i);
                Encode(key, i, value, sizes[a], replacement);
            }
        }

        if (replacement.Count != length) throw new InvalidDataException($"{key}: script layout changed while compiling.");
        if (timelines.Count != 0) replacement.AddRange(events!.Data);
        if (replacement.Count == 0) throw new InvalidDataException($"{key}: an empty script cannot be executed.");
        if (replacement.Count > 0xFFFFFF) throw new InvalidDataException($"{key}: script exceeds the 16 MiB buffer capacity.");
        if (original.Length > 0) labels[address] = 0;
        if (original.Length > 0 && kind == "dialogue") MapScenes(key, rows, layout, original, address, labels);

        nextLabel = counter;
        return new(key, overlay, address, original, replacement.ToArray(), labels) { Glyphs = glyphs };
    }

    public static ScriptEvents CompileEvents(string key, IReadOnlyList<string[]> rows, uint address, IReadOnlyDictionary<string, int> entities)
    {
        var data = new List<byte>();
        var timelines = new HashSet<uint> { address };
        for (int i = 0; i < rows.Count; i++)
        {
            var row = rows[i];
            if (row[0] == "END")
            {
                if (row.Length != 1) throw Error(key, i, "END takes no arguments.");
                data.AddRange([0xFF, 0xFF]);
                timelines.Add(address + (uint)data.Count);
                continue;
            }
            if (row.Length < 2) throw Error(key, i, "an event needs a timer and a command.");
            long timer = Value(key, row, i, 0);
            if (timer is < 0 or >= 0xFFFF) throw Error(key, i, "the timer must be between 0 and 65534.");
            int op = Array.IndexOf(Timeline, row[1]);
            if (op < 0) throw Error(key, i, $"unknown event {row[1]}.");
            int count = op == 0 ? 4 : 1;
            if (row.Length - 2 != count) throw Error(key, i, $"{row[1]} expects {count} argument(s).");
            data.AddRange([(byte)(timer >> 8), (byte)timer, (byte)op]);
            for (int a = 0; a < count; a++)
            {
                string text = row[a + 2];
                long value = op == 0 && a == 1 && !ScriptYaml.IsNumber(text)
                    ? entities.TryGetValue(text, out int entity) ? entity : throw Error(key, i, $"unknown entity {text}.")
                    : Value(key, row, i, a + 2);
                if (op == 0 && a >= 2)
                {
                    if (value is < 0 or > 0xFFF) throw Error(key, i, "positions must be between 0 and 4095.");
                    data.AddRange([(byte)((value & 0xFF0) >> 4), (byte)(value & 0xFF)]);
                }
                else
                {
                    if (value is < 0 or > 0xFF) throw Error(key, i, "the value must fit in one byte.");
                    data.Add((byte)value);
                }
            }
        }
        return new(address, data.ToArray(), timelines);
    }

    private static void MapScenes(string key, IReadOnlyList<string[]> rows, int[] layout, byte[] original, uint address, Dictionary<uint, int> labels)
    {
        var scenes = new List<int>();
        int end = -1;
        uint limit = address + (uint)original.Length;
        for (int i = 0; i < rows.Count; i++)
        {
            if (rows[i][0] == "SET_EVENTS" && unchecked((uint)Value(key, rows[i], i, 1)) is var events && events > address) limit = Math.Min(limit, events);
            if (rows[i][0] != "END_CUTSCENE") continue;
            end = layout[i];
            if (i + 1 < rows.Count) scenes.Add(layout[i + 1]);
        }
        if (end < 0) return;

        var starts = SceneStarts(original);
        for (int k = 0; k < starts.Count && address + (uint)starts[k] < limit; k++)
            labels.TryAdd(address + (uint)starts[k], k < scenes.Count ? scenes[k] : end);
    }

    private static List<int> SceneStarts(byte[] original)
    {
        var starts = new List<int>();
        int i = 0;
        while (i < original.Length)
        {
            byte op = original[i++];
            if (op >= 0x20) continue;
            if (op >= Dialogue.Length) return [];
            if (op == Switch)
            {
                i += 4;
                while (i + 4 <= original.Length && original[i] >= 0x80) i += 4;
            }
            else i += Dialogue[op].Sizes.Sum();
            if (op == 0 && i < original.Length && original.AsSpan(i).IndexOfAnyExcept((byte)0, (byte)0xFF) >= 0) starts.Add(i);
        }
        return starts;
    }

    private static Command[] Commands(string kind) => kind switch
    {
        "dialogue" => Dialogue,
        "prologue" => Prologue,
        "credits" => Credits,
        _ => throw new InvalidDataException($"Unsupported script kind: {kind}.")
    };

    private static int Size(string key, string kind, Command[] commands, string[] row, TextCodec codec, int index)
    {
        if (row[0] == "TEXT")
        {
            var bytes = new List<byte>();
            Text(key, kind, row, index, codec, bytes, null);
            return bytes.Count;
        }
        if (row[0] == "BYTE") return 1;
        int op = Opcode(commands, row[0]);
        int[] sizes = Sizes(kind, commands, op, row.Length - 1);
        if (sizes.Length != row.Length - 1)
            throw Error(key, index, $"{row[0]} expects {sizes.Length} argument(s) but got {row.Length - 1}.");
        return 1 + sizes.Sum();
    }

    private static int[] Sizes(string kind, Command[] commands, int op, int count)
    {
        if (kind == "dialogue" && op == Switch && count >= 2) return Enumerable.Repeat(4, count).ToArray();
        return commands[op].Sizes;
    }

    private static int Opcode(Command[] commands, string name)
    {
        int op = Array.FindIndex(commands, c => c.Name == name);
        return op >= 0 ? op : throw new InvalidDataException($"unknown command {name}.");
    }

    private static void Text(string key, string kind, string[] row, int index, TextCodec codec, List<byte> output, Dictionary<int, int>? glyphs)
    {
        if (row.Length != 2) throw Error(key, index, "TEXT expects exactly one string.");
        var pending = new StringBuilder();
        void Flush()
        {
            if (pending.Length == 0) return;
            output.AddRange(codec.Encode(pending.ToString(), kind == "prologue" ? "prologue-ascii" : "credits-mixed"));
            pending.Clear();
        }
        foreach (var rune in row[1].EnumerateRunes())
        {
            if (rune.Value < 0x20)
            {
                Flush();
                output.Add((byte)rune.Value);
            }
            else if (kind != "dialogue") pending.Append(rune.ToString());
            else
            {
                glyphs?.Add(output.Count, rune.Value);
                output.Add(0x21);
            }
        }
        Flush();
    }

    private static byte Byte(string key, string[] row, int index)
    {
        if (row.Length != 2) throw Error(key, index, "BYTE expects exactly one argument.");
        long value = Value(key, row, index, 1);
        return value is >= 0 and <= 0xFF ? (byte)value : throw Error(key, index, "BYTE must be between 0 and 0xFF.");
    }

    private static void Encode(string key, int index, uint value, int size, List<byte> output)
    {
        switch (size)
        {
            case 1:
                if (value > 0xFF) throw Error(key, index, "argument must be between 0 and 255.");
                output.Add((byte)value);
                break;
            case 2:
                if (value > 0xFFF) throw Error(key, index, "argument must be between 0 and 0xFFF.");
                output.AddRange([(byte)((value & 0xFF0) >> 4), (byte)(value & 0xFF)]);
                break;
            default:
                if ((value & 0xFFF00000) != 0x80100000) throw Error(key, index, $"address 0x{value:x8} is outside 0x80100000..0x801FFFFF.");
                value &= 0xFFFFF;
                output.AddRange([(byte)((value & 0xFF000) >> 12), (byte)((value & 0xFF00) >> 8), (byte)((value & 0xFF0) >> 4), (byte)(value & 0xFF)]);
                break;
        }
    }

    private static long Value(string key, string[] row, int index, int argument)
    {
        try { return ScriptYaml.Number(row[argument]); }
        catch (Exception error) when (error is FormatException or OverflowException)
        {
            throw Error(key, index, $"'{row[argument]}' is not a number.");
        }
    }

    private static InvalidDataException Error(string key, int row, string message) => new($"{key}: row {row + 1}: {message}");
}
