
using UnityEngine;

namespace BubbleJump.Model.Player
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerMutableModel _playerMutableModel;

        public PlayerService(
            IPlayerMutableModel playerMutableModel)
        {
            _playerMutableModel = playerMutableModel;
            _playerMutableModel.SetPlayerRecord(PlayerPrefs.GetFloat("Record", 0f));
        }
        
        public void Die()
        {
            if (_playerMutableModel.IsDead.Value)
                return;
            _playerMutableModel.SetDead(true);
            var curH = _playerMutableModel.PlayerHeight.Value;
            var record = _playerMutableModel.Record.Value;
            if (curH > record)
            {
                PlayerPrefs.SetFloat("Record", curH);
                _playerMutableModel.SetPlayerRecord(curH);
            }
        }

        public void Enable()
        {
            _playerMutableModel.SetEnabled(true);
        }

        public void Respawn()
        {
            _playerMutableModel.SetPlayerOnTheGround(true);
            _playerMutableModel.SetDead(false);
        }

        public void TrackHeight(float height)
        {
            var curH = _playerMutableModel.PlayerHeight.Value;
            if (height > curH)
                _playerMutableModel.SetPlayerHeight(height);
        }
    }
}