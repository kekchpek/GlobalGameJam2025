using AsyncReactAwait.Bindable;
using UnityMVVM.ViewModelCore;

namespace BubbleJump.UI.SuperJump
{
    public interface ISuperJumpViewModel : IViewModel
    {
        IBindable<bool> IndicatorShown { get; }
        IBindable<bool> TutorialShown { get; }
        IBindable<float> IndicatorPercent { get; }
    }
}