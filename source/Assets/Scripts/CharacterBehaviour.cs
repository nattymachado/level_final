using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterBehaviour : MonoBehaviour
{
    //Reference Variables
    [Header("Required References")]
    public CameraBehaviour cameraBehaviour;
    public InputController inputController;
    public Animator animator;
    public InventoryCenterBehaviour inventaryCenter;
    public float rotationSpeed;
    public Transform targetToRotation;
    public GameObject specialCompleteItem;
    private Quaternion _lookRotation;

    //Control Variables
    private FSMController _FSMController;
    public NavMeshAgent _navMeshAgent;
    private bool isIdle = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = true;
        if (specialCompleteItem != null)
            specialCompleteItem.SetActive(false);
    }

    //Start
    private void Start()
    {
        _FSMController = new FSMController(this);
    }

    public bool IsStoped()
    {
        Debug.Log("Actual Position:" + transform.position);
        Debug.Log("NavMesh Destination:" + _navMeshAgent.destination);
        return transform.position == _navMeshAgent.destination;
    }

    public void ActivateSpecialItem()
    {
        specialCompleteItem.SetActive(true);
    }

    //OnDestroy
    private void OnDestroy()
    {
        _FSMController.OnDestroy();
    }

    public void Move(Vector3 position)
    {
        if (!_FSMController.LockedByInteraction)
        {
            isIdle = false;
            Debug.Log("Go to:" + position);
            _navMeshAgent.destination = position;
            _navMeshAgent.isStopped = false;
            _FSMController.SetNextState(GameEnums.FSMInteractionEnum.Moving);
        } else
        {
            Debug.Log("Is locked!");
        }
    }

    public void DisableNavegation()
    {
        _navMeshAgent.enabled = false;
    }


    public void EnableNavegation()
    {

        _navMeshAgent.enabled = true;
    }


    public void SetNavMeshStopped(bool status)
    {
        _navMeshAgent.isStopped = status;
    }

    public bool CheckInventaryObjectOnSelectedPosition(string name)
    {
        bool hasItem = inventaryCenter.CheckItem(name);
        if (hasItem)
        {
            inventaryCenter.UseSelectedItem();

            // trigger event
            GameEvents.LevelEvents.UsedItem.SafeInvoke();
        }
        return hasItem;
    }


    public bool CheckIfSpecialIsActivated()
    {
        return specialCompleteItem.activeSelf;
    }

    //Update
    private void Update()
    {
        if (IsStoped() == true && isIdle == false)
        {
            StartCoroutine(WaitToStop(0.01f));
            isIdle = true;
        }
        _FSMController.UpdateFSM();


    }

    //Update
    private void FixedUpdate()
    {

        if (targetToRotation != null)
        {
            RotateTo();
        }
    }

    public void SetRotation(Transform target)
    {
        if (targetToRotation == null)
        {
            targetToRotation = target;
        }

    }

    private void RotateTo()
    {
        //find the vector pointing from our position to the target
        Vector3 direction = (targetToRotation.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(direction);

        if (Math.Abs(transform.rotation.eulerAngles.y - _lookRotation.eulerAngles.y) < 0.1)
        {
            targetToRotation = null;
            return;
        }
        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.fixedDeltaTime * rotationSpeed);
    }

    IEnumerator WaitToStop(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _FSMController.SetNextState(GameEnums.FSMInteractionEnum.Idle);
    }
}
