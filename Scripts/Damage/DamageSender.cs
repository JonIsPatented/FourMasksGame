using Godot;
using System.Collections.Generic;

namespace FourMasksGame.Scripts.Damage;

[GlobalClass]
public partial class DamageSender : Area2D
{
    private HashSet<DamageSource> loadedSources = [];

    /// <summary>
    /// Make a damage source accessible from this sender.
    /// 
    /// Clears completed sources after adding.
    /// </summary>
    /// <param name="source"></param>
    public void Load(DamageSource source)
    {
        loadedSources.Add(source);
        ClearComplete();
    }

    /// <summary>
    /// Gets all active and incomplete damage sources. Called by receiver.
    /// 
    /// Clears completed sources before polling.
    /// </summary>
    public HashSet<DamageSource> Poll()
    {
        ClearComplete();
        HashSet<DamageSource> activeSources = [];
        foreach(DamageSource source in loadedSources)
        {
            if (source.IsActive() && !source.IsComplete())
            {
                activeSources.Add(source);
            }
        }
        return activeSources;
    }

    /// <summary>
    /// Removes all completed damage sources from loaded sources.
    /// </summary>
    private void ClearComplete()
    {
        loadedSources.RemoveWhere((s) => s.IsComplete());
    }
}
