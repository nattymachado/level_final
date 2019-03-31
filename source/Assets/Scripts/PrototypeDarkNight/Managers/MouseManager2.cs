using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager2 : MonoBehaviour
{

    public float MinSwipeDistX;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CharacterBehaviour2 character;
    [SerializeField] private float speed = 10;
    [SerializeField] private GameObject zoomPointer;
    //[SerializeField] private InventaryCircleBehaviour inventary;

    private Vector2 startPos;
    private CameraBehaviour2 cameraBehaviour;
    [SerializeField] private float perspectiveZoomSpeed = 15f;


    private float mousewheelAxis;
    private bool hasMoved = false;

    Vector3 mousePosition;
    float cooldownTimer = 0.1f;

    private void Start()
    {
        cameraBehaviour = mainCamera.GetComponentInParent<CameraBehaviour2>();
    }

#if UNITY_EDITOR

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0)
        {
            Debug.Log("Mouse:" + Input.mousePosition);
            character.PositionOnBoard(Input.mousePosition);
            mousePosition = Input.mousePosition;
            cooldownTimer = 0.1f;
        }

        /*if (Input.GetButtonDown("Fire1"))
        {
            character.Move(Input.mousePosition);
        }*/

        mousewheelAxis = Input.GetAxis("Mouse ScrollWheel");

        if (mousewheelAxis != 0)
        {
            // increment zoom based on mouse wheel
            mainCamera.fieldOfView += -mousewheelAxis * perspectiveZoomSpeed;

            // Clamp the field of view to make sure it's between 0 and 180.
            mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 5f, 24f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            hasMoved = false;
            startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            /*if (!inventary.gameObject.activeSelf)
            {
                hasMoved = RotateCamera(Input.mousePosition) || hasMoved;

            }
            else
            {
                hasMoved = RotateInventary(Input.mousePosition) || hasMoved;
            }*/
            hasMoved = RotateCamera(Input.mousePosition) || hasMoved;
            startPos = Input.mousePosition;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (!hasMoved)
            {
                
                character.Move(Input.mousePosition);
            }
            /*else if (inventary.gameObject.activeSelf)
            {
                inventary.FixRotation();
            }*/
            hasMoved = false;
        }
    }

#endif

    /*private bool RotateInventary(Vector3 position)
    {
        float swipeDistHorizontal = (new Vector3(position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
        if (swipeDistHorizontal > 1)
        {

            float swipeValue = Mathf.Sign(position.x - startPos.x);
            inventary.RotateItem(swipeValue < 0);
            return true;
        }
        return false;
    }*/

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