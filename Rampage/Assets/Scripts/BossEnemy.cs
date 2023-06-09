using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField]
    private GameObject damageZone;
    private float circleTimer;


    // Start is called before the first frame update
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

        health = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        // Change color back
        colourTimer += Time.deltaTime;
        if (colourTimer >= 0.05f)
        {
            spriteRenderer.sprite = enemySprite;
        }

        // Get player direction
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        if (distanceToPlayer > 2f)
        {

            // Move towards player
            rb.MovePosition(rb.position + direction * moveSpeed);
        }

        if (distanceToPlayer < 2f)
        {

            // Move away from player
            rb.MovePosition(rb.position - direction * moveSpeed);
        }
        // Spawn damage circles
        circleTimer += Time.deltaTime;
        if (circleTimer >= 5f)
        {
            SpawnCircle();
            circleTimer = 0f;
        }
    }


    public void SpawnCircle()
    {
        Instantiate(damageZone, playerTransform.position, Quaternion.identity);
        
    }
}
