using AsyncReactAwait.Bindable;
using UnityEngine;

namespace BubbleJump.Model.Player
{
    public class PlayerModel : IPlayerMutableModel
    {

        private readonly IMutable<bool> _isOnTheGround = new Mutable<bool>();
        private readonly IMutable<bool> _isDead = new Mutable<bool>(true);
        private readonly IMutable<Transform> _transform = new Mutable<Transform>();
        private readonly IMutable<float> _height = new Mutable<float>();
        private readonly IMutable<float> _record = new Mutable<float>();
        private readonly IMutable<bool> _isEnabled = new Mutable<bool>();

        public IBindable<bool> Enabled => _isEnabled;
        public IBindable<bool> IsOnTheGround => _isOnTheGround;
        public IBindable<bool> IsDead => _isDead;
        public IBindable<Transform> Transform => _transform;
        public IBindable<float> PlayerHeight => _height;
        public IBindable<float> Record => _record;

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

        public void SetPlayerHeight(float height)
        {
            _height.Value = height;
        }

        public void SetPlayerRecord(float record)
        {
            _record.Value = record;
        }

        public void SetEnabled(bool isEnabled)
        {
            _isEnabled.Value = isEnabled;
        }
    }
}