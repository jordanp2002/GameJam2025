using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    [SerializeField] private Transform target;  
    [SerializeField] private float height = 10f;
    [SerializeField] private float followSpeed = 10f;
    [SerializeField] private float fixedX = 0f;

    void Start()
    {
        if (target != null)
            fixedX = transform.position.x;  
    }

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 desiredPosition = new Vector3(fixedX, target.position.y + height, target.position.z);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}
