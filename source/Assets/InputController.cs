using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{


  [SerializeField] private CameraBehaviour _cameraBehaviour;
  [SerializeField] private GraphicRaycaster _raycaster;
  [SerializeField] private EventSystem _eventSystem;
  private MovimentController _movementController;
  private bool _hasRotated = false;
  private Vector3 startDragPosition;

  void Awake()
  {
    Input.simulateMouseWithTouches = false; // desablita reconhecimento de evento de muse no mobile

    _movementController = GetComponent<MovimentController>();
  }

  private void Click(Vector3 position)
  {
    _movementController.ActiveItem(position);
    _movementController.Move(position);
  }

  public void Drag(TouchPhase touchPhase, Vector3 screenPosition)
  {
    if (touchPhase == TouchPhase.Began)
    {
      _hasRotated = false;
      startDragPosition = screenPosition;
    }
    else if (touchPhase == TouchPhase.Moved)
    {
      _hasRotated = _cameraBehaviour.RotateCamera(startDragPosition, screenPosition) || _hasRotated;
      startDragPosition = screenPosition;
    }
    else if (touchPhase == TouchPhase.Ended)
    {
      if (!_hasRotated)
      {
        Click(screenPosition);
      }
      _hasRotated = false;
    }
  }

  public void Pinch(float zoomAxis)
  {
    if (zoomAxis != 0)
    {
      _cameraBehaviour.ChangeFoV(zoomAxis);
    }
  }

  public bool IsOnInventary(Vector3 position)
  {
    if (!_raycaster || _eventSystem)
      return false;
    //Set up the new Pointer Event
    PointerEventData _pointerEventData = new PointerEventData(_eventSystem);
    //Set the Pointer Event Position to that of the mouse position
    _pointerEventData.position = position;

    //Create a list of Raycast Results
    List<RaycastResult> results = new List<RaycastResult>();

    //Raycast using the Graphics Raycaster and mouse click position
    _raycaster.Raycast(_pointerEventData, results);

    return results.Count > 0;
  }

}
