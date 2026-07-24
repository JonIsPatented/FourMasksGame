using Godot;
using System;
using FourMasksGame.Scripts.Masks;

namespace FourMasksGame;

public partial class Shutter : Background
{
    public static Shutter Instance;

    public Shutter()
    {
        Instance = this;
        MaskManager.Instance.MaskChanged += OnMaskChange;
        MaskManager.Instance.MaskStartChange += OnMaskStartChange;
    }

    public void OnMaskStartChange()
    {
        FadeOut();
    }

    public Tween FadeIn()
    {
        Tween fade = GetTree().CreateTween();
        fade.TweenProperty(this, "modulate", new Color(1f, 1f, 1f, 0f), 0.8f);
        return fade;
    }

    public void OnMaskChange(Mask mask)
    {
        FadeIn();
    }

    public Tween FadeOut()
    {
        Tween fade = GetTree().CreateTween();
        fade.TweenProperty(this, "modulate", new Color(1f, 1f, 1f, 1f), 0.8f);
        return fade;
    }
}
