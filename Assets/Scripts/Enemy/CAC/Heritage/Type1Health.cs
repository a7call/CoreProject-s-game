using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type1Health : EnemyHealth
{
    // Rien d'autre pour le moment 
    [SerializeField] protected Type1ScriptableObject Type1Data;

    protected virtual void SetData()
    {
        maxHealth = Type1Data.maxHealth;
        whiteMat = Type1Data.whiteMat;
        defaultMat = Type1Data.defaultMat;
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
