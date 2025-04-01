using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour, IHealth
{
    public float maxHealth = 100f;
    private float currentHealth;
    private Renderer enemyRenderer;
    private Color originalColor;
    public GameObject soulPrefab;

    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private AudioClip explosionSound;
    private AudioSource audioSource;

    void Start()
    {
        GameManager.Instance?.RegisterEnemy();
        currentHealth = maxHealth;
        enemyRenderer = GetComponentInChildren<Renderer>();
        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.material.color;
        }
    }

    public void TakeDamage(float damage, GameObject source)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. Remaining health: " + currentHealth);

        if (enemyRenderer != null)
        {
            StartCoroutine(FlashRed());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashRed()
    {
        enemyRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        enemyRenderer.material.color = originalColor;
    }

    void Die()
    {
        int scoreValue = 100;

        if (gameObject.CompareTag("Corvette"))
            scoreValue = 500;
        else if (gameObject.CompareTag("Cruiser"))
            scoreValue = 500;
        else if (gameObject.CompareTag("Destroyer"))
            scoreValue = 1000;
        else if (gameObject.CompareTag("Frigate"))
            scoreValue = 250;
        else if (gameObject.CompareTag("Boss"))
        {
            scoreValue = 5000;
            GameManager.Instance.AddScore(scoreValue);

            // Spawn Particle Explosion
            if (explosionEffect != null)
            {
                GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                Destroy(explosion, 3f);
            }

            // Play Sound
            if (explosionSound != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            }

            StartCoroutine(DelayedEndLevel());
        }

        GameManager.Instance.AddScore(scoreValue);

        // Spawn Particle Explosion
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 3f);
        }

        // Play Sound
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        DropSoul();
        GameManager.Instance?.EnemyKilled();
        Destroy(gameObject);
    }

    IEnumerator DelayedEndLevel()
    {
        yield return new WaitForSeconds(4f);
        GameManager.Instance.SetLastCompletedLevel(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.EndLevel();
    }

    void DropSoul()
    {
        int soulCount = 1;

        if (gameObject.CompareTag("Corvette") || gameObject.CompareTag("Destroyer") || gameObject.CompareTag("Frigate"))
            soulCount = Random.Range(2, 11);
        else if (gameObject.CompareTag("Cruiser"))
            soulCount = Random.Range(11, 21);

        float spawnRadius = 1.5f;

        for (int i = 0; i < soulCount; i++)
        {
            Vector3 offset = Random.insideUnitSphere * spawnRadius;
            offset.y = 0;
            Instantiate(soulPrefab, transform.position + offset, Quaternion.identity);
        }
    }
}
