using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class CharacterBehaviour : MonoBehaviour
{

    [SerializeField] public CameraBehaviour cameraBehaviour;
    [SerializeField] public Animator animator;
    [SerializeField] public InventoryCenterBehaviour inventaryCenter;

    public bool canMove = true;

    
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = true;
        
    }

    public void Move(Vector3 position)
    {
        if (!canMove)
            return;
        _navMeshAgent.destination = position;
        _navMeshAgent.isStopped = false;


    }

    public void Stop()
    {
        _navMeshAgent.isStopped = true;

    }

    public bool CheckInventaryObjectOnSelectedPosition(string name)
    {
        bool hasItem = inventaryCenter.CheckItem(name);
        if (hasItem)
        {
            inventaryCenter.UseSelectedItem();
        }
        return hasItem;
    }

    
}
