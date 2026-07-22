using Godot;
using System;
using FourMasksGame.Scripts.Masks;

namespace FourMasksGame.Scripts.Visual;

public partial class SpriteController : GodotObject
{
    public SpriteController()
    {
        MaskManager.Instance.MaskChanged += HandleMaskChange;
    }

    private AnimatedSprite2D sprite;
    private SpriteFramesSet animations;
    private string currentAnimation = null;

    public void Bind(AnimatedSprite2D sprite, SpriteFramesSet animations)
    {
        this.sprite = sprite;
        this.animations = animations;
        SyncSprite();
    }

    public void Do(string animationName)
    {
        currentAnimation = animationName;
        SyncSprite();
    }

    public void Stop()
    {
        currentAnimation = null;
        SyncSprite();
    }

    private void SyncSprite()
    {
        if (sprite == null) return;

        if (!HasAnimation(currentAnimation) || currentAnimation == null)
        {
            sprite.Stop();
            sprite.Visible = false;
            return;
        }

        if (sprite.Animation == currentAnimation && sprite.IsPlaying() && sprite.Visible)
        {
            return;
        }

        sprite.Animation = currentAnimation;
        sprite.SpriteFrames = MaskFrames();
        sprite.Play();
        sprite.Visible = true;
    }

    private SpriteFrames MaskFrames()
    {
        if (animations == null || MaskManager.Instance.CurrentMask == null)
        {
            return null;
        }

        string maskName = MaskManager.Instance.CurrentMask.Name;

        if (maskName == "Wing")
        {
            return animations.wingSprites;
        }

        if (maskName == "Demon")
        {
            return animations.demonSprites;
        }

        return null;
    }

    private bool HasAnimation(string animationName)
    {
        SpriteFrames frames = MaskFrames();
        if (frames == null || animationName == null)
        {
            return false;
        }

        if (!frames.HasAnimation(animationName))
        {
            return false;
        }

        return true;
    }

    public void Damage()
    {
        Tween tween = sprite.CreateTween();
        tween.SetParallel(true);
        tween.TweenProperty(sprite, "modulate", new Color(0.8f, 0, 0), 0.1f);
        tween.TweenProperty(sprite, "scale", new Vector2(1.1f, 0.9f), 0.1f);
        Tween back = tween.Chain();
        back.TweenProperty(sprite, "modulate", new Color(1f, 1f, 1f), 0.1f);
        back.TweenProperty(sprite, "scale", new Vector2(1f, 1f), 0.1f);
        tween.Play();
    }

    [Signal] public delegate void DeathEndEventHandler();

    public void Die()
    {
        Tween tween = sprite.CreateTween();
        tween.SetParallel(true);
        tween.TweenProperty(sprite, "modulate", new Color(0.8f, 0, 0), 0.2f);
        tween.TweenProperty(sprite, "scale", new Vector2(1.5f, 0f), 0.2f);
        Tween back = tween.Chain();
        back.TweenCallback(Callable.From(() => EmitSignal(SignalName.DeathEnd)));
    }

    public void HandleMaskChange(Mask mask)
    {
        SyncSprite();
    }
}
