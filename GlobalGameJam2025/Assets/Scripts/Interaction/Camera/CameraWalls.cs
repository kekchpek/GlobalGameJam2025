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
            float cameraY = _camera.transform.position.y;

            _leftWall.position = new Vector3(_leftWall.position.x, cameraY, _leftWall.position.z);
            _rightWall.position = new Vector3(_rightWall.position.x, cameraY, _rightWall.position.z);
        }
    
    }
}