using UnityEngine;

public class SoulPickup : MonoBehaviour
{
    public int soulValue = 50;
    public float attractRange = 15f;
    public float moveSpeed = 4f;
    public float maxSpeed = 10f;

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
}