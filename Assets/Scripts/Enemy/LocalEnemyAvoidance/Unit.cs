using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    const float minPathUpdateTime = 0.2f;
    const float pathUpdateMoveThreshHold = 0.5f;

    NodeGrid grid;
    private Animator animator;
    public bool isMoving = false;

    Vector3 currentDir;
    float interpolationSpeed;

    [Header("Target")]
    public Transform target;

    [Header("PathSpeed")]
    public float speed = 2;
    public float turnSpeed = 3;
    public float turnDistance = 5;

    [Header("SlowDownUnit")]
    public float stoppingDist = 2;

    PathWanderer path;
    private void Start()
    {
        grid = FindObjectOfType<NodeGrid>();
        animator = GetComponent<Animator>();
        StartCoroutine(isUnitMoving());
        StartCoroutine(UpdatePath());
    }
    List<Node> oldNodePath = new List<Node>();
    public void OnPathFound(Vector3[] wayPoints, bool pathSuccessful, List<Node> nodePath)
    {
        if (pathSuccessful)
        {
            path = new PathWanderer(wayPoints, transform.position, turnDistance, stoppingDist, nodePath);
            oldNodePath = path._nodePath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
            SetPenaltyToNode(path._nodePath, 5);
        }
       
    }
    private void FixedUpdate()
    {
        GetLastDirection();
        //MoveWithAi();
    }
    protected virtual void GetLastDirection()
    {
        
        animator.SetFloat("Speed", 1);
        animator.SetFloat("lastMoveX", target.position.x - gameObject.transform.position.x);
        animator.SetFloat("lastMoveY", target.position.y - gameObject.transform.position.y);
    }
    void SetPenaltyToNode(List<Node> nodePath, int penalty)
    {
        foreach(var n in nodePath)
        {
            for(int x =-1; x<=1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    var appliedPenalty = 0;
                    if(x == 0 && y == 0) {
                        appliedPenalty = penalty;
                    }
                    else
                    {
                        appliedPenalty = (int)penalty / 2;
                    }
                    if(grid.grid[n._gridX + x, n._gridY + y]._movementPenalty + appliedPenalty >= 0)
                        grid.grid[n._gridX + x, n._gridY + y]._movementPenalty += appliedPenalty;
                    else
                        grid.grid[n._gridX + x, n._gridY + y]._movementPenalty += 0;
                }
            }             
        }
    }
    void SetPenaltyToNode2(List<Node> nodePath, int penalty)
    {
        foreach (var n in nodePath)
        {

                    if (grid.grid[n._gridX  , n._gridY ]._movementPenalty + penalty >= 0)
                        grid.grid[n._gridX , n._gridY ]._movementPenalty += penalty;
                    else
                        grid.grid[n._gridX , n._gridY ]._movementPenalty += 0;
        }
    }

    IEnumerator UpdatePath()
    {
        if(Time.timeSinceLevelLoad < 0.3f)
        {
            yield return new WaitForSeconds(0.3f);
        }

        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position,OnPathFound));

        float sqrMoveThreshHold = pathUpdateMoveThreshHold * pathUpdateMoveThreshHold;
        Vector3 targetPosOld = target.position;
        while (true)
        {
            
            yield return new WaitForSeconds(minPathUpdateTime);
            if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshHold)
            {
                SetPenaltyToNode(oldNodePath, -5);
                PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }

        }
           
    }
    float MoveThreshHold =0.5f;
    private void Update()
    {
        
    }
     
    IEnumerator isUnitMoving()
    {
        float sqrMoveThreshHold = MoveThreshHold * MoveThreshHold;
        Vector3 unitPosOld = transform.position;
        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
           
            if ((transform.position - unitPosOld).sqrMagnitude > sqrMoveThreshHold)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

        }
    }
    public LayerMask enemyLayer;
    //void GetNotMovingUnits()
    //{
    //    var hits = Physics2D.CircleCastAll(transform.position, 5,Vector2.zero,0, enemyLayer);
    //    if (hits.Length > 0)
    //    {
            
    //        foreach(var hit in hits)
    //        {
    //            if (hit.collider.gameObject == this.gameObject)
    //                continue;

    //            print(hit.collider.gameObject);
    //            if (!hit.collider.gameObject.GetComponent<Unit>().isMoving)
    //            {
    //                for (int x = -1; x <= 1; x++)
    //                {
    //                    for (int y = -1; y <= 1; y++)
    //                    {
    //                        grid.grid[(Mathf.RoundToInt((hit.transform.position.x - grid.worldBottomLeft.x  - grid.nodeRadius) / (2 * grid.nodeRadius))+x), (Mathf.RoundToInt((hit.transform.position.y - grid.worldBottomLeft.y - grid.nodeRadius) / (2 * grid.nodeRadius))+y)]._walkable = false;

    //                    }
    //                }                   
    //            }
    //        }
    //    }
    //}


    IEnumerator FollowPath()
    {
        bool followingPath = true;
        int pathIndex = 0;
        float speedPercent = 1;
        while (followingPath)
        {

            Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if(pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    break;
                }
                else
                {
                    currentDir = path.lookPoints[pathIndex] - transform.position;
                    interpolationSpeed = 0;
                    pathIndex++;
                }
            }
            if (followingPath)
            {
                if(pathIndex >= path.slowDownIndex && stoppingDist > 0)
                {
                    speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDist);
                    if (speedPercent < 0.5f)
                    {
                        followingPath = false;
                        break;
                    }
                  
                }
               
                var directiontoMoveTo = path.lookPoints[pathIndex] - transform.position;
                interpolationSpeed += turnSpeed;
                var dir = Vector3.Slerp(currentDir, directiontoMoveTo, interpolationSpeed * Time.deltaTime) ;
                transform.Translate(dir.normalized * Time.deltaTime * speed* speedPercent);
            }
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if(path != null)
        {
            path.DrawWithGizmos();
        }
    }
}
