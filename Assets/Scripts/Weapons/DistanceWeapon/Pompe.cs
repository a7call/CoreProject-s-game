using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pompe : DistanceWeapon
{
   
    [SerializeField] GameObject[] projectiles = null;
    [SerializeField] int angleTir = 0;
    [SerializeField] PompeProjectiles angleProjectile; // à corriger 

   


    protected override IEnumerator Shoot()
    {

        if (!isAttacking && BulletInMag > 0 && !IsReloading)
        {
            isAttacking = true;
            float decalage = angleTir / (projectiles.Length - 1);
            angleProjectile.angleDecalage = -decalage * (projectiles.Length + 1) / 2;

            //base.Shoot();
            for (int i = 0; i < projectiles.Length; i++)
            {
                angleProjectile.angleDecalage = angleProjectile.angleDecalage + decalage;
                GameObject.Instantiate(projectiles[i], transform.position, Quaternion.identity);
            }
            BulletInMag--;
            if (BulletInMag <= 0)
            {
                StartCoroutine(Reload());
            }
            yield return new WaitForSeconds(attackDelay);
            isAttacking = false;
        }

    }

  
}
