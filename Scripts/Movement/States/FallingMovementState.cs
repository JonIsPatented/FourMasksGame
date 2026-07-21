
using FourMasksGame.Scripts.Input;

namespace FourMasksGame.Scripts.Movement.States;

public class FallingMovementState : MovementState
{
    MovementStateLabel MovementState.Label => MovementStateLabel.Falling;

    MovementState[] MovementState.FutureStates() => [
        new IdleMovementState(),
        new JumpMovementState(),
        new RunMovementState(),
    ];

    MovementDirective MovementState.Directive(MovementInfo info) => new()
    {
        playerControlsHorizontalVelocity = true,
        horizontalMovementSpeed = Constants.PLAYER_SPEED,
    };

    bool MovementState.CanEnter(MovementInfo info)
    {
        return !info.grounded;
    }
}
