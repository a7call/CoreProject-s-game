using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDistance : Distance
{

    void Start()
    {
        SetFirstPatrolPoint();
        SetData();
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
        // Couroutine gérant les shoots (à modifier)
        StartCoroutine("CanShoot");

    }

    protected override void SetData()
    {
        base.SetData();
    }

    // Mouvement
    protected override void Aggro()
    {


        if (Vector3.Distance(transform.position, target.position) < inSight)
        {
            isPatroling = false;
            targetPoint = target;
            rb.velocity = Vector2.zero;
            isShooting = true;
        }
        else
        {
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
