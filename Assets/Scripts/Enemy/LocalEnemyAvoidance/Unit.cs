using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    const float minPathUpdateTime = 0.2f;
    const float pathUpdateMoveThreshHold = 0.5f;

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
        StartCoroutine(UpdatePath());
    }

    public void OnPathFound(Vector3[] wayPoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = new PathWanderer(wayPoints, transform.position, turnDistance, stoppingDist);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator UpdatePath()
    {
        if(Time.timeSinceLevelLoad < 0.3f)
        {
            yield return new WaitForSeconds(0.3f);
        }

        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);

        float sqrMoveThreshHold = pathUpdateMoveThreshHold * pathUpdateMoveThreshHold;
        Vector3 targetPosOld = target.position;
        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshHold)
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
        float speedPercent = 1;
        while (followingPath)
        {

            Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
            if(path.turnBoundaries.Length <= 0)
            {
                followingPath = false;
                break;
            }
            
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
