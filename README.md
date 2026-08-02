# Castlevania: Symphony of the Night PSX Recomp

The Castlevania: Symphony of the Night PlayStation Recomp is proudly brought to you by the BlackLabelHQ team! Please note this is an Open BETA and this is NOT the final version!

# Table Of Contacts

todo... Maybe later.

# Special Notes Section

This version is currently in BETA stages. You may experience disasterous game breaking bugs! Every effort has been done so that this will not happen but you should be warned regardless. Stable version 1.0 has YET to be released!

## Special Note 2

The goal of this project is to help bring the game to modern computers without some of the limitations of older consoles. This was accomplished through both recompilation means and decompilation efforts. Stay tuned for the full SOTN Decomp release by the SOTN Decomp community, which will be the de facto means of the modern "PC port" efforts once it's fully released.

## Special Note 3

As mentioned above, SymphonyRecomp is NOT the same as the SOTN Decomp project, although several members of Black Label HQ are contributing to that project, as well. They are seperate. Please treat them as such.

# Just Play The Game

If you'd like to just play the game via the recomp's PC port, you'll need to download the release from the [releases page](https://github.com/BlackLabelHQ/SymphonyRecomp/releases). It has a built-in auto-updater for the latest release :)

# Instructions To Build From Source

Clone repo. Add legally owned game files to disc. Run windows_run.bat or windows_initial_build.bat if you're on Windows, otherwise follow instructions contained within...

Will fix this later, sorry friends.

## Prerequisites

- [.NET 10](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- [Git](https://git-scm.com/install/)
- [SDL2](https://github.com/libsdl-org/SDL/releases/tag/release-2.32.10) - You'll place this inside the `SymphonyRecomp\bin\Debug\net10.0` directory!
- A legally owned copy of the PSX (PlayStation) version of Castlevania: Symphony of the Night to rip your game from, bin/cue format. The files should be hard named the following and placed inside the `disc` directory in the main directory of `SymphonyRecomp`.
    - Castlevania - Symphony of the Night (Track 1).bin
    - Castlevania - Symphony of the Night (Track 2).bin
    - Castlevania - Symphony of the Night (USA).cue

## Nice To Haves (If Wish To Contribute)

- [Visual Studio 2026](https://visualstudio.microsoft.com/downloads/) - More Ideal way to work with the project, you can also use VSCode.
- [VSCode](https://code.visualstudio.com/)

# Todo:

- The rest of the README.MD
