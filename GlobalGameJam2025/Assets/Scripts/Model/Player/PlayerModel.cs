using System;
using AsyncReactAwait.Bindable;
using UnityEngine;

namespace BubbleJump.Model.Player
{
    public class PlayerModel : IPlayerMutableModel
    {

        private readonly IMutable<bool> _isOnTheGround = new Mutable<bool>(true);
        private readonly IMutable<bool> _isDead = new Mutable<bool>();
        private readonly IMutable<Transform> _transform = new Mutable<Transform>();

        public IBindable<bool> IsOnTheGround => _isOnTheGround;
        public IBindable<bool> IsDead => _isDead;
        public IBindable<Transform> Transform => _transform;

        public void SetPlayerOnTheGround(bool isPlayerOnTheGround)
        {
            _isOnTheGround.Value = isPlayerOnTheGround;
        }

        public void SetDead(bool isDead)
        {
            _isDead.Value = isDead;
        }

        public void SetTransform(Transform transform)
        {
            _transform.Value = transform;
        }
    }
}