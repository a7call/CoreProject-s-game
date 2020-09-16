using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCacHealth : Type1Health
{
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

    protected override void SetData()
    {
        base.SetData();
    }

    private void Start()
    {
        SetData();
    }
}
