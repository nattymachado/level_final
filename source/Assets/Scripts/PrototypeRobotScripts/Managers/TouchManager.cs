using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace prototypeRobot
{
    public class TouchManager : MonoBehaviour
    {
        public float MinSwipeDistX;

        [SerializeField] private Camera mainCamera;
        [SerializeField] private CharacterBehaviour character;
        [SerializeField] private float speed = 10;
        [SerializeField] private GameObject zoomPointer;
        [SerializeField] private InventaryCenterBehaviour _inventaryCenter;
        [SerializeField] private GraphicRaycaster _raycaster;
        [SerializeField] private PointerEventData _pointerEventData;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private float _zoomMin;
        [SerializeField] private float _zoomMax;

        private Vector2 startPos;
        private CameraBehaviour cameraBehaviour;
        public float perspectiveZoomSpeed = 0.2f;
        private bool isMoving = false;

        private void Start()
        {
            cameraBehaviour = mainCamera.GetComponentInParent<CameraBehaviour>();
        }

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

        void Update()
        {
            if (IsOnInventary())
            {
                return;
            }
            /*if (Input.touchCount > 0)
            {
                _inventaryCenter.OnClick();
            }*/
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
                mainCamera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, _zoomMin, _zoomMax);
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
                            character.PositionOnBoard(touch.position);
                            character.Move(touch.position);
                        } else {
                            cameraBehaviour.FixRotation();
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