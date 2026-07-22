
namespace FourMasksGame.Scripts.Movement;

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
}
