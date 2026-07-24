using Godot;
using System;

using FourMasksGame.Scripts.Masks;

namespace FourMasksGame.Scripts.Masks;

public partial class MaskEntry : Node
{
    public override void _Ready()
    {
        MaskManager.Instance.FinishChangeMask();
    }
}
