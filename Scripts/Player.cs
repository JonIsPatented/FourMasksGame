using Godot;
using FourMasksGame.Scripts.Movement;
using FourMasksGame.Scripts.Input;
using FourMasksGame.Scripts.Abilities;

namespace FourMasksGame.Scripts;

public partial class Player : CharacterBody2D
{
    private MovementStateMachine movementStateMachine;
    private AbilityBridge abilityBridge;

    [Export] private AnimatedSprite2D sprite;

    // I don't think export variables are set until after _EnterTree and _Ready, so be careful about using exported node references here.
    public override void _EnterTree()
    {
        movementStateMachine = new();
        movementStateMachine.EnterState(new Movement.States.IdleMovementState());
        abilityBridge = new();
    }

    public override void _Process(double delta)
    {
        movementStateMachine.PassInfo(new()
        {
            grounded = IsOnFloor(),
            realVelocity = GetRealVelocity(),
            usingAbility = abilityBridge.UsingAbility(),
        });
        movementStateMachine.Process();

        if (sprite != null)
        {
            sprite.FlipH = InputManager.Instance.GetLastHorizontalAxis() < 0f;
        }

        if (movementStateMachine.TransitionOnLastProcess)
        {
            MovementDirective directive = movementStateMachine.GetDirective();
            if (directive.useAbility)
            {
                bool abilityUsed = abilityBridge.UseAbility(this, directive.abilitySlot);
                if (abilityUsed)
                {
                    sprite.Visible = false;
                }
                else
                {
                    // ability-motivating state failed, so escape to a viable state.
                    movementStateMachine.EscapeState();
                }
            }
        }

        if (abilityBridge.UsingAbility())
        {
            // allow chains and ending
            bool continuedAbility = abilityBridge.ContinueAbility();
            if (!continuedAbility)
            {
                movementStateMachine.EscapeState();
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        MovementDirective directive = movementStateMachine.GetDirective();
        if (directive.useAbilityDirective)
        {
            PhysicsFollowAbilityDirective(abilityBridge.GetDirective(), delta);
        }
        else
        {
            PhysicsFollowMovementDirective(directive, delta);
        }
        MoveAndSlide();
    }

    public void PhysicsFollowMovementDirective(MovementDirective directive, double delta)
    {
        // For a character body, the Velocity property actually represents a requested velocity.
        // Without a reset, applications like gravity will pile up forever.
        // So I'm resetting each step here to the real velocity.
        // That means that the requested Y velocity will stop accumulating if on the ground.
        Velocity = GetRealVelocity();

        if (movementStateMachine.TransitionOnLastProcess)
        {
            //* I think this logic is frame dependent. Not sure how to fix it.
            //* I think GetRealVelocity()'s return value is time-interpolated, so it changes slowly based on the requested velocity.
            //* That would make this the correct method, maybe? It needs to be evaluated at different framerates.

            Velocity = GetRealVelocity() + movementStateMachine.GetDirective().impulseOnEnter;
        }

        Velocity += GetGravity() * (float)delta;

        // The player controls horizontal velocity directive determines whether the player gets to set the horizontal velocity with input.
        if (directive.playerControlsHorizontalVelocity)
        {
            Velocity = new(directive.horizontalMovementSpeed * InputManager.Instance.GetHorizontalAxis(), Velocity.Y);
        }
    }

    public void PhysicsFollowAbilityDirective(AbilityDirective directive, double delta)
    {
        
    }
}
