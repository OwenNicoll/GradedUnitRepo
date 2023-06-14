using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCircle : MonoBehaviour
{

    private bool isDamaging = false;
    private float damageTimer = 0;
    private float despawnTimer = 0;
    private SpriteRenderer spriteRenderer;
   


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageTimer = 0;
         
    }

    // Update is called once per frame
    void Update()
    {
        damageTimer += Time.deltaTime;
        if (damageTimer >= 1.2f)
        {
            isDamaging = true;

            spriteRenderer.color = Color.red;
        }

        if (damageTimer >= 5f)
        {
            Destroy(gameObject);
        }

       
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isDamaging)
            {
                collision.gameObject.GetComponent<Player>().RemoveHealth(10);
            }
        }
    }
}
