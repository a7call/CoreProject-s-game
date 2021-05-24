using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TerrainType
{
    public LayerMask terrainMask;
    public int terrainPenalty;
}
public class Node : IHeapItem<Node>
{
    public bool _walkable;
    public bool _isBusy;
    public Vector3 _worldPosition;
    public int _gCost;
    public int _hCost;
    public int _gridX;
    public int _gridY;
    public Node _parent;
    int _heapIndex;
    public int _movementPenalty;
    public int _unitMovementPenalty;
    
     
    public Node(bool walkable, Vector3 worldPos, int gridX, int gridY, int penalty)
    {
        _walkable = walkable;
        _worldPosition = worldPos;
        _gridX = gridX;
        _gridY = gridY;
        _movementPenalty = penalty;
    }
    public int _fCost
    {
        get
        {
            return _gCost + _hCost;
        }
    }

    public int HeapIndex {
        get
        {
            return _heapIndex;
        }
        set
        {
            _heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = _fCost.CompareTo(nodeToCompare._fCost);
        if(compare == 0)
        {
            compare = _hCost.CompareTo(nodeToCompare._hCost);
        }
        return -compare;
    }
}

public class NodeGrid: MonoBehaviour
{
    public Node[,] grid;
    LayerMask walkableMask;
    Dictionary<int, int> walkableRegionsDictionnary = new Dictionary<int, int>();
    public int obstacleProximityPenalty = 10;
    

    
    float nodeDiameter;
    int gridSizeX, gridSizeY;
    [Header("Grid")]
    public Vector2 gridWorldSize;

    [Header("Nodes")]
    public float nodeRadius;
    public LayerMask unwalkabeMask;
    public LayerMask walkabeMask;
    public TerrainType[] walkableRegions;

    [Header("BlurredMap")]
    public bool isBlurredPenaltyActive = false;
    int penaltyMin = int.MaxValue;
    int penaltyMax = int.MinValue;

    [Header("Gizmos")]
    public bool displayGridGizmos;

    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        //foreach (TerrainType region in walkableRegions)
        //{
        //    walkableMask.value = walkableMask |= region.terrainMask.value;
        //    walkableRegionsDictionnary.Add((int)Mathf.Log(region.terrainMask.value,2),region.terrainPenalty);
        //}

       // CreateGrid();
    }

    public Vector3 worldBottomLeft;
    public void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        
       
        worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        
        for(int x=0; x<gridSizeX ; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkabeMask);
                if (Physics2D.OverlapCircle(worldPoint, nodeRadius, walkabeMask))
                    walkable = true;
                int mouvementPenalty = 0;


                var ray = new Ray(worldPoint + Vector3.forward * 50, -Vector3.forward);
                RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 100, walkableMask);

                if (hits.Length > 0)
                {
                    foreach (var hit in hits)
                    {
                        walkableRegionsDictionnary.TryGetValue(hit.collider.gameObject.layer, out mouvementPenalty);
                    }

                }
                if (!walkable)
                {
                    mouvementPenalty += obstacleProximityPenalty;
                }

                    grid[x, y] = new Node(walkable, worldPoint, x, y, mouvementPenalty);
            }
        }
        if(isBlurredPenaltyActive)
            BlurPenaltyMap(3);
    }

    public void UpdateUnitMouvementPenalty(int penalty, List<Node> path)
    {
        foreach (var n in path)
        {
            for (int x = -1; x <= 1; x++)
            {

                for (int y = -1; y <= 1; y++)
                {
                    int appliedPenalty;
                    if (x == 0 && y == 0)
                    {
                        appliedPenalty = penalty;
                    }
                    else
                    {
                        appliedPenalty = (int)penalty / 2;
                    }

                    if (grid[n._gridX , n._gridY ]._unitMovementPenalty + appliedPenalty <= 0)
                        grid[n._gridX , n._gridY ]._unitMovementPenalty = 0;
                    else
                        grid[n._gridX , n._gridY ]._unitMovementPenalty += appliedPenalty;
                }
            }
        }
    }

    public List<Node> SetNodeBusy(Vector3 pos)
    {
        var busyNodes = new List<Node>();
        
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
               var node = NodeFromWorldPoint(pos);
               grid[node._gridX + x, node._gridY + y]._isBusy = true;
               busyNodes.Add(grid[node._gridX + x, node._gridY + y]);
            }
        }
        return busyNodes;
    }
    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, (Vector3)gridWorldSize);

        if (grid != null && displayGridGizmos)
        {
            foreach (var node in grid)
            {
                if (isBlurredPenaltyActive)
                {
                    Gizmos.color = Color.Lerp(Color.white, Color.black, Mathf.InverseLerp(penaltyMin, penaltyMax, node._movementPenalty));
                    Gizmos.color = (node._walkable) ? Gizmos.color : Color.red;
                    if (node._isBusy)
                    {
                        Gizmos.color = Color.blue;
                    }     
                    Gizmos.DrawCube(node._worldPosition, Vector3.one * (nodeDiameter -0.05f ));
                }
                else
                {
                    Gizmos.color = (node._walkable) ? Color.white : Color.red;
                    Gizmos.DrawCube(node._worldPosition, Vector3.one * (nodeDiameter -0.2f));
                }

               
            }
        }

    }

    void BlurPenaltyMap(int blurSize)
    {
        int kernelSize = blurSize * 2 + 1;
        int kernelExtents = (kernelSize - 1) / 2;

        int[,] penaltiesHorizontalPass = new int[gridSizeX, gridSizeY];
        int[,] penaltiesVerticalPass = new int[gridSizeX, gridSizeY];

        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = -kernelExtents; x <= kernelExtents; x++)
            {
                int sampleX = Mathf.Clamp(x, 0, kernelExtents);
                penaltiesHorizontalPass[0, y] += grid[sampleX, y]._movementPenalty;
            }
            for (int x = 1; x < gridSizeX; x++)
            {
                int removeIndex = Mathf.Clamp(x - kernelExtents - 1,0,gridSizeX);
                int addIndex = Mathf.Clamp(x + kernelExtents, 0, gridSizeX - 1);

                penaltiesHorizontalPass[x, y] = penaltiesHorizontalPass[x - 1, y] - grid[removeIndex, y]._movementPenalty + grid[addIndex, y]._movementPenalty;

            }
        }
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = -kernelExtents; y <= kernelExtents; y++)
            {
                int sampleY = Mathf.Clamp(x, 0, kernelExtents);
                penaltiesVerticalPass[x, 0] += grid[x, sampleY]._movementPenalty;
            }
            int blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x, 0] / (kernelSize * kernelSize));
            grid[x, 0]._movementPenalty = blurredPenalty;

            for (int y = 1; y < gridSizeY; y++)
            {
                int removeIndex = Mathf.Clamp(y - kernelExtents - 1, 0, gridSizeY);
                int addIndex = Mathf.Clamp(y + kernelExtents, 0, gridSizeY - 1);

                penaltiesVerticalPass[x, y] = penaltiesVerticalPass[x , y-1] - penaltiesHorizontalPass[x, removeIndex] + penaltiesHorizontalPass[x, addIndex];
                blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x, y] / (kernelSize * kernelSize));
                grid[x, y]._movementPenalty = blurredPenalty;

                if(blurredPenalty > penaltyMax)
                {
                    penaltyMax = blurredPenalty;
                }
                if(blurredPenalty< penaltyMin)
                {
                    penaltyMin = blurredPenalty;
                }

            }
        }
    }
  

}


