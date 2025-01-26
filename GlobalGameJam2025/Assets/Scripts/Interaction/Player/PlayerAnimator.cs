using System;
using BubbleJump.Model.Player;
using BubbleJump.Model.SuperJump;
using UnityEngine;
using Zenject;

namespace BubbleJump.Interaction.Player
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(GravityBehaviour))]
    public class PlayerAnimator : MonoBehaviour
    {
        
        [Flags]
        public enum State
        {
            Up = 1 << 0,
            Down = 1 << 1,
            Grounded = 1 << 2,
            Flip = 1 << 3,
            Jump = 1 << 4,
            Up2 = 1 << 5,
        }

        private State _state;

        private Rigidbody2D _rigidbody;
        private GravityBehaviour _gravityBehaviour;

        private IPlayerModel _playerModel;
        private ISuperJumpModel _superJumpModel;

        [SerializeField]
        private Animator _animator;

        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Grounded = Animator.StringToHash("Grounded");
        private static readonly int Up = Animator.StringToHash("Up");
        private static readonly int Up2 = Animator.StringToHash("Up2");
        private static readonly int Flip = Animator.StringToHash("Flip");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Down = Animator.StringToHash("Down");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int JumpForce = Animator.StringToHash("JumpForce");

        [Inject]
        public void Construct(
            IPlayerModel playerModel,
            ISuperJumpModel superJumpModel)
        {
            _superJumpModel = superJumpModel;
            _playerModel = playerModel;
            _playerModel.IsOnTheGround.Bind(OnPlayerGrounded);
        }

        private void Awake()
        {
            _gravityBehaviour = GetComponent<GravityBehaviour>();
            _rigidbody = GetComponent<Rigidbody2D>();
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
        }

        public void SetSpeed(float speed)
        {
            _animator.SetFloat(Speed, speed);
        }

        private void Update()
        {
            var v = _rigidbody.linearVelocity;
            var target = _gravityBehaviour.TargetTransform;
            var trs = transform;
            _state = 0;
            _animator.SetFloat(JumpForce, _superJumpModel.Strength.Value);
            if (_playerModel.IsDead.Value)
            {
                _animator.SetTrigger(Death);
            }
            else if (_playerModel.IsOnTheGround.Value)
            {
                ActivateState(State.Grounded);
                trs.up = Vector3.up;
            }
            else if (!target)
            {
                trs.up = v;
                ActivateState(State.Up);
                if (v.magnitude > 20f)
                {
                    ActivateState(State.Up2);
                }
            }
            else
            {
                var outDir = trs.position - target.position;
                var currentUp = trs.up;
                var diff = 1f - Vector3.Dot(outDir.normalized, currentUp);
                if (Mathf.Abs(diff) < 0.01f)
                {
                    trs.up = outDir;
                    ActivateState(Vector2.Dot(_rigidbody.linearVelocity, outDir) > 0 ? State.Up : State.Down);
                    SetSpeed(Vector2.Dot(_rigidbody.linearVelocity, trs.right) * 10f);
                }
                else
                {
                    trs.Rotate(Vector3.forward, Mathf.Sign(diff) * Time.deltaTime * 500f);
                    ActivateState(State.Flip);
                }
            }
            UpdateState();
        }

        public void DisableState(State state)
        {
            _state &= ~state;
        }

        private void UpdateState()
        {
            var up = false;
            var up2 = false;
            var down = false;
            var grounded = false;
            var flip = false;
            var jump = false;
            if ((_state & State.Grounded) > 0)
            {
                grounded = true;
            }
            else if ((_state & State.Jump) > 0)
            {
                jump = true;
            }
            else if ((_state & State.Flip) > 0)
            {
                flip = true;
            }
            else if ((_state & State.Down) > 0)
            {
                down = true;
            }
            else if ((_state & State.Up) > 0)
            {
                up = true;
                if ((_state & State.Up2) > 0)
                {
                    up2 = true;
                }
            }
            _animator.SetBool(Grounded, grounded);
            _animator.SetBool(Up, up);
            _animator.SetBool(Up2, up2);
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