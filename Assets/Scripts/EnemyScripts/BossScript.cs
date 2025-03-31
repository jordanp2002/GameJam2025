using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 5f;
    [SerializeField] private int circularProjectileCount = 12;
    [SerializeField] private float circularSpreadRadius = 15f;
    [SerializeField] private float projectileSpeed = 20f;

    private Transform player;
    private bool isAttacking = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        StartCoroutine(AttackPattern());
    }

    IEnumerator AttackPattern()
    {
        while (true)
        {
            int attackType = Random.Range(0, 2);
            if (attackType == 0)
            {
                CircularSpreadAttack();
            }
            //else
            //{
            //    LineAttack();
            //}
            yield return new WaitForSeconds(fireRate);
        }
    }

    void CircularSpreadAttack()
    {
        for (int i = 0; i < circularProjectileCount; i++)
        {
            float angle = i * (360f / circularProjectileCount);
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;

            // Offset the spawn position outward to prevent overlap
            Vector3 spawnPosition = firePoint.position + direction * 1.5f; // Adjust distance as needed
            SpawnProjectile(spawnPosition, direction);
        }
    }

    void SpawnProjectile(Vector3 position, Vector3 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Combine forward movement with outward expansion
            Vector3 combinedDirection = (direction + Vector3.forward).normalized;
            rb.linearVelocity = combinedDirection * projectileSpeed;
        }
    }
}
