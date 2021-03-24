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
                StartCoroutine(ChargingCoroutine());
                break;
        }
        if(isCharging)
        {
            currentState = State.Charging;
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
    /// Il peut charger toutes les x secondes
    /// La première charge doit être réalisée quelques secondes après le pop du mob


    [SerializeField] private float chargeDelay = 5f, chargeTime = 3f, delayToFirstCharge = 2f, waitingTime = 1f;
    [SerializeField] float newSlowDownDistance = 0.3f, newEndReachedDistance = 0.2f, newMoveSpeed = 6, newNextWayPointDist = 20, chargeDistance = 20;
    float initialMoveSpeed, initialSlowDownDistance, initialEndReachedDistance, initialNextWayPointDist;
    GameObject ChargePoint;

    private bool canCharge = false;
    [SerializeField] private bool isCharging = false;

    private void ChargingMode()
    {
        if (canCharge)
        {
            currentState = State.Charging;
        }
    }
    private IEnumerator ChargeLoading()
    {
        yield return new WaitForSeconds(1);
        canCharge = true;
    }

    private IEnumerator ChargingCoroutine()
    {
        if (canCharge)
        {
            canCharge = false;
            isCharging = true;
            //Get Initial pathfinding parameters
            initialMoveSpeed = aIPath.maxSpeed;
            initialSlowDownDistance = aIPath.slowdownDistance;
            initialEndReachedDistance = aIPath.endReachedDistance;
            initialNextWayPointDist = aIPath.pickNextWaypointDist;
            // Init ChargePoint
            ChargePoint = new GameObject();
            //Init all variable for charge
            InitiateChargeMode(newSlowDownDistance, newEndReachedDistance, newMoveSpeed, newNextWayPointDist, ChargePoint, chargeDistance);
            yield return new WaitForSeconds(waitingTime);
            // CHARGE !!!!
            aIPath.canMove = true;
            yield return new WaitForSeconds(chargeTime);
            //kill chargeMode, go back to normal
            EndChargeMode(initialSlowDownDistance, initialEndReachedDistance, initialMoveSpeed, initialNextWayPointDist, ChargePoint);
            //CD before next charge
            yield return new WaitForSeconds(chargeDelay);
            isCharging = false;
            currentState = State.Chasing;
            canCharge = true;
        }
    }
    void InitiateChargeMode( float _newSlowDownDistance, float _newEndReachedDistance, float _newMoveSpeed, float _newNextWayPointDist, GameObject _ChargePoint, float _chargeDistance)
    {
        //Set up pathfinfing variable
        aIPath.canMove = false;
        aIPath.pickNextWaypointDist = _newNextWayPointDist;
        aIPath.slowdownDistance = _newSlowDownDistance;
        aIPath.endReachedDistance = _newEndReachedDistance;
        aIPath.maxSpeed = _newMoveSpeed;
        
        // Get dir of charge
        Vector3 DirectionCharge  = target.position - transform.position;
        _ChargePoint.transform.position = transform.TransformPoint(DirectionCharge * _chargeDistance);
        targetSetter.target = _ChargePoint.transform;

    }

    void EndChargeMode(float _initialSlowDownDistance, float _initialEndReachedDistance, float _initialMoveSpeed, float _initialNextWayPointDist , GameObject _ChargePoint)
    {
        aIPath.slowdownDistance = _initialSlowDownDistance;
        aIPath.endReachedDistance = _initialEndReachedDistance;
        aIPath.maxSpeed = _initialMoveSpeed;
        aIPath.pickNextWaypointDist = _initialNextWayPointDist;
        targetSetter.target = target;
        isCharging = false;
        currentState = State.Chasing;
        Destroy(_ChargePoint);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
            }
           
                StopCoroutine(ChargingCoroutine());
                EndChargeMode(initialSlowDownDistance, initialEndReachedDistance, initialMoveSpeed, initialNextWayPointDist, ChargePoint);
          
    }
}
