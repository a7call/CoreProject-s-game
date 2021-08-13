using System.Collections;
using UnityEngine;

public class UnDeadCac : Cac
{   

    public override void DoChasingState()
    {
        isInAttackRange(attackRange);
    }

    public override void DoAttackingState()
    {
        isOutOfAttackRange(attackRange);
        // StartAttack sequence.
    }

    public override void DoPatrollingState()
    {
        isInChasingRange(inSight);
    }
}