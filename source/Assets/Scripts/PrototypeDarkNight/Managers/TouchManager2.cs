using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager2 : MonoBehaviour
{
  public float MinSwipeDistX;

  [SerializeField] private CharacterBehaviour2 character;
  [SerializeField] private float speed = 10;
  [SerializeField] private GameObject zoomPointer;
  [SerializeField] private float perspectiveZoomSpeed = 0.2f;

  private Vector2 startPos;
  [SerializeField] private CameraBehaviour2 cameraBehaviour;
  private bool isMoving = false;


  void Update()
  {
    if (Input.touchCount == 2)
    {
      // Store both touches.
      Touch touchZero = Input.GetTouch(0);
      Touch touchOne = Input.GetTouch(1);


      zoomPointer.transform.position = (new Vector3(touchZero.position.x, touchZero.position.y, 0f) + new Vector3(touchOne.position.x, touchOne.position.y, 0f)) / 2;

      // Find the position in the previous frame of each touch.
      Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
      Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

      // Find the magnitude of the vector (the distance) between the touches in each frame.
      float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
      float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

      // Find the difference in the distances between each frame.
      float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

      // Otherwise change the field of view based on the change in distance between the touches.
      cameraBehaviour.ChangeFoV(deltaMagnitudeDiff * perspectiveZoomSpeed);
    }
    else if (Input.touchCount == 1)
    {
      Touch touch = Input.touches[0];

      switch (touch.phase)
      {
        case TouchPhase.Began:
          startPos = touch.position;
          isMoving = false;
          break;
        case TouchPhase.Moved:
          isMoving = RotateCamera(touch) || isMoving;
          startPos = touch.position;
          break;
        case TouchPhase.Ended:
          if (!isMoving)
          {
            character.Move(touch.position);
          }
          isMoving = false;
          break;
      }

    }
  }

  private bool RotateCamera(Touch touch)
  {
    float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
    if (swipeDistHorizontal > MinSwipeDistX)
    {

      float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

      if (swipeValue > 0)
      {
        cameraBehaviour.RotateCameraToLeft(Mathf.Abs(swipeValue) * speed * Time.deltaTime);
      }
      else if (swipeValue < 0)
      {
        cameraBehaviour.RotateCameraToRight(Mathf.Abs(swipeValue) * speed * Time.deltaTime);
      }
      return true;
    }
    return false;
  }
}
