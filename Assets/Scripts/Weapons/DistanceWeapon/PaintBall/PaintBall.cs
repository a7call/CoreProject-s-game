using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBall : DistanceWeapon
{
    [SerializeField] GameObject[] projectiles = null;
     protected PlayerProjectiles ScriptProj = null;
    private int i;

    private void Start()
    {
        foreach (GameObject projectile in projectiles)
        {
            ScriptProj = projectile.GetComponent<PlayerProjectiles>();
        }
    }


    protected override IEnumerator Shoot()
    {
        if (!isAttacking && BulletInMag > 0 && !IsReloading)
        {
            float decalage = Random.Range(-dispersion, dispersion + 1);
            i = Random.Range(0, projectiles.Length);
            ScriptProj.dispersion = decalage;
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

