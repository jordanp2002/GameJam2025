using UnityEngine;
using UnityEngine.SceneManagement;
public class EndWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit the end wall. Ending level.");
            GameManager.Instance.SetLastCompletedLevel(SceneManager.GetActiveScene().buildIndex);
            GameManager.Instance.EndLevel();
        }
    }
}
