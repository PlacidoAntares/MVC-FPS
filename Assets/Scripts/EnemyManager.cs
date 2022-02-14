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
                        Debug.Log(EDC.enemies[i] + "is Idle");
                        //Idle
                        break;
                    case 1:
                        Debug.Log(EDC.enemies[i] + "is pursuing");
                        EDC.enemyData[i].Pursue(target, EDC.enemies[i], EDC.enemyData[i].moveSpeed, playerControl.playerData.walkSpeed,EDC.enemyData[i].agent,playerControl);
                        break;
                    case 2:
                        Debug.Log(EDC.enemies[i] + "is Fleeing");
                        EDC.enemyData[i].Flee(target.transform.position, EDC.enemyData[i].agent);
                        break;
                    case 3:
                        Debug.Log(EDC.enemies[i] + "is Evading");
                        EDC.enemyData[i].Evade(target,EDC.enemies[i], EDC.enemyData[i].moveSpeed, playerControl.playerData.walkSpeed, EDC.enemyData[i].agent, playerControl);
                        break;
                    case 4:
                        Debug.Log(EDC.enemies[i] + "is Wandering");
                        EDC.enemyData[i].Wander(EDC.enemies[i], EDC.enemyData[i].agent);
                        break;
                    case 5:
                        Debug.Log(EDC.enemies[i] + "is Hiding");
                        EDC.enemyData[i].Hide(target, EDC.enemies[i], EDC.enemyData[i].agent);                   
                        break;
                }
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

            if (enemyData.health <= enemyData.hpThreshold && enemyData.health > 0)
            {
                if (enemyData.CanSeeTarget(target, enemy) == true)
                {
                    enemyData.logicID = 5;
                }
                else if (enemyData.CanSeeTarget(target, enemy) == false)
                {
                    enemyData.logicID = 0;
                }
            }

            if (enemyData.health > enemyData.hpThreshold)
            {
                if (enemyData.CanSeeTarget(target, enemy) == true)
                {
                    enemyData.logicID = 1;
                }
                else if (enemyData.CanSeeTarget(target, enemy) == false)
                {
                
                    enemyData.logicID = 4;
                }
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
