using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotateAngle = 90;
    private Vector3 point;//the coord to the point where the camera looks at
    

    // Start is called before the first frame update
    void Start()
    {

        point = target.position;//get target's coords
        transform.LookAt(point);//makes the camera look to it

    }

    public void RotateCameraToRight(float angle)
    {
        if (target)
        {
            transform.RotateAround(point, new Vector3(0.0f, -1.0f, 0.0f), angle);
        }
    }

    public void RotateCameraToLeft(float angle)
    {
        if (target)
        {
            transform.RotateAround(point, new Vector3(0.0f, 1.0f, 0.0f), angle);
        }
    }

   

}
