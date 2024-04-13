using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyBasePrefab;
    public Sprite[] spriteList;
    private List<EnemyStateMachine> stateMachines;
    private EnemyType[] enemyTypes;
    public float noiseScale;
    
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
            Vector2 spawnLocation = new Vector2(Random.Range(-40, -20), Random.Range(15, 30));
            //spawnEnemy(Random.Range(0, enemyTypes.Length), spawnLocation);
        }
    }


    void Update() {
        
    }

    private GameObject spawnEnemy(int typeIndex, Vector2 position) {
        EnemyType type = enemyTypes[typeIndex];
        GameObject newEnemy = Instantiate(enemyBasePrefab, new Vector3(position.x, position.y, 0), Quaternion.identity, this.transform);
        EnemyBase enemyBaseScript = newEnemy.GetComponent<EnemyBase>();
        enemyBaseScript.setSprite(type.sprite);
        enemyBaseScript.setStats(type.health, type.attack, type.defense, type.speed);
        enemyBaseScript.setStateMachine(type.stateMachine);

        return newEnemy;
    }

    private void setupStateMachines() {
        stateMachines = new List<EnemyStateMachine>();
        //------------------------STATE MACHINE A------------------------
        /*  HOW TO CREATE AN ENEMY STATE MACHINE, AS AN EXAMPLE */
        EnemyStateMachine stateMachineA = new EnemyStateMachine();
        /*  ADDING A NEW STATE: requires a unique name and an EnemyState
            EnemyState: requires a function delegate as a parameter. */

        /*  DELEGATE FORMAT: ( function_parameters ) => { function body }

                - For enemy state the only parameter is EnemyStateParams enemyStateParams, a
                struct used to pass any data that can be referenced in the function body.
                the enemy state delegate is a void function, so no value needs
                to be returned inside the function body

                - For enemy state transition delegates (more below), the parameters are the
                same, but the delegate is a boolean function, so a bool value must be returned
                in the function body */
        stateMachineA.addState("patrol", new EnemyState((enemyStateParams)=>{
            int randomLol = Random.Range(0, 1000);
            if(randomLol < 2) {
                Vector2 moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                enemyStateParams.self.setVelocity(moveDirection * enemyStateParams.self.speed);
            } else if (randomLol >= 994) {
                enemyStateParams.self.setVelocity(Vector2.zero);
            }
        }));
        stateMachineA.addState("followPlayer", new EnemyState((enemyStateParams)=>{
            Vector3 moveDirection = (enemyStateParams.player.transform.position - enemyStateParams.self.transform.position).normalized;
            enemyStateParams.self.setVelocity(moveDirection * enemyStateParams.self.speed);
        }));
        /*  ADDING A NEW TRANSITION: requires the name of StateA followed by the name StateB,
            followed by a function delegate with the parameter enemyStateParams. Each frame,
            that StateA is active this delegate will be run. If it returns TRUE, the state machine
            will transition to StateB, otherwise it stays on StateA */
        stateMachineA.addTransition("patrol", "followPlayer", (enemyStateParams)=>{
            float distToPlayer = (enemyStateParams.player.transform.position - enemyStateParams.self.transform.position).sqrMagnitude;
            return distToPlayer <= Mathf.Pow(6, 2);
        });
        stateMachineA.addTransition("followPlayer", "patrol", (enemyStateParams)=>{
            float distToPlayer = (enemyStateParams.player.transform.position - enemyStateParams.self.transform.position).sqrMagnitude;
            return distToPlayer >= Mathf.Pow(8, 2);
        });
        /*  SETS THE STARTING STATE OF THE STATE MACHINE */
        stateMachineA.setCurrentState("patrol");
        /*  ADDS THE STATE MACHINE TO THE POOL */
        stateMachines.Add(stateMachineA);
        /*  END OF EXAMPLE EXPLANATION [: */
        
        //------------------------STATE MACHINE B------------------------
        EnemyStateMachine stateMachineB = new EnemyStateMachine();
        stateMachineB.addState("idle", new EnemyState((enemyStateParams)=>{
            enemyStateParams.self.setVelocity(Vector2.zero);
        }));
        stateMachineB.addState("followPlayer", new EnemyState((enemyStateParams)=>{
            Vector3 moveDirection = (enemyStateParams.player.transform.position - enemyStateParams.self.transform.position).normalized;
            enemyStateParams.self.setVelocity(moveDirection * enemyStateParams.self.speed);
        }));
        stateMachineB.addTransition("idle", "followPlayer", (enemyStateParams)=>{
            float distToPlayer = (enemyStateParams.player.transform.position - enemyStateParams.self.transform.position).sqrMagnitude;
            return distToPlayer <= Mathf.Pow(8, 2);
        });
        stateMachineB.addTransition("followPlayer", "idle", (enemyStateParams)=>{
            float distToPlayer = (enemyStateParams.player.transform.position - enemyStateParams.self.transform.position).sqrMagnitude;
            return distToPlayer >= Mathf.Pow(10, 2);
        });
        stateMachineB.setCurrentState("idle");
        stateMachines.Add(stateMachineB);
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
    public PlayerMovement player;

    public BehaviorParams(EnemyBase self, EnemyBase[] enemies, PlayerMovement player) {
        this.self = self;
        this.enemies = enemies;
        this.player = player;
    }
}
