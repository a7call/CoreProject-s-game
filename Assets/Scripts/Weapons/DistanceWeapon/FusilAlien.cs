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
            
        }
        if (!OkToShoot && IsLoading)
        {
            IsLoading = false;
            StopCoroutine("LoadingTimer");
            
        }

        if (IsWeaponLoad && !OkToShoot)
        {
            CoroutineManager.Instance.StartCoroutine(Shoot());
            IsWeaponLoad = false;
            
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
        
    }

   
}
