using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class DistanceLaserDecalage : Distance
{
    protected Vector3 dir;
    [SerializeField] protected float delayBeforeShoot;
    [SerializeField] GameObject[] projectiles;
    [SerializeField] int angleTir;
    public LaserDecalage LaserDecalage;
    [SerializeField] protected float delayMovement;

    void Start()
    {
        currentState = State.Patrolling;
        // Set premier targetPoint
        SetFirstPatrolPoint();
        // Set data
        SetData();
        SetMaxHealth();

    }

    protected virtual void GetDirection()
    {
        dir = (target.position - transform.position).normalized;
    }
    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case State.Patrolling:
                PlayerInSight();
                MoveToPath();
                break;
            case State.Chasing:
                // récupération de l'aggro
                Aggro();
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


        Debug.DrawRay(transform.position, dir * 100, Color.red);

    }

    // Voir Enemy.cs (héritage)
    protected override void Shoot()
    {
        float decalage = angleTir / (projectiles.Length - 1);
        LaserDecalage.angleDecalage = -decalage * (projectiles.Length + 1) / 2;
        
        //base.Shoot();
        for (int i = 0; i < projectiles.Length; i++)
        {
            LaserDecalage.angleDecalage = LaserDecalage.angleDecalage + decalage;
            GameObject.Instantiate(projectiles[i], transform.position, Quaternion.identity);
        }
        StartCoroutine(MovementDelay());
    }

    protected IEnumerator MovementDelay()
    {
        rb.velocity = Vector2.zero;
        currentState = State.ShootingLaser;
        yield return new WaitForSeconds(delayMovement);
        currentState = State.Chasing;

    }
}
