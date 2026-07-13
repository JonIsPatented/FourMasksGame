using Godot;

namespace FourMasksGame.Scripts.Input;

public partial class InputContextSwitchNode : Node
{
    [Export(PropertyHint.Enum, "Normal,AimLock,Cutscene")]
    private int contextType;

    public override void _EnterTree()
    {
        switch(contextType)
        {
            case 0: InputManager.Instance.PushContext(new NormalInputContext()); break;
            case 1: InputManager.Instance.PushContext(new AimLockInputContext()); break;
            case 2: InputManager.Instance.PushContext(new CutsceneInputContext()); break;
        }
    }

    public override void _ExitTree()
    {
        InputManager.Instance.PopContext();
    }
}
