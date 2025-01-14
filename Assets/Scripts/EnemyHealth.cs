using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;       // Düþmanýn maksimum saðlýðý
    private float currentHealth;                  // Düþmanýn mevcut saðlýðý
    [SerializeField] AudioClip deathSound;        // Ölüm sesi
    [SerializeField] float deathSoundVolume = 0.7f; // Ses seviyesi

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Getdamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log(gameObject.name + " took " + damageAmount + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");

        // Ölüm sesi çal
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
        }

        // Nesneyi yok et
        Destroy(gameObject);
    }
}
