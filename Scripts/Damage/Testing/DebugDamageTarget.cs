using Godot;

namespace FourMasksGame.Scripts.Damage.Testing;

public partial class DebugDamageTarget : Label
{
    [Export] private DamageReceiver damageReceiver;
    [Export] private float health = 2f;
    [Export] private int team = 0;
    
    public override void _Process(double delta)
    {
        foreach (DamageSource source in damageReceiver.Receive(team))
        {
            health -= source.Damage;
        }

        Text = $"{health}";
    }
}
