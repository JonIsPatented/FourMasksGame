using Godot;

namespace FourMasksGame.Scripts.Abilities;

[GlobalClass]
public partial class AbilitySceneRoot : Node2D
{
    public virtual bool CanUse(AbilityInfo info)
    {
        return false;
    }

    public virtual void AddTags(AbilityInfo info, ref AbilityTags.AbilityTagCommands tags)
    {
        
    }

    public virtual AbilityDirective GetDirective(AbilityInfo info)
    {
        return new();
    }

    public virtual bool ShouldEnd(AbilityInfo info)
    {
        return true;
    }

    public virtual int[] ChainsToSlots => [];

    public virtual bool ShouldChainTo(AbilityInfo info, int otherAbility)
    {
        return false;
    }
}
