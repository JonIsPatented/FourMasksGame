
using Godot;

namespace FourMasksGame.Scripts.Movement;

/// <summary>
/// Information block for MovementState implementors.
/// </summary>
public struct MovementInfo
{
    public bool grounded;
    public float groundedTime;
    public Vector2 realVelocity;
    public bool usingAbility;
}
