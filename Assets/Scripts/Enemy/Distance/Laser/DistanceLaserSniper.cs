using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class DistanceLaserSniper : Distance
{
    [SerializeField] protected RafaleDistanceData RafaleDistanceData;

    private float timeIntervale;
    private int nbTir;
    [SerializeField] protected float delayMovement;

    private int n = 0; //compteur pour le while

    protected override void SetData()
    {
        base.SetData();
        timeIntervale = RafaleDistanceData.timeIntervale;
        nbTir = RafaleDistanceData.nbTir;
    }




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

    protected virtual IEnumerator intervalleTir()
    {

        while (n < nbTir)
        {
            Shoot();
            rb.velocity = Vector2.zero;
            currentState = State.ShootingLaser;
            yield return new WaitForSeconds(timeIntervale);
            currentState =State.Attacking;
            n++;
        }
        n = 0;
        StartCoroutine(MovementDelay());
    }

    protected IEnumerator MovementDelay()
    {
        rb.velocity = Vector2.zero;
        currentState = State.ShootingLaser;
        yield return new WaitForSeconds(delayMovement);
        currentState = State.Chasing;

    }

    protected override void Shoot()
    {
        GameObject myproj = Instantiate(projetile, transform.position, Quaternion.identity);
        myproj.transform.parent = gameObject.transform;
        StartCoroutine(MovementDelay());

    }
}
