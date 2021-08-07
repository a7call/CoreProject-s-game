using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMouvement : MonoBehaviour
{
    const float minPathUpdateTime = 0.3f;
    const float pathUpdateMoveThreshHold = 0.5f;

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
    
    private bool shouldMove = false;
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
    [SerializeField]
    private bool shouldSearch = false;
    public bool ShouldSearch {
        get
        {
            return shouldSearch;
        }
        set
        {
            shouldSearch = value;
        }
    }

    private Rigidbody2D rb;
    public bool isMoving = false;
    #endregion  

    int penaltyToNodeOnPath = 400;
    List<Node> currentPath = new List<Node>();


    public Transform target { get; set; }

    public Transform Player { get; private set; }

    
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

    public Coroutine UpdatePathCo = null;

    PathWanderer path;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("AItarget").transform;
        target = Player;
        grid = FindObjectOfType<NodeGrid>();
        rb = GetComponent<Rigidbody2D>();
        UpdatePathCo = StartCoroutine(UpdatePath());
    }
   
    public void OnPathFound(Vector3[] wayPoints, bool pathSuccessful, List<Node> nodePath)
    {
        if (pathSuccessful)
        {
            //shouldMove = true;
            if (this == null)
                return;

            path = new PathWanderer(wayPoints, transform.position, turnDistance, stoppingDist, nodePath); 
            currentPath = path._nodePath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
            grid.UpdateUnitMouvementPenalty(penaltyToNodeOnPath, nodePath);
        }
    }
    private void FixedUpdate()
    {
        if (shouldMove)
        { 
          rb.MovePosition(((Vector2)transform.position + directionToTarget.normalized * Time.deltaTime * speed));
        } 
    }

    public IEnumerator UpdatePath(bool forcePathing = false)
    {
        if (Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }
        float sqrMoveThreshHold = pathUpdateMoveThreshHold * pathUpdateMoveThreshHold;
        Vector3 targetPosOld = target.position;
        while (shouldSearch)
        {
            if (target == null)
                yield break;
            yield return new WaitForSeconds(minPathUpdateTime);
            if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshHold || forcePathing)
            {
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
                targetPosOld = target.position;
            }
        }
    }

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
                    directionToTarget = Vector2.zero;
                    //shouldMove = false;
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
        //if (path != null)
        //{
        //    path.DrawWithGizmos();
        //}
    }
    private void OnDestroy()
    {
    }
}
