using RecompOne.Runtime.Memory;
using RecompOne.Runtime.Updates;
using Recompiled;

RecompOne.Runtime.Config.PortSettings.PrimaryKind = RecompOne.Runtime.Config.PadKind.Digital;

if (AutoUpdater.HandleRelaunch(args)) return 0;
args = AutoUpdater.ConsumeArguments(args);

RecompOne.Runtime.Runtime.UserInterface = RecompOne.UI.BigPicture.UserInterfaces.Create();

var asm = System.Reflection.Assembly.GetExecutingAssembly();

if (Array.Find(asm.GetManifestResourceNames(), n => n.EndsWith(".languages.json", StringComparison.OrdinalIgnoreCase)) is { } languagesRes)
    RecompOne.UI.Localization.MergeEmbedded(asm, languagesRes);

RecompOne.UI.StartupNoticePopup.Set("startup.beta", "startup.title", "SymphonyRecompBetaAck");

RecompOne.Runtime.Features.FrameInterpolation = false;

Directory.CreateDirectory("translations");

DiscCheck.Register();
Recompiled.Translation.GameLanguage.Register();
WidescreenPatch.Register();
TextureSourcesPatch.Register();
RoomFills.Register();
GameMenu.Register();
CheatMenu.Register();
QualityOfLifeMenu.Register();
TrackerMenu.Register();
RandoMenu.Register();
AutoUpdater.Configure("BlackLabelHQ/SymphonyRecomp");
HelpMenu.Register();

var title = AutoUpdater.CurrentTag is { } tag ? $"SymphonyRecomp {tag}" : "SymphonyRecomp"; //get version too

if (Array.Find(asm.GetManifestResourceNames(), n => n.EndsWith(".SymphonyRecomp.ico", StringComparison.OrdinalIgnoreCase)) is { } iconRes)
{
    using var iconStream = asm.GetManifestResourceStream(iconRes)!;
    using var iconMem = new MemoryStream();
    iconStream.CopyTo(iconMem);
    RecompOne.Runtime.Runtime.SetIcon(iconMem.ToArray());
}

const uint RamSize = 0x00800000; //8

RecompOne.Runtime.Runtime.Run(() => Entry.Run(new PSMemory(RamSize), args.Length > 0 ? args[0] : null, title));
return 0;
