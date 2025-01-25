using UnityEngine;

namespace BubbleJump.Interaction.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GravityBehaviour : MonoBehaviour
    {

        private Transform _gravityTarget;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void SetGravityTarget(Transform target)
        {
            if (target)
            {
                _rigidbody.gravityScale = 0f;
            }
            else
            {
                _rigidbody.gravityScale = 1f;
            }
            _gravityTarget = target;
        }

        private void FixedUpdate()
        {
            if (_gravityTarget)
            {
                var dir = (_gravityTarget.position - transform.position).normalized;
                _rigidbody.AddForce(dir * (Physics.gravity.magnitude));
            }
            else
            {
                SetGravityTarget(null);
            }
        }
    }
}
