using Godot;
using System.Collections.Generic;

namespace FourMasksGame.Scripts.Damage;

[GlobalClass]
public partial class DamageReceiver : Area2D
{
    private HashSet<DamageSource> filteredSources = [];

    /// <summary>
    /// Collect all damage sources from different teams sent by overlapping damage senders that have not been previously received.
    /// </summary>
    /// <param name="team"></param>
    /// <returns></returns>
    public HashSet<DamageSource> Receive(int team)
    {
        HashSet<DamageSource> receivedSources = [];
        foreach (Area2D area in GetOverlappingAreas())
        {
            if (area is DamageSender sender)
            {
                receivedSources.UnionWith(sender.Poll());
            }
        }
        Filter(receivedSources, team);
        UpdateFilter(receivedSources);
        return receivedSources;
    }

    /// <summary>
    /// Remove in-place all damage sources in the filter and of the same team.
    /// </summary>
    /// <param name="polledSources"></param>
    /// <param name="team"></param>
    private void Filter(HashSet<DamageSource> polledSources, int team)
    {
        polledSources.RemoveWhere(filteredSources.Contains);
        polledSources.RemoveWhere((s) => team == s.Team);
    }

    /// <summary>
    /// Add all damage sources that should be filtered to the filter.
    /// </summary>
    /// <param name="receivedSources"></param>
    private void UpdateFilter(HashSet<DamageSource> receivedSources)
    {
        HashSet<DamageSource> sourcesToFilter = [.. receivedSources];
        sourcesToFilter.RemoveWhere((s) => !s.Filter);
        filteredSources.UnionWith(sourcesToFilter);
        ClearFilteredCompleted();
    }

    /// <summary>
    /// Remove completed sources from the filter.
    /// </summary>
    private void ClearFilteredCompleted()
    {
        filteredSources.RemoveWhere((s) => s.IsComplete());
    }
}
