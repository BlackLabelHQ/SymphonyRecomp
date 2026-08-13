using RecompOne.Runtime.Host.Window;

namespace Recompiled;

public static class RandoMenu
{
    public static void Register()
    {
        PanelManager.Register(new RandoPanel());
        SaveLoadManager.Register();

        MenuRegistry.BarItem("menu.randomizer", Toggle, 200);
    }

    static void Toggle()
    {
        if (PanelManager.Get<RandoPanel>() is { } panel) panel.IsOpen = !panel.IsOpen;
    }
}
