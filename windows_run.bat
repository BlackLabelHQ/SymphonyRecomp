@echo off
title SymphonyRecomp 

REM ============================================= Credits =============================================
	echo ========
	echo Credits:
	echo ========
	echo.
	echo This project was made possible by:
	echo.
    echo	BlackLabelHQ:
	echo	- flaffymg, creator of PSX Static Recompilation	Tool "RecompOne," Head of Project, Patching/Modding/Configuration System and much more (also possibly a Brazillian Cat)
	echo	- Derp Princess, fixed hundreds of issues with unmatched calls and crashes, extensive knowledge of the inner-workings of the game and general game knowledge to point out issues with static generation, creator of Blackout Mod (with inspiration from Wecoc's Dark Castle mod)... May also have forced a Pixie to sing, in Japanese.
	echo	- wowjinxy, initial version of SymphonyRecomp; initial patching / modding system, configuration system!
	echo.
	echo With major help from:
	echo.
	echo	- Mottzilla0, integration of randomizer compatibility and integrated rando
	echo	- eldri7ch, also integration of integrated rando; Added in-built Quality of Life Mods!
	echo.
	echo Special Thanks:
	echo.
	echo	- All of the Castlevania: Symphony of the Night Decomp project Contributors
	echo	- Dr4gonBlitz, showcasing the project on stream to help us find more bugs!
    echo	- JupiterClimb, showcasing the project on stream for advert
	echo	- Other Private Testers
	echo	- Koji Igarashi, and by extension Konami, for making this 10/10 game and game series!
	echo	- You, for playing our project of passion! ^(Aren't you special?^)
	echo.
	echo This project was PROUDLY made by huge fans of the series, and mostly important humans! No AI/Gen AI/LLMs were used in the making of this Recomp project! Our goal was to allow you to play your legally owned copy of the game, natively on your computer, with many nice features!
	echo.
	echo Final Note:
	echo Please Enjoy! ^(Seriously! It was a LOT of work!^)
	echo.
REM =================================================================================================

REM ========================================= Run SymphonyRecomp ====================================
echo.
echo ^> Running SymphonyRecomp...

if not exist "generated\" (
    echo Initial setup probably wasn't run, so we're running it for you... Aren't we nice?
    echo.
    echo Running windows_initial_build.bat...
    echo.
    call windows_initial_build.bat nocredits

    REM Stop if the build failed
    if errorlevel 1 (
        echo Initial build failed.
        pause
        exit /b 1
    )
)

dotnet run

echo.
REM =================================================================================================
