using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnProjectile();
        }
    }

    void SpawnProjectile()
    {
        if (projectilePrefab != null && spawnPoint != null)
        {
            Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Projectile prefab or spawn point is missing!");
        }
    }
}