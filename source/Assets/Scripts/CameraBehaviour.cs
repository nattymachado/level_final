using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraBehaviour : MonoBehaviour
{
  [SerializeField] private Transform target;
  [SerializeField] private float rotateAngle = 90;
  [SerializeField] private Camera childCamera;
  [SerializeField] private CharacterBehaviour character;
  [SerializeField] private Collider deadArea;
  [SerializeField] private float lerpSpeed = 2f;
  [SerializeField] private float minFoV = 5f;
  [SerializeField] private float maxFoV = 24f;

  private Vector3 targetPosition;
  private float deadAreaOriginalScale;
  private float initialFoV;
  private float initialCharHeightDiff;

  // Start is called before the first frame update
  void Start()
  {
    childCamera.transform.LookAt(transform.position);//makes the camera look to it

    initialFoV = childCamera.fieldOfView;
    deadAreaOriginalScale = deadArea.transform.localScale.x;
    initialCharHeightDiff = character.transform.position.y - transform.position.y;
  }
  void Update()
  {
    childCamera.transform.LookAt(transform.position);

    // follow player outside deadArea
    FollowPlayer();
  }
  private void FollowPlayer()
  {
    if (!deadArea.bounds.Contains(character.transform.position))
    {
      targetPosition = new Vector3(character.transform.position.x, character.transform.position.y - initialCharHeightDiff, character.transform.position.z);
    }
    else
    {
      targetPosition = new Vector3(transform.position.x, character.transform.position.y - initialCharHeightDiff, transform.position.z);
    }
    transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
  }


  public void RotateCameraToRight(float angle)
  {
    if (target)
    {
      transform.RotateAround(transform.position, new Vector3(0.0f, -1.0f, 0.0f), angle);
    }
  }

  public void RotateCameraToLeft(float angle)
  {
    if (target)
    {
      transform.RotateAround(transform.position, new Vector3(0.0f, 1.0f, 0.0f), angle);
    }
  }

  public void ChangeFoV(float incrementFov)
  {
    // calculate new fov
    float newFov = childCamera.fieldOfView + incrementFov;
    newFov = Mathf.Clamp(newFov, minFoV, maxFoV);

    // Clamp the field of view to make sure it's between 0 and 180.
    childCamera.fieldOfView = newFov;

    // update deadArea scale
    deadArea.transform.localScale = Vector3.one * newFov / initialFoV * deadAreaOriginalScale;
  }



}
