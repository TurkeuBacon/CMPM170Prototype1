using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used in order to create ScriptableObjects inside your folders
[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Health")]
    public float Health;
    public float MaxHealth;
}
