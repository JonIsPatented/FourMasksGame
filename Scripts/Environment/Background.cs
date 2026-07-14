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

    public override void _Ready()
    {
        GetParent().CallDeferred("remove_child", this);
        GetViewport().GetCamera2D().CallDeferred("add_child", this);
    }

    public override void _ExitTree()
    {
        RenderingServer.FreeRid(bgCanvasItem);
    }
}
