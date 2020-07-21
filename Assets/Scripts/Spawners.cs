using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] spawns;
        public int count;
        public int spawner;
        public float spawnRate;
        public float waveTimer;
    }

    public Wave[] waves;
    public Transform[] spawners;
    
    private int indexWave = 0;

    public float firstEnemyCountdown = 4f;
    private float countdown;

    private void Start()
    {
        countdown = firstEnemyCountdown;
    }

    private void Update()
    {
        if (indexWave != waves.Length)
        {  
            if (countdown <= 0)
            {
                StartCoroutine(SpawnWave(waves[indexWave]));
                countdown = waves[indexWave].waveTimer;

                indexWave += 1;
            }
            else
            {
                countdown -= Time.deltaTime;
            }
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        for (int i = 0; i < _wave.count; i++)
        {
            Transform randomSpawn = _wave.spawns[Random.Range(0, _wave.spawns.Length - 1)];

            SpawnObject(randomSpawn,_wave.spawner);

            yield return new WaitForSeconds(_wave.spawnRate);
        }        

        yield break;
    }

    void SpawnObject(Transform spawn,int spawnerUsed)
    {
        Transform randomSpawner;

        if (spawnerUsed == 0)
        {
            randomSpawner = spawners[Random.Range(0, spawners.Length - 1)];
        }
        else
        {
            randomSpawner = spawners[spawnerUsed - 1];
        }
        

        Instantiate(spawn, randomSpawner.position, randomSpawner.rotation);
                    
    }

}
