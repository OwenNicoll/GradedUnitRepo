using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEnemy : Enemy
{
    [SerializeField]
    private GameObject damageZone;
    private float circleTimer;
    private float rotationSpeed = 200f;
    private float projectileTimer = 0f;
    private float circleCooldown = 5f;
    private float projectileCooldown = 2;
    [SerializeField] private GameObject projectile;


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

        health = 5000;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.forward, rotationSpeed * Time.deltaTime);

        // Change color back
        colourTimer += Time.deltaTime;
        if (colourTimer >= 0.05f)
        {
            spriteRenderer.sprite = enemySprite;
        }

        // Get player direction
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // Get player distance
        distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > 6f)
        {

            // Move towards player
            rb.MovePosition(rb.position + direction * moveSpeed);

        }


        if (distanceToPlayer < 3f)
        {
            // Move away from player
            rb.MovePosition(rb.position - direction * moveSpeed);
        }
        // Spawn damage circles
        circleTimer += Time.deltaTime;
        if (circleTimer >= circleCooldown)
        {
            SpawnCircle();
            circleTimer = 0f;
        }

        projectileTimer += Time.deltaTime;

        Shoot();

        if(health <= 0)
        {
            KillEnemy();
        }

        Enrage();
    }


    private void SpawnCircle()
    {
        Instantiate(damageZone, playerTransform.position, Quaternion.identity);       
    }

    private void Shoot()
    {
        if(projectileTimer >= projectileCooldown)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            projectileTimer = 0;
            FindObjectOfType<AudioManager>().PlayRandPitch("Laser");
        }
      
    }

    public override void KillEnemy()
    {
        for(int i = 0; i < 10; i++)
        {
            SpawnScore();
        }
        Destroy(gameObject);
        SceneManager.LoadScene("LevelComplete3");
    }

    private void Enrage()
    {
        if (health <= 2500)
        {
            circleCooldown = 3f;
            projectileCooldown = 1f;
            moveSpeed = 0.2f;
        }
        if(health <= 500)
        {
            moveSpeed = 0.4f;
            projectileCooldown = 0.5f;
        }
    }
}
