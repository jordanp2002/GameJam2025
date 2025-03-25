using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public float maxHealth = 100f;
    private float currentHealth;
    private Renderer playerRenderer;
    private Color originalColor;
    public Slider healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        playerRenderer = GetComponentInChildren<Renderer>();
        if (playerRenderer != null)
        {
            Debug.Log("Setting original color!");
            originalColor = playerRenderer.material.color;
        }
    }

    public void TakeDamage(float damage, GameObject source)
    {
        currentHealth -= damage;
        Debug.Log("You took " + damage + " damage. Remaining health: " + currentHealth);
        UpdateHealthBar();

        if (playerRenderer != null)
        {
            Debug.Log("Starting coroutine!");
            StartCoroutine(FlashRed());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashRed()
    {
        Debug.Log("Flashing red!");
        playerRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        playerRenderer.material.color = originalColor;
        Debug.Log("Flashing back to normal! " + originalColor);
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        Destroy(gameObject); // Remove later for death animations
    }
}
