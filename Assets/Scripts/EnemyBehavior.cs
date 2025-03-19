using UnityEngine;

public class Enemy : MonoBehaviour
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