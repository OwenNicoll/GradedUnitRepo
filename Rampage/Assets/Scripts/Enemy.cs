using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //------------------------------------------------------------------------------------------------------------------------
    // DATA
    //------------------------------------------------------------------------------------------------------------------------
    [SerializeField]
    protected float moveSpeed = 2f;
    protected Rigidbody2D rb;
    protected Transform playerTransform;
    protected float minDistance = 3f;
    protected float distanceToPlayer;
    protected SpriteRenderer spriteRenderer;
    protected Color currentColor;
    protected int health = 60;

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

    protected Vector2 direction;

    

    protected GameObject[] powerupArray = new GameObject[4];

    


    //------------------------------------------------------------------------------------------------------------------------
    // START
    //------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        // Add powerups to array
        powerupArray[0] = healthPowerup;
        powerupArray[1] = fuelPowerup;
        powerupArray[2] = burstPowerup;
        powerupArray[3] = shieldPowerup;

        // Get rigid body
        rb = GetComponent<Rigidbody2D>();

        // Get player transform
        playerTransform = GameObject.FindWithTag("Player").transform;



        // Get sprite renderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentColor = spriteRenderer.color;

        enemySprite = spriteRenderer.sprite;
        
    }

    //------------------------------------------------------------------------------------------------------------------------
    // UPDATE
    //------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        // Start Timers
        colourTimer += Time.deltaTime;

        ReturnColor();
       

        // Get player direction
         direction = (playerTransform.position - transform.position).normalized;

        // Get player distance
        distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);



        // Move to player
        MoveTowardPlayer();





        // Check for enemy death
        CheckForDeath();

        // Check if enemy is out of range
        if(distanceToPlayer >= 53)
        {
            despawnTimer += Time.deltaTime;
            if (despawnTimer >= 1)
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
    //------------------------------------------------------------------------------------------------------------------------
    // FUNCTIONS
    //------------------------------------------------------------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if enemy is hit by projectile
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // Change sprite colour
            ChangeColour();      
        }
    }

    protected void OnCollisionStay2D(Collision2D collision)
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

    protected void ReturnColor()
    {
        // Change color back
        if (colourTimer >= 0.05f)
        {
            spriteRenderer.sprite = enemySprite;
        }
    }

    
    
    public void RemoveHealth(int healthToRemove)
    {
        health -= healthToRemove;
        FindObjectOfType<AudioManager>().PlayRandPitch("Hitmarker");
    }

    public void SpawnScore()
    {
        Instantiate(scorePickup, transform.position, Quaternion.identity);
    }


    // Function to spawn powerups when enemy is killed
    public void SpawnPowerup()
    {
        // Get random spawn chance
        spawnChance = Random.Range(0f, 1f);

        // 10% chance for a powerup to spawn
        if(spawnChance >= 0.95)
        {
            RandomDrop();
        }
    }       
    public void RandomDrop()
    {
        int randChance = Random.Range(0, powerupArray.Length);
        Instantiate(powerupArray[randChance], transform.position, Quaternion.identity);
    }
    
    public virtual void KillEnemy()
    {
        SpawnScore();
        SpawnPowerup();       
        Destroy(gameObject);
    }

    protected void CheckForDeath()
    {
        if(health <= 0)
        {
            KillEnemy();
        }
    }
    protected virtual void MoveTowardPlayer()
    {
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
    }
}
