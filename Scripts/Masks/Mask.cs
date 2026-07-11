using Godot;

namespace FourMasksGame.Scripts.Masks;

public partial class Mask : Resource
{
    [Export] public ImageTexture OverlayTexture;
    [Export] public Color EnvironmentColor;

    [ExportGroup("Ability Slots")]
    [Export] public PackedScene AbilitySlot1;
    [Export] public PackedScene AbilitySlot2;
    [Export] public PackedScene AbilitySlot3;
    [Export] public PackedScene AbilitySlot4;

    [ExportGroup("Player Sprites")]
    [Export] public SpriteFrames IdleSpriteFrames;
    [Export] public SpriteFrames RunSpriteFrames;
    [Export] public SpriteFrames JumpSpriteFrames;
    [Export] public SpriteFrames FallingSpriteFrames;
}
