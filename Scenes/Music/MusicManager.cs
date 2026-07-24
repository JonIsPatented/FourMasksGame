using Godot;
using System;
using FourMasksGame.Scripts.Masks;

public partial class MusicManager : Node
{
    [Export] private AudioStreamPlayer demonMusic;
    [Export] private AudioStreamPlayer wingMusic;

    public override void _Ready()
    {
        MaskManager.Instance.MaskChanged += OnMaskChange;
        OnMaskChange(MaskManager.Instance.CurrentMask);
        MaskManager.Instance.MaskStartChange += FadeAll;
    }

    public void FadeAll()
    {
        Tween fadeOut = GetTree().CreateTween();
        fadeOut.SetParallel(true);
        fadeOut.TweenProperty(wingMusic, "volume_linear", 0f, 0.8f);
        fadeOut.TweenProperty(demonMusic, "volume_linear", 0f, 0.8f);
        fadeOut.TweenCallback(Callable.From(MuteAll));
    }

    private void OnMaskChange(Mask mask)
    {
        MuteAll();

        if (mask == null)
        {
            return;
        }

        if (mask.Name == "Wing")
        {
            Tween unmute = GetTree().CreateTween();
            unmute.TweenProperty(wingMusic, "volume_linear", 1f, 0.8f);
        }

        if (mask.Name == "Demon")
        {
            Tween unmute = GetTree().CreateTween();
            unmute.TweenProperty(demonMusic, "volume_linear", 1f, 0.8f);
        }
    }

    private void MuteAll()
    {
        demonMusic.VolumeDb = -80;
        wingMusic.VolumeDb = -80;
    }
}
