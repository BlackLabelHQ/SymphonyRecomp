using ImGuiNET;
using RecompOne.Runtime.Assets.Textures;
using RecompOne.Runtime.Events;
using RecompOne.Runtime.Host.Window;
using RecompOne.Runtime.Memory;

namespace Recompiled;

/*
 * Quicksave/quickload of the whole emulated-machine state (RAM, VRAM, SPU
 * RAM, CPU GPRs + HI/LO) so you can iterate on the game/patches without
 * restarting the process, plus a periodic autosave slot so a crash doesn't
 * lose much progress. This is a raw snapshot of this build's own in-memory
 * state -- not a PS1 memory-card save -- so a .state file only makes sense
 * loaded back into the same (or a save-compatible) build it came from.
 *
 * Mutation only ever happens on VSync (see OnVSync), same pattern as
 * GameMenu's soft reset: the menu items/hotkeys just set a pending flag, and
 * the actual Poke/Array.Copy work happens once per frame at a safe boundary
 * instead of mid-tick from whatever thread the UI callback runs on.
 *
 * RAM is restored via PSMemory.Poke, which writes straight into the backing
 * array -- deliberately bypassing WriteU8/LoadBytes, which would otherwise
 * fire every memory-write listener in the codebase once per restored byte.
 */
public static class SaveState
{
    const string Magic = "SRSS";
    const int Version = 1;

    const string Dir = "savestates";
    static readonly string QuickPath = Path.Combine(Dir, "quick.state");
    static readonly string AutoSlotPath = Path.Combine(Dir, "autosave.state");

    const int AutosaveIntervalTicks = 900; // ~15s @ 60Hz

    static bool _savePending;
    static bool _loadPending;
    static bool _loadAutoPending;
    static int _autosaveTimer;

    public static void Register()
    {
        MenuRegistry.Menu("menu.misc")
            .Submenu("menu.misc.savestate")
                .Item("menu.savestate.save", RequestSave, "F5").Enabled(Cheats.InPlay)
                .Item("menu.savestate.load", RequestLoad, "F9").Enabled(Cheats.InPlay)
                .Item("menu.savestate.load_auto", RequestLoadAuto).Enabled(Cheats.InPlay)
                .End();

        Event.AddListener<VSyncEvent>(OnVSync);
    }

    static void RequestSave() => _savePending = true;
    static void RequestLoad() => _loadPending = true;
    static void RequestLoadAuto() => _loadAutoPending = true;

    static void OnVSync(VSyncEvent e)
    {
        bool inPlay = Cheats.InPlay();

        // Best-effort hotkey polling. Not proven to reliably observe key
        // state from inside a VSyncEvent handler specifically (untested in a
        // live session) -- harmless no-op if it never fires; the menu items
        // above are the reliable primary path regardless.
        if (inPlay)
        {
            if (ImGui.IsKeyPressed(ImGuiKey.F5)) _savePending = true;
            if (ImGui.IsKeyPressed(ImGuiKey.F9)) _loadPending = true;
        }

        if (_savePending)
        {
            _savePending = false;
            Save(QuickPath, silent: false);
        }

        if (_loadPending)
        {
            _loadPending = false;
            Load(QuickPath);
        }

        if (_loadAutoPending)
        {
            _loadAutoPending = false;
            Load(AutoSlotPath);
        }

        // Autosave counter only runs while actually in gameplay, so it never
        // fires while sitting on the title screen.
        if (inPlay)
        {
            if (++_autosaveTimer >= AutosaveIntervalTicks)
            {
                _autosaveTimer = 0;
                Save(AutoSlotPath, silent: true);
            }
        }
        else
        {
            _autosaveTimer = 0;
        }
    }

