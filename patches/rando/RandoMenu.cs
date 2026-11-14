using RecompOne.UI;

namespace Recompiled;

public static class RandoMenu
{
    public static void Register()
    {
        PanelManager.Register(new RandoPanel());
        SaveLoadManager.Register();

        MenuRegistry.BarPanel<RandoPanel>("menu.randomizer").Icon(FontSet.Star).After("menu.misc");
    }
}
