using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BubbleJump.Interaction.Bubble
{
    public class BubbleController : MonoBehaviour
    {

        [SerializeField]
        private List<AudioClip> _killClips = new();

        [SerializeField]
        private AudioClip _jumpClip;
        
        [SerializeField]
        private int _hp;

        [SerializeField]
        private Collider2D _collider;

        [SerializeField]
        private AudioSource _audioSource;

        private IGravityTargetHandle _gravityTargetHandle;

        private Animator _animator;
        private TextMeshPro _hpText;
        [SerializeField]
        private float _velocityTreshold;

        private void Awake()
        {
            
            _animator = GetComponentInChildren<Animator>();
            _gravityTargetHandle = GetComponent<IGravityTargetHandle>();
        }

        private void Start()
        {
            _hpText = GetComponentInChildren<TextMeshPro>();
            _hp = Mathf.Max(2, (int)(Random.Range(4, 10) * ((700f - transform.position.y) / 700f)));
            UpdateHp();
        }

        private void UpdateHp()
        {
            
            if(_hp <= 0)
            {
                Kill();
            }
            else
                _hpText.text = _hp.ToString();
        }

        public void Kill()
        {
            _animator.SetTrigger("Burst");
            _audioSource.clip = _killClips[Random.Range(0, _killClips.Count)];
            _audioSource.Play();
            _collider.enabled = false;
            Destroy(gameObject, 1f);
            _gravityTargetHandle.SetEnabled(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                _audioSource.clip = _jumpClip;
                _audioSource.Play();
                _hp--;
                if( _hp != 0)
                {
                    _animator.SetTrigger("React");
                }
                UpdateHp();
            }
            
        }
    }
}
