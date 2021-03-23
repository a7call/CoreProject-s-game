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
            case State.Chasing:
                isInRange();
                ChargingMode();
                // suit le path créé et s'arrête pour tirer
                break;
            case State.Attacking:
                isInRange();
                // Couroutine gérant les shoots 
                StartCoroutine(CanShootCO());
                break;
            case State.Charging:
                // ChargeToAttack();
                StartCoroutine(ChargingCoroutine());
                break;
        }

    }
    // Voir Enemy.cs (héritage)
    protected override IEnumerator ShootCO()
    {
        float decalage = angleTir / (projectiles.Length - 1);
        AngleProjectile.angleDecalage = -decalage * (projectiles.Length + 1) / 2;

        //base.Shoot();
        for (int i = 0; i < projectiles.Length; i++)
        {
            AngleProjectile.angleDecalage = AngleProjectile.angleDecalage + decalage;
            GameObject myProjectile = Instantiate(projectiles[i], attackPoint.position, Quaternion.identity);
            myProjectile.transform.parent = gameObject.transform;
        }
        yield return new WaitForEndOfFrame();
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
            currentState = State.Charging;
        }
    }

    private void GetProjectile()
    {
        foreach (GameObject projectile in projectiles)
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
    private float chargeTime = 4f;
    // Temps d'attente avant la première charge
    private float delayToFirstCharge = 2f;
    // Temps pendant lequel le personnage ne bouge pas
    private float waitingTime = 1f;

    [SerializeField] private bool canCharge = false;
    [SerializeField] private bool isCharging = false;

    private IEnumerator ChargeLoading()
    {
        yield return new WaitForSeconds(1);
        canCharge = true;
    }
    [SerializeField] 
    float newSlowDownDistance = 0.3f,  newEndReachedDistance = 0.2f,  newMoveSpeed = 6;

    /// <summary>
    /// Faire la charge s'arreter à quand il touche un truc 
    /// Faire la Charge en ligne droite
    /// Faire le script de collision avec joueur / mur
    /// </summary>

    private IEnumerator ChargingCoroutine()
    {
        if (canCharge && !isCharging)
        {
            float initialMoveSpeed = aIPath.maxSpeed;
            float initialSlowDownDistance = aIPath.slowdownDistance;
            float initialEndReachedDistance = aIPath.endReachedDistance;
            
            InitiateChargeMode(newSlowDownDistance, newEndReachedDistance, newMoveSpeed);
            yield return new WaitForSeconds(waitingTime);
            aIPath.canMove = true;
            yield return new WaitForSeconds(chargeTime);
            EndChargeMode(initialSlowDownDistance, initialEndReachedDistance, initialMoveSpeed);
            yield return new WaitForSeconds(chargeDelay);
            canCharge = true;
        }
    }
    void InitiateChargeMode(float newSlowDownDistance, float newEndReachedDistance, float newMoveSpeed)
    {
        canCharge = false;
        aIPath.canMove = false;
        isCharging = true;

        // new target for Charge
        GameObject ChargePoint = new GameObject();
        ChargePoint.transform.position  = target.position;
        // Get dir of charge
        Vector2 ChargeDir = (ChargePoint.transform.position - transform.position).normalized;
        // Set new Charge point (far away in the direction)
        ChargePoint.transform.position = ChargeDir * 20;
        targetSetter.target = ChargePoint.transform;

        // Dont slow down or stop unless touch player
        aIPath.slowdownDistance = newSlowDownDistance;
        aIPath.endReachedDistance = newEndReachedDistance;
        // Set new moveSpeed
        aIPath.maxSpeed = newMoveSpeed;
        isCharging = true;
    }

    void EndChargeMode(float initialSlowDownDistance, float initialEndReachedDistance, float initialMoveSpeed)
    {
        aIPath.slowdownDistance = initialSlowDownDistance;
        aIPath.endReachedDistance = initialEndReachedDistance;
        aIPath.maxSpeed = initialMoveSpeed;
        targetSetter.target = target;
        isCharging = false;
        currentState = State.Chasing;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // SCRIPT DE COLLISION
    }
}
