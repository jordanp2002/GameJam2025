using UnityEngine;

public class CruiserAttackPattern : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint1;
    public Transform firePoint2;
    public float fireRate = 1f;
    public float projectileSpeed = 10f;
    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        FireProjectile(firePoint1);
        FireProjectile(firePoint2);
    }

    void FireProjectile(Transform firePoint)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * projectileSpeed;
        }
    }
}
