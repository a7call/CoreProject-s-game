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



    void Start()
    {
        currentState = State.Chasing;
        // Set data
        SetData();
        SetMaxHealth();
    }

   
    protected override void Update()
    {
        //Debug.Log(isEnemyAlive);

        base.Update();

        switch (currentState)
        {
            case State.Patrolling:
                break;
            case State.Chasing:
                isInRange();
                // suit le path créé et s'arrête pour tirer
                break;
            case State.Attacking:
                isInRange();
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

    }

}
