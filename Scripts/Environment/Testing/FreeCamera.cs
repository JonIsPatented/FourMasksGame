using Godot;

public partial class FreeCamera : Camera2D
{
    [Export(PropertyHint.Range, "30,600")]
    public float Speed = 150f;

    public override void _Process(double delta)
    {
        Vector2 aimVector = Vector2.Zero;
        aimVector += Input.IsKeyPressed(Key.A) ? Vector2.Left: Vector2.Zero;
        aimVector += Input.IsKeyPressed(Key.D) ? Vector2.Right : Vector2.Zero;
        aimVector += Input.IsKeyPressed(Key.W) ? Vector2.Up : Vector2.Zero;
        aimVector += Input.IsKeyPressed(Key.S) ? Vector2.Down : Vector2.Zero;
        Transform2D transform = Transform;
        transform.Origin = Transform.Origin + aimVector * Speed * (float)delta;
        Transform = transform;
    }
}
