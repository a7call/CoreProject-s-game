using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Player
{
    WeaponsManagerSelected weaponManager;
    ActiveObjectManager activeObjectManager;

    protected override void Awake()
    {
        base.Awake();
        activeObjectManager = GetComponentInChildren<ActiveObjectManager>();
        weaponManager = GetComponentInChildren<WeaponsManagerSelected>();
    }
    public void OnShoot()
    {

        if (weaponManager.isPlayingDistance)
        {
            weaponManager.GetComponentInChildren<DistanceWeapon>().toShoot();
        }
        else if (weaponManager.isPlayingCac)
        {
            weaponManager.GetComponentInChildren<CacWeapons>().ToAttack();
        }

    }

    public void OnReload()
    {
        if (weaponManager.isPlayingDistance)
        {
            weaponManager.GetComponentInChildren<DistanceWeapon>().toReload();
        }
    }


    public void OnUseObject()
    {
        if(activeObjectManager.GetComponentInChildren<ActiveObjects>())
        {
            activeObjectManager.GetComponentInChildren<ActiveObjects>().ToUseModule();
        }
    }
}