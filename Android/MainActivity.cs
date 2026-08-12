using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Silk.NET.Windowing.Sdl.Android;
using System;
using System.IO;
using System.Linq;
using RecompOne.Runtime.Config;
using RecompOne.Runtime.Hardware;
using RecompOne.Runtime.Host;
using RecompOne.Runtime.Memory;
using RecompOne.Runtime.Modding;
using Sotn;

namespace RecompOne.SoTN.Android
{
    [Activity(Label = "SymphonyRecomp", Icon = "@mipmap/icon", MainLauncher = true, 
              Theme = "@android:style/Theme.NoTitleBar.Fullscreen", 
              ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.SmallestScreenSize | ConfigChanges.ScreenLayout | ConfigChanges.KeyboardHidden,
              ScreenOrientation = ScreenOrientation.Sensor)]
    public class MainActivity : SilkActivity
    {
        public enum ScreenOrientationMode
        {
            AutoRotate = 0,
            LockLandscape = 1,
            LockPortrait = 2
        }

        public static ScreenOrientationMode CurrentOrientationMode = ScreenOrientationMode.AutoRotate;

        private float _touchOpacity = 0.7f;
        private bool _touchVisible = true;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            Console.WriteLine("[Android] MainActivity OnCreate started.");
            
            var filesPath = FilesDir?.Path ?? "";
            Directory.SetCurrentDirectory(filesPath);
            Console.WriteLine($"[Android] Set current directory to: {filesPath}");

            CopyAssets("assets");
            CopyAssets("config");
            CopyAssets("disc");

            AutoDetectDisc();
        }

        protected override void OnPostCreate(Bundle? savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            SetupTouchControls();
        }

