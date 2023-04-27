using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camera : MonoBehaviour
{

    private Transform playerTransform;
    private Vector3 cameraOffset = new Vector3(0, 0, -10);
    private Vector3 cameraTarget;


    private void Start()
    {
        // Get player transform
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Set camera target to players position
        cameraTarget = playerTransform.position + cameraOffset;

        // Move camera to camera target
        transform.position = Vector3.Lerp(transform.position, cameraTarget, Time.deltaTime * 1000f);
    }
}
