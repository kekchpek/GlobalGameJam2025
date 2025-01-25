using BubbleJump.Model.Player;
using UnityEngine;
using Zenject;

namespace BubbleJump.Interaction.Player
{
    public class PlayerTransformTracker : MonoBehaviour
    {

        private IPlayerMutableModel _playerMutableModel;

        [Inject]
        public void Construct(IPlayerMutableModel playerMutableModel)
        {
            _playerMutableModel = playerMutableModel;
            if (_playerMutableModel.Transform.Value != null)
            {
                Debug.LogError("Player transform not reset!");
            }
            else
            {
                _playerMutableModel.SetTransform(transform);
            }
        }

        private void OnDestroy()
        {
            if (_playerMutableModel.Transform.Value != transform)
            {
                Debug.LogError("Player transform was reset!");
            }
            else
            {
                _playerMutableModel.SetTransform(null);
            }
        }
    }
}