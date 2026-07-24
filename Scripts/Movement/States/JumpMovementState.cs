
using FourMasksGame.Scripts.Input;
using Godot;

namespace FourMasksGame.Scripts.Movement.States;

public class JumpMovementState : MovementState
{
    MovementStateLabel MovementState.Label => MovementStateLabel.Jump;

    MovementState[] MovementState.FutureStates() => [
        new FallingMovementState(),
        new AbilityMovementState(),
        new IdleMovementState(),
    ];

    MovementDirective MovementState.Directive(MovementInfo info) => new()
    {
        playerControlsHorizontalVelocity = true,
        horizontalMovementSpeed = Constants.PLAYER_SPEED,
        impulseOnEnter = new(0f, Constants.PlayerJumpVelocity()),
        useJumpGravity = true,
    };

    bool MovementState.CanEnter(MovementInfo info)
    {
        return (info.grounded || CanCoyote(info)) && InputManager.Instance.GetActionPressed("Jump", Constants.JUMP_PRESS_TOLERANCE);
    }

    bool CanCoyote(MovementInfo info)
    {
        return (Time.GetTicksMsec() / 1000f) - info.groundedTime < 0.1f;
    }

    bool MovementState.ShouldExitTo(MovementInfo info, MovementStateLabel futureState)
    {
        if (futureState == MovementStateLabel.Falling)
        {
            return info.realVelocity.Y > 0f || !InputManager.Instance.GetActionHeld("Jump"); // greater than zero is down.
        }
        return true;
    }
}
