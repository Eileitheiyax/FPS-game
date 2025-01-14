using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;           // Maksimum saðlýk
    private int currentHealth;                     // Mevcut saðlýk
    [SerializeField] GameObject gameOverPanel;     // Game Over ekraný
    [SerializeField] TMP_Text buttonText;          // Restart butonunun TextMeshPro metni
    [SerializeField] TMP_Text healthText;          // Saðlýk göstergesi için TextMeshPro metni

    void Start()
    {
        currentHealth = maxHealth;

        // Saðlýk metnini baþlangýçta güncelle
        UpdateHealthUI();

        // Game Over panelini gizle
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Restart butonu metnini ayarla
        if (buttonText != null)
        {
            buttonText.text = "Restart";
        }

        // Fare imlecini baþlangýçta gizle
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Saðlýk deðerinin sýfýrýn altýna düþmesini engelle
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);

        // UI'yý güncelle
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");

        // Game Over ekranýný göster
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;

        // Fareyi serbest býrak ve görünür yap
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        // Oyunu yeniden baþlat
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        // Fareyi tekrar kilitle ve gizle
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth; // UI metnini güncelle
        }
    }
}
