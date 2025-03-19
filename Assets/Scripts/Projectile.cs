using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f; // Adjustable for powerups. Dmg per projectile
    void OnTriggerEnter(Collider other)
    {
        // Debugging to ensure collisons are being detected
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit an enemy!");
        }

        Health targetHealth = other.GetComponentInParent<Health>();

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage, gameObject);
            Destroy(gameObject);
        }
    }
}