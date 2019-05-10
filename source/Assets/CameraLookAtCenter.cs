using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraLookAtCenter : MonoBehaviour
{
    [SerializeField] private Transform center; 
    void Update()
    {
        transform.LookAt(center.position);
    }
}
