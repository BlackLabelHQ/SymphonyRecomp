using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using RecompOne.Runtime;
using Sotn;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Quantization;

namespace Recompiled;

/*
 * Real, engine-level X sprite replacement: no overlay, no RenderPrimEvent
 * skipping. Richter's current-frame bitmap is streamed into a fixed VRAM
 * slot every tick (confirmed live via probing: dst~(513,257), 4bpp, varies
 * ~6x45 halfwords/rows depending on pose). We intercept that exact byte
 * stream via Gpu.SpriteOverrideProvider (wired in GpuCommands.cs) and
 * substitute X's current pose, re-encoded to the PS1's native 4bpp indexed
 * format at whatever exact size the native code allocated for this frame --
 * so position/scale/clipping are entirely the game engine's own, correct by
 * construction, not something we guess at in screen space.
 *
 * Colors come along for the ride: the quantized 16-color palette for
 * whichever X frame is showing is pushed through Entity.WritePalette (a
 * proven-safe existing accessor) so the real X colors display, not
 * Richter's.
 *
 * Pose selection is driven by XControl's own movement state (dash/airborne/
 * moving) via Tick(), same reasoning as before: X has moves (dash,
 * wall-cling) with no Richter-pose equivalent to map from. The frame
 * choices themselves live in xsprite_config.json, and the frame_NNN.png
 * source images are both hot-reloaded (checked every ~30 ticks) so they
 * can be tuned/redrawn without a rebuild.
 *
 * Source-asset quirks handled here (confirmed by inspecting the actual
 * frame_NNN.png pixels, not guessed): each frame is a 96x96 cell but X's
 * drawn pixels only occupy a small, inconsistently-offset region inside it
 * (e.g. frame_000's content is a 35x45 box starting at (30,26)). Cropping to
 * that per-frame content bounding box (not just trimming the bottom margin)
 * before stretching is what makes X fill the native sprite box at the same
 * size Richter did -- otherwise most of the box is still empty canvas
 * margin and he renders tiny. Separately, the catalog mixes facing
 * directions: idle/dash/walk frames are drawn facing right, but the
 * jump/air-shot frames (around index 32) are drawn facing left, breaking
 * the "one canonical right-facing source, engine mirrors for left" model
 * and showing up as X facing backwards specifically while airborne.
 * flipFrames in the config lists indices that need a horizontal flip to
 * normalize them to the same right-facing canonical as everything else.
 */
public static class XSpriteRenderer
{
    const string FrameDir = "../asset_catalog/x4/X_sprites";
    const string ConfigPath = "patches/xcontrol/xsprite_config.json";
    const int FrameCount = 237;

    // Observed live via XVramProbe: Richter's current-frame bitmap always
    // loads near here. Tolerant bounds so other poses (not yet observed --
    // only idle/walk were captured) still match if their exact w/h differ.
    const int RegionX = 513, RegionY = 257, RegionTolX = 24, RegionTolY = 24;
    const int RegionMaxW = 16, RegionMaxH = 80;

    sealed class Config
    {
        public int idleFrame { get; set; } = 0;
        public int dashFrame { get; set; } = 9;
        public int jumpFrame { get; set; } = 32;
        public int[] walkFrames { get; set; } = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
        public int walkTicksPerAdvance { get; set; } = 6;
        public int maxColors { get; set; } = 15;
        public int[] flipFrames { get; set; } = [32];
    }

    static Config _cfg = new();
    static DateTime _cfgMtime;
    static int _cfgCheckTimer;

    static int _walkTickCounter;
    static int _walkFrameOffset;
    static int _currentFrame;

    static readonly Dictionary<int, (Image<Rgba32> Image, Rectangle ContentBounds)?> _sourceCache = new();
    static readonly Dictionary<int, DateTime> _sourceMtime = new();
    static readonly Dictionary<(int frame, int w, int h), (ushort[] pixels, ushort[] palette)?> _encodeCache = new();
    static int _lastPalette = -1;

    public static void Register()
    {
        ReloadConfigIfChanged(force: true);
        Gpu.SpriteOverrideProvider = ProvideFrame;
    }

