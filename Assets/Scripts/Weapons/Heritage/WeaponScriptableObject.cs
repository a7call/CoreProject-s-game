using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wanderer.CharacterStats;

[CreateAssetMenu(fileName = "new  Weapon", menuName = "Weapon")]
public class WeaponScriptableObject : Item
{
    public float damage;
    public float delayBetweenAttack;
    public LayerMask enemyLayer;
    public Sprite image;
    public float Dispersion;
    public int MagSize;
    public float ReloadDelay;
    public int AmmoStock;

    public override void Equip(PlayerMouvement p)
    {
        if (delayBetweenAttack != 0)
            p.attackSpeed.AddModifier(new StatModifier(delayBetweenAttack, StatModType.Flat, this));
        if (damage != 0)
            p.damage.AddModifier(new StatModifier(damage, StatModType.Flat,this));

    }

    public override void Unequip(PlayerMouvement p)
    {
        p.attackSpeed.RemoveAllModifiersFromSource(this);
        p.damage.RemoveAllModifiersFromSource(this);
    }
}
