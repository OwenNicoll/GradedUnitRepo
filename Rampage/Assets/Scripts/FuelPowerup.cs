using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPowerup : Powerup
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;

        transform.localScale = new Vector3(1.5f, 1.5f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        float scale = Mathf.PingPong(Time.time * scaleSpeed, scaleAmount);  

        transform.localScale = initialScale + Vector3.one * scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for collision with player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set players fuel to 100
            collision.gameObject.GetComponent<Player>().SetFuel(100);

            // Destroy fuel object
            Destroy(gameObject);
        }
    }
}
