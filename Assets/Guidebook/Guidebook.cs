using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class Guidebook : MonoBehaviour
{
    [SerializeField] private bool isGuideBookVisible = true;
    public UnityEngine.UI.Image guidebookUI;

    public TextMeshProUGUI nameTextOne;
    public TextMeshProUGUI speedTextOne;
    public TextMeshProUGUI chaseSpeedTextOne;
    public TextMeshProUGUI weaknessTextOne;
    public TextMeshProUGUI damageTextOne;
    public TextMeshProUGUI detectionRangeTextOne;
    public UnityEngine.UI.Image enemyImageOne;

    public TextMeshProUGUI nameTextTwo;
    public TextMeshProUGUI speedTextTwo;
    public TextMeshProUGUI chaseSpeedTextTwo;
    public TextMeshProUGUI weaknessTextTwo;
    public TextMeshProUGUI damageTextTwo;
    public TextMeshProUGUI detectionRangeTextTwo;
    public UnityEngine.UI.Image enemyImageTwo;


    public Sprite enemy1Sprite;
    public Sprite enemy2Sprite;
    public Sprite enemy3Sprite;
    public Sprite enemy4Sprite;

    private Dictionary<string, EnemyInfo> enemyInfoDictionary = new Dictionary<string, EnemyInfo>();
    private string[] enemyTypes; // Array to store enemy types
    private int currentIndex = 0;

    private CanvasGroup canvasGroup;

    private void Start()    
    {

        canvasGroup = guidebookUI.GetComponent<CanvasGroup>();

        PopulateEnemyInfo();

        UpdatePage();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleGuideBookVisibility();
        }
    }

    private void ToggleGuideBookVisibility()
    {
        float alphaValue = isGuideBookVisible ? 1f : 0f;

        isGuideBookVisible = !isGuideBookVisible;


        canvasGroup.alpha = alphaValue;
    }



    private void PopulateEnemyInfo()
    {

        enemyInfoDictionary.Add("Enemy1", new EnemyInfo("Name: Enemy1", "Weakness : Water", "Speed: 1", "Chase: 1", "Damage: 1", "Detection Range: 1", enemy1Sprite));
        enemyInfoDictionary.Add("Enemy2", new EnemyInfo("Name: Enemy2", "Weakness : Top", "Speed: 1", "Chase: 1", "Damage: 1", "Detection Range: 1", enemy2Sprite));
        enemyInfoDictionary.Add("Enemy3", new EnemyInfo("Name: Enemy3", "Weakness : Bottom", "Speed: 1", "Chase: 1", "Damage: 1", "Detection Range: 1", enemy3Sprite));
        enemyInfoDictionary.Add("Enemy4", new EnemyInfo("Name: Enemy4", "Weakness : Right", "Speed: 1", "Chase: 1", "Damage: 1", "Detection Range: 1", enemy4Sprite));

        // Store keys from the dictionary in the enemyTypes array
        enemyTypes = new string[enemyInfoDictionary.Count];
        enemyInfoDictionary.Keys.CopyTo(enemyTypes, 0);
    }

    private void UpdatePage()
    {
        if (currentIndex + 1 < 0 || currentIndex + 1 >= enemyTypes.Length) return;

        string enemyType = enemyTypes[currentIndex];
        EnemyInfo enemyInfo = enemyInfoDictionary[enemyType];

        string enemyTypeTwo = enemyTypes[currentIndex + 1];
        EnemyInfo enemyInfoTwo = enemyInfoDictionary[enemyTypeTwo];

        nameTextOne.text = enemyInfo.enemyName;
        weaknessTextOne.text = enemyInfo.weakness;
        speedTextOne.text = enemyInfo.speed;
        chaseSpeedTextOne.text = enemyInfo.chase;
        damageTextOne.text = enemyInfo.damage;
        detectionRangeTextOne.text = enemyInfo.detectionRange;
        enemyImageOne.sprite = enemyInfo.enemySprite;

        nameTextTwo.text = enemyInfoTwo.enemyName;
        weaknessTextTwo.text = enemyInfoTwo.weakness;
        speedTextTwo.text = enemyInfoTwo.speed;
        chaseSpeedTextTwo.text = enemyInfoTwo.chase;
        damageTextTwo.text = enemyInfoTwo.damage;
        detectionRangeTextTwo.text = enemyInfoTwo.detectionRange;
        enemyImageTwo.sprite = enemyInfoTwo.enemySprite;

    }

    public void NextPage()
    {
        currentIndex++;
        if (currentIndex + 1 >= enemyTypes.Length)
        {
            currentIndex = 0;
        }
        UpdatePage();
    }

    public void BackPage()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = enemyTypes.Length - 1;
        }

        UpdatePage();
    }
}
