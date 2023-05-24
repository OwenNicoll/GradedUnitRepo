using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    protected float scaleSpeed = 0.3f;
    protected float scaleAmount = 0.5f;

    protected Vector3 initialScale;

    protected float randomX;
    protected float randomY;

    protected Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;

        transform.localScale = new Vector3(1.5f, 1.5f, 1f);

        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(GetRandomForce());
      
    }

    // Update is called once per frame
    void Update()
    {
        float scale = Mathf.PingPong(Time.time * scaleSpeed, scaleAmount);

        transform.localScale = initialScale + Vector3.one * scale;

    }

    protected Vector2 GetRandomForce()
    {
        randomX = Random.Range(-1, 1);
        randomY = Random.Range(-1, 1);


        if(randomX < 0)
        {
            randomX = -200;
        }

        if(randomX > 0)
        {
            randomX = 200;
        }

        if(randomY < 0)
        {
            randomY = -200;
        }

        if(randomY > 0)
        {
            randomY = 200;
        }

        Vector2 randForce = new Vector2(randomX, randomY);

        return randForce;
    }
}
