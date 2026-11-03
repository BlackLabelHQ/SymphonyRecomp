using RecompOne.Runtime.Assets;
using RecompOne.Runtime.Bios;
using RecompOne.Runtime.Config;
using RecompOne.Runtime.Events;
using RecompOne.Runtime.Memory;
using RecompOne.Runtime.Modding;

namespace Recompiled.Translation;

public static class GameLanguage
{
    public const string OriginalPack = "";
    public const string DefaultPack = "default";

    private const string Owner = "translation-pack";
    private const uint CdStepAddr = 0x8006C398;
    private const int TexturePriority = 10000;

    public static readonly Option<string> Pack = Options.String("translation.pack", DefaultPack);

    private sealed record Request(string Id, Task<LanguagePack> Task);

    private static Request? _request;
    private static FontPack? _original;
    private static bool _loaded;

    public static string Active { get; private set; } = DefaultPack;
    public static string? Error { get; private set; }
    public static bool Pending => Volatile.Read(ref _request) != null;

    internal static FontPack? Fonts { get; private set; }

    public static void Register()
    {
        LanguagePacks.EnsureRoot();
        TranslationSettings.Register();
        Event.AddListener<RuntimeReadyEvent>(_ => EnsureLoaded());
        Event.AddListener<OverlayLoadedEvent>(overlay => TextPatches.OverlayLoaded(overlay.Name));
        Event.AddListener<VSyncEvent>(Update);
        Event.AddListener<TilemapRenderedEvent>(CornerTextMarks.Combine);
    }

    public static void Select(string id)
    {
        Error = null;
        Pack.Value = id;
        Volatile.Write(ref _request, new Request(id, Task.Run(() => LanguagePacks.Read(id))));
    }

    internal static void EnsureLoaded()
    {
        if (_loaded) return;
        _loaded = true;
        _original = FontPack.FromBios();
        string id = Pack;
        try
        {
            Apply(LanguagePacks.Read(id), null);
        }
        catch (Exception error) when (error is not OutOfMemoryException)
        {
            Active = OriginalPack;
            Error = error.Message;
            Console.Error.WriteLine($"[translation] Language pack rejected: {error.Message}");
        }
    }

    private static void Update(VSyncEvent frame)
    {
        TextPatches.Update(frame.Memory);
        bool dialogue = ScriptMemory.EndFrame();
        var request = Volatile.Read(ref _request);
        if (request == null || !request.Task.IsCompleted) return;
        if (!request.Task.IsFaulted && (dialogue || frame.Memory.ReadU32(CdStepAddr) != 0)) return;
        if (!ReferenceEquals(Interlocked.CompareExchange(ref _request, null, request), request)) return;
        try
        {
            Apply(request.Task.GetAwaiter().GetResult(), frame.Memory);
        }
        catch (Exception error) when (error is not OutOfMemoryException)
        {
            Error = error.Message;
            if (!Pending) Pack.Value = Active;
            Console.Error.WriteLine($"[translation] Language pack rejected: {error.Message}");
        }
    }

    private static void Apply(LanguagePack pack, IMemory? memory)
    {
        Fonts = pack.Fonts;
        var baseline = _original!;
        KromFont.KromSetFont1(Fonts.Bios.GetValueOrDefault("font1", baseline.Bios["font1"]));
        KromFont.KromSetFont2(Fonts.Bios.GetValueOrDefault("font2", baseline.Bios["font2"]));
        KromFont.KromSetTable(Fonts.Table ?? baseline.Table!);
        DialogueAudio.Load(pack.Audio, memory);
        TextPatches.Load(pack.Text, pack.Dialogue, memory);
        ApplyExtensions(pack.Root);
        if (memory != null) FontGraphics.Apply(Fonts, memory);
        Active = pack.Id;
        Error = null;
    }

    private static void ApplyExtensions(string? root)
    {
        if (root == null) ModLoader.UnloadScripts(Owner);
        else ModLoader.LoadScripts(Owner, Path.Combine(root, "code"));

        var textures = root == null ? null : Path.Combine(root, "textures");
        if (textures != null && Directory.Exists(textures))
            AssetReplacerManager.Instance.LoadTextureFolder(Owner, textures, TexturePriority);
        else
            AssetReplacerManager.Instance.UnloadTextureFolder(Owner);
    }
}
