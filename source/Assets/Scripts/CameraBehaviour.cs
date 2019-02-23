using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speedMod = 10.0f;//a speed modifier
    [SerializeField] private float upLimit = 10.0f;//limit to up moviment
    [SerializeField] private float downLimit = -10.0f;//limit to up moviment
    private Vector3 point;//the coord to the point where the camera looks at
    

    // Start is called before the first frame update
    void Start()
    {

        point = target.position;//get target's coords
        transform.LookAt(point);//makes the camera look to it

    }

    public void RotateCameraToRight()
    {
        if (target)
        {
            transform.RotateAround(point, new Vector3(0.0f, -1.0f, 0.0f), Time.deltaTime * speedMod);
        }
    }

    public void RotateCameraToLeft()
    {
        if (target)
        {
            transform.RotateAround(point, new Vector3(0.0f, 1.0f, 0.0f), Time.deltaTime * speedMod);
        }
    }

    public void RotateCameraToUp()
    {
        float rotationOnX = transform.eulerAngles.x;
        if (transform.eulerAngles.x > 180)
        {
            rotationOnX = transform.eulerAngles.x - 360;
        }

        Debug.Log(rotationOnX);
        if ((target) && (rotationOnX < upLimit))
        {
            transform.RotateAround(point, new Vector3(1.0f, 0.0f, 0.0f), Time.deltaTime * speedMod);
            
        }
    }

    public void RotateCameraToDown()
    {
        float rotationOnX = transform.eulerAngles.x;
        if (transform.eulerAngles.x > 180)
        {
            rotationOnX = transform.eulerAngles.x - 360;
        }
        
        Debug.Log(rotationOnX);
        if ((target) && (rotationOnX > downLimit))
        {
            transform.RotateAround(point, new Vector3(-1.0f, 0.0f, 0.0f), Time.deltaTime * speedMod);
        }
    }

}
