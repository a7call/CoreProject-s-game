using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMouvement : MonoBehaviour
{
    const float minPathUpdateTime = 0.2f;
    const float pathUpdateMoveThreshHold = 0.5f;

    float isMoveThreshHold = 0.1f;
    List<Node> occupiedNodes = new List<Node>();

    NodeGrid grid;

    private Animator animator;

    #region Mouvement variable
    Vector2 directionToTarget = new Vector2();
    Vector3 currentDir;
    public bool shouldMove = false;
    private Rigidbody2D rb;
    public bool isMoving = false;
    #endregion  

    int penaltyToNodeOnPath = 5;
    List<Node> currentPath = new List<Node>();

    
    float interpolationSpeed;

    [Header("Target")]
    public Transform target;

    [Header("PathSpeed")]
    public float speed = 2;
    public float turnSpeed = 3;
    public float turnDistance = 5;

    [Header("SlowDownUnit")]
    private float stoppingDist = 0;

    PathWanderer path;
    private void Start()
    {
        grid = FindObjectOfType<NodeGrid>();
        rb = GetComponent<Rigidbody2D>();
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
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        if (shouldMove)
        {
           rb.MovePosition(((Vector2)transform.position + directionToTarget.normalized * Time.deltaTime * speed));
        }
            
       // GetLastDirection();
        
    }
    //Vector2 lastPos = new Vector2();
    //protected virtual void GetLastDirection()
    //{
    //    Vector2 trackVelocity = (rb.position - lastPos) * 50;
    //    lastPos = rb.position;

        
    //    animator.SetFloat("Speed", 1);
    //    animator.SetFloat("HorizontalSpeed", trackVelocity.x);
    //    animator.SetFloat("VerticalSpeed", trackVelocity.y);
    //}

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
            else if (isMoving && (transform.position - unitPosOld).sqrMagnitude < sqrMoveThreshHold)
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
                    shouldMove = false;
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
                    if (speedPercent < 0.8f)
                    {
                        shouldMove = false;
                        followingPath = false;
                        break;
                    }
                  
                }
                shouldMove = true;
                var directiontoMoveTo = path.lookPoints[pathIndex] - transform.position;
                interpolationSpeed += turnSpeed;
                directionToTarget = Vector3.Slerp(currentDir, directiontoMoveTo, interpolationSpeed * Time.deltaTime);
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
