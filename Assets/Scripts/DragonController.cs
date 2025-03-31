using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DragonController : MonoBehaviour
{
    public static DragonController Instance;

    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 5f;

    [SerializeField] private float dashDistance = 5f;
    [SerializeField] public float dashMeter = 1f;
    public float maxStamina = 1f;

    private bool touchingLeftWall = false;
    private bool touchingRightWall = false;
    public Slider dashMeterSlider;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        PowerUpManager.Instance.ApplyAllPowerUps();
        dashMeterSlider.minValue = 0f;
        dashMeterSlider.maxValue = maxStamina;
        dashMeterSlider.value = dashMeter;
    }

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

        Vector3 clampedPosition = transform.position;
        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * mainCamera.aspect * 2;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, mainCamera.transform.position.x - cameraWidth / 2, mainCamera.transform.position.x + cameraWidth / 2);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, mainCamera.transform.position.z - cameraHeight / 2, mainCamera.transform.position.z + cameraHeight / 2);

        transform.position = clampedPosition;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashMeter >= maxStamina)
        {
            PerformDash(horizontalInput);
        }

        if (dashMeterSlider != null)
        {
            dashMeterSlider.value = dashMeter;
        }
    }
    public void ResetStaminaModifiers(){
        maxStamina = 1f; 
        dashMeter = maxStamina;
        if (dashMeterSlider != null)
        {
            dashMeterSlider.maxValue = maxStamina;
            dashMeterSlider.value = dashMeter;
        }
        Debug.Log("Stamina Reset To Default");
    }
    void PerformDash(float direction){
        if (direction == 0f)
            direction = 1f;
        if ((direction < 0 && touchingLeftWall) || (direction > 0 && touchingRightWall))
            return;
        transform.Translate(Vector3.right * direction * dashDistance, Space.World);
        dashMeter = 0f;
        if (dashMeterSlider) dashMeterSlider.value = dashMeter;
    }
    public void AddSoulToDash(float amount){
        dashMeter += amount;
        Debug.Log("Soul collected, dashMeter now: " + dashMeter);
        if (dashMeter > maxStamina)
            dashMeter = maxStamina;
        if (dashMeterSlider) dashMeterSlider.value = dashMeter;
    }
    public void ReduceMaxStamina(float factor){
        maxStamina -= factor;
        if (dashMeter > maxStamina)
            dashMeter = maxStamina;
        if (dashMeterSlider){
            dashMeterSlider.maxValue = maxStamina;
            dashMeterSlider.value = dashMeter;
        }
        Debug.Log($"New maxStamina: {maxStamina}, current dashMeter: {dashMeter}");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Left"))
            touchingLeftWall = true;
        else if (other.CompareTag("Right"))
            touchingRightWall = true;
        if (other.CompareTag("End")){
            GameManager.Instance.SetLastCompletedLevel(SceneManager.GetActiveScene().buildIndex);
            GameManager.Instance.EndLevel();
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
