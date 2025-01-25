using System;
using System.Collections.Generic;
using System.Linq;
using BubbleJump.Model.Player;
using UnityEngine;
using Zenject;

namespace BubbleJump.Interaction.Level
{
    public class MapController : MonoBehaviour
    {
        public GameObject[] LevelChunk;
        public Transform chunkParent;
        
        private GameObject _currentChunk;

        private readonly List<GameObject> _spawnedChunks = new();

        private IPlayerModel _playerModel;

        private void ResetChunks()
        {
            _currentChunk = Instantiate(LevelChunk[UnityEngine.Random.Range(0,LevelChunk.Count())], Vector3.up * 15f, Quaternion.identity, chunkParent);
            _spawnedChunks.Add(_currentChunk);
        }

        [Inject]
        public void Construct(IPlayerModel playerModel)
        {
            _playerModel = playerModel;
            _playerModel.IsDead.Bind(OnIsDeadChanged);
        }

        private void OnIsDeadChanged(bool isDead)
        {
            if (isDead)
            {
                foreach (var chunk in _spawnedChunks)
                {
                    Destroy(chunk);
                }
                ResetChunks();
            }
        }

        void Update()
        {
            if(_currentChunk)
            {
                CheckAndSpawnChunks();
                ChunkOptimizer();
            }
        }

        void CheckAndSpawnChunks()
        {
            if (_playerModel.Transform.Value)
            {
                while (_playerModel.Transform.Value.position.y > _currentChunk.transform.position.y - 10f)
                {
                    SpawnChunk();
                }
            }
        }

        void SpawnChunk()
        {
            var nextChunkPos = _currentChunk.transform.Find("NextChunk").position;
            _currentChunk = Instantiate(LevelChunk[UnityEngine.Random.Range(0, LevelChunk.Count())], nextChunkPos, Quaternion.identity, chunkParent);
            _spawnedChunks.Add(_currentChunk);
        }

        private void ChunkOptimizer()
        {
            if (_playerModel.Transform.Value)
            {
                foreach (var chunk in _spawnedChunks)
                {
                    if (chunk && chunk.transform.position.y < _playerModel.Transform.Value.position.y - 20f)
                    {
                        Destroy(chunk);
                    }
                }

                _spawnedChunks.RemoveAll(x => !x);
            }
        }
    }
}





