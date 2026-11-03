# pre-release 1.1.2 (this is a pre-release for 1.0.0)
this is a pre-release, it has a lot of new stuff and wasnt tested as much as a full release, so expect some bugs, please report anything you find!

heads up: the settings system was rewritten, so your settings from previous versions are reset to the defaults

# Changes
- added language packs (put them in the translations folder)
- added a game text language option, separate from the interface language
- added room extension (settings > display), fills the area outside the room on widescreen with custom fills, a color fill, or tun off
- added room fills for most rooms of the castle, some still need revision tho
- added support for slang/libretro shader presets through ShaderMod
- added the Asset Pack manager and an in-game Asset Pack Editor to create and edit packs
- texture replacement now works properly on alucard, enemies, items, hud and tilesets with a custom provider (games sends this data weirdly by default)
- added true 24bit color option
- added option to control the display of the dithering
- added autohide for top bar
- added autohide mouse after 5 seconds
- added a crash popup that shows where the crash log was saved
- added skip prologue to the QoL options
- disabling letterbox now zooms the image to cover the bars instead of removing the bars
- fixed water in the underground caverns, reverse caverns, castle entrance and scylla fight in widescreen
- fixed lava glow in the catacombs in widescreen
- fixed trees and foliage in the castle entrance intro in widescreen
- fixed the castle destruction effect in the prologue in widescreen
- fixed static backgrounds ending at the 4:3 border in some rooms
- fixed characters disappearing early near the screen edges, they now fade
- richter's hud now follows the widescreen margin correctly
- fixed the game over screen melting effect being corrupted without letterbox, the game over image being stretched and the title screen corrupting after it
- fixed richter not being able to slide near the screen edges in widescreen
- fixed richter's whip, subweapons and effects disappearing near the screen edges in widescreen
- fixed richter's cross, rebound stone and agunea disappearing before leaving the screen
- fixed a one frame jump on horizontal room transitions
- improved stability overhaul
- improved other widescreen effects

Just to reinterate, this is a pre-release version, some stuff is not yet fully tested or finished

# Dependencies

You're required to install [dotnet 10 runtime](https://dotnet.microsoft.com/pt-br/download/dotnet/10.0) to run the recomp, otherwise the game will not open, ideally also make sure to install [OpenAL](https://www.openal.org/downloads/), as of the current version no further dependencies are required

you can find instructions downloading dotnet 10 for linux systems that dont have it on their packages [here](https://wiki.archlinux.org/title/.NET), or you can run "run.sh" wich will install dotent if not available and execute sotn for you
