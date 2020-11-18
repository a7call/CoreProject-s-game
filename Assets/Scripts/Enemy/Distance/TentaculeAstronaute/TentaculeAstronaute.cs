using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe héritière de distance 
/// Contient en plus de classe distance une coroutine de projectile (spécial) => voir EggProjectile.cs
/// </summary>
public class TentaculeAstronaute : Distance
{

    void Start()
    {
        currentState = State.Patrolling;
        // Set premier targetPoint
        SetFirstPatrolPoint();
        // Set data
        SetData();
        SetMaxHealth();
        
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
                // suit le path créé et s'arrête pour tirer
                MoveToPath();
                break;
            case State.Attacking:
                isInRange();
                // Couroutine gérant les shoots 
                StartCoroutine("CanShoot");
                break;
        }
    }

    // Couroutine du shoot
    protected override IEnumerator CanShoot()
    {
        if (isShooting && isReadytoShoot)
        {
            // Ne peut plus tirer car déjà entrain de tirer
            isReadytoShoot = false;
            // Tire
            Shoot();
            // Repos entre deux tires
            yield return new WaitForSeconds(restTime);
            // Peut tirer de nouveau
            isReadytoShoot = true;
        }
    }


    // Voir Distance.cs (héritage)
    protected override void Shoot()
    {
        base.Shoot();
    }


}
