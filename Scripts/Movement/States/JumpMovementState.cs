
using FourMasksGame.Scripts.Input;

namespace FourMasksGame.Scripts.Movement.States;

public class JumpMovementState : MovementState
{
    MovementStateLabel MovementState.Label => MovementStateLabel.Jump;

    MovementState[] MovementState.FutureStates() => [
        new FallingMovementState(),
        new AbilityMovementState(),
    ];

    MovementDirective MovementState.Directive(MovementInfo info) => new()
    {
        playerControlsHorizontalVelocity = true,
        horizontalMovementSpeed = Constants.PLAYER_SPEED,
        impulseOnEnter = new(0f, Constants.PLAYER_JUMP_VELOCITY)
    };

    bool MovementState.CanEnter(MovementInfo info)
    {
        return info.grounded && InputManager.Instance.GetActionPressed("Jump", Constants.JUMP_PRESS_TOLERANCE);
    }

    bool MovementState.ShouldExitTo(MovementInfo info, MovementStateLabel futureState)
    {
        if (futureState == MovementStateLabel.Falling)
        {
            return info.realVelocity.Y > 0f; // greater than zero is down.
        }
        return true;
    }
}
