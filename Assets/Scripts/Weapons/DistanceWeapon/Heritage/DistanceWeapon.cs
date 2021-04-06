using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DistanceWeapon : Weapons
{
    
    public DistanceWeaponScriptableObject DistanceWeaponData;

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
            dispersion /= PrecisionMultiplier;
        }
        if (isFastReloadModule && !FastReloadAlreadyActive)
        {
            FastReloadAlreadyActive = true;
            reloadDelay /= ReloadSpeedMultiplier;
        }
        #endregion

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, 0.4f);
        Gizmos.color = Color.red;
    }

    public Sprite image;
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
        BulletInMag = magSize;
    }

    protected virtual void SetData()
    {
        projectile = DistanceWeaponData.projectile;
        enemyLayer = DistanceWeaponData.enemyLayer;
        damage = DistanceWeaponData.damage;
        attackDelay = DistanceWeaponData.delayBetweenAttack;
        dispersion = DistanceWeaponData.dispersion;
        magSize = DistanceWeaponData.magSize;
        reloadDelay = DistanceWeaponData.reloadDelay;
        ammoStock = DistanceWeaponData.ammoStock;
        image = DistanceWeaponData.image;

    }


    #endregion

    #region Shoot logic

    protected GameObject projectile;
    protected PlayerProjectiles Proj;
    protected float dispersion;

    [HideInInspector]
    public bool OkToShoot = true;

    protected virtual IEnumerator Shoot()
    {
        float decalage = Random.Range(-dispersion, dispersion);      
        Proj.dispersion = decalage;
        BulletInMag--;
        Instantiate(projectile, attackPoint.position, transform.rotation);
        print(player.attackSpeed.Value);
        yield return new WaitForSeconds(player.attackSpeed.Value);
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

    public int BulletInMag;
    protected float reloadDelay;
    protected int magSize;
    protected bool IsReloading;
    [HideInInspector]
    public int ammoStock;
    protected IEnumerator Reload()
    {
        //if (BulletInMag != magSize && !IsReloading && (ammoStock != 0 | InfiniteAmmo))
        //{

        IsReloading = true;
        yield return new WaitForSeconds(reloadDelay);
        if (ammoStock + BulletInMag >= magSize && !InfiniteAmmo)
        {
            ammoStock = ammoStock + BulletInMag;
            ammoStock = ammoStock - magSize;
            BulletInMag = magSize;
        }
        else if (ammoStock + BulletInMag <= magSize && !InfiniteAmmo)
        {
            ammoStock = ammoStock + BulletInMag;
            BulletInMag = ammoStock;
            ammoStock = ammoStock - BulletInMag;
        }
        else if (isUnlimitedAmmoModule)
        {
            BulletInMag = magSize;
        }

        IsReloading = false;
    }

    public void toReload()
    {
        if (BulletInMag != magSize && !IsReloading && (ammoStock != 0 | InfiniteAmmo))
        {
            StartCoroutine(Reload());
        }
    }

    #endregion

    #region UI
    protected Text AmmoText, AmmoStockText;
    protected bool InfiniteAmmo;
    [HideInInspector]
   
    protected void DisplayAmmo()
    {
        if (!InfiniteAmmo)
        {
            AmmoText.text = BulletInMag.ToString();
            AmmoStockText.text = ammoStock.ToString();
        }

        else
        {
            AmmoText.text = BulletInMag.ToString();
            AmmoStockText.text = "Infini";
        }
    }
    #endregion

}
