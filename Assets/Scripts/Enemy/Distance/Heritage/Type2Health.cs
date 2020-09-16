using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type2Health : EnemyHealth
{
    [SerializeField] protected Type2ScriptableObject Type2Data;
   
    
    protected virtual void SetData()
    {
        maxHealth = Type2Data.maxHealth;
        whiteMat = Type2Data.whiteMat;
        defaultMat = Type2Data.defaultMat;
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
