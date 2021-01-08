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
    protected float timeBeforeShoot = 0.5f;
    protected float durationOfShoot = 1f;
    protected int damage = 1;
    protected bool isShootingLasers;



    void Start()
    {
        currentState = State.Chasing;
        // Set data
        SetData();
        SetMaxHealth();
    }

   
    protected override void Update()
    {
        //Debug.Log(isEnemyAlive);
        
        base.Update();

        switch (currentState)
        {
            case State.Patrolling:
                break;
            case State.Chasing:
                isInRange();
                // suit le path créé et s'arrête pour tirer
                break;
            case State.Attacking:
                
                isInRange();
                StartCoroutine("CanShoot");
                Shoot();
                break;
        }


       

    }


 

    // Voir Enemy.cs (héritage)
    protected override void Shoot()
    {
             
        if(isShootingLasers)
        { 
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, Mathf.Infinity);

            Debug.DrawRay(transform.position, dir*10, Color.red);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    PlayerHealth player = hit.transform.gameObject.GetComponent<PlayerHealth>();
                    player.TakeDamage(damage);
                }
            }
        }
       
    }


    protected virtual IEnumerator ShootLasersCo()
    {
        if(!isShootingLasers && isreadyToAttack)
        {

            dir = (targetSetter.target.position - transform.position).normalized;
            yield return new WaitForSeconds(timeBeforeShoot);
            isShootingLasers = true;
            yield return new WaitForSeconds(durationOfShoot);
            isShootingLasers = false;
        }   
    }

    protected override IEnumerator CanShoot()
    {
        if (isShooting && isreadyToAttack && !isPerturbateurIEM)
        {
            StartCoroutine(ShootLasersCo());
            isreadyToAttack = false;
            yield return new WaitForSeconds(restTime);
            isreadyToAttack = true;
        }
    }
}
