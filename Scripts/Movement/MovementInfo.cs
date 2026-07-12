
using Godot;

namespace FourMasksGame.Scripts.Movement;

/// <summary>
/// Information block for MovementState implementors.
/// </summary>
public struct MovementInfo
{
    public bool grounded;
    public Vector2 realVelocity;
    public bool usingAbility;
}
