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
    public float pointerPosition;
    public bool IsVertical = true;
    public Vector3 GridRotation;

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
                Vector3 oldWorldPoint;
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (z * nodeDiameter + nodeRadius);
                if (!IsVertical)
                {
                    oldWorldPoint = worldPoint;
                    worldPoint = Quaternion.Euler(GridRotation) * worldPoint;
                    worldPoint.z = oldWorldPoint.z;
                    worldPoint.y = worldPoint.y + transform.position.y / 2 + 1.2f;

                }

                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, z] = new Node(walkable, false, worldPoint, x, z);
            }
        }
    }

    public Node NodeFromWorldPosition(Vector3 worldPosition)
    {
        float percentX = ((worldPosition.x - transform.position.x) + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentZ;

        percentZ = ((worldPosition.z - transform.position.z) + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentZ = Mathf.Clamp01(percentZ);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int z = Mathf.RoundToInt((gridSizeZ - 1) * percentZ);
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
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.green : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}