using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBar : HealthBar
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.GetComponent<Player>().GetFuel();
    }
}
