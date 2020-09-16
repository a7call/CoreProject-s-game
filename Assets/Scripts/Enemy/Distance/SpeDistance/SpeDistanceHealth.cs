using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeDistanceHealth : Type2Health
{
    void Start()
    {
        SetData();
        SetMaxHealth();
    }
    protected override void SetData()
    {
        base.SetData();
    }

    protected override void SetMaxHealth()
    {
        base.SetMaxHealth();
    }

    protected override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

    }

    protected override IEnumerator WhiteFlash()
    {
        return base.WhiteFlash();
    }
}
