using UnityEngine;

namespace BubbleJump.Interaction.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform _player;
        [SerializeField]
        private float zOffset;
        [SerializeField]
        private float yOffset;

        private void Update()
        {
            transform.position = new Vector3(transform.position.x, _player.transform.position.y + yOffset, zOffset);
        }
    }
}
