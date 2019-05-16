using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{
    private InputController controller;
    [SerializeField] private float mouseWheelScale = 0.2f;
    private float mousewheelAxis;
    private bool isEnabled;
  
    void Awake()
    {
        controller = GetComponent<InputController>();
        isEnabled = true;
    }

    private void Start()
    {
        GameEvents.UIEvents.PauseMenuStatusEvent += setActive;
    }

    private void OnDestroy()
    {
        GameEvents.UIEvents.PauseMenuStatusEvent -= setActive;
    }

    private void setActive(bool status)
    {
        StartCoroutine("changeEnabledState", status);
    }

    private IEnumerator changeEnabledState(bool status)
    {
        yield return null;
        isEnabled = !status;
    }

    void Update()
    {
        if(isEnabled)
        {
            if (controller.IsOnInventary(Input.mousePosition))
            {
                return;
            }

            mousewheelAxis = Input.GetAxis("Mouse ScrollWheel");

            if (mousewheelAxis != 0)
            {
                controller.Pinch(-mousewheelAxis * mouseWheelScale * Time.deltaTime, Input.mousePosition);
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    controller.Drag(TouchPhase.Began, Input.mousePosition);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    controller.Drag(TouchPhase.Ended, Input.mousePosition);
                }
                else if (Input.GetMouseButton(0))
                {
                    controller.Drag(TouchPhase.Moved, Input.mousePosition);
                }
            }
        }
    }
}