using Godot;

public partial class FreeCamera : Camera2D
{
    [Export(PropertyHint.Range, "30,600")]
    public float Speed = 150f;

    public override void _Process(double delta)
    {
        Vector2 aimVector = Input.GetVector("AimLeft", "AimRight", "AimUp", "AimDown");
        Transform2D transform = Transform;
        transform.Origin = Transform.Origin + aimVector * Speed * (float)delta;
    }
}
