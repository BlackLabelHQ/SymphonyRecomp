using RecompOne.Runtime.Modding;
using RecompOne.Runtime;
using RecompOne.Runtime.Context;
using RecompOne.Runtime.Memory;
using RecompOne.Runtime.Hardware;
using Sotn;
using Recompiled;
using RecompOne.Runtime.Events;

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
    public bool inStage = false; // False - In CD Room or in Main Menu // True - In Stage

    /* Internal State Primary */
    public byte currentFamiliar = 0xFF;
    public byte batEchoTimer = 100;

    /* Customizable Options */
    public bool giveBlackoutStartingRelics = true;
    public bool giveMoreStartingMP = true;

    public void OnLoad()
    {
        Event.AddListener<PlayerLoadedEvent>(Init);
        Event.AddListener<VSyncEvent>(OnVSyncEvent);
    }

    public void Init(PlayerLoadedEvent ple)
    {
        if (giveBlackoutStartingRelics) GiveBlackoutRelics();
        if (giveMoreStartingMP) GivePlayerHigherStartingMP();
    }

    public void OnVSyncEvent(VSyncEvent vse)
    {
        if (!forceLightsOn && Game.InGame && !Game.IsLoading) BlackoutMap();
    }

    /* Sets All Class Variables to False, basically */
    private void ClearFlags()
    {
        giveBlackoutStartingRelics = false;
        giveMoreStartingMP = false;
    }

    private void BlackoutMap()
    {
        Stages.Shade(0, 0, 0);
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
        Player.MpMax = 200;
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