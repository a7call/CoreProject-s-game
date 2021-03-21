using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Distance.cs
/// Classe mère des monstres lasers
/// Le monstre a été modifié du à des problème vis à vis du gameplay de la première version.
/// Cette variation (variation de base) ce comporte comme un distance de base mis à part la gestion des visuel (anim d'attaque + prpojectile différent)
/// L'ancien code ce trouve en bas de page
/// </summary>
public class DistanceLaser : Distance
{
    private bool attackAnimationPlaying = false;
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

   
    protected override void Update()
    {
        //Debug.Log(isEnemyAlive);
        
        base.Update();

        switch (currentState)
        {
            case State.Patrolling:
                PlayerInSight();
                break;
            case State.Chasing:
                isInRange();
                // suit le path créé et s'arrête pour tirer
                break;
            case State.Attacking:
                SetInitialLaserPosition();
                isInRange();
                PlayAttackAnim();
                break;
        }
        ShouldNotMoveDuringShooting();
       
    }

    // Methode lergerement modifié pour permettre la mise en place des animations d'attaque
    protected override IEnumerator CanShootCO()
    {
        if (isReadytoShoot)
        {
            isReadytoShoot = false;
            // Wait for coroutine shoot to end
            yield return StartCoroutine(ShootCO());
            // delay before next Shoot
            yield return new WaitForSeconds(restTime);
            isReadytoShoot = true;
            // gestion de l'animation d'attaque
            attackAnimationPlaying = false;
        }
    }

   //Methode permetant de lancer la séquence de tir via l'animation
    void PlayAttackAnim()
    {
        if (!attackAnimationPlaying && !isPerturbateurIEM)
        {
            attackAnimationPlaying = true;
            animator.SetTrigger("isAttacking");
        }
    }
    
    // Récupere en temps réel la position de l'attaque point en fonction de l'animation joué 
    public void SetInitialLaserPosition()
    {
        
        float lastMoveX = animator.GetFloat("lastMoveX");
        float lastMoveY = animator.GetFloat("lastMoveY");
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
}



























































//protected  void LaserFiring()
//{

//    if(isShooting)
//    {

//        // à revoir
//        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, Mathf.Infinity, testsLayer);
//        laserBeam.GetComponent<LineRenderer>().SetPosition(1, laserBeam.transform.InverseTransformPoint(hits[0].point));

//        Debug.DrawRay(transform.position, dir*10, Color.red);
//        foreach (RaycastHit2D hit in hits)
//        {
//            if (hit.transform.gameObject.CompareTag("Player"))
//            {
//                laserBeam.GetComponent<LineRenderer>().SetPosition(1, laserBeam.transform.InverseTransformPoint(hit.point));
//                PlayerHealth player = hit.transform.gameObject.GetComponent<PlayerHealth>();
//                player.TakeDamage(damage);
//            }
//        }
//    } 
//}


//protected virtual IEnumerator ShootCo()
//{
//        SetInitialLaserPosition();
//        dir = (targetSetter.target.position - transform.position).normalized;
//        yield return new WaitForSeconds(timeBeforeShoot);
//        enableLaser();
//        yield return new WaitForSeconds(durationOfShoot);
//        disableLaser();
//        isShooting = false;
//        laserBeam.GetComponent<LineRenderer>().SetPosition(1, Vector2.zero);

//}
//bool hasStarted = false;
//protected override IEnumerator CanShootCO()
//{
//    if (!hasStarted)
//    {
//        hasStarted = true;   
//        yield return StartCoroutine(ShootCo());
//        yield return new WaitForSeconds(restTime);
//        isReadytoShoot = true;
//        hasStarted = false;
//    }
//}

//void PlayAttackAnim()
//{
//    if (isReadytoShoot && !isPerturbateurIEM)
//    {
//        isReadytoShoot = false;
//        isShooting = true;
//        animator.SetTrigger("isAttacking");
//    }
//}


//protected void enableLaser()
//{
//    laserBeam.GetComponent<LineRenderer>().enabled = true;
//}

//protected void disableLaser()
//{
//    laserBeam.GetComponent<LineRenderer>().enabled = false;
//}


//public void SetInitialLaserPosition()
//{
//    findAttackPoints();
//    float lastMoveX = animator.GetFloat("lastMoveX");
//    float lastMoveY = animator.GetFloat("lastMoveY");
//    if(Mathf.Abs(lastMoveX) > Mathf.Abs(lastMoveY))
//    {
//        if(lastMoveX > 0)
//        {
//            laserBeam.GetComponent<LineRenderer>().SetPosition(0, attackPointRightGO);  
//        }
//        else
//        {
//            laserBeam.GetComponent<LineRenderer>().SetPosition(0, attackPointLeftGO);
//        }
//    }
//    else
//    {
//        if (lastMoveY > 0)
//        {
//            laserBeam.GetComponent<LineRenderer>().SetPosition(0, attackPointBackGO);
//        }
//        else
//        {
//            laserBeam.GetComponent<LineRenderer>().SetPosition(0, attackPointFrontGO);
//        }
//    }
//}

//protected override void GetLastDirection()
//{
//    if (!isShooting)
//    {
//        if (aIPath.desiredVelocity.x > 0.1 || aIPath.desiredVelocity.x < 0.1 || aIPath.desiredVelocity.y < 0.1 || aIPath.desiredVelocity.y > 0.1)
//        {
//            animator.SetFloat("lastMoveX", targetSetter.target.position.x - gameObject.transform.position.x);
//            animator.SetFloat("lastMoveY", targetSetter.target.position.y - gameObject.transform.position.y);
//        }
//    }


//}

//Vector3 attackPointFrontGO;
//Vector3 attackPointBackGO;
//Vector3 attackPointLeftGO;
//Vector3 attackPointRightGO;

//private void findAttackPoints()
//{

//    foreach (Transform trans in transform)
//    {
//        if (trans.gameObject.name == "attackPoints")
//        {
//            Transform attackPointContainer = trans;
//            foreach (Transform t in trans)
//            {
//                    if (t.gameObject.name == "attackFront") attackPointFrontGO = trans.InverseTransformPoint(t.position);  
//                    if (t.gameObject.name == "attackBack") attackPointBackGO = trans.InverseTransformPoint(t.position);
//                    if (t.gameObject.name == "attackLeft") attackPointLeftGO = trans.InverseTransformPoint(t.position);
//                    if (t.gameObject.name == "attackRight") attackPointRightGO = trans.InverseTransformPoint(t.position);
//            }

//        }
//    }
//}    



