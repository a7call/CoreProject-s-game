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
    protected bool isShootingLasers; // à remplacer
    protected GameObject laserBeam;

    protected override void Awake()
    {
        base.Awake();
        SetData();
        SetMaxHealth();
    }

    void Start()
    {   
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
                isInRange();
                PlayAttackAnim();
               // StartCoroutine(CanShootCO());
                LaserFiring();
                break;
        }
        ShouldNotMoveDuringShooting();
    }


    // Voir Enemy.cs (héritage)
    public LayerMask testsLayer; // à remplacer !
    protected  void LaserFiring()
    {
             
        if(isShooting)
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


    protected virtual IEnumerator ShootCo()
    {
            dir = (targetSetter.target.position - transform.position).normalized;
            yield return new WaitForSeconds(timeBeforeShoot);
            laserBeam.GetComponent<LineRenderer>().SetPosition(0, Vector2.zero);
            enableLaser();
            isShooting = true;
            yield return new WaitForSeconds(durationOfShoot);
            disableLaser();
            laserBeam.GetComponent<LineRenderer>().SetPosition(1, Vector2.zero);
            isShooting = false;     
    }

    protected override IEnumerator CanShootCO()
    {
            
            yield return StartCoroutine(ShootCo());
            yield return new WaitForSeconds(restTime);
            isReadytoShoot = true;
    }
    void PlayAttackAnim()
    {
        
        if (isReadytoShoot && !isPerturbateurIEM)
        {
            isReadytoShoot = false;
            animator.SetTrigger("isAttacking");
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

