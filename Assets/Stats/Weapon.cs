using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wanderer.CharacterStats;

[CreateAssetMenu(fileName = "new Weapon", menuName = "WeaponItem")]
public class Weapon : Item
{
    public float delayBetweenAttack = 2;
    public float damage = 1;

    public override void Equip(PlayerMouvement p)
    {
        if (delayBetweenAttack != 0)
            p.attackSpeed.AddModifier(new StatModifier(delayBetweenAttack, StatModType.Flat));
          

        if (damage != 0)
            p.damage.AddModifier(new StatModifier(damage,StatModType.Flat));
        
    }

    public override void Unequip(PlayerMouvement p)
    {
        p.attackSpeed.RemoveAllModifiersFromSource(this);
        p.damage.RemoveAllModifiersFromSource(this);
    }
}
