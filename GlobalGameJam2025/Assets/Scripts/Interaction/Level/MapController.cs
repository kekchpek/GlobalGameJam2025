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

        private List<GameObject> spawnedChunks = new();

        private void Start()
        {
            //Instantiate(LevelChunk, Vector3.zero, Quaternion.identity, chunkParent);
        }
        void Update()
        {
            CheckAndSpawnChunks();
            ChunkOptimizer();
        }

        void CheckAndSpawnChunks()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(player.position, checkerRadius, levelMask);
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject == currentChunk)
                {

                    SpawnChunk();

                    break;
                }
            }
        }

        void SpawnChunk()
        {
            Vector2 nextChunk = currentChunk.transform.Find("NextChunk").position;

            if (!Physics2D.OverlapPoint(nextChunk, levelMask))
            {
                GameObject newChunk1 = Instantiate(LevelChunk, nextChunk, Quaternion.identity, chunkParent);
                spawnedChunks.Add(newChunk1);
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





