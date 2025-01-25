using AsyncReactAwait.Bindable;
using UnityMVVM.ViewModelCore;

namespace BubbleJump.UI.Menu
{
    public interface IMenuViewModel : IViewModel
    {
        
        IBindable<bool> LoseLayoutShown { get; }
        
        IBindable<bool> StartLayoutShown { get; }
        
        IBindable<float> LoseHeight { get; }

        void OnSpaceClicked();

    }
}