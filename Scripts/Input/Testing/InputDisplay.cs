using Godot;

namespace FourMasksGame.Scripts.Input.Testing;

public partial class InputDisplay : Node2D
{
    [Export] public InputType inputType;
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
