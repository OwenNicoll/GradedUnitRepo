using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 27.5f;
    private Rigidbody2D rb;
 
    // Start is called before the first frame update
    void Start()
    {
        // Get Rigid body
        rb = GetComponent<Rigidbody2D>();

    }

    // Function to set rigid body velocity
    void SetVelocity(float xVel, float yVel)
    {
        rb.velocity = new Vector2(xVel * moveSpeed, yVel * moveSpeed);
    }

    void FixedUpdate()
    {
        // Get axis inputs
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 velocity = new Vector2(horizontalInput, verticalInput).normalized;

        // Set velocity according to axis inputs
        SetVelocity(velocity.x, velocity.y);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
