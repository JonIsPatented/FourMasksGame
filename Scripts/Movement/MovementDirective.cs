using Godot;

public struct MovementDirective
{
    public float horizontalMovementSpeed = 0f;
    public bool playerControlsHorizontalVelocity = true;
    public Vector2 impulseOnEnter = Vector2.Zero;

    public MovementDirective() {}
}
