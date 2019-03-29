using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{

  public float MinSwipeDistX;
  [SerializeField] private CharacterBehaviour character;
  [SerializeField] private float speed = 10;
  [SerializeField] private GameObject zoomPointer;

  private Vector2 startPos;
  [SerializeField] private CameraBehaviour cameraBehaviour;
  [SerializeField] private float perspectiveZoomSpeed = 15f;


  private float mousewheelAxis;
  private bool hasMoved = false;



  void Update()
  {

    mousewheelAxis = Input.GetAxis("Mouse ScrollWheel");

    if (mousewheelAxis != 0)
    {
      // increment zoom based on mouse wheel
      cameraBehaviour.ChangeFoV(-mousewheelAxis * perspectiveZoomSpeed);


    }

    if (Input.GetMouseButtonDown(0))
    {
      hasMoved = false;
      startPos = Input.mousePosition;
    }
    else if (Input.GetMouseButton(0))
    {
      hasMoved = RotateCamera(Input.mousePosition) || hasMoved;
      startPos = Input.mousePosition;
    }
    else if (Input.GetMouseButtonUp(0))
    {
      if (!hasMoved)
      {
        character.Move(Input.mousePosition);
      }
      hasMoved = false;
    }
  }

  private bool RotateCamera(Vector3 position)
  {
    float swipeDistHorizontal = (new Vector3(position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
    if (swipeDistHorizontal > MinSwipeDistX)
    {

      float swipeValue = Mathf.Sign(position.x - startPos.x);

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
