using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public float spawnDelay = 0.85f;

    public GameObject carPrefab;

    private float NextTimeToSpawn;

    void Update()
    {               
                            // Tempo inicial do jogo
        if(NextTimeToSpawn <= Time.time)
        {
            SpawnCar();

            NextTimeToSpawn = Time.time + spawnDelay;
        }
    }

    void SpawnCar()
    {
        Instantiate(carPrefab, transform.position, transform.rotation);
                
    }
}
