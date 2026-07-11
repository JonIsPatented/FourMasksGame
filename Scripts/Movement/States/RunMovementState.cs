
using FourMasksGame.Scripts.Input;

namespace FourMasksGame.Scripts.Movement.States;

public class RunMovementState : MovementState
{
    MovementStateLabel MovementState.Label => MovementStateLabel.Run;

    MovementState[] MovementState.FutureStates() => [
        new IdleMovementState(),
        new JumpMovementState(),
        new FallingMovementState(),
    ];

    MovementDirective MovementState.Directive(MovementInfo info) => new()
    {
        playerControlsHorizontalVelocity = true,
        horizontalMovementSpeed = Constants.PLAYER_SPEED,
    };

    bool MovementState.CanEnter(MovementInfo info)
    {
        return info.grounded && InputManager.Instance.GetHorizontalAxis() != 0f;
    }
}
