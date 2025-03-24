using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour, IHealth
{
    public float maxHealth = 100f;
    private float currentHealth;
    private Renderer enemyRenderer;
    private Color originalColor;
    public GameObject soulPrefab;

    void Start()
    {
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

        if (gameObject.CompareTag("EnemyCorvette"))
            scoreValue = 500;
        else if (gameObject.CompareTag("EnemyCruiser"))
            scoreValue = 500;
        else if (gameObject.CompareTag("EnemyDestroyer"))
            scoreValue = 1000;
        else if (gameObject.CompareTag("EnemyFrigate"))
            scoreValue = 250;

        GameManager.Instance.AddScore(scoreValue);

        DropSoul();

        Destroy(gameObject);
    }

    void DropSoul()
    {
        int soulCount = 1;

        if (gameObject.CompareTag("EnemyCorvette") || gameObject.CompareTag("EnemyDestroyer") || gameObject.CompareTag("EnemyFrigate"))
            soulCount = Random.Range(1, 6); 
        else if (gameObject.CompareTag("EnemyDestroyer"))
            soulCount = Random.Range(5, 11); 

        float spawnRadius = 1.5f;

        for (int i = 0; i < soulCount; i++)
        {
            Vector3 offset = Random.insideUnitSphere * spawnRadius;
            offset.y = 0;
            Instantiate(soulPrefab, transform.position + offset, Quaternion.identity);
        }
    }
}
