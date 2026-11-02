using RecompOne.Runtime.Context;
using RecompOne.Runtime.Dispatch;
using RecompOne.Runtime.Memory;

namespace Recompiled.Translation;

public static class TextPatches
{
    private static Dictionary<string, TextWrite[]> _writes = new(StringComparer.OrdinalIgnoreCase);
    private static Dictionary<uint, TextWrite> _menuTexts = [];
    private static Dictionary<uint, (string Overlay, TextWrite Write)[]> _messageTexts = [];
    private static readonly Dictionary<string, uint> Allocated = [];
    private const uint TextBase = 0x80400000;
    private const uint TextEnd = 0x80500000;
    private static uint _textNext = TextBase;
    private static readonly HashSet<string> Pending = new(StringComparer.OrdinalIgnoreCase);
    private static readonly HashSet<string> Reported = new(StringComparer.Ordinal);
    internal static string? LoadError { get; private set; }

    private sealed record Applied(uint Address, byte[] Before, byte[] After);
    private static readonly Dictionary<string, Dictionary<uint, Applied>> Changes = new(StringComparer.OrdinalIgnoreCase);

    private static bool IsCurrent(string name)
    {
        if (!Dispatcher.Overlays.TryGetValue(name, out var target)) return false;
        foreach (string active in Dispatcher.ActiveNames.Reverse())
            if (Dispatcher.Overlays.TryGetValue(active, out var candidate) && candidate.Base == target.Base)
                return active.Equals(name, StringComparison.OrdinalIgnoreCase);
        return false;
    }

    internal static void Load(TextPack pack, DialogueFont font, IMemory? memory)
    {
        if (memory != null)
            foreach (var overlay in Changes)
                if (IsCurrent(overlay.Key))
                    foreach (var change in overlay.Value.Values)
                        if (Matches(memory, change.Address, change.After)) memory.LoadBytes(change.Address, change.Before);
        Changes.Clear();
        Reported.Clear();
        Allocated.Clear();
        _textNext = TextBase;
        ScriptMemory.Load(pack.Scripts);
        DialogueGlyphs.Load(font);
        DialogueScripts.Reset(Math.Max(0x80100000u, pack.NextLabel));
        _writes = pack.Overlays.ToDictionary(p => p.Key, p => p.Value.Where(w => w.Changed).ToArray(), StringComparer.OrdinalIgnoreCase);
        _menuTexts = _writes.GetValueOrDefault("sel", []).Where(w => w.Reader == "menu" && w.Relocated).GroupBy(w => w.Address).ToDictionary(g => g.Key, g => g.First());
        _messageTexts = _writes.SelectMany(p => p.Value.Where(w => w.Reader == "message" && w.Relocated).Select(w => (Overlay: p.Key, Write: w))).GroupBy(w => w.Write.Address).ToDictionary(g => g.Key, g => g.ToArray());
        LoadError = pack.Errors.Count == 0 ? null : $"{pack.Errors.Count} text/script entries were rejected. See the log for mor information.";
        foreach (string error in pack.Errors) Console.Error.WriteLine($"[translation] {error}.");
        if (memory != null)
        {
            foreach (string overlay in Dispatcher.ActiveNames.ToArray()) Apply(overlay, memory);
            if (IsCurrent("sel")) memory.ZeroRange(0x801BC398, 16 * 4);
        }
    }

    private static bool Matches(IMemory memory, uint address, byte[] bytes)
    {
        for (int i = 0; i < bytes.Length; i++) if (memory.ReadU8(address + (uint)i) != bytes[i]) return false;
        return true;
    }

    private static void Write(string overlay, IMemory memory, uint address, byte[] bytes)
    {
        if (!Changes.TryGetValue(overlay, out var changes)) Changes[overlay] = changes = [];
        byte[] before = new byte[bytes.Length];
        for (int i = 0; i < before.Length; i++) before[i] = memory.ReadU8(address + (uint)i);
        changes[address] = new(address, before, bytes);
        memory.LoadBytes(address, bytes);
    }

    internal static void OverlayLoaded(string overlay)
    {
        Changes.Remove(overlay);
        Pending.Add(overlay);
    }

    internal static void Update(IMemory memory)
    {
        if (Pending.Count == 0 || memory.ReadU32(0x80137F78) != 1) return;
        foreach (string overlay in Pending.ToArray())
        {
            Pending.Remove(overlay);
            if (IsCurrent(overlay)) Apply(overlay, memory);
        }
    }

    public static void PrepareMain(CpuContext c, IMemory m) => Apply("main", m);
    public static void PrepareDra(CpuContext c, IMemory m) => Apply("dra", m);

