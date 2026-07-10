using Godot;
using FourMasksGame.Scripts.Movement;
using FourMasksGame.Scripts.Input;

namespace FourMasksGame.Scripts;

public partial class Player : CharacterBody2D
{
    private MovementStateMachine movementStateMachine;
    [Export] private Sprite2D sprite;

    // I don't think export variables are set until after _EnterTree and _Ready, so be careful about using exported node references here.
    public override void _EnterTree()
    {
        movementStateMachine = new();
        movementStateMachine.EnterState(new Movement.States.IdleMovementState());
    }

    public override void _Process(double delta)
    {
        movementStateMachine.PassInfo(new()
        {
            grounded = IsOnFloor(),
            realVelocity = GetRealVelocity()
        });
        movementStateMachine.Process();
        if (movementStateMachine.TransitionOnLastProcess)
        {
            Velocity = GetRealVelocity() + movementStateMachine.GetDirective().impulseOnEnter;
        }

        if (sprite != null)
        {
            sprite.FlipH = InputManager.Instance.GetLastHorizontalAxis() < 0f;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        MovementDirective directive = movementStateMachine.GetDirective();

        // For a character body, the Velocity property actually represents a requested velocity.
        // Without a reset, applications like gravity will pile up forever.
        // So I'm resetting each step here to the real velocity.
        // That means that the requested Y velocity will stop accumulating if on the ground.
        Velocity = GetRealVelocity();
        if (!IsOnFloor())
        {
            Velocity += GetGravity() * (float)delta;
        }

        // The player controls horizontal velocity directive determines whether the player gets to set the horizontal velocity with input.
        if (directive.playerControlsHorizontalVelocity)
        {
            Velocity = new(directive.horizontalMovementSpeed * InputManager.Instance.GetHorizontalAxis(), Velocity.Y);
        }

        MoveAndSlide();
    }
}
