using System;
using UnityEngine;

namespace BubbleJump.Interaction.Bubble
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
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (_isKilling)
            {
                if (col.collider.CompareTag("Bubbles"))
                {
                    var orgVelocity = col.relativeVelocity;
                    _rigidbody.linearVelocity = -orgVelocity;
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