using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BubbleJump.Interaction.Bubble
{
    public class BubbleController : MonoBehaviour
    {
        [SerializeField]
        private int _hp;

        [SerializeField]
        private Collider2D _collider;

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
            _hp = Random.Range(2, 8);
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
            _collider.enabled = false;
            Destroy(gameObject, 1f);
            _gravityTargetHandle.SetEnabled(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
               
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
