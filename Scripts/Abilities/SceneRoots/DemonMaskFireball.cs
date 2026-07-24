using Godot;
using FourMasksGame.Scenes.Projectiles;
using FourMasksGame.Scripts.Input;

namespace FourMasksGame.Scripts.Abilities.SceneRoots;

public partial class DemonMaskFireball : AbilitySceneRoot
{
    [Export] AnimatedSprite2D playerAnimation;
    [Export] Fireball fireball;

    public override void _Ready()
    {
        bool flip = InputManager.Instance.GetLastHorizontalAxis() < 0f;
        playerAnimation.FlipH = flip;
        if (flip)
        {
            fireball.Flip();
            Vector2 _p = fireball.Position;
            _p.X = -_p.X;
            fireball.Position = _p;
        }
        Vector2 _g = fireball.GlobalPosition;
        RemoveChild(fireball);
        GetParent().GetParent().AddChild(fireball);
        fireball.GlobalPosition = _g;
    }

    public override AbilityDirective GetDirective(AbilityInfo info)
    {
        return new()
        {
            fixY = false,
        };
    }

    public override bool CanUse(AbilityInfo info)
    {
        return true;
    }

    public override bool ShouldEnd(AbilityInfo info)
    {
        return !playerAnimation.IsPlaying();
    }
}
