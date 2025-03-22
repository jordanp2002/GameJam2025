using UnityEngine;

public class DragonController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float fixedHeight = 1f;

    [Header("Dash Settings")]
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashCooldown = 5f;

    private float dashCooldownTimer = 0f;

    void Update()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 move = new Vector3(horizontalInput * horizontalSpeed * Time.deltaTime, 0f, 0f);
        transform.Translate(move, Space.World);

        // Handle Dash
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0f)
        {
            PerformDash(horizontalInput);
        }

        // Lock the dragon's height
        Vector3 position = transform.position;
        position.y = fixedHeight;
        transform.position = position;
    }

    void PerformDash(float direction)
    {
        if (direction == 0f)
            direction = 1f;

        transform.Translate(Vector3.right * direction * dashDistance, Space.World);
        dashCooldownTimer = dashCooldown;
    }
}