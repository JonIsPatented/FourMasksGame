
namespace FourMasksGame.Scripts.Movement;

using FourMasksGame.Scripts.Masks;

public static class Constants
{
    public const float JUMP_TIME_TO_PEAK = 0.35f;
    public const float JUMP_HEIGHT = 55f;
    public const float JUMP_TIME_TO_GROUND = 0.30f;

    static Constants() {
        PLAYER_JUMP_VELOCITY = -2f * JUMP_HEIGHT / JUMP_TIME_TO_PEAK;
        PLAYER_RISE_GRAVITY = 2f * JUMP_HEIGHT / (JUMP_TIME_TO_PEAK * JUMP_TIME_TO_PEAK);
        PLAYER_FALL_GRAVITY = 2f * JUMP_HEIGHT / (JUMP_TIME_TO_GROUND * JUMP_TIME_TO_GROUND);
    }

    public const float PLAYER_SPEED = 120f;
    public static readonly float PLAYER_JUMP_VELOCITY;
    public static readonly float PLAYER_RISE_GRAVITY;
    public static readonly float PLAYER_FALL_GRAVITY;
    public static readonly uint JUMP_PRESS_TOLERANCE = 5;

    private static float GetJumpHeight()
    {
        Mask _m = MaskManager.Instance.CurrentMask;

        if (_m == null)
        {
            return 0f;
        }

        if (_m.Name == "Wing")
        {
            return 56f;
        }

        if (_m.Name == "Demon")
        {
            return 64f;
        }

        return 0f;
    }

    private static float GetTimeToPeak()
    {
        return 0.8f;
    }

    private static float GetTimeToGround()
    {
        Mask _m = MaskManager.Instance.CurrentMask;

        if (_m == null)
        {
            return 0.1f;
        }

        if (_m.Name == "Wing")
        {
            return 2.4f;
        }

        if (_m.Name == "Demon")
        {
            return 0.3f;
        }

        return 0.1f;
    }

    public static float PlayerJumpVelocity()
    {
        return 2f * GetJumpHeight() / GetTimeToPeak();
    }

    public static float PlayerRiseGravity()
    {
        return 2f * GetJumpHeight() / (GetTimeToPeak() * GetTimeToPeak());
    }

    public static float PlayerFallGravity()
    {
        return 2f * GetJumpHeight() / (GetTimeToGround() * GetTimeToGround());
    }
}
