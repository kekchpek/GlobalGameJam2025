using AsyncReactAwait.Bindable;

namespace BubbleJump.Model.Player
{
    public class PlayerModel : IPlayerModel
    {

        private readonly IMutable<bool> _isOnTheGround = new Mutable<bool>(true);

        public IBindable<bool> IsOnTheGround => _isOnTheGround;
        
    }
}