using Godot;

namespace FourMasksGame.Scripts.Masks;

public partial class Mask : Resource
{
    [Export] public string Name;
    [Export] public Texture2D OverlayTexture;
    [Export] public Color EnvironmentColor;
    [Export] public SpriteFrames PlayerSprites;

    [ExportGroup("Abilities")]
    [Export] public PackedScene Jump;
    [Export] public PackedScene LightAttack;
    [Export] public PackedScene ChargeAttack;
    [Export] public PackedScene SpecialAbility;
    [Export] public PackedScene Dash;
}
