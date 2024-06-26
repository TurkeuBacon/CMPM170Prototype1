using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActionAttack : FSMAction
{
    [Header("Config")]
    [SerializeField] private float damage;
    [SerializeField] private float timeBtwAttacks;

    private EnemyBrain enemyBrain;
    private float timer;
    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }
    public override void Act()
    {
        AttackPlayer();
    }

    public void AttackPlayer()
    {
        if (enemyBrain.Player == null) return;
        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            IDamageable player = enemyBrain.Player.GetComponent<IDamageable>();

            Vector2 direction = (enemyBrain.Player.position - transform.position).normalized;

            player.TakeDamage(damage, direction);
            timer = timeBtwAttacks;
        }
    }
}
