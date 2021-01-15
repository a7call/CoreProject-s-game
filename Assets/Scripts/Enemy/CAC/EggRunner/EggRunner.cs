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

    protected override void isInRange()
    {
        if (gameObject == null) return;
        
        if(Vector3.Distance(transform.position, randomPosition) >= 0.1f)
        {
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
        print("Attaque");
    }
}
