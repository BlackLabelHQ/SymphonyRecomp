using System;
using System.Numerics;
using ImGuiNET;
using RecompOne.Runtime.Host;
using RecompOne.Runtime.Host.Window;
using RecompOne.Runtime.Memory;
using Sotn;

namespace Recompiled;

public sealed class StatsCheatPanel : IPanel
{
    public string Name => "Stats";
    public bool IsOpen { get; set; }

    bool _revealMap;

    public void Draw()
    {
        ImGui.SetNextWindowSize(new Vector2(340, 500) * HostWindow.DpiScale, ImGuiCond.FirstUseEver);
        bool open = IsOpen;
        if (!ImGui.Begin(Name, ref open))
        {
            IsOpen = open;
            ImGui.End();
            return;
        }

        var m = RecompOne.Runtime.Runtime.Mem;
        if (m == null || !Cheats.InPlay())
        {
            ImGui.TextDisabled("Not in gameplay."); //dont mess with memory you arent supposed to when youre not supposed to >:(
            IsOpen = open;
            ImGui.End();
            return;
        }

        ImGui.Text($"Character: {Player.Character}");
        ImGui.Separator();

        Slider("Level", () => Player.Level, v => Player.Level = v, 1, 99);

        ImGui.SeparatorText("Vitals");
        Input("HP", () => Player.Hp, v => Player.Hp = v);
        ImGui.SameLine();
        if (ImGui.SmallButton("Max##hp")) Player.Hp = Player.HpMax;
        Input("HP Max", () => Player.HpMax, v => Player.HpMax = v);
        Input("MP", () => Player.Mp, v => Player.Mp = v);
        ImGui.SameLine();
        if (ImGui.SmallButton("Max##mp")) Player.Mp = Player.MpMax;
        Input("MP Max", () => Player.MpMax, v => Player.MpMax = v);
        Input("Hearts", () => Player.Hearts, v => Player.Hearts = v);
        Input("Hearts Max", () => Player.HeartsMax, v => Player.HeartsMax = v);
        if (ImGui.Button("Full heal"))
        {
            Player.Hp = Player.HpMax;
            Player.Mp = Player.MpMax;
            Player.Hearts = Player.HeartsMax;
        }

        ImGui.SeparatorText("Attributes");
        Slider("STR", () => Player.Strength, v => Player.Strength = v, 1, 999);
        Slider("CON", () => Player.Constitution, v => Player.Constitution = v, 1, 999);
        Slider("INT", () => Player.Intelligence, v => Player.Intelligence = v, 1, 999);
        Slider("LCK", () => Player.Luck, v => Player.Luck = v, 1, 999);

        ImGui.SeparatorText("Resources");
        Input("Gold", () => Player.Gold, v => Player.Gold = v);
        Input("EXP", () => Player.Exp, v => Player.Exp = v);

        //ImGui.SeparatorText("Map");
        //ImGui.Checkbox("Reveal full map", ref _revealMap); //broken, need to fix
        //if (_revealMap) RevealMap(m);

        IsOpen = open;
        ImGui.End();
    }

   /*  static void RevealMap(IMemory m)
    {
        for (uint i = 0; i < Cheats.CastleMapSize; i += 4)
            m.WriteU32(Cheats.CastleMap + i, 0xFFFFFFFFu); 
    } */

    static void Slider(string label, Func<int> get, Action<int> set, int min, int max)
    {
        int v = get();
        ImGui.SetNextItemWidth(190 * HostWindow.DpiScale);
        if (ImGui.SliderInt(label, ref v, min, max)) set(v);
    }

    static void Input(string label, Func<int> get, Action<int> set)
    {
        int v = get();
        ImGui.SetNextItemWidth(130 * HostWindow.DpiScale);
        if (ImGui.InputInt(label, ref v)) set(Math.Max(0, v));
    }
}
