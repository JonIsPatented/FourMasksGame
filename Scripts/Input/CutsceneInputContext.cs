
namespace FourMasksGame.Scripts.Input;

public partial class CutsceneInputContext : InputContext
{
    bool InputContext.UseAny => false;
    bool InputContext.UseAim => false;
    bool InputContext.UseHorizontalAxis => false;
}
