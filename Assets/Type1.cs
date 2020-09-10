using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type1 : EnemyMouvement
{
    private Type1Attack type1Attack;
    private void Start()
    {
        type1Attack = GetComponent<Type1Attack>();
    }
    protected override void Aggro()
    {
        if (!type1Attack.isCharging)
        {
            Vector3 dir = (targetToFollow.position - transform.position).normalized;
            rb.velocity = dir * moveSpeed * Time.fixedDeltaTime;
        }
        else
        {
            return;
        }
        


    }
    protected override void Patrol()
    {
       // DO nothing
    }


   
}
