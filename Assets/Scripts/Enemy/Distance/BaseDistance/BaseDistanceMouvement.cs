using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDistanceMouvement : Type2Mouvement
{

    private Vector3 dir;
    
    void Start()
    {
        SetFirstPatrolPoint();
        SetData();
    }
    private void Update()
    {
        Aggro();
        Patrol();
    }
    protected override void Aggro()
    {
         dir = (targetToFollow.position - transform.position).normalized;

        if (Vector3.Distance(transform.position, targetToFollow.position) < aggroDistance)
        {
            isPatroling = false;
            rb.velocity = dir * moveSpeed * Time.fixedDeltaTime;
            rb.velocity = Vector2.zero;
            isShooting = true;
        }
        else
        {
            rb.velocity = dir * moveSpeed * Time.fixedDeltaTime;
            isShooting = false;

        }
    }


    protected override void Patrol()
    {
        base.Patrol();
    }

    protected override void SetFirstPatrolPoint()
    {
        base.SetFirstPatrolPoint();
    }

    protected override void SetData()
    {
        base.SetData();
    }
   
}
