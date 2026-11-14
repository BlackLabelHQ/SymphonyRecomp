# pre-release 1.2.0 (this is a pre-release for 1.0.0)
this is a pre-release, it has a lot of new stuff and wasnt tested as much as a full release, so expect some bugs, please report anything you find!

heads up: the settings system was rewritten, so your settings from previous versions are reset to the defaults

# Changes
- Improved some widescreen room extensions, fixed some corruptions
- Added "BigPicture" UI
- Improved asset pack audio handling
- Improved asset pack image handling (added filtering and stretching options)
- Added CD Room extension
- Improved recomp config

## Breaking: recompone now usese hexaimgui instead of imguinet and variants

Just to reinterate, this is a pre-release version, some stuff is not yet fully tested or finished

# Dependencies

You're required to install [dotnet 10 runtime](https://dotnet.microsoft.com/pt-br/download/dotnet/10.0) to run the recomp, otherwise the game will not open, ideally also make sure to install [OpenAL](https://www.openal.org/downloads/), as of the current version no further dependencies are required

you can find instructions downloading dotnet 10 for linux systems that dont have it on their packages [here](https://wiki.archlinux.org/title/.NET), or you can run "run.sh" wich will install dotent if not available and execute sotn for you
