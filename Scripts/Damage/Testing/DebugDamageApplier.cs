using Godot;

namespace FourMasksGame.Scripts.Damage.Testing;

public partial class DebugDamageApplier : Node2D
{
    [Export] DamageSender damageSender;
    [Export] float damage = 1f;

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey keyEvent)
        {
            if (keyEvent.Keycode == Key.Ctrl && keyEvent.IsPressed() && !keyEvent.IsEcho())
            {
                damageSender.Load(new DamageSource.Attack(0f, 0.5f, damage, 1));
            }
        }
    }
}