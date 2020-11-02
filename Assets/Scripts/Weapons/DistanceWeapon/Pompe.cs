using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pompe : DistanceWeapon
{
   
    [SerializeField] GameObject[] projectiles;
    [SerializeField] int angleTir;
    [SerializeField] PompeProjectiles angleProjectile;

   


    protected override IEnumerator Shoot()
    {
        
        if (!isAttacking && !IsMagEmpty)
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
                IsMagEmpty = true;
                StartCoroutine(Reload());
            }
            yield return new WaitForSeconds(attackDelay);
            isAttacking = false;
        }

    }

  
}
