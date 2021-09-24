using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wanderer.Utils;
public class UnDeadMeca : GunUserDistance
{
    public override void DoChasingState()
    {
        isInAttackRange(attackRange);
    }

    public override void DoAttackingState()
    {
        
        isOutOfAttackRange(attackRange);
        PlayAttackAnim(WeaponAnimator);
        StartCoroutine(SwitchToFleeState());

    }

    public override void DoPatrollingState()
    {
        isInChasingRange(inSight);
    }

    private IEnumerator SwitchToFleeState()
    {
        if (!isAttacking && CanFlee)
        {
            CanFlee = false;
            SetState(new FleeingState(this, fleeingSpeed: AIMouvement.MoveForce *1.3f, fleeingDebuffTime: 2f, minFleeDistance: 2f, maxFleeDistance : 4f));
            yield return null;
        }
    }

}
