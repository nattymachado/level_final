using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{

    [SerializeField] private MovimentController controller;

#if UNITY_EDITOR
    void Update()
    {
        if (controller.IsOnInventary(Input.mousePosition))
        {
            return;
        }

        float mousewheelAxis = Input.GetAxis("Mouse ScrollWheel");

        if (mousewheelAxis != 0)
        {
            controller.Zoom(-mousewheelAxis);
        }
        else
        {
            controller.MoveOrRotate(Input.mousePosition, Input.GetMouseButtonDown(0), Input.GetMouseButton(0), Input.GetMouseButtonUp(0));
        }
    }

#endif

}