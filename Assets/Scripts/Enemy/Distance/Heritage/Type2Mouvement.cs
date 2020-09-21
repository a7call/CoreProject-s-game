using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type2Mouvement : EnemyMouvement
{
    [SerializeField] protected Type2ScriptableObject Type2Data;
    public bool isShooting;
   


    // Aggro mais ne bouge pas et met à jour l'état de l'énemi
    protected override void Aggro()
    {
        if (Vector3.Distance(transform.position, targetToFollow.position) < aggroDistance)
        {
            isPatroling = false;
            rb.velocity = Vector2.zero;
            isShooting = true;
           
        }
        else
        {
            isPatroling = true;
            isShooting = false;
            return;
        }
    }

    protected virtual void SetData()
    {
        moveSpeed = Type2Data.moveSpeed;
        aggroDistance = Type2Data.aggroDistance;
    }

    protected override void SetFirstPatrolPoint()
    {
        base.SetFirstPatrolPoint();
    }
}
