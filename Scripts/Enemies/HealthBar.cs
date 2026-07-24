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

    private float lastDamageTime = -1000f;


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
        foreach (DamageSource source in damageSources)
        {
            source.OnDamage();
            
            if (FloorDamage)
            {
                //Health -= Mathf.Floor(source.Damage);
            }
            else
            {
                Health -= source.Damage;
            }
            
            lastDamageTime = Time.GetTicksMsec() / 1000f;
        }
    }

    public bool IsAlive()
    {
        return Health > 0f;
    }

    public float SinceLastDamage()
    {
        return (Time.GetTicksMsec() / 1000f) - lastDamageTime;
    }
}
