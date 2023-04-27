using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float moveSpeed = 150f;

    private float destroyTimer;

    // Start is called before the first frame update
    void Start()
    {
        // Get Rigid Body
        rb = GetComponent<Rigidbody2D>();

        // Set velocity
        rb.velocity = transform.right * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Start destruction timer
        destroyTimer += Time.deltaTime;

        // Check if enough time has passed...
        if (destroyTimer >= 3f)
        {
            // Destroy the projectile
          //  Destroy(gameObject);
        }


    }
}
