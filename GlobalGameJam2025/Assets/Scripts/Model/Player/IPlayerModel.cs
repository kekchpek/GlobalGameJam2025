using System;
using AsyncReactAwait.Bindable;
using UnityEngine;

namespace BubbleJump.Model.Player
{
    public interface IPlayerModel
    {
        IBindable<bool> IsOnTheGround { get; }
        IBindable<bool> IsDead { get; }
        IBindable<Transform> Transform { get; }
    }
}