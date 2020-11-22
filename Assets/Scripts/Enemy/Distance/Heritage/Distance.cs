using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe mère des Distance et héritière de Enemy.cs
/// Elle contient une fonction setData permettant de récupérer les données du scriptable object 
/// Une fonction aggro permettant de commencer à suivre l'ennemi si le joueur est à porté
/// Une fonction permettant de savoir si le joueur est en range de shoot
/// Une coroutine de shoot
/// Une fonction de shoot qui instansiate le projectile
/// </summary>
public class Distance : Enemy
{
    // Scriptable Object
    [SerializeField] protected DistanceScriptableObject DistanceData;
    // Check si tire
    [HideInInspector]
    public bool isShooting;
 


    protected virtual void SetData()
    {
        moveSpeed = DistanceData.moveSpeed;
        inSight = DistanceData.aggroDistance;


        maxHealth = DistanceData.maxHealth;
        whiteMat = DistanceData.whiteMat;
        defaultMat = DistanceData.defaultMat;


        restTime = DistanceData.restTime;
        projetile = DistanceData.projetile;
        attackRange = DistanceData.attackRange;
        timeToSwitch = DistanceData.timeToSwich;
    }

    protected override void isInRange()
    {
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            currentState = State.Attacking;
            isShooting = true;
            isReadyToSwitchState = false;
        }
        else
        {
            if (currentState == State.Attacking && !isInTransition) StartCoroutine(transiChasing());
            if (isReadyToSwitchState)
            {
                currentState = State.Chasing;
                isShooting = false;
            }
           
        }
    }

    
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
        
        Instantiate(projetile, transform.position, Quaternion.identity);
        
    }

    protected void DontMoveShooting()
    {
        rb.velocity = Vector2.zero;
    }

}
