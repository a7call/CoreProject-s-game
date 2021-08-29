using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerturbateurIEM : StacksObjects
{
    [SerializeField] protected float ActiveTime = 0;
    protected override void Update()
    {
        base.Update();
        if (UseModule)
        {
            ActiveTimeIEM();
            UseModule = false;
        }
    }

    protected void ActiveTimeIEM()
    {
        AIProjectile[] enemyProjectile = FindObjectsOfType<AIProjectile>();
        if (enemyProjectile.Length > 0)
        {
            foreach(AIProjectile projectile in enemyProjectile)
            {
                Destroy(projectile.gameObject);
            }
            
        }
    }
}
