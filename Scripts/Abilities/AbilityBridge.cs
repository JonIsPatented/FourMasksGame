using Godot;
using System.Collections.Generic;

namespace FourMasksGame.Scripts.Abilities;

public partial class AbilityBridge : GodotObject
{
    public Dictionary<int, PackedScene> AbilitySlots = new()
    {
        {1, ResourceLoader.Load<PackedScene>("res://Scenes/Abilities/WingMaskLightAttack.tscn")}
    };

    public AbilitySceneRoot ActiveAbility;
    private bool usingAbility = false;

    private AbilityInfo abilityInfo;
    private readonly AbilityTags tags;

    // GodotObject-derived classes need empty constructors.
    public AbilityBridge()
    {
        tags = new();
    }

    public void PassInfo(AbilityInfo info)
    {
        abilityInfo = info;
    }

    public void ClearTags()
    {
        tags.Clear();
    }

    /// <summary>
    /// Attempt to create an ability scene under the scene parent.
    /// 
    /// Will only succeed if there is not an ability under this bridge. Use ChainToAbility in that case.
    /// 
    /// Returns true if the ability was successfully used, false if not.
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    public bool UseAbility(Node sceneParent, int slot)
    {
        if (!AbilityAvailable(slot) || sceneParent == null || usingAbility)
        {
            return false;
        }

        Node sceneRoot = AbilitySlots[slot].Instantiate();
        AbilitySceneRoot abilitySceneRoot = (AbilitySceneRoot)sceneRoot;
        if (!abilitySceneRoot.CanUse(abilityInfo))
        {
            abilitySceneRoot.QueueFree();
            return false;
        }

        usingAbility = true;
        ActiveAbility = abilitySceneRoot;
        sceneParent.AddChild(abilitySceneRoot);
        return true;
    }

    /// <summary>
    /// Attempt to create a new ability scene under the scene parent, disposing of the previous ability scene if there is one.
    /// 
    /// Will only succeed if there is an ability under this bridge. Use UseAbility in that case.
    /// 
    /// Returns true if the ability chained succesfully, false otherwise.
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    public bool ChainToAbility(int slot)
    {
        if (!AbilityAvailable(slot) || !usingAbility)
        {
            return false;
        }

        Node sceneParent = ActiveAbility.GetParent();

        Node sceneRoot = AbilitySlots[slot].Instantiate();
        AbilitySceneRoot abilitySceneRoot = (AbilitySceneRoot)sceneRoot;
        if (!abilitySceneRoot.CanUse(abilityInfo))
        {
            abilitySceneRoot.QueueFree();
            return false;
        }

        EndAbility();
        usingAbility = true;
        ActiveAbility = abilitySceneRoot;
        sceneParent.AddChild(abilitySceneRoot);
        return true;
    }

    /// <summary>
    /// Query an ability scene to allow it to end or chain to a different ability.
    /// 
    /// Returns false if the ability ends without chaining to a new ability.
    /// 
    /// Returns true if the ability chains to a new ability or continues.
    /// </summary>
    /// <returns></returns>
    public bool ContinueAbility()
    {
        if (!usingAbility)
        {
            return false;
        }

        if (ActiveAbility.ShouldEnd(abilityInfo))
        {
            EndAbility();
            return false;
        }

        foreach (int slot in ActiveAbility.ChainsToSlots)
        {
            if (ActiveAbility.ShouldChainTo(abilityInfo, slot))
            {
                ChainToAbility(slot);
            }
        }

        return true;
    }

    /// <summary>
    /// Immediately end the active ability and remove it from the scene tree.
    /// </summary>
    public void EndAbility()
    {
        usingAbility = false;
        ActiveAbility.QueueFree();
        ActiveAbility = null;
    }

    public bool AbilityAvailable(int slot)
    {
        return AbilitySlots.ContainsKey(slot);
    }

    public bool UsingAbility()
    {
        return usingAbility;
    }

    public AbilityDirective GetDirective()
    {
        if (usingAbility)
        {
            return ActiveAbility.GetDirective(abilityInfo);
        }
        return new();
    }
}
