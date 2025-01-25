using System;
using UnityEngine;

namespace BubbleJump.Interaction.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GravityBehaviour : MonoBehaviour
    {

        private Transform _gravityTarget;
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private Transform _graphics;

        public Transform Target => _gravityTarget;

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

        private void Update()
        {
            if (_graphics && Target)
            {
                var pos = transform.position;
                var targetPos = Target.position;
                _graphics.position = (pos + targetPos) / 2f;
                _graphics.right = targetPos - pos;
                _graphics.localScale = new Vector3((pos - targetPos).magnitude, 0.1f, 1f);
            }

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
