using System.Numerics;
using ImGuiNET;
using RecompOne.Runtime.Host.Window;
using Sotn;

namespace Recompiled;

/*
 * Mega Man X-style vertical segmented health bar, drawn as a plain ImGui
 * foreground overlay (no native GPU primitives touched, no native HUD draw
 * call hooked) -- the safest option available after this session's repeated
 * crashes from touching native engine internals. Reads Player.Hp/HpMax each
 * frame; does not suppress SOTN's native HP display (doing so would require
 * hooking the native DrawHud function, which is exactly the kind of native
 * hook this file deliberately avoids), so both are visible together.
 *
 * Positioned via OutputPanel.NativeToScreen, not raw ForegroundDrawList
 * coordinates -- the game picture lives inside a dockable/resizable ImGui
 * panel, not the raw OS window, so a fixed screen-pixel origin landed
 * outside the visible game area (confirmed by real playtesting).
 */
public sealed class XHealthBarPanel : IFloatingPanel
{
    public string Name => "X Health Bar";
    public bool IsOpen { get; set; } = true;

    const int SegmentCount = 16;
    const float SegmentWidth = 14f;
    const float SegmentHeight = 8f;
    const float SegmentGap = 2f;
    const float OriginX = 12f;
    const float OriginY = 24f;

    public void Draw()
    {
        if (!Game.Available || !Game.InGame || Game.IsLoading) return;
        if (!OutputPanel.HasRect) return;

        int hp = Player.Hp;
        int hpMax = Player.HpMax;
        if (hpMax <= 0) return;

        float frac = System.Math.Clamp((float)hp / hpMax, 0f, 1f);
        int filled = (int)System.MathF.Round(frac * SegmentCount);

        var draw = ImGui.GetForegroundDrawList();
        uint colBorder = ImGui.GetColorU32(new Vector4(1f, 1f, 1f, 0.9f));
        uint colEmpty = ImGui.GetColorU32(new Vector4(0.08f, 0.08f, 0.08f, 0.85f));
        uint colFill = ImGui.GetColorU32(new Vector4(0.95f, 0.15f, 0.15f, 0.95f));

        // Segments stack top-to-bottom, draining from the top as the classic
        // X health bar does (index 0 = topmost = last to empty... actually X
        // drains from the top down, so the TOP segment empties first as
        // damage is taken -- i.e. remaining health fills from the BOTTOM up).
        for (int i = 0; i < SegmentCount; i++)
        {
            float nx0 = OriginX, nx1 = OriginX + SegmentWidth;
            float ny0 = OriginY + i * (SegmentHeight + SegmentGap);
            float ny1 = ny0 + SegmentHeight;
            var min = OutputPanel.NativeToScreen(nx0, ny0);
            var max = OutputPanel.NativeToScreen(nx1, ny1);

            // Segment index from the bottom: bottom segment is filled first.
            int fromBottom = SegmentCount - 1 - i;
            bool on = fromBottom < filled;

            draw.AddRectFilled(min, max, on ? colFill : colEmpty);
            draw.AddRect(min, max, colBorder);
        }
    }
}
