using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Classe héritière de Distance.cs
/// Elle contient les fonctions de la classe mère
/// </summary>
public class PompeDistanceEnemy : Distance
{

    
    [SerializeField] GameObject[] projectiles;
    [SerializeField] int angleTir;
    public AngleProjectile AngleProjectile ;

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

    }

    // Voir Enemy.cs (héritage)
    protected override void Shoot()
    {
        float decalage = angleTir / (projectiles.Length - 1);
        AngleProjectile.angleDecalage = - decalage * (projectiles.Length + 1) / 2;

        //base.Shoot();
        for(int i=0; i <projectiles.Length; i++)
            {
            AngleProjectile.angleDecalage = AngleProjectile.angleDecalage + decalage;
                GameObject.Instantiate(projectiles[i], transform.position, Quaternion.identity);
            }

    }

}
