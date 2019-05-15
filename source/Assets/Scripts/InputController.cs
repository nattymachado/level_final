using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{

  private static LayerMask _raycastMaskFloor;
  [SerializeField] private CameraBehaviour _cameraBehaviour;
  [SerializeField] private GraphicRaycaster _raycaster;
  [SerializeField] private EventSystem _eventSystem;
  private MovementController _movementController;
  private bool _hasRotated = false;
  private Vector3 startDragPosition;

  void Awake()
  {
    Input.simulateMouseWithTouches = false; // desablita reconhecimento de evento de muse no mobile

    _movementController = GetComponent<MovementController>();

    _raycastMaskFloor = LayerMask.GetMask(new string[] { "Floor" });
  }

  private void Click(Vector3 position)
  {
    _movementController.ActiveItemOrMove(position);
    _cameraBehaviour.ResetZoomDislocation();
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
      else
      {
        _cameraBehaviour.StopRotating();
        _hasRotated = false;
      }
    }
  }

  public void Pinch(float zoomAxis, Vector3 screenPosition)
  {
    if (zoomAxis != 0)
    {
      _cameraBehaviour.ChangeFoV(zoomAxis, screenPosition);
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
    
    if (results.Count > 0)
        {
            Debug.Log("E inventario");
        }
    return results.Count > 0;
  }

  public static bool IsPointOnBoard(Vector3 screenPosition, out RaycastHit hit)
  {
    Ray ray = Camera.main.ScreenPointToRay(screenPosition);

    RaycastHit[] hits = new RaycastHit[1];

    Physics.RaycastNonAlloc(ray, hits, 500f, _raycastMaskFloor);
    hit = hits[0];

    if (hits[0].collider != null && hits[0].collider.GetComponent<GridBehaviour>())
    {
      return true;
    }

    return false;
  }

}
