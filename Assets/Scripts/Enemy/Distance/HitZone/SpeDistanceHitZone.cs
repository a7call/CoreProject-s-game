using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe héritière de distance 
/// Contient en plus de classe distance une coroutine de projectile (spécial) => voir HitZoneProjectile.cs
/// </summary>
public class SpeDistanceHitZone : Distance
{

    void Start()
    {
        currentState = State.Patrolling;
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
                break;
            case State.Attacking:
                isInRange();
                // Couroutine gérant les shoots 
                StartCoroutine("CanShoot");
                break;
        }
    }


    // Projectile spé
    [SerializeField] protected GameObject HitZoneProjectiles;
    // Check si attaque spé rdy
    private bool isSpeRdy = true;
    // Time entre deux attaque spé
    [SerializeField] protected float reloadSpe;

    // Couroutine du shoot
    protected override IEnumerator CanShoot()
    {
        if (isShooting && isReadytoShoot && !isSpeRdy)
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

        else if (isSpeRdy && isShooting && isReadytoShoot)
        {
            // Ne peut plus tirer car déjà entrain de tirer spé + normal
            isSpeRdy = false;
            isReadytoShoot = false;
            // Shoot spé
            Zone();
            // Repos entre deux tire
            yield return new WaitForSeconds(restTime);
            // Peut tirer normalement
            isReadytoShoot = true;
            // Reload attaque spé
            yield return new WaitForSeconds(reloadSpe);
            // attaque spé rdy
            isSpeRdy = true;
        }
    }

    // Instantiate projectile spé
    protected void Zone()
    {
        GameObject.Instantiate(HitZoneProjectiles, transform.position, Quaternion.identity);
    }


}
