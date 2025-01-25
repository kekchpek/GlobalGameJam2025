using BubbleJump.Interaction.Bubble;
using UnityEngine;

namespace BubbleJump.Interaction.Player
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    public class BubbleKillBehaviour : MonoBehaviour
    {

        private Rigidbody2D _rigidbody;

        private bool _isKilling;

        [SerializeField]
        private GameObject _killingLayout;

        [SerializeField]
        private float _startKillingThreshold;
        
        [SerializeField]
        private float _endKillingThreshold;

        [SerializeField]
        private Vector2 _linearVelocity;
        [SerializeField]
        private Vector2 _position;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_isKilling)
            {
                if (_rigidbody.linearVelocity.magnitude < _endKillingThreshold)
                {
                    SetKilling(false);
                }
            }
            else
            {
                if (_rigidbody.linearVelocity.magnitude > _startKillingThreshold)
                {
                    SetKilling(true);
                }
            }

            _linearVelocity = _rigidbody.linearVelocity;
            _position = _rigidbody.position;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (_isKilling)
            {
                if (col.collider.CompareTag("Bubbles"))
                {
                    _rigidbody.linearVelocity = _linearVelocity;
                    _rigidbody.position = _position;
                    transform.position = _position;
                    col.collider.GetComponent<BubbleController>().Kill();
                }
            }
        }

        private void SetKilling(bool isKilling)
        {
            _isKilling = isKilling;
            if (_killingLayout)
                _killingLayout.SetActive(isKilling);
        }
    }
}