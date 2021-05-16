using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Line 
{
    const float verticalLineGradient = 1e5f;
    float _gradient, _y_intercept, _gradientPerpendicular;
    Vector2 _pointOnLine_1;
    Vector2 _pointOnLine_2;

    bool approachSide;
    
    public Line(Vector2 pointOnLine, Vector2 pointPerpendicularToLine)
    {
        float dx = pointOnLine.x - pointPerpendicularToLine.x;
        float dy = pointOnLine.y - pointPerpendicularToLine.y;
        if(dx == 0)
        {
            _gradientPerpendicular = verticalLineGradient;
        }
        else
        {
            _gradientPerpendicular = dy / dx;
        }
        if(_gradientPerpendicular == 0)
        {
            _gradient = verticalLineGradient;
        }
        else
        {
            _gradient = -1 / _gradientPerpendicular;
        }
        _y_intercept = pointOnLine.y - _gradient * pointOnLine.x;
        _pointOnLine_1 = pointOnLine;
        _pointOnLine_2 = pointOnLine + new Vector2(1,_gradient);
        approachSide = false;
        approachSide = GetSide(pointPerpendicularToLine);
    }

    bool GetSide(Vector2 p)
    {
        return (p.x - _pointOnLine_1.x) * (_pointOnLine_2.y - _pointOnLine_1.y) > (p.y - _pointOnLine_1.y) * (_pointOnLine_2.x - _pointOnLine_1.x);
    }

    public bool HasCrossedLine(Vector2 p)
    {
        return GetSide(p) != approachSide;
    }
    public float DistanceFromPoint(Vector2 p)
    {
        float yInterceptPerpendicular = p.y - _gradientPerpendicular * p.x;
        float intersectX = (yInterceptPerpendicular - _y_intercept) / _gradient - _gradientPerpendicular;
        float intersectY = _gradient * intersectX + _y_intercept;
        return Vector2.Distance(p, new Vector2(intersectX, intersectY));
    }

    public void DrawWithGizmos(float length)
    {
        Vector3 lineDir = new Vector3(1, 0, _gradient).normalized;
        Vector3 lineCentre = new Vector3(_pointOnLine_1.x, _pointOnLine_1.y);
        Gizmos.DrawLine(lineCentre - lineDir * length / 2, lineCentre + lineDir * length / 2);

    }
}
