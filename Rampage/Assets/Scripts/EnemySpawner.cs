using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 180, 360 = down
    //90, 270 = left
    //0, 180 = top
    //
    private float minDistance = 50f;
    private float maxDistance = 52f;
    private float spawnChance;

    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private GameObject chargerEnemy;

    private float spawnTimer;

    private float randAngle;

    public Vector3 GetRandomPosition()
    {
        // If player is moving right
        if (gameObject.GetComponent<Player>().GetMovingRight() == true)
        {
            randAngle = Random.Range(-90, 90);
        }

        // If player is moving left
        if (gameObject.GetComponent<Player>().GetMovingLeft() == true)
        {
           randAngle = Random.Range(90, 270);
        }

        // If player is moving up
        if (gameObject.GetComponent<Player>().GetMovingUp() == true)
        {
            randAngle = Random.Range(0, 180);
        }

        // If player is moving down
        if (gameObject.GetComponent<Player>().GetMovingDown() == true)
        {
            randAngle = Random.Range(180, 360);
        }


        // Calculate random angle
       // randAngle = Random.Range(-90, 90);

        // Calculate random distance
        float distance = Random.Range(minDistance, maxDistance);

        // Calculate spawn position
        Vector2 spawnPosition = transform.position + (Quaternion.Euler(0, 0, randAngle) * Vector2.right) * distance;

        return spawnPosition;
    }

    public void SpawnEnemy(Vector3 position)
    {
        // Generate random number
        spawnChance = Random.Range(0f, 1f);

        // 95% chance to spawn regular enemy
        if (spawnChance <= 0.95)
        {
             Instantiate(enemy, position, Quaternion.identity);
            
        }

        // 5% chance to spawn charger enemy
        else
        {
            Instantiate(chargerEnemy, position, Quaternion.identity);
        }
                        
    }

   

    // Update is called once per frame
    void Update()
    {
        // start spawn timer
        spawnTimer += Time.deltaTime;

        // Check if it is time to spawn enemy
        if (spawnTimer >= 1)
        {
            SpawnEnemy(GetRandomPosition());
            spawnTimer = 0;
        }
    }
}
