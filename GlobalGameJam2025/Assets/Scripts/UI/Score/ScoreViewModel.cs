using AsyncReactAwait.Bindable;
using BubbleJump.Model.Player;
using UnityMVVM.ViewModelCore;

namespace BubbleJump.UI.Score
{
    public class ScoreViewModel : ViewModel, IScoreViewModel
    {
        private readonly IPlayerModel _playerModel;
        public IBindable<float> Score => _playerModel.PlayerHeight;
        public IBindable<float> Record => _playerModel.Record;

        public ScoreViewModel(IPlayerModel playerModel)
        {
            _playerModel = playerModel;
        }
        
    }
}