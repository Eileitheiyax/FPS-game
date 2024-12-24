using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damageAmount = 45f;
    [SerializeField] ParticleSystem shoot;
    [SerializeField] GameObject explosion;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
        

}

    void Shoot()
    {
        ShootingEffect();
        Raycasting();
    }

    private void Raycasting()
    {
        RaycastHit hit;

        if(Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            HitEffect(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.Getdamage(damageAmount);

        }
        else
        {
            return;
        }
    }


    private void HitEffect(RaycastHit hit)
    {
        GameObject hitVisual = Instantiate(explosion, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(hitVisual,1f);
    }

    private void ShootingEffect()
    {
        shoot.Play();
    }
}
