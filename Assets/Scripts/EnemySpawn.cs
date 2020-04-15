using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave //A serializable class for enemy waves
{
    public int EnemyCount;
    public GameObject enemy;
}
public class EnemySpawn : MonoBehaviour //Script to spawn enemies
{
    public EnemyWave[] enemyWaves; //Array for individual waves
    public Transform[] SpawnPoints; //Where enemies spawn from
    public float timeBetweenEnemies = 2f; //time between waves

    private int currentEnemyCount; //How many enemies are there total in wave
    private int remainingEnemies; //enemies that are left in the wave currently
    private int spawnedEnemies; //spawned number of enemies already

    private int currentWave;
    private int totalWaves;

    private GameManager gameManager;

    public Transform[] patrolPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        currentWave = -1;
        totalWaves = enemyWaves.Length - 1;

        StartNextWave();
        gameManager = GetComponentInParent<GameManager>();
    }

    void StartNextWave()
    {
        currentWave++;

        if(currentWave > totalWaves) //Condition to check if player has won or not by beating the waves
        {
            gameManager.playerWin();
        }

        currentEnemyCount = enemyWaves[currentWave].EnemyCount;
        remainingEnemies = 0;
        spawnedEnemies = 0;

        StartCoroutine(Spawn());
    }

    //Does exactly what it's named after
    IEnumerator Spawn()
    {
        GameObject enemy = enemyWaves[currentWave].enemy;
        while(spawnedEnemies < currentEnemyCount)
        {
            spawnedEnemies++;
            remainingEnemies++;

            int spawnPoint = Random.Range(0, SpawnPoints.Length);

            GameObject newEnemy = Instantiate(enemy, SpawnPoints[spawnPoint].position, SpawnPoints[spawnPoint].rotation) as GameObject;
            if(newEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
            {
                newEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(SpawnPoints[spawnPoint].position);
            }
            yield return new WaitForSeconds(timeBetweenEnemies);
        }

        yield return null;
    }

    //Does exactly what it's named after
    public void enemyDeath()
    {
        remainingEnemies--;

        if (remainingEnemies == 0 && spawnedEnemies == currentEnemyCount)
        {
            StartNextWave();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
