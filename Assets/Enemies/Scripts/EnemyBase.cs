using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public delegate void MoveDelegate(BehaviorParams behaviorParams);
    public delegate void AttackDelegate(BehaviorParams behaviorParams);
 
    public int health;
    public float attack, defense, speed;
    private MoveDelegate doMove;
    private AttackDelegate doAttack;

    private static List<EnemyBase> enemies;
    private PlayerController player;

    void Start() {
        if(enemies == null) {
            enemies = new List<EnemyBase>();
        }
        enemies.Add(this);
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        if(doMove != null) {
            doMove(new BehaviorParams(this, enemies.ToArray(), player));
        }
        if(doAttack != null) {
            doAttack(new BehaviorParams(this, enemies.ToArray(), player));
        }
    }

    public void setSprite(Sprite sprite) {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public void setMoveBehavior(MoveDelegate newMove) {
        doMove = newMove;
    }
    public void setAttackBehavior(AttackDelegate newAttack) {
        doAttack = newAttack;
    }
    public void setStats(int health, float attack, float defense, float speed) {
        this.health = health;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
    }
}
