using Godot;
using System.Collections.Generic;

namespace FourMasksGame.Scripts.Masks;

public partial class MaskManager : Node
{
    public static MaskManager Instance;

    [Signal]
    public delegate void MaskChangedEventHandler(Mask mask);

    private readonly Dictionary<int, string> maskPaths = new()
    {
        {0, "res://Resources/Masks/wingMask.tres"},
        {1, "res://Resources/Masks/demonMask.tres"},
        {2, "res://Resources/Masks/mistMask.tres"},
        {3, "res://Resources/Masks/golemMask.tres"}
    };

    public MaskManager()
    {
        Instance = this;
    }

    public void ChangeMask(int mask)
    {
        EmitSignal(SignalName.MaskChanged, ResourceLoader.Load<Mask>(maskPaths[mask]));
    }
}
