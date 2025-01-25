using BubbleJump.Model.SuperJump;
using UnityEngine;
using Zenject;

namespace BubbleJump.Interaction.Player
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    public class SuperJumpBehaviour : MonoBehaviour
    {

        private ISuperJumpService _superJumpService;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        [Inject]
        public void Construct(ISuperJumpService superJumpService)
        {
            _superJumpService = superJumpService;
            _superJumpService.Jumped += OnJump;
        }

        private void OnJump(float str)
        {
            _rigidbody.AddForce(Vector3.up * str);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
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