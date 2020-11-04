﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBall : DistanceWeapon
{
    [SerializeField] GameObject[] projectiles;
    [SerializeField] protected PlayerProjectiles[] ScriptProj;
    private int i;


    protected override IEnumerator Shoot()
    {
        if (!isAttacking && BulletInMag > 0 && !IsReloading)
        {
            float decalage = Random.Range(-Dispersion, Dispersion + 1);
            i = Random.Range(0, projectiles.Length);
            ScriptProj[i].Dispersion = decalage;
            isAttacking = true;
            GameObject.Instantiate(projectiles[i], transform.position, Quaternion.identity);
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

