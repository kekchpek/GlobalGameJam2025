
namespace BubbleJump.Model.Player
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerMutableModel _playerMutableModel;

        public PlayerService(
            IPlayerMutableModel playerMutableModel)
        {
            _playerMutableModel = playerMutableModel;
        }
        
        public void Die()
        {
            if (_playerMutableModel.IsDead.Value)
                return;
            _playerMutableModel.SetDead(true);
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
        
    }
}