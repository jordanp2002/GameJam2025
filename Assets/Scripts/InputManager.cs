using UnityEngine;
using UnityEngine.Audio;

public class InputManager : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    public AudioClip shootSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
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

            if (shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
        }
        else
        {
            Debug.LogError("Projectile prefab or spawn point is missing!");
        }
    }
}