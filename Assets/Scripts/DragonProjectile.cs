using UnityEngine;

public class DragonProjectile : MonoBehaviour
{
    public static float speedMultiplier = 1f;
    public static float damageMultiplier = 1f;

    public float baseDamage = 20f;
    public float baseSpeed = 10f;
    public float lifetime = 5f;

    [SerializeField] private AudioClip shootSound;
    private AudioSource audioSource;

    void Start(){
        float damage = baseDamage * damageMultiplier;
        float speed = baseSpeed * speedMultiplier;

        audioSource = GetComponent<AudioSource>();
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null){
            rb.linearVelocity = transform.forward * speed;
        }
        Debug.Log("Applied multipliers: speedMultiplier = " + speedMultiplier + ", damageMultiplier = " + damageMultiplier);
        Debug.Log("Values speed = " + speed + ", damage = " + damage);

        Destroy(gameObject, lifetime);
    }
    void OnTriggerEnter(Collider other){
        IHealth targetHealth = other.GetComponentInParent<IHealth>();
        if (targetHealth != null){
            float damage = baseDamage * damageMultiplier;
            targetHealth.TakeDamage(damage, gameObject);
            Debug.Log("DragonProjectile hit target; applied damage = " + damage);
            Destroy(gameObject);
        }
    }
    public static void ResetModifiers(){
        speedMultiplier = 1f;
        damageMultiplier = 1f;
        Debug.Log("DragonProjectile modifiers reset: speedMultiplier = " + speedMultiplier + ", damageMultiplier = " + damageMultiplier);
    }
}
