using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDistance : Distance
{

    void Start()
    {
        // Set premier targetPoint
        SetFirstPatrolPoint();
        // Set data
        SetData();
        // Vie initial
        SetMaxHealth();
    }
    private void Update()
    {
        // récupération de l'aggro
        Aggro();
        // script de patrol
        Patrol();
        // suit le path créé et s'arrête pour tirer
        if(!isShooting ) MoveToPath();
        // Couroutine gérant les shoots 
        StartCoroutine("CanShoot");

    }

    protected override void SetData()
    {
        base.SetData();
    }

    // Mouvement

    // Override(Enemy.cs) Aggro s'arrete pour tirer et suit le player si plus à distance
    protected override void Aggro()
    {


        if (Vector3.Distance(transform.position, target.position) < inSight)
        {
            // Stop patrouiller
            isPatroling = false;
            // Target = player
            targetPoint = target;
            // Stop to shoot
            rb.velocity = Vector2.zero;
            // Entrain de shoot
            isShooting = true;
        }
        else
        {
            // S'arrete de tirer popur suivre le joueur
            isShooting = false;

        }
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
    protected override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

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
    }

}
