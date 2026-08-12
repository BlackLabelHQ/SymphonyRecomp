using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using RecompOne.Runtime.Hardware;

namespace RecompOne.SoTN.Android
{
    public class TouchOverlayView : View
    {
        public Action? OnMenuClicked;
        public float TouchOpacity = 0.7f;
        public bool TouchVisible = true;

        private readonly Paint _fillPaint = new Paint(PaintFlags.AntiAlias);
        private readonly Paint _strokePaint = new Paint(PaintFlags.AntiAlias);
        private readonly Paint _textPaint = new Paint(PaintFlags.AntiAlias);

        // Active pressed states for visual feedback
        private bool _pUp, _pDown, _pLeft, _pRight;
        private bool _pTriangle, _pSquare, _pCircle, _pCross;
        private bool _pL1, _pL2, _pR1, _pR2;
        private bool _pSelect, _pMenu, _pStart;

        public TouchOverlayView(Context context) : base(context)
        {
            SetBackgroundColor(Color.Transparent);
        }

        public override bool OnTouchEvent(MotionEvent? e)
        {
            if (e == null || !TouchVisible) return base.OnTouchEvent(e);

            int action = (int)e.ActionMasked;
            int actionIndex = e.ActionIndex;

            bool menuTriggered = false;

            bool pUp = false, pDown = false, pLeft = false, pRight = false;
            bool pTriangle = false, pSquare = false, pCircle = false, pCross = false;
            bool pL1 = false, pL2 = false, pR1 = false, pR2 = false;
            bool pSelect = false, pMenu = false, pStart = false;

            float density = Context?.Resources?.DisplayMetrics?.Density ?? 1f;
            int w = Width;
            int h = Height;
            if (w <= 0 || h <= 0) return true;

            bool isPortrait = Context?.Resources?.Configuration?.Orientation == global::Android.Content.Res.Orientation.Portrait;

            // Geometry calculations
            float btnDp = isPortrait ? 52f : 58f;
            float btnPx = btnDp * density;
            float padPx = 15f * density;

            // D-Pad center
            float dpadW = btnPx * 3.1f;
            float dpadLeft = padPx;
            float dpadBottom = isPortrait ? (56f * density) : padPx;
            float dpadY = h - dpadBottom - dpadW;
            float dpadCenterX = dpadLeft + dpadW / 2f;
            float dpadCenterY = dpadY + dpadW / 2f;
            float dpadRadius = dpadW / 2f;

            // Action Buttons center
            float actionW = btnPx * 3.1f;
            float actionRight = padPx;
            float actionBottom = isPortrait ? (56f * density) : padPx;
            float actionX = w - actionRight - actionW;
            float actionY = h - actionBottom - actionW;
            float actionCenterX = actionX + actionW / 2f;
            float actionCenterY = actionY + actionW / 2f;
            float actionRadius = actionW / 2f;

            // Shoulders
            float shW = (isPortrait ? 58f : 65f) * density;
            float shH = (isPortrait ? 34f : 38f) * density;
            float shBottom = isPortrait ? (btnPx * 3.1f + 62f * density) : (h - (10f * density) - shH);

            RectF rectL1 = isPortrait 
                ? new RectF(padPx, h - shBottom - shH, padPx + shW, h - shBottom)
                : new RectF(padPx, 10f * density, padPx + shW, 10f * density + shH);

            RectF rectL2 = isPortrait
                ? new RectF(padPx + shW + 6f * density, h - shBottom - shH, padPx + shW * 2 + 6f * density, h - shBottom)
                : new RectF(padPx + shW + 8f * density, 10f * density, padPx + shW * 2 + 8f * density, 10f * density + shH);

            RectF rectR1 = isPortrait
                ? new RectF(w - padPx - shW * 2 - 6f * density, h - shBottom - shH, w - padPx - shW - 6f * density, h - shBottom)
                : new RectF(w - padPx - shW, 10f * density, w - padPx, 10f * density + shH);

            RectF rectR2 = isPortrait
                ? new RectF(w - padPx - shW, h - shBottom - shH, w - padPx, h - shBottom)
                : new RectF(w - padPx - shW * 2 - 8f * density, 10f * density, w - padPx - shW - 8f * density, 10f * density + shH);

            // System buttons (Select, Menu, Start)
            float sysW = 68f * density;
            float sysMenuW = 82f * density;
            float sysH = 34f * density;
            float sysBottom = 10f * density;
            float sysTotalW = sysW + sysMenuW + sysW + 16f * density;
            float sysStartX = (w - sysTotalW) / 2f;
            float sysY = h - sysBottom - sysH;

            RectF rectSelect = new RectF(sysStartX, sysY, sysStartX + sysW, sysY + sysH);
            RectF rectMenu = new RectF(sysStartX + sysW + 8f * density, sysY, sysStartX + sysW + 8f * density + sysMenuW, sysY + sysH);
            RectF rectStart = new RectF(sysStartX + sysW + sysMenuW + 16f * density, sysY, sysStartX + sysW + sysMenuW + 16f * density + sysW, sysY + sysH);

            for (int i = 0; i < e.PointerCount; i++)
            {
                if ((action == (int)MotionEventActions.PointerUp || action == (int)MotionEventActions.Up || action == (int)MotionEventActions.Cancel) && i == actionIndex)
                    continue;

                float px = e.GetX(i);
                float py = e.GetY(i);

                // 1. D-Pad hit test
                float dx = px - dpadCenterX;
                float dy = py - dpadCenterY;
                float dist = MathF.Sqrt(dx * dx + dy * dy);

                if (dist <= dpadRadius * 1.4f && dist > 6f * density)
                {
                    double angle = Math.Atan2(dy, dx) * (180.0 / Math.PI); // -180 to 180
                    // Angles: Right is 0, Down is 90, Left is 180/-180, Up is -90

                    if (angle >= -67.5 && angle <= 67.5) pRight = true;
                    if (angle >= 22.5 && angle <= 157.5) pDown = true;
                    if (angle >= 112.5 || angle <= -112.5) pLeft = true;
                    if (angle >= -157.5 && angle <= -22.5) pUp = true;
                }

                // 2. Action Buttons hit test
                float ax = px - actionCenterX;
                float ay = py - actionCenterY;
                float aDist = MathF.Sqrt(ax * ax + ay * ay);

                if (aDist <= actionRadius * 1.4f)
                {
                    float subR = btnPx * 0.6f;
                    // Triangle (Up)
                    if (MathF.Sqrt(ax * ax + (ay + btnPx * 0.9f) * (ay + btnPx * 0.9f)) <= subR || (ay < -btnPx * 0.3f && MathF.Abs(ax) < btnPx * 0.85f))
                        pTriangle = true;

                    // Cross (Down)
                    if (MathF.Sqrt(ax * ax + (ay - btnPx * 0.9f) * (ay - btnPx * 0.9f)) <= subR || (ay > btnPx * 0.3f && MathF.Abs(ax) < btnPx * 0.85f))
                        pCross = true;

                    // Square (Left)
                    if (MathF.Sqrt((ax + btnPx * 0.9f) * (ax + btnPx * 0.9f) + ay * ay) <= subR || (ax < -btnPx * 0.3f && MathF.Abs(ay) < btnPx * 0.85f))
                        pSquare = true;

                    // Circle (Right)
                    if (MathF.Sqrt((ax - btnPx * 0.9f) * (ax - btnPx * 0.9f) + ay * ay) <= subR || (ax > btnPx * 0.3f && MathF.Abs(ay) < btnPx * 0.85f))
                        pCircle = true;
                }

                // 3. Shoulders
                if (rectL1.Contains(px, py)) pL1 = true;
                if (rectL2.Contains(px, py)) pL2 = true;
                if (rectR1.Contains(px, py)) pR1 = true;
                if (rectR2.Contains(px, py)) pR2 = true;

                // 4. System buttons
                if (rectSelect.Contains(px, py)) pSelect = true;
                if (rectStart.Contains(px, py)) pStart = true;
                if (rectMenu.Contains(px, py))
                {
                    pMenu = true;
                    if (action == (int)MotionEventActions.Down || action == (int)MotionEventActions.PointerDown)
                        menuTriggered = true;
                }
            }

            // Update pressed states for rendering
            _pUp = pUp; _pDown = pDown; _pLeft = pLeft; _pRight = pRight;
            _pTriangle = pTriangle; _pSquare = pSquare; _pCircle = pCircle; _pCross = pCross;
            _pL1 = pL1; _pL2 = pL2; _pR1 = pR1; _pR2 = pR2;
            _pSelect = pSelect; _pMenu = pMenu; _pStart = pStart;

            // Apply controller bits atomically to Controller.State (active LOW: 0 = pressed, 1 = unpressed)
            ushort state = 0xFFFF;
            if (pUp) state &= unchecked((ushort)~Controller.Up);
            if (pDown) state &= unchecked((ushort)~Controller.Down);
            if (pLeft) state &= unchecked((ushort)~Controller.Left);
            if (pRight) state &= unchecked((ushort)~Controller.Right);
            if (pCross) state &= unchecked((ushort)~Controller.Cross);
            if (pCircle) state &= unchecked((ushort)~Controller.Circle);
            if (pSquare) state &= unchecked((ushort)~Controller.Square);
            if (pTriangle) state &= unchecked((ushort)~Controller.Triangle);
            if (pL1) state &= unchecked((ushort)~Controller.L1);
            if (pL2) state &= unchecked((ushort)~Controller.L2);
            if (pR1) state &= unchecked((ushort)~Controller.R1);
            if (pR2) state &= unchecked((ushort)~Controller.R2);
            if (pSelect) state &= unchecked((ushort)~Controller.Select);
            if (pStart) state &= unchecked((ushort)~Controller.Start);

            Controller.State = state;

            if (menuTriggered) OnMenuClicked?.Invoke();

            Invalidate();
            return true;
        }

