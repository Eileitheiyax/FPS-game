using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;             // Hedef oyuncu
    [SerializeField] float chasingRadius = 6f;     // Takip yar��ap�
    [SerializeField] GameObject attackEffectPrefab; // Sald�r� s�ras�nda oynat�lacak efekt prefab�
    [SerializeField] Transform effectSpawnPoint;   // Efektin ��k�� noktas�
    [SerializeField] int attackDamage = 10;        // D��man�n verdi�i hasar miktar�

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float targetDistance = Mathf.Infinity;
    private bool isProved = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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

    // Animasyon olay� ile �a�r�lacak fonksiyon
    public void PerformAttack()
    {
        // Hasar verme i�lemi
        DealDamageToPlayer();

        // Efekt oynatma
        if (attackEffectPrefab != null && effectSpawnPoint != null)
        {
            Instantiate(attackEffectPrefab, effectSpawnPoint.position, effectSpawnPoint.rotation);
        }

        Debug.Log("Enemy performed an attack!");
    }

    private void DealDamageToPlayer()
    {
        // Oyuncuya hasar verme i�lemi
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chasingRadius);
    }
}
