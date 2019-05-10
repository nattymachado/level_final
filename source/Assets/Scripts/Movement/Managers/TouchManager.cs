using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TouchManager : MonoBehaviour
{

  private InputController controller;
  [SerializeField] private float pinchScale = 0.2f;

  private bool isPinching = false;
  private bool isTouching = false;
  private int firstFingerId = 1000;

  void Awake()
  {
    controller = GetComponent<InputController>();
  }

  void Update()
  {
    if (controller.IsOnInventary(Input.mousePosition))
    {
      return;
    }

    int touchCount = Input.touches.Length;

    if (touchCount == 0)
    {
      if (isTouching)
      {
        isTouching = false;
        isPinching = false;
        Debug.Log("removed fingers");
      }
      firstFingerId = -1000;
    }
    else if (touchCount == 1)
    {
      Touch touch = Input.GetTouch(0);

      if (!isTouching)
      {
        firstFingerId = touch.fingerId;
        isTouching = true;
        Debug.Log("first finger");
      }

      if (!isPinching)
      {
        if (touch.fingerId == firstFingerId)
        {
          Debug.Log("first finger");
          controller.Drag(touch.phase, touch.position);
        }
      }
    }
    else
    {
      isTouching = true;
      isPinching = true;
      Debug.Log("second finger");

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

      controller.Pinch(deltaMagnitudeDiff * pinchScale * Time.deltaTime);

    }
  }

}
