using UnityEngine;
using UnityEngine.UI;

public class DragonController : MonoBehaviour
{ 
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 5f;

    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashCooldown = 5f;

    private float dashCooldownTimer = 0f;

    private bool touchingLeftWall = false;
    private bool touchingRightWall = false;
    private bool gameStopped = false;

    public Slider dashCooldownSlider;

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if ((horizontalInput < 0 && touchingLeftWall) || (horizontalInput > 0 && touchingRightWall))
        {
            horizontalInput = 0;
        }

        Vector3 move = new Vector3(horizontalInput * horizontalSpeed * Time.deltaTime, 0f, verticalInput * verticalSpeed * Time.deltaTime);
        transform.Translate(move, Space.World);

        // Clamp the dragon's position within the camera's bounds
        Vector3 clampedPosition = transform.position;
        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * mainCamera.aspect * 2;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, mainCamera.transform.position.x - cameraWidth / 2, mainCamera.transform.position.x + cameraWidth / 2);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, mainCamera.transform.position.z - cameraHeight / 2, mainCamera.transform.position.z + cameraHeight / 2);

        transform.position = clampedPosition;

        // Handle Dash
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0f)
        {
            PerformDash(horizontalInput);
        }

        if (dashCooldownSlider != null)
        {
            dashCooldownSlider.value = 1 - (dashCooldownTimer / dashCooldown);
        }
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
