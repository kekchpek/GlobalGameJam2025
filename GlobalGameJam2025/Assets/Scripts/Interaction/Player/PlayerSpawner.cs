using BubbleJump.Model.Player;
using UnityEngine;
using Zenject;

namespace BubbleJump.Interaction.Player
{
    public class PlayerSpawner : MonoBehaviour
    {

        [SerializeField]
        private GameObject _playerPrefab;

        [SerializeField]
        private Transform _spawnPoint;

        private IPlayerModel _playerModel;
        private IInstantiator _instantiator;

        [Inject]
        public void Construct(
            IPlayerModel playerModel,
            IInstantiator instantiator)
        {
            _playerModel = playerModel;
            _instantiator = instantiator;
            _playerModel.IsDead.Bind(OnIsDeadChanged);
        }

        private void OnIsDeadChanged(bool isDead)
        {
            if (!isDead)
            {
                _instantiator.InstantiatePrefab(_playerPrefab, _spawnPoint.position, Quaternion.identity, null);
            }
        }

        private void OnDestroy()
        {
            _playerModel.IsDead.Unbind(OnIsDeadChanged);
        }
    }
}