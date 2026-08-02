using RecompOne.Runtime.Context;
using RecompOne.Runtime.Events;
using RecompOne.Runtime.Memory;
using RecompOne.Runtime.Hle;

namespace Recompiled;

public static partial class WidescreenPatch
{
    public static bool PillarBoxing = true;
    public static bool Unstretch = true;
    public static bool OriginalAspect = true;
    public const int StageWidth = 256;
    public const int StageHeight = 240;

    public static float SourceAspect => Unstretch ? (float)StageWidth / StageHeight : 4f / 3f;
    public static short StageViewTop => PillarBoxing ? (short)0x14 : (short)0;
    public static short StageViewHeight => PillarBoxing ? (short)0xCF : (short)240;
    public static float StageAspect = 16f / 9f;

    const uint GpuBuffers = 0x8003CB08;
    const uint GpuBufferStride = 0x177F4;
    const uint CurrentBufferPtr = 0x8006C37C;
    const uint BackbufferX = 0x8006C39C;
    const uint BackbufferY = 0x8006C3A0;
    const uint TilemapAddr = 0x80073084;
    const uint BgLayersAddr = 0x800730D8;
    const uint BgLayerStride = 0x30;
    const uint GpuUsageAddr = 0x8009792C;
    const uint ClutIdsAddr = 0x8003C104;

    const uint FadeAddr = 0x8013799C;
    const uint PrimBufAddr = 0x80086FEC;
    const uint PrimStride = 0x34;

    const uint DrawOfs = 0x04;
    const uint DispOfs = 0x60;
    const uint OtOfs = 0x474;
    const uint DrawModesOfs = 0xC74;
    const uint Sprite16Ofs = 0x117F4;
    const int OtSize = 0x200;
    const int MaxDrawModes = 0x400;
    const int MaxSprt16 = 0x280;
    const int HorizTiles = 17;
    const int VertiTiles = 16;
    const int MaxBgLayers = 16;

    const uint LayerShow = 0x1;
    const uint LayerTpageId = 0x60;
    const uint LayerSemiTrans = 0x80;
    const uint LayerTpageAlt = 0x100;
    const uint LayerClutAlt = 0x200;
    const uint LayerWrapBg = 0x1000;

    static uint Buf(int i) => GpuBuffers + (uint)i * GpuBufferStride;

    static bool _initialized;

    static void EnsureInitialized()
    {
        if (_initialized) return;
        _initialized = true;
        float aspect = RecompOne.Runtime.Runtime.View.GetFloat("WidescreenAspect", 16f / 9f);
        Display.TargetAspect = aspect;
        StageAspect = aspect;
        PillarBoxing = RecompOne.Runtime.Runtime.View.GetBool("WidescreenPillarBoxing", true);
        Unstretch = RecompOne.Runtime.Runtime.View.GetBool("WidescreenUnstretch", true);
        OriginalAspect = RecompOne.Runtime.Runtime.View.GetBool("WidescreenOriginalAspect", true);
    }

    public static void Register() => Event.AddListener<DisplayModeEvent>(OnDisplayMode);

    static void OnDisplayMode(DisplayModeEvent e) => ApplyDisplay(e.Mode);

    public static void Refresh() => ApplyDisplay(DisplayModeHooks.Current);

    static void ApplyDisplay(DisplayMode mode)
    {
        EnsureInitialized();
        StageAspect = Display.TargetAspect;
        bool stage = mode == DisplayMode.Stage;
        Display.SourceAspect = stage ? SourceAspect : 4f / 3f;
        Display.OutputAspect = 4f / 3f;
        Display.WideAspect = stage && !OriginalAspect ? StageAspect : 0f;
    }

    public static void ConfigStageClip(CpuContext c, IMemory m)
    {
        EnsureInitialized();

        ushort top = (ushort)StageViewTop;
        ushort h = (ushort)StageViewHeight;
        ushort buf1Top = (ushort)((c.A0 != 0 ? 0x100 : 0) + StageViewTop);

        for (int i = 0; i < 2; i++)
        {
            uint draw = Buf(i) + DrawOfs;
            m.WriteU16(draw + 0x02, i == 0 ? top : buf1Top);
            m.WriteU16(draw + 0x06, h);
            m.WriteU8(draw + 0x16, 0);
            m.WriteU8(draw + 0x18, 1);
            m.WriteU8(draw + 0x19, 0);
            m.WriteU8(draw + 0x1A, 0);
            m.WriteU8(draw + 0x1B, 0);
            m.WriteU8(Buf(i) + DispOfs + 0x11, 0);
        }
    }

