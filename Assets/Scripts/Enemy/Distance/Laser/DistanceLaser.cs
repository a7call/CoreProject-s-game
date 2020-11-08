using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class DistanceLaser : Distance
{
    protected Vector3 dir;
    [SerializeField] protected float delayMovement;
    public Laser Laser;
    


    void Start()
    {
        currentState = State.Patrolling;
        // Set premier targetPoint
        SetFirstPatrolPoint();
        // Set data
        SetData();
        SetMaxHealth();
        Laser.EnnemyAlive = true;

    }

   
    protected override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case State.Patrolling:
                // script de patrol
                Patrol();
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

            case State.ShootingLaser:
                break;
        }


       

    }

    protected override void SetData()
    {
        base.SetData();
    }

    public override void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        StartCoroutine(WhiteFlash());
        if (currentHealth < 1)
        {
            Laser.EnnemyAlive = false;
            Destroy(gameObject);
        }
    }

    // Mouvement

    // Override(Enemy.cs) Aggro s'arrete pour tirer et suit le player si plus à distance
    protected override void Aggro()
    {
            targetPoint = target;
    }

    protected override void PlayerInSight()
    {
        base.PlayerInSight();
    }

    protected override void isInRange()
    {
        base.isInRange();
    }

    // Voir Enemy.cs (héritage)
    protected override void Patrol()
    {
        base.Patrol();
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
    protected override IEnumerator WhiteFlash()
    {
        return base.WhiteFlash();
    }


    // Attack

    

    // Voir Enemy.cs (héritage)
    protected override IEnumerator CanShoot()
    {
        return base.CanShoot();
    }

    // Voir Enemy.cs (héritage)
    protected override void ResetAggro()
    {
        base.ResetAggro();
    }

    // Voir Enemy.cs (héritage)
    protected override void Shoot()
    {
        base.Shoot();
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
