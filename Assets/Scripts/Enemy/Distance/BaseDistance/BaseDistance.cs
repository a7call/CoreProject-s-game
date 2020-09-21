using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDistance : Distance
{

    private Vector3 dir;

    void Start()
    {
        SetFirstPatrolPoint();
        SetData();
        SetMaxHealth();
    }
    private void Update()
    {
        Aggro();
        Patrol();
        StartCoroutine("CanShoot");
    }

    protected override void SetData()
    {
        base.SetData();
    }

    // Mouvement
    protected override void Aggro()
    {
        dir = (targetToFollow.position - transform.position).normalized;

        if (Vector3.Distance(transform.position, targetToFollow.position) < aggroDistance)
        {
            isPatroling = false;
            rb.velocity = dir * moveSpeed * Time.fixedDeltaTime;
            rb.velocity = Vector2.zero;
            isShooting = true;
        }
        else
        {
            rb.velocity = dir * moveSpeed * Time.fixedDeltaTime;
            isShooting = false;

        }
    }


    protected override void Patrol()
    {
        base.Patrol();
    }

    protected override void SetFirstPatrolPoint()
    {
        base.SetFirstPatrolPoint();
    }

 

    // Health

    protected override void SetMaxHealth()
    {
        base.SetMaxHealth();
    }

    protected override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

    }

    protected override IEnumerator WhiteFlash()
    {
        return base.WhiteFlash();
    }


    // Attack

    protected override IEnumerator CanShoot()
    {
        return base.CanShoot();
    }

    protected override void ResetAggro()
    {
        base.ResetAggro();
    }


    protected override void Shoot()
    {
        base.Shoot();
    }

}
