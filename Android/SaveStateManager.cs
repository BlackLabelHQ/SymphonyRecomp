using System;
using System.IO;
using System.Text;
using RecompOne.Runtime.Context;
using RecompOne.Runtime.Hle;
using RecompOne.Runtime.Memory;

namespace RecompOne.SoTN.Android
{
    public static class SaveStateManager
    {
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
            _baseFilesDir = baseFilesDir;
            _onComplete = onComplete;
            _pendingSaveSlot = slot;
        }

        public static void RequestLoadState(string baseFilesDir, int slot, Action<bool, string, int> onComplete)
        {
            _baseFilesDir = baseFilesDir;
            _onComplete = onComplete;
            _pendingLoadSlot = slot;
        }

        public static void ProcessPending()
        {
            if (_pendingSaveSlot > 0)
            {
                int slot = _pendingSaveSlot;
                _pendingSaveSlot = 0;
                bool success = DoSaveState(_baseFilesDir, slot, out string err);
                var cb = _onComplete;
                _onComplete = null;
                cb?.Invoke(success, err, slot);
            }
            else if (_pendingLoadSlot > 0)
            {
                int slot = _pendingLoadSlot;
                _pendingLoadSlot = 0;
                bool success = DoLoadState(_baseFilesDir, slot, out string err);
                var cb = _onComplete;
                _onComplete = null;
                cb?.Invoke(success, err, slot);
            }
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
                bw.Write(Encoding.ASCII.GetBytes("SOTNSS01"));
                bw.Write(DateTime.UtcNow.ToBinary());

                // CPU Registers
                var snapshot = cpu.Snapshot();
                for (int i = 0; i < 32; i++) bw.Write(snapshot.gpr[i]);
                bw.Write(snapshot.hi);
                bw.Write(snapshot.lo);
                bw.Write(snapshot.sr);
                bw.Write(snapshot.cause);
                bw.Write(snapshot.epc);

                // RAM Buffer (2MB / 8MB)
                var ram = mem.RamBuffer;
                bw.Write(ram.Length);
                bw.Write(ram);

                // VRAM Pixels (1024x512 ushorts = 1MB)
                var vram = gpu.Vram;
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
                if (Encoding.ASCII.GetString(magic) != "SOTNSS01")
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

                // RAM Restore
                int ramLen = br.ReadInt32();
                byte[] ramData = br.ReadBytes(ramLen);
                Array.Copy(ramData, mem.RamBuffer, Math.Min(ramLen, mem.RamBuffer.Length));

                // VRAM Restore
                int vramLen = br.ReadInt32();
                var vram = gpu.Vram;
                for (int i = 0; i < Math.Min(vramLen, vram.Length); i++)
                    vram[i] = br.ReadUInt16();

                // Upload restored VRAM buffer to GPU backend immediately
                GpuHle.Backend?.WriteVram(0, 0, 1024, 512, gpu.Vram);

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
