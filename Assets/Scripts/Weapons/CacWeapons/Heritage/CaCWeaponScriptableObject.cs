using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wanderer.CharacterStats;


[CreateAssetMenu(fileName = "new Cac Weapon", menuName = "CacWeapon")]
public class CaCWeaponScriptableObject : WeaponScriptableObject
{
    public float attackRadius;
    public float attackRange;
    public float knockFrontForce;
    public float knockFrontTime;
    public override void Equip(Player p)
    {
        base.Equip(p);
        if (attackRadius != 0)
            p.attackRadius.AddModifier(new StatModifier(attackRadius, StatModType.Flat, this));
        if (attackRange != 0)
            p.attackRange.AddModifier(new StatModifier(attackRange, StatModType.Flat, this));


    }
    public override void Unequip(Player p)
    {
        base.Unequip(p);
        p.attackRadius.RemoveAllModifiersFromSource(this);
        p.attackRange.RemoveAllModifiersFromSource(this);
    }
}
