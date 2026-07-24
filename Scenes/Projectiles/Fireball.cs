using Godot;
using FourMasksGame.Scripts.Damage;
using System;
using FourMasksGame.Scripts.Masks;

namespace FourMasksGame.Scenes.Projectiles;

public partial class Fireball : CharacterBody2D
{
    const float FIREBALL_SPEED = 200f;

    private bool isAlive = true;
    private float movement = 1f;

    [Export] DamageSender damageSender;
    [Export] AnimatedSprite2D sprite;
    [Export] AudioStreamPlayer2D deathSound;

    public override void _Ready()
    {
        damageSender.Load(new FireballDamage(this));
        sprite.Play("default");
        Velocity = new Vector2(movement * FIREBALL_SPEED, 0f).Rotated(Rotation);
        MaskManager.Instance.MaskStartChange += Finish;
        deathSound.Finished += QueueFree;
    }

    public void Flip()
    {
        sprite.FlipH = !sprite.FlipH;
        movement = -movement;
        Velocity = new Vector2(movement * FIREBALL_SPEED, 0f).Rotated(Rotation);
    }

    public override void _PhysicsProcess(double delta)
    {
        KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);
        if (collision != null)
        {
            deathSound.Play();
            Finish();
        }
    }

    public void Finish()
    {
        if (!IsInstanceValid(this))
        {
            return;
        }

        SetPhysicsProcess(false);
        sprite.Visible = false;
        if (!deathSound.Playing)
        {
            QueueFree();
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
    }
}
