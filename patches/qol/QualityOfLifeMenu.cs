using RecompOne.Runtime.Events;
using RecompOne.Runtime.Host.Window;

namespace Recompiled;

public static class QualityOfLifeMenu
{
    public static void Register()
    {
        Event.AddListener<RuntimeReadyEvent>(_ => QualityOfLife.Load());
        PanelManager.Register(new QualityOfLifePanel());

        MenuRegistry.Menu("menu.misc", MenuRegistry.OrderGame)
            .Panel<QualityOfLifePanel>("panel.qol").Order(10);
    }
}
