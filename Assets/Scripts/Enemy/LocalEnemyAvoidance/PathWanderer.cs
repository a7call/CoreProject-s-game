using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathWanderer
{
    public readonly Vector3[] lookPoints;
    public readonly Line[] turnBoundaries;
    public readonly int finishLineIndex;
    public readonly int slowDownIndex;
    public PathWanderer(Vector3[] wayPoints, Vector3 startPos, float turnDist, float stoppingDist)
    {
        lookPoints = wayPoints;
        turnBoundaries = new Line[lookPoints.Length];
        finishLineIndex = turnBoundaries.Length - 1;
        Vector2 previousPoint = V3toV2(startPos);
        for (int i = 0; i < lookPoints.Length; i++)
        {
            Vector2 currentPoint = V3toV2(lookPoints[i]);
            Vector2 dirToCurrentPoint = (currentPoint - previousPoint).normalized;
            Vector2 turnBoundaryPoint = (i == finishLineIndex) ? currentPoint : currentPoint - dirToCurrentPoint * turnDist;
            turnBoundaries[i] = new Line(turnBoundaryPoint, previousPoint - dirToCurrentPoint * turnDist);
            previousPoint = turnBoundaryPoint;
        }
        float distFromEndPoint = 0;
        for (int i = lookPoints.Length -1; i > 0; i--)
        {
            distFromEndPoint += Vector3.Distance(lookPoints[i], lookPoints[i - 1]);
            if(distFromEndPoint > stoppingDist)
            {
                slowDownIndex = i;
                break;
            }
        }
    }
    Vector2 V3toV2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.y);
    }
    public void DrawWithGizmos()
    {
        Gizmos.color = Color.black;
        foreach (Vector3 p in lookPoints)
        {
            Gizmos.DrawCube(p, new Vector3(0.5f,0.5f,0.1f));
        }
        Gizmos.color = Color.white;
        foreach (Line l in turnBoundaries)
        {
            l.DrawWithGizmos(10);
        }
    }
}
