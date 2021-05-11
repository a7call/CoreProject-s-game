using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAvoidance: MonoBehaviour
{

    public float _avoidanceRadius;
    public List<Enemy> allEnemies = new List<Enemy>();
    public float avoidancePriority;
    public float pathfindPriority;
    private Player player;

    private Rigidbody2D rb;
    private AIPath aIPath;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        allEnemies.AddRange(FindObjectsOfType<Enemy>());
        player = FindObjectOfType<Player>();

        aIPath = GetComponent<AIPath>();
    }
    private void Update()
    {
        //  Move();
        MoveWithAi();
    }

   

    List<Enemy> GetEnemiesInRadius()
    {
        List<Enemy> returnedEnemies = new List<Enemy>();
        foreach (var enemy in allEnemies)
            if(Vector3.Distance(transform.position, enemy.transform.position) <= _avoidanceRadius )
            {
                if(enemy.gameObject != this.gameObject)
                {
                    returnedEnemies.Add(enemy);
                }
               
            }

        return returnedEnemies;
    }

    Vector3 Avoidance()
    {
        Vector3 avoidVector = new Vector3();
        var enemyList = GetEnemiesInRadius();
        if (enemyList.Count == 0)
            return avoidVector;
        foreach(var enemy in enemyList)
        {
            avoidVector += RunAway(enemy.transform.position);
        }
        return avoidVector;
    }

    void Move()
    {
        rb.velocity = (pathfindPriority * MoveTowardTarget(player.transform.position)
            + avoidancePriority * Avoidance());
        
    }
    void MoveWithAi()
    {

        rb.AddForce(20*Avoidance()*Time.deltaTime);
    }
    private Vector3 RunAway(Vector3 position) 
    {
        Vector3 neededVelocity = (transform.position - position) * 200;
        return neededVelocity;
    }

    Vector3 MoveTowardTarget(Vector3 targetPos)
    {
        Vector3 velocityTowardTarget = (targetPos - transform.position).normalized;
        return velocityTowardTarget;
    }
}
