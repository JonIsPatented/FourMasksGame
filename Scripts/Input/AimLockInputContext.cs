
namespace FourMasksGame.Scripts.Input;

public partial class AimLockInputContext : InputContext
{
    bool InputContext.UseAny => true;
    bool InputContext.UseAim => true;
    bool InputContext.UseHorizontalAxis => false;
}
