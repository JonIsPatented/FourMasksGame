using Godot;

namespace FourMasksGame.Scripts.Input.Testing;

public partial class InputTestControl : Node2D
{
    public override void _Ready()
    {
        InputManager.Instance.PushContext(new NormalInputContext());
    }

    public override void _Process(double delta)
    {
        if (Godot.Input.IsKeyPressed(Key.Key1))
        {
            InputManager.Instance.PushContext(new NormalInputContext());
        }
        if (Godot.Input.IsKeyPressed(Key.Key2))
        {
            InputManager.Instance.PushContext(new AimLockInputContext());
        }
        if (Godot.Input.IsKeyPressed(Key.Key3))
        {
            InputManager.Instance.PushContext(new CutsceneInputContext());
        }
        if (Godot.Input.IsKeyPressed(Key.Key4))
        {
            InputManager.Instance.PopContext();
        }
    }
}
