using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : DistanceWeapon
{
    //[SerializeField] float LoadingDelay;
    protected int count;
    protected bool IsToHot = false;
    protected bool IsCooling = false;
   [SerializeField] protected float coolingTime;
   [SerializeField] protected float coolingDelay;
   [SerializeField] protected int countMax;
    


    protected override void Update()
    {
        base.Update();
        if (!isAttacking && !IsCooling)
        {
            StartCoroutine(CoolDelay());
        }

    }


    protected override IEnumerator Shoot()
    {
        
        if (!isAttacking && !IsToHot)
        {
            //yield return new WaitForSeconds(LoadingDelay);
            isAttacking = true;
            Instantiate(projectile, attackPoint.position, Quaternion.identity);
            count++;
            yield return new WaitForSeconds(attackDelay);
            isAttacking = false;
            if (count >= countMax)
            {
                IsToHot = true;
                StartCoroutine(LaserCooling());
            }
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
