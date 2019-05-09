using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Node : IHeapItem<Node>
{
    public bool walkable;
    public bool jumpy;
    public Vector3 worldPosition;
    public int gCost;
    public int hCost;
    public int gridX;
    public int gridY;
    public Node parent;
    int heapIndex;

    public Node(bool _walkable, bool _jumpy, Vector3 _worldPosition, int _gridX, int _gridY)
    {
        walkable = _walkable;
        jumpy = _jumpy;
        worldPosition = _worldPosition;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }

    public override string ToString()
    {
        return "Node: " + worldPosition;
    }

    public override bool Equals(object obj)
    {
        return this.worldPosition == ((Node)obj).worldPosition;
    }

    public override int GetHashCode()
    {
        return this.ToString().GetHashCode();
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
