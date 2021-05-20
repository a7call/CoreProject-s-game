using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;


public class PathRequestManager : MonoBehaviour
{
    Queue<PathResult> results = new Queue<PathResult>();

    static PathRequestManager instance;
    WandererPathFinding pathFinding;


    private void Awake()
    {
        instance = this;
        pathFinding = GetComponent<WandererPathFinding>();
    }
    private void Update()
    {
        if(results.Count > 0)
        {
            int itemsInQueue = results.Count;
            lock (results)
            {
                for (int i = 0; i < itemsInQueue; i++)
                {
                    PathResult result = results.Dequeue();
                    result._callback(result._path, result._success, result._nodePath);
                    
                }
            }
        }
    }
    public static void RequestPath(PathRequest request)
    {
        ThreadStart threadStart = delegate
        {
            instance.pathFinding.FindPath(request, instance.FinishedProcessingPath);
        };
        threadStart.Invoke();
    }


    public void FinishedProcessingPath(PathResult result)
    {
        lock(results){
            results.Enqueue(result);
        }
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
    public Vector3 _pathStart;
    public Vector3 _pathEnd;
    public Action<Vector3[], bool, List<Node>> _callback;

    public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool, List<Node>> callback)
    {
        _pathStart = start;
        _pathEnd = end;
        _callback = callback;
    }
}
