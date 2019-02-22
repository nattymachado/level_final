using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIBehaviour : MonoBehaviour
{
    [SerializeField] private Camera mainCamera = null;



    public void RightButtonOnClick()
    {
        CameraBehaviour cameraBehaviour = mainCamera.GetComponent<CameraBehaviour>();
        cameraBehaviour.RotateCameraToRight();
    }
}
