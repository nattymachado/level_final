using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyboardManager : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    
    private CameraBehaviour cameraBehaviour;

    private void Start()
    {
        cameraBehaviour = mainCamera.GetComponent<CameraBehaviour>();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            cameraBehaviour.RotateCameraToRight(90);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cameraBehaviour.RotateCameraToLeft(90);
        }
    }
}
