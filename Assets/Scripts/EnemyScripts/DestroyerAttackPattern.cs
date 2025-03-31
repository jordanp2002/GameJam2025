using UnityEngine;

public class DestroyerAttackPattern : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 3f;
    public float spreadAngle = 15f;
    public float projectileSpeed = 10f;
    private float nextFireTime = 0f;

    private Renderer rend;

    void Start()
    {
 
        rend = GetComponentInChildren<Renderer>();
    }
    void Update()
    {
        if (rend != null && rend.isVisible) { 
            if (Time.time >= nextFireTime)
            {
                ShootSpread();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void ShootSpread()
    {
        for (int i = -1; i <= 1; i++)
        {
            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, i * spreadAngle, 0);
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = rotation * Vector3.forward * projectileSpeed;
            }
        }
    }
}
