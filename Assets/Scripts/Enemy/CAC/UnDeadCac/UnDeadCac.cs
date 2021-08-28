﻿using System.Collections;
using UnityEngine;
using Wanderer.Utils;

public class UnDeadCac : Cac
{
    protected override void Awake()
    {
        base.Awake();
        Utils.AddAnimationEvent("UnDeadAttackRight", "ApplyDamage", animator, time : Utils.GetAnimationClipDurantion("UnDeadAttackRight", animator) * 0.75f, param : 50f);
        Utils.AddAnimationEvent("UnDeadAttackLeft", "ApplyDamage", animator, time:  Utils.GetAnimationClipDurantion("UnDeadAttackLeft", animator) * 0.75f, param: 50f);

    }
    public override void DoChasingState()
    {
        isInAttackRange(attackRange);
    }

    public override void DoAttackingState()
    {
        isOutOfAttackRange(attackRange);
        StartCoroutine(BaseAttack());
    }

    public override void DoPatrollingState()
    {
        isInChasingRange(inSight);
    }
}