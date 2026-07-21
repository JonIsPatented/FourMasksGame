using Godot;
using System.Collections.Generic;
using FourMasksGame.Scripts.Damage;

namespace FourMasksGame.Scripts.Enemies;

public partial class HealthBar : GodotObject
{
    public float Health { get; set; } = 0;

    public float StartingHealth { get; set; } = 0;
    public float MaxHealth { get; set; } = 0;
    public bool FloorDamage { get; set; } = false;


    public HealthBar() {
        
    }

    public HealthBar(float startingHealth, float maxHealth, bool floorDamage = false)
    {
        StartingHealth = startingHealth;
        MaxHealth = maxHealth;
        FloorDamage = floorDamage;
    }

    public void Start()
    {
        Health = StartingHealth;
    }

    public void Damage(HashSet<DamageSource> damageSources)
    {
        for (DamageSource source in damageSources)
        {
            if (FloorDamage)
            {
                Health -= Mathf.Floor(source.damage);
            }
            else
            {
                Health -= source.damage;
            }
        }
    }

    public bool IsAlive()
    {
        return Health > 0f;
    }
}
