using Godot;

namespace FourMasksGame.Scripts.Abilities.SceneRoots;

public partial class WingMaskLightAttack : AbilitySceneRoot
{
    [Export] AnimatedSprite2D animation;
    bool shouldExit = false;

    public override void _EnterTree()
    {
        animation.AnimationFinished += ReadyToExit;
    }

    public void ReadyToExit()
    {
        shouldExit = true;
    }

    public override bool CanUse(AbilityInfo info)
    {
        return true;
    }

    public override bool ShouldEnd(AbilityInfo info)
    {
        return shouldExit;
    }
}
