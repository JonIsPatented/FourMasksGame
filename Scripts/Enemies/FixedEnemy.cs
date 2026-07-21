using Godot;
using System;
using FourMasksGame.Scripts.Damage;
using FourMasksGame.Scripts.Visual;

namespace FourMasksGame.Scripts.Enemies;

public partial class FixedEnemy : Node2D
{
    HealthBar healthBar = new(4f, 4f);
    SpriteController spriteController = new();
    [Export] DamageReceiver damageReceiver;
    [Export] AnimatedSprite2D sprite;

    private float shootCooldown = SHOOT_COOLDOWN_TIME;
    private const float SHOOT_COOLDOWN_TIME = 4f;


    public override void _Ready()
    {
        healthBar.Start();
        spriteController.Bind(sprite);
    }

    public override void _Process(double delta)
    {
        if (!healthBar.IsAlive())
        {
            return;
        }

        HashSet<DamageSource> damage = damageReceiver.Receive(2);
        if (!damage.IsEmpty())
        {
            spriteController.Damage();
            healthBar.Damage(damage);

            if (!healthBar.IsAlive())
            {
                Die();
                return;
            }
        }

        if (healthBar.SinceLastDamage() > 2f)
        {
            shootCooldown -= (float)delta;
            if (shootCooldown < 0.0)
            {
                shootCooldown = SHOOT_COOLDOWN_TIME;

            }
        }
    }

    private void Die()
    {
        spriteController.Die();
        spriteController.DeathComplete += QueueFree();
    }
}
