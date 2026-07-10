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

    /// <summary>
    /// Allow the current state to process, then query for and possibly perform a transition. No more than one transition will be performed per call, so each state in a chain will be current after at least one process call.
    /// </summary>
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
