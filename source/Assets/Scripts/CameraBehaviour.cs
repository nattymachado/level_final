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
  [SerializeField] private float zoomLerpFactor = 10;
  [SerializeField] private float zoomSpeed = 15f;
  private float targetFoV;

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
    if (character.transform.position != lastCharacterPosition)
    {
      // follow player outside deadArea
      UpdateTranslation();
    }

    UpdateRotation();
    UpdateZoom();

  }

  private void UpdateZoom()
  {
    if (Mathf.Abs(targetFoV - childCamera.fieldOfView) > 0.005f)
    {
      float newFov = Mathf.Lerp(childCamera.fieldOfView, targetFoV, zoomLerpFactor * Time.deltaTime);

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
      transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation,rotationLerpFactor * Time.deltaTime);
    }
  }

  private void UpdateTranslation()
  {
    Bounds currentBounds = deadArea.bounds;

    if (!currentBounds.Contains(character.transform.position))
    {
      isFollowingPlayer = true;
    }

    if (isFollowingPlayer)
    {
      targetPosition = new Vector3(character.transform.position.x, character.transform.position.y - initialCharHeightDiff, character.transform.position.z);
    }
    else
    {
      targetPosition = new Vector3(transform.position.x, character.transform.position.y - initialCharHeightDiff, transform.position.z);
    }

    if ((targetPosition - transform.position).magnitude > 0.01f)
    {
      transform.position = Vector3.Lerp(transform.position, targetPosition, translationLerpFactor * Time.deltaTime);
    }
    else
    {
      isFollowingPlayer = false;
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
      targetRotation =  transform.rotation * Quaternion.Euler(0,swipeDistHorizontal * rotationSpeed,0);
      return true;
    }
    return false;
  }

  public void StopRotating(){
     isRotating = false;
  }

  public void ChangeFoV(float increment)
  {
    // calculate new fov
    targetFoV = childCamera.fieldOfView + increment * zoomSpeed;
    targetFoV = Mathf.Clamp(targetFoV, minFoV, maxFoV);
  }

}