    public static void SetTitleDisplayBuffer(CpuContext c, IMemory m) => SetupTitleBuffers(m, 512);

    public static void SetTitleDisplayBuffer256(CpuContext c, IMemory m) => SetupTitleBuffers(m, 384);

    static void SetupTitleBuffers(IMemory m, short width)
    {
        SetDefDrawEnv(m, Buf(0) + DrawOfs, 0, 0, width, 240);
        SetDefDrawEnv(m, Buf(1) + DrawOfs, 0, 256, width, 240);
        SetDefDispEnv(m, Buf(0) + DispOfs, 0, 256, width, 240);
        SetDefDispEnv(m, Buf(1) + DispOfs, 0, 0, width, 240);

        for (int i = 0; i < 2; i++)
        {
            uint draw = Buf(i) + DrawOfs;
            m.WriteU16(draw + 0x02, (ushort)(i == 0 ? 0 : 256));
            m.WriteU16(draw + 0x06, 240);
            m.WriteU8(draw + 0x18, 1);
            m.WriteU8(draw + 0x19, 0);
            m.WriteU8(draw + 0x1A, 0);
            m.WriteU8(draw + 0x1B, 0);
            m.WriteU8(Buf(i) + DispOfs + 0x11, 0);
        }
    }

    static void SetDefDrawEnv(IMemory m, uint env, short x, short y, short w, short h)
    {
        m.WriteU16(env + 0x00, (ushort)x);
        m.WriteU16(env + 0x02, (ushort)y);
        m.WriteU16(env + 0x04, (ushort)w);
        m.WriteU16(env + 0x06, (ushort)h);
        m.WriteU16(env + 0x08, (ushort)x);
        m.WriteU16(env + 0x0A, (ushort)y);
        m.WriteU16(env + 0x0C, 0);
        m.WriteU16(env + 0x0E, 0);
        m.WriteU16(env + 0x10, 0);
        m.WriteU16(env + 0x12, 0);
        m.WriteU16(env + 0x14, 0x0A);
        m.WriteU8(env + 0x16, 1);
        m.WriteU8(env + 0x17, (byte)(h <= 256 ? 1 : 0));
        m.WriteU8(env + 0x18, 0);
        m.WriteU8(env + 0x19, 0);
        m.WriteU8(env + 0x1A, 0);
        m.WriteU8(env + 0x1B, 0);
    }

    static void SetDefDispEnv(IMemory m, uint env, short x, short y, short w, short h)
    {
        m.WriteU16(env + 0x00, (ushort)x);
        m.WriteU16(env + 0x02, (ushort)y);
        m.WriteU16(env + 0x04, (ushort)w);
        m.WriteU16(env + 0x06, (ushort)h);
        m.WriteU16(env + 0x08, 0);
        m.WriteU16(env + 0x0A, 0);
        m.WriteU16(env + 0x0C, 0);
        m.WriteU16(env + 0x0E, 0);
        m.WriteU8(env + 0x10, 0);
        m.WriteU8(env + 0x11, 0);
        m.WriteU8(env + 0x12, 0);
        m.WriteU8(env + 0x13, 0);
    }

    static int StageMargin() => Display.WideMargin(256);

    static void LayoutFadePrims(IMemory m, uint fadePrim, int width)
    {
        int margin = width == 256 ? StageMargin() : 0;
        int half = width / 2;
        uint prim = PrimBufAddr + fadePrim * PrimStride;
        for (int i = 0; prim != 0; i++)
        {
            bool right = (i & 1) != 0;
            m.WriteU16(prim + 0x08, (ushort)(short)(right ? half : -margin));
            m.WriteU16(prim + 0x0A, 0);
            m.WriteU8(prim + 0x0C, (byte)(half + margin));
            m.WriteU8(prim + 0x0D, 0xF0);
            m.WriteU8(prim + 0x04, 0);
            m.WriteU8(prim + 0x05, 0);
            m.WriteU8(prim + 0x06, 0);
            m.WriteU16(prim + 0x26, 0x1FD);
            m.WriteU16(prim + 0x32, 8);
            prim = m.ReadU32(prim);
        }
    }

