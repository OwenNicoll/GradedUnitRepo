using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    private Rigidbody2D rb;

    private Transform playerTransform;
    private float moveSpeed = 100f;

    private Vector3 direction;

    private GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        player = GameObject.FindGameObjectWithTag("Player");

        playerTransform = player.transform;

        direction = (playerTransform.position - transform.position).normalized;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
