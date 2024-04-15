using UnityEngine;
public enum WeaponType
{
    Melee
}
[CreateAssetMenu(fileName = "Weapon_")]
public class Weapon : ScriptableObject
{
    [Header("Config")]
    public Sprite Icon;
    public WeaponType WeaponType;
    public float Damage;
    
}
