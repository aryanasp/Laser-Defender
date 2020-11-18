using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    GameObject enemyPathPrefab;
    List<Transform> enemyWayPoints;
    [SerializeField]
    float timeBetweenSpawns = 0.5f;
    [SerializeField]
    float spawnRandomFactor = 0.3f;
    [SerializeField]
    int numberOfEnemies = 5;
    [SerializeField]
    int movementSpeed = 8;
    [SerializeField]
    int mintimeToPrepareWave = 2;
    [SerializeField]
    int maxTimePrepareWave = 8;
    int timeToPrepareWave;
    [SerializeField]
    float health;
    [SerializeField]
    float powerFire;
    [SerializeField]
    float fireCooldown;
    [SerializeField]
    float crashDamage;

    public GameObject EnemyPrefab { get => enemyPrefab;}
    public List<Transform> EnemyWayPoints 
    {
        get 
        {
            enemyWayPoints = new List<Transform>();
            foreach(Transform child in enemyPathPrefab.transform)
            {
                enemyWayPoints.Add(child);
            }
            return enemyWayPoints;
        } 
    }
    public float TimeBetweenSpawns { get => timeBetweenSpawns;}
    public float SpawnRandomFactor { get => spawnRandomFactor;}
    public int NumberOfEnemies { get => numberOfEnemies;}
    public int MovementSpeed { get => movementSpeed;}
    public int TimeToPrepareWave 
    {
        get
        {
            timeToPrepareWave = Random.Range(mintimeToPrepareWave, maxTimePrepareWave);
            return timeToPrepareWave;
        }
    }

    public float Health { get => health;}
    public float PowerFire { get => powerFire; set => powerFire = value; }
    public float FireCooldown { get => fireCooldown; set => fireCooldown = value; }
    public float CrashDamage { get => crashDamage; set => crashDamage = value; }
}
