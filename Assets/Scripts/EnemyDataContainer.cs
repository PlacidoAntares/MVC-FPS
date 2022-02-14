using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDataContainer : MonoBehaviour
{
    public int maxEnemies;
    public int enemyCount;
    public int enemySpawned;
    public Vector3[] spawnPoints;
    public List<GameObject> enemies = new List<GameObject>();
    public List<EnemyData>  enemyData = new List<EnemyData>();
    public List<GameObject> removeEnemies = new List<GameObject>();
    public List<EnemyData>  removeEnemyData = new List<EnemyData>();

}
