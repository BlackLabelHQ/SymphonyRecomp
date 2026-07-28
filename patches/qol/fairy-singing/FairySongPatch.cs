using RecompOne.Runtime.Context;
using RecompOne.Runtime.Memory;
using Sotn;

namespace Recompiled;

public static class FairySongPatch
{
    const uint FaerieActive = 0x8009797B;
    const uint PlayerStepS = 0x80073406;
    const uint SongState = 0x801375C8;
    const uint SndBusyXa = 0x8013901C;
    const uint SndBusySeq = 0x8013B61C;
    const uint CurrentMusicId = 0x80097910;

    const uint Fairy = 0x800736C8;
    const uint FairyMode = Fairy + 0x26;
    const uint FairyStep = Fairy + 0x2C;
    const uint FairyFrameCounter = Fairy + 0x8C;
    const uint FairyUnkB4 = Fairy + 0xB4;

    const ushort ModeSitOnShoulder = 0xDA;

    static bool _sung;
    static int _abort;

    public static void RestoreNocturne(CpuContext c, IMemory m)
    {
        if (QualityOfLife.RestoreFairySong == false) return;
         
        if (m.ReadU16(FairyMode) != ModeSitOnShoulder)
        {
            if (_sung)
            {
                _sung = false;
                uint st = m.ReadU32(SongState);
                if (st >= 1 && st <= 5)
                {
                    m.WriteU32(SongState, 0);
                    _abort = 20;
                }
            }

            if (_abort > 0)
            {
                _abort--;
                if (_abort > 0)
                {
                    m.WriteU16(SndBusyXa, 0);
                    m.WriteU32(SndBusySeq, 0);
                }
                else
                {
                    int mid = m.ReadU16(CurrentMusicId);
                    if (mid != 0)
                        GameApi.PlaySfx(mid);
                }
            }
            return;
        }

        _abort = 0;
        if (m.ReadU16(PlayerStepS) != 4 || m.ReadU8(FaerieActive) != 3)
            return;

        ushort step = m.ReadU16(FairyStep);
        if (!_sung && step >= 5 && step <= 8 && m.ReadU32(SongState) == 0)
        {
            m.WriteU16(FairyStep, 2);
            m.WriteU16(FairyFrameCounter, 0x60);
            m.WriteU16(FairyUnkB4, 0);
            _sung = true;
        }
    }
}