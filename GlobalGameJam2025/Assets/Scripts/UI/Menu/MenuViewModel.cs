using AsyncReactAwait.Bindable;
using AsyncReactAwait.Bindable.BindableExtensions;
using BubbleJump.Model.Player;
using UnityMVVM.ViewModelCore;

namespace BubbleJump.UI.Menu
{
    public class MenuViewModel : ViewModel, IMenuViewModel
    {
        private readonly IPlayerModel _playerModel;
        private readonly IPlayerService _playerService;


        public IBindable<bool> LoseLayoutShown => _playerModel.IsDead;
        public IBindable<bool> StartLayoutShown { get; }
        public IBindable<float> LoseRecord => _playerModel.Record;

        public MenuViewModel(
            IPlayerModel playerModel,
            IPlayerService playerService)
        {
            _playerModel = playerModel;
            _playerService = playerService;
            StartLayoutShown = _playerModel.Enabled.ConvertTo(x => !x);
        }
        
        public void OnSpaceClicked()
        {
            if (!_playerModel.Enabled.Value)
            {
                _playerService.Enable();
            }
            if (_playerModel.IsDead.Value)
            {
                _playerService.Respawn();
            }
        }
    }
}