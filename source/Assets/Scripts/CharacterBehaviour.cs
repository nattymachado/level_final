using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterBehaviour : MonoBehaviour
{
    //Reference Variables
    [Header("Required References")]
    public CameraBehaviour cameraBehaviour;
    public Animator animator;
    public InventoryCenterBehaviour inventaryCenter;

    //Control Variables
    private FSMController _FSMController;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = true;
    }

    //Start
    private void Start()
    {
        _FSMController = new FSMController(this);
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

    //Update
    private void Update()
    {
        if(this.transform.position == _navMeshAgent.destination) _FSMController.SetNextState(GameEnums.FSMInteractionEnum.Idle);
        _FSMController.UpdateFSM();
    }
}
