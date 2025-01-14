using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chasingRadius = 6f;
    [SerializeField] RuntimeAnimatorController animatorController; // Her prefab için farklý Animator Controller

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float targetDistance = Mathf.Infinity;
    private bool isProved = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Prefab'a özgü Animator Controller'ý ata
        if (animatorController != null)
        {
            animator.runtimeAnimatorController = animatorController;
        }
    }

    void Update()
    {
        targetDistance = Vector3.Distance(target.position, transform.position);

        if (isProved)
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
        animator.SetBool("attack", true);
        Debug.Log(name + " is attacking " + target.name);
    }

    private void ChaseTarget()
    {
        animator.SetBool("attack", false);
        animator.SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chasingRadius);
    }
}
