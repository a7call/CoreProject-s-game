using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArretTemporel : ActiveObjects
{
    [SerializeField] protected float activeTime;
    protected GameObject[] ennemis;

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

        foreach (GameObject enemy in ennemis)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.currentState = Enemy.State.Paralysed;
            
        }

        yield return new WaitForSeconds(activeTime);
        
    }
}