    public static void AfterStageCopy(CpuContext c, IMemory m)
    {
        if (m.ReadU32(0x80137F78) == 1 && m.ReadU32(0x80137F58) != 4) ApplyRegion(0x80180000, m);
    }

    public static void AfterRicCopy(CpuContext c, IMemory m)
    {
        if (m.ReadU32(0x80137F78) == 1) ApplyRegion(0x8013C000, m);
    }

    public static void AfterSupportCopy(CpuContext c, IMemory m)
    {
        if (m.ReadU32(0x80137F78) != 1) return;
        uint region = m.ReadU32(0x80137F58) == 15 ? 0x80170000u : m.ReadU8(0x80137F96) == 0 ? 0x8017A000u : 0x8017D000u;
        ApplyRegion(region, m);
    }

    public static void AfterSimLoad(CpuContext c, IMemory m)
    {
        foreach (string name in Dispatcher.ActiveNames) Apply(name, m);
    }

    private static void ApplyRegion(uint address, IMemory memory)
    {
        foreach (string name in Dispatcher.ActiveNames.Reverse())
            if (Dispatcher.Overlays.TryGetValue(name, out var overlay) && overlay.Base == address)
            {
                Apply(name, memory);
                return;
            }
    }

    internal static void Apply(string overlay, IMemory memory)
    {
        GameLanguage.EnsureLoaded();
        if (!IsCurrent(overlay)) return;
        ScriptMemory.Activate(overlay, memory);
        if (!_writes.TryGetValue(overlay, out var writes) || writes.Length == 0) return;
        var pending = new List<TextWrite>();
        foreach (var write in writes)
        {
            if (write.Relocated)
            {
                ApplyRelocated(overlay, write, memory);
                continue;
            }
            bool original = true, replacement = true;
            for (int i = 0; i < write.Original.Length; i++)
            {
                byte value = memory.ReadU8(write.Address + (uint)i);
                original &= value == write.Original[i];
                replacement &= value == write.Replacement[i];
            }
            if (replacement) continue;
            if (original) pending.Add(write);
            else if (Reported.Add(overlay + ":" + write.Key)) Console.Error.WriteLine($"[translation] {overlay}/{write.Key}: original bytes differ, replacement was skipped.");
        }
        foreach (var write in pending) Write(overlay, memory, write.Address, write.Replacement);
    }
    public static void MenuText(CpuContext context, IMemory memory)
    {
        if (!IsCurrent("sel") || !_menuTexts.TryGetValue(context.A0, out var write)) return;
        ApplyRelocated("sel", write, memory);
        string key = Convert.ToHexString(write.Replacement);
        if (Allocated.TryGetValue(key, out uint address) && Matches(memory, address, write.Replacement)) context.A0 = address;
    }

    public static void MessageBox(CpuContext context, IMemory memory)
    {
        if (memory.ReadU16(context.A0 + 0x2C) != 0 || !_messageTexts.TryGetValue(memory.ReadU32(context.A0 + 0x7C), out var writes)) return;
        foreach (var (overlay, write) in writes)
        {
            if (!IsCurrent(overlay)) continue;
            ApplyRelocated(overlay, write, memory);
            if (Allocated.TryGetValue(Convert.ToHexString(write.Replacement), out uint address) && Matches(memory, address, write.Replacement))
                memory.WriteU32(context.A0 + 0x7C, address);
            return;
        }
    }

    private static void ApplyRelocated(string overlay, TextWrite write, IMemory memory)
    {
        string key = Convert.ToHexString(write.Replacement);
        bool alreadyAllocated = Allocated.TryGetValue(key, out uint address) && Matches(memory, address, write.Replacement);
        foreach (uint pointer in write.Pointers)
        {
            uint value = memory.ReadU32(pointer);
            if (value != write.Address && (!alreadyAllocated || value != address))
            {
                if (Reported.Add(overlay + ":" + write.Key)) Console.Error.WriteLine($"[translation] {overlay}/{write.Key}: pointer differs, replacement was skipped.");
                return;
            }
        }
        if (!alreadyAllocated)
        {
            for (int i = 0; i < write.Original.Length; i++)
                if (memory.ReadU8(write.Address + (uint)i) != write.Original[i]) return;
            address = _textNext;
            if (address + (uint)write.Replacement.Length > TextEnd)
            {
                if (Reported.Add(overlay + ":" + write.Key)) Console.Error.WriteLine($"[translation] {overlay}/{write.Key}: no heap space, translation not aplied");
                return;
            }
            memory.LoadBytes(address, write.Replacement);
            Allocated[key] = address;
            _textNext = (address + (uint)write.Replacement.Length + 3) & ~3u;
        }
        foreach (uint pointer in write.Pointers)
            if (memory.ReadU32(pointer) != address) Write(overlay, memory, pointer, BitConverter.GetBytes(address));
    }

}
