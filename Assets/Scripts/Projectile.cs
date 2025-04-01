using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Adjustable for powerups
    public float damage = 10f;
    public float speed = 10f;
    public float lifetime = 5f;
    public GameObject source;
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * speed;
            Debug.Log("Projectile velocity set to: " + rb.linearVelocity);
        }
        else
        {
            Debug.LogError("Projectile is missing a Rigidbody!");
        }

        Destroy(gameObject, lifetime);
    }
    void Update()
    {
        if (Camera.main == null)
            return;
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.x < 0f || viewportPos.x > 1f || viewportPos.y < 0f || viewportPos.y > 1f){
            Debug.Log("Destroyed Enemy Projectile");
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == source)
        {
            Debug.Log("Ignored collision with source");
            return;
        }

        if (CompareTag("DragonProjectile") && other.CompareTag("Player"))
        {
            Debug.Log("Ignored collision with dragon's own projectile");
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit an enemy!");
        }

        IHealth targetHealth = other.GetComponentInParent<IHealth>();

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage, source);
            Destroy(gameObject);
        }
    }
}