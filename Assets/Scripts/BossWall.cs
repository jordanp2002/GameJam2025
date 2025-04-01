using UnityEngine;

public class BossWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TopDownCamera camera = Camera.main.GetComponent<TopDownCamera>();
            if (camera != null)
            {
                Debug.Log("Player hit the boss wall. stopping camera movement.");
                camera.StopCameraMovement();
            }
        }
    }
}
