using Godot;

namespace FourMasksGame.Scripts.Movement;

public partial class MovementStateMachine : GodotObject
{
    private MovementState currentState;

    // An empty constructor is necessary for classes inheriting GodotObject.
    public MovementStateMachine() {}
}
