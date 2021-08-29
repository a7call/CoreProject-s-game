using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DistanceNoGun : Distance
{
    private Transform attackPointFrontGO;
    private Transform attackPointBackGO;
    private Transform attackPointLeftGO;
    private Transform attackPointRightGO;
    protected override void Awake()
    {
        base.Awake();
        SetData();
        SetMaxHealth();
        findAttackPoints();
    }

    protected override void GetReference()
    {
        base.GetReference();
    }
    // Start Shoot Sequence
   
   
    // Récupere en temps réel la position de l'attaque point en fonction de l'animation joué 
    public void SetInitialAttackPosition()
    {

        float lastMoveX = animator.GetFloat(EnemyConst.DIRECTION_X_CONST);
        float lastMoveY = animator.GetFloat(EnemyConst.DIRECTION_Y_CONST);
        if (Mathf.Abs(lastMoveX) > Mathf.Abs(lastMoveY))
        {
            if (lastMoveX > 0)
            {
                attackPoint = attackPointRightGO;
            }
            else
            {
                attackPoint = attackPointLeftGO;
            }
        }
        else
        {
            if (lastMoveY > 0)
            {
                attackPoint = attackPointBackGO;
            }
            else
            {
                attackPoint = attackPointFrontGO;
            }
        }
        
    }
   //Recherche les position potentiel des attaques points 
    private void findAttackPoints()
    {

        foreach (Transform trans in transform)
        {
            if (trans.gameObject.name == "attackPoints")
            {
                Transform attackPointContainer = trans;
                foreach (Transform t in trans)
                {
                    if (t.gameObject.name == "attackFront") attackPointFrontGO = t;
                    if (t.gameObject.name == "attackBack") attackPointBackGO = t;
                    if (t.gameObject.name == "attackLeft") attackPointLeftGO = t;
                    if (t.gameObject.name == "attackRight") attackPointRightGO = t;
                }

            }
        }
    }

    public override void DoChasingState()
    {
        isInAttackRange(attackRange);
    }

    public override void DoAttackingState()
    {
       isOutOfAttackRange(StopAttackRange);
       SetInitialAttackPosition();
       PlayAttackAnim(animator);
    }

    public override void DoPatrollingState()
    {
        isInChasingRange(inSight);
    }
    public override IEnumerator StartShootingProcessCo()
    {
        if (isReadytoShoot)
        {
            isReadytoShoot = false;
            // Wait for coroutine shoot to end
            yield return StartCoroutine(InstantiateProjectileCO());
            isAttacking = false;
            animator.SetBool(EnemyConst.ATTACK_BOOL_CONST, false);
            // delay before next Shoot
            yield return new WaitForSeconds(RestTime);
            isReadytoShoot = true;
            // gestion de l'animation d'attaque

            attackAnimationPlaying = false;
        }
    }
    
    // WHEN TO FLEE ?

}