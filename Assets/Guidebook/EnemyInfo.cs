using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{

    public string enemyName;
    public string weakness;
    public string speed;
    public string chase;
    public string damage;
    public string detectionRange;
    public Sprite enemySprite;

    public EnemyInfo(string enemyName, string weakness, string speed, string chase, string damage, string detectionRange, Sprite enemySprite)
    {
        this.enemyName = enemyName;
        this.weakness = weakness;
        this.speed = speed;
        this.chase = chase;
        this.damage = damage;
        this.detectionRange = detectionRange;
        this.enemySprite = enemySprite;
    }
}