        protected override void OnDraw(Canvas? canvas)
        {
            base.OnDraw(canvas);
            if (canvas == null || !TouchVisible) return;

            float density = Context?.Resources?.DisplayMetrics?.Density ?? 1f;
            int w = Width;
            int h = Height;
            if (w <= 0 || h <= 0) return;

            bool isPortrait = Context?.Resources?.Configuration?.Orientation == global::Android.Content.Res.Orientation.Portrait;

            float btnDp = isPortrait ? 52f : 58f;
            float btnPx = btnDp * density;
            float padPx = 15f * density;

            int alphaNorm = (int)(128 * TouchOpacity);
            int alphaHigh = (int)(220 * TouchOpacity);

            void DrawBtn(RectF rect, string label, Color textColor, bool pressed, float cornerRadius = 25f)
            {
                _fillPaint.Color = pressed ? Color.Argb(alphaHigh, 100, 100, 240) : Color.Argb(alphaNorm, 40, 40, 40);
                canvas.DrawRoundRect(rect, cornerRadius * density, cornerRadius * density, _fillPaint);

                _strokePaint.Color = pressed ? Color.White : Color.Argb(180, 200, 200, 200);
                _strokePaint.SetStyle(Paint.Style.Stroke);
                _strokePaint.StrokeWidth = 1.5f * density;
                canvas.DrawRoundRect(rect, cornerRadius * density, cornerRadius * density, _strokePaint);

                _textPaint.Color = textColor;
                _textPaint.TextSize = 14f * density;
                _textPaint.TextAlign = Paint.Align.Center;

                Paint.FontMetrics fm = _textPaint.GetFontMetrics();
                float textY = rect.CenterY() - (fm.Ascent + fm.Descent) / 2f;
                canvas.DrawText(label, rect.CenterX(), textY, _textPaint);
            }

            // 1. D-Pad
            float dpadW = btnPx * 3.1f;
            float dpadLeft = padPx;
            float dpadBottom = isPortrait ? (56f * density) : padPx;
            float dpadY = h - dpadBottom - dpadW;

            float dpadCenterX = dpadLeft + dpadW / 2f;
            float dpadCenterY = dpadY + dpadW / 2f;

            RectF rUp = new RectF(dpadCenterX - btnPx / 2f, dpadY, dpadCenterX + btnPx / 2f, dpadY + btnPx);
            RectF rDown = new RectF(dpadCenterX - btnPx / 2f, dpadY + dpadW - btnPx, dpadCenterX + btnPx / 2f, dpadY + dpadW);
            RectF rLeft = new RectF(dpadLeft, dpadCenterY - btnPx / 2f, dpadLeft + btnPx, dpadCenterY + btnPx / 2f);
            RectF rRight = new RectF(dpadLeft + dpadW - btnPx, dpadCenterY - btnPx / 2f, dpadLeft + dpadW, dpadCenterY + btnPx / 2f);

            DrawBtn(rUp, "▲", Color.White, _pUp);
            DrawBtn(rDown, "▼", Color.White, _pDown);
            DrawBtn(rLeft, "◄", Color.White, _pLeft);
            DrawBtn(rRight, "►", Color.White, _pRight);

            // 2. Action Buttons
            float actionW = btnPx * 3.1f;
            float actionRight = padPx;
            float actionBottom = isPortrait ? (56f * density) : padPx;
            float actionX = w - actionRight - actionW;
            float actionY = h - actionBottom - actionW;

            float actionCenterX = actionX + actionW / 2f;
            float actionCenterY = actionY + actionW / 2f;

            RectF rTriangle = new RectF(actionCenterX - btnPx / 2f, actionY, actionCenterX + btnPx / 2f, actionY + btnPx);
            RectF rCross = new RectF(actionCenterX - btnPx / 2f, actionY + actionW - btnPx, actionCenterX + btnPx / 2f, actionY + actionW);
            RectF rSquare = new RectF(actionX, actionCenterY - btnPx / 2f, actionX + btnPx, actionCenterY + btnPx / 2f);
            RectF rCircle = new RectF(actionX + actionW - btnPx, actionCenterY - btnPx / 2f, actionX + actionW, actionCenterY + btnPx / 2f);

            DrawBtn(rTriangle, "Δ", Color.Rgb(60, 220, 100), _pTriangle);
            DrawBtn(rSquare, "□", Color.Rgb(240, 100, 180), _pSquare);
            DrawBtn(rCircle, "O", Color.Rgb(240, 60, 60), _pCircle);
            DrawBtn(rCross, "X", Color.Rgb(80, 140, 240), _pCross);

            // 3. Shoulders
            float shW = (isPortrait ? 58f : 65f) * density;
            float shH = (isPortrait ? 34f : 38f) * density;
            float shBottom = isPortrait ? (btnPx * 3.1f + 62f * density) : (h - (10f * density) - shH);

            RectF rectL1 = isPortrait
                ? new RectF(padPx, h - shBottom - shH, padPx + shW, h - shBottom)
                : new RectF(padPx, 10f * density, padPx + shW, 10f * density + shH);

            RectF rectL2 = isPortrait
                ? new RectF(padPx + shW + 6f * density, h - shBottom - shH, padPx + shW * 2 + 6f * density, h - shBottom)
                : new RectF(padPx + shW + 8f * density, 10f * density, padPx + shW * 2 + 8f * density, 10f * density + shH);

            RectF rectR1 = isPortrait
                ? new RectF(w - padPx - shW * 2 - 6f * density, h - shBottom - shH, w - padPx - shW - 6f * density, h - shBottom)
                : new RectF(w - padPx - shW, 10f * density, w - padPx, 10f * density + shH);

            RectF rectR2 = isPortrait
                ? new RectF(w - padPx - shW, h - shBottom - shH, w - padPx, h - shBottom)
                : new RectF(w - padPx - shW * 2 - 8f * density, 10f * density, w - padPx - shW - 8f * density, 10f * density + shH);

            DrawBtn(rectL1, "L1", Color.White, _pL1, 15f);
            DrawBtn(rectL2, "L2", Color.White, _pL2, 15f);
            DrawBtn(rectR1, "R1", Color.White, _pR1, 15f);
            DrawBtn(rectR2, "R2", Color.White, _pR2, 15f);

            // 4. System buttons
            float sysW = 68f * density;
            float sysMenuW = 82f * density;
            float sysH = 34f * density;
            float sysBottom = 10f * density;
            float sysTotalW = sysW + sysMenuW + sysW + 16f * density;
            float sysStartX = (w - sysTotalW) / 2f;
            float sysY = h - sysBottom - sysH;

            RectF rectSelect = new RectF(sysStartX, sysY, sysStartX + sysW, sysY + sysH);
            RectF rectMenu = new RectF(sysStartX + sysW + 8f * density, sysY, sysStartX + sysW + 8f * density + sysMenuW, sysY + sysH);
            RectF rectStart = new RectF(sysStartX + sysW + sysMenuW + 16f * density, sysY, sysStartX + sysW + sysMenuW + 16f * density + sysW, sysY + sysH);

            DrawBtn(rectSelect, "SELECT", Color.White, _pSelect, 15f);
            DrawBtn(rectStart, "START", Color.White, _pStart, 15f);

            // Menu button with yellow highlight
            _fillPaint.Color = _pMenu ? Color.Argb(alphaHigh, 120, 120, 40) : Color.Argb((int)(180 * TouchOpacity), 20, 20, 20);
            canvas.DrawRoundRect(rectMenu, 15f * density, 15f * density, _fillPaint);

            _strokePaint.Color = Color.Yellow;
            _strokePaint.SetStyle(Paint.Style.Stroke);
            _strokePaint.StrokeWidth = 1.5f * density;
            canvas.DrawRoundRect(rectMenu, 15f * density, 15f * density, _strokePaint);

            _textPaint.Color = Color.Yellow;
            _textPaint.TextSize = 12f * density;
            _textPaint.TextAlign = Paint.Align.Center;
            Paint.FontMetrics fmMenu = _textPaint.GetFontMetrics();
            float textYMenu = rectMenu.CenterY() - (fmMenu.Ascent + fmMenu.Descent) / 2f;
            canvas.DrawText("⚙ MENU", rectMenu.CenterX(), textYMenu, _textPaint);
        }
    }
}
