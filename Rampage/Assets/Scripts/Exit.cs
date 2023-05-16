using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private GameObject[] generatorArray = new GameObject[3];
    private bool allGensDestroyed = false;
    

    // Start is called before the first frame update
    void Start()
    {
        // Find generators
        generatorArray[0] = GameObject.Find("Crystal1");
        generatorArray[1] = GameObject.Find("Crystal2");
        generatorArray[2] = GameObject.Find("Crystal3");
    }

    // Update is called once per frame
    void Update()
    {
        // all generators are null
        allGensDestroyed = true;

        // Loop through generator array
        for (int i = 0; i < 3; i++)
        {
            // Check if generator is null
            if (generatorArray[i] != null)
            {
                allGensDestroyed = false;

                // break if gen is not null
                break;
            }
                       
        }

        if (allGensDestroyed)
        {
            Debug.Log("Exit is open!");
        }
    }

    
}
