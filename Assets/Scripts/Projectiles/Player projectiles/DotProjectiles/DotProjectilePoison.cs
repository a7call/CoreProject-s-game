using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotProjectilePoison : PlayerProjectiles
{
    [SerializeField] protected float damageAmount;
    [SerializeField] protected float duration;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        IAbility PoisonEffect = new PoisonAbility(damageAmount, duration);
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (!enemy.IsPoisoned)
                PoisonEffect.ApplyEffect(collision.GetComponent<ICharacter>());
        }
        base.OnTriggerEnter2D(collision);
        

    }

}
