using RecompOne.Runtime.Events;
using RecompOne.Runtime.Host.Window;

namespace Recompiled;

public static class SaveLoadManager
{
    static SaveStamp? _applied;

    public static SaveStamp? Applied => _applied;

    public static bool HasRando => _applied.HasValue;

    public static void Register()
    {
        Event.AddListener<SaveCreatedEvent>(OnSaveCreated);
        Event.AddListener<SaveLoadedEvent>(OnSaveLoaded);
        Event.AddListener<RuntimeReadyEvent>(_ => Clear());
    }

    public static void MarkApplied(SaveStamp stamp) => _applied = stamp;

    public static void Clear() => _applied = null;

    static void OnSaveCreated(SaveCreatedEvent e)
    {
        if (e.Block == 0) return;

        if (_applied is { } stamp) stamp.Write(e.Memory, e.Block);
        else SaveStamp.Clear(e.Memory, e.Block);
    }

    static void OnSaveLoaded(SaveLoadedEvent e)
    {
        if (e.Block == 0) return;

        bool stamped = SaveStamp.TryRead(e.Memory, e.Block, out var stamp);

        if (!stamped)
        {
            if (_applied.HasValue)
                RecompOne.Runtime.Runtime.ShowNotice(Localization.T("rando.save.vanilla_with_rando"));
            return;
        }

        if (_applied is { } current)
        {
            if (!current.Equals(stamp))
                RecompOne.Runtime.Runtime.ShowNotice(Localization.T("rando.save.mismatch"));
            return;
        }

        stamp.ApplyToRandomizer();
        Randomizer.RandomizeSeed();

        ToastNotifications.Show("rando.save.applied_title", "rando.save.applied", RandomIcon);
    }

    static uint RandomIcon()
    {
        var names = TrackerIcons.Names();
        if (names.Length == 0) return 0;

        var pick = names[Random.Shared.Next(names.Length)];
        return TrackerIcons.TryGetTexture(pick, out uint texture) ? texture : 0;
    }
}
