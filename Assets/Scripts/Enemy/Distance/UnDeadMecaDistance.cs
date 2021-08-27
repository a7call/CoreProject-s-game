using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wanderer.Utils;
public class UnDeadMecaDistance : GunUserDistance
{
    public override void DoChasingState()
    {
        isInAttackRange(attackRange);
    }

    public override void DoAttackingState()
    {
        isOutOfAttackRange(attackRange);
        PlayAttackAnim(WeaponAnimator);       
    }

    public override void DoPatrollingState()
    {
        isInChasingRange(inSight);
    }

}
