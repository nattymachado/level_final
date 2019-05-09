using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TouchManager : MonoBehaviour
{

    [SerializeField] private MovementController controller;
    [SerializeField] private CameraBehaviour camera;


    void Update()
    {
        if (controller.IsOnInventary(Input.mousePosition))
        {
            return;
        }

        if (Input.touchCount == 2)
        {

            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);


            //zoomPointer.transform.position = (new Vector3(touchZero.position.x, touchZero.position.y, 0f) + new Vector3(touchOne.position.x, touchOne.position.y, 0f)) / 2;

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // Otherwise change the field of view based on the change in distance between the touches.
            //mainCamera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

            // Clamp the field of view to make sure it's between 0 and 180.
            //.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, _zoomMin, _zoomMax);
            camera.ChangeFoV(deltaMagnitudeDiff * 0.2f);

        }
        else if (Input.touchCount == 1)
        {
            Touch touch = Input.touches[0];

            controller.MoveOrRotate(touch.position, touch.phase == TouchPhase.Began, touch.phase == TouchPhase.Moved, touch.phase == TouchPhase.Ended);

        }
    }
    
}
