using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;


public class PathRequestManager : MonoBehaviour
{
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    static PathRequestManager instance;
    WandererPathFinding pathfinding;

    bool isProcessingPath;

    void Awake()
    {
        instance = this;
        pathfinding = GetComponent<WandererPathFinding>();
    }
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd , Action<Vector3[], bool, List<Node>> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    void TryProcessNext()
    {
        if (!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success, List<Node> nodes)
    {
        currentPathRequest._callback(path, success, nodes);
        isProcessingPath = false;
        TryProcessNext();
    }
}
public struct PathResult
{
    public Vector3[] _path;
    public bool _success;
    public List<Node> _nodePath;
    public Action<Vector3[], bool, List<Node>> _callback;
    public PathResult(Vector3[] path, bool success, List<Node> nodePath, Action<Vector3[], bool, List<Node>> callback )
    {
        _path = path;
        _success = success;
        _callback = callback;
        _nodePath = nodePath;
    }
}
public struct PathRequest
{
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public Action<Vector3[], bool, List<Node>> _callback;

    public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool, List<Node>> callback)
    {
       pathStart = start;
        pathEnd = end;
        _callback = callback;
    }
}
