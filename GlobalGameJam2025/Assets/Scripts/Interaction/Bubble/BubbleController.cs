using UnityEngine;
using TMPro;

namespace BubbleJump
{
    public class BubbleController : MonoBehaviour
    {
        [SerializeField]
        private int _hp;

        private Animator _animator;
        private TextMeshProUGUI _hpText;
        private float _velocityTreshold;

        private void Start()
        {
            _hpText = GetComponentInChildren<TextMeshProUGUI>();
            _hp = Random.Range(3, 7);
            UpdateHp();
            //_animator = GetComponent<Animator>();
        }

        private void UpdateHp()
        {
            _hpText.text = _hp.ToString();
        }

        private void Update()
        {
            if(_hp <= 0)
            {
                //_animator.SetBool("Explode",true);
                Destroy(gameObject, 0.5f);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player")) ;
            {
                Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
                Vector2 orgVelocity = rb.linearVelocity;

                Debug.Log($"Player Velocity Magnitude: {orgVelocity.sqrMagnitude}");

                if (orgVelocity.sqrMagnitude >= _velocityTreshold)
                {
                    Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), collision.collider, true);
                    rb.linearVelocity = orgVelocity;
                    _hp = 0;
                }
                else
                {
                    _hp--;
                    UpdateHp();
                }
            }
            
        }
    }
}
