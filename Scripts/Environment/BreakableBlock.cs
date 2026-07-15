using Godot;
using FourMasksGame.Scripts.Damage;

namespace FourMasksGame.Scripts.Environment;

public partial class BreakableBlock : Node2D
{
    [Export] private DamageReceiver damageReceiver;
    [Export] private float health = 2f;
    
    public override void _Process(double delta)
    {
        foreach (DamageSource source in damageReceiver.Receive(0))
        {
            health -= source.Damage;
        }

        if (health <= 0)
        {
            QueueFree();
        }
    }
}
