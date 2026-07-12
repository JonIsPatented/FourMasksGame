
using FourMasksGame.Scripts.Input;

namespace FourMasksGame.Scripts.Movement.States;

public class IdleMovementState : MovementState
{
    MovementStateLabel MovementState.Label => MovementStateLabel.Idle;

    MovementState[] MovementState.FutureStates() => [
        new FallingMovementState(),
        new JumpMovementState(),
        new RunMovementState(),
        new AbilityMovementState(),
    ];

    MovementDirective MovementState.Directive(MovementInfo info) => new()
    {
        playerControlsHorizontalVelocity = true,
        horizontalMovementSpeed = 0f,
    };

    bool MovementState.CanEnter(MovementInfo info)
    {
        return info.grounded && InputManager.Instance.GetHorizontalAxis() == 0f;
    }
}
