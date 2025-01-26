using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BubbleJump.Level
{
    public class BubbleSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] bubblePrefabs;
        [SerializeField]
        private GameObject[] spawnPoints;



        private void Start()
        {
            spawnBubbles();
        }

        void spawnBubbles()
        {
            foreach(var spawnPoint in spawnPoints)
            {
                GameObject bubble = Instantiate(bubblePrefabs[Random.Range(0, bubblePrefabs.Count())], spawnPoint.transform.position, Quaternion.identity);

                int chance = Random.Range(0, 101);
                int radius;
                if (chance <= 20)
                {
                    radius = 1;
                }
                else radius = 2;
                bubble.transform.localScale = Vector3.one * radius;
                bubble.transform.SetParent(spawnPoint.transform);
            }
        }
    }
}
