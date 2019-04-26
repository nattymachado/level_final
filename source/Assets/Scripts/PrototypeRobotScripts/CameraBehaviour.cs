using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace prototypeRobot
{
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
        [SerializeField] private Vector3[] cornerPositions;
        [SerializeField] private float[] cornerAngles;
        [SerializeField] private bool canFixRotation=true;
        [SerializeField] private float ZAngle=0;
        [SerializeField] private float XAngle = 0;
        [SerializeField] float targetAngle = 410;
        [SerializeField] float speedZoomInitAnimation = 4;
        [SerializeField] float speedRotationInitAnimation = 40;
        [SerializeField] float zoomInitAnimation = 10;
        public bool InitAnimationIsEnded = false;
        private bool _initAnimationIsStarted = false;
        private float _rotateSum = 0;
        public bool isMoving = false;
        
        bool lastIsRight = false;
        

        private Vector3 targetPosition;
        private float deadAreaOriginalScale;
        private float initialFoV;
        private float initialCharHeightDiff;
        private Vector3 lastCharacterPosition;

        // Start is called before the first frame update
        void Start()
        {
            childCamera.transform.LookAt(transform.position);//makes the camera look to it
            lastCharacterPosition = character.transform.position;

            initialFoV = childCamera.fieldOfView;
            deadAreaOriginalScale = deadArea.transform.localScale.x;
            initialCharHeightDiff = character.transform.position.y - transform.position.y;
            transform.eulerAngles = new Vector3(XAngle, targetAngle,  ZAngle);

            changeAngle();
            
        }

        void Update()
        {
            childCamera.transform.LookAt(transform.position);

                if (character.transform.position != lastCharacterPosition)
                {
                    // follow player outside deadArea
                    FollowPlayer();

                }
            
            
        }


        private void ExecuteInitAnimation()
        {
            if (InitAnimationIsEnded == false)
            {
                    if (childCamera.fieldOfView > zoomInitAnimation)
                    {
                        childCamera.fieldOfView = childCamera.fieldOfView - speedZoomInitAnimation * Time.deltaTime;
                    }
                    else
                    {
                        InitAnimationIsEnded = true;

                    }
            }
            
            
        }

        private void FixedUpdate()
        {
            if (!isMoving)
            {
                changeAngle();
            }
            ExecuteInitAnimation();
        }

        private void changeAngle()
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, 50f * Time.deltaTime);
            transform.eulerAngles = new Vector3(XAngle, angle, ZAngle);
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
                isMoving = true;
                transform.RotateAround(transform.position, new Vector3(0.0f, -1.0f, 0.0f), angle);

            }
        }

        public void RotateCameraToLeft(float angle)
        {
            if (target)
            {
                isMoving = true;
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

        public void FixRotation()
        {
            if (canFixRotation)
            {
                float minorDistance = Mathf.Infinity;
                float newDistance = 0;
                int newPosition = 0;
                for (int i = 0; i < cornerPositions.Length; i++)
                {
                    newDistance = Vector3.Distance(childCamera.transform.position, cornerPositions[i]);
                    if (newDistance < minorDistance)
                    {
                        minorDistance = newDistance;
                        newPosition = i;
                    }
                }
                targetAngle = cornerAngles[newPosition];
                isMoving = false;
            }
            
        }
    }

}
