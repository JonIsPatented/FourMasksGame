using Godot;

namespace FourMasksGame.Scripts.Masks.Testing;

/// <summary>
/// Responds directly to mask change inputs by calling for a mask change.
/// </summary>
public partial class DebugMaskSwitch : Node
{
    public void _Process()
    {
        if (Input.IsActionJustPressed("SwitchMask1"))
        {
            MaskManager.Instance.ChangeMask(1);
        }
        if (Input.IsActionJustPressed("SwitchMask2"))
        {
            MaskManager.Instance.ChangeMask(2);
        }
        if (Input.IsActionJustPressed("SwitchMask3"))
        {
            MaskManager.Instance.ChangeMask(3);
        }
        if (Input.IsActionJustPressed("SwitchMask4"))
        {
            MaskManager.Instance.ChangeMask(4);
        }
    }
}
