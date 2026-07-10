
namespace FourMasksGame.Scripts.Movement;

/// <summary>
/// Implementors define a state for the movement state machine.
/// </summary>
public interface MovementState
{
    /// <summary>
    /// Whether this state can become the current state. If the current state returns true for ShouldExitTo for this state and CanEnter returns true for this state, a transition occurs to this state.
    /// </summary>
    /// <param name="info"></param>
    public bool CanEnter(MovementInfo info) { return true; }

    /// <summary>
    /// Called once when this state is entered during a transition.
    /// </summary>
    /// <param name="info"></param>
    public void OnEnter(MovementInfo info) { }

    /// <summary>
    /// Called once each frame that this state is the current state.
    /// </summary>
    /// <param name="info"></param>
    public void Process(MovementInfo info) { }

    /// <summary>
    /// Called once when this state is exited during a transition.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="futureState"></param>
    public void OnExit(MovementInfo info, MovementState futureState) { }

    /// <summary>
    /// Called once when the state is entered to get all states for which should exit to will be queried.
    /// 
    /// Future states will be queried in the order they are declared here.
    /// </summary>
    /// <returns></returns>
    public MovementState[] FutureStates();

    /// <summary>
    /// Called once per frame for each state in FutureStates. If ShouldExitTo returns true and CanEnter returns true for the future state, a transition occurs to that future state.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="futureState"></param>
    /// <returns></returns>
    public bool ShouldExitTo(MovementInfo info, MovementState futureState) { return true; }
}
