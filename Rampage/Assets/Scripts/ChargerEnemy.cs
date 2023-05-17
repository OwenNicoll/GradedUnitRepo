using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerEnemy : Enemy
{
    private bool isBacktracking = false;
    private float backtrackTimer;

   


    // Start is called before the first frame update


    private void Awake()
    {
        moveSpeed = 0.6f;
    }

    void Start()
    {
        // Get rigid body
        rb = GetComponent<Rigidbody2D>();

        // Get player transform
        playerTransform = GameObject.FindWithTag("Player").transform;

        // Get sprite renderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentColor = spriteRenderer.color;

        enemySprite = spriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        // Start Timers
        colourTimer += Time.deltaTime;
        backtrackTimer += Time.deltaTime;
        

        

        // Change color back
        if (colourTimer >= 0.05f)
        {
            spriteRenderer.sprite = enemySprite;
        }

        // Get player direction
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // Get player distance
        distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);


        // Check if backtracking is false
        if (!isBacktracking)
        {
            // Move towards player
            rb.MovePosition(rb.position + direction * moveSpeed);
        }

        // Check if backtracking is true
        if (isBacktracking)
        {
            // Lower speed
            moveSpeed = 0.1f;

            // Move away from player
            rb.MovePosition(rb.position - direction * moveSpeed);


            if (backtrackTimer >= 4)
            {
                moveSpeed = 0.6f;
                isBacktracking = false;
            }
        }

        // Check for enemy death
        if (health <= 0)
        {
            SpawnScore();
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            ChangeColour();
            // health -= 20;
            // Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isBacktracking = true;
            backtrackTimer = 0;
        }
    }
}
