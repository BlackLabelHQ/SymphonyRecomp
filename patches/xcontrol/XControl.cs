using RecompOne.Runtime.Events;
using Sotn;

namespace Recompiled;

/*
 * Gives the player X-like controls inside SOTN's castle: an air/ground dash
 * (R1), a buster shot (R2, reuses the native Dagger subweapon-throw path so
 * projectile spawning/collision/damage all run through already-proven game
 * logic instead of a hand-built entity), wall-cling/wall-jump, and a
 * dash-jump that carries dash speed into the jump arc. Palette shade gives a
 * quick, honest visual cue -- real sprite replacement is a separate, much
 * larger reverse-engineering task and is intentionally not attempted here.
 */
public static class XControl
{
    const int FixedOne = 0x10000;
    const int DashSpeed = 12 * FixedOne;
    const int DashFrames = 12;
    const int DashCooldownFrames = 24;

    // Wall-cling / wall-jump tuning. Sign convention assumed from the
    // existing dash code (positive VelocityX = right) and typical PS1 2D
    // platformer convention (positive VelocityY = falling/down, negative =
    // up). If jumping/falling look inverted in-game, flip these signs.
    const int WallSlideMaxFallSpeed = 2 * FixedOne;
    const int WallJumpVelocityY = -14 * FixedOne;
    const int WallJumpPushX = 10 * FixedOne;
    const int WallStickVelocityThreshold = FixedOne / 4; // "near zero" = blocked by a wall

    // Dash-jump: pressing jump while a dash is active, or shortly after one
    // ends, boosts upward velocity and carries dash speed into the jump arc
    // instead of native deceleration killing it immediately.
    const int DashJumpBufferFrames = 8;
    const int DashJumpExtraVelocityY = -4 * FixedOne;
    const int DashJumpCarryFrames = 16;
    const int DashJumpCarrySpeed = 9 * FixedOne;
    const int NoDashSentinel = 9999;

    static bool _prevDash;
    static bool _prevShoot;
    static bool _prevJump;
    static int _dashFramesLeft;
    static int _dashCooldown;
    static int _dashEndedFramesAgo = NoDashSentinel;
    static int _dashJumpCarryLeft;
    static bool _dashJumpCarryFacingLeft;

    // Exposed for XSpriteRenderer's frame-selection state machine -- dash is
    // purely injected state (not a native field), so it can't be read any
    // other way.
    public static bool IsDashingNow => _dashFramesLeft > 0;

    public static void Register()
    {
        Event.AddListener<VSyncEvent>(OnVSync);
    }

    static void OnVSync(VSyncEvent e)
    {
        if (!Game.Available || !Game.InGame || Game.IsLoading) return;
        if (!Player.HasControl) return;

        bool dashHeld = Game.IsPressed(Button.R1);
        bool shootHeld = Game.IsPressed(Button.R2);
        bool jumpHeld = Game.IsPressed(Button.Cross);
        bool leftHeld = Game.IsPressed(Button.Left);
        bool rightHeld = Game.IsPressed(Button.Right);

        bool dashPressed = dashHeld && !_prevDash;
        bool shootPressed = shootHeld && !_prevShoot;
        bool jumpPressed = jumpHeld && !_prevJump;
        _prevDash = dashHeld;
        _prevShoot = shootHeld;
        _prevJump = jumpHeld;

        if (_dashCooldown > 0) _dashCooldown--;
        if (_dashEndedFramesAgo < NoDashSentinel) _dashEndedFramesAgo++;

        bool airborne = Player.Step == PlayerStep.Aerial;

        // --- Dash (ground/air velocity burst) ---
        bool dashActiveThisFrame = _dashFramesLeft > 0;
        if (dashActiveThisFrame)
        {
            Player.VelocityX = Player.FacingLeft ? -DashSpeed : DashSpeed;
            _dashFramesLeft--;
            if (_dashFramesLeft == 0) _dashEndedFramesAgo = 0;
        }
        else if (dashPressed && _dashCooldown == 0)
        {
            _dashFramesLeft = DashFrames;
            _dashCooldown = DashCooldownFrames;
            dashActiveThisFrame = true;
        }

        // --- Dash-jump: pressing jump during, or shortly after, a dash ---
        bool dashActiveOrRecent = dashActiveThisFrame || _dashEndedFramesAgo <= DashJumpBufferFrames;
        if (jumpPressed && dashActiveOrRecent)
        {
            Player.VelocityY += DashJumpExtraVelocityY;
            _dashJumpCarryLeft = DashJumpCarryFrames;
            _dashJumpCarryFacingLeft = Player.FacingLeft;
            _dashFramesLeft = 0; // hand off from dash to jump-carry
            _dashEndedFramesAgo = NoDashSentinel; // consume the buffer
        }

        if (_dashJumpCarryLeft > 0)
        {
            Player.VelocityX = _dashJumpCarryFacingLeft ? -DashJumpCarrySpeed : DashJumpCarrySpeed;
            _dashJumpCarryLeft--;
        }

        // --- Wall-cling / wall-jump ---
        // No direct wall-collision flag is exposed on Entity, so this is
        // approximated: airborne, holding a direction, and horizontal
        // velocity near zero despite holding that direction (native
        // collision is blocking movement into a wall).
        bool holdingOneDirection = leftHeld != rightHeld;
        bool wallBlocked = airborne && holdingOneDirection &&
                            System.Math.Abs(Player.VelocityX) < WallStickVelocityThreshold;

        if (wallBlocked)
        {
            bool wallOnRight = rightHeld;

            if (Player.VelocityY > WallSlideMaxFallSpeed)
                Player.VelocityY = WallSlideMaxFallSpeed;

            if (jumpPressed)
            {
                Player.VelocityY = WallJumpVelocityY;
                Player.VelocityX = wallOnRight ? -WallJumpPushX : WallJumpPushX;
                Player.FacingLeft = wallOnRight; // kick off facing away from the wall
                _dashJumpCarryLeft = 0; // wall-jump takes priority over any dash-jump carry
            }
        }

        if (shootPressed)
        {
            FireBusterShot();
        }

        XSpriteRenderer.Tick(dashActiveThisFrame, airborne, leftHeld != rightHeld, Player.FacingLeft);
    }

    static void FireBusterShot()
    {
        // Force the native Dagger subweapon path: guarantee ammo, select the
        // dagger, then flip the same status bit the game's own input handling
        // sets when the player throws a subweapon. The native code owns
        // spawning/velocity/collision/damage from there -- no hand-built
        // entity, so no risk of a malformed spawn.
        if (Player.Hearts <= 0) Player.Hearts = 1;
        Inventory.HeldSubweapon = Subweapon.Dagger;
        Player.Status |= PlayerStatus.SubWeapon;
    }
}
