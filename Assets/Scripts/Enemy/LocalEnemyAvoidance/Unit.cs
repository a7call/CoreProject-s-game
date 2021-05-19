using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    const float minPathUpdateTime = 0.2f;
    const float pathUpdateMoveThreshHold = 0.5f;

    float isMoveThreshHold = 0.1f;
    List<Node> occupiedNodes = new List<Node>();

    NodeGrid grid;

    private Animator animator;
    public bool isMoving = false;

    int penaltyToNodeOnPath = 5;
    List<Node> currentPath = new List<Node>();


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
   
    public void OnPathFound(Vector3[] wayPoints, bool pathSuccessful, List<Node> nodePath)
    {
        if (pathSuccessful)
        {
            path = new PathWanderer(wayPoints, transform.position, turnDistance, stoppingDist, nodePath);
            currentPath = path._nodePath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
            grid.UpdateUnitMouvementPenalty(penaltyToNodeOnPath, nodePath);
        }
       
    }
    private void FixedUpdate()
    {
        GetLastDirection();
    }
    protected virtual void GetLastDirection()
    {
        
        animator.SetFloat("Speed", 1);
        animator.SetFloat("lastMoveX", target.position.x - gameObject.transform.position.x);
        animator.SetFloat("lastMoveY", target.position.y - gameObject.transform.position.y);
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
                grid.UpdateUnitMouvementPenalty(-penaltyToNodeOnPath, currentPath);
                PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }

        }
           
    }
   
    private void Update()
    {
        
    }
  
    IEnumerator isUnitMoving()
    {
        float sqrMoveThreshHold = isMoveThreshHold * isMoveThreshHold;
        
        while (true)
        {
            Vector3 unitPosOld = transform.position;
            yield return new WaitForSeconds(0.5f);
           
            if ((transform.position - unitPosOld).sqrMagnitude > sqrMoveThreshHold && !isMoving)
            {
                isMoving = true;
                UnBusyNodes();
            }
            else if (isMoving)
            {
                isMoving = false;
                occupiedNodes =  grid.SetNodeBusy(transform.position);  
            }

        }
    }
    void UnBusyNodes()
    {
        foreach(var node in occupiedNodes)
        {
            node._isBusy = false;
        }
    }

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
