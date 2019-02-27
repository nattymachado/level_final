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
    private float perspectiveZoomSpeed = 0.5f;

    private void Start()
    {
        cameraBehaviour = mainCamera.GetComponent<CameraBehaviour>();
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // Otherwise change the field of view based on the change in distance between the touches.
            mainCamera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

            // Clamp the field of view to make sure it's between 0 and 180.
            mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 5f, 24f);
        } else if (Input.touchCount == 1)
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
