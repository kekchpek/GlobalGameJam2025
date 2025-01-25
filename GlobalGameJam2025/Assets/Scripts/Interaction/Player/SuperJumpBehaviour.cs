using BubbleJump.Model.Player;
using BubbleJump.Model.SuperJump;
using UnityEngine;
using Zenject;

namespace BubbleJump.Interaction.Player
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    public class SuperJumpBehaviour : MonoBehaviour
    {

        private ISuperJumpService _superJumpService;
        private IPlayerModel _playerModel;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        [Inject]
        public void Construct(
            ISuperJumpService superJumpService,
            IPlayerModel playerModel)
        {
            _playerModel = playerModel;
            _superJumpService = superJumpService;
            _superJumpService.Jumped += OnJump;
        }

        private void OnJump(float str)
        {
            _rigidbody.AddForce(Vector3.up * str);
        }

        private void Update()
        {
            if (_playerModel.Enabled.Value && Input.GetKeyDown(KeyCode.Space))
            {
                _superJumpService.Charge();
            }
        }

        private void OnDestroy()
        {
            _superJumpService.Jumped -= OnJump;
        }
    }
}