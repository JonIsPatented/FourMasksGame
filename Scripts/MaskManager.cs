using Godot;
using System.Collections.Generic;

namespace FourMasksGame.Scripts.Masks;

public partial class MaskManager : Node
{
    public static MaskManager Instance;

    [Signal]
    public delegate void MaskChangedEventHandler(Mask mask);

    private readonly Dictionary<int, Mask> masks = new()
    {
        {0, ResourceLoader.Load<Mask>("res://Resources/Masks/wingMask.tres")},
        {1, ResourceLoader.Load<Mask>("res://Resources/Masks/demonMask.tres")},
        {2, ResourceLoader.Load<Mask>("res://Resources/Masks/mistMask.tres")},
        {3, ResourceLoader.Load<Mask>("res://Resources/Masks/golemMask.tres")}
    };

    public MaskManager()
    {
        Instance = this;
    }

    public void ChangeMask(int mask)
    {
        EmitSignal(SignalName.MaskChanged, masks[mask]);
    }
}
