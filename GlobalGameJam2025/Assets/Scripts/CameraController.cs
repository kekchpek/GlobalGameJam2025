using UnityEngine;

namespace BubbleJump
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform _player;
        [SerializeField]
        private Vector3 offset;

        private void Start()
        {
            //player = FindFirstObjectByType<Player>;
        }

        private void Update()
        {
            transform.position = player.position + offset;
        }
    }
}
