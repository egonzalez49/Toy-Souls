using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        public int currentEnemies = 0;
        //public PlayerHealth playerHealth;
        public GameObject enemy;
        public float spawnTime = 11f;
        public Transform[] spawnPoints;
        public int maxEnemies = 6;

        // Start is called before the first frame update
        void Start()
        {
            Spawn();
            InvokeRepeating("Spawn", spawnTime, spawnTime); //calls a function on repeat, only once every so many seconds
        }

        // Update is called once per frame
        void Spawn()
        {
            if (currentEnemies < maxEnemies)
            {
                int spawnPointIndex = Random.Range(0, spawnPoints.Length - 1);
                Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                currentEnemies += 1;
            }
        }
    }
}