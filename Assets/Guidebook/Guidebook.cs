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
    [Header("Config")]
    [SerializeField] private bool isGuideBookVisible = true;


    public UnityEngine.UI.Image indexPage;
    public UnityEngine.UI.Image pageOne;
    public UnityEngine.UI.Image pageTwo;
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
    public Sprite enemy5Sprite;

    private Dictionary<string, EnemyInfo> enemyInfoDictionary = new Dictionary<string, EnemyInfo>();
    private string[] enemyTypes; // Array to store enemy types
    private int currentIndex = 0;

    private CanvasGroup canvasGroup;
    private CanvasGroup indexPageCanvasGroup;
    private CanvasGroup pageOneCanvasGroup;
    private CanvasGroup pageTwoCanvasGroup;
    private void Start()    
    {

        canvasGroup = guidebookUI.GetComponent<CanvasGroup>();
        indexPageCanvasGroup = indexPage.GetComponent<CanvasGroup>();
        pageOneCanvasGroup = pageOne.GetComponent<CanvasGroup>();
        pageTwoCanvasGroup = pageTwo.GetComponent<CanvasGroup>();

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

        enemyInfoDictionary.Add("Enemy1", new EnemyInfo("Name: Enemy1", "Weakness : Unda", "Speed: Slow", "Chase: Fast", "Damage: Small", "Detection Range: Far", enemy1Sprite));
        enemyInfoDictionary.Add("Enemy2", new EnemyInfo("Name: Enemy2", "Weakness : Sinistrorsum", "Speed: Medium", "Chase: Slow", "Damage: High", "Detection Range: Short", enemy2Sprite));
        enemyInfoDictionary.Add("Enemy3", new EnemyInfo("Name: Enemy3", "Weakness : Summo Direcium", "Speed: Slow", "Chase: Fast", "Damage: Medium", "Detection Range: Short", enemy3Sprite));
        enemyInfoDictionary.Add("Enemy4", new EnemyInfo("Name: Enemy4", "Weakness : Lapis", "Speed: Fast", "Chase: Medium", "Damage: Small", "Detection Range: High", enemy4Sprite));
        enemyInfoDictionary.Add("Enemy5", new EnemyInfo("Name: Enemy5", "Weakness : Lapis", "Speed: Short", "Chase: Short", "Damage: Medium", "Detection Range: Medium", enemy4Sprite));

        // Store keys from the dictionary in the enemyTypes array
        enemyTypes = new string[enemyInfoDictionary.Count];
        enemyInfoDictionary.Keys.CopyTo(enemyTypes, 0);
    }

    private int pOneincrementer = -1;
    private int pTwoincrementer = 0;
    private void UpdatePage()
    {
        print(pOneincrementer + ", " + pTwoincrementer + ", " + currentIndex);
        if (currentIndex == 0)
        {
            indexPageCanvasGroup.alpha = 1f;
            pageOneCanvasGroup.alpha = 0f;

            string enemyType = enemyTypes[0];
            EnemyInfo enemyInfo = enemyInfoDictionary[enemyType];

            nameTextTwo.text = enemyInfo.enemyName;
            weaknessTextTwo.text = enemyInfo.weakness;
            speedTextTwo.text = enemyInfo.speed;
            chaseSpeedTextTwo.text = enemyInfo.chase;
            damageTextTwo.text = enemyInfo.damage;
            detectionRangeTextTwo.text = enemyInfo.detectionRange;
            enemyImageTwo.sprite = enemyInfo.enemySprite;
        }
        else
        {
            string enemyType = enemyTypes[currentIndex + pOneincrementer];
            EnemyInfo enemyInfo = enemyInfoDictionary[enemyType];

            string enemyTypeTwo = enemyTypes[currentIndex + pTwoincrementer];
            EnemyInfo enemyInfoTwo = enemyInfoDictionary[enemyTypeTwo];


            indexPageCanvasGroup.alpha = 0f;
            pageOneCanvasGroup.alpha = 1f;

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
        

        

    }

    public void NextPage()
    {
        currentIndex += 1;
        pOneincrementer += 1;
        pTwoincrementer += 1;

        if (currentIndex + 2 >= enemyTypes.Length)
        {
            currentIndex = 0;
            pOneincrementer = -1;
            pTwoincrementer = 0;
        }
        UpdatePage();
    }

    public void BackPage()
    {
        

        if (currentIndex <= 0)
        {
            currentIndex = enemyTypes.Length - 3;
            pOneincrementer = enemyTypes.Length - 4;
            pTwoincrementer = enemyTypes.Length - 3;
        }
        else
        {
            currentIndex -= 1;
            pOneincrementer -= 1;
            pTwoincrementer -= 1;
        }

        UpdatePage();
    }
}
