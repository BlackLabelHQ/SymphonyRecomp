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

        private FrameLayout? _overlayLayout;
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

                // Run the game!
                var m = new PSMemory();
                Recompiled.Entry.Run(m, null, "SymphonyRecomp");
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

        private void SetupTouchControls()
        {
            RunOnUiThread(() =>
            {
                try
                {
                    var decorView = Window?.DecorView as ViewGroup;
                    if (decorView == null) return;

                    if (_overlayLayout != null && _overlayLayout.Parent is ViewGroup p)
                    {
                        p.RemoveView(_overlayLayout);
                    }

                    _overlayLayout = new FrameLayout(this);
                    _overlayLayout.SetBackgroundColor(Color.Transparent);

                    var density = Resources?.DisplayMetrics?.Density ?? 1f;

                    global::Android.Widget.Button CreateBtn(string text, ushort mask, int wDp, int hDp, float bgAlpha = 0.5f)
                    {
                        var btn = new global::Android.Widget.Button(this);
                        btn.Text = text;
                        btn.SetTextColor(Color.White);
                        btn.TextSize = 14f;
                        btn.SetPadding(0, 0, 0, 0);

                        var shape = new global::Android.Graphics.Drawables.GradientDrawable();
                        shape.SetShape(global::Android.Graphics.Drawables.ShapeType.Rectangle);
                        shape.SetCornerRadius(25f * density);
                        shape.SetColor(Color.Argb((int)(bgAlpha * 255 * _touchOpacity), 40, 40, 40));
                        shape.SetStroke((int)(1.5f * density), Color.Argb(180, 200, 200, 200));
                        btn.Background = shape;

                        btn.Touch += (s, e) =>
                        {
                            if (e.Event?.Action == MotionEventActions.Down || e.Event?.Action == MotionEventActions.PointerDown)
                                SetControllerBit(mask, true);
                            else if (e.Event?.Action == MotionEventActions.Up || e.Event?.Action == MotionEventActions.PointerUp || e.Event?.Action == MotionEventActions.Cancel)
                                SetControllerBit(mask, false);
                        };
                        return btn;
                    }

                    bool isPortrait = Resources?.Configuration?.Orientation == global::Android.Content.Res.Orientation.Portrait;

                    int btnDp = isPortrait ? 52 : 58;
                    int btnPx = (int)(btnDp * density);
                    int padPx = (int)(15 * density);

                    // 1. D-PAD (LEFT SIDE)
                    var dpadLayout = new RelativeLayout(this);
                    var dpadParams = new FrameLayout.LayoutParams((int)(btnPx * 3.1f), (int)(btnPx * 3.1f))
                    {
                        Gravity = GravityFlags.Bottom | GravityFlags.Left,
                        LeftMargin = padPx,
                        BottomMargin = isPortrait ? (int)(25 * density) : padPx
                    };

                    var bUp = CreateBtn("▲", Controller.Up, btnDp, btnDp);
                    var bDown = CreateBtn("▼", Controller.Down, btnDp, btnDp);
                    var bLeft = CreateBtn("◄", Controller.Left, btnDp, btnDp);
                    var bRight = CreateBtn("►", Controller.Right, btnDp, btnDp);

                    var pUp = new RelativeLayout.LayoutParams(btnPx, btnPx); pUp.AddRule(LayoutRules.AlignParentTop); pUp.AddRule(LayoutRules.CenterHorizontal);
                    var pDown = new RelativeLayout.LayoutParams(btnPx, btnPx); pDown.AddRule(LayoutRules.AlignParentBottom); pDown.AddRule(LayoutRules.CenterHorizontal);
                    var pLeft = new RelativeLayout.LayoutParams(btnPx, btnPx); pLeft.AddRule(LayoutRules.AlignParentLeft); pLeft.AddRule(LayoutRules.CenterVertical);
                    var pRight = new RelativeLayout.LayoutParams(btnPx, btnPx); pRight.AddRule(LayoutRules.AlignParentRight); pRight.AddRule(LayoutRules.CenterVertical);

                    dpadLayout.AddView(bUp, pUp);
                    dpadLayout.AddView(bDown, pDown);
                    dpadLayout.AddView(bLeft, pLeft);
                    dpadLayout.AddView(bRight, pRight);
                    _overlayLayout.AddView(dpadLayout, dpadParams);

                    // 2. PSX ACTION BUTTONS (RIGHT SIDE)
                    var actionLayout = new RelativeLayout(this);
                    var actionParams = new FrameLayout.LayoutParams((int)(btnPx * 3.1f), (int)(btnPx * 3.1f))
                    {
                        Gravity = GravityFlags.Bottom | GravityFlags.Right,
                        RightMargin = padPx,
                        BottomMargin = isPortrait ? (int)(25 * density) : padPx
                    };

                    var bTriangle = CreateBtn("Δ", Controller.Triangle, btnDp, btnDp);
                    var bSquare = CreateBtn("□", Controller.Square, btnDp, btnDp);
                    var bCircle = CreateBtn("O", Controller.Circle, btnDp, btnDp);
                    var bCross = CreateBtn("X", Controller.Cross, btnDp, btnDp);

                    bTriangle.SetTextColor(Color.Rgb(60, 220, 100)); // Green
                    bSquare.SetTextColor(Color.Rgb(240, 100, 180));   // Pink
                    bCircle.SetTextColor(Color.Rgb(240, 60, 60));     // Red
                    bCross.SetTextColor(Color.Rgb(80, 140, 240));     // Blue

                    actionLayout.AddView(bTriangle, pUp);
                    actionLayout.AddView(bCross, pDown);
                    actionLayout.AddView(bSquare, pLeft);
                    actionLayout.AddView(bCircle, pRight);
                    _overlayLayout.AddView(actionLayout, actionParams);

                    // 3. SHOULDER BUTTONS
                    int shW = (int)((isPortrait ? 58 : 65) * density);
                    int shH = (int)((isPortrait ? 34 : 38) * density);

                    var bL1 = CreateBtn("L1", Controller.L1, 65, 38);
                    var bL2 = CreateBtn("L2", Controller.L2, 65, 38);
                    var bR1 = CreateBtn("R1", Controller.R1, 65, 38);
                    var bR2 = CreateBtn("R2", Controller.R2, 65, 38);

                    FrameLayout.LayoutParams pL1, pL2, pR1, pR2;
                    if (isPortrait)
                    {
                        int shBottom = (int)(btnPx * 3.35f + 30 * density);
                        pL1 = new FrameLayout.LayoutParams(shW, shH) { Gravity = GravityFlags.Bottom | GravityFlags.Left, LeftMargin = padPx, BottomMargin = shBottom };
                        pL2 = new FrameLayout.LayoutParams(shW, shH) { Gravity = GravityFlags.Bottom | GravityFlags.Left, LeftMargin = padPx + shW + (int)(6 * density), BottomMargin = shBottom };
                        pR1 = new FrameLayout.LayoutParams(shW, shH) { Gravity = GravityFlags.Bottom | GravityFlags.Right, RightMargin = padPx + shW + (int)(6 * density), BottomMargin = shBottom };
                        pR2 = new FrameLayout.LayoutParams(shW, shH) { Gravity = GravityFlags.Bottom | GravityFlags.Right, RightMargin = padPx, BottomMargin = shBottom };
                    }
                    else
                    {
                        pL1 = new FrameLayout.LayoutParams(shW, shH) { Gravity = GravityFlags.Top | GravityFlags.Left, LeftMargin = padPx, TopMargin = (int)(10 * density) };
                        pL2 = new FrameLayout.LayoutParams(shW, shH) { Gravity = GravityFlags.Top | GravityFlags.Left, LeftMargin = padPx + shW + (int)(8 * density), TopMargin = (int)(10 * density) };
                        pR1 = new FrameLayout.LayoutParams(shW, shH) { Gravity = GravityFlags.Top | GravityFlags.Right, RightMargin = padPx, TopMargin = (int)(10 * density) };
                        pR2 = new FrameLayout.LayoutParams(shW, shH) { Gravity = GravityFlags.Top | GravityFlags.Right, RightMargin = padPx + shW + (int)(8 * density), TopMargin = (int)(10 * density) };
                    }

                    _overlayLayout.AddView(bL1, pL1);
                    _overlayLayout.AddView(bL2, pL2);
                    _overlayLayout.AddView(bR1, pR1);
                    _overlayLayout.AddView(bR2, pR2);

                    // 4. SYSTEM CONTROL BAR (SELECT, MENU, START)
                    var sysBar = new LinearLayout(this) { Orientation = Orientation.Horizontal };
                    var sysBarParams = new FrameLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent)
                    {
                        Gravity = GravityFlags.Bottom | GravityFlags.CenterHorizontal,
                        BottomMargin = (int)(10 * density)
                    };

                    var bSel = CreateBtn("SELECT", Controller.Select, 68, 34);
                    var bStart = CreateBtn("START", Controller.Start, 68, 34);

                    var bMenu = new global::Android.Widget.Button(this);
                    bMenu.Text = "⚙ MENU";
                    bMenu.SetTextColor(Color.Yellow);
                    bMenu.TextSize = 12f;
                    bMenu.SetPadding(0, 0, 0, 0);
                    var mShape = new global::Android.Graphics.Drawables.GradientDrawable();
                    mShape.SetShape(global::Android.Graphics.Drawables.ShapeType.Rectangle);
                    mShape.SetCornerRadius(18f * density);
                    mShape.SetColor(Color.Argb(180, 20, 20, 20));
                    mShape.SetStroke((int)(1.5f * density), Color.Yellow);
                    bMenu.Background = mShape;
                    bMenu.Click += (s, e) => ShowMenuDialog();

                    int btnW = (int)(68 * density);
                    int btnH = (int)(34 * density);
                    int barPad = (int)(8 * density);

                    var lpSel = new LinearLayout.LayoutParams(btnW, btnH) { RightMargin = barPad };
                    var lpMenu = new LinearLayout.LayoutParams((int)(82 * density), btnH) { RightMargin = barPad };
                    var lpStart = new LinearLayout.LayoutParams(btnW, btnH);

                    sysBar.AddView(bSel, lpSel);
                    sysBar.AddView(bMenu, lpMenu);
                    sysBar.AddView(bStart, lpStart);

                    _overlayLayout.AddView(sysBar, sysBarParams);

                    _overlayLayout.Visibility = _touchVisible ? ViewStates.Visible : ViewStates.Gone;
                    decorView.AddView(_overlayLayout);
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
                                case 1: ShowDisplayMenu(); break;
                                case 2: ShowTouchControlsMenu(); break;
                                case 3: AutoDetectDisc(); Toast.MakeText(this, "Disc reloaded", ToastLength.Short)?.Show(); break;
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
            var options = new string[]
            {
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
                        _touchVisible = !_touchVisible;
                        if (_overlayLayout != null) _overlayLayout.Visibility = _touchVisible ? ViewStates.Visible : ViewStates.Gone;
                    }
                    else if (e.Which == 1) { _touchOpacity = 1.0f; SetupTouchControls(); }
                    else if (e.Which == 2) { _touchOpacity = 0.7f; SetupTouchControls(); }
                    else if (e.Which == 3) { _touchOpacity = 0.4f; SetupTouchControls(); }
                })
                .SetNegativeButton("Back", (s, e) => ShowMenuDialog())
                .Show();
        }

        private void AutoDetectDisc()
        {
            try
            {
                ConfigManager.Load();
                var currentPath = ConfigManager.Game.CdPath;
                if (string.IsNullOrWhiteSpace(currentPath) || !File.Exists(currentPath))
                {
                    var discDir = System.IO.Path.Combine(FilesDir?.Path ?? "", "disc");
                    if (Directory.Exists(discDir))
                    {
                        var cueFiles = Directory.GetFiles(discDir, "*.cue");
                        if (cueFiles.Length > 0)
                        {
                            ConfigManager.Game.CdPath = cueFiles[0];
                            ConfigManager.SaveGame();
                            Console.WriteLine($"[Android] Auto-configured CdPath to: {cueFiles[0]}");
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
                    catch (Java.IO.FileNotFoundException) { Directory.CreateDirectory(localFile); }
                    catch (FileNotFoundException) { Directory.CreateDirectory(localFile); }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[Android] Asset copy error for '{path}': {ex.Message}");
            }
        }
    }
}
