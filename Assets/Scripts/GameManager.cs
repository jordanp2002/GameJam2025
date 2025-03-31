using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int enemiesAlive = 0;
    public TextMeshProUGUI scoreText;
    private int score = 000000;
    private int lastCompletedLevel = 1;
    public int maxLevel = 3;

    void Awake(){
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

    void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if (scene.name.StartsWith("Level"))
        {
            OnDragonSpawn();
        }
    }
    
    public void OnDragonSpawn(){
        PowerUpManager.Instance.ApplyAllPowerUps();
        PlayerHealth.Instance.currentHealth = PlayerHealth.Instance.maxHealth;
        PlayerHealth.Instance.UpdateHealthBar();
    }

    public void RegisterEnemy(){
        enemiesAlive++;
    }
    void OnLevelWasLoaded(int level){
        UpdateUIReferences();
        UpdateScoreText();
    }
    public void ResetGame(){
        score = 000000;
        UpdateScoreText();
        ResetEnemyCount();
    }
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString("D6");
        }
    }

    private void UpdateUIReferences(){
        scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
        var transitionScene = FindAnyObjectByType<TransitionScene>();
        transitionScene.UpdatePowerUpUI();
    }

    public void EnemyKilled()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0){
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            SetLastCompletedLevel(currentLevel);
            EndLevel();
        }
    }
    public void ResetEnemyCount()
    {
        enemiesAlive = 0;
    }
    public void EndGame(){
        Debug.Log("Game Over!");
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOver");
    }
    public void EndLevel(){
        ResetEnemyCount();
        SceneManager.LoadScene("TransitionScene");
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }
    public void SetLastCompletedLevel(int level){
        lastCompletedLevel = level;
    }
    public int GetNextLevel(){
        int next = lastCompletedLevel + 1;
        if (next > maxLevel){
            next = 1; 
        }
        return next;
    }
}

