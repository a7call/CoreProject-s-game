using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBall : DistanceWeapon
{
    [SerializeField] GameObject[] projectiles;
    [SerializeField] protected PlayerProjectiles[] ScriptProj;
    private int i;


    protected override IEnumerator Shoot()
    {
        if (!isAttacking)
        {
            float decalage = Random.Range(-Dispersion, Dispersion + 1);
            i = Random.Range(0, projectiles.Length);
            ScriptProj[i].Dispersion = decalage;
            isAttacking = true;
            GameObject.Instantiate(projectiles[i], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(attackDelay);
            isAttacking = false;
        }

    }
}

