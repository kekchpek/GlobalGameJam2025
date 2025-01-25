using AsyncReactAwait.Bindable;
using AsyncReactAwait.Bindable.BindableExtensions;
using BubbleJump.Model.Player;
using BubbleJump.Model.SuperJump;
using UnityMVVM.ViewModelCore;

namespace BubbleJump.UI.SuperJump
{
    public class SuperJumpViewModel : ViewModel, ISuperJumpViewModel
    {
        public IBindable<bool> IndicatorShown { get; }
        public IBindable<bool> TutorialShown { get; }
        public IBindable<float> IndicatorPercent { get; }

        public SuperJumpViewModel(
            ISuperJumpModel superJumpModel,
            IPlayerModel playerModel)
        {
            IndicatorShown = superJumpModel.Strength.ConvertTo(x => x > 0f);
            TutorialShown = Bindable.Aggregate(playerModel.IsOnTheGround, playerModel.Enabled,
                (x, y) => x && y);
            IndicatorPercent =
                Bindable.Aggregate(
                    superJumpModel.Strength, superJumpModel.MaxStrength, 
                    (x, y) => x / y);
        }
        
    }
}