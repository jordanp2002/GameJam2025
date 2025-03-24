using UnityEngine;
using UnityEngine.UI;

public class DragonController : MonoBehaviour
{

    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float fixedHeight = 1f;

    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashCooldown = 5f;

    private float dashCooldownTimer = 0f;

    public Slider dashCooldownSlider;

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

        if (dashCooldownSlider != null)
        {
            dashCooldownSlider.value = 1- (dashCooldownTimer / dashCooldown);
        }
    }

    void PerformDash(float direction)
    {
        if (direction == 0f)
            direction = 1f;

        transform.Translate(Vector3.right * direction * dashDistance, Space.World);
        dashCooldownTimer = dashCooldown;
    }
}