    public static void InitFade(CpuContext c, IMemory m)
    {
        c.A0 = 1;
        c.A1 = 4;
        SoTN.AllocPrimitives(c, m);
        uint fadePrim = c.V0;
        m.WriteU32(FadeAddr + 0x0, fadePrim);
        LayoutFadePrims(m, fadePrim, 256);
        m.WriteU32(FadeAddr + 0x8, 0);
        m.WriteU32(FadeAddr + 0xC, 0);

        c.A0 = 4;
        c.A1 = 1;
        SoTN.AllocPrimitives(c, m);
        uint mapPrim = c.V0;
        m.WriteU32(FadeAddr + 0x4, mapPrim);
        uint prim = PrimBufAddr + mapPrim * PrimStride;
        m.WriteU8(prim + 0x0C, 0);
        m.WriteU8(prim + 0x0D, 0);
        m.WriteU8(prim + 0x18, 0xFF);
        m.WriteU8(prim + 0x19, 0);
        m.WriteU8(prim + 0x24, 0);
        m.WriteU8(prim + 0x25, 0xFF);
        m.WriteU8(prim + 0x30, 0xFF);
        m.WriteU8(prim + 0x31, 0xFF);
        m.WriteU16(prim + 0x1A, 0x1D);
        m.WriteU16(prim + 0x0E, 0x1C0);
        m.WriteU16(prim + 0x26, 0x1FE);
        m.WriteU16(prim + 0x32, 8);
    }

    public static void SetFadeWidth(CpuContext c, IMemory m)
    {
        LayoutFadePrims(m, m.ReadU32(FadeAddr), (int)c.A0);
    }

    struct Layer
    {
        public uint Layout, GfxPage, GfxIndex, Clut, Flags;
        public int Order, RoomW, RoomH, ScrollX, ScrollY, TpageBase;
        public bool Wrap, SemiTrans, ClutAlt;
    }

    public static void RenderTilemapWide(CpuContext c, IMemory m)
    {
        uint buf = m.ReadU32(CurrentBufferPtr);
        uint ot = buf + OtOfs;
        int bbX = (int)m.ReadU32(BackbufferX);
        int bbY = (int)m.ReadU32(BackbufferY);
        int marginCols = (Display.WideMargin(256) + 15) / 16;

        uint sp16 = m.ReadU32(GpuUsageAddr + 0x14);
        uint dm = m.ReadU32(GpuUsageAddr + 0x00);

        Span<Layer> layers = stackalloc Layer[1 + MaxBgLayers];
        int n = 0;

        uint hide = m.ReadU32(TilemapAddr + 0x28);
        if (hide > 0)
            m.WriteU32(TilemapAddr + 0x28, hide - 1);
        else if (ReadLayer(m, TilemapAddr, out var fg))
            layers[n++] = fg;

        for (int l = 0; l < MaxBgLayers; l++)
        {
            uint addr = BgLayersAddr + (uint)l * BgLayerStride;
            hide = m.ReadU32(addr + 0x28);
            if (hide > 0) { m.WriteU32(addr + 0x28, hide - 1); continue; }
            if (ReadLayer(m, addr, out var bg))
                layers[n++] = bg;
        }

        int budget = 0;
        if (marginCols > 0)
        {
            int stock = 0;
            for (int i = 0; i < n; i++)
                stock += CountStock(m, in layers[i]);
            budget = Math.Max(0, MaxSprt16 - (int)sp16 - stock);
        }

        for (int i = 0; i < n; i++)
            DrawLayer(m, in layers[i], buf, ot, bbX, bbY, marginCols, ref sp16, ref dm, ref budget);

        m.WriteU32(GpuUsageAddr + 0x14, sp16);
        m.WriteU32(GpuUsageAddr + 0x00, dm);
    }

    static bool ReadLayer(IMemory m, uint addr, out Layer L)
    {
        L = default;
        uint flags = m.ReadU32(addr + 0x1C);
        if ((flags & LayerShow) == 0) return false;
        uint tileDef = m.ReadU32(addr + 0x04);
        if (tileDef == 0) return false;
        L.Layout = m.ReadU32(addr + 0x00);
        L.GfxPage = m.ReadU32(tileDef + 0x00);
        L.GfxIndex = m.ReadU32(tileDef + 0x04);
        L.Clut = m.ReadU32(tileDef + 0x08);
        L.Order = (int)m.ReadU32(addr + 0x18);
        L.Flags = flags;
        L.RoomW = (int)m.ReadU32(addr + 0x20) * 16;
        L.RoomH = (int)m.ReadU32(addr + 0x24) * 16;
        L.ScrollX = (int)m.ReadU32(addr + 0x08) >> 16;
        L.ScrollY = (int)m.ReadU32(addr + 0x0C) >> 16;
        L.Wrap = (flags & LayerWrapBg) != 0;
        L.SemiTrans = (flags & LayerSemiTrans) != 0;
        L.ClutAlt = (flags & LayerClutAlt) != 0;
        L.TpageBase = L.SemiTrans ? (int)(flags & LayerTpageId) : 0;
        return true;
    }

