using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wanderer.CharacterStats;

public abstract class WeaponScriptableObject : Item
{
    public float damage;
    public float delayBetweenAttack;
    public LayerMask enemyLayer;
    public Sprite image;
    public float knockBackForce;
    public float knockBackTime;
    public float projectileSpeed;


    public override void Equip(Player p)
    {
        
        if (delayBetweenAttack != 0)
            p.attackSpeed.AddModifier(new StatModifier(delayBetweenAttack, StatModType.Flat, this));
        if (damage != 0)
            p.damage.AddModifier(new StatModifier(damage, StatModType.Flat,this));
        if (knockBackForce != 0)
            p.knockBackForce.AddModifier(new StatModifier(knockBackForce, StatModType.Flat, this));
        if (knockBackTime != 0)
            p.knockBackTime.AddModifier(new StatModifier(knockBackTime, StatModType.Flat, this));
        if (projectileSpeed != 0)
            p.projectileSpeed.AddModifier(new StatModifier(projectileSpeed, StatModType.Flat, this));
    }

    public override void Unequip(Player p)
    {
        p.attackSpeed.RemoveAllModifiersFromSource(this);
        p.damage.RemoveAllModifiersFromSource(this);
        p.knockBackForce.RemoveAllModifiersFromSource(this);
        p.knockBackTime.RemoveAllModifiersFromSource(this);
        p.projectileSpeed.RemoveAllModifiersFromSource(this);
    }
}
