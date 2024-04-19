using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Build.Content;
using UnityEngine;


public class EnemyHealth : MonoBehaviour, IDamageable
{
    public static event Action OnEnemyDeadEvent;
    [Header("Config")]
    [SerializeField] private float health;
    [SerializeField] private string enemyType;
    [SerializeField] private PlayerStats stats;

    [Header("FX Config")]
    [SerializeField] private ParticleSystem slashFX;
    public float CurrentHealth { get; private set; }

    private Animator animator;
    private EnemyBrain enemyBrain;
    private EnemySelector enemySelector;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyBrain = GetComponent<EnemyBrain>();
        enemySelector = GetComponent<EnemySelector>();
    }
    private void Start()
    {
        CurrentHealth = health;
    }


    // water weakness variables here
    private bool isInWater = false;
    private bool isInStone = false;
    public void TakeDamage(float amount, Vector2 plrDirection)
    {
        // handle weaknesses here:
        float totalDmg = amount;

        if (enemyType == "Water")
        {
            if (isInWater)
            {
                totalDmg *= 1.5f;
            }
        }
        else if (enemyType == "Stone")
        {
            if (isInStone)
            {
                totalDmg *= 1.5f;
            }
        }
        else if (enemyType == "TopDirection")
        {
            // print(plrDirection);
            if (plrDirection.y > transform.position.y)
            {
                totalDmg *= 1.5f;
            }
        }
        else if (enemyType == "LeftDirection")
        {
            // print(plrDirection);
            if (plrDirection.x < transform.position.x)
            {
                totalDmg *= 1.5f;
            }
        }

        slashFX.Play();

        CurrentHealth -= totalDmg;

        if (CurrentHealth <= 0f)
        {

            stats.Health = stats.MaxHealth;
            // todo
            animator.SetTrigger("Dead");
            enemyBrain.enabled = false;
            enemySelector.NoSelectionCallback();
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

            OnEnemyDeadEvent?.Invoke();
        }
        else
        {
            DamageManager.Instance.ShowDamageText(totalDmg, transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // print("hit something");
        if (other.CompareTag("Water"))
        {
            //print("Yes water");
            isInWater = true;
        }

        if (other.CompareTag("Stone"))
        {
            //print("Yes stone");
            isInWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            //print("No water");
            isInWater = false;
        }

        if (other.CompareTag("Stone"))
        {
            //print("No stone");
            isInWater = false;
        }
    }




}