﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : CollingWeapons
{

    Vector3 dir;
    [SerializeField] protected float range;

    protected override void Update()
    {
        base.Update();
        if (!isAttacking && !IsCooling)
        {
            CoroutineManager.Instance.StartCoroutine(CoolDelay());
        }
        if (OkToShoot && !IsToHot)
        {
            dir = (attackPoint.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, dir, range, enemyLayer);

            Debug.DrawRay(attackPoint.position, dir * range, Color.red);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy")) 
            {
                Enemy enemyScript = hit.collider.gameObject.GetComponent<Enemy>();
                CoroutineManager.Instance.StartCoroutine(enemyScript.KnockCo(knockBackforce, dir, knockBackTime, enemyScript));
                enemyScript.GetComponent<Enemy>().TakeDamage(damage);
            }


        }
    }
    protected override IEnumerator Shoot()
    {
        if (!isAttacking && !IsToHot)
        {
            isAttacking = true;
            count++;
            yield return new WaitForSeconds(1f);
            isAttacking = false;
        }
        if (count >= countMax)
        {
            IsToHot = true;
            CoroutineManager.Instance.StartCoroutine(LaserCooling());
        }
       
    }

    protected IEnumerator LaserCooling()
    {
        yield return new WaitForSeconds(coolingTime);
        IsToHot = false;
        count = 0;
    }

    protected IEnumerator CoolDelay()
    {
        if (count > 0)
        {
            IsCooling = true;
            yield return new WaitForSeconds(coolingDelay);
            count--;
            IsCooling = false;
        }
        
    }



}
