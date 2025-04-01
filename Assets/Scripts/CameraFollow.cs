using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    [SerializeField] private float height = 10f;
    [SerializeField] private float zOffset = 50f;
    [SerializeField] private float forwardSpeed = 5f;

    private bool stopMovement = false;

    void LateUpdate()
    {
        if (stopMovement) return;

        Vector3 desiredPosition = new Vector3(
            transform.position.x,
            height,
            transform.position.z + forwardSpeed * Time.deltaTime
        );

        transform.position = desiredPosition;
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    public void StopCameraMovement()
    {
        stopMovement = true;
    }
}
