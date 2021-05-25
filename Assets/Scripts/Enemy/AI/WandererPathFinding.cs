using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class WandererPathFinding : MonoBehaviour
{
    PathRequestManager requestManager;
    NodeGrid grid;
    private void Awake()
    {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<NodeGrid>();
    }
    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    public IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] wayPoints = new Vector3[0];
        bool pathSuccess = false;

        var startNode = grid.NodeFromWorldPoint(startPos);
        var targetNode = grid.NodeFromWorldPoint(targetPos);

        if (!targetNode._walkable )
        {
            targetNode = ChangeTargetNode(targetNode);
        }
        if (!startNode._walkable)
        {
            startNode = ChangeTargetNode(startNode);
        }

        if (startNode._walkable && targetNode._walkable && !targetNode._isBusy)
        {
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closeSet = new HashSet<Node>();
            openSet.Add(startNode);
            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                
                closeSet.Add(currentNode);

                   
                if (currentNode == targetNode)
                {
                    pathSuccess = true;                    
                    break;
                }
                
                foreach (var neighboor in grid.GetNeighboors(currentNode))
                {
                    if (!neighboor._walkable || closeSet.Contains(neighboor))
                        continue;
                    // BUSY NODE ABANDONNED FEATURE
                    //if (Vector3.Distance(neighboor._worldPosition, request._pathStart) <= 1f && neighboor._isBusy && Vector3.Distance(neighboor._worldPosition, request._pathStart) >= 4 * grid.nodeRadius)
                    //    continue;

                    int newMouvementCostToNeighboor;

                    if (Vector3.Distance(neighboor._worldPosition, startPos) <= 1f)
                    {
                        newMouvementCostToNeighboor = currentNode._gCost + GetDistance(currentNode, neighboor) + neighboor._movementPenalty + neighboor._unitMovementPenalty; ;
                    }
                    else
                    {
                        newMouvementCostToNeighboor = currentNode._gCost + GetDistance(currentNode, neighboor) + neighboor._movementPenalty;
                    }
                    
                    if (newMouvementCostToNeighboor < neighboor._gCost || !openSet.Contains(neighboor))
                    {
                        neighboor._gCost = newMouvementCostToNeighboor;
                        neighboor._hCost = GetDistance(neighboor, targetNode);
                        neighboor._parent = currentNode;
                        if (!openSet.Contains(neighboor))
                            openSet.Add(neighboor);
                        else
                        {
                            openSet.UpdateItem(neighboor);
                        }
                    }
                }
            }
            yield return null;
            var path = new List<Node>();
            if (pathSuccess)
            {
                wayPoints = RetracePath(startNode, targetNode,out path);
                pathSuccess = wayPoints.Length > 0;
            }
            requestManager.FinishedProcessingPath(wayPoints, pathSuccess, path);
        }
    }

    Node ChangeTargetNode(Node targetNode)
    {
        var nodelist = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == y)
                    continue;

                var newNode = grid.grid[targetNode._gridX + x, targetNode._gridY + y];
                if (newNode._walkable)
                {
                   return  newNode;
                }
                else
                {
                    nodelist.Add(newNode);
                    
                }
            }
        }
        foreach (var node in nodelist)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == y)
                        continue;

                    var newNode = grid.grid[node._gridX + x, node._gridY + y];
                    if (newNode._walkable)
                    {
                        
                        return newNode;

                    }
                }
            }
        }
        return targetNode;
    }

    Vector3[] RetracePath(Node startNode, Node endNode, out List<Node> path)
    {
        path = new List<Node>();
        var currentNode = endNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode._parent;   
        }
        Vector3[] wayPoints = SimplifyPath(path);
        Array.Reverse(wayPoints);
        return wayPoints;
    }




    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> wayPoints = new List<Vector3>();
        Vector2 directionHold = Vector2.zero;
        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directioNew = new Vector2(path[i - 1]._gridX - path[i]._gridX, path[i - 1]._gridY - path[i]._gridY);
            if(directioNew != directionHold)
            {
                wayPoints.Add(path[i]._worldPosition);
                directionHold = directioNew;
            }
        }
        return wayPoints.ToArray();
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA._gridX - nodeB._gridX);
        int distY = Mathf.Abs(nodeA._gridY - nodeB._gridY);

        if (distX > distY)
            return 14 * distY + 10 * (distX - distY);

        return 14 * distX + 10 * (distY - distX);
    }
}
