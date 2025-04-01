using UnityEngine;
public class BossScript : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 5f;
    [SerializeField] private float projectileSpeed = 20f;

    private float nextFireTime = 0f;

    private Renderer rend;
    private Transform player;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (rend != null && rend.isVisible)
        {
            if (Time.time >= nextFireTime)
            {
                TrackAndShoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void TrackAndShoot()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - firePoint.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = direction * projectileSpeed;
            }
        }
    }
}
