using Godot;

public struct MovementDirective
{
    public float horizontalMovementSpeed = 0f;
    public bool playerControlsHorizontalVelocity = true;
    public Vector2 impulseOnEnter = Vector2.Zero;
    public bool useAbility = false;
    public int abilitySlot = -1;
    public bool useAbilityDirective = false;
    public bool useJumpGravity = false;

    public MovementDirective() {}
}
