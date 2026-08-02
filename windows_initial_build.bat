@echo off
title SymphonyRecomp Initial Build

REM =============================================== INIT ==============================================
set "DISC=disc"
set "CONFIG=config"
set "RECOMPONE=RecompOne"
set "RECOMPILER=%RecompOne%/RecompOne.Recompiler"
set "MISSING=0"
set "SOTNJSON=%CONFIG%/sotn.json"
REM ===================================================================================================

REM ============================================= Credits =============================================
if /I not "%~1"=="nocredits" (
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
)
REM ===================================================================================================

REM ================================= Did You Mean To Run This File? ==================================
if exist "generated\" (
    echo.
    echo Note:
    choice /m "You've probably already initialized this project before. Are you sure you meant to run this?"
    if errorlevel 2 (
        echo Cancelled.
	echo.
        pause
	exit /b 1
    )
)
REM ===================================================================================================

echo ============================================================================================
echo Pull and Initialize Dependencies, Build C# Solutions, and Build from sotn.json!
echo ============================================================================================

REM ======================================== Update Submodules ========================================
echo.
echo ^> Updating Submodules...
git submodule update --init --recursive >nul 2>&1

if errorlevel 1 (
    echo Failed to update submodules! The project will not work correctly without them!
    echo.
    choice /m "Do you want to continue anyway? Hint: Probably not, but if you had them already and they just failed to update...

    if errorlevel 2 (
        echo Cancelled.
	echo.
        pause
	exit /b 1
    )

    echo Continuing anyway... Hopefully it still works!
)

echo.
REM ===================================================================================================

REM ======================================== Check for .NET 10+ ========================================
echo ^> Checking for .NET 10+...
dotnet --list-sdks | findstr /r "^10\." >nul

if errorlevel 1 (
    echo.
    echo ============================================================
    echo ERROR: .NET 10 SDK or higher is required!
    echo Please install .NET 10 SDK before continuing.
    echo Download it from:
    echo https://dotnet.microsoft.com/download/dotnet/10.0
    echo ============================================================
    echo.
    pause
    exit /b 1
)

echo You have it! Aren't you such a good human?
echo.
REM ===================================================================================================

REM ========================================= Build RecompOne =========================================
echo.
echo ^> Building RecompOne...

dotnet build "%RECOMPONE%\RecompOne.sln"

if errorlevel 1 (
    echo.
    echo Failed to build RecompOne! That's a huge problem. Can't continue, sorry!
    pause
    exit /b 1
)

echo Build of RecompOne's Recompiler and Runtime complete!
echo.
REM ===================================================================================================

REM ======================================== Check Disc Files =========================================
echo.
echo ^> Checking disc files...


if not exist "%DISC%\Castlevania - Symphony of the Night (Track 1).bin" (
    echo Missing: Castlevania - Symphony of the Night ^(Track 1^).bin
    set "MISSING=1"
)

if not exist "%DISC%\Castlevania - Symphony of the Night (Track 2).bin" (
    echo Missing: Castlevania - Symphony of the Night ^(Track 2^).bin
    set "MISSING=1"
)

if not exist "%DISC%\Castlevania - Symphony of the Night (USA).cue" (
    echo Missing: Castlevania - Symphony of the Night ^(USA^).cue
    set "MISSING=1"
)

if "%MISSING%"=="1" (
    echo.
    echo ============================================================
    echo ERROR: Required disc files are missing or incorrectly named!
    echo Please place the following files in:
    echo %CD%\disc
    echo.
    echo Files must be named exactly:
    echo Castlevania - Symphony of the Night ^(Track 1^).bin
    echo Castlevania - Symphony of the Night ^(Track 2^).bin
    echo Castlevania - Symphony of the Night ^(USA^).cue
    echo ============================================================
    echo.
    pause
    exit /b 1
)

echo Disc files found and properly named!
echo.
REM ===================================================================================================

REM ========================================== Check Config ===========================================
echo.
echo ^> Checking configuration file...

if not exist "%SOTNJSON%" (
    echo.
    echo ============================================================
    echo ERROR: Missing configuration file!
    echo Expected:
    echo %CD%\config\sotn.json
    echo ============================================================
    echo.
    pause
    exit /b 1
)

echo Config file found!
echo.

REM ======================================== Generate C# Code =========================================
echo.
echo ^> Generating Recompiled C# Code

dotnet run --project %RECOMPILER% %SOTNJSON%

echo.
REM ===================================================================================================


REM ========================================= Build SymphonyRecomp ====================================
echo.
echo ^> Building SymphonyRecomp...

dotnet build "%RECOMPONE%\RecompOne.sln"

if errorlevel 1 (
    echo.
    echo Failed to build SymphonyRecomp! That's a very huge problem. Can't continue, sorry!
    pause
    exit /b 1
)

echo Build of SymphonyRecomp complete!
echo.
REM ===================================================================================================

REM ======================================== How To Run Game ==========================================
echo.
echo ^> Running the Game Instructions!

echo Contratulations, you've successfully built SymphonyRecomp! Use windows_run.bat to play the game!

echo.
REM ===================================================================================================

echo.
pause
