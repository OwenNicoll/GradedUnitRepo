using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy
{
    private float fireTimer;

    [SerializeField]
    private GameObject enemyProjectile;

    [SerializeField]
    private GameObject rotatePoint;

    [SerializeField]
    private GameObject firePoint;

    private Vector3 direction;
    private float rotationAngle;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;

        if(distanceToPlayer <= 30f)
        {
            if (fireTimer >= 3)
            {
                Instantiate(enemyProjectile, firePoint.transform.position, Quaternion.identity);
                fireTimer = 0;
            }

            direction = player.transform.position - rotatePoint.transform.position;
            rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rotatePoint.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
        }

       
        
    }
}
