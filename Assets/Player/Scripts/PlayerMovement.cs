using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed;

    private PlayerActions actions;
    private Rigidbody2D rb2D;
    private Vector2 moveDirection;
    private PlayerAnimations playerAnimations;
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
        actions = new PlayerActions();
        rb2D = GetComponent<Rigidbody2D>();
        playerAnimations = GetComponent<PlayerAnimations>();
        
    }
   
    // Update is called once per frame
    void Update()
    {
        ReadMovement();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        if (player.Stats.Health <= 0) return;
        
        // fixedDeltaTime ensures that movement is independent of the framerate
        rb2D.MovePosition(rb2D.position + moveDirection * (speed * Time.fixedDeltaTime));
        
        
    }
    private void ReadMovement()
    {
        // normalized: vector maintains a constant length of 1 -> prevents diagonal movement from having extra length
        moveDirection = actions.Movement.Move.ReadValue<Vector2>().normalized;
        if (moveDirection == Vector2.zero)
        {
            playerAnimations.SetMoveBoolTransition(false);
            return;
        }

        playerAnimations.SetMoveBoolTransition(true);
        playerAnimations.SetMoveAnimation(moveDirection);

    }
    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}
