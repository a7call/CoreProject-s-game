﻿using System.Collections;
using UnityEngine;
using Wanderer.Utils;


public abstract class GunUserDistance : Distance
{

    #region MonoBehiavour 

    protected EnemyWeaponManager WeaponManager { get; private set; }
    protected Animator WeaponAnimator { get; set; }

    protected override void Start()
    {
        base.Start();
        GetWeaponReferences();
    }
    void GetWeaponReferences()
    {
        WeaponManager = GetComponentInChildren<EnemyWeaponManager>();
        WeaponAnimator = WeaponManager.Weapon.animator;
        attackPoint = WeaponManager.Weapon.attackPoint;
        //Utils.AddAnimationEvent(EnemyConst.ATTACK_ANIMATION_NAME, EnemyConst.SHOOT_COROUTINE_EVENT_FUNCTION_NAME, WeaponAnimator);
    }

    #endregion

    #region Shoot 
    public override IEnumerator StartShootingProcessCo()
    {
        if (isReadyToAttack)
        {
            isReadyToAttack = false;
            // Wait for coroutine shoot to end
            yield return StartCoroutine(InstantiateProjectileCO());
            yield return StartCoroutine(RestCo(WeaponAnimator));
        }
    }
    public override IEnumerator InstantiateProjectileCO()
    {
        float decalage = Random.Range(-Dispersion, Dispersion);
        GameObject myProjectile = PoolManager.GetInstance().ReuseObject(Projetile, WeaponManager.Weapon.attackPoint.position, Quaternion.identity);
        myProjectile.GetComponent<SingleProjectile>().SetProjectileDatas(Damage, Dispersion, ProjetileSpeed, HitLayer, this.gameObject, 10, WeaponManager.aimDirection);
        yield return new WaitForEndOfFrame();
    }

    #endregion

    #region Animations

    private void UpdateWeaponMaterial(Material _material)
    {
        //WeaponManager.WeaponSR.material = _material;
    }

    #endregion

}
