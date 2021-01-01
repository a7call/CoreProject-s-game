using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortionGrenade : ExplosivesModule
{


    protected override void Start()
    {
        base.Start();
        StartCoroutine(ExplosionOnEnemy());
    }
   

    protected override void Update()
    {
        base.Update();
        if (isNotMoving && !isAlreadyActive)
        {
            isAlreadyActive = true;
            StartCoroutine(ExplosionOnEnemy());
        }
    }
}
