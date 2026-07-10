
namespace FourMasksGame.Scripts.Input;

public partial class NormalInputContext : InputContext
{
    bool InputContext.UseAny => true;
    bool InputContext.UseAim => false;
    bool InputContext.UseHorizontalAxis => true;
}
