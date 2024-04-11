using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyBasePrefab;
    public Sprite[] spriteList;
    private List<EnemyStateMachine> stateMachines;
    private EnemyType[] enemyTypes;
    
    void Start()
    {
        setupStateMachines();
        enemyTypes = new EnemyType[spriteList.Length];
        for(int i = 0; i < enemyTypes.Length; i++) {
            int stateMachineIndex = Random.Range(0, stateMachines.Count);
            enemyTypes[i] = new EnemyType(
                spriteList[i],
                Random.Range(5, 20),
                Random.Range(2f, 10f),
                Random.Range(5f, 20f),
                Random.Range(2f, 4f),
                stateMachines[stateMachineIndex]);
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
        enemyBaseScript.setStateMachine(type.stateMachine);

        return newEnemy;
    }

    private void setupStateMachines() {
        stateMachines = new List<EnemyStateMachine>();
        EnemyStateMachine stateMachineA = new EnemyStateMachine();
        stateMachineA.addState("patrol", new EnemyState((EnemyStateParams)=>{
            EnemyStateParams.self.transform.position += Vector3.right * Time.deltaTime;
        }));
        stateMachineA.setCurrentState("patrol");
        stateMachines.Add(stateMachineA);
    }
}

public struct EnemyType {
    public Sprite sprite;
    public int health;
    public float attack, defense, speed;
    public EnemyStateMachine stateMachine;

    public EnemyType(Sprite sprite, int health, float attack, float defense, float speed, EnemyStateMachine stateMachine) {
        this.sprite = sprite;
        this.health = health;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
        this.stateMachine = stateMachine;
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
