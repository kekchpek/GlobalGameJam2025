using System;
using BubbleJump.Auxiliary;
using UnityEngine;

namespace BubbleJump.Player
{
    public class BouncingBehaviour : MonoBehaviour
    {

        [SerializeField]
        private PhysicsDispatcher _collider;

        private void Awake()
        {
            _collider.ColliderEntered += OnColliderEntered;
        }

        private void OnColliderEntered(Collision obj)
        {
            
        }
    }
}