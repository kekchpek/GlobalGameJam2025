using AsyncReactAwait.Bindable;
using UnityMVVM.ViewModelCore;

namespace BubbleJump.UI.Score
{
    public interface IScoreViewModel : IViewModel
    {
        
        IBindable<float> Score { get; }
        IBindable<float> Record { get; }

    }
}