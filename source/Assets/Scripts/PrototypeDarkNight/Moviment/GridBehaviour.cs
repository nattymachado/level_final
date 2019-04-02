using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridBehaviour : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public List<Node> path;
    public string GridName;

    private float nodeDiameter;
    private int gridSizeX, gridSizeZ;
    private Node[,] grid;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeZ = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    public void CreateGrid()
    {

        grid = new Node[gridSizeX, gridSizeZ];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (z * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, z] = new Node(walkable, false, worldPoint, x, z);
            }
        }
    }

    public Node NodeFromWorldPosition(Vector3 worldPosition)
    {
        Debug.Log("Poinst:"+ worldPosition);
        float percentX = ((worldPosition.x - transform.position.x) + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentZ = ((worldPosition.z-transform.position.z)+ gridWorldSize.y / 2) / gridWorldSize.y;
        Debug.Log("Grid Size" + gridWorldSize.x  + "Grip Size x:" + gridWorldSize.y);
        percentX = Mathf.Clamp01(percentX);
        percentZ = Mathf.Clamp01(percentZ);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int z = Mathf.RoundToInt((gridSizeZ - 1) * percentZ);
        Debug.Log("Grid Name:" + GridName +"Grip Position:" + x + "-" + z);
        return grid[x, z];


    }

    public int GetMaxSize()
    {
        return gridSizeX * gridSizeZ;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (grid != null)
        {
            //Node playerNode = NodeFromWorldPosition(player.position);
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.green : Color.red;
               /*f (playerNode.Equals(n))
                {
                    Gizmos.color = Color.green;
                }*/

                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }


}