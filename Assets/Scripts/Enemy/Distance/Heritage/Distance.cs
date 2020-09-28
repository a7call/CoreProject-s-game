using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe mère des distances et héritière de Enemy.cs
/// Elle contient une focntion setData permettant de récupérer les données du scriptable object 
/// Une focntion aggro permettant de commencer à suivre l'ennemi si le joueur est à porté
/// Une fonction permettant de savoir si le joueur est en range de shoot
/// Une coroutine de shoot
/// Une focntiond de shoot qui instansiate le projectile
/// </summary>
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
        attackRange = Type2Data.attackRange;
    }


    //Mouvement


    // Aggro s'arrete pour tirer et ne bouge pas (à modifier) retourne patrouiller si plus à distance
    protected override void Aggro()
    {
        if (Vector3.Distance(transform.position, target.position) < inSight)
        {
            targetPoint = target;
        }
        else
        {
            // retourne patrouiller
            currentState = State.Patrolling;
            return;
        }
    }

    protected override void PlayerInSight()
    {
        if (Vector3.Distance(transform.position, target.position) < inSight) 
            currentState = State.Chasing;
    }

    protected virtual void isInRange()
    {
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            currentState = State.Attacking;
            isShooting = true;
            rb.velocity = Vector2.zero;
        }
        else
        {
            currentState = State.Chasing;
            isShooting = false;
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
    protected float attackRange;


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
