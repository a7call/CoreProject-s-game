using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool _walkable;
    public Vector3 _worldPosition;
    public int _gCost;
    public int _hCost;
    public int _gridX;
    public int _gridY;
    public Node _parent;
     
    public Node(bool walkable, Vector3 worldPos, int gridX, int gridY)
    {
        _walkable = walkable;
        _worldPosition = worldPos;
        _gridX = gridX;
        _gridY = gridY;
    }
    public int _fCost
    {
        get
        {
            return _gCost + _hCost;
        }
    }

}

public class NodeGrid: MonoBehaviour
{
    Node[,] grid;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public LayerMask unwalkabeMask;

    float nodeDiameter;
    int gridSizeX, gridSizeY;
    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        
        for(int x=0; x<gridSizeX ; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkabeMask);
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    public List<Node> GetNeighboors(Node node)
    {
        List<Node> neighboors = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                int checkX = node._gridX + x;
                int checkY = node._gridY + y;
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighboors.Add(grid[checkX, checkY]);
                } 
            }

        }
        return neighboors;
    }
    public List<Node> path;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, (Vector3)gridWorldSize);
        if(grid != null)
        {
            foreach(var node in grid)
            {
                Gizmos.color = (node._walkable) ? Color.white : Color.red;
                if (path != null)
                {
                    if (path.Contains(node))
                    {
                        Gizmos.color = Color.black;
                    }
                }
                    
                Gizmos.DrawCube(node._worldPosition, Vector3.one * (nodeDiameter - 0.1f)); 
            }
        }
    }
} 
