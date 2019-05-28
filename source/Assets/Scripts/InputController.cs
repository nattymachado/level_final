using System;
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
  private bool _hasRotatedOnce = false;
  private bool _hasPannedOnce = false;
  private Vector3 startMultiFingersDragPosition;
  private Vector3 startDragPosition;
  private bool startedMultiFingerDrag = false;
  private bool isActive = true;
  private bool _hasZoomedOnce = false;
  private bool canPinch = true;
  private bool canClick = true;
  private bool canRotate = true;
  private bool canDrag = true;

  void Awake()
  {
    Input.simulateMouseWithTouches = false; // desablita reconhecimento de evento de muse no mobile

    _movementController = GetComponent<MovementController>();

    _raycastMaskFloor = LayerMask.GetMask(new string[] { "Floor" });
  }

  private void Click(Vector3 position)
  {
    _movementController.ActiveItemOrMove(position);
    _cameraBehaviour.ResetPanZoomDislocation();
  }

  public void Drag(TouchPhase touchPhase, Vector3 screenPosition)
  {
    if (isActive)
    {
      if (touchPhase == TouchPhase.Began)
      {
        _hasRotated = false;
        startDragPosition = screenPosition;
      }
      else if (touchPhase == TouchPhase.Moved)
      {
        if (canRotate)
        {
          _hasRotated = _cameraBehaviour.RotateCamera(startDragPosition, screenPosition) || _hasRotated;

          if (_hasRotated && !_hasRotatedOnce)
          {
            _hasRotatedOnce = true;
            // trigger evento
            GameEvents.LevelEvents.Rotated.SafeInvoke();
          }
          startDragPosition = screenPosition;
        }
      }
      else if (touchPhase == TouchPhase.Ended)
      {
        if (!_hasRotated)
        {
          if (canClick)
          {
            Click(screenPosition);

            // trigger evento
            GameEvents.LevelEvents.Clicked.SafeInvoke();
          }
        }
        else
        {
          _cameraBehaviour.StopRotating();
          _hasRotated = false;
          _hasRotatedOnce = false;
        }
      }
    }
  }

  public bool Pinch(float zoomAxis, Vector3 screenPosition)
  {
    if (isActive && canPinch)
    {
      _hasZoomedOnce = _hasZoomedOnce || _cameraBehaviour.Zoom(zoomAxis, screenPosition);
      return _cameraBehaviour.Zoom(zoomAxis, screenPosition);
    }
    return false;
  }

  public void StopPinch()
  {
    if (_hasZoomedOnce)
    {
      _hasZoomedOnce = false;
      _cameraBehaviour.StopZoom();

      // trigger evento
      GameEvents.LevelEvents.Zoomed.SafeInvoke();
    }
  }

  public bool MultiFingerDrag(Vector3 screenPosition)
  {
    if (isActive && canDrag)
    {
      if (!startedMultiFingerDrag)
      {
        startMultiFingersDragPosition = screenPosition;
        startedMultiFingerDrag = true;
      }
      bool panned = _cameraBehaviour.Pan(startMultiFingersDragPosition, screenPosition);
      _hasPannedOnce = panned || _hasPannedOnce;
      if (panned) startMultiFingersDragPosition = screenPosition;

      return panned;
    }
    return false;
  }

  public void StopMultiFingerDrag()
  {
    if (_hasPannedOnce)
    {
      _hasPannedOnce = false;
      _cameraBehaviour.StopPan();

      // trigger evento
      GameEvents.LevelEvents.Panned.SafeInvoke();
    }

    startedMultiFingerDrag = false;
  }

  public bool IsOnInventary(Vector3 position)
  {
    if (!_raycaster || !_eventSystem)
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

  public void ChangePermissions(bool pinch, bool click, bool rotate, bool drag)
  {
    canPinch = pinch;
    canClick = click;
    canRotate = rotate;
    canDrag = drag;
  }

  public static bool IsPointOnBoard(Vector3 screenPosition, out RaycastHit hit)
  {
    Ray ray = Camera.main.ScreenPointToRay(screenPosition);

    RaycastHit[] hits = new RaycastHit[2];

    Physics.RaycastNonAlloc(ray, hits, 100f, _raycastMaskFloor);
    hit = hits[0];

    if (hits[0].collider != null && hits[0].collider.GetComponent<GridBehaviour>())
    {
      return true;
    }

    return false;
  }

  internal void SetActive(bool active)
  {
    isActive = active;
  }
}