using BubbleJump.Interaction.Bubble;
using UnityEngine;

namespace BubbleJump.Interaction.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GravityBehaviour : MonoBehaviour
    {

        private IGravityTarget _gravityTarget;
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private Transform _graphics;

        public Transform TargetTransform => _gravityTarget is { IsEnabled: true } ? _gravityTarget.Transform : null;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void SetGravityTarget(IGravityTarget target)
        {
            if (target != null)
            {
                _rigidbody.gravityScale = 0f;
            }
            else
            {
                _rigidbody.gravityScale = 1f;
            }
            _gravityTarget = target;
        }

        private void Update()
        {
            if (_graphics && TargetTransform)
            {
                var pos = transform.position;
                var targetPos = TargetTransform.position;
                _graphics.position = (pos + targetPos) / 2f;
                _graphics.right = targetPos - pos;
                _graphics.localScale = new Vector3((pos - targetPos).magnitude, 0.1f, 1f);
            }

        }

        private void FixedUpdate()
        {
            if (TargetTransform)
            {
                var dir = (TargetTransform.position - transform.position).normalized;
                _rigidbody.AddForce(dir * (Physics.gravity.magnitude));
            }
            else
            {
                SetGravityTarget(null);
            }
        }
    }
}
