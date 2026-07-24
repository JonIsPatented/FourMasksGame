using Godot;
using System;
using FourMasksGame.Scripts.Damage;

namespace FourMasksGame.Scenes.Environment;

public partial class BreakableBlock : StaticBody2D
{
    [Export] DamageReceiver damageReceiver;
    [Export] CpuParticles2D particles;
    [Export] AudioStreamPlayer2D audio;
    [Export] Node2D sprite;

    bool broken = false;

    public override void _Ready()
    {
        damageReceiver.AreaEntered += CheckForFireball;
    }

    private void CheckForFireball(Area2D area)
    {
        if (broken)
        {
            return;
        }

        if (damageReceiver.Receive(0).Count > 0)
        {
            sprite.Visible = false;
            particles.Emitting = true;
            audio.Play();
            CollisionLayer = 0;
            damageReceiver.AreaEntered -= CheckForFireball;
            broken = true;
        }
    }
}
