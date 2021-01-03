using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : ExplosivesModule
{
    DetonatorModule detonatorModule;


    protected override void Start()
    {
        base.Start();
        detonatorModule = FindObjectOfType<DetonatorModule>();
        
    }

    protected override void Update()
    {
        base.Update();
        if (detonatorModule.readyToExplode && detonatorModule.UseModule)
        {
            CoroutineManager.Instance.StartCoroutine(ExplosionOnEnemy());
            detonatorModule.readyToExplode = false;
            detonatorModule.UseModule = false;
            if (detonatorModule.numberOfUse < 1)
            {
                detonatorModule.isOutOfUse = true;
            }
        }
    }
}