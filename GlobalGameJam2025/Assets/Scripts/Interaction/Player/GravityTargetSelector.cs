using System.Collections.Generic;
using System.Linq;
using BubbleJump.Interaction.Auxiliary;
using BubbleJump.Interaction.Bubble;
using BubbleJump.Model.Player;
using UnityEngine;
using Zenject;

namespace BubbleJump.Interaction.Player
{
    
    [RequireComponent(typeof(GravityBehaviour))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class GravityTargetSelector : MonoBehaviour
    {

        [SerializeField]
        private float _closeFactor = 3f;
        
        [SerializeField]
        private PhysicsDispatcher _trigger;

        [SerializeField]
        private PhysicsDispatcher _collider;

        private bool _selectClosestOnce = true;


        private readonly HashSet<IGravityTarget> _targets = new();

        private GravityBehaviour _gravityBehaviour;
        private IGravityTarget _lastSelectedTarget;
        private Rigidbody2D _rigidbody;
        private IPlayerModel _playerModel;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _gravityBehaviour = GetComponent<GravityBehaviour>();
            _trigger.TriggerExited += OnTriggerExited;
            _trigger.TriggerEntered += OnTriggerEntered;
            _collider.ColliderEntered += OnCollisionEntered;
        }

        [Inject]
        public void Construct(IPlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        private void OnCollisionEntered(Collision2D obj)
        {
            if (obj.collider.TryGetComponent<IGravityTarget>(out var target))
            {
                _gravityBehaviour.SetGravityTarget(target);
                _lastSelectedTarget = target;
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
                if (_lastSelectedTarget is { IsEnabled: true } && 
                    otherPos.y > _lastSelectedTarget.Transform.position.y &&
                    (otherPos - pos).sqrMagnitude * _closeFactor < (_lastSelectedTarget.Transform.position - pos).sqrMagnitude)
                {
                    _gravityBehaviour.SetGravityTarget(trs);
                    _lastSelectedTarget = trs;
                }
            }
        }

        private void SelectClosest()
        {
            if (!_selectClosestOnce)
                return;
            _selectClosestOnce = false;
            var minDist = float.MaxValue;
            var minIndex = -1;
            var arr = _targets.ToArray();
            for (var i = 0; i < arr.Length; i++)
            {
                var target = _targets.ToArray()[i];
                var dist = (target.Transform.position - transform.position).sqrMagnitude;
                if (dist < minDist)
                {
                    minDist = dist;
                    minIndex = i;
                }
            }

            if (minIndex > -1)
                _gravityBehaviour.SetGravityTarget(arr[minIndex]);
        }

        private void OnTriggerExited(Collider2D obj)
        {
            if (obj.TryGetComponent<IGravityTarget>(out var gravityTarget))
                _targets.Remove(gravityTarget);
        }
        
    }
}