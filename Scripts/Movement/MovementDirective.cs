using Godot;

public struct MovementDirective
{
    float horizontalMovementSpeed = 0f;
    bool playerControlsHorizontalVelocity = true;
    Vector2 impulseOnEnter = Vector2.Zero;

    public MovementDirective() {}
}
