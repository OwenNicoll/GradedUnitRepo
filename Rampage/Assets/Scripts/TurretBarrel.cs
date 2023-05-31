using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBarrel : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        // Get sprite renderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentColor = spriteRenderer.color;

        enemySprite = spriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        // Start Timers
        colourTimer += Time.deltaTime;

        // Change color back
        if (colourTimer >= 0.05f)
        {
            spriteRenderer.sprite = enemySprite;
        }
        
    }

    
}
