using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainUtils;

namespace BubbleJump.Level
{
    public class MapController : MonoBehaviour
    {
        public GameObject LevelChunk;
        public Transform chunkParent;
        public Transform player;
        public GameObject currentChunk;

        public float checkerRadius = 2f;
        public float optimizerCooldownDur;
        public float maxOptimizeDistance;
        public LayerMask levelMask;

        private float optimizerCooldown;
        private GameObject lastChunk;

        private List<GameObject> spawnedChunks = new List<GameObject>();

        private void Start()
        {
            //Instantiate(LevelChunk, Vector3.zero, Quaternion.identity, chunkParent);
        }
        void Update()
        {
            if(currentChunk != null)
            {
                CheckAndSpawnChunks();
                ChunkOptimizer();
            }
        }

        void CheckAndSpawnChunks()
        {
            if (player.transform.position.y <= currentChunk.transform.position.y)
            {
                SpawnChunk();
            }
        }

        void SpawnChunk()
        {
            Vector3 nextChunk = currentChunk.transform.Find("NextChunk").position;
            if(lastChunk != null)
            {
                if (lastChunk.transform.position != nextChunk)
                {
                    lastChunk = Instantiate(LevelChunk, nextChunk, Quaternion.identity, chunkParent);
                    spawnedChunks.Add(lastChunk);
                }
            }
            else
            {
                lastChunk = Instantiate(LevelChunk, nextChunk, Quaternion.identity, chunkParent);
                spawnedChunks.Add(lastChunk);
            }
            
        }

        private void ChunkOptimizer()
        {
            optimizerCooldown -= Time.deltaTime;

            if (optimizerCooldown <= 0f)
            {
                optimizerCooldown = optimizerCooldownDur;
            }
            else
            {
                return;
            }

            foreach (GameObject chunk in spawnedChunks)
            {
                float opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
                if (opDist > maxOptimizeDistance)
                {
                    chunk.SetActive(false);
                }
                else
                {
                    chunk.SetActive(true);
                }
            }
        }
    }
}





