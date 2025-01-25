using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleJump.Level
{
    public class ChunkTrigger : MonoBehaviour
    {
        MapController mapController;
        public GameObject targetMap;

        void Awake()
        {
            mapController = FindFirstObjectByType<MapController>();
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                mapController.currentChunk = targetMap;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                mapController.currentChunk = null;
            }
        }
    }
}