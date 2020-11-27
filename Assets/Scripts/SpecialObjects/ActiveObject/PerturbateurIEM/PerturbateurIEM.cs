using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerturbateurIEM : ActiveObjects
{
    [SerializeField] protected float ActiveTime = 0;
    protected override void Update()
    {
        base.Update();
        if (UseModule)
        {
            StartCoroutine(ActiveTimeIEM());
            Enemy.isPerturbateurIEM = true;
            UseModule = false;
        }
    }

    protected IEnumerator ActiveTimeIEM()
    {
        yield return new WaitForSeconds(ActiveTime);
        Enemy.isPerturbateurIEM = false;
    }
}
