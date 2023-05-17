using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveArrow : MonoBehaviour
{
    [SerializeField]
    private GameObject objective;
    private Vector3 direction;
    private float rotationAngle;

    [SerializeField]
    private GameObject arrowSprite;

    public bool destroy = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (objective)
        {
            direction = objective.transform.position - transform.position;
            rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(rotationAngle - 90, Vector3.forward);
        }
        else
        {
            Destroy(arrowSprite);
        }
       

       
    }


}