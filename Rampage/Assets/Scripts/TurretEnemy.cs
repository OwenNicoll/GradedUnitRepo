using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy
{
    private float fireTimer;

    [SerializeField]
    private GameObject enemyProjectile;

    [SerializeField]
    private GameObject rotatePoint;

    [SerializeField]
    private GameObject firePoint;

    [SerializeField]
    private GameObject turretBarrel;

    private float rotationAngle;

    private GameObject player;

    private bool canFire = false;

    
    // Start is called before the first frame update
    void Start()
    {
        health = 300;

        player = GameObject.FindWithTag("Player");

        // Get sprite renderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentColor = spriteRenderer.color;

        enemySprite = spriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {

        // Get player distance
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        fireTimer += Time.deltaTime;

        if(distanceToPlayer <= 50)
        {
            canFire = true;
        }
        else
        {
            canFire = false;
        }


        if(canFire)
        {
            if (fireTimer >= 3)
            {
                Shoot();
            }


            RotateBarrel();
            
        }

        // Check for enemy death
        CheckForDeath();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            turretBarrel.GetComponent<Enemy>().ChangeColour();
        }
    }

    private void Shoot()
    {
        Instantiate(enemyProjectile, firePoint.transform.position, Quaternion.identity);
        fireTimer = 0;
        FindObjectOfType<AudioManager>().PlayRandPitch("Laser");
    }

    private void RotateBarrel()
    {
        direction = player.transform.position - rotatePoint.transform.position;
        rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotatePoint.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
    }
}
