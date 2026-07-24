using Godot;
using System;
using FourMasksGame.Scripts.Damage;

namespace FourMasksGame.Scenes.Environment;

public partial class Hazard : Node2D
{
    [Export] DamageSender sender;

    public override void _Ready()
    {
        sender.Load(new DamageSource.Hazard(this, 1f, 0));
    }
}
