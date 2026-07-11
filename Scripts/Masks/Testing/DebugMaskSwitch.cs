using Godot;

namespace FourMasksGame.Scripts.Masks.Testing;

/// <summary>
/// Responds directly to mask change inputs by calling for a mask change.
/// </summary>
public partial class DebugMaskSwitch : Node
{
    public override void _Input(InputEvent @event) {
        if (@event.IsActionPressed("DonMask1"))
        {
            MaskManager.Instance.ChangeMask(1);
        }
        if (@event.IsActionPressed("DonMask2"))
        {
            MaskManager.Instance.ChangeMask(2);
        }
        if (@event.IsActionPressed("DonMask3"))
        {
            MaskManager.Instance.ChangeMask(3);
        }
        if (@event.IsActionPressed("DonMask4"))
        {
            MaskManager.Instance.ChangeMask(4);
        }
    }
}
