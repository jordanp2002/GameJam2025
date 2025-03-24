using UnityEngine;
using System.Collections; 

public class CarrierAttackPattern : MonoBehaviour
{
    public LineRenderer laserBeam;
    public Transform firePoint;
    public float laserDuration = 0.5f;
    public float fireRate = 2f;
    public float laserRange = 20f;
    public float laserDamage = 10f;
    private float nextFireTime = 0f;

    private Renderer rend;
    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
    }
    void Update()
    {
        if (rend != null && rend.isVisible)
        {
            if (Time.time >= nextFireTime)
            {
                FireLaser();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    IEnumerator FireLaser()
    {
        laserBeam.enabled = true;

        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, laserRange))
        {
            IHealth targetHealth = hit.collider.GetComponentInParent<IHealth>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(laserDamage, gameObject);
            }
        }

        yield return new WaitForSeconds(laserDuration);
        laserBeam.enabled = false;
    }
}
