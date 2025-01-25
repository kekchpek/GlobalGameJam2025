using System;
using UnityEngine;

namespace BubbleJump.Interaction.Camera
{
    
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraWalls : MonoBehaviour
    {

        private UnityEngine.Camera _camera;
        
        [SerializeField]
        private Transform _leftWall;

        [SerializeField]
        private Transform _rightWall;

        private void Awake()
        {
            _camera = GetComponent<UnityEngine.Camera>();
        }

        private void Update()
        {
            float halfHeight = Mathf.Tan(Mathf.Deg2Rad * _camera.fieldOfView * 0.5f) * 22f;
            float halfWidth = halfHeight * _camera.aspect;

            Vector3 leftWallPosition = _camera.transform.position + _camera.transform.forward * 20f;
            leftWallPosition -= _camera.transform.right * halfWidth;

            Vector3 rightWallPosition = _camera.transform.position + _camera.transform.forward * 20f;
            rightWallPosition += _camera.transform.right * halfWidth;

            _leftWall.position = leftWallPosition;
            _rightWall.position = rightWallPosition;
        }
    }
}