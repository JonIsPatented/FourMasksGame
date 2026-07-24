using Godot;
using System.Collections.Generic;

namespace FourMasksGame.Scripts.Masks;

public partial class MaskManager : Node
{
    public static MaskManager Instance;

    public Mask CurrentMask { get; private set; }
    private int futureMask;

    [Signal]
    public delegate void MaskChangedEventHandler(Mask mask);

    [Signal] public delegate void MaskStartChangeEventHandler();

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
        futureMask = mask;
        GetTree().CreateTimer(0.8f).Timeout += FinishChangeMask;
        EmitSignal(SignalName.MaskStartChange);
    }

    public void FinishChangeMask()
    {
        int mask = futureMask;
        CurrentMask = ResourceLoader.Load<Mask>(maskPaths[mask]);
        RenderingServer.GlobalShaderParameterSet("demon", mask == 2);
        EmitSignal(SignalName.MaskChanged, CurrentMask);
    }
}
