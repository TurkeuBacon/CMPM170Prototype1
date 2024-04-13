using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    EnemyStateMachine stateMachine;

    public int health;
    public float attack, defense, speed;

    private static List<EnemyBase> enemies;
    private PlayerMovement player;

    void Start() {
        if(enemies == null) {
            enemies = new List<EnemyBase>();
        }
        enemies.Add(this);
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update() {
        if(stateMachine != null) {
            stateMachine.execute(new EnemyStateParams(this, enemies.ToArray(), player));
        }
    }

    public void setSprite(Sprite sprite) {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public void setStateMachine(EnemyStateMachine stateMachine) {
        this.stateMachine = stateMachine.Clone();
    }
    public void setVelocity(Vector2 velocity) {
        GetComponent<Rigidbody2D>().velocity = velocity;
    }
    public void setStats(int health, float attack, float defense, float speed) {
        this.health = health;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
    }
}
