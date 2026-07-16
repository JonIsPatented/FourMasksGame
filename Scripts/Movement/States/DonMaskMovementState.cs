using FourMasksGame.Scripts.Masks;
using FourMasksGame.Scripts.Input;

namespace FourMasksGame.Scripts.Movement.States;

public partial class DonMaskMovementState : MovementState
{
    bool MovementState.CanEnter(MovementInfo info)
    {
        return GetMaskShouldChangeTo() != -1;
    }

    private int GetMaskShouldChangeTo()
    {
        Mask currentMask = MaskManager.Instance.CurrentMask;
        if (InputManager.Instance.GetActionPressed("DonMask1", 5) && currentMask.Name != "Wing") {
            return 1;
        }
        if (InputManager.Instance.GetActionPressed("DonMask2", 5) && currentMask.Name != "Demon") {
            return 2;
        }
        if (InputManager.Instance.GetActionPressed("DonMask3", 5) && currentMask.Name != "Mist") {
            return 3;
        }
        if (InputManager.Instance.GetActionPressed("DonMask4", 5) && currentMask.Name != "Golem") {
            return 4;
        }
        return -1;
    }

    MovementDirective MovementState.Directive(MovementInfo info)
    {
        return new() {
            playerControlsHorizontalVelocity = false,
        };
    }

    MovementState[] MovementState.FutureStates() => [
        new IdleMovementState(),
    ];

    void MovementState.OnEnter(MovementInfo info)
    {
        MaskManager.Instance.ChangeMask(GetMaskShouldChangeTo());
    }

    MovementStateLabel MovementState.Label => MovementStateLabel.DonMask;
}
