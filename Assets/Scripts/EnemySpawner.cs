using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemySpawner : MonoBehaviour
{
    public EnemyDataContainer EDC;
    public GameObject enemyPrefab;
    GameObject tempHolder;
    // Start is called before the first frame update
    void Start()
    {
        EDC = GameObject.FindGameObjectWithTag("EDC").GetComponent<EnemyDataContainer>();
        while (EDC.enemyCount < EDC.maxEnemies)
        {
            InitialSpawnEnemies();
        }
    }

    // Update is called once per frame
    void Update()
    {
        RespawnEnemies();
    }

    void InitialSpawnEnemies()
    {
        if (EDC.enemyCount < EDC.maxEnemies)
        {
            tempHolder = Instantiate(enemyPrefab, EDC.spawnPoints[EDC.enemyCount], Quaternion.identity);
            tempHolder.name = "Enemy" + EDC.enemySpawned;
            EDC.enemyCount++;
            EDC.enemySpawned++;
        }
    }

    void RespawnEnemies()
    {
        if (EDC.enemyCount < EDC.maxEnemies)
        {
            Debug.Log("Respawning Enemy");
            tempHolder = Instantiate(enemyPrefab, EDC.spawnPoints[Random.Range(0, EDC.spawnPoints.Length -1)], Quaternion.identity);
            tempHolder.name = "Enemy" + EDC.enemySpawned;
            EDC.enemyCount++;
            EDC.enemySpawned++;
        }
    }
}
