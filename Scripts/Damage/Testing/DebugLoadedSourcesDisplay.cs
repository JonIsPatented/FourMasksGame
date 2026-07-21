using Godot;

namespace FourMasksGame.Scripts.Damage.Testing;

public partial class DebugLoadedSourcesDisplay : Label
{
    [Export] DamageSender sender;

    public override void _Process(double delta)
    {
        Text = sender.LoadedSources.Count.ToString();
    }
}
