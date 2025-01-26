using BubbleJump.Model.Player;
using UnityEngine;
using Zenject;

namespace BubbleJump.Interaction.Player
{
    public class PlayerFallBehaviour : MonoBehaviour
    {

        [SerializeField]
        private Animator _animator;
         
        private IPlayerService _playerService;
        private IPlayerModel _playerModel;
        private static readonly int DeathSpeed = Animator.StringToHash("DeathSpeed");

        [Inject]
        public void Construct(
            IPlayerModel playerModel,
            IPlayerService playerService)
        {
            _playerService = playerService;
            _playerModel = playerModel;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Floor"))
            {
                if (!_playerModel.IsOnTheGround.Value)
                {
                    _playerService.Die();
                    if (_animator)
                        _animator.SetFloat(DeathSpeed, collision.relativeVelocity.magnitude);
                    Destroy(gameObject, 1f);
                }
            }
        }
    }
}