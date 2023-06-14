using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : MonoBehaviour
{
    private float distanceToPlayer;
    private float moveSpeed = 0.75f;
    private GameObject player;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get player direction
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Get distance to player
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= 20)
        {
            // Move towards player
            rb.MovePosition(rb.position + direction * moveSpeed);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().AddScore(10);
            FindObjectOfType<AudioManager>().PlayRandPitch("Score");
            Destroy(gameObject);
        }
    }
}
