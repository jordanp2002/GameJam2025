using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int enemiesAlive = 0;
    private bool gameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterEnemy()
    {
        enemiesAlive++;
    }

    public void EnemyKilled()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0 && !gameOver)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        gameOver = true;
        Debug.Log("All enemies defeated! Game Over.");
        Time.timeScale = 0f;
    }
}

