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
            var width = _camera.orthographicSize / Screen.height * Screen.width + 0.5f;
            _leftWall.localPosition = Vector3.left * width - Vector3.forward;
            _rightWall.localPosition = Vector3.right * width - Vector3.forward;
        }
    }
}