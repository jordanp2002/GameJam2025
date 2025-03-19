using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    public float projectileSpeed = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnProjectile();
        }
    }

    void SpawnProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = spawnPoint.forward * projectileSpeed;
            Debug.Log("Projectile velocity set to: " + rb.linearVelocity);
        }
        else
        {
            Debug.LogError("Projectile prefab is missing a Rigidbody!");
        }
    }
}