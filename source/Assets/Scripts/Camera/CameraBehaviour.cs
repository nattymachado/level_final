using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Camera childCamera;
    [SerializeField] private CharacterBehaviour character;
    [Header("Translation")]
    [SerializeField] private Collider deadArea;
    [SerializeField] private float translationLerpFactor = 2f;
    private bool isFollowingPlayer = false;
    private Vector3 targetPosition;
    private Vector3 playerTargetPosition;
    [Header("Rotation")]
    [SerializeField] private float minDistToRotate = 0.01f;
    // [SerializeField] private float rotateAngle = 90;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private float rotationLerpFactor = 10;
    private bool isRotating;
    private Quaternion targetRotation;
    [Header("Zoom")]
    [SerializeField] private float minFoV = 5f;
    [SerializeField] private float maxFoV = 24f;
    [SerializeField] private float camDislocationLerpFactor = 10;
    [SerializeField] private float camDislocationSpeed = 15f;
    [SerializeField] private float minDistToZoom = 0.01f;
    private Vector3 targetDislocatedPosition;
    private bool isCameraDislocated;
    private float targetFoV;
    private bool isZooming;
    [Header("Pan")]
    [SerializeField] private float minDistToPan = 0.01f;
    [SerializeField] private float panFactor = 10f;
    [SerializeField] private bool panInverseX = false;
    [SerializeField] private bool panInverseY = false;
    private bool isPanning;

    private float initialFoV;
    // [SerializeField] private Vector3[] cornerPositions;
    // [SerializeField] private float[] cornerAngles;
    // [SerializeField] private bool canFixRotation = true;
    // [SerializeField] private float ZAngle = 0;
    // [SerializeField] private float XAngle = 0;
    // [SerializeField] float targetAngle = 410;
    // [SerializeField] float speedZoomInitAnimation = 4;
    // [SerializeField] float speedRotationInitAnimation = 40;
    // [SerializeField] float zoomInitAnimation = 10;
    // public bool InitAnimationIsEnded {get; private set;}
    // private bool _initAnimationIsStarted = false;
    // private float _rotateSum = 0;
    // private bool isMoving = false;
    // private bool lastIsRight = false;
    private float deadAreaOriginalScale;
    private float initialCharHeightDiff;
    private Vector3 lastCharacterPosition;

    // Start is called before the first frame update
    void Start()
    {
        // InitAnimationIsEnded = false;

        // inicia com o angulo alvo zerado
        targetRotation = transform.rotation;

        lastCharacterPosition = character.transform.position;

        initialFoV = childCamera.fieldOfView;
        targetFoV = initialFoV;
        deadAreaOriginalScale = deadArea.transform.localScale.x;
        initialCharHeightDiff = character.transform.position.y - transform.position.y;
        // transform.eulerAngles = new Vector3(XAngle, targetAngle, ZAngle);

        // changeAngle();

    }


    // private void ExecuteInitAnimation()
    // {
    //     if (InitAnimationIsEnded == false)
    //     {
    //         if (childCamera.fieldOfView > zoomInitAnimation)
    //         {
    //             childCamera.fieldOfView = childCamera.fieldOfView - speedZoomInitAnimation * Time.deltaTime;
    //         }
    //         else
    //         {
    //             InitAnimationIsEnded = true;

    //         }
    //     }


    // }

    private void LateUpdate()
    {
        UpdateTranslation();
        UpdateRotation();
        UpdateZoom();
    }

    private void UpdateZoom()
    {
        if (Mathf.Abs(targetFoV - childCamera.fieldOfView) > 0.005f)
        {
            float newFov = Mathf.Lerp(childCamera.fieldOfView, targetFoV, camDislocationLerpFactor * Time.deltaTime);

            // Clamp the field of view to make sure it's between 0 and 180.
            childCamera.fieldOfView = newFov;

            // update deadArea scale
            deadArea.transform.localScale = Vector3.one * newFov / initialFoV * deadAreaOriginalScale;
        }
    }

    private void UpdateRotation()
    {
        if (Mathf.Abs(targetRotation.eulerAngles.y - transform.rotation.eulerAngles.y) > 0.005f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationLerpFactor * Time.deltaTime);
        }
    }

    private void UpdateTranslation()
    {
        // update target position if locked at player    
        playerTargetPosition = new Vector3(character.transform.position.x, character.transform.position.y - initialCharHeightDiff, character.transform.position.z);

        if (!deadArea.bounds.Contains(character.transform.position))
        {
            isFollowingPlayer = true;
        }

        // calculate target Position
        if (isCameraDislocated)
        {
            targetPosition = targetDislocatedPosition;
        }
        else
        {
            if (isFollowingPlayer)
            {
                targetPosition = playerTargetPosition;
            }
            else
            {
                targetPosition = new Vector3(transform.position.x, character.transform.position.y - initialCharHeightDiff, transform.position.z);
            }
        }

        if ((targetPosition - playerTargetPosition).magnitude > 0.01)
        {
            isFollowingPlayer = false;
        }


        // translate camera
        if ((targetPosition - transform.position).magnitude > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, translationLerpFactor * Time.deltaTime);
        }

    }

    public bool RotateCamera(Vector3 startPos, Vector3 position)
    {
        Vector3 positionViewport = childCamera.ScreenToViewportPoint(position);
        Vector3 startPosViewport = childCamera.ScreenToViewportPoint(startPos);

        float swipeDistHorizontal = positionViewport.x - startPosViewport.x;
        float absSwipeDist = Mathf.Abs(swipeDistHorizontal);

        if (isRotating || absSwipeDist > minDistToRotate)
        {
            isRotating = true;
            targetRotation = transform.rotation * Quaternion.Euler(0, swipeDistHorizontal * rotationSpeed, 0);
            return true;
        }
        return false;
    }

    public void StopRotating()
    {
        isRotating = false;
    }

    public void ResetPanZoomDislocation()
    {
        isCameraDislocated = false;
        isZooming = false;
        isPanning = false;
    }

    public bool Zoom(float increment, Vector3 screenPosition)
    {

        if (isZooming || Mathf.Abs(increment) >= minDistToZoom)
        {
            // calculate new fov
            targetFoV = childCamera.fieldOfView + increment * camDislocationSpeed;
            targetFoV = Mathf.Clamp(targetFoV, minFoV, maxFoV);

            isCameraDislocated = true;
            if (!isZooming)
            {
                RaycastHit hit;
                if (InputController.IsPointOnBoard(screenPosition, out hit))
                {
                    targetDislocatedPosition = hit.point;
                }
                else
                {
                    targetDislocatedPosition = transform.position;
                }
            }

            isZooming = true;

            return true;
        }

        return false;
    }

    public void StopZoom()
    {
        isZooming = false;
    }

    public bool Pan(Vector3 startPos, Vector3 position)
    {
        Vector3 positionViewport = childCamera.ScreenToViewportPoint(position);
        Vector3 startPosViewport = childCamera.ScreenToViewportPoint(startPos);

        float swipeDistHorizontal = positionViewport.x - startPosViewport.x;
        float swipeDistVertical = positionViewport.y - startPosViewport.y;
        float absSw = Mathf.Abs(swipeDistHorizontal);

        if (isPanning || Mathf.Abs(swipeDistHorizontal) > minDistToPan || Mathf.Abs(swipeDistVertical) > minDistToPan)
        {
            isCameraDislocated = true;
            if (!isPanning)
            {
                if (!isZooming)
                {
                    targetDislocatedPosition = transform.position;
                }
            }
            Vector3 verticalDisloc = Vector3.down * swipeDistVertical * panFactor;
            Vector3 horizontalDisloc = Vector3.Cross(childCamera.transform.forward, Vector3.up).normalized * swipeDistHorizontal * panFactor;
            targetDislocatedPosition += (panInverseY ? -1 : 1) * verticalDisloc + (panInverseX ? -1 : 1) * horizontalDisloc;

            isPanning = true;

            return true;
        }
        return false;

    }

    public void StopPan()
    {
        isPanning = false;
    }

}