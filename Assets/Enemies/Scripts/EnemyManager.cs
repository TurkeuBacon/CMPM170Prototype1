using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyBasePrefab;
    public Sprite[] spriteList;
    private static EnemyBase.MoveDelegate[] moveBehaviors = {
        (behaviourParams)=>{
            behaviourParams.self.transform.position += Vector3.right * behaviourParams.self.speed * Time.deltaTime;
        },
        (behaviourParams)=>{
            behaviourParams.self.transform.position += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized * behaviourParams.self.speed * Time.deltaTime;
        }
    };
    private static EnemyBase.AttackDelegate[] attackBehaviors = {
        (behaviourParams)=>{
            
        }
    };

    private EnemyType[] enemyTypes;
    
    void Start()
    {
        enemyTypes = new EnemyType[spriteList.Length];
        for(int i = 0; i < enemyTypes.Length; i++) {
            int moveBehaviorIndex = Random.Range(0, moveBehaviors.Length);
            int attackBehaviorIndex = Random.Range(0, attackBehaviors.Length);
            enemyTypes[i] = new EnemyType(
                spriteList[i],
                Random.Range(5, 20),
                Random.Range(2f, 10f),
                Random.Range(5f, 20f),
                Random.Range(2f, 4f),
                moveBehaviors[moveBehaviorIndex], attackBehaviors[attackBehaviorIndex]);
        }
        for(int i = 0; i < 10; i++) {
            spawnEnemy(Random.Range(0, enemyTypes.Length));
        }
    }


    void Update()
    {
        
    }

    private GameObject spawnEnemy(int typeIndex) {
        EnemyType type = enemyTypes[typeIndex];
        GameObject newEnemy = Instantiate(enemyBasePrefab, Vector3.zero, Quaternion.identity);
        EnemyBase enemyBaseScript = newEnemy.GetComponent<EnemyBase>();
        enemyBaseScript.setSprite(type.sprite);
        enemyBaseScript.setStats(type.health, type.attack, type.defense, type.speed);
        enemyBaseScript.setMoveBehavior(type.moveBehavior);
        enemyBaseScript.setAttackBehavior(type.attackBehavior);

        return newEnemy;
    }
}

public struct EnemyType {
    public Sprite sprite;
    public int health;
    public float attack, defense, speed;
    public EnemyBase.MoveDelegate moveBehavior;
    public EnemyBase.AttackDelegate attackBehavior;

    public EnemyType(Sprite sprite, int health, float attack, float defense, float speed, EnemyBase.MoveDelegate moveBehavior, EnemyBase.AttackDelegate attackBehavior) {
        this.sprite = sprite;
        this.health = health;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
        this.moveBehavior = moveBehavior;
        this.attackBehavior = attackBehavior;
    }
}

public struct BehaviorParams {

    public EnemyBase self;
    public EnemyBase[] enemies;
    public PlayerController player;

    public BehaviorParams(EnemyBase self, EnemyBase[] enemies, PlayerController player) {
        this.self = self;
        this.enemies = enemies;
        this.player = player;
    }
}
