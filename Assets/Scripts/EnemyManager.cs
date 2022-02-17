using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
    public EnemyDataContainer EDC;
    public EnemyData enemyData;
    public GameObject target;
    public PlayerControl playerControl;
    // Start is called before the first frame update
    void Start()
    {
        EDC = GameObject.FindGameObjectWithTag("EDC").GetComponent<EnemyDataContainer>();
        target = GameObject.FindGameObjectWithTag("Player");
        playerControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    { 
        ManageEnemyLogic();
        ManageEnemyHP();
    }

    void ManageEnemyLogic()
    {
        for (int i = 0; i < EDC.enemies.Count; i++)
        {
            if (EDC.enemyData[i] != null)
            {
                switch (EDC.enemyData[i].logicID)
                {
                    case 0:
                        //Debug.Log(EDC.enemies[i] + "is Idle");
                        //Idle
                        EDC.enemyData[i].Scan(EDC.enemies[i], EDC.enemyData[i].newRoT,EDC.enemyData[i].reverseRoT ,EDC.enemyData[i].rotateDur);
                        break;
                    case 1:
                        //Debug.Log(EDC.enemies[i] + "is pursuing");
                        EDC.enemyData[i].Pursue(target, EDC.enemies[i], EDC.enemyData[i].moveSpeed, playerControl.playerData.walkSpeed,EDC.enemyData[i].agent,playerControl);
                        break;
                    case 2:
                        //Debug.Log(EDC.enemies[i] + "is Wandering");
                        EDC.enemyData[i].Wander(EDC.enemies[i], EDC.enemyData[i].agent);
                        break;
                    case 3:
                        //Debug.Log(EDC.enemies[i] + "is Hiding");
                        EDC.enemyData[i].Hide(target, EDC.enemies[i], EDC.enemyData[i].agent);                   
                        break;
                }
            }

            
        }
       
    }

    void ManageEnemyBehavior()
    {
        for (int i = 0; i < EDC.enemies.Count; i++)
        {
            if (EDC.enemyData[i] != null)
            {
                
            }
        }
    }
    void ManageEnemyHP()
    {
        
        foreach (GameObject enemy in EDC.enemies)
        {
            enemyData = enemy.GetComponent<EnemyData>();
            if (enemyData.health <= 0)
            {
                EDC.removeEnemies.Add(enemy);
                EDC.removeEnemyData.Add(enemyData);
                EDC.enemyCount--;
            }

            else if (enemyData.health > 0 && enemyData.health <= enemyData.hpThreshold)
            {
                enemyData.logicID = 3;
            }
            else if (enemyData.health > enemyData.hpThreshold)
            {
                ManageEnemyBehavior(); 
            }
             
        }

        foreach (GameObject enemy in EDC.removeEnemies)
        {
            EDC.enemies.Remove(enemy);
            Destroy(enemy);
        }
        foreach (EnemyData enemyData in EDC.removeEnemyData)
        {
            EDC.enemyData.Remove(enemyData);
        }
    }
}
