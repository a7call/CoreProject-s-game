using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArretTemporel : CdObjects
{
    [SerializeField] protected float activeTime;
    protected GameObject[] ennemis;
    protected float ProjSpeed = 0;

    protected override void Update()
    {
        base.Update();
        if (UseModule)
        {
            UseModule = false;
            StartCoroutine(ActiveTime());
        }

        
    }

    protected virtual IEnumerator ActiveTime()
    {

        GameObject[] ennemis = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("EnemyProjectil");

        foreach (GameObject enemy in ennemis)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
        }

        foreach (GameObject proj in projectiles)
        {
            Projectile projScript = proj.GetComponent<Projectile>();
            ProjSpeed = projScript.projectileSpeed;
            projScript.projectileSpeed = 0;

        }


        yield return new WaitForSeconds(activeTime);

        foreach (GameObject enemy in ennemis)
        {
            if (enemy == null)
            {
                continue;
            }
            Enemy enemyScript = enemy.GetComponent<Enemy>();
           // enemyScript.AIMouvement.canMove = true;

        }

        foreach (GameObject proj in projectiles)
        {
            if (proj == null)
            {
                continue;
            }
            Projectile projScript = proj.GetComponent<Projectile>();
            projScript.projectileSpeed = ProjSpeed;

        }
    }
}
