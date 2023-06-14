using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Player : MonoBehaviour
{
    // Health
    private int score;

    // Score
    private int health = 100;
   
    // Projectile Speed
    [SerializeField]
    private float moveSpeed = 27.5f;

    // RigidBody
    private Rigidbody2D rb;

    // Projectile prefab
    [SerializeField]
    private GameObject projectile;

    
    // Projectile spawn points
    [SerializeField]
    private Transform leftTransform;
    [SerializeField]
    private Transform leftTransform2;
    [SerializeField]
    private Transform rightTransform;
    [SerializeField]
    private Transform rightTransform2;
    [SerializeField]
    private Transform upTransform;
    [SerializeField]
    private Transform upTransform2;
    [SerializeField]
    private Transform downTransform;
    [SerializeField]
    private Transform downTransform2;

    // Bool for if the player can shoot
    private bool canFire = true;

    // Timer for shooting cooldown
    private float fireTimer;


    // Fuel
    private int fuel = 100;
    private float fuelTimer = 0;


     // Movement bools for enemy spawner
     private bool movingRight;
     private bool movingLeft;
     private bool movingUp;
     private bool movingDown;
     private bool standingStill;


    // Score Text
    [SerializeField]
    private TMP_Text scoreText;

    private bool burst = false;
    private float burstTimer;
    private float burstCooldown;

    private bool shield = false;
    private float shieldTimer = 0;

    private float horizontalInput;
    private float verticalInput;

    private Vector2 direction;

    [SerializeField]
    private GameObject shieldSprite;

    private bool invunrable = false;
    private float invunrableTimer;



    //--------------------------------------------------------------------------------------------------------------------------------------
    //  START
    //--------------------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        // Get Rigid body
        rb = GetComponent<Rigidbody2D>();

        // Start background music
        FindObjectOfType<AudioManager>().Play("GameMusic");
              
    }

    //--------------------------------------------------------------------------------------------------------------------------------------
    //  FIXED UPDATE
    //--------------------------------------------------------------------------------------------------------------------------------------

    void FixedUpdate()
    {
        // Get axis inputs
         horizontalInput = Input.GetAxisRaw("Horizontal");
         verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 velocity = new Vector2(horizontalInput, verticalInput).normalized;

        // Set velocity according to axis inputs
        SetVelocity(velocity.x, velocity.y);

        // Check for moving right
        if(horizontalInput > 0)
        {
            movingRight = true;
        }
        else
        {
            movingRight = false;
        }

        // Check for moving left
        if (horizontalInput < 0)
        {
            movingLeft = true;
        }
        else
        {
            movingLeft = false;
        }

        // Check for moving up
        if (verticalInput > 0)
        {
            movingUp = true;
        }
        else
        {
            movingUp = false;
        }

        // Check for moving down
        if(verticalInput < 0)
        {
            movingDown = true;
        }
        else
        {
            movingDown = false;
        }

        if((verticalInput == 0) && (horizontalInput ==0))
        {
            standingStill = true;
        }
        else
        {
            standingStill = false;
        }

    }



    //--------------------------------------------------------------------------------------------------------------------------------------
    //  UPDATE
    //--------------------------------------------------------------------------------------------------------------------------------------
    void Update()
    {

        // Timers
        fireTimer += Time.deltaTime;
        shieldTimer += Time.deltaTime;
       
        invunrableTimer += Time.deltaTime;


        // Set direction
        direction = new Vector2(horizontalInput, verticalInput);


        // Check if cooldown has passed....
        if (fireTimer >= 0.1f)
        {
            // Allow player to shoot
            canFire = true;
        }


        // Check for user input
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.J))
        {
            if (canFire)
            {
                ShootLeft();
                canFire = false;
                fireTimer = 0;
                FindObjectOfType<AudioManager>().PlayRandPitch("Gunshot");
            }

        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.L))
        {
            if (canFire)
            {
                ShootRight();
                canFire = false;
                fireTimer = 0;
                FindObjectOfType<AudioManager>().PlayRandPitch("Gunshot");
            }

        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.K))
        {
            if (canFire)
            {
                ShootDown();
                canFire = false;
                fireTimer = 0;
                FindObjectOfType<AudioManager>().PlayRandPitch("Gunshot");
            }
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.I))
        {
            if (canFire)
            {
                ShootUp();
                canFire = false;
                fireTimer = 0;
                FindObjectOfType<AudioManager>().PlayRandPitch("Gunshot");
            }
        }

        // Check for player death
        CheckForDeath();


        // Check if player is shielded
        if (shield)
        {
            // Enable shield renderer
            shieldSprite.GetComponent<SpriteRenderer>().enabled = true;
        }
        // Check if shield has been active for 10s
        if (shieldTimer >= 10)
        {
            // Remove shield
            RemoveShield();
        }

        // Update Score label
        scoreText.text = score.ToString();

       
                        
        // Drain fuel
        fuelTimer += Time.deltaTime;
        DrainFuel();

        // Check if player has burst powerup   
        if (burst)
        {
            burstCooldown += Time.deltaTime;
            burstTimer += Time.deltaTime;


            // Fire in all directions every 0.1s
            if (burstTimer >= 0.1)
            {
                ShootLeft();
                ShootRight();
                ShootUp();
                ShootDown();
                burstTimer = 0;
                FindObjectOfType<AudioManager>().PlayRandPitch("Gunshot");
            }

            // Check if 5s has passed
            if(burstCooldown >= 5)
            {
                // End burst
                burstCooldown = 0;
                burst = false;
            }
        }

              
        // If player has been invunrable for 0.5s
        if(invunrableTimer >= 0.5f)
        {
            // Set invunrable to false
            invunrable = false;
        }
    }


    //-----------------------------------------------------------------------------------------------------------------------------------------------
    //  METHODS
    //--------------------------------------------------------------------------------------------------------------------------------------------


    // Functions for shooting
    private void ShootLeft()
    {
        Instantiate(projectile, leftTransform.transform);
        Instantiate(projectile, leftTransform2.transform);
    }
    private void ShootRight()
    {
        Instantiate(projectile, rightTransform.transform);
        Instantiate(projectile, rightTransform2.transform);
    }
    private void ShootUp()
    {
        Instantiate(projectile, upTransform.transform);
        Instantiate(projectile, upTransform2.transform);
    }
    private void ShootDown()
    {
        Instantiate(projectile, downTransform.transform);
        Instantiate(projectile, downTransform2.transform);
    }

    // Drain fuel
    private void DrainFuel()
    {
        // Reduce fuel by 1 every second
        if (fuelTimer >= 1)
        {
            fuel -= 1;
            fuelTimer = 0;
        }
    }

    // Fuel getter
    public int GetFuel()
    {
        return fuel;
    }

    // Fuel setter
    public void SetFuel(int newAmount)
    {
        fuel = newAmount;
        FindObjectOfType<AudioManager>().Play("Fuel");
    }

    // Health Getter
    public int GetHealth()
    {
        return health;
    }

    // Health setter
    public void SetHealth(int newHealth)
    {
        health = newHealth;
        FindObjectOfType<AudioManager>().Play("Heal");
    }

    // Score setter
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    // Remove health
    public void RemoveHealth(int healthToRemove)
    {
        if (!invunrable && !shield)
        {
            health -= healthToRemove;
            invunrable = true;
            invunrableTimer = 0;
            FindObjectOfType<AudioManager>().PlayRandPitch("PlayerDamage");
        }
       
    }

    // Burst 
    public void Burst()
    {
        burst = true;

    }

    // Shield
    public void Shield()
    {
        shield = true;
        shieldTimer = 0;
    }
    public bool GetShield()
    {
        return shield;
    }

    // Moving up
    public bool GetMovingUp()
    {
        return movingUp;
    }

    // Moving Left
    public bool GetMovingLeft()
    {
        return movingLeft;
    }

    // Moving Right
    public bool GetMovingRight()
    {
        return movingRight;
    }

    // Moving Down
    public bool GetMovingDown()
    {
        return movingDown;
    }

    // Standing Still
    public bool GetStandingStill()
    {
        return standingStill;
    }

    // Death
    private void Death()
    {
        SceneManager.LoadScene("GameOver");
    }

    // Check for death
    private void CheckForDeath()
    {
        if(health <= 0 || fuel <= 0)
        {
            Death();
        }
    }

    // Get direction
    public Vector2 GetDirection()
    {
        return direction;
    }

    // Set velocity
    void SetVelocity(float xVel, float yVel)
    {
        rb.velocity = new Vector2(xVel * moveSpeed, yVel * moveSpeed);
    }

    
    // Show Shield
    private void ShowShield()
    {
        // Enable shield renderer
        shieldSprite.GetComponent<SpriteRenderer>().enabled = true;

        // Reset timer
        shieldTimer = 0;

       
    }

    private void RemoveShield()
    {

        // Disable shield Renderer
        shieldSprite.GetComponent<SpriteRenderer>().enabled = false;

        // Set shield to false
        shield = false;

        shieldTimer = 0;
    }
}
