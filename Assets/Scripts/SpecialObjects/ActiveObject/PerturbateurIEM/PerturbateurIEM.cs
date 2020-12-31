using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerturbateurIEM : ActiveObjects
{
    [SerializeField] protected float ActiveTime = 0;
    protected override void Update()
    {
        if (UseModule)
        {
            ActiveTimeIEM();
            UseModule = false;
        }
    }

    protected void ActiveTimeIEM()
    {
        Projectile enemyProjectile = FindObjectOfType<Projectile>();
        if(enemyProjectile) Destroy(enemyProjectile.gameObject);
    }
}
