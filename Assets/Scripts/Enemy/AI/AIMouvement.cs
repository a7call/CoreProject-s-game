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

    #region Mouvement variable
    private Vector2 directionToTarget = new Vector2();
    public Vector2 DirectionToTarget
    {
        get
        {
            return directionToTarget;

        }
        set
        {
            directionToTarget = value;
        }
    }
    
    public bool shouldMove = true;
    bool canFindPath = false;
    public bool ShouldMove
    {
        get
        {
            return shouldMove;
        }
        set
        {
            if (shouldMove != value && !canFindPath)
                shouldMove = value;
        }

    }
    private Rigidbody2D rb;
    public bool isMoving = false;
    #endregion  

    int penaltyToNodeOnPath = 5;
    List<Node> currentPath = new List<Node>();

    
 

    [Header("Target")]
    public Transform target;

    
    private float speed = 2;
    public float Speed { 
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    // Smooth path (not used)
    private float turnSpeed = 4;
    private float turnDistance = 0f;
    float interpolationSpeed;

    [Header("SlowDownUnit")]
    private float stoppingDist = 0;

    PathWanderer path;
    private void Start()
    {
        grid = FindObjectOfType<NodeGrid>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(UpdatePath());
    }
   
    public void OnPathFound(Vector3[] wayPoints, bool pathSuccessful, List<Node> nodePath)
    {
        if (pathSuccessful)
        {
            canFindPath = false;
            shouldMove = true;
            if (this == null)
                return;

            path = new PathWanderer(wayPoints, transform.position, turnDistance, stoppingDist, nodePath);
            
            currentPath = path._nodePath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
            grid.UpdateUnitMouvementPenalty(penaltyToNodeOnPath, nodePath);
        }
        else
        {
            canFindPath = true;
            shouldMove = false;
        }
    }
    private void FixedUpdate()
    {
        if (shouldMove)
        { 
          rb.MovePosition(((Vector2)transform.position + directionToTarget.normalized * Time.deltaTime * speed));
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
                grid.UpdateUnitMouvementPenalty(-penaltyToNodeOnPath, currentPath);
                PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }
        }   
    }

    // BUSY NODE ABANDONNED FEATURE

    //IEnumerator isUnitMoving()
    //{
    //    float sqrMoveThreshHold = isMoveThreshHold * isMoveThreshHold; 
    //    while (true)
    //    {
    //        Vector3 unitPosOld = transform.position;
    //        yield return new WaitForSeconds(0.5f);

    //        if ((transform.position - unitPosOld).sqrMagnitude > sqrMoveThreshHold && !isMoving)
    //        {
    //            isMoving = true;
    //            UnBusyNodes();
    //        }
    //        else if (isMoving && (transform.position - unitPosOld).sqrMagnitude < sqrMoveThreshHold)
    //        {
    //            isMoving = false;
    //            UnBusyNodes();
    //            occupiedNodes =  grid.SetNodeBusy(transform.position);  
    //        }
    //    }
    //}
    //void UnBusyNodes()
    //{
    //    foreach(var node in occupiedNodes)
    //    {
    //        node._isBusy = false;
    //    }
    //}

    //END

    IEnumerator FollowPath()
    {
        bool followingPath = true;
        int pathIndex = 0;
        //float speedPercent = 1;
        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if(pathIndex == path.finishLineIndex)
                {
                    shouldMove = false;
                    followingPath = false;
                    break;
                }
                else
                {
                    pathIndex++;
                }
            }
            if (followingPath)
            {
                //if(pathIndex >= path.slowDownIndex && stoppingDist > 0)
                //{
                //    speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDist);
                //    if (speedPercent < 0.8f)
                //    {
                //        shouldMove = false;
                //        followingPath = false;
                //        break;
                //    }
                //}
               
                if (shouldMove)
                    directionToTarget = path.lookPoints[pathIndex] - transform.position;
                else
                    directionToTarget = Vector2.zero;
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
    private void OnDestroy()
    {
    }
}
