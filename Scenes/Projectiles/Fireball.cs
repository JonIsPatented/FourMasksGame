using Godot;
using FourMasksGame.Scripts.Damage;
using System;

namespace FourMasksGame.Scenes.Projectiles;

public partial class Fireball : Node2D
{
    const float FIREBALL_SPEED = 200f;

    private bool isAlive = true;
    private float movement = 1f;

    [Export] DamageSender damageSender;
    [Export] AnimatedSprite2D sprite;
    [Export] Area2D groundCollider;

    public override void _Ready()
    {
        damageSender.Load(new FireballDamage(this));
        sprite.Play("default");
        groundCollider.BodyEntered += CollidedWithGround;
        groundCollider.AreaEntered += CollidedWithGround;
        GetTree().CreateTimer(10f).Timeout += Finish;
    }

    private void CollidedWithGround(Node2D body)
    {
        Finish();
    }

    public void Flip()
    {
        sprite.FlipH = !sprite.FlipH;
        movement = -movement;
    }


    public override void _PhysicsProcess(double delta)
    {
        Translate(new Vector2(movement * FIREBALL_SPEED * (float)delta, 0f).Rotated(Rotation));
    }

    public void Finish()
    {
        if (isAlive)
        {
            isAlive = false;
            sprite.Play("finish");
            sprite.AnimationFinished += QueueFree;
        }
    }

    public class FireballDamage : DamageSource
    {
        Fireball fireball;
        float DamageSource.Damage => 1f;
        int DamageSource.Team => 1;


        public FireballDamage(Fireball fireball)
        {
            this.fireball = fireball;
        }

        bool DamageSource.Filter => true;
        bool DamageSource.IsActive()
        {
            return fireball.isAlive;
        }

        bool DamageSource.IsComplete()
        {
            return !fireball.isAlive;
        }

        void DamageSource.OnDamage()
        {
            fireball.Finish();
        }
    }
}
