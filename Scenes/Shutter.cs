using Godot;
using System;
using FourMasksGame.Scripts.Masks;

namespace FourMasksGame;

public partial class Shutter : Node2D
{
    public static Shutter Instance;

    private bool fadedOut = false;

    private Background background;

    public Shutter()
    {
        Instance = this;
    }

    public override void _EnterTree()
    {
        MaskManager.Instance.MaskChanged += OnMaskChange;
        MaskManager.Instance.MaskStartChange += OnMaskStartChange;
        GetTree().SceneChanged += CreateBackground;
    }

    public void CreateBackground()
    {
        background = new Background() { Material = Material };
        background.SetZIndex(10);
        GetTree().Root.AddChild(background);

        if (fadedOut)
        {
            FadeOut();
        }
        else
        {
            FadeIn();
        }
    }

    public void OnMaskStartChange()
    {
        FadeOut();
    }

    public Tween FadeIn()
    {
        if (background == null)
        {
            fadedOut = false;
            return null;
        }

        Tween fade = GetTree().CreateTween();
        fade.TweenProperty(background, "modulate", new Color(1f, 1f, 1f, 0f), 0.8f);
        return fade;
    }

    public void OnMaskChange(Mask mask)
    {
        FadeIn();
    }

    public Tween FadeOut()
    {
        if (background == null)
        {
            fadedOut = true;
            return null;
        }

        Tween fade = GetTree().CreateTween();
        fade.TweenProperty(background, "modulate", new Color(1f, 1f, 1f, 1f), 0.8f);
        return fade;
    }
}
