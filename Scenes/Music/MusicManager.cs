using Godot;
using System;
using FourMasksGame.Scripts.Masks;

public partial class MusicManager : Node
{
    private bool playing = false;
    private AudioStreamPlayer demonMusic;
    private AudioStreamPlayer wingMusic;

    public override void _Ready()
    {
        MaskManager.Instance.MaskChanged += OnMaskChange;
        OnMaskChange(MaskManager.Instance.CurrentMask);
    }

    private void OnMaskChange(Mask mask)
    {
        if (mask == null)
        {
            playing = false;
            demonMusic.Stop();
            wingMusic.Stop();
            return;
        }

        if (mask.Name == "Demon")
        {
            if (!playing)
            {
                demonMusic.Play();
                wingMusic.Play();
                playing = true;
            }

            AudioServer.SetBusMute(1, true);
            AudioServer.SetBusMute(2, false);
        }
    }
}
