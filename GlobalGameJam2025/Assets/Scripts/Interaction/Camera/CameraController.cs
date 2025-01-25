using BubbleJump.Model.Player;
using UnityEngine;
using Zenject;

namespace BubbleJump.Interaction.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private float zOffset;
        [SerializeField]
        private float yOffset;

        private IPlayerModel _playerModel;
        private Transform _player;
        
        [Inject]
        public void Construct(IPlayerModel playerModel)
        {
            _playerModel = playerModel;
            _playerModel.Transform.Bind(OnPlayerTrsChanged);
        }

        private void OnPlayerTrsChanged(Transform trs)
        {
            _player = trs;
        }

        private void Update()
        {
            if (_player)
            {
                var trs = transform;
                trs.position = new Vector3(trs.position.x, _player.transform.position.y + yOffset, zOffset);
            }
        }

        private void OnDestroy()
        {
            _playerModel.Transform.Unbind(OnPlayerTrsChanged);
        }
    }
}
