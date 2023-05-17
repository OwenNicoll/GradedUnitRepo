using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : Powerup
{
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().SetHealth(100);
            Destroy(gameObject);
        }
    }

}
