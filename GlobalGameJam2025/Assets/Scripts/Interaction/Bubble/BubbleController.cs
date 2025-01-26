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
            _hpText.text = _hp.ToString();
            if(_hp <= 0)
            {
                _animator.SetTrigger("Burst");
                _collider.enabled = false;
                Destroy(gameObject, 1f);
                _gravityTargetHandle.SetEnabled(false);
            }
        }

        public void Kill()
        {
            _hp = 0;
            UpdateHp();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                _animator.SetTrigger("React");
                _hp--;
                UpdateHp();
            }
            
        }
    }
}
