using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    
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

    private bool movingRight;
    private bool movingLeft;
    private bool movingUp;
    private bool movingDown;


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
       

        // Start fire timer
        fireTimer += Time.deltaTime;

        // Check if cooldown has passed....
        if (fireTimer >= 0.1f)
        {
            // Allow player to shoot
            canFire = true;
        }

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
    }
}
