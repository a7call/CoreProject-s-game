using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : DistanceWeapon
{
    [SerializeField] float LoadingDelay;



    protected override IEnumerator Shoot()
    {
        if (!isAttacking)
        {
            yield return new WaitForSeconds(LoadingDelay);
            isAttacking = true;
            Instantiate(projectile, attackPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(attackDelay);
            isAttacking = false;
        }

    }


}
