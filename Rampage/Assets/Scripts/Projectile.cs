using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float moveSpeed = 150f;

    private float destroyTimer;

    // Start is called before the first frame update
    void Start()
    {
        // Get Rigid Body
        rb = GetComponent<Rigidbody2D>();

        // Set velocity
        rb.velocity = transform.right * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Start destruction timer
        destroyTimer += Time.deltaTime;

        // Check if enough time has passed...
        if (destroyTimer >= 3f)
        {
            // Destroy the projectile
          //  Destroy(gameObject);
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet collided with an obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Destroy the bullet
            Destroy(gameObject);
        }

        // Check if the bullet collided with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Remove health from the enemy
            collision.gameObject.GetComponent<Enemy>().RemoveHealth(10);

            // Destroy bullet
            Destroy(gameObject);
        }
    }

    
}
