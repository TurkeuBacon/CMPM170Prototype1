using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static event Action<EnemyBrain> OnEnemySelectedEvent;
    public static event Action OnNoSelectionEvent;

    [Header("Config")]
    [SerializeField] private LayerMask enemyMask;

    private Camera mainCamera;

    private void Awake()
    { 
        mainCamera = Camera.main;
    }

    private void Update()
    {
        SelectEnemy();
    }
    private void SelectEnemy()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, Mathf.Infinity, enemyMask);

            if (hit.collider != null)
            {
                print("Hit something");
                EnemyBrain enemy = hit.collider.GetComponent<EnemyBrain>();
                if (enemy != null)
                {
                    // ? just checks if it's existing first
                    OnEnemySelectedEvent?.Invoke(enemy);
                }
            }
            else
            {
                print("Hit nothing");
                OnNoSelectionEvent?.Invoke();
            }
        }
    }



}
