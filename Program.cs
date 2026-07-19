using RecompOne.Runtime.Memory;
using Recompiled;

WidescreenSettings.Register();
SotNAudioSettings.Register();
CheatMenu.Register();
QualityOfLifeMenu.Register();

var m = new PSMemory();
Entry.Run(m, args.Length > 0 ? args[0] : null);
return 0;
