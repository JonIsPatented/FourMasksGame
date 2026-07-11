using Godot;
using System.Collections.Generic;

public partial class AbilityStateMachine : GodotObject
{
    public Dictionary<uint, PackedScene> AbilitySlots = new()
    {
        {0, ResourceLoader.Load<PackedScene>("res://Scenes/Abilities/WingMaskLightAttack.tscn")}
    };

    public AbilitySceneRoot ActiveAbility;
    public bool UsingAbility = false;

    // GodotObject-derived classes need empty constructors.
    public AbilityStateMachine() { }

    public void UseAbility(uint slot)
    {
        
    }

    public MovementDirective GetDirective()
    {
        return new();
    }

    public void EndAbility()
    {
        ActiveAbility = null;
    }
}
