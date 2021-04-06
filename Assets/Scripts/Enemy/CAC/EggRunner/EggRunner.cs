using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EggRunner : Cac
{
    private Vector3 randomPosition;
    [SerializeField] private float maxDistance = 8f;
    [SerializeField] private LayerMask layer;

    protected override void Start()
    {
        base.Start();
        aIPath.canMove = false;
        randomPosition = maxDistance*Random.insideUnitCircle.normalized;
        direction = (randomPosition - transform.position).normalized;
    }

    protected override void Update()
    {
        base.Update();
        aIPath.canMove = false;
        switch (currentState)
        {
            case State.Chasing:
                isInRange();
                break;
            case State.Attacking:
                Attack();
                break;
        }

    }

    private float agressiveDistance = 1f;
    protected override void isInRange()
    {
        if (gameObject == null) return;
        float moveSpeed = aIPath.maxSpeed * 100f;
        if (Vector3.Distance(transform.position, randomPosition) >= agressiveDistance)
        {
            
            rb.velocity = moveSpeed * direction * Time.fixedDeltaTime;
            currentState = State.Chasing;
        }
        else if(Vector3.Distance(transform.position, randomPosition) <= agressiveDistance && Vector3.Distance(transform.position, randomPosition) >= 0.1f)
        {
            // Lancer une animation qui fait comprendre qu'il va exploser
            rb.velocity = moveSpeed * direction * Time.fixedDeltaTime;
            currentState = State.Chasing;
        }
        else
        {
            rb.velocity = Vector3.zero;
            currentState = State.Attacking;
        }
    }

    private float zoneExplose = 1f;
    private void Attack()
    {
        // Rajouter animation d'explosion
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, zoneExplose, layer);
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Player")) hit.GetComponent<Player>().TakeDamage(1);
            }
        Destroy(gameObject);
    }

    protected override void EnemyDie()
    {
        if (isDying)
        {
            isDying = false;
            nanoRobot();
            Attack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Attack();
        }

        if (collision.CompareTag("Player"))
        {
            Attack();
        }
    }

}
