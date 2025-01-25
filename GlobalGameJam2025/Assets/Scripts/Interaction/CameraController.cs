using System.Linq.Expressions;
using UnityEngine;

namespace BubbleJump
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform _player;
        [SerializeField]
        private float zOffset;

        private void Start()
        {
            //player = FindFirstObjectByType<Player>;
        }

        private void Update()
        {
            transform.position = new Vector3(transform.position.x, _player.transform.position.y, zOffset);
        }
    }
}
