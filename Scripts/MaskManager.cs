using Godot;

namespace FourMasksGame.Scripts;

public partial class MaskManager : Node
{
    public static MaskManager Instance;

    [Signal]
    public delegate void MaskChangedEventHandler();

    public override void _Ready()
    {
        Instance = this;
    }

    public void MaskChanged()
    {
        EmitSignal(SignalName.MaskChanged);
    }
}
