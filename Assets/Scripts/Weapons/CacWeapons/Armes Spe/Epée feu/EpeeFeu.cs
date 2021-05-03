using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpeeFeu : CacWeapons
{

    [SerializeField] protected float burnTime;
    [SerializeField] protected float burnDamageTotalAmount;
    private DotAbility _burningAbility;
    protected override void Awake()
    {
        base.Awake();
        _burningAbility = new BurnAbility(burnDamageTotalAmount, burnTime);
        
    }


    protected override void AttackAppliedOnEnemy(Collider2D[] enemyHit)
    {
        base.AttackAppliedOnEnemy(enemyHit);
        foreach (Collider2D enemy in enemyHit)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                ICharacter enemyAsCarac = enemy.GetComponent<ICharacter>();
                 _burningAbility.ApplyEffect(enemyAsCarac);
            }
        }
    }
 
}
