using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{

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




    //Mouvement
    protected float moveSpeed;
    // Array de point pour patrouille
    [SerializeField] protected Transform[] wayPoints;
    // Destination pour patrouille
    protected Transform targetPoint;
    // Index de l'array
    private int index = 0;
    // Distance ou l'ennemi repère le joueur
    protected float inSight;
    // Player
    [SerializeField] protected Transform target;
    
    protected Rigidbody2D rb;
    // boolean check if Patroling
    [SerializeField] protected bool isPatroling;


  

    // Enemy patrol fonction
    protected virtual void Patrol()
    {
        // si en mode patrouille
        if (isPatroling)
        {
            // Check la distance en lui et le prochain point (puis change de point)
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
        // SUit le joueur si il est en vu
        if (Vector3.Distance(transform.position, target.position) < inSight)
        {
            // Change de target 
            targetPoint = target;
            // Desactive le mode patrouille
            isPatroling = false;

        }
    }
    // Set le premier point de patrouille
    protected virtual void SetFirstPatrolPoint()
    {
        targetPoint = wayPoints[0];
    }



    //Health

    // Vie actuelle
    [SerializeField] protected int currentHealth;
    // Vie initial
    protected int maxHealth;
    // Material d'indication pour un ennemi touché
    protected Material whiteMat;
    protected Material defaultMat;
    // sprite rendered de l'ennemi
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

    

    // Face le player quand il le suit
    void FacePlayer()
    {
        //to do
    }











}