    static int CountStock(IMemory m, in Layer L)
    {
        int startTx = L.ScrollX >> 4, startTy = L.ScrollY >> 4;
        if (L.Wrap) { startTx &= 0xF; startTy &= 0xF; }
        int count = 0;
        for (int i = 0; i < VertiTiles; i++)
        {
            int ty = startTy + i;
            if (L.Wrap) ty &= 0xF;
            if (ty < 0) continue;
            if (ty >= L.RoomH) break;
            int row = ty * L.RoomW;
            for (int j = 0; j < HorizTiles; j++)
            {
                int tx = startTx + j;
                if (L.Wrap) tx &= 0xF;
                if (tx < 0) continue;
                if (tx >= L.RoomW) break;
                if (m.ReadU16(L.Layout + (uint)(row + tx) * 2) != 0) count++;
            }
        }
        return count;
    }

    static void DrawLayer(IMemory m, in Layer L, uint buf, uint ot, int bbX, int bbY,
        int marginCols, ref uint sp16, ref uint dm, ref int budget)
    {
        int subX = L.ScrollX & 0xF, startTx = L.ScrollX >> 4;
        int subY = L.ScrollY & 0xF, startTy = L.ScrollY >> 4;
        if (L.Wrap) { startTx &= 0xF; startTy &= 0xF; }

        uint cmd = L.SemiTrans ? 0x7Fu : 0x7Du;
        int y0 = bbY - subY;
        for (int i = 0; i < VertiTiles; i++, y0 += 16)
        {
            int ty = startTy + i;
            if (L.Wrap) ty &= 0xF;
            if (ty < 0) continue;
            if (ty >= L.RoomH) break;
            int row = ty * L.RoomW;
            int x0 = bbX - subX - marginCols * 16;
            for (int j = -marginCols; j < HorizTiles + marginCols; j++, x0 += 16)
            {
                int tx = startTx + j;
                if (L.Wrap) tx &= 0xF;
                if (tx < 0) continue;
                if (tx >= L.RoomW) break;
                ushort tile = m.ReadU16(L.Layout + (uint)(row + tx) * 2);
                if (tile == 0) continue;
                bool marginTile = j < 0 || j >= HorizTiles;
                if (marginTile && budget <= 0) continue;
                if (sp16 >= MaxSprt16) continue;
                if (marginTile) budget--;

                byte g = m.ReadU8(L.GfxIndex + tile);
                byte u = (byte)(g << 4);
                byte v = (byte)(g & 0xF0);
                byte page = m.ReadU8(L.GfxPage + tile);
                byte clutIdx = m.ReadU8(L.Clut + tile);
                ushort clut = m.ReadU16(ClutIdsAddr + (uint)((L.ClutAlt ? 0x100 : 0) + clutIdx) * 2);

                uint prim = buf + Sprite16Ofs + sp16 * 16;
                AddPrim(m, ot, L.Order + page, prim, 3);
                m.WriteU32(prim + 4, cmd << 24 | 0x808080);
                m.WriteU32(prim + 8, (uint)(ushort)y0 << 16 | (ushort)x0);
                m.WriteU32(prim + 12, (uint)clut << 16 | (uint)v << 8 | u);
                sp16++;
            }
        }

        for (int i = 0; i < 24; i++)
        {
            if (dm >= MaxDrawModes) break;
            int tpage = i + 8 + L.TpageBase;
            if ((L.Flags & LayerTpageAlt) != 0) tpage += 0x80;
            uint prim = buf + DrawModesOfs + dm * 12;
            AddPrim(m, ot, L.Order + i, prim, 2);
            m.WriteU32(prim + 4, 0xE1000000u | ((uint)tpage & 0x9FF));
            m.WriteU32(prim + 8, 0xE2000000u);
            dm++;
        }
    }

    static void AddPrim(IMemory m, uint ot, int idx, uint prim, uint len)
    {
        if ((uint)idx >= OtSize) return;
        uint entry = ot + (uint)idx * 4;
        uint old = m.ReadU32(entry);
        m.WriteU32(prim, len << 24 | (old & 0xFFFFFFu));
        m.WriteU32(entry, (old & 0xFF000000u) | (prim & 0xFFFFFFu));
    }
}
