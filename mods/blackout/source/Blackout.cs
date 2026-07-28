using RecompOne.Runtime.Modding;
using RecompOne.Runtime;
using RecompOne.Runtime.Context;
using RecompOne.Runtime.Memory;
using RecompOne.Runtime.Hardware;
using Sotn;

/*
[PreHook("dra", "func_800FE044")]
[PostHook("dra", "func_800FE044")]
[Replace("dra", "func_800FE044")]
*/

public class BlackoutMod : IMod
{
    public void OnLoad()
    {
        /* Gives Soul of Bat, Echo of Bat, Spirit Orb, and Faerie Scroll */
        Inventory.GiveRelic(Relic.SoulOfBat, true);
        Inventory.GiveRelic(Relic.EchoOfBat, true);
        Inventory.GiveRelic(Relic.SpiritOrb, true);
        Inventory.GiveRelic(Relic.FaerieScroll, true);
    }

    [Replace("OVERLAY", "function")]
    public void SomeFunction(CpuContext c, IMemory m)
    {
        
    }
}