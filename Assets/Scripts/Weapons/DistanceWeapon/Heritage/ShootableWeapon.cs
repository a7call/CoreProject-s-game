using System;
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
        SpecialAttackDelay = WeaponData.specialAttackDelay;
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
    int shotValue;
    protected int ShotValue { get; set; }
    protected abstract IEnumerator Shooting();
    protected abstract IEnumerator SpecialShooting();
    protected void Shoot(int shotValue)
    {
        if (CameraController.instance != null)
            CameraController.instance.StartShakeD(screenShakeTime, screenShakeMagnitude, (attackPoint.position - transform.position).normalized);

        ShootSequence(shotValue);
    }

    protected void ShootSequence(int shotValue)
    {
        AudioManagerEffect.GetInstance().Play(ShootAudioName, player.gameObject);
        if (shotValue == 0)
        {
            isAttacking = true;
            StartCoroutine(Shooting());
        }
        else
            StartCoroutine(SpecialShooting());

    }

    protected void ProjectileSetUp(GameObject projectile, float dispersion, float damage, float projectileSpeed, LayerMask enemyLayer, float timeAlive = 10f)
    {
        GameObject instantiatedProjectile = PoolManager.GetInstance().ReuseObject(projectile, attackPoint.position, transform.rotation);
        instantiatedProjectile.GetComponent<SingleProjectile>().SetProjectileDatas(damage, dispersion, projectileSpeed, enemyLayer, player.gameObject, timeAlive, transform.right);
    }

    public bool IsAbleToShoot(int shotValue)
    {
        return (shotValue == 0 && !isAttacking && BulletInMag > 0) || (shotValue == 1 && isSpecialReady);
    }

    public void StartShootingProcess(int shotValue)
    {
        if (IsAbleToShoot(shotValue))
        {      
            Shoot(shotValue);
            animator.SetTrigger("isAttacking");
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

