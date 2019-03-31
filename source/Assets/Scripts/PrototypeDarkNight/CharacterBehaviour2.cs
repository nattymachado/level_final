using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterBehaviour2 : MonoBehaviour
{

    [SerializeField] GridBehaviour grid;
    [SerializeField] List<string> inventary;
    [SerializeField] GameObject cubeMark;
    [SerializeField] float speed = 100f;

    private LayerMask raycastMask;
    private Vector3[] path;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        raycastMask = LayerMask.GetMask(new string[] { "Floor","Interactable" });
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccess)
    {
        if (pathSuccess)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    public void PositionOnBoard(Vector3 position)
    {
        Debug.Log("Position:" + position);
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out hitInfo, 100, raycastMask))
        {
            
            Node boardNode = grid.NodeFromWorldPosition(hitInfo.point);
            //Debug.Log(boardNode);
            cubeMark.transform.position = new Vector3(boardNode.worldPosition.x, cubeMark.transform.position.y, boardNode.worldPosition.z);
        }
    }


    public void Move(Vector3 position)
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out hitInfo, 100, raycastMask))
        {
            Node boardNode = grid.NodeFromWorldPosition(hitInfo.point);
            navMeshAgent.destination = boardNode.worldPosition;
            navMeshAgent.isStopped = false;

        }
    }
}
