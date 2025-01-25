using System;
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
        private TextMeshProUGUI _hpText;
        [SerializeField]
        private float _velocityTreshold;

        private void Awake()
        {
            _gravityTargetHandle = GetComponent<IGravityTargetHandle>();
        }

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
            if(_hp <= 0)
            {
                //_animator.SetBool("Explode",true);
                _collider.enabled = false;
                Destroy(gameObject, 0.5f);
                _gravityTargetHandle.SetEnabled(false);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                Rigidbody2D rb = collision.rigidbody;
                Vector2 orgVelocity = collision.relativeVelocity;

                Debug.Log($"Player Velocity Magnitude: {orgVelocity.sqrMagnitude}");

                if (orgVelocity.sqrMagnitude >= _velocityTreshold)
                {
                    Physics2D.IgnoreCollision(_collider, collision.collider, true);
                     rb.linearVelocity = orgVelocity;
                    _hp = 0;
                    UpdateHp();
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
