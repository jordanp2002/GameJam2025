using UnityEngine;

public class DragonController : MonoBehaviour
{

    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float fixedHeight = 1f;

    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashCooldown = 5f;

    private float dashCooldownTimer = 0f;

    private bool touchingLeftWall = false;
    private bool touchingRightWall = false;
    private bool gameStopped = false;
    void Update()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        float horizontalInput = Input.GetAxisRaw("Horizontal");


        if ((horizontalInput < 0 && touchingLeftWall) || (horizontalInput > 0 && touchingRightWall))
        {
            horizontalInput = 0;
        }
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
        if ((direction < 0 && touchingLeftWall) || (direction > 0 && touchingRightWall))
            return;
        transform.Translate(Vector3.right * direction * dashDistance, Space.World);
        dashCooldownTimer = dashCooldown;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Left"))
            touchingLeftWall = true;
        else if (other.CompareTag("Right"))
            touchingRightWall = true;
        else if (other.CompareTag("End"))
        {
            gameStopped = true;
            Debug.Log("Reached the end! Game Over!");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Left"))
            touchingLeftWall = false;
        else if (other.CompareTag("Right"))
            touchingRightWall = false;
    }
}