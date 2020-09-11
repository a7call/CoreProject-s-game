using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type2Mouvement : EnemyMouvement
{

    public bool isShooting;



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
}
