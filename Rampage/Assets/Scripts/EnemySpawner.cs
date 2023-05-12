using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private float minDistance = 45f;
    private float maxDistance = 46f;

    [SerializeField]
    private GameObject enemy;

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
        Instantiate(enemy, position, Quaternion.identity);
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
