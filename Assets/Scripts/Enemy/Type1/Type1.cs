using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type1 : EnemyMouvement
{

    [SerializeField] protected Type1ScriptableObject Type1Data;
    private Type1Attack type1Attack;
    private void Start()
    {
        // type1Attack Ref
        SetData();
        type1Attack = GetComponent<Type1Attack>();
    }

    // Aggro si pas entrain de charger
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


    protected void SetData()
    {
        moveSpeed = Type1Data.moveSpeed;
        aggroDistance = Type1Data.aggroDistance;
    }
}
