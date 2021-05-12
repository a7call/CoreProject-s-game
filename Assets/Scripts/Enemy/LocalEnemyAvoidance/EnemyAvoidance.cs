using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAvoidance : MonoBehaviour
{
    public Transform targetPosition;
    Seeker seeker;
    public Path path;

    public float speed = 2;

    Vector3 velocity;
    Vector3 acceleration;

    public float maxAcceleration; 
    public float maxVelocity; 

    public float nextWaypointDistance = 3;

    private int currentWaypoint = 0;

    public bool reachedEndOfPath;

    public float _avoidanceRadius;
    public float maxAvoidanceRadius;

    public List<EnemyAvoidance> allEnemies = new List<EnemyAvoidance>();

    public float avoidancePriority;

    public float pathfindPriority;


    public void Start()
    {
        // Get a reference to the Seeker component we added earlier
        seeker = GetComponent<Seeker>();
        allEnemies.AddRange(FindObjectsOfType<EnemyAvoidance>());

        // Start to calculate a new path to the targetPosition object, return the result to the OnPathComplete method.
        // Path requests are asynchronous, so when the OnPathComplete method is called depends on how long it
        // takes to calculate the path. Usually it is called the next frame.
        InvokeRepeating("UpdatePath", 0f, 0.5f);
      
    }

    public void OnPathComplete(Path p)
    {


        if (!p.error)
        {
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        }
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
        }
    }
    bool avoidMode = false;
    public void Update()
    {
        if (GetEnemiesInRadius(_avoidanceRadius).Count > 0 && !avoidMode)
        {
            avoidMode = true;
            velocity = Vector3.zero;
        } 
        if (GetEnemiesInRadius(2*_avoidanceRadius).Count == 0 && avoidMode)
        {
            avoidMode = false;
            velocity = Vector3.zero;
        }
            

        if (!avoidMode)
        {
            velocity = PathFindingMouv();
        }
        else
        {
            acceleration = Combine();
            acceleration = Vector3.ClampMagnitude(acceleration, maxAcceleration);
            velocity += acceleration * Time.deltaTime;
            velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
        }
        transform.Translate(  velocity * Time.deltaTime);

    }
    public void FixedUpdate()
    {
        
    }

    public void OnDisable()
    {
        seeker.pathCallback -= OnPathComplete;
    }

    float speedFactor;
    Vector3 PathFindingMouv()
    {
        var pathFindingVector = new Vector2();

        if (path == null)
        {
            // We have no path to follow yet, so don't do anything
            return pathFindingVector;
        }

        // Check in a loop if we are close enough to the current waypoint to switch to the next one.
        // We do this in a loop because many waypoints might be close to each other and we may reach
        // several of them in the same frame.
        reachedEndOfPath = false;
        // The distance to the next waypoint in the path
        float distanceToWaypoint;
        while (true)
        {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        // Slow down smoothly upon approaching the end of the path
        // This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
         speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;

        // Direction to the next waypoint
        // Normalize it so that it has a length of 1 world unit
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        // Multiply the direction by our desired speed to get a velocity
        pathFindingVector = dir * speed * speedFactor;

        return pathFindingVector;
    }
    Vector3 Combine()
    {
        return pathfindPriority * PathFindingMouv()
            + avoidancePriority * Avoidance();
    }
    List<EnemyAvoidance> GetEnemiesInRadius(float radius)
    {
        List<EnemyAvoidance> returnedEnemies = new List<EnemyAvoidance>();
        foreach (var enemy in allEnemies)
            if (Vector3.Distance(transform.position, enemy.transform.position) <= radius)
            {
                if (enemy.gameObject != this.gameObject)
                {
                    returnedEnemies.Add(enemy);
                }

            }
        return returnedEnemies;

    }

    Vector3 Avoidance()
    {
        
        Vector3 avoidVector = new Vector3();
        var enemyList = GetEnemiesInRadius(_avoidanceRadius);
        if (enemyList.Count <= 0)
            return avoidVector;
        foreach (var enemy in enemyList)
        {
            avoidVector += RunAway(enemy.transform.position);
        }
        return avoidVector;
    }

    private Vector3 RunAway(Vector3 position)
    {
        Vector3 neededVelocity = (transform.position - position).normalized * maxAcceleration/3 ;
        print(neededVelocity);
        return neededVelocity - velocity;
    }

}
