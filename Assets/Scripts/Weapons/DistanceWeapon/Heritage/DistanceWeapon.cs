using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class DistanceWeapon : Weapons, IShootableWeapon
{
       
    public WeaponScriptableObject WeaponData
    {
        get
        {
            return DistanceWeaponDataCast;
        }
    }
    public DistanceWeaponScriptableObject DistanceWeaponDataCast;

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
        OkToShoot = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (IsReloading)
            return;

        if (BulletInMag <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
            

        StartShootingProcess();
    }

    #endregion

    #region Datas && References 
    protected override void GetReferences()
    {
        base.GetReferences();
        Proj = projectile.GetComponent<PlayerProjectiles>();
    }
    private void InitializeMag()
    {
        BulletInMag = (int)magSize;
    }

    protected virtual void SetData()
    {
        projectile = DistanceWeaponDataCast.projectile;
        enemyLayer = WeaponData.enemyLayer;
        image = WeaponData.image;
        screenShakeMagnitude = WeaponData.screenShakeMagnitude;
        screenShakeTime = WeaponData.screenShakeTime;
        ShootAudioName = WeaponData.shootAudioName;
        ReloadAudioName = WeaponData.reloadAudioName;
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
        if (CameraController.instance != null) CameraController.instance.StartShakeD(screenShakeTime, screenShakeMagnitude, (attackPoint.position - transform.position).normalized);

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

            AudioManagerEffect.GetInstance().Play(ShootAudioName, player.gameObject);

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
        IsReloading = true;
        AudioManagerEffect.GetInstance().Play(ReloadAudioName, player.gameObject);
        yield return new WaitForSeconds(reloadDelay);
        if (ammoStock + BulletInMag >= magSize)
        {
            ammoStock = ammoStock + BulletInMag;
            ammoStock = ammoStock - magSize;
            BulletInMag = (int)magSize;
        }
        else if (ammoStock + BulletInMag <= magSize)
        {
            ammoStock = ammoStock + BulletInMag;
            BulletInMag = (int)ammoStock;
            ammoStock = ammoStock - BulletInMag;
        }
        IsReloading = false;
    }

    public void toReload()
    {
        if (BulletInMag != magSize && !IsReloading && (ammoStock != 0))
        {
            StartCoroutine(Reload());
        }
    }

    #endregion  
}
