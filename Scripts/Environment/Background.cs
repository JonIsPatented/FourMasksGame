using Godot;

[GlobalClass]
public partial class Background : Node2D
{
    Rid bgCanvasItem;

    public override void _EnterTree()
    {
        bgCanvasItem = RenderingServer.CanvasItemCreate();
        RenderingServer.CanvasItemSetDrawIndex(bgCanvasItem, -10);
        RenderingServer.CanvasItemSetUseParentMaterial(bgCanvasItem, true);
        RenderingServer.CanvasItemSetParent(bgCanvasItem, GetCanvasItem());
        Rect2 viewportRect = GetViewport().GetVisibleRect();
        RenderingServer.CanvasItemAddRect(bgCanvasItem, new(viewportRect.Position - viewportRect.Size / 2, viewportRect.Size), new(1, 1, 1));
    }

    public override void _Process(double delta)
    {
        GlobalPosition = GetViewport().GetCamera2D().GlobalPosition;
    }

    public override void _ExitTree()
    {
        RenderingServer.FreeRid(bgCanvasItem);
    }
}
