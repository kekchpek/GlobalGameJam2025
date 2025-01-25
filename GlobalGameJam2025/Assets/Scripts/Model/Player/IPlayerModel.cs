using AsyncReactAwait.Bindable;
using UnityEngine;

namespace BubbleJump.Model.Player
{
    public interface IPlayerModel
    {
        IBindable<bool> Enabled { get; }
        IBindable<bool> IsOnTheGround { get; }
        IBindable<bool> IsDead { get; }
        IBindable<Transform> Transform { get; }
        IBindable<float> PlayerHeight { get; }
        IBindable<float> Record { get; }
    }
}