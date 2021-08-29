using System.Collections;
using UnityEngine;

public abstract class ShootableWeapon : Weapons, IShootableWeapon
{
    public WeaponScriptableObject WeaponData
    {
        get
        {
            return DistanceWeaponDataCast;
        }
    }
    public DistanceWeaponScriptableObject DistanceWeaponDataCast;


    protected override void ResetWeaponState()
    {
        base.ResetWeaponState();
        IsReloading = false;
        OkToShoot = false;
    }

    #region Unity Mono

    protected void Update()
    {

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

    protected override void SetData()
    {
        projectile = DistanceWeaponDataCast.projectile;
        enemyLayer = WeaponData.enemyLayer;
        image = WeaponData.image;
        screenShakeMagnitude = WeaponData.screenShakeMagnitude;
        screenShakeTime = WeaponData.screenShakeTime;
        ShootAudioName = WeaponData.shootAudioName;
        ReloadAudioName = WeaponData.reloadAudioName;
    }

    protected override void SetStatDatasAndInitialization()
    {
        damage = player.damage.Value;
        attackDelay = player.attackSpeed.Value;
        dispersion = player.dispersion.Value;
        ProjectileSpeed = player.projectileSpeed.Value;
        magSize = player.magSize.Value;
        reloadDelay = player.reloadSpeed.Value;
        ammoStock = player.ammoStock.Value;

        InitializeMag();
    }


    #endregion

    #region Shoot logic

    protected GameObject projectile;
    protected PlayerProjectiles Proj { get; private set; }
    protected float ProjectileSpeed { get; private set; }
    protected float dispersion { get; private set; }
    public bool OkToShoot { get; set; }

    protected abstract IEnumerator Shooting();
    protected void Shoot()
    {
        if (CameraController.instance != null)
            CameraController.instance.StartShakeD(screenShakeTime, screenShakeMagnitude, (attackPoint.position - transform.position).normalized);

        StartCoroutine(Shooting());
    }

    protected void ProjectileSetUp(float dispersion, float damage, float projectileSpeed, LayerMask enemyLayer, float timeAlive = 10f)
    {
        GameObject instantiatedProjectile = Instantiate(projectile, attackPoint.position, transform.rotation);
        instantiatedProjectile.GetComponent<Projectile>().SetProjectileDatas(damage, dispersion, projectileSpeed, enemyLayer, player.gameObject, timeAlive, transform.right);
    }

    public bool IsAbleToShoot()
    {
        return OkToShoot && !isAttacking && BulletInMag > 0;
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
                Shoot();
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

