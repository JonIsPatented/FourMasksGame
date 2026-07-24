using Godot;
using FourMasksGame.Scripts.Masks;

namespace FourMasksGame.Scenes;

public partial class Menu : Node2D
{
    [Export] private Button wingStartButton;
    [Export] private Button demonStartButton;

    public override void _Ready()
    {
        wingStartButton.ButtonUp += StartWing;
        demonStartButton.ButtonUp += StartDemon;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustReleased("DonMask1"))
        {
            StartWing();
        }
        if (Input.IsActionJustPressed("DonMask2"))
        {
            StartDemon();
        }
    }

    private void StartWing()
    {
        MaskManager.Instance.QuietlyChangeMask(1);
        Start();
    }

    private void StartDemon()
    {
        MaskManager.Instance.QuietlyChangeMask(2);
        Start();
    }

    private void Start()
    {
        GetTree().CallDeferred("change_scene_to_file", "res://Scenes/demo_level.tscn");
    }
}
