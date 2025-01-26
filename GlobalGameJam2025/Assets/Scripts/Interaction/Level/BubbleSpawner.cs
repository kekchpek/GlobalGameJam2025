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
                bubble.transform.localScale = Vector3.one * Random.Range(2, 3);
                bubble.transform.SetParent(spawnPoint.transform);
            }
        }
    }
}
