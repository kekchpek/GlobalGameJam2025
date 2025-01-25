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
            _gravityTargetHandle = GetComponent<IGravityTargetHandle>();
        }

        private void Start()
        {
            _hpText = GetComponentInChildren<TextMeshPro>();
            _hp = Random.Range(10, 20);
            UpdateHp();
            //_animator = GetComponent<Animator>();
        }

        private void UpdateHp()
        {
            _hpText.text = _hp.ToString();
            if(_hp <= 0)
            {
                //_animator.SetBool("Explode",true);
                _collider.enabled = false;
                Destroy(gameObject, 0.5f);
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
                _hp--;
                UpdateHp();
            }
            
        }
    }
}
