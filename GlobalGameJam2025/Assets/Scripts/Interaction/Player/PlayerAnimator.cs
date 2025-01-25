using System;
using BubbleJump.Model.Player;
using UnityEngine;
using Zenject;

namespace BubbleJump.Interaction.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        
        [Flags]
        public enum State
        {
            Up = 1 << 0,
            Down = 1 << 1,
            Grounded = 1 << 2,
            Flip = 1 << 3,
            Jump = 1 << 4
        }

        private State _state;

        private IPlayerModel _playerModel;

        [SerializeField]
        private Animator _animator;

        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Grounded = Animator.StringToHash("Grounded");
        private static readonly int Up = Animator.StringToHash("Up");
        private static readonly int Flip = Animator.StringToHash("Flip");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Down = Animator.StringToHash("Down");

        [Inject]
        public void Construct(IPlayerModel playerModel)
        {
            _playerModel = playerModel;
            _playerModel.IsOnTheGround.Bind(OnPlayerGrounded);
        }

        private void OnPlayerGrounded(bool isGrounded)
        {
            if (isGrounded)
            {
                ActivateState(State.Grounded);
            }
            else
            {
                DisableState(State.Grounded);
            }
        }

        public void ActivateState(State state)
        {
            _state |= state;
            UpdateState();
        }

        public void SetSpeed(float speed)
        {
            _animator.SetFloat(Speed, speed);
        }

        public void DisableState(State state)
        {
            _state &= ~state;
            UpdateState();
        }

        private void UpdateState()
        {
            var up = false;
            var down = false;
            var grounded = false;
            var flip = false;
            var jump = false;
            if (_state is State.Jump)
            {
                jump = true;
            }
            else if (_state is State.Flip)
            {
                flip = true;
            }
            else if (_state is State.Up)
            {
                up = true;
            }
            else if (_state is State.Down)
            {
                down = true;
            }
            else if (_state is State.Grounded)
            {
                grounded = true;
            }
            _animator.SetBool(Grounded, grounded);
            _animator.SetBool(Up, up);
            _animator.SetBool(Flip, flip);
            _animator.SetBool(Jump, jump);
            _animator.SetBool(Down, down);
        }

        private void OnDestroy()
        {
            _playerModel.IsOnTheGround.Unbind(OnPlayerGrounded);
        }
    }
}