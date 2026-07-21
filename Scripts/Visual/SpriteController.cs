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

    public void Bind(AnimatedSprite2D sprite, SpriteFramesSet animations)
    {
        this.sprite = sprite;
        this.animations = animations;
    }

    private SpriteFrames MaskFrames()
    {
        if (animations == null)
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
        if (frames == null)
        {
            return false;
        }

        if (!frames.HasAnimation(animationName))
        {
            return false;
        }

        return true;
    }

    public void Do(string animationName)
    {
        
    }

    public void Damage()
    {
        
    }

    public void Die()
    {
        
    }

    public void HandleMaskChange(Mask mask)
    {
        
    }
}
