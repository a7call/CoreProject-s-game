using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArretTemporel : ActiveObjects
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
            
        }

        foreach (GameObject proj in projectiles)
        {
            Projectile projScript = proj.GetComponent<Projectile>();
            ProjSpeed = projScript.speed;
            projScript.speed = 0;

        }


        yield return new WaitForSeconds(activeTime);

        Enemy.isArretTemporel = false;
        foreach (GameObject enemy in ennemis)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.currentState = Enemy.State.Patrolling;

        }

        foreach (GameObject proj in projectiles)
        {
            Projectile projScript = proj.GetComponent<Projectile>();
            projScript.speed = ProjSpeed;

        }
    }
}
