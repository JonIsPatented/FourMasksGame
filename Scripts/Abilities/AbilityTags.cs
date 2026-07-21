using System.Collections.Generic;
using Godot;

namespace FourMasksGame.Scripts.Abilities;

public class AbilityTags
{
    private readonly Dictionary<string, HashSet<string>> clearOnGlobalEvent;
    private readonly HashSet<string> tags;

    public AbilityTags()
    {
        
    }

    /// <summary>
    /// Get the full set of tags.
    /// </summary>
    /// <returns></returns>
    public HashSet<string> GetTags()
    {
        return tags;
    }

    /// <summary>
    /// Reset all state in the ability tags instance to the default values.
    /// </summary>
    public void Clear()
    {
        tags.Clear();
        clearOnGlobalEvent.Clear();
    }

    public void OnGlobalEvent(string eventName, params Variant[] args)
    {
        if (clearOnGlobalEvent.ContainsKey(eventName))
        {
            HashSet<string> tagsToClear = clearOnGlobalEvent[eventName];
            tags.ExceptWith(tagsToClear);
        }
    }

    public static AbilityTagCommands Commands()
    {
        return new AbilityTagCommands();
    }

    public void ApplyCommands(AbilityTagCommands commands)
    {
        foreach(string tag in commands.addTags)
        {
            tags.Add(tag);
        }

        foreach((string eventName, HashSet<string> tags) in commands.clearOnGlobalEvent)
        {
            if (!clearOnGlobalEvent.ContainsKey(eventName))
            {
                clearOnGlobalEvent[eventName] = [];
            }
            
            clearOnGlobalEvent[eventName].UnionWith(tags);
        }
    }

    public class AbilityTagCommands
    {
        public readonly Dictionary<string, HashSet<string>> clearOnGlobalEvent;
        public readonly HashSet<string> addTags;

        public AbilityTagCommands()
        {
            clearOnGlobalEvent = [];
            addTags = [];
        }

        /// <summary>
        /// Enter command to insert a tag with a specific clear condition. The condition parameters change given the clear condition selected.
        /// 
        /// If the clear condition is UntilGlobalEvent, the only parameter is the string name of the event.
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="clearCondition"></param>
        /// <param name="conditionParams"></param>
        public void AttachTag(string tag, ClearCondition clearCondition, params Variant[] conditionParams)
        {
            if (clearCondition == ClearCondition.UntilGlobalEvent)
            {
                string eventName = (string)conditionParams[0];
                if (!clearOnGlobalEvent.ContainsKey(eventName))
                {
                    clearOnGlobalEvent.Add(eventName, []);
                }

                clearOnGlobalEvent[eventName].Add(tag);
                addTags.Add(tag);
            }
        }

        public enum ClearCondition
        {
            UntilGlobalEvent,
        }
    }
}
