using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wanderer.CharacterStats;


[CreateAssetMenu(fileName = "new Distance Weapon", menuName = "DistanceWeapon")]

public class DistanceWeaponScriptableObject : WeaponScriptableObject
{
   
    public GameObject projectile;
    public float dispersion;
    public int magSize;
    public float reloadDelay;
    public int ammoStock;

    public override void Equip(Player p)
    {
        base.Equip(p);
        if (dispersion != 0)
            p.dispersion.AddModifier(new StatModifier(dispersion, StatModType.PercentMult, this));
        if(reloadDelay != 0)
            p.reloadSpeed.AddModifier(new StatModifier(reloadDelay, StatModType.Flat, this));
        if (ammoStock != 0)
            p.ammoStock.AddModifier(new StatModifier(ammoStock, StatModType.Flat, this));
        if (magSize != 0)
            p.magSize.AddModifier(new StatModifier(magSize, StatModType.Flat, this));

    }
    public override void Unequip(Player p)
    {
        base.Unequip(p);
        p.dispersion.RemoveAllModifiersFromSource(this);
        p.reloadSpeed.RemoveAllModifiersFromSource(this);
        p.ammoStock.RemoveAllModifiersFromSource(this);
        p.magSize.RemoveAllModifiersFromSource(this);
    }


}