    static void Save(string path, bool silent)
    {
        var mem = RecompOne.Runtime.Runtime.Mem as PSMemory;
        var gpu = RecompOne.Runtime.Runtime.Gpu;
        var spu = RecompOne.Runtime.Runtime.Spu;
        var cpu = RecompOne.Runtime.Runtime.Cpu;

        if (mem == null || gpu == null || spu == null || cpu == null)
        {
            if (!silent)
                RecompOne.Runtime.Runtime.ShowNotice("Save state failed: engine not ready");
            return;
        }

        try
        {
            Directory.CreateDirectory(Dir);

            using var ms = new MemoryStream();
            using (var w = new BinaryWriter(ms))
            {
                w.Write(System.Text.Encoding.ASCII.GetBytes(Magic));
                w.Write(Version);

                var ram = mem.Ram;
                w.Write(ram.Length);
                w.Write(ram);

                var vram = gpu.Vram;
                w.Write(vram.Length);
                for (int i = 0; i < vram.Length; i++) w.Write(vram[i]);

                var spuRam = spu.Ram;
                w.Write(spuRam.Length);
                w.Write(spuRam);

                for (int i = 0; i < 32; i++) w.Write(cpu[i]);
                w.Write(cpu.HI);
                w.Write(cpu.LO);
            }

            File.WriteAllBytes(path, ms.ToArray());

            if (silent)
                Console.WriteLine($"[SaveState] autosaved to {path}");
            else
                RecompOne.Runtime.Runtime.ShowNotice(Localization.T("savestate.saved"));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SaveState] save to {path} failed: {ex.Message}");
            if (!silent)
                RecompOne.Runtime.Runtime.ShowNotice("Save state failed: " + ex.Message);
        }
    }

    static void Load(string path)
    {
        if (!File.Exists(path))
        {
            RecompOne.Runtime.Runtime.ShowNotice(Localization.T("savestate.not_found"));
            return;
        }

        var mem = RecompOne.Runtime.Runtime.Mem as PSMemory;
        var gpu = RecompOne.Runtime.Runtime.Gpu;
        var spu = RecompOne.Runtime.Runtime.Spu;
        var cpu = RecompOne.Runtime.Runtime.Cpu;

        if (mem == null || gpu == null || spu == null || cpu == null)
        {
            RecompOne.Runtime.Runtime.ShowNotice("Load state failed: engine not ready");
            return;
        }

        try
        {
            var bytes = File.ReadAllBytes(path);
            using var ms = new MemoryStream(bytes);
            using var r = new BinaryReader(ms);

            var magic = System.Text.Encoding.ASCII.GetString(r.ReadBytes(4));
            if (magic != Magic)
            {
                RecompOne.Runtime.Runtime.ShowNotice(Localization.T("savestate.not_found"));
                return;
            }

            int version = r.ReadInt32();
            if (version != Version)
            {
                RecompOne.Runtime.Runtime.ShowNotice(Localization.T("savestate.not_found"));
                return;
            }

            int ramLen = r.ReadInt32();
            var ramBytes = r.ReadBytes(ramLen);

            int vramCount = r.ReadInt32();
            var vramVals = new ushort[vramCount];
            for (int i = 0; i < vramCount; i++) vramVals[i] = r.ReadUInt16();

            int spuLen = r.ReadInt32();
            var spuBytes = r.ReadBytes(spuLen);

            var gprs = new uint[32];
            for (int i = 0; i < 32; i++) gprs[i] = r.ReadUInt32();
            uint hi = r.ReadUInt32();
            uint lo = r.ReadUInt32();

            // Validate against the current build's layout before mutating
            // anything -- a size mismatch means this state came from a
            // different/incompatible build and must not be applied partially.
            if (ramBytes.Length != (int)RecompOne.Runtime.Runtime.RamSize ||
                vramVals.Length != gpu.Vram.Length ||
                spuBytes.Length != spu.Ram.Length)
            {
                RecompOne.Runtime.Runtime.ShowNotice(Localization.T("savestate.not_found"));
                return;
            }

            for (uint i = 0; i < ramBytes.Length; i++)
                mem.Poke(i, ramBytes[i]);

            Array.Copy(vramVals, gpu.Vram, vramVals.Length);
            VramTracker.MarkCpuWrite(0, 0, RecompOne.Runtime.Gpu.VramWidth, RecompOne.Runtime.Gpu.VramHeight);

            Array.Copy(spuBytes, spu.Ram, spuBytes.Length);

            for (int i = 0; i < 32; i++) cpu[i] = gprs[i];
            cpu.HI = hi;
            cpu.LO = lo;

            RecompOne.Runtime.Runtime.ShowNotice(Localization.T("savestate.loaded"));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SaveState] load from {path} failed: {ex.Message}");
            RecompOne.Runtime.Runtime.ShowNotice("Load state failed: " + ex.Message);
        }
    }
}
