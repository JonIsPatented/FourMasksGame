using Godot;

namespace FourMasksGame.Scripts.Masks.Testing;

public partial class DebugMaskDisplay : Label
{
    public override void _EnterTree()
    {
        MaskManager.Instance.MaskChanged += ModifyText;
        ModifyText(MaskManager.Instance.CurrentMask);
    }

    private void ModifyText(Mask mask)
    {
        if (mask == null)
        {
            Text = "No mask";
            return;
        }
        Text = mask.Name;
    }

    public override void _ExitTree()
    {
        MaskManager.Instance.MaskChanged -= ModifyText;
    }
}
