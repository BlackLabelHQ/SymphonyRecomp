using System;
using System.Numerics;
using Hexa.NET.ImGui;
using RecompOne.UI;
using RecompOne.Runtime.Memory;
using Sotn;

namespace Recompiled;

public sealed class StatsCheatPanel : IEmbeddedPanel
{
    public string Name => "Stats";
    public string TitleKey => "panel.cheats.stats";
    public bool IsOpen { get; set; }

    bool _revealMap;

    public void Draw()
    {
        ImGui.SetNextWindowSize(new Vector2(340, 500), ImGuiCond.FirstUseEver);
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

        Ui.Text($"Character: {Player.Character}");
        Ui.Separator();

        Slider("Level", () => Player.Level, v => Player.Level = v, 1, 99);

        Ui.Section("Vitals");
        Input("HP", () => Player.Hp, v => Player.Hp = v);
        if (MaxButton("HP")) Player.Hp = Player.HpMax;
        Input("HP Max", () => Player.HpMax, v => Player.HpMax = v);
        Input("MP", () => Player.Mp, v => Player.Mp = v);
        if (MaxButton("MP")) Player.Mp = Player.MpMax;
        Input("MP Max", () => Player.MpMax, v => Player.MpMax = v);
        Input("Hearts", () => Player.Hearts, v => Player.Hearts = v);
        Input("Hearts Max", () => Player.HeartsMax, v => Player.HeartsMax = v);
        if (Ui.Button("Full heal"))
        {
            Player.Hp = Player.HpMax;
            Player.Mp = Player.MpMax;
            Player.Hearts = Player.HeartsMax;
        }

        Ui.Section("Attributes");
        Slider("STR", () => Player.Strength, v => Player.Strength = v, 1, 999);
        Slider("CON", () => Player.Constitution, v => Player.Constitution = v, 1, 999);
        Slider("INT", () => Player.Intelligence, v => Player.Intelligence = v, 1, 999);
        Slider("LCK", () => Player.Luck, v => Player.Luck = v, 1, 999);

        Ui.Section("Resources");
        Input("Gold", () => Player.Gold, v => Player.Gold = v);
        Input("EXP", () => Player.Exp, v => Player.Exp = v);

        //ImGui.SeparatorText("Map");
        //ImGui.Checkbox("Reveal full map", ref _revealMap); //broken, need to fix
        //if (_revealMap) RevealMap(m);
    }

   /*  static void RevealMap(IMemory m)
    {
        for (uint i = 0; i < Cheats.CastleMapSize; i += 4)
            m.WriteU32(Cheats.CastleMap + i, 0xFFFFFFFFu); 
    } */

    static bool MaxButton(string name)
    {
        if (Ui.UsesRows) return Ui.Button($"Max {name}##{name}");
        ImGui.SameLine();
        return ImGui.SmallButton($"Max##{name}");
    }

    static void Slider(string label, Func<int> get, Action<int> set, int min, int max)
    {
        int v = get();
        ImGui.SetNextItemWidth(190);
        if (Ui.Slider(label, ref v, min, max)) set(v);
    }

    static void Input(string label, Func<int> get, Action<int> set)
    {
        int v = get();
        ImGui.SetNextItemWidth(130);
        if (Ui.InputInt(label, ref v)) set(Math.Max(0, v));
    }
}
