using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EggRunner : Cac
{
    private Vector3 randomPosition;
    [SerializeField] private float maxDistance = 3f;

    protected override void Start()
    {
        base.Start();
        aIPath.canMove = false;
        randomPosition = maxDistance*Random.insideUnitCircle;
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
        
        if(Vector3.Distance(transform.position, randomPosition) >= agressiveDistance)
        {
            // Va au point
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

    private void Attack()
    {
        // Explosion + Destroy
        print("Attaque");
    }
}
