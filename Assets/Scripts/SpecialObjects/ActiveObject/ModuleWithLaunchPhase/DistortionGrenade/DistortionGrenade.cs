using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortionGrenade : ExplosivesModule
{


    protected override void Start()
    {
        base.Start();
        CoroutineManager.GetInstance().StartCoroutine(ExplosionOnEnemy());
    }
   

    protected override void Update()
    {
        base.Update();
    }
}
