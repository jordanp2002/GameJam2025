using UnityEngine;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance;
    public List<PowerUp> allPowerUps;
    private List<PowerUp> activePowerUps = new List<PowerUp>();
    public List<PowerUp> currentLevelPowerUps { get; private set; } = new List<PowerUp>();

    void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if (allPowerUps == null || allPowerUps.Count == 0){
                InitializePowerUpList();
            }
            RefreshCurrentLevelPowerUps();
        }
        else{
            Destroy(gameObject);
        }
    }
    private void InitializePowerUpList(){
        if (allPowerUps == null)
            allPowerUps = new List<PowerUp>();

        if (allPowerUps.Count == 0){
            allPowerUps.AddRange(Resources.LoadAll<PowerUp>("PowerUps"));
        }
    }
    public void ClearActivePowerUps(){
        activePowerUps.Clear();
    }
    public List<PowerUp> GetRandomPowerUps(int count){
        if (allPowerUps == null || allPowerUps.Count == 0){
            return new List<PowerUp>();
        }
        List<PowerUp> selectedPowerUps = new List<PowerUp>();
        while (selectedPowerUps.Count < count){
            PowerUp randomPowerUp = allPowerUps[Random.Range(0, allPowerUps.Count)];
            if (!selectedPowerUps.Contains(randomPowerUp))
                selectedPowerUps.Add(randomPowerUp);
        }
        return selectedPowerUps;
    }

    public void RefreshCurrentLevelPowerUps(){
        currentLevelPowerUps = GetRandomPowerUps(3);
    }

    public void ApplyPowerUp(PowerUp powerUp){
        if (powerUp == null){
            Debug.LogError("ApplyPowerUp received a null power-up!");
            return;
        }
        activePowerUps.Add(powerUp);
        if (ActivePowerUps.instance != null){
            ActivePowerUps.instance.UpdateDisplay();
        }
        else{
            Debug.Log("ActivePowerUps is Null");
        }
        ApplyAllPowerUps();
    }
    public List<PowerUp> GetActivePowerUps(){
        return activePowerUps;
    }
    public void ApplyAllPowerUps()
    {
        foreach (PowerUp P in activePowerUps){
            switch (P.type){
                case PowerUp.PowerUpType.HealthMod2:
                    PlayerHealth.Instance.maxHealth *= 1.1f;
                    Debug.Log($"maxHealth: {PlayerHealth.Instance.maxHealth}");
                    break;
                case PowerUp.PowerUpType.ProjectileMod2:
                    DragonProjectile.speedMultiplier *= 1.08f;

                    break;
                case PowerUp.PowerUpType.ProjectileMod3:
                    DragonProjectile.damageMultiplier *= 1.05f;
                    break;
                case PowerUp.PowerUpType.DragonMod2:
                    PlayerHealth.Instance.damageResistance += 0.05f;
                    Debug.Log($"damageResistance: {PlayerHealth.Instance.damageResistance}");
                    break;
                case PowerUp.PowerUpType.StaminaMod1:
                    DragonController.Instance.ReduceMaxStamina(0.1f);
                    break;
            }
        }
    }
}


