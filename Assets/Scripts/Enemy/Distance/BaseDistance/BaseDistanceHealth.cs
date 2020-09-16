using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDistanceHealth : Type2Health
{
    // Start is called before the first frame update
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
