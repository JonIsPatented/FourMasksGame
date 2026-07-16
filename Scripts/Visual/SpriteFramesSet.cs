using Godot;

namespace FourMasksGame.Scripts.Visual;

public partial class SpriteFramesSet : Resource
{
    [Export] public SpriteFrames wingSprites;
    [Export] public SpriteFrames demonSprites;
    [Export] public SpriteFrames mistSprites;
    [Export] public SpriteFrames golemSprites;

    public SpriteFrames GetSprites(int? maskNumber)
    {
        return maskNumber switch
        {
            1 => wingSprites,
            2 => demonSprites,
            3 => mistSprites,
            4 => golemSprites,
            _ => null,
        };
    }
}
