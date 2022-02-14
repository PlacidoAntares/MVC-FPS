using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyData : MonoBehaviour
{
    public float moveSpeed;
    public float health;
    public float hpThreshold;
    public Vector3 currentDestination;
    public NavMeshAgent agent;
    public EnemyDataContainer EDC;
    public int logicID;
    public Vector3 targetloc;
    public float rotationSpeed;
    public float visDist = 20.0f;
    public float visAngle = 30.0f;
    public float detectRange = 10.0f;
    //
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        EDC = GameObject.FindGameObjectWithTag("EDC").GetComponent<EnemyDataContainer>();
        EDC.enemies.Add(this.gameObject);
        EDC.enemyData.Add(this);
    }


    public void Seek(Vector3 location, NavMeshAgent agent)
    {
        agent.SetDestination(location);
    }

    Vector3 wanderTarget = Vector3.zero;
    public void Wander(GameObject thisObj,NavMeshAgent agent)
    {
        float wanderRadius = 10;
        float wanderDistance = 10;
        float wanderJitter = 1;
        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0,0,wanderDistance);
        Vector3 targetWorld = thisObj.transform.InverseTransformVector(targetLocal);
        Seek(targetWorld, agent);
    }

    public void Evade(GameObject target,GameObject thisPos,float agentSpeed,float playerSpeed,NavMeshAgent agent,PlayerControl pc)
    {
        Vector3 targetDir = target.transform.position - thisPos.transform.position;
        float lookAhead = targetDir.magnitude / (agentSpeed + playerSpeed);
        Flee(target.transform.position + target.transform.forward * lookAhead, agent);
    }
    public void Flee(Vector3 location, NavMeshAgent agent)
    {
        Vector3 fleeVector = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }

    public void Pursue(GameObject target, GameObject thisPos, float agentSpeed, float playerSpeed, NavMeshAgent agent, PlayerControl pc)
    {
        Vector3 targetDir = target.transform.position - thisPos.transform.position;
        float relativeHeading = Vector3.Angle(thisPos.transform.forward, thisPos.transform.TransformVector(target.transform.forward));
        float toTarget = Vector3.Angle(thisPos.transform.forward, thisPos.transform.TransformVector(targetDir));
        if ((toTarget > 90 && relativeHeading < 20) || (pc.playerData.velocity.x != 0.0f && pc.playerData.velocity.z != 0.0f))
        {
            //Debug.Log("Seeking");
            Seek(target.transform.position, agent);
            return;
        }
        //Debug.Log("Pursuing");
        float lookAhead = targetDir.magnitude / (agentSpeed + playerSpeed);
        Seek(target.transform.position + target.transform.forward * lookAhead,agent);
    }

    public void Hide(GameObject target,GameObject thisObj,NavMeshAgent agent)
    {
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;
        GameObject chosenGO = WorldData.Instance.GetHidingSpots()[0];
        for (int i = 0; i < WorldData.Instance.GetHidingSpots().Length; i++)
        {
            Vector3 hideDir = WorldData.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePos = WorldData.Instance.GetHidingSpots()[i].transform.position + hideDir.normalized * 5;
            if (Vector3.Distance(thisObj.transform.position, hidePos) < dist)
            {
                chosenSpot = hidePos;
                chosenDir = hideDir;
                chosenGO = WorldData.Instance.GetHidingSpots()[i];
                dist = Vector3.Distance(thisObj.transform.position, hidePos);
            }
        }

        Collider hideCol = chosenGO.GetComponent<Collider>();
        Ray backRay = new Ray(chosenSpot, -chosenDir.normalized);
        RaycastHit info;
        float distance = 100.0f;
        hideCol.Raycast(backRay, out info, distance);

        Seek(info.point + chosenDir.normalized * 5,agent);
    }

    public bool CanSeeTarget(GameObject target,GameObject thisObj)
    {
        Vector3 direction = target.transform.position - thisObj.transform.position;
        float angle = Vector3.Angle(direction,this.transform.forward);
        if (direction.magnitude < visDist && angle < visAngle)
        {
            direction.y = 0;
            if (direction.magnitude > detectRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        
    }
}
