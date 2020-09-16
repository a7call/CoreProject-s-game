using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCacAttack : Type1Attack
{
 protected override void isInRange()
    {
        base.isInRange();
    }

    protected override void BaseAttack()
    {
        base.BaseAttack();
    }

    protected override void GetPlayerPos()
    {
        base.GetPlayerPos();
    }

    protected override void SetData()
    {
        base.SetData();
    }


    private void Update()
    {
        isInRange();
        GetPlayerPos();
    }

    private void Start()
    {
        SetData();
    }
}
