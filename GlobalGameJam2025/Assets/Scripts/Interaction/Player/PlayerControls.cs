using System;
using UnityEngine;

namespace BubbleJump.Interaction.Player
{
    
    [RequireComponent(typeof(GravityBehaviour))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerControls : MonoBehaviour
    {

        [SerializeField]
        private float _speed = 2f;
        
        private GravityBehaviour _gravityBehaviour;
        private Rigidbody2D _rigidbody2D;
        
        private void Awake()
        {
            _gravityBehaviour = GetComponent<GravityBehaviour>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_gravityBehaviour.Target)
            {
                var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
                var position = transform.position;
                var currentPos = (position - _gravityBehaviour.Target.position);
                var currentPosNorm = currentPos.normalized;
                var crossIm = Vector3.Cross(currentPosNorm, input).normalized;
                var rotateDir = Vector3.Cross(crossIm, currentPosNorm).normalized;
                position += rotateDir * (_speed * Time.fixedDeltaTime * currentPos.magnitude);
                transform.position = position;
            }
        }
    }
    
}