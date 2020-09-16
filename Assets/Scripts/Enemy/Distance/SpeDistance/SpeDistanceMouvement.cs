using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeDistanceMouvement : Type2Mouvement
{
    void Start()
    {
        SetFirstPatrolPoint();
        SetData();
    }
    private void Update()
    {
        Aggro();
        Patrol();
    }
    protected override void Aggro()
    {
        base.Aggro();
    }


    protected override void Patrol()
    {
        base.Patrol();
    }

    protected override void SetFirstPatrolPoint()
    {
        base.SetFirstPatrolPoint();
    }

    protected override void SetData()
    {
        base.SetData();
    }
}
