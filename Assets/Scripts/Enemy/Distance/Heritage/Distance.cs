using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : Enemy
{
    // Scriptable Object
    [SerializeField] protected Type2ScriptableObject Type2Data;
    // Check si tire
    public bool isShooting;


    protected virtual void SetData()
    {
        moveSpeed = Type2Data.moveSpeed;
        inSight = Type2Data.aggroDistance;


        maxHealth = Type2Data.maxHealth;
        whiteMat = Type2Data.whiteMat;
        defaultMat = Type2Data.defaultMat;


        restTime = Type2Data.restTime;
        projetile = Type2Data.projetile;
    }


    //Mouvement


    // Aggro s'arrete pour tirer et ne bouge pas (à modifier) retourne patrouiller si plus à distance
    protected override void Aggro()
    {
        if (Vector3.Distance(transform.position, target.position) < inSight)
        {
            // l'ennemi commence à tirer
            isShooting = true;
            // l'ennemi ne patrouille plus 
            isPatroling = false;
            // set the target to player
            targetPoint = target;
            // ne bouge plus
            rb.velocity = Vector2.zero;
            

        }
        else
        {
            // retourne patrouiller
            isPatroling = true;
            // ne tire plus
            isShooting = false;
            return;
        }
    }


    // Voir Enemy.cs (héritage)
    protected override void SetFirstPatrolPoint()
    {
        base.SetFirstPatrolPoint();
    }



    // Health

    // Voir Enemy.cs (héritage)
    protected override void SetMaxHealth()
    {
        base.SetMaxHealth();
    }

    // Voir Enemy.cs (héritage)
    protected override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    // Voir Enemy.cs (héritage)
    protected override IEnumerator WhiteFlash()
    {
        return base.WhiteFlash();
    }



    // Attack


    // Check si prêt à tirer
    protected bool isReadytoShoot = true;
    // Repos après tire
    protected float restTime;
    // Projectile to instantiate
    protected GameObject projetile;


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


    // Reset la couroutine CanShoot ( Non ultilisé)
    protected virtual void ResetAggro()
    {
        if (isShooting)
        {
            StopCoroutine("CanShoot");
            isReadytoShoot = true;
        }
    }


}
