using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : Enemy
{
    [SerializeField] protected Type2ScriptableObject Type2Data;
    public bool isShooting;


    protected virtual void SetData()
    {
        moveSpeed = Type2Data.moveSpeed;
        aggroDistance = Type2Data.aggroDistance;


        maxHealth = Type2Data.maxHealth;
        whiteMat = Type2Data.whiteMat;
        defaultMat = Type2Data.defaultMat;


        restTime = Type2Data.restTime;
        projetile = Type2Data.projetile;
    }


    //Mouvement


    // Aggro mais ne bouge pas et met à jour l'état de l'énemi
    protected override void Aggro()
    {
        if (Vector3.Distance(transform.position, targetToFollow.position) < aggroDistance)
        {
            isPatroling = false;
            rb.velocity = Vector2.zero;
            isShooting = true;

        }
        else
        {
            isPatroling = true;
            isShooting = false;
            return;
        }
    }

   

    protected override void SetFirstPatrolPoint()
    {
        base.SetFirstPatrolPoint();
    }



    // Health


    protected override void SetMaxHealth()
    {
        base.SetMaxHealth();
    }

    protected override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    protected override IEnumerator WhiteFlash()
    {
        return base.WhiteFlash();
    }



    // Attack

    protected bool isReadytoShoot = true;
    protected float restTime;
    protected GameObject projetile;

    // Ref to Type2Mouvement + base 

    protected virtual IEnumerator CanShoot()
    {
        if (isShooting && isReadytoShoot)
        {
            isReadytoShoot = false;
            Shoot();
            yield return new WaitForSeconds(restTime);
            isReadytoShoot = true;
        }
    }
    // Instansiate projectiles
    protected virtual void Shoot()
    {
        GameObject.Instantiate(projetile, transform.position, Quaternion.identity);
    }


    // Reset la couroutine CanShoot
    protected virtual void ResetAggro()
    {
        if (isShooting)
        {
            StopCoroutine("CanShoot");
            isReadytoShoot = true;
        }
    }


}
