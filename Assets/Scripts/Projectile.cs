using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit an enemy!");
        }
    }
}