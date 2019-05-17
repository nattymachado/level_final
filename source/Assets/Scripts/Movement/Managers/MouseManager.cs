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
    private bool botaoEsquerdo = false;
    private bool botaoDireito = false;
    private bool pinched = false;
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
        StartCoroutine(changeEnabledState(status));
    }

    private IEnumerator changeEnabledState(bool status)
    {
        yield return null;
        isEnabled = !status;
    }

    void Update()
    {
        if (controller.IsOnInventary(Input.mousePosition))
        {
            return;
        }

        if (isEnabled)
        {

            mousewheelAxis = Input.GetAxis("Mouse ScrollWheel");

            if (mousewheelAxis != 0)
            {
                controller.Pinch(-mousewheelAxis * mouseWheelScale, Input.mousePosition);
                pinched = true;
            }
            else
            {

                // stop pinching
                if (pinched)
                {
                    controller.StopPinch();
                    pinched = false;
                }

                // botao esquerdo
                if (!botaoDireito)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        botaoEsquerdo = true;
                        controller.Drag(TouchPhase.Began, Input.mousePosition);
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        botaoEsquerdo = true;
                        controller.Drag(TouchPhase.Ended, Input.mousePosition);
                    }
                    else if (Input.GetMouseButton(0))
                    {
                        botaoEsquerdo = true;
                        controller.Drag(TouchPhase.Moved, Input.mousePosition);
                    }
                    else
                    {
                        botaoEsquerdo = false;
                    }
                }
                // botao direito
                if (!botaoEsquerdo)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        botaoDireito = true;
                        // controller.TwoFingersDrag(TouchPhase.Began, Input.mousePosition);
                    }
                    else if (Input.GetMouseButtonUp(1))
                    {
                        botaoDireito = true;
                        controller.StopMultiFingerDrag();
                    }
                    else if (Input.GetMouseButton(1))
                    {
                        botaoDireito = true;
                        controller.MultiFingerDrag(Input.mousePosition);
                    }
                    else
                    {
                        botaoDireito = false;
                    }
                }
            }
        }
    }
}