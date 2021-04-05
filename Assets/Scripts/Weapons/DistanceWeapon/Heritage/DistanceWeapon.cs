using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DistanceWeapon : Weapons
{
    
    public DistanceWeaponScriptableObject DistanceWeaponData;
    protected GameObject projectile;
    protected PlayerProjectiles Proj;
    protected float Dispersion;
    protected bool IsReloading;
    [HideInInspector]
    public bool OkToShoot = true;
    [HideInInspector]
    public int BulletInMag;
    protected float ReloadDelay;
    protected int MagSize;
    protected Text AmmoText;
    protected Text AmmoStockText;
    protected bool InfiniteAmmo;
    [HideInInspector]
    public int AmmoStock;
    #region Module Et des betises
    //CanonRapideModule
    [HideInInspector]
    protected bool CadenceAlreadyUp = false;
    [HideInInspector]
    public static bool isCanonRapideModule;
    [HideInInspector]
    public static int CadenceMultiplier;

    //PrecisionModule
    [HideInInspector]
    protected bool PrecisionAlreadyUp = false;
    [HideInInspector]
    public static bool isPrecisionModule;
    [HideInInspector]
    public static int PrecisionMultiplier;

    //FastReloadModule
    [HideInInspector]
    protected bool FastReloadAlreadyActive = false;
    [HideInInspector]
    public static bool isFastReloadModule;
    [HideInInspector]
    public static float ReloadSpeedMultiplier;

    //UnlimitedAmmoModule
    [HideInInspector]
    public static bool isUnlimitedAmmoModule;
    #endregion
    [HideInInspector]
    public Sprite image;


    #region Unity Mono
    protected override void Awake()
    {
        base.Awake();
        SetData();
        InitializeMag();
        GetReferences();
    }



    protected override  void OnEnable()
    {
        base.OnEnable();
        IsReloading = false;
        isAttacking = false;  
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        InfiniteAmmo = isUnlimitedAmmoModule;
        DisplayAmmo();

        if (IsReloading)
            return;

        if (BulletInMag <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
            

        StartShootingProcess();


        #region Module et des betises
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
        #endregion

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, 0.4f);
        Gizmos.color = Color.red;
    }
     bool isalreadyDisable = false;
    private void OnDisable()
    {
        GetComponent<SpriteRenderer>().sprite = image;
    }

    #endregion

    #region Datas && References 
    private void GetReferences()
    {
        AmmoText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<Text>();
        AmmoStockText = GameObject.FindGameObjectWithTag("AmmoStockText").GetComponent<Text>();
        Proj = projectile.GetComponent<PlayerProjectiles>();
    }
    private void InitializeMag()
    {
        BulletInMag = MagSize;
    }

    protected virtual void SetData()
    {
        projectile = DistanceWeaponData.projectile;
        enemyLayer = DistanceWeaponData.enemyLayer;
        damage = DistanceWeaponData.damage;
        attackDelay = DistanceWeaponData.delayBetweenAttack;
        Dispersion = DistanceWeaponData.Dispersion;
        MagSize = DistanceWeaponData.MagSize;
        ReloadDelay = DistanceWeaponData.ReloadDelay;
        AmmoStock = DistanceWeaponData.AmmoStock;
        image = DistanceWeaponData.image;

    }


    #endregion


    #region Shoot logic
    protected virtual IEnumerator Shoot()
    {
        float decalage = Random.Range(-Dispersion, Dispersion);      
        Proj.Dispersion = decalage;
        BulletInMag--;
        Instantiate(projectile, attackPoint.position, Quaternion.identity);
        print(playerMouv.attackSpeed.Value);
        yield return new WaitForSeconds(playerMouv.attackSpeed.Value);
        isAttacking = false;
    }

    private bool IsAbleToShoot()
    {
        return OkToShoot && !isAttacking  && BulletInMag > 0 && !PauseMenu.isGamePaused;
    }

    private void StartShootingProcess()
    {
        if (IsAbleToShoot())
        {
            isAttacking = true;
            if (animator)
            {
                animator.SetTrigger("isAttacking");
            }
            else
            {
                StartCoroutine(Shoot());
            }
           
        }
    }


    #endregion


    #region Reload logic
    protected IEnumerator Reload()
    {
        //if (BulletInMag != MagSize && !IsReloading && (AmmoStock != 0 | InfiniteAmmo))
        //{

        IsReloading = true;
        yield return new WaitForSeconds(ReloadDelay);
        if (AmmoStock + BulletInMag >= MagSize && !InfiniteAmmo)
        {
            AmmoStock = AmmoStock + BulletInMag;
            AmmoStock = AmmoStock - MagSize;
            BulletInMag = MagSize;
        }
        else if (AmmoStock + BulletInMag <= MagSize && !InfiniteAmmo)
        {
            AmmoStock = AmmoStock + BulletInMag;
            BulletInMag = AmmoStock;
            AmmoStock = AmmoStock - BulletInMag;
        }
        else if (isUnlimitedAmmoModule)
        {
            BulletInMag = MagSize;
        }

        IsReloading = false;
    }

    public void toReload()
    {
        if (BulletInMag != MagSize && !IsReloading && (AmmoStock != 0 | InfiniteAmmo))
        {
            StartCoroutine(Reload());
        }
    }

    #endregion

    

    #region UI

    protected void DisplayAmmo()
    {
        if (!InfiniteAmmo)
        {
            AmmoText.text = BulletInMag.ToString();
            AmmoStockText.text = AmmoStock.ToString();
        }

        else
        {
            AmmoText.text = BulletInMag.ToString();
            AmmoStockText.text = "Infini";
        }
    }
    #endregion

}
