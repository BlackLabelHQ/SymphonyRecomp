using RecompOne.Runtime.Events;
using RecompOne.UI;

namespace Recompiled;

public static class QualityOfLifeMenu
{
    public static void Register()
    {
        Event.AddListener<RuntimeReadyEvent>(_ => QualityOfLife.Load());
        foreach (var option in QualityOfLifeOptions.All)
            option.Changed += _ => QualityOfLife.Load();
        PanelManager.Register(new QualityOfLifePanel());

        MenuRegistry.Menu("menu.misc").After("menu.mods").Panel<QualityOfLifePanel>("panel.qol");
    }
}
