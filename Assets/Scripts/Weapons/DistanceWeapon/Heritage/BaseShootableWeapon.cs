﻿using System.Collections;
using UnityEngine;

public class BaseShootableWeapon : ShootableWeapon
{
    protected override IEnumerator Shooting()
    {
        BulletInMag--;
        float Dispersion = Random.Range(-dispersion, dispersion);
        ProjectileSetUp(Dispersion, damage, ProjectileSpeed, enemyLayer);
        yield return new WaitForSeconds(player.attackSpeed.Value);
        isAttacking = false;
    }

}