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
    [SerializeField] public List<InventaryObjectBehaviour2> Inventary;
    [SerializeField] public int SelectedItemPosition = 0;

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
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(position);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, 100, raycastMask);

        if (hits.Length > 0)
        {
            System.Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));
            RaycastHit hit;
            hit = hits[0];
            FloorBehaviour floor = hits[0].collider.GetComponent<FloorBehaviour>();
            if (floor && floor.Grid)
            {
                Node boardNode = floor.Grid.NodeFromWorldPosition(hit.point);
                //Debug.Log(boardNode);
                cubeMark.transform.position = new Vector3(boardNode.worldPosition.x, boardNode.worldPosition.y, boardNode.worldPosition.z);
            }
        }


        /*if (Physics.Raycast(ray, out hitInfo, 100, raycastMask))
        {
            
            Node boardNode = grid.NodeFromWorldPosition(hitInfo.point);
            //Debug.Log(boardNode);
            cubeMark.transform.position = new Vector3(boardNode.worldPosition.x, cubeMark.transform.position.y, boardNode.worldPosition.z);
        }*/
    }


    public void Move(Vector3 position)
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(position);

        /*if (Physics.Raycast(ray, out hitInfo, 100, raycastMask))
        {
            FloorBehaviour floor = hitInfo.collider.GetComponent<FloorBehaviour>();
            if (floor && floor.Grid)
            {
                Node boardNode = floor.Grid.NodeFromWorldPosition(hitInfo.point);
                navMeshAgent.destination = boardNode.worldPosition;
                navMeshAgent.isStopped = false;
            }
            

        }*/
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, 100, raycastMask);
        
        if (hits.Length > 0)
        {
            System.Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));
            RaycastHit hit;
            hit = hits[0];
            GridBehaviour grid = null;
            SwitchBehaviour switchBehaviour = hits[0].collider.GetComponent<SwitchBehaviour>();
            SunPilarBehaviour sunPilarBehaviour = hits[0].collider.GetComponent<SunPilarBehaviour>();
            SwitchSolarBehaviour switchSolarBehaviour = hits[0].collider.GetComponent<SwitchSolarBehaviour>();
            if (switchBehaviour)
            {
                switchBehaviour.isActiveToMove = true;
                grid = switchBehaviour.Grid;
            }
            else if(sunPilarBehaviour)
            {
                sunPilarBehaviour.isActiveToMove = true;
                grid = sunPilarBehaviour.Grid;
            } else if (switchSolarBehaviour)
            {
                switchSolarBehaviour.isActiveToMove = true;
                grid = switchSolarBehaviour.Grid;
            }
            else
            {
                FloorBehaviour floor = hits[0].collider.GetComponent<FloorBehaviour>();
                if (floor)
                {
                    grid = floor.Grid;
                }
            }
            
            if (grid)
            {
                Node boardNode = grid.NodeFromWorldPosition(hits[0].point);
                Debug.Log("Point:" + hits[0].point);
                Debug.Log("POsition Board:" + boardNode.worldPosition);
                navMeshAgent.destination = boardNode.worldPosition;
                navMeshAgent.isStopped = false;
            }
        }
        

    }
}
