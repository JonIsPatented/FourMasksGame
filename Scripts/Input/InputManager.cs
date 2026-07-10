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
    private readonly Dictionary<string, ActionRecord> actionRecords = new() {
        {"Jump", new()},
    };

    // The InputManager node enters the SceneTree with some default settings.
    public override void _Ready()
    {
        AimWithJoystick = false;
        contextStack.Push(new CutsceneInputContext()); // Using cutscene input as the default to prevent consumers collecting input without manipulating the context
        Instance = this;
    }

    // When the InputManager node receives input events allowed by the current context, it logs the process frame in which they occured in the bufferedActions table.
    public override void _Input(InputEvent inputEvent)
    {
        ulong frame = Engine.GetProcessFrames();
        if (CurrentContext.UseAny && !inputEvent.IsEcho())
        {
            foreach (string actionName in actionRecords.Keys)
            {
                if (inputEvent.IsActionPressed(actionName, false))
                {
                    actionRecords[actionName].hasPress = true;
                    actionRecords[actionName].framePressed = frame;
                }
                if (inputEvent.IsActionReleased(actionName))
                {
                    actionRecords[actionName].hasRelease = true;
                    actionRecords[actionName].frameReleased = frame;
                }
            }
        }
    }

    /// <summary>
    /// Enters a new current input context on top of the previous input context.
    /// </summary>
    /// <param name="context">The context to make current.</param>
    public void PushContext(InputContext context)
    {
        contextStack.Push(context);
    }

    /// <summary>
    /// Returns to the previous input context. Should only be used after pushing. Never pops the bottom context.
    /// </summary>
    public void PopContext()
    {
        if (contextStack.Count > 1)
        {
            contextStack.Pop();
        }
    }

    /// <summary>
    /// Change the bottom context of the context stack and remove all other contexts.
    /// </summary>
    /// <param name="context">The context to make both base and current.</param>
    public void ClearContext(InputContext context)
    {
        contextStack.Clear();
        contextStack.Push(context);
    }

    /// <summary>
    /// Get the axis of horizontal movement based on input actions. Returns zero if the context filters out the horizontal axis.
    /// </summary>
    /// <returns>A float where right is positive.</returns>
    public float GetHorizontalAxis()
    {
        if (CurrentContext.UseAny && CurrentContext.UseHorizontalAxis)
        {
            return Godot.Input.GetAxis("MoveLeft", "MoveRight");
        }
        return 0f;
    }

    /// <summary>
    /// Not implemented.
    /// </summary>
    /// <returns></returns>
    // TODO: Implement persistent storage of the most recent nonzero horizontal value.
    public float GetLastHorizontalAxis()
    {
        return 0f;
    }

    /// <summary>
    /// Get the aim direction as a Vector2 with a length in [0, 1].
    /// If the current input context filters aim inputs, this method returns Vector2.Zero.
    /// If AimWithJoystick is true, the result of this method is determined solely by input actions.
    /// Otherwise, the result of this method is determined by the mouse position.
    /// </summary>
    /// <param name="viewportAimOrigin">The point in the viewport from which the aim vector originates.</param>
    /// <returns>The vector in the direction of the mouse from the viewport aim origin, or the aim vector from joystick.</returns>
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

    // TODO: Implement persistent storage of the most recent nonzero aim vector.
    public Vector2 GetLastAim(Vector2 viewportAimOrigin)
    {
        return Vector2.Zero;
    }

    /// <summary>
    /// Get whether an action was pressed without being filtered within a certain tolerance.
    /// </summary>
    /// <param name="actionName">The action name in the input map.</param>
    /// <param name="bufferTolerance">The number of frames before the current frame to include in the window of possible press frames.</param>
    /// <returns></returns>
    public bool GetActionPressed(string actionName, uint bufferTolerance = 0)
    {
        if (actionRecords.ContainsKey(actionName))
        {
            return actionRecords[actionName].hasPress && actionRecords[actionName].framePressed >= Engine.GetProcessFrames() - bufferTolerance;
        }
        return false;
    }

    /// <summary>
    /// Get whether an action is currently held.
    /// </summary>
    /// <param name="actionName">The action name in the input map.</param>
    /// <returns>True if the action is held and not filtered by the context.</returns>
    public bool GetActionHeld(string actionName)
    {
        return CurrentContext.UseAny && Godot.Input.IsActionPressed(actionName);
    }

    /// <summary>
    /// Records input events for a particular boolean action.
    /// </summary>
    private class ActionRecord
    {
        public ActionRecord() { }

        /// <summary>
        /// Whether a press has ever been recorded in this record.
        /// </summary>
        public bool hasPress = false;

        /// <summary>
        /// The frame this action was most recently pressed. The default value is zero, use hasPress to determine if this field actually represents a press.
        /// </summary>
        public ulong framePressed = 0;

        /// <summary>
        /// Whether a release has ever been recorded in this record.
        /// </summary>
        public bool hasRelease = false;

        /// <summary>
        /// The frame this action was most recently released. The default value is zero, use hasRelease to determine if this field actually represents a release.
        /// </summary>
        public ulong frameReleased = 0;
    }
}

