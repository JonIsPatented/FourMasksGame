using Godot;
using System.Collections.Generic;

namespace FourMasksGame.Scripts.Masks;

public partial class MaskManager : Node
{
    public static MaskManager Instance;

    public Mask CurrentMask { get; private set; }

    [Signal]
    public delegate void MaskChangedEventHandler(Mask mask);

    private readonly Dictionary<int, string> maskPaths = new()
    {
        {1, "res://Resources/Masks/wingMask.tres"},
        {2, "res://Resources/Masks/demonMask.tres"},
        {3, "res://Resources/Masks/mistMask.tres"},
        {4, "res://Resources/Masks/golemMask.tres"}
    };

    public MaskManager()
    {
        Instance = this;
    }

    public void ChangeMask(int mask)
    {
        CurrentMask = ResourceLoader.Load<Mask>(maskPaths[mask]);
        EmitSignal(SignalName.MaskChanged, CurrentMask);
    }
}
