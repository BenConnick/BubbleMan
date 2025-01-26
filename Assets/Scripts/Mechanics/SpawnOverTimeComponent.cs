using Unity.Mathematics;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class SpawnOverTimeComponent : MonoBehaviour
    {
        public GameObject Spawn;
        public Vector3 SpawnOffset;
        public float SpawnInterval = 1;
        
        private float LastSpawnTime = 0;

        public void Update()
        {
            if (Time.time - LastSpawnTime > SpawnInterval)
            {
                Instantiate(Spawn, transform.position + SpawnOffset, quaternion.identity);
                LastSpawnTime = Time.time;
            }
        }
    }
}