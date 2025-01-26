using BubbleJump.Interaction.Rip;
using BubbleJump.Model.Player;
using UnityEngine;
using Zenject;

namespace BubbleJump.Interaction.Player
{
    public class PlayerFallBehaviour : MonoBehaviour
    {

        [SerializeField]
        private AudioClip _dieClip;

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private Animator _animator;
        
        [SerializeField]
        private RipBehaviour _ripPrefab;
         
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
                    _audioSource.clip = _dieClip;
                    _audioSource.Play();
                    _playerService.Die();
                    if (_ripPrefab)
                        Instantiate(_ripPrefab, transform.position, Quaternion.identity).SetTitle(_playerModel.PlayerHeight.Value.ToString("0"));
                    if (_animator)
                        _animator.SetFloat(DeathSpeed, collision.relativeVelocity.magnitude);
                    Destroy(gameObject, 1f);
                }
            }
        }
    }
}