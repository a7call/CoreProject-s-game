using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    

    //Mouvement
    protected float moveSpeed;
    [SerializeField] protected Transform[] wayPoints;
    protected Transform targetPoint;
    protected float inSight;
    private int index = 0;
    [SerializeField] protected Transform target;
    protected Rigidbody2D rb;
    [SerializeField] protected bool isPatroling;


    // PathFinding

    public float nextWayPointDistance = 3f;

    Path path;
    int currentWayPoint;
    bool reachedEndOfPath;
    Seeker seeker;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.1f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(transform.position, targetPoint.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    protected virtual void MoveToPath()
    {
        if (path == null)
        {
            return;
        }

        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Vector2 dir = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = dir * moveSpeed * Time.fixedDeltaTime;
        rb.velocity = force;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }
    }

    // Enemy patrol fonction
    protected virtual void Patrol()
    {
        if (isPatroling)
        {
            
            if (Vector3.Distance(transform.position, targetPoint.position) < 1f)
            {
                index = (index + 1) % wayPoints.Length;
                targetPoint = wayPoints[index];

            }
        }
    }

    // Enemy take Player aggro 
    protected virtual void Aggro()
    {
        if (Vector3.Distance(transform.position, target.position) < inSight)
        {

            targetPoint = target;
            isPatroling = false;

        }
    }

    protected virtual void SetFirstPatrolPoint()
    {
        targetPoint = wayPoints[0];
    }



    //Health


    [SerializeField] protected int currentHealth;
    protected int maxHealth;
    protected Material whiteMat;
    protected Material defaultMat;
    [SerializeField] protected SpriteRenderer spriteRenderer;


    // Set health to maximum
    protected virtual void SetMaxHealth()
    {
        currentHealth = maxHealth;
    }

    // prends les dammages
    protected virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        StartCoroutine(WhiteFlash());
    }

    // Couroutine white flash on hit
    protected virtual IEnumerator WhiteFlash()
    {

        spriteRenderer.material = whiteMat;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMat;

    }



    //Attack

    


    void FacePlayer()
    {
        //to do
    }











}
