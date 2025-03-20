using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile hit: " + other.gameObject.name);

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit an enemy!");
        }
    }
}