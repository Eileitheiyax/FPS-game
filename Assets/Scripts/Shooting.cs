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
    [SerializeField] AudioClip gunshotSound; // Ateþ sesi
    [Range(0f, 1f)][SerializeField] float gunshotVolume = 0.5f; // Ses seviyesi

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
        PlayGunshotSound();
        Raycasting();
    }

    private void Raycasting()
    {
        RaycastHit hit;

        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            HitEffect(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;

            target.Getdamage(damageAmount);
        }
    }

    private void HitEffect(RaycastHit hit)
    {
        GameObject hitVisual = Instantiate(explosion, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(hitVisual, 1f);
    }

    private void ShootingEffect()
    {
        if (shoot != null)
        {
            shoot.Play();
        }
    }

    private void PlayGunshotSound()
    {
        if (audioSource != null && gunshotSound != null)
        {
            audioSource.PlayOneShot(gunshotSound, gunshotVolume);
        }
    }
}
