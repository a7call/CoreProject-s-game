using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class WandererPathFinding : MonoBehaviour
{
    NodeGrid grid;
    private void Awake()
    {
        grid = GetComponent<NodeGrid>();
    }

    public void FindPath(PathRequest request, Action<PathResult> callback)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Vector3[] wayPoints = new Vector3[0];
        bool pathSuccess = false;

        var startNode = grid.NodeFromWorldPoint(request._pathStart);
        var targetNode = grid.NodeFromWorldPoint(request._pathEnd);

        if(startNode._walkable && targetNode._walkable)
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
                    sw.Stop();
                   // print("Path found: " + sw.ElapsedMilliseconds + " ms");
                    pathSuccess = true;

                    break;
                }

                foreach (var neighboor in grid.GetNeighboors(currentNode))
                {
                    if (!neighboor._walkable || closeSet.Contains(neighboor))
                        continue;
                    int newMouvementCostToNeighboor;
                    if (Vector3.Distance(neighboor._worldPosition, request._pathStart) <= 1f)
                    {
                      newMouvementCostToNeighboor = currentNode._gCost + GetDistance(currentNode, neighboor) + neighboor._movementPenalty;
                    }
                    else
                    {
                        newMouvementCostToNeighboor = currentNode._gCost + GetDistance(currentNode, neighboor);
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
            var path = new List<Node>();
            if (pathSuccess)
            {
                wayPoints = RetracePath(startNode, targetNode,out path);
                pathSuccess = wayPoints.Length > 0;
            }
            callback(new PathResult(wayPoints, pathSuccess, path, request._callback));
        }  
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
