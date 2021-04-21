using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wanderer.CharacterStats;


[CreateAssetMenu(fileName = "new Cac Weapon", menuName = "CacWeapon")]
public class CaCWeaponScriptableObject : WeaponScriptableObject
{
    public float attackRadius;
    public override void Equip(Player p)
    {
        base.Equip(p);
        if (attackRadius != 0)
            p.attackRadius.AddModifier(new StatModifier(attackRadius, StatModType.Flat, this));
        

    }
    public override void Unequip(Player p)
    {
        base.Unequip(p);
        p.attackRadius.RemoveAllModifiersFromSource(this);;
    }
}
