# Castlevania: Symphony of the Night PSX Recomp

The Castlevania: Symphony of the Night PlayStation Recomp, called SymphonyRecomp, is proudly brought to you by the BlackLabelHQ team! 

# Please Read This
Before we get started on the README  - This project is a "RE"comp. It is NOT a "DE"comp. Please do NOT go to the SOTN Decomp Discord server to talk about SymphonyRecomp. They are two separate concepts! We, however, encourage you to help out with the SOTN Decomp project if you're interested in helping us fully DECOMPILE the game!

Please note this is an Open BETA and this is NOT the final version! This Recomp was made by human hands, no AI is involved in writing this code!

# Do You Just Want To Play?
If you just want to play [download the latest release here](https://github.com/BlackLabelHQ/SymphonyRecomp/releases)!

# Do You Need Help?
You can join our Discord or open an issue on this GitHub! Again, you'll join the BlackLabelHQ Discord Server for help... NOT the SOTN Decomp server.

[![Discord](https://discord.com/api/guilds/1525942688728481983/widget.png?style=banner2)](https://discord.gg/65g8ZEPnbR)

# Special Notes Section

This version is currently in BETA stages. You may experience disastrous game breaking bugs! Every effort has been done so that this will not happen but you should be warned regardless. Stable version 1.0 has YET to be released!

The goal of this project is to help bring the game to modern computers without some of the limitations of older consoles. This was accomplished through both recompilation means and decompilation efforts. Stay tuned for the full SOTN Decomp release by the SOTN Decomp community, which will be the de facto means of the modern "PC port" efforts once it's fully released.

As mentioned above, SymphonyRecomp is NOT the same as the SOTN Decomp project, although several members of Black Label HQ are contributing to that project, as well. They are separate. Please treat them as such.

# Instructions To Build From Source

Clone repo. Add legally owned game files to disc. Run windows_run.bat or windows_initial_build.bat or manually run RecompOne against sotn.json, this will produce the game code, you can then compile it yourself, dev builds do not auto-update

## Prerequisites
- A GPU that supports at least OpenGL 3.3 (Desktop) or OpenGL ES 3.0+ (Android)
- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- [OpenAL](https://www.openal.org/documentation/) (Desktop)
- [Git](https://git-scm.com/install/)
- A legally owned copy of the North American PSX (PlayStation) version of Castlevania: Symphony of the Night to rip your game from, bin/cue format. The files should be hard named the following and placed inside the `disc` directory in the main directory of `SymphonyRecomp`.
    - Castlevania - Symphony of the Night (Track 1).bin
    - Castlevania - Symphony of the Night (Track 2).bin
    - Castlevania - Symphony of the Night (USA).cue

---

## 📱 Android Build & Playing Instructions

### Building the Android APK
1. Install the .NET 10 SDK with Android Workload:
   ```bash
   dotnet workload install android
   ```
2. Place the PSX game disc files into the `disc/` folder (or copy them to `/sdcard/Android/data/com.blacklabelhq.sotn/files/disc/` on your device).
3. Publish the Release APK:
   ```bash
   dotnet publish RecompOne.SoTN.Android.csproj -c Release
   ```
4. The generated signed APK will be located at:
   `bin/Release/net10.0-android/com.blacklabelhq.sotn-Signed.apk`
5. Install on your Android phone, tablet, or handheld (Retroid Pocket, Odin, etc.):
   ```bash
   adb install -r bin/Release/net10.0-android/com.blacklabelhq.sotn-Signed.apk
   ```

### Android Features & Controls
- **⚙️ In-Game Menu**: Tap the yellow **⚙ MENU** button on-screen to access Cheats, Display Settings, Touch Controls, and Disc Reloader.
- **⚡ Built-in Cheats**: Includes Full Heal, God Mode (Max Stats & Gold), Level 99, and Max Gold toggles.
- **📱 Dynamic Aspect Ratio & Auto-Fit**: Supports 4:3 Original, 16:9 Widescreen, Stretch, and **Auto-Fit Device** (dynamic fitting for landscape and portrait).
- **🔄 Auto-Rotate & Orientation Lock**: Choose Auto-Rotate (Sensor), Lock Landscape, or Lock Portrait under Display Settings.
- **🎮 Controller & Touch Overlay**:
  - Full PSX Touch Control Overlay with D-Pad, 🔺 🟦 🔴 ✖ Action buttons, L1/L2/R1/R2, Select, and Start.
  - Native Bluetooth, USB, and Handheld Controller support (Retroid Pocket, Xbox, DualSense, Odin).
- **🔊 Native Audio**: High-fidelity 44.1kHz audio powered by native `Android.Media.AudioTrack`.

---

## Nice To Haves (If Wish To Contribute)

- [Visual Studio 2026](https://visualstudio.microsoft.com/downloads/) - More Ideal way to work with the project, you can also use VSCode.
- [VSCode](https://code.visualstudio.com/)

## How Was This Made?
this project was made using RecompOne to statically recompile the game, it also used some references from the decomp to help name functions and make patches, please show some love for the Decomp team, they deserve it!

# Todo:

- The rest of the README.MD ... eventually.
