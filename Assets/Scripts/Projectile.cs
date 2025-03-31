using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Adjustable for powerups
    public float damage = 10f;
    public float speed = 10f;
    public float lifetime = 5f;

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

    void OnTriggerEnter(Collider other)
    {
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
            targetHealth.TakeDamage(damage, gameObject);
            Destroy(gameObject);
        }
    }
}
