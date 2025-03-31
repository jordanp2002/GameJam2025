using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public static PlayerHealth Instance;
    public float maxHealth = 200f;
    public float currentHealth;
    private Renderer playerRenderer;
    private Color originalColor;
    public Slider healthBar;
    public float damageResistance = 0f;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        playerRenderer = GetComponentInChildren<Renderer>();
        if (playerRenderer != null)
        {
            Debug.Log("Setting original color!");
            Debug.Log("Current Health" + currentHealth);
            originalColor = playerRenderer.material.color;
        }
    }

    public void TakeDamage(float damage, GameObject source)
    {
        float reducedDamage = damage * (1f - damageResistance);
        currentHealth -= reducedDamage;
        Debug.Log($"You took {reducedDamage} damage (original {damage} reduced by {damageResistance:P}). Remaining health: {currentHealth}");
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

    public void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth;
        }
    }
    
    void Die()
    {
        Debug.Log("Dragon has died!");
        GameManager.Instance.EndGame();
        Destroy(gameObject); 
    }
}
