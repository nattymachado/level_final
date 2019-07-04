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
            _navMeshAgent.destination = position;
            _navMeshAgent.isStopped = false;
            _FSMController.SetNextState(GameEnums.FSMInteractionEnum.Moving);
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
            StartCoroutine(WaitToStop(0.05f));
            isIdle = true;
        }
        _FSMController.UpdateFSM();


    }

   public void SetRotation(Transform target)
    {
        this.transform.LookAt(target);
        this.transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    IEnumerator WaitToStop(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (IsStoped() == true)
        {
            _FSMController.SetNextState(GameEnums.FSMInteractionEnum.Idle);
        }
            
    }
}
