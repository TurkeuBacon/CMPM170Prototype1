using UnityEngine;

public class Player: MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    // This will return the stats private variable
    public PlayerStats Stats => stats;

}
