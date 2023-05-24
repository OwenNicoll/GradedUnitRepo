using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : Powerup
{
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
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Shield();
            Destroy(gameObject);
        }
    }
}
