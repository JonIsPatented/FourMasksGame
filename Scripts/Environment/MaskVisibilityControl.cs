using Godot;

using FourMasksGame.Scripts.Masks;

namespace FourMasksGame.Scripts.Environment;

/// <summary>
/// Responds to "mask changed" signals by adjusting the visibility value of itself and possibly additional targeted nodes.
/// </summary>
[GlobalClass]
public partial class MaskVisibilityControl : Node2D
{
    /// <summary>
    /// Additional targets to adjust the modulate value for. This node's modulate value is always adjusted, note that children will be affected by this node's modulate value automatically.
    /// </summary>
    [Export] private CanvasItem[] additionalTargets = [];
    [Export] private string maskName;

    public override void _EnterTree()
    {
        MaskManager.Instance.MaskChanged += AdjustVisible;
        AdjustVisible(MaskManager.Instance.CurrentMask);
    }

    private void AdjustVisible(Mask mask)
    {
        if (mask == null)
        {
            ResetVisible();
            return;
        }

        bool shouldShow = mask.Name == maskName;

        foreach (CanvasItem visibilityTarget in additionalTargets)
        {
            visibilityTarget.Visible = shouldShow;
        }
        Visible = shouldShow;
    }

    private void ResetVisible()
    {
        foreach (CanvasItem modulateTarget in additionalTargets)
        {
            modulateTarget.Visible = false;
        }
        Visible = false;
    }

    public override void _ExitTree()
    {
        MaskManager.Instance.MaskChanged -= AdjustVisible;
        ResetVisible();
    }
}