    // Called once per tick from XControl.OnVSync with the current movement
    // state; decides which X frame should show on the *next* native sprite
    // upload (a 1-frame/16ms lag versus the real animation, imperceptible).
    // facingLeft is accepted but unused -- the native engine already mirrors
    // the sprite itself based on Entity.FacingLeft, so we only ever supply
    // one canonical (unflipped) orientation. See Encode()'s header comment.
    public static void Tick(bool dashing, bool airborne, bool moving, bool facingLeft)
    {
        if (++_cfgCheckTimer >= 30)
        {
            _cfgCheckTimer = 0;
            ReloadConfigIfChanged(force: false);
            CheckSourceReload();
        }

        if (dashing) { _currentFrame = _cfg.dashFrame; return; }
        if (airborne) { _currentFrame = _cfg.jumpFrame; return; }
        if (moving)
        {
            var frames = _cfg.walkFrames;
            if (frames.Length == 0) { _currentFrame = _cfg.idleFrame; return; }
            if (++_walkTickCounter >= Math.Max(1, _cfg.walkTicksPerAdvance))
            {
                _walkTickCounter = 0;
                _walkFrameOffset = (_walkFrameOffset + 1) % frames.Length;
            }
            _currentFrame = frames[_walkFrameOffset];
            return;
        }
        _walkTickCounter = 0;
        _walkFrameOffset = 0;
        _currentFrame = _cfg.idleFrame;
    }

