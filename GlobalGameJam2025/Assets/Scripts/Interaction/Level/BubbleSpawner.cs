using System.Collections.Generic;
using UnityEngine;

namespace BubbleJump.Level
{
    public class BubbleSpawner : MonoBehaviour
    {
        public GameObject bubble;
        public int numberOfBubbles;
        public int minDistance;

        //private Vector2 spawnArea;
        public Vector2 spawnArea = new Vector2(21f, 13f);
        private List<Vector2> spawnedBubbles = new List<Vector2>();
        private float bubbleRadius;


        private void Start()
        {
            spawnBubbles();
        }

        void spawnBubbles()
        {
            int attempts = 0; 
            int maxAttempts = 20;
            while (spawnedBubbles.Count < numberOfBubbles && attempts < maxAttempts) 
            {
                Vector2 randomPosition = GenerateRandomPosition();
                Vector3 worldPosition = (Vector3)randomPosition + transform.position;

                if (IsValidPosition(randomPosition))
                {
                    GameObject newBubble = Instantiate(bubble, worldPosition, Quaternion.identity);
                    newBubble.transform.localScale = Vector3.one * bubbleRadius;
                    spawnedBubbles.Add(randomPosition);
                    newBubble.transform.SetParent(transform);
                }

                attempts++;
            }
        }
            

        Vector2 GenerateRandomPosition()
        {
            float x = Random.Range(-spawnArea.x / 2, spawnArea.x / 2);
            float y = Random.Range(-spawnArea.y / 2, spawnArea.y / 2);
            return new Vector2(x, y);
        }

        bool IsValidPosition(Vector2 position)
        {
            bubbleRadius = Random.Range(1, 3);
            if (position.x - bubbleRadius < -spawnArea.x / 2 || position.x + bubbleRadius > spawnArea.x / 2 ||
                position.y - bubbleRadius < -spawnArea.y / 2 || position.y + bubbleRadius > spawnArea.y / 2)
            {
                return false;
            }

            foreach (Vector2 existingBubble in spawnedBubbles)
            {
                if (Vector2.Distance(position, existingBubble) < minDistance)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
