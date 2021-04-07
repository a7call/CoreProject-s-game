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
        Enemy.isArretTemporel = true;
        GameObject[] ennemis = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("EnemyProjectil");

        foreach (GameObject enemy in ennemis)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.currentState = Enemy.State.Freeze;
            enemyScript.isreadyToAttack = false;
            
        }

        foreach (GameObject proj in projectiles)
        {
            Projectile projScript = proj.GetComponent<Projectile>();
            ProjSpeed = projScript.projectileSpeed;
            projScript.projectileSpeed = 0;

        }


        yield return new WaitForSeconds(activeTime);

        Enemy.isArretTemporel = false;
        foreach (GameObject enemy in ennemis)
        {
            if (enemy == null)
            {
                continue;
            }
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.currentState = Enemy.State.Chasing;
            enemyScript.aIPath.canMove = true;
            enemyScript.isreadyToAttack = true;

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
