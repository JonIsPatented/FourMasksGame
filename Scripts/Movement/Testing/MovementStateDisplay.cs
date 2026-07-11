using Godot;
using System;

namespace FourMasksGame.Scripts.Movement.Testing;

public partial class MovementStateDisplay : Label
{
    [Export] private Player player;

    public override void _Process(double delta)
    {
        if (player == null)
        {
            Text = "";
            return;
        }

        switch(player.StateMachine.GetLabel())
        {
            case MovementStateLabel.None:
                Text = "None";
                return;
            case MovementStateLabel.Idle:
                Text = "Idle";
                return;
            case MovementStateLabel.Falling:
                Text = "Falling";
                return;
            case MovementStateLabel.Jump:
                Text = "Jump";
                return;
            case MovementStateLabel.Run:
                Text = "Run";
                return;
        }
    }
}
