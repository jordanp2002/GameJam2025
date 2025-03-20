using UnityEngine;
using System.Collections;

public class CorvetteAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f;

    void Start()
    {
        StartCoroutine(FireRoutine());
    }

    IEnumerator FireRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Debug.Log(gameObject.name + " fired a projectile!");
        }
    }
}
