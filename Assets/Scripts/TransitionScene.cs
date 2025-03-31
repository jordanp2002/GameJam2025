using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class TransitionScene : MonoBehaviour
{
    private List<PowerUp> currentChoices;

    public GameObject powerUpPanel1;
    public GameObject powerUpPanel2;
    public GameObject powerUpPanel3;
    public Button powerUpButton1;
    public Button powerUpButton2;
    public Button powerUpButton3;
    public TextMeshProUGUI powerUpText1;
    public TextMeshProUGUI powerUpText2;
    public TextMeshProUGUI powerUpText3;
    void Start(){
        DisplayPowerUpChoices();
    }

    void DisplayPowerUpChoices(){
        currentChoices = PowerUpManager.Instance.GetRandomPowerUps(3);
        SetPowerUpButton(powerUpPanel1, powerUpButton1, powerUpText1, currentChoices[0]);
        SetPowerUpButton(powerUpPanel2, powerUpButton2, powerUpText2,currentChoices[1]);
        SetPowerUpButton(powerUpPanel3, powerUpButton3, powerUpText3, currentChoices[2]);
    }
    void SetPowerUpButton(GameObject panel, Button button, TextMeshProUGUI text,PowerUp powerUp){
        if (powerUp == null){
            Debug.LogError("PowerUp asset is null in SetPowerUpButton");
            return;
        }
        panel.SetActive(true);
        text.text = powerUp.powerUpName;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => SelectPowerUp(powerUp));
    }

    void SelectPowerUp(PowerUp selectedPowerUp){
        if (selectedPowerUp == null){
            Debug.LogError("Selected power-up is null");
            return;
        }
        PowerUpManager.Instance.ApplyPowerUp(selectedPowerUp);
        Debug.Log("Selected Power-Up: " + selectedPowerUp.powerUpName);
        LoadNextLevel();  
    }
    void LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex;
        if (currentSceneIndex == 4) {
            nextSceneIndex = GameManager.Instance.GetNextLevel();
        }
        else if (currentSceneIndex == 3) {
  
            nextSceneIndex = 4; 
        }
        else{
            nextSceneIndex = currentSceneIndex + 1;
        }
        StartCoroutine(LoadLevelAsync(nextSceneIndex));
    }
    IEnumerator LoadLevelAsync(int sceneIndex){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone){
            yield return null;
        }
        PowerUpManager.Instance?.ApplyAllPowerUps();
    }
    public void UpdatePowerUpUI(){
        powerUpPanel1 = GameObject.Find("PowerUpPanel1");
        powerUpPanel2 = GameObject.Find("PowerUpPanel2");
        powerUpPanel3 = GameObject.Find("PowerUpPanel3");

        powerUpButton1 = GameObject.Find("PowerUpButton1")?.GetComponent<Button>();
        powerUpButton2 = GameObject.Find("PowerUpButton2")?.GetComponent<Button>();
        powerUpButton3 = GameObject.Find("PowerUpButton3")?.GetComponent<Button>();

        powerUpText1 = GameObject.Find("PowerUpText1")?.GetComponent<TextMeshProUGUI>();
        powerUpText2 = GameObject.Find("PowerUpText2")?.GetComponent<TextMeshProUGUI>();
        powerUpText3 = GameObject.Find("PowerUpText3")?.GetComponent<TextMeshProUGUI>();

        if (powerUpPanel1 == null || powerUpPanel2 == null || powerUpPanel3 == null)
            Debug.LogError("PowerUp Panels not found!");
        if (powerUpButton1 == null || powerUpButton2 == null || powerUpButton3 == null)
            Debug.LogError("PowerUp Buttons not found!");
        if (powerUpText1 == null || powerUpText2 == null || powerUpText3 == null)
            Debug.LogError("PowerUp Texts not found!");

        if (powerUpPanel1) powerUpPanel1.SetActive(true);
        if (powerUpPanel2) powerUpPanel2.SetActive(true);
        if (powerUpPanel3) powerUpPanel3.SetActive(true);
    }

}