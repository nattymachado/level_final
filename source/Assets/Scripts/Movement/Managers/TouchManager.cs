using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TouchManager : MonoBehaviour
{

    [SerializeField] private CameraBehaviour cameraBehaviour;
    private InputController controller;
    [SerializeField] private float pinchScale = 0.2f;

    private bool isTouching = false;
    private bool wasDoubleTouching = false;
    private bool wasTripleTouching;
    private int firstFingerId = 1000;
    private float panZoomResetTime = 1f;
    private float panZoomResetTimer = 1f;
    private bool isZooming = false;
    private bool isPanning = false;
    private Vector2[] lastFrameTouchesPositions = new Vector2[2];
    private bool isEnabled;

    void Awake()
    {
        controller = GetComponent<InputController>();
        isEnabled = true;
    }

    private void Start()
    {
        GameEvents.UIEvents.OpenMenu += setActive;
    }

    private void OnDestroy()
    {
        GameEvents.UIEvents.OpenMenu -= setActive;
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

        if (!isEnabled)
        {
            return;
        }

        int touchCount = Input.touches.Length;

        if (touchCount == 0)
        {
            isTouching = false;
            firstFingerId = -1000;
            if (wasDoubleTouching)
            {
                controller.StopMultiFingerDrag();
                controller.StopPinch();
            }
            wasDoubleTouching = false;
            wasTripleTouching = false;
        }
        else
        {
            if (wasTripleTouching)
            {
                controller.MultiFingerDrag(Input.GetTouch(0).position);
            }
            else
            {
                if (touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);

                    if (!isTouching)
                    {
                        firstFingerId = touch.fingerId;
                        isTouching = true;
                    }

                    if (!wasDoubleTouching && !wasTripleTouching)
                    {
                        if (touch.fingerId == firstFingerId)
                        {
                            controller.Drag(touch.phase, touch.position);
                        }
                    }
                }
                else if (touchCount == 2)
                {
                    // Store both touches.
                    Vector2[] thisFrameTouchesPositions = new Vector2[2] { Input.GetTouch(0).position, Input.GetTouch(1).position };

                    if (!wasDoubleTouching)
                    {
                        lastFrameTouchesPositions = thisFrameTouchesPositions;
                        panZoomResetTimer = 1f;
                        wasDoubleTouching = true;
                    }

                    float newDistance = Vector2.Distance(thisFrameTouchesPositions[0], thisFrameTouchesPositions[1]);
                    float oldDistance = Vector2.Distance(lastFrameTouchesPositions[0], lastFrameTouchesPositions[1]);
                    float deltaMagnitudeDiff = oldDistance - newDistance;

                    Vector2 pontoMedio = thisFrameTouchesPositions[0] + (thisFrameTouchesPositions[1] - thisFrameTouchesPositions[0]) / 2;

                    controller.Pinch(deltaMagnitudeDiff * pinchScale, pontoMedio);

                    lastFrameTouchesPositions = thisFrameTouchesPositions;
                }
                else if (touchCount >= 3)
                {
                    if (!wasTripleTouching)
                    {
                        wasTripleTouching = true;
                    }
                }
            }

        }



    }

}
