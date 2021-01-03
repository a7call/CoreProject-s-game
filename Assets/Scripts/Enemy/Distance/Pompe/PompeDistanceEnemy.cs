using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class PompeDistanceEnemy : Distance
{
    [SerializeField] GameObject[] projectiles = null;
    [SerializeField] int angleTir = 0;
    private AngleProjectile AngleProjectile;

    void Start()
    {
        GetProjectile();
        currentState = State.Chasing;
        // Set data
        SetData();
        SetMaxHealth();
        StartCoroutine(ChargeLoading());
    }

    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case State.Patrolling:
                PlayerInSight();
                break;
            case State.Chasing:
                isInRange();
                ChargingMode();
                // suit le path créé et s'arrête pour tirer
                break;
            case State.Attacking:
                isInRange();
                // Couroutine gérant les shoots 
                StartCoroutine("CanShoot");
                break;
            case State.Charging:
                ChargeToAttack();
                StartCoroutine(ChargingCoroutine());
                break;
        }

    }
    // Voir Enemy.cs (héritage)
    protected override void Shoot()
    {
        float decalage = angleTir / (projectiles.Length - 1);
        AngleProjectile.angleDecalage = - decalage * (projectiles.Length + 1) / 2;

        //base.Shoot();
        for(int i=0; i <projectiles.Length; i++)
            {
                AngleProjectile.angleDecalage = AngleProjectile.angleDecalage + decalage;
                GameObject myProjectile = GameObject.Instantiate(projectiles[i], transform.position, Quaternion.identity);
                myProjectile.transform.parent = gameObject.transform;
        }

    }

    private void ChargeToAttack()
    {
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            currentState = State.Attacking;
            // A changer par la suite
            aIPath.maxSpeed = 2f;
        }
    }

    private void ChargingMode()
    {
        if (canCharge)
        {
            currentState = Enemy.State.Charging;
        }
    }

    private void GetProjectile()
    {
        foreach(GameObject projectile in projectiles)
        {
           AngleProjectile = projectile.GetComponent<AngleProjectile>();
        }
    }

    /// Fonctionnement du charging
    /// S'arrête quelques secondes
    /// Accélère lorsqu'il charge
    /// Charge et s'arrête légèrement avant l'ennemi
    /// Passe en state attacking car il est en range
    /// Il peut charger toutes les x secondes
    /// La première charge doit être réalisée quelques secondes après le pop du mob
    
   // Délai entre deux charges
    [SerializeField] private float chargeDelay = 10f;
    // Temps de la charge
    private float chargeTime = 1f;
    // Temps d'attente avant la première charge
    private float delayToFirstCharge = 2f;
    // Temps pendant lequel le personnage ne bouge pas
    private float waitingTime = 1f;

    [SerializeField] private bool canCharge = false;
    [SerializeField] private bool isCharging = false;

    private IEnumerator ChargeLoading()
    {
        yield return new WaitForSeconds(delayToFirstCharge);
        canCharge = true;
    }


    private IEnumerator ChargingCoroutine()
    {
        if(canCharge && !isCharging)
        {
            canCharge = false;
            aIPath.canMove = false;
            isCharging = true;
            float initialMoveSpeed = aIPath.maxSpeed;
            yield return new WaitForSeconds(waitingTime);
            aIPath.maxSpeed = 10f;
            aIPath.canMove = true;
            yield return new WaitForSeconds(chargeTime);
            isCharging = false;
            aIPath.maxSpeed = initialMoveSpeed;
            currentState = Enemy.State.Chasing;
            yield return new WaitForSeconds(chargeDelay);
            canCharge = true;
        }
        else
        {
            yield return null;
        }
    }

}
