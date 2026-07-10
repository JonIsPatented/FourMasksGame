using Godot;
using FourMasksGame.Scripts.Movement;

namespace FourMasksGame.Scripts;

public partial class Player : CharacterBody2D
{
    private MovementStateMachine movementStateMachine;

    public override void _EnterTree()
    {
        movementStateMachine = new();
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
    }
}
