using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chasingRadius = 6f;

    NavMeshAgent navMeshAgent;
    float targetDistance = Mathf.Infinity;
    bool isProved = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        targetDistance = Vector3.Distance(target.position, transform.position);

        if(isProved )
        {
            DelayWithTarget();
        }

        else if (targetDistance <= chasingRadius)
        {
            isProved = true;
        }

    }
     
    private void DelayWithTarget()
    {
        if (targetDistance >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (targetDistance <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        Debug.Log(name + "is destroying" + target.name);
    }

    private void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
    }

     void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chasingRadius);
    }

}

