using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollingWeapons : BaseShootableWeapon
{
    public CollingWeaponScriptableObject CollingWeaponScriptable;
    protected int count =0;
    protected bool IsToHot = false;
    protected bool IsCooling = false;
    protected float coolingTime;
    protected float coolingDelay;
    protected int countMax;
    protected float knockBackforce;
    protected float knockBackTime;

    protected override void Awake()
    {
        this.enabled = false;
        SetData();
    }

    protected override void SetData()
    {
        enemyLayer = CollingWeaponScriptable.enemyLayer;
        damage = CollingWeaponScriptable.damage;
        attackDelay = CollingWeaponScriptable.delayBetweenAttack;
        //image = CollingWeaponScriptable.image;
        //coolingDelay = CollingWeaponScriptable.coolingDelay;
        //coolingTime = CollingWeaponScriptable.coolingTime;
        //knockBackforce = CollingWeaponScriptable.knockBackforce;
        //knockBackTime = CollingWeaponScriptable.knockBackTime;
        //countMax = CollingWeaponScriptable.countMax;
    }
}
