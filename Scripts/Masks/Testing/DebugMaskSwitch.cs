using Godot;

namespace FourMasksGame.Scripts.Masks.Testing;

/// <summary>
/// Responds directly to mask change inputs by calling for a mask change.
/// </summary>
public partial class DebugMaskSwitch : Node
{
    public void _Process()
    {
        if (Input.IsActionJustPressed("DonMask1"))
        {
            MaskManager.Instance.ChangeMask(1);
        }
        if (Input.IsActionJustPressed("DonMask2"))
        {
            MaskManager.Instance.ChangeMask(2);
        }
        if (Input.IsActionJustPressed("DonMask3"))
        {
            MaskManager.Instance.ChangeMask(3);
        }
        if (Input.IsActionJustPressed("DonMask4"))
        {
            MaskManager.Instance.ChangeMask(4);
        }
    }
}
