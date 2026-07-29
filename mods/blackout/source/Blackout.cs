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
    public bool giveMoreStartingMP = false;

    public void OnLoad()
    {
        Event.AddListener<PlayerLoadedEvent>(Init);
        Event.AddListener<VSyncEvent>(OnVSyncEvent);
    }

    public void OnVSyncEvent(VSyncEvent vse)
    {
        
    }

    public void Init(PlayerLoadedEvent ple)
    {
        GiveBlackoutRelics();
        if (giveMoreStartingMP) GivePlayerHigherStartingMP();
        
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