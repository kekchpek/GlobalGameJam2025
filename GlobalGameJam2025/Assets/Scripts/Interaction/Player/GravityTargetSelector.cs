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

        private void OnCollisionEntered(Collision2D obj)
        {
            _gravityBehaviour.SetGravityTarget(obj.transform);
            _lastSelectedTargetPosition = obj.transform.position;
        }

        private void OnTriggerEntered(Collider2D obj)
        {
            _enteredTransforms.Add(obj.transform);
        }

        private void FixedUpdate()
        {
            var pos = transform.position;
            foreach (var trs in _enteredTransforms)
            {
                var otherPos = trs.position;
                if (_lastSelectedTargetPosition.HasValue && 
                    otherPos.y > _lastSelectedTargetPosition.Value.y &&
                    (otherPos - pos).sqrMagnitude < (_lastSelectedTargetPosition.Value - pos).sqrMagnitude)
                {
                    _gravityBehaviour.SetGravityTarget(trs);
                    _lastSelectedTargetPosition = trs.position;
                }
            }
        }

        private void OnTriggerExited(Collider2D obj)
        {
            _enteredTransforms.Remove(obj.transform);
        }
        
    }
}