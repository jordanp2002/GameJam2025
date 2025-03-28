using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    [SerializeField] private float height = 10f;
    [SerializeField] private float zOffset = 50f;

    // Define the bounds
    //[SerializeField] private float minX = -4f;
    //[SerializeField] private float maxX = 4f;
    //[SerializeField] private float minZ = -4f;
    //[SerializeField] private float maxZ = 4f;

    // Define the forward speed
    [SerializeField] private float forwardSpeed = 5f;

    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(
            transform.position.x,
            height,
            transform.position.z + forwardSpeed * Time.deltaTime
        );

        // Clamp the desired position to stay within bounds
        //desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        //desiredPosition.z = Mathf.Clamp(desiredPosition.z, minZ, maxZ);

        transform.position = desiredPosition;
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}
