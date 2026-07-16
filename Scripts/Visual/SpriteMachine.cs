using Godot;
using FourMasksGame.Scripts.Masks;

namespace FourMasksGame.Scripts.Visual;

public partial class SpriteMachine : AnimatedSprite2D
{
    [Export] private SpriteFramesSet spriteFramesSet;

    public override void _EnterTree()
    {
        MaskManager.Instance.MaskChanged += SwapLibrary;
        SwapLibrary(MaskManager.Instance.CurrentMask);
    }

    public void SwapLibrary(Mask mask)
    {
        SpriteFrames = spriteFramesSet.GetSprites(mask?.Number);
    }

    public void SwitchAnimation(string animation)
    {
        if (SpriteFrames != null)
        {
            if (SpriteFrames.HasAnimation(animation))
            {
                SetAnimation(animation);
                Play();
            }
        }
    }
}
