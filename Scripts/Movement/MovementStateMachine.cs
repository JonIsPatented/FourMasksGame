using Godot;

namespace FourMasksGame.Scripts.Movement;

public partial class MovementStateMachine : GodotObject
{
    private MovementState currentState;
    private MovementState[] futureStates = [];
    private MovementInfo movementInfo = new();

    // An empty constructor is necessary for classes inheriting GodotObject.
    public MovementStateMachine() { }

    public void PassInfo(MovementInfo info)
    {
        movementInfo = info;
    }

    public void Process()
    {
        currentState?.Process(movementInfo);
        foreach (MovementState futureState in futureStates)
        {
            if (futureState.CanEnter(movementInfo))
            {
                if (currentState != null)
                {
                    if (currentState.ShouldExitTo(movementInfo, futureState))
                    {
                        TransitionToState(futureState);
                    }
                }
                else
                {
                    TransitionToState(futureState);
                }
            }
        }
    }

    public void EnterState(MovementState state)
    {
        TransitionToState(state);
    }

    private void TransitionToState(MovementState state)
    {
        currentState?.OnExit(movementInfo, state);
        currentState = state;
        futureStates = currentState?.FutureStates();
        futureStates ??= [];
        state?.OnEnter(movementInfo);
    }
}
