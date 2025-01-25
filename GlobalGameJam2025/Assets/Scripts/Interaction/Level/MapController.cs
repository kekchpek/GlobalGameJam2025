using System.Collections.Generic;
using UnityEngine;

namespace BubbleJump.Level
{
    public class MapController : MonoBehaviour
    {
        public GameObject LevelChunk;
        public Transform chunkParent;
        public Transform player;
        
        private GameObject _currentChunk;
        
        private readonly List<GameObject> _spawnedChunks = new();

        private void Awake()
        {
            _currentChunk = Instantiate(LevelChunk, Vector3.up * 20f, Quaternion.identity, chunkParent);
            _spawnedChunks.Add(_currentChunk);
        }
        
        void Update()
        {
            if(_currentChunk != null)
            {
                CheckAndSpawnChunks();
                ChunkOptimizer();
            }
        }

        void CheckAndSpawnChunks()
        {
            while (player.transform.position.y > _currentChunk.transform.position.y - 10f)
            {
                SpawnChunk();
            }
        }

        void SpawnChunk()
        {
            var nextChunkPos = _currentChunk.transform.Find("NextChunk").position;
            _currentChunk = Instantiate(LevelChunk, nextChunkPos, Quaternion.identity, chunkParent);
            _spawnedChunks.Add(_currentChunk);
        }

        private void ChunkOptimizer()
        {
            foreach (var chunk in _spawnedChunks)
            {
                if (chunk && chunk.transform.position.y < player.transform.position.y - 20f)
                {
                    Destroy(chunk);
                }
            }

            _spawnedChunks.RemoveAll(x => !x);
        }
    }
}





