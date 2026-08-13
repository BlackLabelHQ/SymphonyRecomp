using RecompOne.Runtime.Memory;
using Recompiled;

if (AutoUpdater.HandleRelaunch(args)) return 0;

var asm = System.Reflection.Assembly.GetExecutingAssembly();

if (Array.Find(asm.GetManifestResourceNames(), n => n.EndsWith(".languages.json", StringComparison.OrdinalIgnoreCase)) is { } languagesRes)
    RecompOne.Runtime.Runtime.AddLanguages(asm, languagesRes);

RecompOne.Runtime.Runtime.SetStartupNotice("startup.beta", "startup.title", "SymphonyRecompBetaAck");

DiscCheck.Register();
WidescreenPatch.Register();
WidescreenSettings.Register();
ThroneLeftFill.Register();
GameMenu.Register();
CheatMenu.Register();
QualityOfLifeMenu.Register();
TrackerMenu.Register();
RandoMenu.Register();
AutoUpdater.Register();
HelpMenu.Register();

var title = AutoUpdater.CurrentTag is { } tag ? $"SymphonyRecomp {tag}" : "SymphonyRecomp"; //get version too

if (Array.Find(asm.GetManifestResourceNames(), n => n.EndsWith(".SymphonyRecomp.ico", StringComparison.OrdinalIgnoreCase)) is { } iconRes)
{
    using var iconStream = asm.GetManifestResourceStream(iconRes)!;
    using var iconMem = new MemoryStream();
    iconStream.CopyTo(iconMem);
    RecompOne.Runtime.Runtime.SetIcon(iconMem.ToArray());
}

RecompOne.Runtime.Runtime.Run(() => Entry.Run(new PSMemory(), args.Length > 0 ? args[0] : null, title));
return 0;
