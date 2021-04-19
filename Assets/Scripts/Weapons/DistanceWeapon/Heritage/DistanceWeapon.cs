using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DistanceWeapon : Weapons, IShootableWeapon
{
    
    
    public WeaponScriptableObject DistanceWeaponData
    {
        get
        {
            return DistanceWeaponDataCast;
        }
    }
    public DistanceWeaponScriptableObject DistanceWeaponDataCast;

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
        SetData();
        base.Awake();
    }

    protected virtual void Start()
    {
        SetStatDatas();
        InitializeMag();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        IsReloading = false;
        isAttacking = false;
        OkToShoot = false;
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

    #region Bug de l'animation
   
    #endregion

    #endregion

    #region Datas && References 
    protected override void GetReferences()
    {
        base.GetReferences();
        AmmoText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<Text>();
        AmmoStockText = GameObject.FindGameObjectWithTag("AmmoStockText").GetComponent<Text>();
        Proj = projectile.GetComponent<PlayerProjectiles>();
    }
    private void InitializeMag()
    {
        BulletInMag = (int)magSize;
    }

    protected virtual void SetData()
    {
        projectile = DistanceWeaponDataCast.projectile;
        enemyLayer = DistanceWeaponData.enemyLayer;
        image = DistanceWeaponData.image;
    }

    protected virtual void SetStatDatas()
    {
       
        damage = player.damage.Value;
        attackDelay = player.attackSpeed.Value;
        dispersion = player.dispersion.Value;
        magSize = player.magSize.Value;
        reloadDelay = player.reloadSpeed.Value;
        ammoStock = player.ammoStock.Value;
       
    }


    #endregion

    #region Shoot logic

    protected GameObject projectile;
    protected PlayerProjectiles Proj;
    protected float dispersion;

    [HideInInspector]
    public bool OkToShoot { get; set; }

    float force = 100;
    protected virtual IEnumerator Shoot()
    {
        float decalage = Random.Range(-dispersion, dispersion);      
        Proj.dispersion = decalage;
        BulletInMag--;
        Instantiate(projectile, attackPoint.position, transform.rotation);
        yield return new WaitForSeconds(player.attackSpeed.Value);
        isAttacking = false;
    }

    public bool IsAbleToShoot()
    {
        return OkToShoot && !isAttacking  && BulletInMag > 0 && !PauseMenu.isGamePaused;
    }

    public void StartShootingProcess()
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

    [HideInInspector]
    public int BulletInMag;
    protected float reloadDelay;
    protected float magSize;
    protected bool IsReloading;
    [HideInInspector]
    public float ammoStock;
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
            BulletInMag = (int)magSize;
        }
        else if (ammoStock + BulletInMag <= magSize && !InfiniteAmmo)
        {
            ammoStock = ammoStock + BulletInMag;
            BulletInMag = (int)ammoStock;
            ammoStock = ammoStock - BulletInMag;
        }
        else if (isUnlimitedAmmoModule)
        {
            BulletInMag = (int)magSize;
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
