using Godot;
using System.Collections.Generic;

namespace FourMasksGame.Scripts.Input;

// USAGE

// Use PushContext and PopContext with InputContext instances to control input filtering.
// GetAim and GetHorizontalAxis deal with the special cases for aiming and horizontal control.
// GetActionPressed uses buffered actions (not yet implemented), GetActionHeld uses the direct held state.

// Future plans for this class: implement persistent storage of the aiming and horizontal axis so that default values can be used when the player triggers a move without providing direction input.

public partial class InputManager : Node
{
    // The 
    public static InputManager Instance { get; private set; }

    // If AimWithJoystick is true, GetAim will use input actions to calculate aim. Otherwise, the mouse position will be used.
    // TODO: Set AimWithJoystick automatically based on gamepad use.
    public bool AimWithJoystick { get; set; }

    // CurrentContext is the top context of the context stack, which is the context that controls the current input filtering.
    public InputContext CurrentContext { get => contextStack.Peek(); }

    // The context stack contains the current input context and the series of input contexts that should be returned to with the PopContext method.
    private readonly Stack<InputContext> contextStack = new();

    // The InputManager populates this table with the frames that these actions occurred as it receives them.
    // The InputManager will not log an action unless it already has an entry.
    // TODO: Populate this table with "frame zero" entries.
    private readonly Dictionary<string, ulong> bufferedActions = [];

    // The InputManager node enters the SceneTree with some default settings.
    public override void _Ready()
    {
        AimWithJoystick = false;
        contextStack.Push(new NormalInputContext());
        Instance = this;
    }

    // When the InputManager node receives input events allowed by the current context, it logs the process frame in which they occured in the bufferedActions table.
    public override void _Input(InputEvent inputEvent)
    {
        ulong frame = Engine.GetProcessFrames();
        if (CurrentContext.UseAny)
        {
            foreach (string actionName in bufferedActions.Keys)
            {
                if (inputEvent.IsAction(actionName))
                {
                    bufferedActions[actionName] = frame;
                }
            }
        }
    }

    // Enters a new current input context on top of the previous input context.
    public void PushContext(InputContext context)
    {
        contextStack.Push(context);
    }

    // Returns to the previous input context. Should only be used after pushing.
    public void PopContext()
    {
        contextStack.Pop();
    }

    // Get the axis of horizontal movement based on input actions.
    // Returns zero if the context filters out the horizontal axis.
    // TODO: Implement persistent storage of the most recent nonzero horizontal value.
    public float GetHorizontalAxis()
    {
        if (CurrentContext.UseAny && CurrentContext.UseHorizontalAxis)
        {
            return Godot.Input.GetAxis("MoveLeft", "MoveRight");
        }
        return 0f;
    }

    // Get the aim direction as a Vector2 with a length in [0, 1].
    // If the current input context filters aim inputs, this method returns Vector2.Zero.
    // If AimWithJoystick is true, the result of this method is determined solely by input actions.
    // Otherwise, the result of this method is determined by the mouse position.
    // The viewport aim origin is the point in the viewport from which the aim vector originates.
    // The aim vector returned will be in the direction of the mouse from the viewport aim origin.
    // TODO: Implement persistent storage of the most recent nonzero aim vector.
    public Vector2 GetAim(Vector2 viewportAimOrigin)
    {
        if (CurrentContext.UseAny && CurrentContext.UseAim)
        {
            Vector2 aimVector;

            if (AimWithJoystick)
            {
                aimVector = Godot.Input.GetVector("AimLeft", "AimRight", "AimUp", "AimDown");
            }
            else
            {
                Vector2 viewportMousePosition = GetViewport().GetMousePosition();
                aimVector = viewportMousePosition - viewportAimOrigin;
            }

            if (aimVector.LengthSquared() < 1)
            {
                return aimVector;
            }
            else
            {
                return aimVector.Normalized();
            }
        }
        return Vector2.Zero;
    }

    // Get whether an action was pressed without being filtered within a certain tolerance.
    public bool GetActionPressed(string actionName, uint bufferTolerance = 0)
    {
        if (bufferedActions.ContainsKey(actionName))
        {
            return bufferedActions[actionName] < Engine.GetProcessFrames() + bufferTolerance;
        }
        return false;
    }

    // Get whether an action is currently held.
    public bool GetActionHeld(string actionName)
    {
        return CurrentContext.UseAny && Godot.Input.IsActionJustPressed(actionName);
    }
}

