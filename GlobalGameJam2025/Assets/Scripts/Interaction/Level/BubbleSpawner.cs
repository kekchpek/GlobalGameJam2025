using System.Collections.Generic;
using UnityEngine;

namespace BubbleJump.Level
{
    public class BubbleSpawner : MonoBehaviour
    {
        public GameObject bubble;
        public int numberOfBubbles;
        public int minDistance;

        private Vector2 spawnArea;
        private List<Vector2> spawnedBubbles = new List<Vector2>();
        private float bubbleRadius;


        void Start()
        {
            Collider2D parentCollider = transform.parent.GetComponent<Collider2D>();
            spawnArea.x = parentCollider.bounds.size.x;
            spawnArea.y = parentCollider.bounds.size.y;

            
        }
        private void Update()
        {
            if (spawnedBubbles.Count < numberOfBubbles)
            {
                spawnBubbles();
            }
        }

        void spawnBubbles()
        {
            Vector2 randomPosition = GenerateRandomPosition();

            if (IsValidPosition(randomPosition))
            {
                GameObject newBubble = Instantiate(bubble, randomPosition, Quaternion.identity);
                Vector3 newRadius = newBubble.transform.localScale;
                newRadius.x = bubbleRadius;
                newRadius.y = bubbleRadius;
                newBubble.transform.localScale = newRadius;
                spawnedBubbles.Add(randomPosition);
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
            bubbleRadius = Random.Range(3, 11);
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
