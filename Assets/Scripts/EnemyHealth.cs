using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    public void Getdamage (float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }

    }
}
