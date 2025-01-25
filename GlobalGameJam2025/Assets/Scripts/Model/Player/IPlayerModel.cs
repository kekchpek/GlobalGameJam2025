using AsyncReactAwait.Bindable;

namespace BubbleJump.Model.Player
{
    public interface IPlayerModel
    {
        IBindable<bool> IsOnTheGround { get; }
    }
}