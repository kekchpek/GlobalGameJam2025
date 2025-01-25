using System;
using BubbleJump.Infrastructure.Time;

namespace BubbleJump.Model.Player
{
    public class PlayerService : IPlayerService
    {

        private const long RespawnTicks = 5 * TimeSpan.TicksPerSecond;
        
        private readonly IPlayerMutableModel _playerMutableModel;
        private readonly ITimeManager _timeManager;

        public PlayerService(
            IPlayerMutableModel playerMutableModel,
            ITimeManager timeManager)
        {
            _playerMutableModel = playerMutableModel;
            _timeManager = timeManager;
        }
        
        public void Die()
        {
            if (_playerMutableModel.IsDead.Value)
                return;
            _playerMutableModel.SetDead(true);
            _timeManager.AddCallback(_timeManager.CurrentTimestampUtc.Value + RespawnTicks, 
                Respawn);
        }

        private void Respawn()
        {
            _playerMutableModel.SetPlayerOnTheGround(true);
            _playerMutableModel.SetDead(false);
        }
        
    }
}