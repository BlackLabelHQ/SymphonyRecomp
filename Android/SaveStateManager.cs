using System;
using System.IO;
using System.Text;
using RecompOne.Runtime;
using RecompOne.Runtime.Context;
using RecompOne.Runtime.Dispatch;
using RecompOne.Runtime.Hle;
using RecompOne.Runtime.Memory;

namespace RecompOne.SoTN.Android
{
    public static class SaveStateManager
    {
        // Guards the pending-request fields below: they are written from the Android UI
        // thread and read from the emulation thread in ProcessPending().
        private static readonly object _pendingLock = new();
        private static int _pendingSaveSlot;
        private static int _pendingLoadSlot;
        private static string _baseFilesDir = "";
        private static Action<bool, string, int>? _onComplete;

        static SaveStateManager()
        {
            RecompOne.Runtime.Runtime.OnBeforePresentFrame += ProcessPending;
        }

        public static string GetSaveDir(string baseFilesDir)
        {
            string dir = Path.Combine(baseFilesDir, "savestates");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            return dir;
        }

        public static string GetSlotFilePath(string baseFilesDir, int slot)
        {
            return Path.Combine(GetSaveDir(baseFilesDir), $"slot_{slot}.sav");
        }

        public static string GetSlotInfo(string baseFilesDir, int slot)
        {
            string file = GetSlotFilePath(baseFilesDir, slot);
            if (!File.Exists(file)) return $"Slot {slot}: (Empty)";
            try
            {
                var fi = new FileInfo(file);
                return $"Slot {slot}: {fi.LastWriteTime:yyyy-MM-dd HH:mm:ss}";
            }
            catch
            {
                return $"Slot {slot}: (Saved)";
            }
        }

        public static void RequestSaveState(string baseFilesDir, int slot, Action<bool, string, int> onComplete)
        {
            lock (_pendingLock)
            {
                _baseFilesDir = baseFilesDir;
                _onComplete = onComplete;
                _pendingSaveSlot = slot;
            }
        }

        public static void RequestLoadState(string baseFilesDir, int slot, Action<bool, string, int> onComplete)
        {
            lock (_pendingLock)
            {
                _baseFilesDir = baseFilesDir;
                _onComplete = onComplete;
                _pendingLoadSlot = slot;
            }
        }

        public static void ProcessPending()
        {
            int saveSlot, loadSlot;
            string baseDir;
            Action<bool, string, int>? cb;

            lock (_pendingLock)
            {
                saveSlot = _pendingSaveSlot;
                loadSlot = _pendingLoadSlot;
                if (saveSlot <= 0 && loadSlot <= 0) return;

                if (saveSlot > 0) _pendingSaveSlot = 0;
                else _pendingLoadSlot = 0;

                baseDir = _baseFilesDir;
                cb = _onComplete;
                _onComplete = null;
            }

            bool success;
            string err;
            if (saveSlot > 0)
                success = DoSaveState(baseDir, saveSlot, out err);
            else
                success = DoLoadState(baseDir, loadSlot, out err);

            cb?.Invoke(success, err, saveSlot > 0 ? saveSlot : loadSlot);
        }

