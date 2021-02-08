using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusilAlien : DistanceWeapon
{
    protected bool IsWeaponLoad = false;
    protected bool IsLoading = false;
    [SerializeField]protected float LoadingTime;

    protected override void Update()
    {
        //base.Update();
        if (isTotalDestructionModule && !damagealReadyMult)
        {
            damagealReadyMult = true;
            damage *= damageMultiplier;
        }

        InfiniteAmmo = isUnlimitedAmmoModule;
        GetAttackDirection();
        if (OkToShoot && !IsLoading)
        {
            IsLoading = true;
            StartCoroutine("LoadingTimer");
            print("starCo");
        }
        if (!OkToShoot && IsLoading)
        {
            IsLoading = false;
            StopCoroutine("LoadingTimer");
            print("stopCo");
        }

        if (IsWeaponLoad && !OkToShoot)
        {
            CoroutineManager.Instance.StartCoroutine(Shoot());
            IsWeaponLoad = false;
            print("Shoot");
        }


        DisplayAmmo();

        if (isCanonRapideModule && !CadenceAlreadyUp)
        {
            CadenceAlreadyUp = true;
            attackDelay /= CadenceMultiplier;
        }

        if (isPrecisionModule && !PrecisionAlreadyUp)
        {
            PrecisionAlreadyUp = true;
            Dispersion /= PrecisionMultiplier;
        }
        if (isFastReloadModule && !FastReloadAlreadyActive)
        {
            FastReloadAlreadyActive = true;
            ReloadDelay /= ReloadSpeedMultiplier;
        }

    }

    protected IEnumerator LoadingTimer()
    {
         
        yield return new WaitForSeconds(LoadingTime);
        IsWeaponLoad = true;
        print("weaponLoad");
    }

    //protected void Loading()
    //{
    //    while (OkToShoot)
    //    {

    //    }
    //    if (IsWeaponLoad)
    //    {
    //        CoroutineManager.Instance.StartCoroutine(Shoot());
    //    }
    //}
}
