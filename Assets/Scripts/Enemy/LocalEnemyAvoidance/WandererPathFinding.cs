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
        grid = GetComponent<NodeGrid>();
        requestManager = GetComponent<PathRequestManager>();

    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Vector3[] wayPoints = new Vector3[0];
        bool pathSuccess = false;

        var startNode = grid.NodeFromWorldPoint(startPos);
        var targetNode = grid.NodeFromWorldPoint(targetPos);

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
                    print("Path found: " + sw.ElapsedMilliseconds + " ms");
                    pathSuccess = true;

                    break;
                }

                foreach (var neighboor in grid.GetNeighboors(currentNode))
                {
                    if (!neighboor._walkable || closeSet.Contains(neighboor))
                        continue;
                    int newMouvementCostToNeighboor = currentNode._gCost + GetDistance(currentNode, neighboor) + neighboor._movementPenalty;
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
            if (pathSuccess)
            {
                wayPoints = RetracePath(startNode, targetNode);
            }
            requestManager.FinishedProcessingPath(wayPoints, pathSuccess);
        }  
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
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
