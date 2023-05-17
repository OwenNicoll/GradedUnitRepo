using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private float minDistance = 50f;
    private float maxDistance = 52f;
    private float spawnChance;

    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private GameObject chargerEnemy;

    private float spawnTimer;

    public Vector3 GetRandomPosition()
    {
        // Calculate random angle
        float randAngle = Random.Range(0, 360);

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

        // 90% chance to spawn regular enemy
        if (spawnChance <= 0.9)
        {
             Instantiate(enemy, position, Quaternion.identity);
            
        }

        // 10% chance to spawn charger enemy
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
