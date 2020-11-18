using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //config params
    [SerializeField]
    List<WaveConfig> waveConfigs;
    [SerializeField]
    int startingWave = 0;
    [SerializeField]
    private bool looping;
    private bool waveGenerator = true;
    private int waveSpawnCooldown = 0;
    private float waveSpawnTimer;


    //state
    WaveConfig currentWave;
    private void Start()
    {
        //Initialize First Wave
        currentWave = waveConfigs[0];
        waveSpawnCooldown = currentWave.TimeToPrepareWave;
    }
    private void Update()
    {
        Debug.Log(waveSpawnCooldown);
        HandleGenerateWaves();
    }

    private void HandleGenerateWaves()
    {
        waveSpawnTimer += Time.deltaTime;
        if (waveSpawnTimer >= waveSpawnCooldown && looping)
        {
            waveGenerator = true;
        }
        if (waveGenerator)
        {
            waveGenerator = false;
            waveSpawnTimer = 0;
            StartCoroutine(SpawnAllWaves());
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        currentWave = waveConfigs[Random.Range(0, waveConfigs.Count)];
        waveSpawnCooldown = currentWave.TimeToPrepareWave;
        yield return StartCoroutine(SpawnAllEnenmiesInWave(currentWave));
    }
    private IEnumerator SpawnAllEnenmiesInWave(WaveConfig waveConfig)
    {
        
        for (int enemyCount = 0; enemyCount < waveConfig.NumberOfEnemies; enemyCount++)
        {
            GameObject newEnemy = (GameObject) Instantiate(waveConfig.EnemyPrefab,
                waveConfig.EnemyWayPoints[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<Enemy>().MovementSpeed = waveConfig.MovementSpeed;
            newEnemy.GetComponent<Enemy>().WayPoints = waveConfig.EnemyWayPoints;
            yield return new WaitForSeconds(waveConfig.TimeBetweenSpawns + Random.Range(0f, waveConfig.SpawnRandomFactor));
        }
    }
}
