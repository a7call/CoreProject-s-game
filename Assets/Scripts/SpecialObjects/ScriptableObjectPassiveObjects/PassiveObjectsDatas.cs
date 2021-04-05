using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wanderer.CharacterStats;

[CreateAssetMenu(fileName = "new  PassiveObject", menuName = "PassiveObject")]
public class PassiveObjectsDatas : Item
{
    [Header("Projectile")]
    public float damage;
    public float projectileSpeed;
    public float dispersion;
    public float knockBackForce;
    public float explosionDamage;
    public float imolationDamage;
    public float slow;

    [Header("Chance of drops")]
    public float chanceToDropCoins;
    public float chanceToDropObjects;
    public float chanceToDropAmmos;
    public float chanceToDropHeart;
    
    [Header("Weapon")]
    public float reloadSpeed;
    public float cadence;
    public float attackSpeed;
    public float range;


    [Header("Player")]
    public float maxHealth;
    public float moveSpeed;


  
    public override void Equip(Player p) 
    {
        if (damage != 0)
            p.damage.AddModifier(new StatModifier(damage, StatModType.PercentMult, this));
    }
    public override void Unequip(Player p)
    {
        p.damage.RemoveAllModifiersFromSource(this);
    }
}
