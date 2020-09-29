using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
/// <summary>
/// Classe mère des ennemis 
/// Elle contient une enum permettant d'indiquer le State de l'ennemi
/// Elle contient des fonctions permettant de gerer le pathfinding (UpdatePath() + MovetoPath() + OnPathComplete(Path p)). Pour avoir des détails se référer à Lopez
/// Une fonction de patrouille
/// Une fonction permettant de suivre le joueur si il est en range d'aggro
/// Une fonction permmettant de savoir si le joueur est en range d'aggro
/// Une fonction permettant d'initialiser le premier point de patrouille
/// Les fonctions nécessaires à la gestion de la vie de l'ennemi (se référer à Lopez ou au tuto FR)
/// </summary>
public class Enemy : MonoBehaviour
{
    public State currentState;
    public enum State
    {
        Patrolling,
        Chasing,
        Attacking,
    }

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


    //seeker.IsDone() vérifie si le path est calculé
    //seeker.StartPath() est appellée pour commencer à calculer le chemin
   protected virtual void UpdatePath()
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
    public float moveSpeed;
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



  

    // Enemy patrol fonction
    protected virtual void Patrol()
    {
            // Check la distance en lui et le prochain point (puis change de point)
            if (Vector3.Distance(transform.position, targetPoint.position) < 1f)
            {
                index = (index + 1) % wayPoints.Length;
                targetPoint = wayPoints[index];

            }
    }

    // Actualise le State en Chasing si le joueur est repéré
    protected virtual void PlayerInSight()
    {
        if (Vector3.Distance(transform.position, target.position) < inSight) currentState = State.Chasing;
    }

    // Enemy take Player aggro 
    protected virtual void Aggro()
    {
        if(currentState == State.Chasing)
            // Change de target 
            targetPoint = target;
    }

    // Set le premier point de patrouille
    protected virtual void SetFirstPatrolPoint()
    {
        targetPoint = wayPoints[0];
    }



    //Health

    // Vie actuelle
    public int currentHealth;
    // Vie initial
    public int maxHealth;
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
        if (currentHealth < 1)
        {
            Destroy(gameObject);
        }
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
