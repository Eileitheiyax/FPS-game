using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;             // Hedef oyuncu
    [SerializeField] float chasingRadius = 6f;     // Takip yarýçapý
    [SerializeField] GameObject attackEffectPrefab; // Saldýrý sýrasýnda oynatýlacak efekt prefabý
    [SerializeField] Transform effectSpawnPoint;   // Efektin çýkýþ noktasý
    [SerializeField] int attackDamage = 10;        // Düþmanýn verdiði hasar miktarý

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

    // Animasyon olayý ile çaðrýlacak fonksiyon
    public void PerformAttack()
    {
        // Hasar verme iþlemi
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
        // Oyuncuya hasar verme iþlemi
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
