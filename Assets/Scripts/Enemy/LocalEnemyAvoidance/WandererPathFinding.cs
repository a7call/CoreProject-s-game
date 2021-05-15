using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererPathFinding : MonoBehaviour
{
    NodeGrid grid;
    public Transform seeker, target;

    private void Awake()
    {
        grid = GetComponent<NodeGrid>();
    }
    private void Update()
    {
        FindPath(seeker.position, target.position);
    }
    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        var startNode = grid.NodeFromWorldPoint(startPos);
        var targetNode = grid.NodeFromWorldPoint(targetPos);
        List<Node> openSet = new List<Node>();
        HashSet<Node> closeSet = new HashSet<Node>();
        openSet.Add(startNode);
        while(openSet.Count > 0)
        {
            var currentNode = openSet[0];
            for(int i =1; i<openSet.Count; i++)
            {
                if(openSet[i]._fCost < currentNode._fCost || openSet[i]._fCost == currentNode._fCost && openSet[i]._hCost < currentNode._hCost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closeSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach(var neighboor in grid.GetNeighboors(currentNode))
            {
                if (!neighboor._walkable || closeSet.Contains(neighboor))
                    continue;
                int newMouvementCostToNeighboor = currentNode._gCost + GetDistance(currentNode, neighboor);
                if (newMouvementCostToNeighboor < neighboor._gCost || !openSet.Contains(neighboor))
                {
                    neighboor._gCost = newMouvementCostToNeighboor;
                    neighboor._hCost = GetDistance(neighboor, targetNode);
                    neighboor._parent = currentNode;
                    if (!openSet.Contains(neighboor))
                        openSet.Add(neighboor);
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        var currentNode = endNode;
        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode._parent;
        }
        path.Reverse();

        grid.path = path;
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
