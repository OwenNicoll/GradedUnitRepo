using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    private Rigidbody2D rb;

    private Transform playerTransform;
    private float moveSpeed = 50f;

    private Vector3 direction;

    private GameObject player;

    private float rotationAngle;

    private float despawnTimer;



    // Start is called before the first frame update
    void Start()
    {
        // Get rigid body
        rb = GetComponent<Rigidbody2D>();

        // Get player ovhect
        player = GameObject.FindGameObjectWithTag("Player");

        // Get player transform
        playerTransform = player.transform;

        // Get direction to player
        direction = (playerTransform.position - transform.position).normalized;

        rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);

    }

    // Update is called once per frame
    void Update()
    {
        // Move towards players position
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Start despawn timer
        despawnTimer += Time.deltaTime;

        // Check if its time to despawn
        if(despawnTimer >= 5)
        {
            // Destroy projectile
            Destroy(gameObject);
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if projectile hit player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if player is shielded
            if (!collision.gameObject.GetComponent<Player>().GetShield())
            {
                // Remove health from player
                collision.gameObject.GetComponent<Player>().RemoveHealth(10);
            }
            
            Destroy(gameObject);
        }

        // Check if projcetile hit obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
