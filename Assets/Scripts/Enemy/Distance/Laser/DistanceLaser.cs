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
    public bool isEnemyAlive;



    void Start()
    {
        isEnemyAlive = true;
        currentState = State.Patrolling;
        // Set premier targetPoint
        SetFirstPatrolPoint();
        // Set data
        SetData();
        SetMaxHealth();
        //isEnemyAlive = true;

    }

   
    protected override void Update()
    {
        //Debug.Log(isEnemyAlive);

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

            case State.ShootingLaser:
                break;
        }


       

    }


 

    // Voir Enemy.cs (héritage)
    protected override void Shoot()
    {
        GameObject myproj = Instantiate(projetile, transform.position, Quaternion.identity);
        myproj.transform.parent = gameObject.transform;
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
