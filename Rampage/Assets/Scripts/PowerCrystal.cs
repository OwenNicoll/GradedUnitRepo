using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCrystal : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        health = 400;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
