using BubbleJump.Model.Player;
using BubbleJump.Model.SuperJump;
using UnityEngine;
using Zenject;

namespace BubbleJump.Interaction.Player
{
    
    [RequireComponent(typeof(GravityBehaviour))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerControls : MonoBehaviour
    {

        [SerializeField]
        private float _speed = 2f;

        [SerializeField]
        private PlayerAnimator _playerAnimator;
        
        private GravityBehaviour _gravityBehaviour;
        private Rigidbody2D _rigidbody2D;

        private IPlayerModel _playerModel;
        private ISuperJumpModel _superJumpModel;
        
        private void Awake()
        {
            _gravityBehaviour = GetComponent<GravityBehaviour>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        [Inject]
        public void Construct(
            IPlayerModel playerModel,
            ISuperJumpModel superJumpModel)
        {
            _superJumpModel = superJumpModel;
            _playerModel = playerModel;
        }

        private void FixedUpdate()
        {
            if (!_playerModel.Enabled.Value)
                return;
            if (_playerAnimator)
                _playerAnimator.SetSpeed(Input.GetAxis("Horizontal"));
            if (!_playerModel.IsOnTheGround.Value)
            {
                if (_gravityBehaviour.TargetTransform)
                {
                    var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
                    var position = transform.position;
                    var currentPos = (position - _gravityBehaviour.TargetTransform.position);
                    var currentPosNorm = currentPos.normalized;
                    var crossIm = Vector3.Cross(currentPosNorm, input).normalized;
                    var rotateDir = Vector3.Cross(crossIm, currentPosNorm).normalized;


                    var linear = _rigidbody2D.linearVelocity;
                    var d = rotateDir * (_speed * linear.magnitude);
                    var linearY = Vector3.Dot(linear, -currentPosNorm);

                    _rigidbody2D.linearVelocity = -currentPosNorm * linearY + d;
                }
                else
                {
                    var v = _rigidbody2D.linearVelocity;
                    v.x = Input.GetAxis("Horizontal") * 8f;
                    _rigidbody2D.linearVelocity = v;
                }
            }
            else if (_superJumpModel.Strength.Value <= 0f)
            {
                var v = _rigidbody2D.linearVelocity;
                v.x = Input.GetAxis("Horizontal") * 3f;
                _rigidbody2D.linearVelocity = v;
            }
        }
    }
    
}