    static void ReloadConfigIfChanged(bool force)
    {
        try
        {
            if (!File.Exists(ConfigPath)) return;
            var mtime = File.GetLastWriteTimeUtc(ConfigPath);
            if (!force && mtime == _cfgMtime) return;
            _cfgMtime = mtime;
            var json = File.ReadAllText(ConfigPath);
            var cfg = JsonSerializer.Deserialize<Config>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (cfg != null)
            {
                _cfg = cfg;
                _encodeCache.Clear();
                _sourceCache.Clear(); // flipFrames may have changed, which bakes into the cached source
                Console.WriteLine("[XSpriteRenderer] config (re)loaded");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[XSpriteRenderer] config load failed: {ex.Message}");
        }
    }

    static bool IsPlayerRegion(int x, int y, int w, int h) =>
        Math.Abs(x - RegionX) <= RegionTolX && Math.Abs(y - RegionY) <= RegionTolY &&
        w > 0 && w <= RegionMaxW && h > 0 && h <= RegionMaxH;

    static ushort[]? ProvideFrame(int x, int y, int w, int h)
    {
        if (!Game.Available || !Game.InGame || Game.IsLoading) return null;
        if (!IsPlayerRegion(x, y, w, h)) return null;

        int frame = _currentFrame;
        var key = (frame, w, h);
        if (!_encodeCache.TryGetValue(key, out var enc))
        {
            enc = Encode(frame, w * 4, h, _cfg.maxColors);
            _encodeCache[key] = enc;
        }
        if (enc == null) return null;

        if (_lastPalette != frame)
        {
            _lastPalette = frame;
            Player.Entity.WritePalette(enc.Value.palette);
        }
        return enc.Value.pixels;
    }

    static (Image<Rgba32> Image, Rectangle ContentBounds)? LoadSource(int frame)
    {
        if (_sourceCache.TryGetValue(frame, out var cached)) return cached;
        (Image<Rgba32> Image, Rectangle ContentBounds)? result = null;
        try
        {
            if (frame >= 0 && frame < FrameCount)
            {
                string path = Path.Combine(FrameDir, $"frame_{frame:000}.png");
                _sourceMtime[frame] = File.Exists(path) ? File.GetLastWriteTimeUtc(path) : DateTime.MinValue;
                if (File.Exists(path))
                {
                    var img = Image.Load<Rgba32>(path);
                    // Normalize to the same canonical right-facing orientation as
                    // every other frame (see header comment) before measuring
                    // content bounds, so the crop below lines up post-flip too.
                    if (Array.IndexOf(_cfg.flipFrames, frame) >= 0)
                        img.Mutate(ctx => ctx.Flip(FlipMode.Horizontal));
                    result = (img, FindContentBounds(img));
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[XSpriteRenderer] failed to load frame {frame}: {ex.Message}");
        }
        _sourceCache[frame] = result;
        return result;
    }

    // Mirrors ReloadConfigIfChanged's approach but for the PNG sources
    // themselves: editing a frame_NNN.png on disk should show up in-game
    // without a full process restart. Only frames that have actually been
    // loaded once (i.e. are present in _sourceMtime) are worth re-stating
    // here; anything never touched will simply be read fresh on first use.
    static void CheckSourceReload()
    {
        int changed = 0;
        foreach (var frame in new List<int>(_sourceMtime.Keys))
        {
            string path = Path.Combine(FrameDir, $"frame_{frame:000}.png");
            var mtime = File.Exists(path) ? File.GetLastWriteTimeUtc(path) : DateTime.MinValue;
            if (mtime == _sourceMtime[frame]) continue;

            _sourceCache.Remove(frame);
            _sourceMtime.Remove(frame);
            changed++;
        }

        if (changed > 0)
        {
            _encodeCache.Clear();
            _lastPalette = -1;
            Console.WriteLine($"[XSpriteRenderer] reloaded {changed} sprite source(s)");
        }
    }

    // Source frames carry a large, inconsistently-positioned transparent
    // margin around X on all sides (the sprite-sheet cell is a fixed 96x96,
    // but the figure is a much smaller, off-center region inside it).
    // Stretch-resizing the whole cell onto the native sprite box squeezes
    // the actual character down to a fraction of the box (confirmed live:
    // the "too small" bug) and, since the margin isn't symmetric, also
    // shifts/floats him within it. Cropping to exactly the opaque bounding
    // box first means the stretch below fills the native box with X himself,
    // not his padding.
    static Rectangle FindContentBounds(Image<Rgba32> img)
    {
        int minX = img.Width, minY = img.Height, maxX = -1, maxY = -1;
        img.ProcessPixelRows(accessor =>
        {
            for (int yy = 0; yy < accessor.Height; yy++)
            {
                var row = accessor.GetRowSpan(yy);
                for (int xx = 0; xx < row.Length; xx++)
                {
                    if (row[xx].A < 32) continue;
                    if (xx < minX) minX = xx;
                    if (xx > maxX) maxX = xx;
                    if (yy < minY) minY = yy;
                    if (yy > maxY) maxY = yy;
                }
            }
        });
        if (maxX < 0) return new Rectangle(0, 0, img.Width, img.Height); // fully transparent frame: don't crop to nothing
        return new Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1);
    }

    static (ushort[] pixels, ushort[] palette)? Encode(int frame, int pixelW, int pixelH, int maxColors)
    {
        var src = LoadSource(frame);
        if (src == null || pixelW <= 0 || pixelH <= 0) return null;

        using var work = src.Value.Image.Clone(ctx =>
        {
            ctx.Crop(src.Value.ContentBounds);

            // Stretch to exactly fill the native canvas (same footprint
            // Richter's own sprite occupied) rather than letterboxing with
            // transparent padding, which made X look shrunken inside his
            // own silhouette space (confirmed live: the "too small" bug).
            // NearestNeighbor keeps pixel-art edges crisp -- the default
            // bicubic sampler blurred this heavy a downscale badly enough
            // that, combined with 15-color quantization, X read as a muddy
            // blob (confirmed live: the "not clear" bug).
            ctx.Resize(new ResizeOptions
            {
                Size = new Size(pixelW, pixelH),
                Mode = ResizeMode.Stretch,
                Sampler = KnownResamplers.NearestNeighbor,
            });
            ctx.Quantize(new WuQuantizer(new QuantizerOptions { MaxColors = Math.Clamp(maxColors, 1, 15) }));
        });

        var palette = new ushort[16];
        var colorToIndex = new Dictionary<Rgba32, byte>();
        byte nextIndex = 1; // index 0 reserved transparent
        var indices = new byte[pixelW * pixelH];

        work.ProcessPixelRows(accessor =>
        {
            for (int yy = 0; yy < accessor.Height; yy++)
            {
                var row = accessor.GetRowSpan(yy);
                for (int xx = 0; xx < row.Length; xx++)
                {
                    var px = row[xx];
                    byte idx;
                    if (px.A < 32)
                    {
                        idx = 0;
                    }
                    else if (!colorToIndex.TryGetValue(px, out idx))
                    {
                        idx = nextIndex < 16 ? nextIndex++ : (byte)15;
                        colorToIndex[px] = idx;
                        if (idx < 16)
                            palette[idx] = (ushort)(((px.R >> 3) & 0x1F) | (((px.G >> 3) & 0x1F) << 5) | (((px.B >> 3) & 0x1F) << 10));
                    }
                    indices[yy * pixelW + xx] = idx;
                }
            }
        });

        int words = pixelW / 4;
        var packed = new ushort[words * pixelH];
        for (int yy = 0; yy < pixelH; yy++)
        {
            for (int wx = 0; wx < words; wx++)
            {
                int baseI = yy * pixelW + wx * 4;
                ushort word = 0;
                for (int n = 0; n < 4; n++)
                    word |= (ushort)((indices[baseI + n] & 0xF) << (n * 4));
                packed[yy * words + wx] = word;
            }
        }

        return (packed, palette);
    }
}
