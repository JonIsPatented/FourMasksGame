using Godot;
using System.Collections.Generic;

namespace FourMasksGame.Scripts.Damage;

[GlobalClass]
public partial class DamageSender : Area2D
{
    private readonly HashSet<DamageSource> loadedSources = [];
    public HashSet<DamageSource> LoadedSources { get
        {
            loadedSources.RemoveWhere((s) => s.IsComplete());
            return loadedSources;
        }
    }

    /// <summary>
    /// Make a damage source accessible from this sender.
    /// 
    /// Clears completed sources after adding.
    /// </summary>
    /// <param name="source"></param>
    public void Load(DamageSource source)
    {
        LoadedSources.Add(source);
    }

    /// <summary>
    /// Gets all active and incomplete damage sources. Called by receiver.
    /// 
    /// Clears completed sources before polling.
    /// </summary>
    public HashSet<DamageSource> Poll()
    {
        HashSet<DamageSource> activeSources = [];
        foreach(DamageSource source in LoadedSources)
        {
            if (source.IsActive() && !source.IsComplete())
            {
                activeSources.Add(source);
            }
        }
        return activeSources;
    }
}
