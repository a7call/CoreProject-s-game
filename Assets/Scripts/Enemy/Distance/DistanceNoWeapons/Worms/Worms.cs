using System;
using System.Collections;
using UnityEngine;


public class Worms : DistanceNoGun
{
    protected override void Awake()
    {
        base.Awake();
        AddAnimationEvent("Attack", "CanShootCO");
    }
    private IEnumerator SwitchToFleeState(float fleeRange)
    {
        if (!isAttacking && (Vector3.Distance(transform.position, target.position) < fleeRange) && CanFlee)
        {
            CanFlee = false;
            yield return PrepareToFlee();   
            SetState(new FleeingState(this, fleeingSpeed: 2.5f, fleeingDebuffTime: 2.5f, minFleeDistance: 4f));
        }
    }

    private IEnumerator PrepareToFlee()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("isFleeing", true);
        BecomeInvulnerable();
    }

    private void BecomeInvulnerable()
    {
        GetComponent<BoxCollider2D>().enabled = false;  
    }

    public override void DoChasingState()
    {
        StartCoroutine(SwitchToFleeState(1f));
        isInAttackRange(attackRange);
    }

    public override void DoAttackingState()
    {
        StartCoroutine(SwitchToFleeState(1f));
        isOutOfAttackRange(stopAttackRange);
        SetInitialAttackPosition();
        PlayAttackAnim();
    }

    public override void EndFleeingState()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        animator.SetBool("isFleeing", false);
    }

    public override void StartChasingState()
    {
        //Do nothing;
    }
}
