using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleOneWave : Distance
{


    [SerializeField] int angleTir;

    void Start()
    {
        currentState = State.Chasing;
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

    protected override void SetData()
    {
        base.SetData();
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

    }

}

