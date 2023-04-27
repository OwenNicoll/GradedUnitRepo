using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed = 5f;
    protected Rigidbody2D rb;
    protected Transform playerTransform;
    protected float minDistance = 1f;
    private float distanceToPlayer;



    // Start is called before the first frame update
    void Start()
    {
        // Get rigid body
        rb = GetComponent<Rigidbody2D>();

        // Get player transform
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Get player direction
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // Get player distance
        distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > minDistance)
        {

            // Move towards player
            rb.MovePosition(rb.position + direction * moveSpeed);
        }

        if (distanceToPlayer < minDistance && distanceToPlayer != minDistance)
        {

            // Move away from player
            rb.MovePosition(rb.position - direction * moveSpeed);
        }
    }
}
