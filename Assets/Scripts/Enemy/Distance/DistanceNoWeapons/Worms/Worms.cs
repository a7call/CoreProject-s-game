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
    protected override void Start()
    {
        base.Start();
        StartCoroutine(AllowFleeing());
    }
    #region States
    // Flee State
    private IEnumerator SwitchToFleeState(float fleeRange)
    {
        if (!isAttacking && CanFlee)
        {
            CanFlee = false;
            yield return PrepareToFlee();   
            SetState(new FleeingState(this, fleeingSpeed: 3.5f, fleeingDebuffTime: 5f, minFleeDistance: 4f));
        }
    }

    private IEnumerator PrepareToFlee()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool(EnemyConst.FLEE_BOOL_CONST, true);
        BecomeInvulnerable();
    }

    private IEnumerator AllowFleeing()
    {
        yield return new WaitForSeconds(1f);
        CanFlee = true;
    }
    public override void EndFleeingState()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        animator.SetBool(EnemyConst.FLEE_BOOL_CONST, false);
    }

    private void BecomeInvulnerable()
    {
        GetComponent<BoxCollider2D>().enabled = false;  
    }

    // Chasing
    public override void DoChasingState()
    {        
        isInAttackRange(attackRange);
        StartCoroutine(SwitchToFleeState(1f));
    }
    public override void StartChasingState()
    {
        //Do nothing;
    }

    // Attacking
    public override void DoAttackingState()
    {
        StartCoroutine(SwitchToFleeState(1f));
        isOutOfAttackRange(stopAttackRange);
        SetInitialAttackPosition();
        PlayAttackAnim();
    }
    #endregion

    #region Attack
    private int maxNumberOfBullet = 30;
    float angle = 0f;
    protected override IEnumerator ShootCO()
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
            GameObject bul = Instantiate(projetile, transform.position, Quaternion.identity);
            bul.GetComponent<Projectile>().SetMoveDirection(bulDir);
        }
        angle += 10;

        if (angle >= 360f)
            angle = 0f;
    }
    #endregion

}
