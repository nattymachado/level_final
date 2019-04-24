using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace prototypeRobot
{
    public class MouseManager : MonoBehaviour
    {

        public float MinSwipeDistX;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private CharacterBehaviour character;
        [SerializeField] private float speed = 10;
        [SerializeField] private GameObject zoomPointer;
        [SerializeField] private InventaryCenterBehaviour _inventaryCenter;
        [SerializeField] private float _zoomMin;
        [SerializeField] private float _zoomMax;


        private Vector2 startPos;
        private CameraBehaviour cameraBehaviour;
        [SerializeField] private GraphicRaycaster _raycaster;
        [SerializeField] private PointerEventData _pointerEventData;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private float perspectiveZoomSpeed = 15f;


        private float mousewheelAxis;
        private bool hasMoved = false;

        Vector3 mousePosition;
        float cooldownTimer = 0.1f;


        private bool IsOnInventary()
        {
            //Set up the new Pointer Event
            _pointerEventData = new PointerEventData(_eventSystem);
            //Set the Pointer Event Position to that of the mouse position
            _pointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            _raycaster.Raycast(_pointerEventData, results);

            return results.Count > 0;
        }

        private void Start()
        {
            cameraBehaviour = mainCamera.GetComponentInParent<CameraBehaviour>();
        }

#if UNITY_EDITOR


        void Update()
        {
            if (IsOnInventary())
            {
                return;
            }
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                character.PositionOnBoard(Input.mousePosition);
                mousePosition = Input.mousePosition;
                cooldownTimer = 0.1f;
            }

           
            mousewheelAxis = Input.GetAxis("Mouse ScrollWheel");

            if (mousewheelAxis != 0)
            {
                // increment zoom based on mouse wheel
                //mainCamera.fieldOfView += -mousewheelAxis * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                // mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, _zoomMin, _zoomMax);
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
                //_inventaryCenter.OnClick();
                if (!hasMoved)
                {

                    character.Move(Input.mousePosition);
                }
                else {
                    cameraBehaviour.FixRotation();
                }

                hasMoved = false;
                
            }
        }

#endif

        private bool RotateCamera(Vector3 position)
        {
            float swipeDistHorizontal = (new Vector3(position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
            if (swipeDistHorizontal > MinSwipeDistX)
            {

                float swipeValue = Mathf.Sign(position.x - startPos.x);

                if (swipeValue > 0)
                {
                    cameraBehaviour.RotateCameraToLeft(Mathf.Abs(swipeValue) * speed);
                }
                else if (swipeValue < 0)
                {
                    cameraBehaviour.RotateCameraToRight(Mathf.Abs(swipeValue) * speed);
                }
                return true;
            }
            return false;
        }
    }
}