using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile hit: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player was hit!");
        }

        if (other.CompareTag("Soul"))
        {
            Debug.Log("Collected a Soul!");
            GameManager.Instance.AddScore(100);
            Destroy(other.gameObject);
        }
    }
}
