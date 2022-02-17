using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float projDmg;
    public float projDur;
    public EnemyData enemyData;

    private void Start()
    {
        Destroy(this.gameObject,projDur);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyData = other.gameObject.GetComponent<EnemyData>();
            enemyData.health -= projDmg;
            enemyData.logicID = 1;
            Destroy(this.gameObject);
        }

    }
}
