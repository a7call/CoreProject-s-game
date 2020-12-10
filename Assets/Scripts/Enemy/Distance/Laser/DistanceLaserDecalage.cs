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
    [SerializeField] protected float delayBeforeShoot = 0f;
    [SerializeField] GameObject[] projectiles = null;
    [SerializeField] int angleTir = 0;
    private LaserDecalage LaserDecalage;
    [SerializeField] protected float delayMovement = 0f;

    void Start()
    {

        GetProj();
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
                break;
            case State.Chasing:
                isInRange();
                // suit le path créé et s'arrête pour tirer
                break;
            case State.Attacking:
                isInRange();
                DontMoveShooting();
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
            GameObject myproj = Instantiate(projectiles[i], transform.position, Quaternion.identity);
            myproj.transform.parent = gameObject.transform;
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

    private void GetProj()
    {
        foreach (GameObject projectile in projectiles)
        {
            LaserDecalage = projectile.GetComponent<LaserDecalage>();
        }
    }
}
