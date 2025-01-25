using System.Collections.Generic;
using BubbleJump.Interaction.Auxiliary;
using UnityEngine;

namespace BubbleJump.Interaction.Player
{
    
    [RequireComponent(typeof(GravityBehaviour))]
    public class GravityTargetSelector : MonoBehaviour
    {

        [SerializeField]
        private PhysicsDispatcher _trigger;

        [SerializeField]
        private PhysicsDispatcher _collider;

        private readonly HashSet<Transform> _enteredTransforms = new();

        private GravityBehaviour _gravityBehaviour;
        private Vector3? _lastSelectedTargetPosition;

        private void Awake()
        {
            _gravityBehaviour = GetComponent<GravityBehaviour>();
            _trigger.TriggerExited += OnTriggerExited;
            _trigger.TriggerEntered += OnTriggerEntered;
            _collider.ColliderEntered += OnCollisionEntered;
        }

        private void OnCollisionEntered(Collision obj)
        {
            _gravityBehaviour.SetGravityTarget(obj.transform);
        }

        private void OnTriggerEntered(Collider obj)
        {
            _enteredTransforms.Add(obj.transform);
        }

        private void FixedUpdate()
        {
            foreach (var trs in _enteredTransforms)
            {
                if (_lastSelectedTargetPosition.HasValue && trs.position.y > _lastSelectedTargetPosition.Value.y)
                {
                    _gravityBehaviour.SetGravityTarget(trs);
                }
            }
        }

        private void OnTriggerExited(Collider obj)
        {
            _enteredTransforms.Remove(obj.transform);
        }
        
    }
}