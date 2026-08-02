using RecompOne.Runtime.Modding;
using RecompOne.Runtime;
using RecompOne.Runtime.Context;
using RecompOne.Runtime.Memory;
using RecompOne.Runtime.Hardware;
using Sotn;
using Recompiled;
using RecompOne.Runtime.Events;
using ImGuiNET;

/*
[PreHook("dra", "func_800FE044")]
[PostHook("dra", "func_800FE044")]
[Replace("dra", "func_800FE044")]
*/

public class BlackoutMod : IMod
{
    /* Debug */
    public bool forceLightsOn = false;

    /* Internal State Core */
    public bool shouldUpdatePalette = true; 

    /* Internal State Primary */
    public bool blackoutEntities = true;
    public bool invisibleEntities = false;

    /* Internal State Secondary */
    public byte currentFamiliar = 0xFF;
    public byte batEchoTimer = 100;

    /* Customizable Options */
    public bool giveBlackoutStartingRelics = true;
    public bool giveMoreStartingMP = true;

    public void DrawSettings()
    {
        //float v = DarkStage.Brightness;
        //if (ImGui.SliderFloat("Brightness", ref v, 0f, 1f, "%.2f"))
        //    DarkStage.Brightness = v;

        if(ImGui.Checkbox("Completely Blackout Entities", ref blackoutEntities)) BlackoutEntities(); 
        if(ImGui.Checkbox("Invisible Enemies", ref invisibleEntities)) BlackoutEntities(); 
    }

    public void OnLoad()
    {
        BlackoutEntities();

        Event.AddListener<PlayerLoadedEvent>(Init);
        Event.AddListener<RoomLayerLoadEvent>(OnRoomLayerLoadEvent);
    }

    public void OnUnload()
    {
        Event.RemoveListener<PlayerLoadedEvent>(Init);
        Event.RemoveListener<RoomLayerLoadEvent>(OnRoomLayerLoadEvent);

        Stages.Restore();
        Stages.RestoreEntities();
    }

    public void Init(PlayerLoadedEvent ple)
    {
        if (giveBlackoutStartingRelics) GiveBlackoutRelics();
        if (giveMoreStartingMP) GivePlayerHigherStartingMP();
    }


    public void OnRoomLayerLoadEvent(RoomLayerLoadEvent rle)
    {
        if (!forceLightsOn && Game.InGame && !Game.IsLoading) BlackoutMap();
        if (!forceLightsOn && Game.InGame && !Game.IsLoading && blackoutEntities) BlackoutEntities();
    }

    /* Sets All Class Variables to False, basically */
    private void ClearFlags()
    {
        giveBlackoutStartingRelics = false;
        giveMoreStartingMP = false;
    }

    private void BlackoutMap()
    {
        // Stages.Shade(0, 0, 0);

        Stages.WriteAllPalettes(0x8000);
    }

    private void BlackoutEntities()
    {
        // Stages.ShadeEntities(0, 0, 0);
        if (blackoutEntities) Stages.WriteAllEntityPalettes(0x8000); else Stages.RestoreEntities();
        if (invisibleEntities) Stages.ShadeEntities(0, 0, 0); else Stages.RestoreEntities();
    }

    /* Gives Soul of Bat, Echo of Bat, Spirit Orb, and Faerie Scroll */
    private void GiveBlackoutRelics()
    {
        Inventory.GiveRelic(Relic.SoulOfBat, true);
        Inventory.GiveRelic(Relic.EchoOfBat, true);
        Inventory.GiveRelic(Relic.SpiritOrb, true);
        Inventory.GiveRelic(Relic.FaerieScroll, true);
    }

    private void GivePlayerHigherStartingMP()
    {
        Player.MpMax += 200;
    }

    private uint GetEchoOfBatTimer()
    {
        return 0;
    }

    [Replace("OVERLAY", "function")]
    public void SomeFunction(CpuContext c, IMemory m)
    {
        
    }
}