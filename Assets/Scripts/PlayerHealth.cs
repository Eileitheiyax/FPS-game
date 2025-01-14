using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;           // Maksimum sa�l�k
    private int currentHealth;                     // Mevcut sa�l�k
    [SerializeField] GameObject gameOverPanel;     // Game Over ekran�
    [SerializeField] TMP_Text buttonText;          // Restart butonunun TextMeshPro metni
    [SerializeField] TMP_Text healthText;          // Sa�l�k g�stergesi i�in TextMeshPro metni

    void Start()
    {
        currentHealth = maxHealth;

        // Sa�l�k metnini ba�lang��ta g�ncelle
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

        // Fare imlecini ba�lang��ta gizle
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Sa�l�k de�erinin s�f�r�n alt�na d��mesini engelle
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);

        // UI'y� g�ncelle
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");

        // Game Over ekran�n� g�ster
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;

        // Fareyi serbest b�rak ve g�r�n�r yap
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        // Oyunu yeniden ba�lat
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
            healthText.text = "Health: " + currentHealth; // UI metnini g�ncelle
        }
    }
}
