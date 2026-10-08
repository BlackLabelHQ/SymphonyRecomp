using RecompOne.Runtime;
using RecompOne.Runtime.Assets.Textures;
using RecompOne.Runtime.Events;
using Sotn;

namespace Recompiled;

/*
 * One-time diagnostic: enables the engine's own built-in texture dumper
 * (writes every observed VRAM texture tile to dump/<game>/textures/*.png,
 * named by content hash) and, once in-game, self-drives a brief "hold Right"
 * autopilot by writing directly to the pad-state memory address -- so sprite
 * texture tiles actually get drawn/dumped without needing a human to walk
 * the character around. This is how the real sprite-replacement pack gets
 * built: inspect the dumped PNGs to find Richter's sprite tiles, then supply
 * replacement PNGs with matching hashes in packs/<name>/textures/.
 * Safe: TextureDumper only reads VRAM/writes files, no game-state mutation.
 * The autopilot write is time-boxed and reverts to passing through real
 * input afterward. Remove once a real texture pack exists.
 */
public static class XTextureDiscovery
{
    const int MoveFrames = 240; // ~4 seconds at 60fps
    const int StartTapPeriod = 40;
    const int StartTapHold = 4;
    const int MaxTitleFrames = 1800; // ~30s ceiling before giving up on title nav

    static int _titleFrames;
    static int _moveFrames;
    static bool _done;

    public static void Register()
    {
        TextureDumper.SetEnabled(true);
        Event.AddListener<VSyncEvent>(OnVSync);
    }

    static void OnVSync(VSyncEvent e)
    {
        if (_done) return;
        if (!Game.Available) return;

        if (!Game.InGame || Game.IsLoading)
        {
            // Not in gameplay yet: periodically tap Start to try to push
            // through title/menu/file-select screens on its own.
            _titleFrames++;
            if (_titleFrames % 60 == 0) Heartbeat($"waiting, titleFrames={_titleFrames} inGame={Game.InGame} loading={Game.IsLoading}");
            if (_titleFrames > MaxTitleFrames)
            {
                _done = true;
                Heartbeat("gave up trying to reach gameplay automatically -- play manually to trigger dumps instead");
                return;
            }
            int phase = _titleFrames % StartTapPeriod;
            Runtime.Mem!.WriteU16(Game.PadsAddr, phase < StartTapHold ? (ushort)Button.Start : (ushort)0);
            return;
        }

        // In gameplay: hold Right for a few seconds to trigger sprite draws.
        Runtime.Mem!.WriteU16(Game.PadsAddr, (ushort)Button.Right);
        _moveFrames++;

        if (_moveFrames == 1)
            Heartbeat("reached gameplay -- autopilot forcing Right to trigger sprite draws for dumping");

        if (_moveFrames >= MoveFrames)
        {
            _done = true;
            Heartbeat($"autopilot done -- dumped {TextureDumper.Written} unique tiles to {TextureDumper.Root}");
        }
    }

    static void Heartbeat(string msg)
    {
        System.Console.WriteLine($"[XTextureDiscovery] {msg}");
        System.Console.Out.Flush();
    }
}
