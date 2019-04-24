using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace prototypeRobot
{

    public class CharacterBehaviour: MonoBehaviour
    {

        [SerializeField] prototypeRobot.GridBehaviour grid;
        [SerializeField] GameObject cubeMark;
        [SerializeField] float speed = 100f;
        [SerializeField] public int SelectedItemPosition = 0;
        [SerializeField] public InventaryCenterBehaviour inventaryCenter;
        [SerializeField] public CameraBehaviour cameraBehaviour;

        private LayerMask raycastMask;
        private Vector3[] path;
        private NavMeshAgent navMeshAgent;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            raycastMask = LayerMask.GetMask(new string[] { "Floor", "Interactable" });
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
            if (!cameraBehaviour.InitAnimationIsEnded)
                return;

            cubeMark.SetActive(true);
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(position);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray, Mathf.Infinity, raycastMask);

            if (hits.Length > 0)
            {
                //System.Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));
                hits = System.Array.FindAll(hits, x => x.collider.GetComponent<GridBehaviour>() != null);
                System.Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));
                RaycastHit hit;
                if (hits.Length > 0)
                {
                    hit = hits[0];
                    GridBehaviour grid = hits[0].collider.GetComponent<GridBehaviour>();
                    /*if (!grid)
                     {
                         grid = hits[0].collider.GetComponentInParent<GridBehaviour>();
                     }*/
                    Debug.Log(grid);
                    //Debug.Log(hits[0].collider.gameObject);
                    //Debug.Log(hits[1].collider.gameObject);
                    //Debug.Log(hits[2].collider.gameObject);
                    if (grid)
                    {
                        Node boardNode = grid.NodeFromWorldPosition(hit.point);
                        if (grid.IsVertical)
                        {
                            cubeMark.transform.position = new Vector3(boardNode.worldPosition.x, grid.transform.position.y + 0.5f, boardNode.worldPosition.z);
                        }
                        else
                        {
                            //cubeMark.transform.position = new Vector3(boardNode.worldPosition.x, boardNode.worldPosition.y - 0.3f , boardNode.worldPosition.z );
                            cubeMark.transform.position = new Vector3(boardNode.worldPosition.x, boardNode.worldPosition.y - 0.3f, boardNode.worldPosition.z);
                        }
                        cubeMark.transform.eulerAngles = grid.MarkCubeRotation;
                    }
                }
                
            }
        }


        public void Move(Vector3 position)
        {
            if (!cameraBehaviour.InitAnimationIsEnded)
                return;

            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(position);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray, Mathf.Infinity, raycastMask);

            if (hits.Length > 0)
            {
                
                System.Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));
                RaycastHit hit;
                hit = hits[0];
                GridBehaviour grid = null;

                InteractableItemBehaviour item = hits[0].collider.GetComponent<InteractableItemBehaviour>();

                if (item)
                {
                    item.SetActive(true);
                    grid = item.Grid;
                } else
                {
                    ClientBehaviour client = hits[0].collider.GetComponent<ClientBehaviour>();
                    if (client)
                    {
                        client.Active();
                        grid = client.Grid;
                    }
                    else
                    {
                        hits = System.Array.FindAll(hits, x => x.collider.GetComponent<GridBehaviour>() != null || x.collider.GetComponentInParent<GridBehaviour>() != null);
                        if (hits.Length > 0)
                        {
                            grid = hits[0].collider.GetComponent<GridBehaviour>();
                            if (inventaryCenter)
                            {
                                inventaryCenter.CloseOrOpen(true);
                            }
                            if (!grid)
                            {
                                grid = hits[0].collider.GetComponentInParent<GridBehaviour>();
                            }
                        }
                        
                    }
                }

                if (grid)
                {
                    Node boardNode = grid.NodeFromWorldPosition(hits[0].point);
                    navMeshAgent.destination = boardNode.worldPosition;
                    navMeshAgent.isStopped = false;
                }
            }
            else
            {
                Debug.Log("Nothing");
                inventaryCenter.CloseOrOpen(true);
            }


        }

        public bool checkInventaryObjectOnSelectedPosition(string name)
        {
            bool hasItem = inventaryCenter.CheckItem(name);
            if (hasItem)
            {
                inventaryCenter.ClearSelection();
            }
            return hasItem;
        }
    }

    
}
