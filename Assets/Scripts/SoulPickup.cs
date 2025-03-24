using UnityEngine;

public class SoulPickup : MonoBehaviour
{
    public int soulValue = 50;
    public float attractRange = 50f;
    public float moveSpeed = 2f;
    public float maxSpeed = 8f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attractRange)
        {
            float speed = Mathf.Lerp(moveSpeed, maxSpeed, 1 - (distance / attractRange));
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        GameManager.Instance.AddScore(soulValue);
    //        Destroy(gameObject);
    //    }
    //}
}
