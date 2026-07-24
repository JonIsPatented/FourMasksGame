using Godot;
using System;

namespace FourMasksGame;

public partial class SoundEffects : Node
{
    public static SoundEffects Instance;

    public SoundEffects()
    {
        Instance = this;
    }

    public AudioStream Jump = ResourceLoader.Load<AudioStream>("res://assets/Sounds/Jump.ogg");
    public AudioStream Step = ResourceLoader.Load<AudioStream>("res://assets/Sounds/Step.ogg");
    public AudioStream Land = ResourceLoader.Load<AudioStream>("res://assets/Sounds/Land.ogg");
}
