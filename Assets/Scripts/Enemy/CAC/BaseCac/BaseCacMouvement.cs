using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCacMouvement : Type1
{
    protected override void Aggro()
    {
        base.Aggro();
    }

    protected override void Patrol()
    {
        base.Patrol();
    }

    protected  void Update()
    {
        Aggro();
        Patrol();
    }

    protected override void SetData()
    {
        base.SetData();
    }
    private void Start()
    {
        SetFirstPatrolPoint();
        SetData();
    }
}
