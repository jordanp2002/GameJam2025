using UnityEngine;
public class PowerUp : ScriptableObject
{
    public string powerUpName;
    public enum PowerUpType { HealthMod1, HealthMod2, StaminaMod1, ProjectileMod1, ProjectileMod2, ProjectileMod3, DragonMod1, DragonMod2 }
    public PowerUpType type;
    public float value;
}
