using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCrystal : Enemy
{

    


        // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemySprite = spriteRenderer.sprite;
        health = 1000;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        // Start Timers
        colourTimer += Time.deltaTime;

        // Change color back
        if (colourTimer >= 0.05f)
        {
            spriteRenderer.sprite = enemySprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            spriteRenderer.sprite = damageSprite;
            colourTimer = 0;

        }
    }
}
