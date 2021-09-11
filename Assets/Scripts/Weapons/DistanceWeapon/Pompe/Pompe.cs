﻿using System.Collections;
using UnityEngine;

public class Pompe : ShootableWeapon
{

    private int numberOfProj = 8;

    protected override void GetReferences()
    {
        base.GetReferences();
        PoolManager.GetInstance().CreatePool(projectile, 20);
    }
    protected override IEnumerator Shooting()
    {
        BulletInMag--;
        for (int i = 0; i < numberOfProj; i++)
        {
            float Dispersion = Random.Range(-dispersion, dispersion);
            float projectileSpeed = Random.Range(ProjectileSpeed - ProjectileSpeed/3, ProjectileSpeed);
            ProjectileSetUp(Dispersion, damage, projectileSpeed, enemyLayer,0.5f);           
        }
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;

    }
}
