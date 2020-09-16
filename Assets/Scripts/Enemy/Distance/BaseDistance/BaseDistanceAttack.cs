using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDistanceAttack : Type2Attack
{

    private void Start()
    {
        SetData();
    }

    private void Update()
    {
        StartCoroutine("CanShoot");
    }

    protected override IEnumerator CanShoot()
    {
        return base.CanShoot();
    }

    protected override void ResetAggro()
    {
        base.ResetAggro();
    }

    protected override void SetData()
    {
        base.SetData();
    }

    protected override void Shoot()
    {
        base.Shoot();
    }

}
