using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ActivePowerUps: MonoBehaviour
{
    public static ActivePowerUps instance;
    [SerializeField] public Image healthIcon;
    [SerializeField] public Image staminaIcon;
    [SerializeField] public Image projectileSpeedIcon;
    [SerializeField] public Image projectileDamageIcon;
    [SerializeField] public Image resistIcon;

    public float activeOpacity = 1f;     
    public float inactiveOpacity = 0.3f;

    private void Awake(){
        instance = this;
    }
    private void Start(){
        UpdateDisplay();
    }
    public void UpdateDisplay(){
        SetOpacity(healthIcon, inactiveOpacity);
        SetOpacity(staminaIcon, inactiveOpacity);
        SetOpacity(projectileSpeedIcon, inactiveOpacity);
        SetOpacity(projectileDamageIcon, inactiveOpacity);
        SetOpacity(resistIcon, inactiveOpacity);

        List<PowerUp> activePowerUps = PowerUpManager.Instance.GetActivePowerUps();
        if (activePowerUps == null) return;
        foreach (PowerUp P in activePowerUps){
            switch (P.type){
                case PowerUp.PowerUpType.HealthMod2:
                    SetOpacity(healthIcon, activeOpacity);
                    break;
                case PowerUp.PowerUpType.StaminaMod1:
                    SetOpacity(staminaIcon, activeOpacity);
                    break;
                case PowerUp.PowerUpType.ProjectileMod2:
                    SetOpacity(projectileSpeedIcon, activeOpacity);
                    break;
                case PowerUp.PowerUpType.ProjectileMod3:
                    SetOpacity(projectileDamageIcon, activeOpacity);
                    break;
                case PowerUp.PowerUpType.DragonMod2:
                    SetOpacity(resistIcon, activeOpacity);
                    break;
          
            }
        }
    }
    void SetOpacity(Image img, float alpha){
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
}

