using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusilAlien : BaseShootableWeapon
{
    protected bool IsWeaponLoad = false;
    protected bool IsLoading = false;
    [SerializeField]protected float LoadingTime;

    //protected void Update()
    //{

    //    //GetAttackDirection();
    //    if (OkToShoot && !IsLoading)
    //    {
    //        IsLoading = true;
    //        StartCoroutine("LoadingTimer");
            
    //    }
    //    if (!OkToShoot && IsLoading)
    //    {
    //        IsLoading = false;
    //        StopCoroutine("LoadingTimer");
            
    //    }

    //    if (IsWeaponLoad && !OkToShoot)
    //    {
    //        CoroutineManager.GetInstance().StartCoroutine(Shoot());
    //        IsWeaponLoad = false;
            
    //    }

    //}

    protected IEnumerator LoadingTimer()
    {
         
        yield return new WaitForSeconds(LoadingTime);
        IsWeaponLoad = true;
        
    }

   
}
