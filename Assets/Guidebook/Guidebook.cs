using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Guidebook : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI chaseSpeedText;
    public TextMeshProUGUI enemyText;
    public TextMeshProUGUI detectionRangeText;
    public Image enemyImage;

    private Dictionary<string, EnemyInfo> enemyInfoDictionary = new Dictionary<string, EnemyInfo>();

}
