using System.Numerics;
using Hexa.NET.ImGui;
using RecompOne.UI;
using Sotn;

namespace Recompiled;

public sealed class MovementCheatPanel : IEmbeddedPanel
{
    public string Name => "Movement Cheats";
    public string TitleKey => "panel.cheats.movement";
    public bool IsOpen { get; set; }

    int _inX;
    int _inY;

    public void Draw()
    {
        ImGui.SetNextWindowSize(new Vector2(320, 420), ImGuiCond.FirstUseEver);
        bool open = IsOpen;
        if (!ImGui.Begin(this.Title(), ref open))
        {
            IsOpen = open;
            ImGui.End();
            return;
        }

        DrawContent();

        IsOpen = open;
        ImGui.End();
    }

    public void DrawContent()
    {
        var m = RecompOne.Runtime.Runtime.Mem;
        if (m == null || !Cheats.InPlay())
        {
            ImGui.TextWrapped(Localization.T("common.not_in_play"));
            return;
        }

        int curX = (int)m.ReadU32(Cheats.PlayerXWorld);
        int curY = (int)m.ReadU32(Cheats.PlayerYWorld);

        Ui.Section("Position (room)");
        Ui.Text($"Current:  X {curX}   Y {curY}");
        Ui.InputInt("New X", ref _inX);
        Ui.InputInt("New Y", ref _inY);
        if (Ui.Button("Teleport"))
            MovementCheat.RequestTeleport(_inX, _inY);
        if (!Ui.UsesRows) ImGui.SameLine();
        if (Ui.Button("Copy Current"))
        {
            _inX = curX;
            _inY = curY;
        }

        Ui.Section("Speed"); //sp
        float speed = Player.Entity.VelocityX / (float)Cheats.One;
        Ui.Text($"Velocity X: {speed:0.00} px/f   (default 1.00x)");
        Ui.Toggle("Override Speed", ref MovementCheat.SpeedOverride);
        Ui.Slider("Multiplier", ref MovementCheat.SpeedMul, 0.1f, 5f, "%.2fx");

        Ui.Section("Jump");
        float velY = Player.Entity.VelocityY / (float)Cheats.One;
        Ui.Text($"Velocity Y: {velY:0.00} px/f");
        Ui.Slider("Strength", ref MovementCheat.JumpStrength, 1f, 16f, "%.1f px/f");
        Ui.Toggle("Override Jump", ref MovementCheat.JumpOverride);

        Ui.Section("Toggles");
        Ui.Toggle("Infinite Jump (Fly)", ref MovementCheat.InfiniteJump);
        Ui.Toggle("No Clip (press L2, then dpad)", ref MovementCheat.NoClip); //press or hold?
        Ui.Toggle("Invincible", ref MovementCheat.Invincible);
    }
}