        protected override void OnRun()
        {
            Console.WriteLine("[Android] MainActivity OnRun executing game Entry.");
            try
            {
                AutoDetectDisc();
                var cdPath = ConfigManager.Game.CdPath;
                Console.WriteLine($"[Android] Launching Entry.Run with CdPath = '{cdPath}'");

                // Run the game!
                var m = new PSMemory();
                Recompiled.Entry.Run(m, cdPath, "SymphonyRecomp");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Android] Game execution crashed: {ex.ToString()}");
            }
        }

        // --- RETROID POCKET, BLUETOOTH & WIRED CONTROLLER INPUT HANDLING ---

        public override bool OnKeyDown(Keycode keyCode, KeyEvent? e)
        {
            if (HandleGamepadButton(keyCode, isDown: true)) return true;
            if (keyCode == Keycode.Back || keyCode == Keycode.Menu)
            {
                ShowMenuDialog();
                return true;
            }
            return base.OnKeyDown(keyCode, e);
        }

        public override bool OnKeyUp(Keycode keyCode, KeyEvent? e)
        {
            if (HandleGamepadButton(keyCode, isDown: false)) return true;
            return base.OnKeyUp(keyCode, e);
        }

        public override bool OnGenericMotionEvent(MotionEvent? e)
        {
            if (e != null && HandleGamepadAxis(e)) return true;
            return base.OnGenericMotionEvent(e);
        }

        private bool HandleGamepadButton(Keycode keyCode, bool isDown)
        {
            ushort mask = keyCode switch
            {
                Keycode.ButtonA => Controller.Cross,
                Keycode.ButtonB => Controller.Circle,
                Keycode.ButtonX => Controller.Square,
                Keycode.ButtonY => Controller.Triangle,
                Keycode.ButtonL1 => Controller.L1,
                Keycode.ButtonR1 => Controller.R1,
                Keycode.ButtonL2 => Controller.L2,
                Keycode.ButtonR2 => Controller.R2,
                Keycode.ButtonStart => Controller.Start,
                Keycode.ButtonSelect => Controller.Select,
                Keycode.DpadUp => Controller.Up,
                Keycode.DpadDown => Controller.Down,
                Keycode.DpadLeft => Controller.Left,
                Keycode.DpadRight => Controller.Right,
                _ => 0
            };

            if (mask != 0)
            {
                SetControllerBit(mask, isDown);
                return true;
            }
            return false;
        }

        private bool HandleGamepadAxis(MotionEvent e)
        {
            float hx = e.GetAxisValue(Axis.HatX);
            float hy = e.GetAxisValue(Axis.HatY);
            float lx = e.GetAxisValue(Axis.X);
            float ly = e.GetAxisValue(Axis.Y);
            float lt = e.GetAxisValue(Axis.Ltrigger);
            float rt = e.GetAxisValue(Axis.Rtrigger);

            bool left = hx < -0.5f || lx < -0.5f;
            bool right = hx > 0.5f || lx > 0.5f;
            bool up = hy < -0.5f || ly < -0.5f;
            bool down = hy > 0.5f || ly > 0.5f;
            bool l2 = lt > 0.5f;
            bool r2 = rt > 0.5f;

            SetControllerBit(Controller.Left, left);
            SetControllerBit(Controller.Right, right);
            SetControllerBit(Controller.Up, up);
            SetControllerBit(Controller.Down, down);
            SetControllerBit(Controller.L2, l2);
            SetControllerBit(Controller.R2, r2);

            return left || right || up || down || l2 || r2;
        }

        private static void SetControllerBit(ushort mask, bool pressed)
        {
            if (pressed) Controller.State &= unchecked((ushort)~mask);
            else Controller.State |= mask;
        }

        // --- TOUCH OVERLAY CREATION (PSX LAYOUT) ---

        protected override void OnPause()
        {
            base.OnPause();
            Console.WriteLine("[Android] MainActivity OnPause - pausing audio and resetting controls.");
            Audio.Pause();
            Controller.State = 0xFFFF; // Clear all active inputs on pause
        }

        protected override void OnResume()
        {
            base.OnResume();
            Console.WriteLine("[Android] MainActivity OnResume - resuming audio.");
            Audio.Resume();
        }

        private TouchOverlayView? _touchView;

        private void SetupTouchControls()
        {
            RunOnUiThread(() =>
            {
                try
                {
                    var decorView = Window?.DecorView as ViewGroup;
                    if (decorView == null) return;

                    if (_touchView != null && _touchView.Parent is ViewGroup p)
                    {
                        p.RemoveView(_touchView);
                    }

                    _touchView = new TouchOverlayView(this)
                    {
                        TouchOpacity = _touchOpacity,
                        TouchVisible = _touchVisible,
                        ControlMode = (TouchControlMode)ConfigManager.View.TouchControlMode,
                        OnMenuClicked = ShowMenuDialog
                    };

                    var paramsMatch = new ViewGroup.LayoutParams(
                        ViewGroup.LayoutParams.MatchParent,
                        ViewGroup.LayoutParams.MatchParent);

                    decorView.AddView(_touchView, paramsMatch);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[Android] Touch controls creation failed: {ex.Message}");
                }
            });
        }

        public override void OnConfigurationChanged(global::Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            SetupTouchControls();
        }

        // --- MENU & CHEATS DIALOG ---

        private void ShowMenuDialog()
        {
            RunOnUiThread(() =>
            {
                try
                {
                    var options = new string[]
                    {
                        "⚡ Cheats (Full Heal, God Mode, Gold)",
                        "🧩 Mods Manager",
                        "🎨 Display Settings (Aspect, Resolution)",
                        "🎮 Touch Controls (Opacity, Visibility)",
                        "🔄 Reset / Reload Disc"
                    };

                    new AlertDialog.Builder(this)
                        .SetTitle("SymphonyRecomp Menu")
                        .SetItems(options, (s, e) =>
                        {
                            switch (e.Which)
                            {
                                case 0: ShowCheatsMenu(); break;
                                case 1: ShowModsMenu(); break;
                                case 2: ShowDisplayMenu(); break;
                                case 3: ShowTouchControlsMenu(); break;
                                case 4: AutoDetectDisc(); Toast.MakeText(this, "Disc reloaded", ToastLength.Short)?.Show(); break;
                            }
                        })
                        .SetNegativeButton("Close", (IDialogInterfaceOnClickListener?)null)
                        .Show();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[Android] Menu dialog failed: {ex.Message}");
                }
            });
        }

        private void ShowModsMenu()
        {
            try
            {
                string modsDir = System.IO.Path.Combine(FilesDir?.Path ?? "/sdcard/Android/data/com.blacklabelhq.sotn/files", "mods");
                if (!Directory.Exists(modsDir)) Directory.CreateDirectory(modsDir);

                var mods = ModLoader.Mods;
                if (mods.Count == 0)
                {
                    try { ModLoader.LoadAll(modsDir); mods = ModLoader.Mods; } catch { }
                }

                if (mods.Count == 0)
                {
                    new AlertDialog.Builder(this)
                        .SetTitle("Mods Manager")
                        .SetMessage($"No mods found.\n\nMods Folder:\n{modsDir}\n\nPlace mod folders or precompiled mod DLLs in this directory.")
                        .SetPositiveButton("Refresh", (s, e) => { try { ModLoader.LoadAll(modsDir); } catch { } ShowModsMenu(); })
                        .SetNegativeButton("Back", (s, e) => ShowMenuDialog())
                        .Show();
                    return;
                }

                var modItems = new string[mods.Count + 1];
                for (int i = 0; i < mods.Count; i++)
                {
                    var m = mods[i];
                    string stateStr = m.Enabled ? "🟢 [ON]" : "⚪ [OFF]";
                    string name = string.IsNullOrWhiteSpace(m.Info.Name) ? m.Info.Id : m.Info.Name;
                    modItems[i] = $"{stateStr} {name} (v{m.Info.Version})";
                }
                modItems[mods.Count] = "🔄 Reload All Mods";

                new AlertDialog.Builder(this)
                    .SetTitle($"Mods ({mods.Count} Available)")
                    .SetItems(modItems, (s, e) =>
                    {
                        if (e.Which == mods.Count)
                        {
                            try { ModLoader.LoadAll(modsDir); } catch { }
                            Toast.MakeText(this, "Reloaded mods folder", ToastLength.Short)?.Show();
                            ShowModsMenu();
                        }
                        else if (e.Which >= 0 && e.Which < mods.Count)
                        {
                            var selectedMod = mods[e.Which];
                            bool newState = !selectedMod.Enabled;
                            ModLoader.SetEnabled(selectedMod.Info.Id, newState);
                            Toast.MakeText(this, $"{(newState ? "Enabled" : "Disabled")}: {selectedMod.Info.Name}", ToastLength.Short)?.Show();
                            ShowModsMenu();
                        }
                    })
                    .SetNegativeButton("Back", (s, e) => ShowMenuDialog())
                    .Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, $"Mods Manager error: {ex.Message}", ToastLength.Long)?.Show();
            }
        }

        private void ShowCheatsMenu()
        {
            var cheats = new string[]
            {
                "💖 Full Heal (Max HP, MP, Hearts)",
                "🔥 God Mode (Max Stats, 9999 HP, $999k Gold)",
                "⭐ Max Level 99",
                "💰 Add $999,999 Gold"
            };

            new AlertDialog.Builder(this)
                .SetTitle("Cheats")
                .SetItems(cheats, (s, e) =>
                {
                    try
                    {
                        switch (e.Which)
                        {
                            case 0:
                                Player.FullHeal();
                                Toast.MakeText(this, "Full Heal Applied!", ToastLength.Short)?.Show();
                                break;
                            case 1:
                                Player.HpMax = Player.Hp = 9999;
                                Player.MpMax = Player.Mp = 9999;
                                Player.HeartsMax = Player.Hearts = 999;
                                Player.Strength = 999;
                                Player.Constitution = 999;
                                Player.Intelligence = 999;
                                Player.Luck = 999;
                                Player.Gold = 999999;
                                Toast.MakeText(this, "God Mode Enabled!", ToastLength.Short)?.Show();
                                break;
                            case 2:
                                Player.Level = 99;
                                Toast.MakeText(this, "Set to Level 99!", ToastLength.Short)?.Show();
                                break;
                            case 3:
                                Player.Gold = 999999;
                                Toast.MakeText(this, "Max Gold Added!", ToastLength.Short)?.Show();
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Toast.MakeText(this, $"Cheat unavailable: {ex.Message}", ToastLength.Long)?.Show();
                    }
                })
                .SetNegativeButton("Back", (s, e) => ShowMenuDialog())
                .Show();
        }

        private void ShowDisplayMenu()
        {
            string aspectStr = HostWindow.CurrentAspectRatio switch
            {
                HostWindow.AspectRatioMode.AutoDevice => "📱 Auto-Fit (Portrait & Landscape)",
                HostWindow.AspectRatioMode.Widescreen_16_9 => "16:9 Widescreen",
                HostWindow.AspectRatioMode.Stretch => "Stretch to Screen",
                _ => "4:3 Original"
            };

            string orientStr = CurrentOrientationMode switch
            {
                ScreenOrientationMode.LockLandscape => "↔️ Lock Landscape",
                ScreenOrientationMode.LockPortrait => "↕️ Lock Portrait",
                _ => "🔄 Auto-Rotate (Sensor)"
            };

            var options = new string[]
            {
                $"Orientation: {orientStr}",
                $"Aspect Ratio: {aspectStr}",
                $"Resolution: {(ConfigManager.View.NativeResolution ? "Native PSX (1x)" : "High Resolution (4x)")}",
                $"VSync: {(ConfigManager.View.VSync ? "Enabled" : "Disabled")}"
            };

            new AlertDialog.Builder(this)
                .SetTitle("Display Settings")
                .SetItems(options, (s, e) =>
                {
                    if (e.Which == 0)
                    {
                        ShowOrientationSubmenu();
                    }
                    else if (e.Which == 1)
                    {
                        ShowAspectRatioSubmenu();
                    }
                    else if (e.Which == 2)
                    {
                        ConfigManager.View.NativeResolution = !ConfigManager.View.NativeResolution;
                        ConfigManager.SaveView(null);
                        HostWindow.RequestGpuReset();
                        Toast.MakeText(this, $"Resolution toggled: {(ConfigManager.View.NativeResolution ? "Native (1x)" : "High Res (4x)")}", ToastLength.Short)?.Show();
                    }
                    else if (e.Which == 3)
                    {
                        ConfigManager.View.VSync = !ConfigManager.View.VSync;
                        HostWindow.SetVSync(ConfigManager.View.VSync);
                        ConfigManager.SaveView(null);
                        Toast.MakeText(this, $"VSync: {(ConfigManager.View.VSync ? "ON" : "OFF")}", ToastLength.Short)?.Show();
                    }
                })
                .SetNegativeButton("Back", (s, e) => ShowMenuDialog())
                .Show();
        }

        private void ShowOrientationSubmenu()
        {
            var modes = new string[]
            {
                "🔄 Auto-Rotate / Sensor (Follow device rotation)",
                "↔️ Lock Landscape (Horizontal)",
                "↕️ Lock Portrait (Vertical)"
            };

            new AlertDialog.Builder(this)
                .SetTitle("Screen Orientation")
                .SetItems(modes, (s, e) =>
                {
                    CurrentOrientationMode = e.Which switch
                    {
                        1 => ScreenOrientationMode.LockLandscape,
                        2 => ScreenOrientationMode.LockPortrait,
                        _ => ScreenOrientationMode.AutoRotate
                    };

                    RequestedOrientation = CurrentOrientationMode switch
                    {
                        ScreenOrientationMode.LockLandscape => ScreenOrientation.SensorLandscape,
                        ScreenOrientationMode.LockPortrait => ScreenOrientation.SensorPortrait,
                        _ => ScreenOrientation.Sensor
                    };

                    SetupTouchControls();
                    Toast.MakeText(this, $"Orientation set to: {modes[e.Which]}", ToastLength.Short)?.Show();
                })
                .SetNegativeButton("Back", (s, e) => ShowDisplayMenu())
                .Show();
        }

        private void ShowAspectRatioSubmenu()
        {
            var ratios = new string[]
            {
                "📱 Auto-Fit Device (Dynamic Portrait & Landscape)",
                "🖥️ 4:3 Original (PSX Centered)",
                "📺 16:9 Widescreen",
                "↔️ Stretch to Fill Screen"
            };

            new AlertDialog.Builder(this)
                .SetTitle("Aspect Ratio")
                .SetItems(ratios, (s, e) =>
                {
                    HostWindow.CurrentAspectRatio = e.Which switch
                    {
                        0 => HostWindow.AspectRatioMode.AutoDevice,
                        1 => HostWindow.AspectRatioMode.Original_4_3,
                        2 => HostWindow.AspectRatioMode.Widescreen_16_9,
                        3 => HostWindow.AspectRatioMode.Stretch,
                        _ => HostWindow.AspectRatioMode.AutoDevice
                    };
                    Toast.MakeText(this, $"Aspect ratio set to: {ratios[e.Which]}", ToastLength.Short)?.Show();
                })
                .SetNegativeButton("Back", (s, e) => ShowDisplayMenu())
                .Show();
        }

        private void ShowTouchControlsMenu()
        {
            int mode = ConfigManager.View.TouchControlMode;
            string modeStr = mode == 0 ? "🔲 Four Arrows (D-Pad)" : "🕹️ Virtual Analog Joystick";

            var options = new string[]
            {
                $"Movement Style: {modeStr}",
                $"Touch Overlay: {(_touchVisible ? "Visible" : "Hidden")}",
                "Opacity: 100%",
                "Opacity: 70%",
                "Opacity: 40%"
            };

            new AlertDialog.Builder(this)
                .SetTitle("Touch Control Settings")
                .SetItems(options, (s, e) =>
                {
                    if (e.Which == 0)
                    {
                        int nextMode = mode == 0 ? 1 : 0;
                        ConfigManager.View.TouchControlMode = nextMode;
                        try { ConfigManager.SaveView(); } catch { }
                        if (_touchView != null)
                        {
                            _touchView.ControlMode = (TouchControlMode)nextMode;
                            _touchView.Invalidate();
                        }
                        Toast.MakeText(this, $"Movement style: {(nextMode == 0 ? "Four Arrows (D-Pad)" : "Virtual Joystick")}", ToastLength.Short)?.Show();
                    }
                    else if (e.Which == 1)
                    {
                        _touchVisible = !_touchVisible;
                        if (_touchView != null) { _touchView.TouchVisible = _touchVisible; _touchView.Invalidate(); }
                    }
                    else if (e.Which == 2) { _touchOpacity = 1.0f; if (_touchView != null) { _touchView.TouchOpacity = _touchOpacity; _touchView.Invalidate(); } }
                    else if (e.Which == 3) { _touchOpacity = 0.7f; if (_touchView != null) { _touchView.TouchOpacity = _touchOpacity; _touchView.Invalidate(); } }
                    else if (e.Which == 4) { _touchOpacity = 0.4f; if (_touchView != null) { _touchView.TouchOpacity = _touchOpacity; _touchView.Invalidate(); } }
                })
                .SetNegativeButton("Back", (s, e) => ShowMenuDialog())
                .Show();
        }

        private void AutoDetectDisc()
        {
            try
            {
                ConfigManager.Load();
                var searchDirs = new string[]
                {
                    "/sdcard/Android/data/com.blacklabelhq.sotn/files/disc",
                    global::Android.OS.Environment.ExternalStorageDirectory?.AbsolutePath != null
                        ? System.IO.Path.Combine(global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Android", "data", PackageName ?? "com.blacklabelhq.sotn", "files", "disc")
                        : "",
                    System.IO.Path.Combine(FilesDir?.Path ?? "", "disc"),
                    "/sdcard/SymphonyRecomp/disc",
                    "/sdcard/disc"
                };

                foreach (var discDir in searchDirs)
                {
                    if (!string.IsNullOrWhiteSpace(discDir) && Directory.Exists(discDir))
                    {
                        var cueFiles = Directory.GetFiles(discDir, "*.cue");
                        if (cueFiles.Length > 0)
                        {
                            var validCue = cueFiles.FirstOrDefault(f => File.Exists(f) && new FileInfo(f).Length > 0);
                            if (validCue != null)
                            {
                                ConfigManager.Game.CdPath = validCue;
                                ConfigManager.SaveGame();
                                Console.WriteLine($"[Android] Auto-configured CdPath to valid cue: {validCue}");
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Android] AutoDetectDisc error: {ex.Message}");
            }
        }

        private void CopyAssets(string path)
        {
            try
            {
                var filesPath = FilesDir?.Path ?? "";
                var normalizedPath = path.Replace('\\', '/');
                var list = Assets?.List(normalizedPath);

                if (list != null && list.Length > 0)
                {
                    var localDir = System.IO.Path.Combine(filesPath, normalizedPath);
                    Directory.CreateDirectory(localDir);

                    foreach (var item in list)
                    {
                        var childPath = string.IsNullOrEmpty(normalizedPath) ? item : $"{normalizedPath}/{item}";
                        CopyAssets(childPath);
                    }
                }
                else
                {
                    var localFile = System.IO.Path.Combine(filesPath, normalizedPath);
                    if (normalizedPath.StartsWith("disc") && File.Exists(localFile) && new FileInfo(localFile).Length > 0)
                        return;

                    try
                    {
                        using var stream = Assets?.Open(normalizedPath);
                        if (stream != null)
                        {
                            var parentDir = System.IO.Path.GetDirectoryName(localFile);
                            if (!string.IsNullOrEmpty(parentDir))
                                Directory.CreateDirectory(parentDir);

                            using var dest = File.Create(localFile);
                            stream.CopyTo(dest);
                        }
                    }
                    catch (Java.IO.FileNotFoundException) { }
                    catch (FileNotFoundException) { }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Android] Asset copy error for '{path}': {ex.Message}");
            }
        }
    }
}
