using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    public float MinSwipeDistX;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private float speed = 10;

    private Vector2 startPos;
    private CameraBehaviour cameraBehaviour;

    private void Start()
    {
        cameraBehaviour = mainCamera.GetComponent<CameraBehaviour>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;
                case TouchPhase.Moved:
                    RotateCamera(touch);
                    startPos = touch.position;
                    break;
                case TouchPhase.Ended:
                    break;
            }

        }
    }

    private void RotateCamera(Touch touch)
    {
        float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
        if (swipeDistHorizontal > MinSwipeDistX)
        {

            float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

            if (swipeValue > 0)
            {
                cameraBehaviour.RotateCameraToLeft(Mathf.Abs(swipeValue) * speed);
            }
            else if (swipeValue < 0)
            {
                cameraBehaviour.RotateCameraToRight(Mathf.Abs(swipeValue) * speed);
            }
        }
    }
}
