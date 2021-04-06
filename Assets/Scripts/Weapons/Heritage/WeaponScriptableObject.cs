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


    public override void Equip(Player p)
    {
        if (delayBetweenAttack != 0)
            p.attackSpeed.AddModifier(new StatModifier(delayBetweenAttack, StatModType.Flat, this));
        if (damage != 0)
            p.damage.AddModifier(new StatModifier(damage, StatModType.Flat,this));

    }

    public override void Unequip(Player p)
    {
        p.attackSpeed.RemoveAllModifiersFromSource(this);
        p.damage.RemoveAllModifiersFromSource(this);
    }
}
