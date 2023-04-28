using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed = 5f;
    protected Rigidbody2D rb;
    protected Transform playerTransform;
    protected float minDistance = 3f;
    protected float distanceToPlayer;
    protected SpriteRenderer spriteRenderer;
    protected Color currentColor;
    protected int health = 100;

    protected float colourTimer;



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
    }

    // Update is called once per frame
    void Update()
    {
        // Start Timers
        colourTimer += Time.deltaTime;

        // Change color back
        if (colourTimer >= 0.05f)
        {
            spriteRenderer.color = currentColor;
        }

        // Get player direction
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // Get player distance
        distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > minDistance)
        {

            // Move towards player
            rb.MovePosition(rb.position + direction * moveSpeed);
        }

        if (distanceToPlayer < minDistance && distanceToPlayer != minDistance)
        {

            // Move away from player
            rb.MovePosition(rb.position - direction * moveSpeed);
        }

        // Check for enemy death
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            ChangeColour();
            health -= 10;
            Destroy(collision.gameObject);
        }
    }

    private void ChangeColour()
    {
       
        spriteRenderer.color = Color.white;
        colourTimer = 0;
    }
}
