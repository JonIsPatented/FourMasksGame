
using FourMasksGame.Scripts.Input;

namespace FourMasksGame.Scripts.Movement.States;

public class AbilityMovementState : MovementState
{
    MovementStateLabel MovementState.Label => MovementStateLabel.Ability;
    MovementState[] MovementState.FutureStates() => [
        new FallingMovementState(),
        new IdleMovementState(),
        new RunMovementState(),
    ];

    bool MovementState.CanEnter(MovementInfo info)
    {
        return InputAbilitySlot() != -1;
    }

    bool MovementState.ShouldExitTo(MovementInfo info, MovementStateLabel futureState)
    {
        return !info.usingAbility;
    }

    int abilitySlot;

    void MovementState.OnEnter(MovementInfo info)
    {
        abilitySlot = InputAbilitySlot();
    }

    /// <summary>
    /// Return the slot triggered by user input. Returns -1 if no slot is requested.
    /// </summary>
    /// <returns></returns>
    int InputAbilitySlot()
    {
        if (InputManager.Instance.GetActionPressed("LightAttack")) return 1;
        if (InputManager.Instance.GetActionPressed("ChargeAttack")) return 2;
        if (InputManager.Instance.GetActionPressed("Special")) return 3;
        return -1;
    }

    MovementDirective MovementState.Directive(MovementInfo info)
    {
        return new()
        {
            useAbility = true,
            abilitySlot = abilitySlot,
            useAbilityDirective = true,
        };
    }
}
