using Godot;

namespace FourMasksGame.Scripts.Damage.Testing;

public partial class DebugDamageApplier : Node2D
{
    [Export] DamageSender damageSender;

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey keyEvent)
        {
            if (keyEvent.Keycode == Key.Ctrl)
            {
                damageSender.Load(new DamageSource.Attack(0f, 0.5f, 1f, 1));
            }
        }
    }
}