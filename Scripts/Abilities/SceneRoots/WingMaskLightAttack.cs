using Godot;
using FourMasksGame.Scripts.Input;

namespace FourMasksGame.Scripts.Abilities.SceneRoots;

public partial class WingMaskLightAttack : AbilitySceneRoot
{
    [Export] AnimatedSprite2D animation;
    bool shouldExit = false;

    public override void _EnterTree()
    {
        animation.AnimationFinished += ReadyToExit;
    }

    public override void _Ready()
    {
        animation.Play();
        animation.FlipH = InputManager.Instance.GetLastHorizontalAxis() < 0f;
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
