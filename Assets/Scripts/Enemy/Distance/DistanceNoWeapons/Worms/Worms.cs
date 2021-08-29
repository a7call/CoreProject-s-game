using System;
using System.Collections;
using UnityEngine;
using Wanderer.Utils;


public class Worms : DistanceNoGun
{
    private ParticleSystem smokeFleeingParticules;

    protected override void Awake()
    {
        base.Awake();
        Utils.AddAnimationEvent(EnemyConst.ATTACK_ANIMATION_NAME, EnemyConst.SHOOT_COROUTINE_EVENT_FUNCTION_NAME, animator);
        SetUpPS();
        Utils.TogglePs(smokeFleeingParticules, enabled: false);
    }
    #region States
    // Flee State
    private IEnumerator SwitchToFleeState()
    {
        if (!isAttacking && CanFlee)
        {
            CanFlee = false;
            yield return PrepareToFlee();
            SetState(new FleeingState(this, fleeingSpeed: AIMouvement.MoveForce, fleeingDebuffTime: 5f, minFleeDistance: 4f));

        }
    }

    private IEnumerator PrepareToFlee()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool(EnemyConst.FLEE_BOOL_CONST, true);
        while (!Utils.isClipPlaying("Flee", this.animator))
        {
            yield return null;
        }
        yield return new WaitForSeconds(Utils.GetAnimationClipDurantion("UnBorrow", animator, timeToRemove: 0.1f));
        Utils.TogglePs(smokeFleeingParticules, enabled: true);
        BecomeInvulnerable();
    }


    public override IEnumerator EndFleeingState()
    {
        Utils.TogglePs(smokeFleeingParticules, enabled: false);
        animator.SetBool(EnemyConst.FLEE_BOOL_CONST, false);
        while (!Utils.isClipPlaying("UnBorrow", this.animator))
        {
            yield return null;
        }
        yield return new WaitForSeconds(Utils.GetAnimationClipDurantion("UnBorrow", animator, timeToRemove: 0.2f));
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void BecomeInvulnerable()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    // Chasing
    public override void DoChasingState()
    {
        isInAttackRange(attackRange);
        StartCoroutine(SwitchToFleeState());
    }
    public override void StartChasingState()
    {
        //Do nothing;
    }

    // Attacking
    public override void DoAttackingState()
    {
        StartCoroutine(SwitchToFleeState());
        isOutOfAttackRange(StopAttackRange);
        SetInitialAttackPosition();
        PlayAttackAnim(animator);
    }
    #endregion

    #region Attack
    private int maxNumberOfBullet = 30;
    float angle = 0f;
    public override IEnumerator InstantiateProjectileCO()
    {
        int numberOfBullet = 0;
        do
        {
            numberOfBullet++;
            SpiralFire();
            yield return new WaitForSeconds(0.1f);
        }
        while (numberOfBullet <= maxNumberOfBullet);
    }


    private void SpiralFire()
    {
        for (int i = 0; i <= 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((float)(((angle + 180f * i) * Math.PI) / 180f));
            float bulDirY = transform.position.y + Mathf.Cos((float)(((angle + 180f * i) * Math.PI) / 180f));
            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;
            GameObject bul = Instantiate(Projetile, attackPoint.position, Quaternion.identity);
            bul.GetComponent<Projectile>().SetProjectileDatas(Damage, Dispersion, ProjetileSpeed, HitLayer, this.gameObject, 10, bulDir);
        }
        angle += 10;

        if (angle >= 360f)
            angle = 0f;
    }
    #endregion

    #region Particule System
    private void SetUpPS()
    {
        smokeFleeingParticules = GetComponentInChildren<ParticleSystem>();
        smokeFleeingParticules.Play();
    }
    #endregion

}
