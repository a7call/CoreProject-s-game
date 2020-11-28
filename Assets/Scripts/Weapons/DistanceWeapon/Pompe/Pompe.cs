﻿using System.Collections;
using UnityEngine;

public class Pompe : DistanceWeapon
{
   
    [SerializeField] GameObject[] projectiles = null;
    [SerializeField] int angleTir = 0;
    private PompeProjectiles PompeProjectile;

    private void Start()
    {
        foreach(GameObject projectile in projectiles)
        {
            PompeProjectile = projectile.GetComponent<PompeProjectiles>();
        }
    }


    protected override IEnumerator Shoot()
    {

        if (!isAttacking && BulletInMag > 0 && !IsReloading)
        {
            

            isAttacking = true;
            float decalage = angleTir / (projectiles.Length - 1);
            PompeProjectile.angleDecalage = -decalage * (projectiles.Length + 1) / 2;

            //base.Shoot();
            for (int i = 0; i < projectiles.Length; i++)
            {
                PompeProjectile.angleDecalage = PompeProjectile.angleDecalage + decalage;
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