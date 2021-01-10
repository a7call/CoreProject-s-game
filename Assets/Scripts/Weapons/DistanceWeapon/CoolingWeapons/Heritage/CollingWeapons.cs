using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollingWeapons : DistanceWeapon
{
    public CollingWeaponScriptableObject CollingWeaponScriptable;
    protected int count =0;
    protected bool IsToHot = false;
    protected bool IsCooling = false;
    protected float coolingTime = 3f;
    protected float coolingDelay = 0.5f;
    protected int countMax = 6;
    protected float knockBackforce = 15f;
    protected float knockBackTime = 0.1f;

    protected override void Awake()
    {
        this.enabled = false;
        SetData();
        AmmoText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<Text>();
        AmmoStockText = GameObject.FindGameObjectWithTag("AmmoStockText").GetComponent<Text>();
    }

    protected override void SetData()
    {
        enemyLayer = CollingWeaponScriptable.enemyLayer;
        damage = CollingWeaponScriptable.damage;
        attackDelay = CollingWeaponScriptable.AttackDelay;
        image = CollingWeaponScriptable.image;
        coolingDelay = CollingWeaponScriptable.coolingDelay;
        coolingTime = CollingWeaponScriptable.coolingTime;
        knockBackforce = CollingWeaponScriptable.knockBackforce;
        knockBackTime = CollingWeaponScriptable.knockBackTime;
    }
}
