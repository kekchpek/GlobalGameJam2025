using System;
using UnityEngine;

namespace BubbleJump.Auxiliary
{
    public class PhysicsDispatcher : MonoBehaviour
    {

        [SerializeField]
        private string _tagFilter;

        public event Action<Collider> TriggerEntered;
        public event Action<Collider> TriggerExited;
        
        public event Action<Collision> ColliderEntered;
        public event Action<Collision> ColliderExited;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_tagFilter))
                return;
            TriggerEntered?.Invoke(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(_tagFilter))
                return;
            TriggerExited?.Invoke(other);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.collider.CompareTag(_tagFilter))
                return;
            ColliderEntered?.Invoke(other);
        }

        private void OnCollisionExit(Collision other)
        {
            if (!other.collider.CompareTag(_tagFilter))
                return;
            ColliderExited?.Invoke(other);
        }
    }
}