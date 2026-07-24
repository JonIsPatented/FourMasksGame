
namespace FourMasksGame.Scripts.Damage;

public interface DamageSource
{
    /// <summary>
    /// Whether this damage source should be added to a receiver's filter set when received.
    /// 
    /// Should be false for persistent damage sources such as hazards.
    /// </summary>
    bool Filter { get; }

    /// <summary>
    /// The amount of damage that this damage source should apply.
    /// </summary>
    float Damage { get; }

    /// <summary>
    /// The team value for this damage source. Team value is used to filter attacks that should not affect particular damage consumers.
    /// </summary>
    int Team { get; }

    /// <summary>
    /// Returns true if this damage source should be distributed in a poll to a damage sender.
    /// 
    /// If isComplete returns true, the damage source will not be distributed regardless of the return value of this function.
    /// </summary>
    /// <returns></returns>
    bool IsActive();

    /// <summary>
    /// Returns true if this damage source is okay to be removed from a damage sender's sources set.
    /// 
    /// If this function returns true, the damage source will not be distributed.
    /// </summary>
    /// <returns></returns>
    bool IsComplete();

    void OnDamage() { }

    public class Attack : DamageSource
    {
        public float Damage { get; }
        public int Team { get; }
        private float startTime;
        private float endTime;
        
        public Attack(float delay, float lifetime, float damage, int team)
        {
            float now = Godot.Time.GetTicksMsec() / 1000f;
            startTime = now + delay;
            endTime = now + delay + lifetime;
            Damage = damage;
            Team = team;
        }

        float DamageSource.Damage => Damage;
        bool DamageSource.Filter => true;
        bool DamageSource.IsActive()
        {
            float now = Godot.Time.GetTicksMsec() / 1000f;
            return now >= startTime && now < endTime;
        }

        bool DamageSource.IsComplete()
        {
            float now = Godot.Time.GetTicksMsec() / 1000f;
            return now >= endTime;
        }
    }
}
