using BubbleJump.Interaction.Auxiliary;
using UnityEngine;

namespace BubbleJump.Interaction.Player
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    public class BouncingBehaviour : MonoBehaviour
    {

        [SerializeField]
        private float _additionSpeed = 2f;
        
        [SerializeField]
        private float _speedFactor = 0.9f;

        [SerializeField]
        private PhysicsDispatcher _collider;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider.ColliderEntered += OnColliderEntered;
        }

        private void OnColliderEntered(Collision2D col)
        {
            Vector2 pos = transform.position;
            Vector2 bubblePos = col.transform.position;
            var outDir = (pos - bubblePos).normalized;
            var speed = col.relativeVelocity;
            var speedOutProjectionMgn = Mathf.Abs(Vector3.Dot(speed, outDir)); 
            _rigidbody.linearVelocity = outDir * (speedOutProjectionMgn * _speedFactor + _additionSpeed);
            _rigidbody.totalForce = Vector2.zero;
        }
    }
}