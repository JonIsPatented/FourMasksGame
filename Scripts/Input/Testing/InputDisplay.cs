using Godot;

namespace FourMasksGame.Scripts.Input.Testing;

public partial class InputDisplay : Node2D
{
    /// <summary>
    /// The type of input this display should represent.
    /// 
    /// Action input represents the pressed and held state of an action with a circle. The action name determines which action is represented.
    /// 
    /// Aim input represents the aim input state with a line. Persistence is not implemented for the aim input.
    /// 
    /// Horizontal Axis input represents the horizontal axis input state. If persistent is checked, it will represent the "last" horizontal axis state.
    /// </summary>
    [Export] public InputType inputType;

    /// <summary>
    /// If the input type is Horizontal Axis, whether the "last" or "current" horizontal axis should be represented.
    /// </summary>
    [Export] public bool persistent;

    /// <summary>
    /// If the input type is Action, the name of the action to be represented.
    /// </summary>
    [Export] public string actionName;

    private Rid canvasItem;

    public override void _EnterTree()
    {
        canvasItem = RenderingServer.CanvasItemCreate();
        RenderingServer.CanvasItemSetParent(canvasItem, GetCanvasItem());
    }

    public override void _Process(double delta)
    {
        InputManager im = InputManager.Instance;
        RenderingServer.CanvasItemClear(canvasItem);
        switch (inputType)
        {
            case InputType.Action:
                bool actionPressed = im.GetActionPressed(actionName);
                bool actionHeld = im.GetActionHeld(actionName);
                RenderingServer.CanvasItemAddCircle(canvasItem, Vector2.Zero, actionPressed ? 20f : actionHeld ? 10f : 5f, new(1, 1, 1), true);
                break;
            case InputType.Aim:
                Vector2 aimVector = im.GetAim(GetGlobalTransformWithCanvas().Origin);
                RenderingServer.CanvasItemAddLine(canvasItem, Vector2.Zero, aimVector * 20f, new(1, 1, 1), 5f, true);
                break;
            case InputType.Horizontal:
                float horizontalAxis = im.GetHorizontalAxis();
                if (persistent)
                    horizontalAxis = im.GetLastHorizontalAxis();
                RenderingServer.CanvasItemAddLine(canvasItem, Vector2.Zero, horizontalAxis * Vector2.Right * 20f, new(1, 1, 1), 5f, true);
                break;
        }
    }

    public override void _ExitTree()
    {
        RenderingServer.FreeRid(canvasItem);
    }

    public enum InputType
    {
        Action,
        Aim,
        Horizontal,
    }
}
