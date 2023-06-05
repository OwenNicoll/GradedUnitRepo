using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Player : MonoBehaviour
{

    private int score;

    private int health = 100;
   

    // Projectile Speed
    [SerializeField]
    private float moveSpeed = 27.5f;

    // RigidBody
    private Rigidbody2D rb;

    // Projectile object
    [SerializeField]
    private GameObject projectile;

    // Fire point transorms
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
    private float fuelDrainRate = 1f;
    private float fuelTimer = 0;



     private bool movingRight;
     private bool movingLeft;
     private bool movingUp;
     private bool movingDown;
     private bool standingStill;

    
    public Text fuelText;
    public Text healthText;
    public Text scoreText;

    private bool burst = false;
    private float burstTimer;
    private float burstCooldown;

    private bool shield = false;
    private float shieldTimer = 0;

    [SerializeField]
    private GameObject shieldSprite;



    //--------------------------------------------------------------------------------------------------------------------------------------
    //  START
    //--------------------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        // Get Rigid body
        rb = GetComponent<Rigidbody2D>();

        // Get projectile
       // projectile = GameObject.FindWithTag("Projectile");

    }

    // Function to set rigid body velocity
    void SetVelocity(float xVel, float yVel)
    {
        rb.velocity = new Vector2(xVel * moveSpeed, yVel * moveSpeed);
    }

    void SpawnProjectile(Transform spawnPoint1, Transform spawnPoint2)
    {
        Instantiate(projectile, spawnPoint1.transform);
        Instantiate(projectile, spawnPoint2.transform);
    }

    void FixedUpdate()
    {
        // Get axis inputs
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

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

    //--------------------------------------------------------------------------------------------------------------------------------------
    //  UPDATE
    //--------------------------------------------------------------------------------------------------------------------------------------
    void Update()
    {

        if(health <= 0 || fuel <=0)
        {
            Death();
        }

       
        // Score label
        scoreText.text = "Score: " + score.ToString();

        // Shield Timer
        
        

        if(shield)
        {
            shieldTimer += Time.deltaTime;

            shieldSprite.GetComponent<SpriteRenderer>().enabled = true;

            if (shieldTimer >= 10)
            {
                shieldSprite.GetComponent<SpriteRenderer>().enabled = false;
                shield = false;
                shieldTimer = 0;
            }
        }

        // Start fire timer
        fireTimer += Time.deltaTime;

        // Check if cooldown has passed....
        if (fireTimer >= 0.1f)
        {
            // Allow player to shoot
            canFire = true;
        }

        // Drain fuel
        fuelTimer += Time.deltaTime;
        DrainFuel();

        // Check for shoot inputs
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (canFire)
            {
                ShootLeft();
                canFire = false;
                fireTimer = 0;
            }

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (canFire)
            {
                ShootRight();
                canFire = false;
                fireTimer = 0;
            }

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (canFire)
            {
                ShootDown();
                canFire = false;
                fireTimer = 0;
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (canFire)
            {
                ShootUp();
                canFire = false;
                fireTimer = 0;
            }
        }

        
        if (burst)
        {
            
            burstCooldown += Time.deltaTime;
            burstTimer += Time.deltaTime;
            if(burstTimer >= 0.1)
            {
                ShootLeft();
                ShootRight();
                ShootUp();
                ShootDown();
                burstTimer = 0;
            }
            if(burstCooldown >= 5)
            {
                burstCooldown = 0;
                burst = false;
            }
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------
    //  METHODS
    //--------------------------------------------------------------------------------------------------------------------------------------
    private void DrainFuel()
    {
        if (fuelTimer >= 1)
        {
            fuel -= 1;
            fuelTimer = 0;
        }
    }

    // Fuel setter + getter
    public int GetFuel()
    {
        return fuel;
    }
    public void SetFuel(int newAmount)
    {
        fuel = newAmount;
    }

    // Health Setter + Getter
    public int GetHealth()
    {
        return health;
    }

    // Score setter
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }
    public void RemoveHealth(int healthToRemove)
    {
        health -= healthToRemove;
    }
    public void Burst()
    {
        burst = true;
    }

    public void Shield()
    {
        shield = true;
    }

    public bool GetShield()
    {
        return shield;
    }

    public bool GetMovingUp()
    {
        return movingUp;
    }

    public bool GetMovingLeft()
    {
        return movingLeft;
    }

    public bool GetMovingRight()
    {
        return movingRight;
    }

    public bool GetMovingDown()
    {
        return movingDown;
    }

    public bool GetStandingStill()
    {
        return standingStill;
    }

    private void Death()
    {
        SceneManager.LoadScene("GameOver");
    }
}
