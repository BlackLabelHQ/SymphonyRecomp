using RecompOne.Runtime.Memory;

namespace Recompiled.Translation;

internal readonly struct ScriptMemory(IMemory memory, string overlay, uint cursor, bool layout = true) : IMemory
{
    private sealed record Buffer(ScriptWrite Script, uint Base, Dictionary<uint, uint> Labels);
    private static readonly Dictionary<string, Buffer[]> Scripts = new(StringComparer.OrdinalIgnoreCase);
    private static readonly Dictionary<string, Dictionary<uint, uint>> Active = new(StringComparer.OrdinalIgnoreCase);
    private static readonly List<Buffer> Buffers = [];

    private static bool _executing;

    internal static bool EndFrame()
    {
        bool executing = _executing;
        _executing = false;
        return executing;
    }

    internal static void Load(IEnumerable<ScriptWrite> scripts)
    {
        DialogueGlyphs.Reset();
        Scripts.Clear();
        Active.Clear();
        Buffers.Clear();
        foreach (var script in scripts) Register(script);
    }

    internal static uint Register(ScriptWrite script)
    {
        if (Buffers.Any(b => b.Script.Key == script.Key))
            throw new InvalidDataException($"Script already registered: {script.Key}.");
        if (Buffers.Count >= 32) throw new InvalidDataException("The script buffer capacity is 32 scripts.");

        uint address = 0x40000000u + (uint)Buffers.Count * 0x1000000u;
        var buffer = new Buffer(script, address, script.Labels.ToDictionary(p => p.Key, p => address + (uint)p.Value));

        Buffers.Add(buffer);
        Scripts.TryGetValue(script.Overlay, out var existing);
        Scripts[script.Overlay] = [.. existing ?? [], buffer];
        return address;
    }

    internal static uint GetAddress(string id, string overlay)
    {
        var buffer = Buffers.FirstOrDefault(b => b.Script.Key == id && b.Script.Overlay.Equals(overlay, StringComparison.OrdinalIgnoreCase)) ?? throw new KeyNotFoundException($"No script {id} for overlay {overlay}.");
        if (buffer.Script.Original.Length != 0 && (!Active.TryGetValue(overlay, out var labels) || !labels.ContainsKey(buffer.Script.Address)))
            throw new InvalidOperationException("The script overlay has not been loaded or doesnt match the disc.");

        return buffer.Base;
    }

    internal static void Activate(string overlay, IMemory memory)
    {
        if (!Scripts.TryGetValue(overlay, out var scripts)) return;
        if (!Active.TryGetValue(overlay, out var labels)) Active.Add(overlay, labels = []);

        foreach (var buffer in scripts)
        {
            if (buffer.Script.Original.Length != 0 && labels.ContainsKey(buffer.Script.Address)) continue;
            bool original = true;

            for (int i = 0; i < buffer.Script.Original.Length; i++)
                if (memory.ReadU8(buffer.Script.Address + (uint)i) != buffer.Script.Original[i]) { original = false; break; }

            if (!original) continue;

            foreach (var label in buffer.Labels)
            {
                if (labels.TryGetValue(label.Key, out uint existing) && existing != label.Value)
                    throw new InvalidDataException($"Conflicting script label at {label.Key:X8}.");

                labels[label.Key] = label.Value;
            }
        }
    }

    internal static bool Enabled(string overlay, IMemory memory, bool dialogue = false)
    {
        GameLanguage.EnsureLoaded();
        _executing |= dialogue;
        Activate(overlay, memory);
        return Scripts.ContainsKey(overlay);
    }

    internal IMemory Native => memory;
    internal uint Cursor => cursor;
    internal bool Layout => layout;
    internal int LineCount => Ferryman ? 3 : 5;
    internal bool Richter => overlay.Equals("st0", StringComparison.OrdinalIgnoreCase);
    internal bool Ferryman => overlay.Equals("no4", StringComparison.OrdinalIgnoreCase);

    internal bool TryGlyph(out int code)
    {
        uint position = memory.ReadU32(cursor);
        if (position > 0x40000000 && position < 0x60000000)
        {
            uint previous = position - 1;
            int slot = (int)((previous - 0x40000000) >> 24);
            if ((uint)slot < (uint)Buffers.Count && Buffers[slot].Script.Glyphs.TryGetValue((int)(previous & 0xFFFFFF), out code))
                return true;
        }
        code = 0;
        return false;
    }

    private uint Resolve(uint address)
    {
        uint canonical = address < 0x200000 ? address | 0x80000000u : address;
        return Active.TryGetValue(overlay, out var labels) && labels.TryGetValue(canonical, out uint relocated) ? relocated : address;
    }

    private bool IsCursor(uint address) => address == cursor || address == cursor + 0x40 || address == cursor + 0x3C;

    private static bool Virtual(uint address) => address >= 0x40000000 && address < 0x60000000;

    public byte ReadU8(uint address)
    {
        if (!Virtual(address)) return (byte)DialogueGlyphs.ReadLayout(this, address, memory.ReadU8(address));
        int slot = (int)((address - 0x40000000) >> 24);
        uint offset = address & 0xFFFFFF;

        if ((uint)slot >= (uint)Buffers.Count || offset >= Buffers[slot].Script.Replacement.Length)
            throw new InvalidDataException($"Script read outside its buffer at {address:X8}.");

        return Buffers[slot].Script.Replacement[offset];
    }

    public ushort ReadU16(uint address) => Virtual(address) ? (ushort)(ReadU8(address) | ReadU8(address + 1) << 8) : (ushort)DialogueGlyphs.ReadLayout(this, address, memory.ReadU16(address));
    public uint ReadU32(uint address)
    {
        if (Virtual(address)) return (uint)(ReadU8(address) | ReadU8(address + 1) << 8 | ReadU8(address + 2) << 16 | ReadU8(address + 3) << 24);

        uint value = memory.ReadU32(address);
        
        return IsCursor(address) ? Resolve(value) : value;

        
    }
    public void WriteU32(uint address, uint value) => memory.WriteU32(address, IsCursor(address) ? Resolve(value) : value);
    public void WriteU8(uint address, byte value) => memory.WriteU8(address, (byte)DialogueGlyphs.WriteLayout(this, address, value));
    public void WriteU16(uint address, ushort value) => memory.WriteU16(address, (ushort)DialogueGlyphs.WriteLayout(this, address, value));
    public uint ReadWordLeft(uint current, uint address) => memory.ReadWordLeft(current, address);
    public uint ReadWordRight(uint current, uint address) => memory.ReadWordRight(current, address);
    public void WriteWordLeft(uint address, uint value) => memory.WriteWordLeft(address, value);
    public void WriteWordRight(uint address, uint value) => memory.WriteWordRight(address, value);
    public void LoadBytes(uint address, byte[] data) => memory.LoadBytes(address, data);
    public void ZeroRange(uint address, uint length) => memory.ZeroRange(address, length);
    public void SetCd(RecompOne.Runtime.Cdrom.CdController cd) => memory.SetCd(cd);
}
