using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Bubble360 : Distance
{
    [SerializeField] protected GameObject rayon;

    [SerializeField] private List<GameObject> differentRadius = new List<GameObject>();

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
                break;
            case State.Attacking:
                isInRange();
                DontMoveShooting();
                StartCoroutine(CanShoot());
                break;
        }
    }

   


    //Voir Enemy.cs(héritage)
    protected override IEnumerator CanShoot()
    {
        if (isReadytoShoot)
        {
            isReadytoShoot = false;
            Shoot();
            yield return new WaitForSeconds(restTime);
            isReadytoShoot = true;
        }
    }

    // Voir Enemy.cs (héritage)
    protected override void Shoot()
    {
        Instantiate(rayon, transform.position, Quaternion.identity);
        AddShoot();
    }

    private void AddShoot()                     
    {
        differentRadius.Insert(0,rayon);
    }
}
