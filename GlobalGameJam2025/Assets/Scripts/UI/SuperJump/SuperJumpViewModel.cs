using AsyncReactAwait.Bindable;
using AsyncReactAwait.Bindable.BindableExtensions;
using BubbleJump.Model.Player;
using BubbleJump.Model.SuperJump;
using UnityMVVM.ViewModelCore;

namespace BubbleJump.UI.SuperJump
{
    public class SuperJumpViewModel : ViewModel, ISuperJumpViewModel
    {
        private readonly IPlayerModel _playerModel;

        public IBindable<bool> IndicatorShown { get; }
        public IBindable<bool> TutorialShown => _playerModel.IsOnTheGround;
        public IBindable<float> IndicatorPercent { get; }

        public SuperJumpViewModel(
            ISuperJumpModel superJumpModel,
            IPlayerModel playerModel)
        {
            _playerModel = playerModel;
            IndicatorShown = superJumpModel.Strength.ConvertTo(x => x > 0f);
            IndicatorPercent =
                Bindable.Aggregate(
                    superJumpModel.Strength, superJumpModel.MaxStrength, 
                    (x, y) => x / y);
        }
        
    }
}