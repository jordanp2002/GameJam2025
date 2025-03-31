using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void ReturnToMainMenu(){
        if (GameManager.Instance != null){
            GameManager.Instance.ResetGame();
        }
        if (PowerUpManager.Instance != null){
            PowerUpManager.Instance.ClearActivePowerUps();
        }
        DragonProjectile.ResetModifiers();
        if (DragonController.Instance != null){
            DragonController.Instance.ResetStaminaModifiers();
        }
        SceneManager.LoadScene("MainMenu");
    }
}
