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
    protected int health = 50;

    protected float despawnTimer;

    protected float colourTimer;

    protected Sprite enemySprite;
    [SerializeField]
    protected Sprite damageSprite;

    [SerializeField]
    protected GameObject scorePickup;

    [SerializeField]
    protected GameObject shieldPowerup;

    [SerializeField]
    protected GameObject fuelPowerup;
    [SerializeField]
    protected GameObject healthPowerup;
    [SerializeField]
    protected GameObject burstPowerup;

    protected float spawnChance;


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
        
    }

    // Update is called once per frame
    void Update()
    {
        // Start Timers
        colourTimer += Time.deltaTime;
       
        // Change color back
        if (colourTimer >= 0.05f)
        {
            spriteRenderer.sprite = enemySprite;
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
            SpawnScore();
            SpawnPowerup();
            
            Destroy(gameObject);
        }

        // Check if enemy is out of range
        if(distanceToPlayer >= 60)
        {
            despawnTimer += Time.deltaTime;
            if (despawnTimer >= 3)
            {
                Destroy(gameObject);
            }           
        }

        // Check if enemy is back in range
        if (distanceToPlayer <= 99f)
        {
            despawnTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if enemy is hit by projectile
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // Change sprite colour
            ChangeColour();      
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for collision with player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if player shield is active
            if (!collision.gameObject.GetComponent<Player>().GetShield())
            {
                // Remove health from player if shield is not active
                collision.gameObject.GetComponent<Player>().RemoveHealth(10);
            }
            
        }
    }

    // Function to change enemy sprite when hit
    public void ChangeColour()
    {   
        // Change sprite
        spriteRenderer.sprite = damageSprite;
        colourTimer = 0;
    }

    
    
    public void RemoveHealth(int healthToRemove)
    {
        health -= healthToRemove;
    }

    public void SpawnScore()
    {
        Instantiate(scorePickup, transform.position, Quaternion.identity);
    }

    public void SpawnFuel()
    {
        Instantiate(fuelPowerup, transform.position, Quaternion.identity);
    }
    public void SpawnHealth()
    {
        Instantiate(healthPowerup, transform.position, Quaternion.identity);
    }
    public void SpawnBurst()
    {
        Instantiate(burstPowerup, transform.position, Quaternion.identity);
    }
    public void SpawnShield()
    {
        Instantiate(shieldPowerup, transform.position, Quaternion.identity);
    }

    // Function to spawn powerups when enemy is killed
    public void SpawnPowerup()
    {
        // Get random spawn chance
        spawnChance = Random.Range(0f, 1f);

        // 10% chance for a powerup to spawn
        if(spawnChance >= 0.9)
        {
            // 1% chance for burst to spawn
            if(spawnChance == 0.91)
            {
                SpawnBurst();
            }

            // 1% chance for shield to spawn
            if(spawnChance == 0.92)
            {
                SpawnShield();
            }

            // 4% chance for health to spawn
            else if (spawnChance > 0.92 && spawnChance <= 0.96)
            {
                SpawnHealth();
            }

            // 4% chance for fuel to spawn
            else if(spawnChance > 0.96 && spawnChance <= 1)
            {
                SpawnFuel();
            }
           
        }
    }    
}
