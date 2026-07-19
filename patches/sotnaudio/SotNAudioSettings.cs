using ImGuiNET;
using RecompOne.Runtime.Hle;
using RecompOne.Runtime.Host.Window;
using RecompOne.Runtime.Context;
using RecompOne.Runtime.Memory;
using System.Runtime.InteropServices.Marshalling;

namespace Recompiled
{
    internal class SotNAudioSettings
    {
        public static UInt32 MusicVolumeAddr = 0x8013B668;
        public static int MusicVolumeLevelInt = RecompOne.Runtime.Runtime.View.GetInt("MusicVolume", 0x20);

        public static void Register()
        {
            SettingsRegistry.Extend("audio", DrawControls);
        }

        public static void DrawControls()
        {
            ImGui.Spacing();
            ImGui.TextUnformatted("Music (XA) Volume");
            ImGui.SliderInt("MusicVolume", ref MusicVolumeLevelInt, 0, 32);
            RecompOne.Runtime.Runtime.SaveView();
        }

        public static void ApplyAudio(CpuContext c, IMemory m)
        {
            UInt16 MusicVolumeLevelShort = (UInt16)MusicVolumeLevelInt;
            m.WriteU16(MusicVolumeAddr, MusicVolumeLevelShort);
        }
    }
}
