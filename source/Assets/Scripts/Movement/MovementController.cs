using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{

    public float MinSwipeDistX;
    public Vector3 startPosition;

    private LayerMask _raycastMaskFloor;
    private LayerMask _raycastMaskItem;

    [SerializeField] private CharacterBehaviour _character;
    [SerializeField] private CameraBehaviour _cameraBehaviour;
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _zoomMin;
    [SerializeField] private float _zoomMax; 
    [SerializeField] private GraphicRaycaster _raycaster;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private float perspectiveZoomSpeed = 15f;
    [SerializeField] private Pointer pointer;
    private bool _hasMoved;


    private void Start()
    {
        _raycastMaskFloor = LayerMask.GetMask(new string[] { "Floor"});
        _raycastMaskItem = LayerMask.GetMask(new string[] { "Interactable" });
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

    public void MoveOrRotate(Vector3 position, bool isStarting, bool isMoving, bool isFinishing)
    {
        if (!_cameraBehaviour.InitAnimationIsEnded)
            return;

        if (isStarting)
        {
            InitStartPosition(position);
        }
        else if (isMoving)
        {
            RotateCameraAndUpdateStartPosition(position);

        }
        else if (isFinishing)
        {
            if (!_hasMoved)
            {
                ActiveItem(position);
                Move(position);
                
            }
            _hasMoved = false;
        }
    }

    private void Move(Vector3 position)
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(position);

        RaycastHit[] hits = new RaycastHit[1];
        Physics.RaycastNonAlloc(ray, hits, 500f, _raycastMaskFloor);

        if (hits[0].collider != null)
        {
            pointer.gameObject.SetActive(true);
            pointer.transform.position = hits[0].point;
            _character.Move(hits[0].point);
        }
    }

    private bool ActiveItem(Vector3 position)
    {
        RaycastHit hitInfo;
        bool activateItem = false;
        Ray ray = Camera.main.ScreenPointToRay(position);

        RaycastHit[] hits = new RaycastHit[1];
        Physics.RaycastNonAlloc(ray, hits, 100f, _raycastMaskItem);

        if (hits[0].collider != null)
        {
            InteractableItemBehaviour item = hits[0].collider.GetComponent<InteractableItemBehaviour>();
            if (item)
            {
                item.SetActive(true);
                activateItem = true;
            }
            
        }
        return activateItem;
    }

    public void Zoom(float zoomAxis)
    {

        if (zoomAxis != 0 && _cameraBehaviour.InitAnimationIsEnded)
        {
            _cameraBehaviour.ChangeFoV(zoomAxis * perspectiveZoomSpeed);
        }
        
    }

    private bool RotateCamera(Vector3 startPos, Vector3 position)
    {
        float swipeDistHorizontal = (new Vector3(position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
        if (swipeDistHorizontal > MinSwipeDistX)
        {

            float swipeValue = Mathf.Sign(position.x - startPos.x);

            if (swipeValue > 0)
            {
                _cameraBehaviour.RotateCameraToLeft(Mathf.Abs(swipeValue) * _speed);
            }
            else if (swipeValue < 0)
            {
                _cameraBehaviour.RotateCameraToRight(Mathf.Abs(swipeValue) * _speed);
            }
            return true;
        }
        return false;
    }

    private void InitStartPosition(Vector3 position)
    {
        startPosition = position;
        _hasMoved = false;
    }

    private void RotateCameraAndUpdateStartPosition(Vector3 position)
    {
        _hasMoved = RotateCamera(startPosition, position) || _hasMoved;
        startPosition = position;
    }
}
