using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{

    public string name;
    public string speed;
    public string chase;
    public string enemy;
    public string detectionRange;
    public Sprite enemySprite;

    public EnemyInfo(string name, string speed, string chase, string enemy, string detectionRange, Sprite enemySprite)
    {
        this.name = name;
        this.speed = speed;
        this.chase = chase;
        this.enemy = enemy;
        this.detectionRange = detectionRange;
        this.enemySprite = enemySprite;
    }
}
