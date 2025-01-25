using System;
using UnityEngine;

namespace BubbleJump.Interaction.Auxiliary
{
    public class PhysicsDispatcher : MonoBehaviour
    {

        [SerializeField]
        private string _tagFilter;

        public event Action<Collider2D> TriggerEntered;
        public event Action<Collider2D> TriggerExited;
        
        public event Action<Collision2D> ColliderEntered;
        public event Action<Collision2D> ColliderExited;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(_tagFilter))
                return;
            TriggerEntered?.Invoke(other);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag(_tagFilter))
                return;
            TriggerExited?.Invoke(other);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.collider.CompareTag(_tagFilter))
                return;
            ColliderEntered?.Invoke(col);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (!other.collider.CompareTag(_tagFilter))
                return;
            ColliderExited?.Invoke(other);
        }
    }
}