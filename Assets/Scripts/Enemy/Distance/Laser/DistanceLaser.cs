using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class DistanceLaser : Distance
{
    protected Vector3 dir;
    [SerializeField] protected float delayMovement;
    protected float timeBeforeShoot = 0.25f;
    protected float durationOfShoot = 1f;
    protected int damage = 1;
    protected bool isShootingLasers;
    protected GameObject laserBeam;



    void Start()
    {
        // Set data
        SetData();
        SetMaxHealth();
        GetLaserComp();
    }

    protected override void GetReference()
    {
        base.GetReference();
        targetSetter.target = target;
    }
    void GetLaserComp()
    {
        foreach(Transform child in transform)
        {
            if (child.GetComponent<LineRenderer>())
            {
                laserBeam = child.gameObject;
                laserBeam.GetComponent<LineRenderer>().enabled = false;
            }
        }
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
                aIPath.canMove = false;
                isInRange();
                StartCoroutine("CanShoot");
                Shoot();
                break;
        }


       

    }

    protected override void isInRange()
    {
        if (Vector3.Distance(transform.position, target.position) < attackRange )
        { 
            currentState = State.Attacking;
            isShooting = true;
            isReadyToSwitchState = false;
            aIPath.canMove = false;
        }
        else
        {
            if (currentState == State.Attacking && !isInTransition) StartCoroutine(transiChasing());
            if (isReadyToSwitchState && currentState != State.Chasing && !isreadyToAttack)
            {
                currentState = State.Chasing;
                isShooting = false;
            }

        }
    }



    // Voir Enemy.cs (héritage)
    public LayerMask testsLayer;
    protected override void Shoot()
    {
             
        if(isShootingLasers)
        {

            
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, Mathf.Infinity, testsLayer);
            laserBeam.GetComponent<LineRenderer>().SetPosition(1, laserBeam.transform.InverseTransformPoint(hits[0].point));
            
            Debug.DrawRay(transform.position, dir*10, Color.red);
            foreach (RaycastHit2D hit in hits)
            {
               
               
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    laserBeam.GetComponent<LineRenderer>().SetPosition(1, laserBeam.transform.InverseTransformPoint(hit.point));
                    PlayerHealth player = hit.transform.gameObject.GetComponent<PlayerHealth>();
                    player.TakeDamage(damage);
                }
            }
        }
       
    }


    protected virtual IEnumerator ShootLasersCo()
    {
        if(!isShootingLasers)
        {

            dir = (targetSetter.target.position - transform.position).normalized;

            yield return new WaitForSeconds(timeBeforeShoot);
            laserBeam.GetComponent<LineRenderer>().SetPosition(0, Vector2.zero);
            enableLaser();
            isShootingLasers = true;
            yield return new WaitForSeconds(durationOfShoot);
            disableLaser();
            laserBeam.GetComponent<LineRenderer>().SetPosition(1, Vector2.zero);
            
            isShootingLasers = false;
        }   
    }

    protected override IEnumerator CanShoot()
    {
        if (isShooting && isreadyToAttack && !isPerturbateurIEM)
        {   
            isreadyToAttack = false;
            StartCoroutine(ShootLasersCo());
            yield return new WaitForSeconds(restTime);
            isreadyToAttack = true;
        }
    }


    protected void enableLaser()
    {
        laserBeam.GetComponent<LineRenderer>().enabled = true;
    }

    protected void disableLaser()
    {
        laserBeam.GetComponent<LineRenderer>().enabled = false;
    }
}

