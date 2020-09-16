using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type1 : EnemyMouvement
{

    [SerializeField] protected Type1ScriptableObject Type1Data;


    protected override void Aggro()
    {
        base.Aggro();
    }

    protected override void SetFirstPatrolPoint()
    {
        base.SetFirstPatrolPoint();
    }
    protected override void Patrol()
    {
        base.Patrol();
    }

    protected virtual void SetData()
    {
        moveSpeed = Type1Data.moveSpeed;
        aggroDistance = Type1Data.aggroDistance;
    }
}
