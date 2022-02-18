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
                        EDC.enemyData[i].isRotating = true;
                        StartCoroutine(Scanning(EDC.enemyData[i], EDC.enemies[i]));
                        //Idle
                        break;
                    case 1:
                        //Debug.Log(EDC.enemies[i] + "is Wandering");
                        //EDC.enemyData[i].Wander(EDC.enemies[i], EDC.enemyData[i].agent);
                        StartCoroutine(Wandering(EDC.enemyData[i], EDC.enemies[i]));
                        break;
                    case 2:
                        //Debug.Log(EDC.enemies[i] + "is pursuing");
                        EDC.enemyData[i].Pursue(target, EDC.enemies[i], EDC.enemyData[i].moveSpeed, playerControl.playerData.walkSpeed, EDC.enemyData[i].agent, playerControl);                       
                        break;
                    case 3:
                        //Debug.Log(EDC.enemies[i] + "is Hiding");
                        EDC.enemyData[i].Hide(target, EDC.enemies[i], EDC.enemyData[i].agent);                   
                        break;
                }
            }

            
        }
       
    }



    private IEnumerator Wandering(EnemyData ED, GameObject EGO)
    {
        ED.Wander(EGO, ED.agent);
        yield return new WaitForSeconds(10.0f);
        if (ED.logicID == 1)
        {
            ED.logicID = 0;
        }
    }

    private IEnumerator Scanning(EnemyData ED,GameObject EGO)
    {
        ED.Scan(EGO, ED.newRoT, ED.rotateDur, ED.isRotating);
        yield return new WaitForSeconds(ED.rotateDur);
        //Debug.Log("Rotating Counter-clockwise");
        ED.Scan(EGO,ED.reverseRoT, ED.rotateDur,ED.isRotating);
        yield return new WaitForSeconds(ED.rotateDur);
        ED.isRotating = false;
        if (ED.isRotating == false)
        {
            ED.logicID = 1;
        }
        //
        //Debug.Log("Stopping Rotations");
    }
    void ManageEnemyHP()
    {
        for (int i = 0; i < EDC.enemies.Count; i++)
        {
            if (EDC.enemyData[i] != null)
            {
                if (EDC.enemyData[i].health <= 0)
                {
                    EDC.removeEnemies.Add(EDC.enemies[i]);
                    EDC.removeEnemyData.Add(EDC.enemyData[i]);
                    EDC.enemyCount--;
                }
                else if (EDC.enemyData[i].health > 0 && EDC.enemyData[i].health <= EDC.enemyData[i].hpThreshold)
                {
                    EDC.enemyData[i].logicID = 3;
                }
                else if (EDC.enemyData[i].health > EDC.enemyData[i].hpThreshold)
                {

                    
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
