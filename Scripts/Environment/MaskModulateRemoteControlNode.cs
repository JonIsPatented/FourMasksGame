using Godot;

using FourMasksGame.Scripts.Masks;

namespace FourMasksGame.Scripts.Environment;

/// <summary>
/// Responds to "mask changed" signals by adjusting the modulate value of itself and possibly additional targeted nodes.
/// </summary>
[GlobalClass]
public partial class MaskModulateRemoteControlNode : Node2D
{
    private static Color defaultModulate = new(1, 1, 1);

    /// <summary>
    /// Additional targets to adjust the modulate value for. This node's modulate value is always adjusted, note that children will be affected by this node's modulate value automatically.
    /// </summary>
    [Export] private CanvasItem[] additionalTargets = [];

    public override void _EnterTree()
    {
        MaskManager.Instance.MaskChanged += AdjustModulate;
        AdjustModulate(MaskManager.Instance.CurrentMask);
    }

    private void AdjustModulate(Mask mask)
    {
        if (mask == null)
        {
            ResetModulate();
            return;
        }

        foreach (CanvasItem modulateTarget in additionalTargets)
        {
            modulateTarget.Modulate = mask.EnvironmentColor;
        }
        Modulate = mask.EnvironmentColor;
    }

    private void ResetModulate()
    {
        foreach (CanvasItem modulateTarget in additionalTargets)
        {
            modulateTarget.Modulate = defaultModulate;
        }
        Modulate = defaultModulate;
    }

    public override void _ExitTree()
    {
        MaskManager.Instance.MaskChanged -= AdjustModulate;
        ResetModulate();
    }
}
