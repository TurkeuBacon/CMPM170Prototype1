using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Weapon initialWeapon;
    [SerializeField] private Transform[] attackPositions;

    private PlayerActions actions;
    private PlayerAnimations playerAnimations;
    private EnemyBrain enemyTarget;
    // this stores the current coroutine (coroutines are methods but you can add times inside the methods so it runs asynchronously)
    private Coroutine attackCoroutine;
    private PlayerMovement playerMovement;

    private float currentAttackPosition;

    [Header("Melee Config")]
    [SerializeField] private float minDistanceMeleeAttack;

    public Weapon CurrentWeapon { get; set; }

    private void Awake()
    {
        actions = new PlayerActions();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
    }

    private void Start()
    {
        actions.Attack.ClickAttack.performed += ctx => Attack();
        CurrentWeapon = initialWeapon;
    }

    private void Attack()
    {
        if (enemyTarget == null) return;

        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

        attackCoroutine = StartCoroutine(IEAttack());
    }

    // IENumerator is the interface that allows you to manage Unity's coroutine systerm
    private IEnumerator IEAttack()
    {
        if (CurrentWeapon.WeaponType == WeaponType.Melee)
        {
            MeleeAttack();
            yield return new WaitForSeconds(0.5f); // add animations later
        }
    }

    private void MeleeAttack()
    {
        float currentDistanceToEnemy = Vector3.Distance(enemyTarget.transform.position, transform.position);

        if (currentDistanceToEnemy <= minDistanceMeleeAttack)
        {
            Vector2 direction = transform.position;
            enemyTarget.GetComponent<IDamageable>().TakeDamage(1f, direction);

        }
    }

    private void EnemySelectedCallback(EnemyBrain enemySelected)
    {
        enemyTarget = enemySelected;
    }

    private void NoEnemySelectionCallback()
    {
        enemyTarget = null;
    }
    private void OnEnable()
    {
        actions.Enable();
        SelectionManager.OnEnemySelectedEvent += EnemySelectedCallback;
        SelectionManager.OnNoSelectionEvent += NoEnemySelectionCallback;
    }

    private void OnDisable()
    {
        actions.Disable();
        SelectionManager.OnEnemySelectedEvent -= EnemySelectedCallback;
        SelectionManager.OnNoSelectionEvent -= NoEnemySelectionCallback;
    }
}
