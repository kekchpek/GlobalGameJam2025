using System.Collections.Generic;
using BubbleJump.Interaction.Auxiliary;
using BubbleJump.Interaction.Bubble;
using UnityEngine;

namespace BubbleJump.Interaction.Player
{
    
    [RequireComponent(typeof(GravityBehaviour))]
    public class GravityTargetSelector : MonoBehaviour
    {

        [SerializeField]
        private float _closeFactor = 3f;
        
        [SerializeField]
        private PhysicsDispatcher _trigger;

        [SerializeField]
        private PhysicsDispatcher _collider;

        private readonly HashSet<IGravityTarget> _targets = new();

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
            if (obj.collider.TryGetComponent<IGravityTarget>(out var target))
            {
                _gravityBehaviour.SetGravityTarget(target);
                _lastSelectedTargetPosition = obj.transform.position;
            }
        }

        private void OnTriggerEntered(Collider2D obj)
        {
            if (obj.TryGetComponent<IGravityTarget>(out var target)) 
                _targets.Add(target);
        }

        private void FixedUpdate()
        {
            var pos = transform.position;
            foreach (var trs in _targets)
            {
                var otherPos = trs.Transform.position;
                if (_lastSelectedTargetPosition.HasValue && 
                    otherPos.y > _lastSelectedTargetPosition.Value.y &&
                    (otherPos - pos).sqrMagnitude * _closeFactor < (_lastSelectedTargetPosition.Value - pos).sqrMagnitude)
                {
                    _gravityBehaviour.SetGravityTarget(trs);
                    _lastSelectedTargetPosition = trs.Transform.position;
                }
            }
        }

        private void OnTriggerExited(Collider2D obj)
        {
            if (obj.TryGetComponent<IGravityTarget>(out var gravityTarget))
                _targets.Remove(gravityTarget);
        }
        
    }
}