        private static bool DoSaveState(string baseFilesDir, int slot, out string error)
        {
            error = "";
            try
            {
                var mem = RecompOne.Runtime.Runtime.Mem as PSMemory;
                var cpu = RecompOne.Runtime.Runtime.Cpu;
                var gpu = RecompOne.Runtime.Runtime.Gpu;
                if (mem == null || cpu == null || gpu == null)
                {
                    error = "Game engine not initialized yet.";
                    return false;
                }

                string file = GetSlotFilePath(baseFilesDir, slot);
                using var ms = new MemoryStream();
                using var bw = new BinaryWriter(ms);

                // Header magic and timestamp
                bw.Write(Encoding.ASCII.GetBytes("SOTNSS03"));
                bw.Write(DateTime.UtcNow.ToBinary());

                // CPU Registers
                var snapshot = cpu.Snapshot();
                for (int i = 0; i < 32; i++) bw.Write(snapshot.gpr[i]);
                bw.Write(snapshot.hi);
                bw.Write(snapshot.lo);
                bw.Write(snapshot.sr);
                bw.Write(snapshot.cause);
                bw.Write(snapshot.epc);

                // Active Overlays
                var activeOverlays = Dispatcher.ActiveNames;
                bw.Write(activeOverlays.Length);
                foreach (var ov in activeOverlays) bw.Write(ov);

                // RAM Buffer (2MB / 8MB)
                var ram = mem.RamBuffer;
                bw.Write(ram.Length);
                bw.Write(ram);

                // Scratchpad Buffer (1KB)
                var scratch = mem.ScratchpadBuffer;
                bw.Write(scratch.Length);
                bw.Write(scratch);

                // Hardware Registers Buffer (8KB)
                var hwregs = mem.HwRegsBuffer;
                bw.Write(hwregs.Length);
                bw.Write(hwregs);

                // GPU State Snapshot
                var gs = gpu.SnapshotState();
                bw.Write(gs.DrawAreaLeft); bw.Write(gs.DrawAreaTop); bw.Write(gs.DrawAreaRight); bw.Write(gs.DrawAreaBottom);
                bw.Write(gs.DrawOffsetX); bw.Write(gs.DrawOffsetY);
                bw.Write(gs.TexPageX); bw.Write(gs.TexPageY); bw.Write(gs.TexDepth); bw.Write(gs.BlendMode);
                bw.Write(gs.Dither); bw.Write(gs.TexDisable);
                bw.Write(gs.TexWinMaskX); bw.Write(gs.TexWinMaskY); bw.Write(gs.TexWinOffX); bw.Write(gs.TexWinOffY);
                bw.Write(gs.SetMask); bw.Write(gs.CheckMask);
                bw.Write(gs.DispVramX); bw.Write(gs.DispVramY);
                bw.Write(gs.HRange1); bw.Write(gs.HRange2); bw.Write(gs.VRange1); bw.Write(gs.VRange2);
                bw.Write(gs.HRes);
                bw.Write(gs.HRes368); bw.Write(gs.VRes480); bw.Write(gs.Pal); bw.Write(gs.Disp24); bw.Write(gs.Interlace); bw.Write(gs.DisplayDisabled);
                bw.Write(gs.DmaDir);

                // VRAM Pixels (1024x512 ushorts = 1MB)
                //
                // Gpu.Vram is only the CPU-side shadow. With the GL backend every primitive is
                // rendered into GPU render targets and written back into the backend's own VRAM
                // texture - the shadow never receives any rendered output. Serializing it
                // directly captured an essentially blank framebuffer, which is why restoring a
                // state produced a black screen. ReadVram flushes the pipeline, writes back any
                // dirty render targets and pulls the real VRAM out of the GPU (which also
                // refreshes the shadow we are about to write).
                var vram = gpu.Vram;
                if (GpuHle.Backend is { Ready: true } backend)
                    backend.ReadVram(0, 0, 1024, 512, vram);

                bw.Write(vram.Length);
                for (int i = 0; i < vram.Length; i++)
                    bw.Write(vram[i]);

                File.WriteAllBytes(file, ms.ToArray());
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        private static bool DoLoadState(string baseFilesDir, int slot, out string error)
        {
            error = "";
            try
            {
                var mem = RecompOne.Runtime.Runtime.Mem as PSMemory;
                var cpu = RecompOne.Runtime.Runtime.Cpu;
                var gpu = RecompOne.Runtime.Runtime.Gpu;
                if (mem == null || cpu == null || gpu == null)
                {
                    error = "Game engine not initialized yet.";
                    return false;
                }

                string file = GetSlotFilePath(baseFilesDir, slot);
                if (!File.Exists(file))
                {
                    error = $"Slot {slot} is empty.";
                    return false;
                }

                byte[] data = File.ReadAllBytes(file);
                using var ms = new MemoryStream(data);
                using var br = new BinaryReader(ms);

                // Magic check
                byte[] magic = br.ReadBytes(8);
                string magicStr = Encoding.ASCII.GetString(magic);
                if (magicStr != "SOTNSS01" && magicStr != "SOTNSS02" && magicStr != "SOTNSS03")
                {
                    error = "Invalid savestate file format.";
                    return false;
                }
                long timeStamp = br.ReadInt64();

                // CPU Restore
                uint[] gpr = new uint[32];
                for (int i = 0; i < 32; i++) gpr[i] = br.ReadUInt32();
                uint hi = br.ReadUInt32();
                uint lo = br.ReadUInt32();
                uint sr = br.ReadUInt32();
                uint cause = br.ReadUInt32();
                uint epc = br.ReadUInt32();
                cpu.Restore((gpr, hi, lo, sr, cause, epc));

                if (magicStr == "SOTNSS03")
                {
                    int overlayCount = br.ReadInt32();
                    string[] overlays = new string[overlayCount];
                    for (int i = 0; i < overlayCount; i++)
                        overlays[i] = br.ReadString();
                    Dispatcher.SetActiveOverlays(overlays);
                }

                // RAM Restore
                int ramLen = br.ReadInt32();
                byte[] ramData = br.ReadBytes(ramLen);
                Array.Copy(ramData, mem.RamBuffer, Math.Min(ramLen, mem.RamBuffer.Length));

                if (magicStr == "SOTNSS02" || magicStr == "SOTNSS03")
                {
                    // Scratchpad Restore
                    int scratchLen = br.ReadInt32();
                    byte[] scratchData = br.ReadBytes(scratchLen);
                    Array.Copy(scratchData, mem.ScratchpadBuffer, Math.Min(scratchLen, mem.ScratchpadBuffer.Length));

                    // Hardware Registers Restore
                    int hwregsLen = br.ReadInt32();
                    byte[] hwregsData = br.ReadBytes(hwregsLen);
                    Array.Copy(hwregsData, mem.HwRegsBuffer, Math.Min(hwregsLen, mem.HwRegsBuffer.Length));

                    // GPU State Restore
                    var gs = new RecompOne.Runtime.Gpu.GpuStateSnapshot
                    {
                        DrawAreaLeft = br.ReadInt32(), DrawAreaTop = br.ReadInt32(), DrawAreaRight = br.ReadInt32(), DrawAreaBottom = br.ReadInt32(),
                        DrawOffsetX = br.ReadInt32(), DrawOffsetY = br.ReadInt32(),
                        TexPageX = br.ReadInt32(), TexPageY = br.ReadInt32(), TexDepth = br.ReadInt32(), BlendMode = br.ReadInt32(),
                        Dither = br.ReadBoolean(), TexDisable = br.ReadBoolean(),
                        TexWinMaskX = br.ReadInt32(), TexWinMaskY = br.ReadInt32(), TexWinOffX = br.ReadInt32(), TexWinOffY = br.ReadInt32(),
                        SetMask = br.ReadBoolean(), CheckMask = br.ReadBoolean(),
                        DispVramX = br.ReadInt32(), DispVramY = br.ReadInt32(),
                        HRange1 = br.ReadInt32(), HRange2 = br.ReadInt32(), VRange1 = br.ReadInt32(), VRange2 = br.ReadInt32(),
                        HRes = br.ReadInt32(),
                        HRes368 = br.ReadBoolean(), VRes480 = br.ReadBoolean(), Pal = br.ReadBoolean(), Disp24 = br.ReadBoolean(), Interlace = br.ReadBoolean(), DisplayDisabled = br.ReadBoolean(),
                        DmaDir = br.ReadInt32()
                    };
                    gpu.RestoreState(gs);
                }

                // VRAM Restore
                int vramLen = br.ReadInt32();
                var vram = gpu.Vram;
                for (int i = 0; i < Math.Min(vramLen, vram.Length); i++)
                    vram[i] = br.ReadUInt16();

                // Reset the GPU pipeline, then push the restored VRAM straight back into the
                // backend. These must stay adjacent: Reset() destroys the render targets that
                // hold the visible frame, so the re-upload must not be skippable by anything
                // that throws in between (the audio reset below used to sit here).
                GpuHle.Backend?.Reset();
                GpuHle.NotifyDisplay(gpu.DisplayX, gpu.DisplayY, gpu.DisplayWidth, gpu.DisplayHeight);
                GpuHle.Backend?.WriteVram(0, 0, 1024, 512, gpu.Vram);

                // Reset Audio & XA playback
                RecompOne.Runtime.Runtime.Spu?.Reset();
                XaAudio.Reset();

                // NOTE: do NOT unwind the C# callstack here.
                //
                // Recompiled MIPS functions are plain C# methods: `jal` is a C# call and
                // `jr $ra` is a C# `return`, so the *real* program counter and return chain
                // live in the C# callstack, not in c.EPC. Nothing in the generated code ever
                // writes c.EPC (the recompiler only emits it for `mtc0 $14`, which SoTN never
                // executes), so c.EPC is permanently the boot entry 0x80010DFC. Throwing to
                // unwind and re-dispatching on c.EPC therefore did not resume at the save
                // point - it re-entered the boot entry, which zeroes .bss over the RAM we just
                // restored and reboots the game on top of a half-restored state, recursing
                // until the thread stack guard page was hit (SIGSEGV).
                //
                // Instead we swap the emulated state in place. Both save and load run from
                // OnBeforePresentFrame inside LibEtc.VSync, so the callstack shape here already
                // matches the one captured at save time; returning from VSync normally lets the
                // game continue with the restored state.
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
